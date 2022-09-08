use std::{iter::Enumerate, str::Chars};

use crate::{constants::TokenTypeName, models::Token};

pub struct Lexer {}

impl Lexer {
    pub fn parse(input: &str) -> Vec<Token> {
        let mut all_tokens = Vec::new();

        let mut input_iter = input.chars().enumerate();
        let mut current_char = input_iter.next();

        while current_char.is_some() {
            let this_char = current_char.unwrap().1;

            if this_char.is_whitespace() {
                current_char = input_iter.next();
            } else if this_char == '(' || this_char == ')' {
                all_tokens.push(Token {
                    type_name: if this_char == '(' {
                        TokenTypeName::OpenParenthesis
                    } else {
                        TokenTypeName::CloseParenthesis
                    },
                    value: this_char.to_string(),
                });

                current_char = input_iter.next();

            // (add 123 456)
            //      ^^^ ^^^
            } else if is_number(this_char) {
                // Advances and points `current_char` to ')', if it's the right member.
                let value =
                    extract_values_until(&mut input_iter, &mut current_char, |current_char| {
                        is_number(current_char)
                    });

                all_tokens.push(Token {
                    type_name: TokenTypeName::Number,
                    value,
                });

            // (concat "foo" "bar")
            //          ^^^   ^^^
            } else if this_char == '"' {
                // Skip the first opening double quote.
                current_char = input_iter.next();

                // Advances and points `current_char` to ')', if it's the right member.
                let value =
                    extract_values_until(&mut input_iter, &mut current_char, |current_char| {
                        current_char != '"'
                    });

                all_tokens.push(Token {
                    type_name: TokenTypeName::String,
                    value,
                });

                // Skip the last closing double quote.
                current_char = input_iter.next();

            // (add 2 4)
            //  ^^^
            } else if this_char.is_alphanumeric() {
                let value =
                    extract_values_until(&mut input_iter, &mut current_char, |inner_char| {
                        inner_char.is_alphanumeric()
                    });

                all_tokens.push(Token {
                    type_name: TokenTypeName::FunctionName,
                    value,
                });
            } else {
                panic!(
                    "Unexpected char '{}' at position {}",
                    this_char,
                    current_char.unwrap().0
                )
            }
        }

        all_tokens
    }
}

fn is_number(char: char) -> bool {
    char.is_digit(10)
}

fn extract_values_until(
    input_iter: &mut Enumerate<Chars>,
    current_char: &mut Option<(usize, char)>,
    predicate_while: fn(char) -> bool,
) -> String {
    let mut value = String::new();

    while current_char.is_some() && predicate_while((*current_char).unwrap().1) {
        value.push((*current_char).unwrap().1);
        *current_char = input_iter.next();
    }

    value
}

#[cfg(test)]
mod tests {
    use crate::{
        mock_data::{create_tokens_vec_add, create_tokens_vec_concat},
        Lexer,
    };

    #[test]
    fn lisp_add_passes() {
        let expected = create_tokens_vec_add();

        let result = Lexer::parse("(add 123 456)");

        assert_eq!(result, expected)
    }

    #[test]
    fn lisp_concat_passes() {
        let expected = create_tokens_vec_concat();

        let result = Lexer::parse(r#"(concat "foo" "bar")"#);

        assert_eq!(result, expected)
    }

    #[test]
    #[should_panic]
    fn lisp_unexpected_token_fails() {
        let _ = Lexer::parse("(add ` 2)");
    }
}
