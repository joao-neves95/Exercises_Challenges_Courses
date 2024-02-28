use anyhow::{Error, Result};

use crate::resp_parser::data_types::move_resp_bulk_string;

use super::{data_types::move_to_crlf_end, shared::RespCommand};

pub fn parse_resp_no_param_command<'a>(command_name: &String) -> Result<RespCommand, Error> {
    Ok(RespCommand {
        name: command_name.to_owned(),
        parameters: Vec::new(),
    })
}

pub fn parse_resp_multi_param_command<'a>(
    command_name: &'a String,
    parameter_count: u8,
    command_iter: &mut std::iter::Peekable<std::iter::Enumerate<std::str::Chars<'_>>>,
    current_char: &Option<(usize, char)>,
) -> Result<RespCommand, Error> {
    let mut curr_char = *current_char;
    let mut parameters = Vec::<String>::new();

    for _ in 0..parameter_count {
        let param = move_resp_bulk_string(command_iter, &curr_char)?
            .get_bulk_string_value()
            .to_owned();

        parameters.push(param);
        curr_char = command_iter.next();
    }

    move_to_crlf_end(command_iter);

    Ok(RespCommand {
        name: command_name.to_owned(),
        parameters,
    })
}
