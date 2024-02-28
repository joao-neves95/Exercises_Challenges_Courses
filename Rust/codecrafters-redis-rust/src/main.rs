mod command_handlers;
mod resp_parser;
mod shared;
mod utils;

use crate::{
    command_handlers::{
        handle_command_echo, handle_command_get_async, handle_command_ping,
        handle_command_set_async,
    },
    resp_parser::{parse_redis_resp_proc_command, shared::RespCommandNames},
    shared::AppContext,
    shared::InMemoryRecord,
};

use std::{collections::HashMap, sync::Arc, time::Duration};

use anyhow::{Error, Result};
use tokio::{
    io::{AsyncReadExt, AsyncWriteExt},
    net::{TcpListener, TcpStream},
    sync::Mutex,
};

const TCP_READ_TIMEOUT: Duration = Duration::from_millis(1000);

#[tokio::main]
async fn main() -> Result<(), Error> {
    let mem_db = Arc::new(Mutex::new(HashMap::<String, InMemoryRecord>::new()));

    let listener = TcpListener::bind("127.0.0.1:6379").await?;

    loop {
        let _ = match listener.accept().await {
            Ok((mut _tcp_stream, _)) => {
                println!("accepted new connection");

                let mem_db_arc_pointer = Arc::clone(&mem_db);

                tokio::spawn(async move {
                    match handle_client_connection(&mem_db_arc_pointer, &mut _tcp_stream).await {
                        Err(e) => {
                            println!("connection handling error: {}", e);
                        }
                        Ok(()) => (),
                    }

                    println!("finished handling request");
                });
            }
            Err(e) => {
                println!("tcp connection error: {}", e);
            }
        };
    }
}

async fn handle_client_connection<'a>(
    mem_db: &Arc<Mutex<HashMap<String, InMemoryRecord>>>,
    tcp_stream: &mut TcpStream,
) -> Result<(), anyhow::Error> {
    loop {
        let mut request_buffer: [u8; 1024] = [0; 1024];

        println!("reading request");

        match tokio::time::timeout(TCP_READ_TIMEOUT, tcp_stream.read(&mut request_buffer)).await {
        // match tcp_stream.read(&mut request_buffer).await {
            Err(e) => {
                println!("timeout while reading request - {}", e);
                break;
            }
            Ok(read_result) => {
                let request_len = read_result.unwrap();

                println!("request received of len {}", request_len);

                if request_len == 0 {
                    break;
                }

                let command_response = handle_command(mem_db, &request_buffer).await?;
                tcp_stream.write_all(command_response.as_bytes()).await?;
                tcp_stream.flush().await
            }
        }?;

        println!("finished reading request");
    }

    Ok(())
}

async fn handle_command<'a>(
    mem_db: &Arc<Mutex<HashMap<String, InMemoryRecord>>>,
    request_buffer: &[u8],
) -> Result<String, anyhow::Error> {
    let mut context = AppContext::new(mem_db, &request_buffer)?;

    println!("parsing request");

    parse_redis_resp_proc_command(&mut context)?;

    println!("handling request: {}", context.format_request_info(true));

    match context
        .get_request_resp_command_ref()
        .unwrap()
        .name
        .as_str()
    {
        RespCommandNames::PING => handle_command_ping(&mut context)?,
        RespCommandNames::ECHO => handle_command_echo(&mut context)?,
        RespCommandNames::GET => handle_command_get_async(&mut context).await?,
        RespCommandNames::SET => handle_command_set_async(&mut context).await?,

        _ => {
            return Err(Error::msg(
                "Could not handle command - Unknown or not implemented command.",
            ))
        }
    };

    Ok(context.unwrap_response_command_response().to_owned())
}

#[cfg(test)]
mod tests {
    use crate::{
        command_handlers::{
            handle_command_echo, handle_command_get_async, handle_command_ping,
            handle_command_set_async,
        },
        handle_command,
        resp_parser::{parse_redis_resp_proc_command, shared::RespCommandNames},
        shared::{AppContext, InMemoryRecord},
    };

    use std::{collections::HashMap, sync::Arc};

    use anyhow::Ok;
    use tokio::sync::Mutex;

    fn create_test_mem_db() -> Arc<Mutex<HashMap<String, InMemoryRecord>>> {
        Arc::new(Mutex::new(HashMap::<String, InMemoryRecord>::new()))
    }

