#[derive(Debug, PartialEq, strum_macros::Display)]
pub enum AstNodeType {
    ExpressionStatement,
    VariableDeclaration,
    CallExpression,
    NumberLiteral,
    StringLiteral,
}
