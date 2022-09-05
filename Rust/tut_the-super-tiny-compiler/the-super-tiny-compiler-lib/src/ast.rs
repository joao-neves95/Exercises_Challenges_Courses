use crate::models::Token;

pub struct Ast<'a> {
    tokens: &'a Vec<Token>,
}

impl<'a> From<&'a Vec<Token>> for Ast<'a> {
    fn from(tokens: &'a Vec<Token>) -> Self {
        Ast { tokens }
    }
}
