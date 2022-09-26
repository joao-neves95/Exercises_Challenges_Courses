#[derive(Debug, PartialEq, strum_macros::Display)]
pub enum AstNodeType {
    // TODO: Add support for binary operations/expressions like ArithmeticOperator (numeric operators +, -, *, /)
    ExpressionStatement,
    CallExpression,
    NumberLiteral,
    StringLiteral,
}
