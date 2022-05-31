use std::{
    error::Error,
    fmt::{write, Display},
    str::FromStr,
};

use tokio::{
    io::AsyncReadExt,
    net::{TcpListener, TcpStream},
};

use strum::{Display, EnumString};

use request_handlers::handle_get_balance;
mod request_handlers;

#[derive(Debug, PartialEq, EnumString, Display)]
enum HttpMethodType {
    GET,
    POST,
}

#[derive(Debug)]
struct InternalError {
    message: String,
    inner_error: Option<String>,
}

impl InternalError {
    fn new(message: &str) -> InternalError {
        InternalError {
            message: message.to_string(),
            inner_error: None,
        }
    }

    fn new_inner(message: &str, inner_error_message: &str) -> InternalError {
        InternalError {
            message: message.to_string(),
            inner_error: Some(inner_error_message.to_string()),
        }
    }
}

impl Display for InternalError {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let mut message = format!("Error: {}", self.message);

        if let Some(inner_error) = self.inner_error.as_ref() {
            message.push_str(format!("Inner Error: {}", inner_error).as_str());
        }

        write(f, format_args!("{}", message))
    }
}

impl Error for InternalError {}

#[tokio::main]
async fn main() {
    let task = tokio::spawn(async {
        println!("Hello, world!");
    });

    task.await.unwrap();

    let server_addy = "127.0.0.1:5151";
    let tcp_listener = TcpListener::bind(server_addy).await.unwrap();
    println!("Server listening on {server_addy}...");

    loop {
        match tcp_listener.accept().await {
            Ok((mut stream, addy)) => {
                let http_method = match get_http_method_type(&mut stream).await {
                    Ok(method) => method.to_string(),
                    Err(e) => {
                        println!("Error while handling the HTTP call: {e:?}");

                        break;
                    }
                };

                println!("New {http_method} request from: {addy}");

                tokio::spawn(async { handle_get_balance(stream).await });
            }
            Err(e) => println!("Error while getting request: {e:?}"),
        }
    }
}

async fn get_http_method_type(stream: &mut TcpStream) -> Result<HttpMethodType, InternalError> {
    let mut buffer: [u8; 16] = [0; 16];
    stream.read(&mut buffer).await.unwrap();

    return match std::str::from_utf8(&buffer[0..4]) {
        Ok(method) => match HttpMethodType::from_str(method.trim_end().to_uppercase().as_str()) {
            Ok(parsed_method) => Ok(parsed_method),
            Err(e) => Err(InternalError::new_inner(
                "Invalid HTTP method type format",
                e.to_string().as_str(),
            )),
        },
        // Err(e) => panic!("Invalid UTF-8 sequence: {}", e),
        Err(e) => Err(InternalError::new_inner(
            "Invalid UTF-8 encoding on the request",
            e.to_string().as_str(),
        )),
    };
}
