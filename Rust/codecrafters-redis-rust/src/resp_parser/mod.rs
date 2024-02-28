mod commands;
mod data_types;
pub(crate) mod shared;

use self::{
    commands::{parse_resp_multi_param_command, parse_resp_no_param_command},
    data_types::move_resp_bulk_string,
    shared::{RespCommandNames, RespDataTypesFirstByte},
};
use crate::{shared::AppContext, utils::LineEndings};

use anyhow::{Error, Result};

/// Input examples: <br/>
/// "*1\r\n$4\r\nping\r\n" <br/>
/// "*2\r\n$4\r\necho\r\n$3\r\nhey\r\n" <br/>
pub(crate) fn parse_redis_resp_proc_command(context: &mut AppContext<'_>) -> Result<(), Error> {
    let raw_command = context
        .get_request_ref()
        .raw_command
        .split_once(LineEndings::CRLF_STR);

    if raw_command.is_none() {
        return Err(Error::msg("Could not parse command."));
    }

    let raw_command = raw_command.unwrap();

    if raw_command.0.len() < 2 {
        return Err(Error::msg("Could not parse command: Command malformed."));
    }

    let (array_type, num_of_parts) = raw_command.0.split_at(1);

    if array_type != RespDataTypesFirstByte::ARRAYS_STR {
        return Err(Error::msg(
            "Could not parse command: Command malformed, the command is not an array.",
        ));
    }

    let mut command_body_iter = raw_command.1.chars().enumerate().peekable();
    let current_char: Option<(usize, char)> = command_body_iter.next();

    let command_name = move_resp_bulk_string(&mut command_body_iter, &current_char)?;
    let command_name = command_name.get_bulk_string_value().to_ascii_uppercase();

    let current_char: Option<(usize, char)> = command_body_iter.next();

    context.set_request_resp_command(match command_name.as_str() {
        RespCommandNames::PING => parse_resp_no_param_command(&command_name),
        RespCommandNames::ECHO | RespCommandNames::GET | RespCommandNames::SET => {
            parse_resp_multi_param_command(
                &command_name,
                num_of_parts.parse::<u8>()? - 1,
                &mut command_body_iter,
                &current_char,
            )
        }

        _ => {
            return Err(Error::msg(
                "Could not parse command - Unknown or not implemented command.",
            ))
        }
    }?);

    Ok(())
}
