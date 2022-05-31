use tokio::{io::AsyncWriteExt, net::TcpStream};

pub async fn handle_get_balance(stream: TcpStream) {
    build_response(stream, format!("{{\"balance\": {}}}", 0.00)).await;
}

pub async fn handle_post_balance(stream: TcpStream, request_buffer: &mut [u8; 16]) {
    // TODO: Error handling.

    // Take the content after 'POST /' unntil whitespace.
    let input: String = request_buffer[6..16]
        .iter()
        .take_while(|c| !(**c as char).is_whitespace())
        .map(|c| *c as char)
        .collect();

    let new_balance = input.parse::<f32>().unwrap();
    build_response(stream, format!("{{\"balance\": {}}}", new_balance)).await;
}

async fn build_response(mut stream: TcpStream, content: String) {
    let response = format!(
        "HTTP/1.1 200 OK\nContent-Type: application/json\r\nContent-Length: {}\n\n{}",
        content.len(),
        content
    );

    stream.write(response.as_bytes()).await.unwrap();
    stream.flush().await.unwrap();
}
