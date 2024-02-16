use std::{
    io::{Error, ErrorKind, Read, Write},
    net::{TcpListener, TcpStream},
    str,
    time::Duration,
};

const REQUEST_READ_TIMEOUT: Option<Duration> = Some(Duration::from_millis(100));

fn main() -> Result<(), Error> {
    let tcp_listener = TcpListener::bind("localhost:51458")?;

    for stream_result in tcp_listener.incoming() {
        match stream_result {
            Err(e) => eprint!("Failed: {}", e),

            Ok(stream) => {
                stream.set_read_timeout(REQUEST_READ_TIMEOUT)?;
                handle_client(stream)?;
            }
        }
    }

    Ok(())
}

fn handle_client(mut stream: TcpStream) -> Result<(), Error> {
    let mut request_buffer = [0; 5120];
    let mut start: usize = 0;

    loop {
        let mut buffer = [0; 1024];
        let request_part_result = stream.read(&mut buffer);

        match request_part_result {
            Err(e) => match e.kind() {
                // On a TcpStream there's no explicit EOF unless the connection is dropped on the other end.
                ErrorKind::TimedOut => break,
                _ => panic!("{}", e.kind().to_string()),
            },
            Ok(received_bytes_count) => {
                if received_bytes_count == 0 {
                    break;
                }

                append_to_array(&mut request_buffer, start, 1024, &buffer);
                start += received_bytes_count;
            }
        }
    }

    println!("data: {:?}", str::from_utf8(&request_buffer).unwrap());

    const BODY: &str = "<html><body><h1>Hello, World!</h1></body></html>";
    stream.write_all(
        format!(
            "HTTP/1.1 200 OK
Content-Type: text/html; charset=UTF-8
Cross-Origin-Opener-Policy: same-origin
X-Content-Type-Options: nosniff
X-Frame-Options: DENY
X-Xss-Protection: 1; mode=block
Content-Length: {}

{}",
            BODY.len(),
            BODY
        )
        .as_bytes(),
    )?;
    stream.flush()?;

    Ok(())
}

fn append_to_array(target: &mut [u8], start: usize, max_write: usize, data: &[u8]) {
    let mut i_data = 0;
    for i_target in start..max_write {
        if data[i_data] == 0 || i_target == max_write {
            break;
        }

        target[i_target as usize] = data[i_data];
        i_data += 1;
    }
}
