use std::str::Chars;

use crate::{constants::TokenTypeName, models::Token};

pub struct Lexer {}

impl Lexer {
    pub fn run(input: &str) -> Vec<Token> {
        let mut all_tokens = Vec::new();

        let mut position = 0;
        let count = input.chars().count();
        let mut input_iter = input.chars();

        while position < count {
            let mut current_char = input_iter.next().unwrap_or(' ');

            if current_char.is_whitespace() {
                position += 1;

            } else if current_char == '(' || current_char == ')' {
                all_tokens.push(Token {
                    type_name: TokenTypeName::Parenthesis,
                    value: current_char.to_string(),
                });

                position += 1;

            // (add 123 456)
            //      ^^^ ^^^
            } else if current_char.is_digit(10) {
                let value = extract_values_until(
                    &mut input_iter,
                    &mut current_char,
                    &mut position,
                    |current_char| current_char.is_digit(10),
                );

                all_tokens.push(Token {
                    type_name: TokenTypeName::Number,
                    value,
                });

            // (concat "foo" "bar")
            //          ^^^   ^^^
            } else if current_char == '"' {
                // Skip the first double quote.
                current_char = next_char(&mut input_iter);

                let value = extract_values_until(
                    &mut input_iter,
                    &mut current_char,
                    &mut position,
                    |current_char| current_char != '"',
                );

                all_tokens.push(Token {
                    type_name: TokenTypeName::String,
                    value,
                });

            // (add 2 4)
            //  ^^^
            } else if current_char.is_alphanumeric() {
                if is_number(current_char) {
                    panic!("Function names cannot start with a number; char '{}' at position {}", current_char, position)
                }

                let value = extract_values_until(
                    &mut input_iter,
                    &mut current_char,
                    &mut position,
                    |current_char| current_char.is_alphanumeric(),
                );

                all_tokens.push(Token {
                    type_name: TokenTypeName::FunctionName,
                    value,
                });

            } else {
                panic!("Unknown char: '{}' at position {}", current_char, position)
            }
        }

        all_tokens
    }
}

fn next_char(input_iter: &mut Chars) -> char {
    input_iter.next().unwrap_or(' ')
}

fn is_number(char: char) -> bool {
    char.is_digit(10)
}

fn extract_values_until(
    input_iter: &mut Chars,
    current_char: &mut char,
    position: &mut usize,
    predicate_while: fn(char) -> bool,
) -> String {
    let mut value = String::new();

    while predicate_while(*current_char) {
        value.push(*current_char);
        *current_char = next_char(input_iter);
        *position += 1;
    }

    value
}
