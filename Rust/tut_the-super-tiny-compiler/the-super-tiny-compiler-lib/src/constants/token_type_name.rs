#[derive(Debug, PartialEq, strum_macros::Display)]
pub enum TokenTypeName {
    OpenParenthesis,
    CloseParenthesis,
    Number,
    String,
    FunctionName,
}
