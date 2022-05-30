use response_handlers::handle_connection;
use tokio::net::TcpListener;

mod response_handlers;

#[tokio::main]
async fn main() {
    let task = tokio::spawn(async {
        println!("Hello, world!");
    });

    task.await.unwrap();

    let tcp_listener = TcpListener::bind("127.0.0.1:5151").await.unwrap();

    loop {
        match tcp_listener.accept().await {
            Ok((stream, addy)) => {
                println!("New client request from: {addy}");

                tokio::spawn(async { handle_connection(stream).await });
            }
            Err(e) => println!("Error while getting request: {e:?}"),
        }
    }
}