    #[tokio::test]
    async fn handle_command_handles_ping() -> Result<(), anyhow::Error> {
        let request_buffer = b"*1\r\n$4\r\npiNg\r\n";
        let fake_mem_db = create_test_mem_db();
        let mut fake_app_context = AppContext::new(&fake_mem_db, request_buffer)?;

        parse_redis_resp_proc_command(&mut fake_app_context)?;
        assert_eq!(
            fake_app_context
                .get_request_resp_command_ref()
                .unwrap()
                .name,
            RespCommandNames::PING
        );
        assert_eq!(
            fake_app_context
                .get_request_resp_command_ref()
                .unwrap()
                .parameters
                .len(),
            0
        );

        handle_command_ping(&mut fake_app_context)?;
        let handled_command_response = handle_command(&fake_mem_db, request_buffer).await?;
        assert_eq!(
            fake_app_context
                .unwrap_response_command_response()
                .to_owned(),
            "+PONG\r\n".to_owned()
        );
        assert_eq!(handled_command_response, "+PONG\r\n".to_owned());

        Ok(())
    }

    #[tokio::test]
    async fn handle_command_handles_echo() -> Result<(), anyhow::Error> {
        let request_buffer = b"*2\r\n$4\r\nEcHo\r\n$19\r\nHey world, I'm Joe!\r\n";
        let fake_mem_db = create_test_mem_db();
        let mut fake_app_context = AppContext::new(&fake_mem_db, request_buffer)?;

        parse_redis_resp_proc_command(&mut fake_app_context)?;
        assert_eq!(
            fake_app_context
                .get_request_resp_command_ref()
                .unwrap()
                .name,
            RespCommandNames::ECHO
        );
        assert_eq!(
            fake_app_context
                .get_request_resp_command_ref()
                .unwrap()
                .parameters
                .len(),
            1
        );

        handle_command_echo(&mut fake_app_context)?;
        let handled_command_response = handle_command(&fake_mem_db, request_buffer).await?;
        assert_eq!(
            fake_app_context
                .unwrap_response_command_response()
                .to_owned(),
            "$19\r\nHey world, I'm Joe!\r\n".to_owned()
        );
        assert_eq!(
            handled_command_response,
            "$19\r\nHey world, I'm Joe!\r\n".to_owned()
        );

        Ok(())
    }

    #[tokio::test]
    async fn handle_command_handles_set_get() -> Result<(), anyhow::Error> {
        let fake_mem_db = create_test_mem_db();

        // Set:
        let request_buffer_set = b"*3\r\n$3\r\nsET\r\n$3\r\nfoo\r\n$19\r\nHey world, I'm Joe!\r\n";
        // let request_buffer_set = b"*3\r\n$3\r\nsET\r\n$3\r\nfoo\r\n$19\r\nHey world, I'm Joe!\r\n$2\r\nPx\r\n$3\r\n100\r\n";
        let mut fake_app_context_set = AppContext::new(&fake_mem_db, request_buffer_set)?;

        parse_redis_resp_proc_command(&mut fake_app_context_set)?;
        assert_eq!(
            fake_app_context_set
                .get_request_resp_command_ref()
                .unwrap()
                .name,
            RespCommandNames::SET
        );
        assert_eq!(
            fake_app_context_set
                .get_request_resp_command_ref()
                .unwrap()
                .parameters
                .len(),
            2
        );

        handle_command_set_async(&mut fake_app_context_set).await?;
        let handled_command_response_set = handle_command(&fake_mem_db, request_buffer_set).await?;
        assert_eq!(
            fake_app_context_set
                .unwrap_response_command_response()
                .to_owned(),
            "+OK\r\n".to_owned()
        );
        assert_eq!(handled_command_response_set, "+OK\r\n".to_owned());

        // Get:
        let request_buffer_get = b"*2\r\n$3\r\ngET\r\n$3\r\nfoo\r\n";
        let mut fake_app_context_get = AppContext::new(&fake_mem_db, request_buffer_get)?;

        parse_redis_resp_proc_command(&mut fake_app_context_get)?;
        assert_eq!(
            fake_app_context_get
                .get_request_resp_command_ref()
                .unwrap()
                .name,
            RespCommandNames::GET
        );
        assert_eq!(
            fake_app_context_get
                .get_request_resp_command_ref()
                .unwrap()
                .parameters
                .len(),
            1
        );

        handle_command_get_async(&mut fake_app_context_get).await?;
        let handled_command_response_get = handle_command(&fake_mem_db, request_buffer_get).await?;
        assert_eq!(
            fake_app_context_get
                .unwrap_response_command_response()
                .to_owned(),
            "$19\r\nHey world, I'm Joe!\r\n".to_owned()
        );
        assert_eq!(
            handled_command_response_get,
            "$19\r\nHey world, I'm Joe!\r\n".to_owned()
        );

        Ok(())
    }
}
