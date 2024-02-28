use super::shared::{RespData, RespDataTypesFirstByte};
use crate::utils::{concat_u32, LineEndings};

use anyhow::Error;

// E.g.: "*$4\r\nPING\r\n"
pub fn move_resp_bulk_string(
    command_iter: &mut std::iter::Peekable<std::iter::Enumerate<std::str::Chars<'_>>>,
    current_char: &Option<(usize, char)>,
) -> Result<RespData, Error> {
    if current_char.is_none()
        || current_char.unwrap().1 != RespDataTypesFirstByte::BULK_STRINGS_CHAR
    {
        return Err(Error::msg(
            "Could not parse command: Command malformed, expected a bulk string.",
        ));
    }

    let current_char = command_iter.next();

    let string_length = move_collect_data_len_number(command_iter, &current_char)?;

    let mut bulk_string = String::new();
    let mut current_char;

    for _ in 0..string_length {
        current_char = command_iter.next();

        if current_char.is_none() {
            break;
        }

        bulk_string.push(current_char.unwrap().1);
    }

    move_to_crlf_end(command_iter);

    Ok(RespData::BulkString {
        size: string_length,
        value: bulk_string,
    })
}

/// Extracts the data length and moves the iterator to the `\n` char in `\r\n`.
fn move_collect_data_len_number(
    command_iter: &mut std::iter::Peekable<std::iter::Enumerate<std::str::Chars<'_>>>,
    current_char: &Option<(usize, char)>,
) -> Result<u32, Error> {
    if current_char.is_none() || !current_char.unwrap().1.is_ascii_digit() {
        return Err(Error::msg(
            "Could not parse command: Command malformed, expected a number describing data length.",
        ));
    }

    let mut num = get_data_length_number(current_char.unwrap().1)?;

    while current_char.is_some() {
        match command_iter.peek() {
            None => break,

            Some((_, next_char)) => {
                if !next_char.is_ascii_digit() {
                    break;
                }

                let current_char = command_iter.next();

                num = concat_u32(num, get_data_length_number(current_char.unwrap().1)?).unwrap();
            }
        };
    }

    move_to_crlf_end(command_iter);

    Ok(num)
}

fn get_data_length_number(current_char: char) -> Result<u32, Error> {
    match current_char.to_digit(10) {
        None => Err(Error::msg(
            "Could not parse command: Command malformed, invalid number describing data length.",
        )),
        Some(num) => Ok(num),
    }
}

/// Moves the iterator to the `\n` char in `\r\n`.
pub fn move_to_crlf_end(
    command_iter: &mut std::iter::Peekable<std::iter::Enumerate<std::str::Chars<'_>>>,
) -> Option<(usize, char)> {
    let mut current_char = command_iter.next();

    while current_char.is_some() {
        let (_, char) = current_char.unwrap();

        if char != LineEndings::CR_CHAR {
            current_char = command_iter.next();
            continue;
        }

        current_char = command_iter.next();

        match current_char {
            None => return current_char,

            Some((_, char)) => {
                if char == LineEndings::LF_CHAR {
                    break;
                }
            }
        };
    }

    current_char
}
