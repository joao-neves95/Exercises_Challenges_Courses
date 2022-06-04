use crate::models::{request::Request, response::Response};

pub async fn handle_get_balance(request: Request) -> Response {
    build_response(
        200,
        format!("{{\"balance\": {}}}", request.balance_store.lock().await),
    )
    .await
}

pub async fn handle_post_balance(request: Request) -> Response {
    // Take the content after 'POST /' unntil whitespace.
    let input: String = request.buffer[6..32]
        .iter()
        .take_while(|c| !(**c as char).is_whitespace())
        .map(|c| *c as char)
        .collect();

    if input.is_empty() {
        return build_response(
            400,
            "Error: The new balance amount parameter is required.".to_string(),
        )
        .await;
    }

    let new_balance = input.parse::<f32>().unwrap();
    let mut balance_store_lock = request.balance_store.lock().await;
    *balance_store_lock += new_balance;

    build_response(200, format!("{{\"balance\": {}}}", balance_store_lock)).await
}

async fn build_response(status_code: u16, content: String) -> Response {
    let status = match status_code {
        200 => "OK",
        400 => "Bad Request",

        _ => "Not Implemented",
    }
    .to_string();

    Response {
        status_code,
        status,
        content,
    }
}
