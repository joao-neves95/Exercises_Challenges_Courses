use tokio::{io::AsyncWriteExt, net::TcpStream};

pub async fn handle_connection(mut stream: TcpStream) {
    let content = "{\"balance\": 0.00}";

    let response = format!(
        "HTTP/1.1 200 OK\nContent-Type: application/json\r\nContent-Length: {}\n\n{}",
        content.len(),
        content
    );

    stream.write(response.as_bytes()).await.unwrap();
    stream.flush().await.unwrap();
}
