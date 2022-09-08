use crate::{constants::TokenTypeName, models::Token};

pub fn create_tokens_vec_add() -> Vec<Token> {
    vec![
        Token {
            type_name: TokenTypeName::OpenParenthesis,
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
            type_name: TokenTypeName::CloseParenthesis,
            value: ")".to_owned(),
        },
    ]
}

pub fn create_tokens_vec_concat() -> Vec<Token> {
    vec![
        Token {
            type_name: TokenTypeName::OpenParenthesis,
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
            type_name: TokenTypeName::CloseParenthesis,
            value: ")".to_owned(),
        },
    ]
}
