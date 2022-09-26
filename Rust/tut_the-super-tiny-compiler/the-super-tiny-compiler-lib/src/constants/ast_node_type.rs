#[derive(Debug, PartialEq, strum_macros::Display)]
pub enum AstNodeType {
    ExpressionStatement,
    CallExpression,
    NumberLiteral,
    StringLiteral,
}
