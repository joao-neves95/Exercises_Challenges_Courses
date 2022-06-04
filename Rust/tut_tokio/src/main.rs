use std::{
    error::Error,
    fmt::{write, Display},
    str::FromStr,
};

use tokio::{
    io::{AsyncReadExt, AsyncWriteExt},
    net::{TcpListener, TcpStream},
};

use strum::{Display, EnumString};

use request_handlers::{handle_get_balance, handle_post_balance};
mod request_handlers;

#[derive(Debug, PartialEq, EnumString, Display)]
enum HttpMethodType {
    GET,
    POST,
}

pub struct Response {
    status_code: u16,
    status: String,
    content: String,
}

#[derive(Debug)]
struct InternalError {
    message: String,
    inner_error: Option<String>,
}

impl InternalError {
    #[allow(dead_code)]
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

    const SERVER_ADDY: &str = "127.0.0.1:5151";
    let tcp_listener = TcpListener::bind(SERVER_ADDY).await.unwrap();
    println!("Server listening on {SERVER_ADDY}...");

    loop {
        match tcp_listener.accept().await {
            Ok((stream, addy)) => tokio::spawn(async move {
                handle_connection(stream, addy.to_string()).await;
            }),
            Err(e) => tokio::spawn(async move { println!("Error while getting request: {e:?}") }),
        };
    }
}

async fn handle_connection(mut stream: TcpStream, client_address: String) {
    let mut request_buffer: [u8; 32] = [0; 32];
    stream.read(&mut request_buffer).await.unwrap();

    let http_method = match get_http_method_type(request_buffer).await {
        Ok(method) => method,
        Err(e) => {
            println!("Error while handling the HTTP call: {e:?}");
            return;
        }
    };

    println!(
        "New {} request from: {}",
        http_method.to_string(),
        client_address
    );

    let res = match http_method {
        HttpMethodType::GET => handle_get_balance().await,
        HttpMethodType::POST => handle_post_balance(&mut request_buffer).await,
    };

    let http_response = format!(
        "HTTP/1.1 {} {}\nContent-Type: application/json\nContent-Length: {}\n\n{}",
        res.status_code,
        res.status,
        res.content.len(),
        res.content
    );

    println!("Response: {http_response:?}");

    stream.write(http_response.as_bytes()).await.unwrap();
    stream.flush().await.unwrap();
}

async fn get_http_method_type(request_buffer: [u8; 32]) -> Result<HttpMethodType, InternalError> {
    return match std::str::from_utf8(&request_buffer[0..4]) {
        Ok(method) => match HttpMethodType::from_str(method.trim_end().to_uppercase().as_str()) {
            Ok(parsed_method) => Ok(parsed_method),
            Err(e) => Err(InternalError::new_inner(
                "Invalid HTTP method type",
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
