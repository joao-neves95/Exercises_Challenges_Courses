use anyhow::{Error, Ok};

use crate::{
    resp_parser::shared::RespCommandSetOptions,
    shared::{AppContext, InMemoryRecord, Response},
};

pub fn handle_command_ping<'a>(context: &mut AppContext<'_>) -> Result<(), Error> {
    context.response = Some(Response {
        command_response: format_string(&"PONG"),
    });

    Ok(())
}

pub fn handle_command_echo<'a>(context: &mut AppContext<'_>) -> Result<(), Error> {
    let message = context
        .get_request_resp_command_ref()
        .unwrap()
        .parameters
        .first()
        .unwrap();

    context.response = Some(Response {
        command_response: format_bulk_string(message),
    });

    Ok(())
}

/// Example commands:
/// "redis-cli set foo bar"
/// "redis-cli set foo bar px 100" (px = key expiry)
pub async fn handle_command_set_async<'a>(context: &mut AppContext<'_>) -> Result<(), Error> {
    let mut db_lock = context.get_mem_db_ref().lock().await;

    let parameters = &context.get_request_resp_command_ref().unwrap().parameters;

    let expiry =
        if parameters.len() == 2 {
            None
        } else if parameters[2].to_uppercase() == RespCommandSetOptions::EXPIRY {
            match parameters[3].parse::<u128>() {
                Err(_) => return Err(Error::msg(
                    "Could not parse command: The SET command's PX option only accepts numbers.",
                )),
                Result::Ok(expiry) => Some(expiry),
            }
        } else {
            return Err(Error::msg(
                "Could not parse command: The SET command correctly only supports the PX option.",
            ));
        };

    (*db_lock).insert(
        parameters[0].to_owned(),
        InMemoryRecord::new(parameters[1].to_owned(), expiry),
    );

    context.set_response_command_response(format_string(&"OK"));

    Ok(())
}

pub async fn handle_command_get_async<'a>(context: &mut AppContext<'_>) -> Result<(), Error> {
    let key = &context.get_request_resp_command_ref().unwrap().parameters[0];

    let db_lock = context.get_mem_db_ref().lock().await;

    let existing_value = (*db_lock).get(key).map(|val| val.to_owned());

    context.set_response_command_response(if existing_value.is_none() {
        format_null_bulk_string()
    } else {
        let existing_value = existing_value.unwrap();

        if existing_value.has_expired()? {
            let mut db_lock = db_lock;

            (*db_lock).remove(key);

            format_null_bulk_string()
        } else {
            format_bulk_string(&existing_value.value.to_owned())
        }
    });

    Ok(())
}

fn format_null_bulk_string() -> String {
    "$-1\r\n".to_owned()
}

fn format_string(message: &str) -> String {
    format!("+{}\r\n", message)
}

fn format_bulk_string(message: &str) -> String {
    format!("${}\r\n{}\r\n", message.len(), message)
}
