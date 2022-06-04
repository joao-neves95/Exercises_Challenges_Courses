use std::{str::FromStr, sync::Arc};

use tokio::{
    io::{AsyncReadExt, AsyncWriteExt},
    net::{TcpListener, TcpStream},
    sync::Mutex,
};

use strum::{Display, EnumString};

use models::internal_error::InternalError;
use request_handlers::{handle_get_balance, handle_post_balance};

use crate::models::request::Request;

mod models;
mod request_handlers;

#[derive(Debug, PartialEq, EnumString, Display)]
enum HttpMethodType {
    GET,
    POST,
}

#[tokio::main]
async fn main() {
    let task = tokio::spawn(async {
        println!("Hello, world!");
    });

    task.await.unwrap();

    let balance_store: Arc<Mutex<f32>> = Arc::new(Mutex::new(0.00));

    const SERVER_ADDY: &str = "127.0.0.1:5151";
    let tcp_listener = TcpListener::bind(SERVER_ADDY).await.unwrap();
    println!("Server listening on {SERVER_ADDY}...");

    loop {
        match tcp_listener.accept().await {
            Ok((stream, addy)) => {
                let request = Request {
                    buffer: [0; 32],
                    balance_store: balance_store.clone(),
                };

                tokio::spawn(
                    async move { handle_connection(stream, addy.to_string(), request).await },
                )
            }
            Err(e) => tokio::spawn(async move { println!("Error while getting request: {e:?}") }),
        };
    }
}

async fn handle_connection(mut stream: TcpStream, client_address: String, mut request: Request) {
    stream.read(&mut request.buffer).await.unwrap();

    let http_method = match get_http_method_type(request.buffer).await {
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
        HttpMethodType::GET => handle_get_balance(request).await,
        HttpMethodType::POST => handle_post_balance(request).await,
    };

    let http_response = format!(
        "HTTP/1.1 {} {}\nContent-Type: application/json\nContent-Length: {}\n\n{}",
        res.status_code,
        res.status,
        res.content.len(),
        res.content
    );

    println!("Response: {http_response:?}");

    stream.write_all(http_response.as_bytes()).await.unwrap();
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
