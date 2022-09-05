use std::str::Chars;

use crate::{constants::TokenTypeName, models::Token};

pub struct Lexer {}

impl Lexer {
    pub fn run(input: &str) -> Vec<Token> {
        let mut all_tokens = Vec::new();

        let mut position = 0;
        let count = input.chars().count();
        let mut input_iter = input.chars();

        let mut current_char = next_char(&mut input_iter);

        while position < count {
            if current_char.is_whitespace() {
                position += 1;
                current_char = next_char(&mut input_iter);
            } else if current_char == '(' || current_char == ')' {
                all_tokens.push(Token {
                    type_name: TokenTypeName::Parenthesis,
                    value: current_char.to_string(),
                });

                position += 1;
                current_char = next_char(&mut input_iter);

            // (add 123 456)
            //      ^^^ ^^^
            } else if is_number(current_char) {
                // Advances and points `current_char` to ')', if it's the right member.
                let value = extract_values_until(
                    &mut input_iter,
                    &mut current_char,
                    &mut position,
                    |current_char| is_number(current_char),
                );

                all_tokens.push(Token {
                    type_name: TokenTypeName::Number,
                    value,
                });

            // (concat "foo" "bar")
            //          ^^^   ^^^
            } else if current_char == '"' {
                // Skip the first opening double quote.
                current_char = next_char(&mut input_iter);

                // Advances and points `current_char` to ')', if it's the right member.
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

                // Skip the last closing double quote.
                current_char = next_char(&mut input_iter);

            // (add 2 4)
            //  ^^^
            } else if current_char.is_alphanumeric() {
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
                panic!(
                    "Unexpected char '{}' at position {}",
                    current_char, position
                )
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

#[cfg(test)]
mod tests {
    use crate::{constants::TokenTypeName, models::Token, Lexer};

    #[test]
    fn lisp_add_passes() {
        let result = Lexer::run("(add 123 456)");

        let expected = vec![
            Token {
                type_name: TokenTypeName::Parenthesis,
                value: "(".to_owned(),
            },
            Token {
                type_name: TokenTypeName::FunctionName,
                value: "add".to_owned(),
            },
            Token {
                type_name: TokenTypeName::Number,
                value: "123".to_owned(),
            },
            Token {
                type_name: TokenTypeName::Number,
                value: "456".to_owned(),
            },
            Token {
                type_name: TokenTypeName::Parenthesis,
                value: ")".to_owned(),
            },
        ];

        assert_eq!(result, expected)
    }

    #[test]
    fn lisp_concat_passes() {
        let result = Lexer::run(r#"(concat "foo" "bar")"#);

        let expected = vec![
            Token {
                type_name: TokenTypeName::Parenthesis,
                value: "(".to_owned(),
            },
            Token {
                type_name: TokenTypeName::FunctionName,
                value: "concat".to_owned(),
            },
            Token {
                type_name: TokenTypeName::String,
                value: "foo".to_owned(),
            },
            Token {
                type_name: TokenTypeName::String,
                value: "bar".to_owned(),
            },
            Token {
                type_name: TokenTypeName::Parenthesis,
                value: ")".to_owned(),
            },
        ];

        assert_eq!(result, expected)
    }

    #[test]
    #[should_panic]
    fn lisp_unexpected_token_fails() {
        let _ = Lexer::run("(add ` 2)");
    }
}
