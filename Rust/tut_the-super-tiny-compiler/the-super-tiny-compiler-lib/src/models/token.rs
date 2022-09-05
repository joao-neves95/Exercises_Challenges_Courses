use crate::constants::TokenTypeName;

#[derive(Debug, PartialEq)]
pub struct Token {
    pub type_name: TokenTypeName,
    pub value: String,
}
