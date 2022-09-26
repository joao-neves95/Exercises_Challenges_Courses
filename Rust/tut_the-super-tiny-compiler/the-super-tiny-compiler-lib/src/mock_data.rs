use crate::{
    ast::{AstNode, AstNodeCallee},
    constants::{AstNodeType, NodeCalleeType, TokenTypeName},
    intermediate_ast::{IntermediateAst, IntermediateAstNode},
    models::Token,
    Ast,
};

use concat_strs::concat_strs;

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

pub fn create_intermediate_ast_add<'a>(
    expression_name: &'a String,
    param_val_1: &'a String,
    param_val_2: &'a String,
) -> IntermediateAst<'a> {
    IntermediateAst {
        ast_type: "IntermediateProgram",
        body: vec![IntermediateAstNode {
            node_type: AstNodeType::CallExpression,
            name: Some(expression_name),
            value: None,
            params: Some(vec![
                IntermediateAstNode {
                    node_type: AstNodeType::NumberLiteral,
                    value: Some(param_val_1),
                    name: None,
                    params: None,
                },
                IntermediateAstNode {
                    node_type: AstNodeType::NumberLiteral,
                    value: Some(param_val_2),
                    name: None,
                    params: None,
                },
            ]),
        }],
    }
}

pub fn create_ast_add<'a>(
    expression_name: &'a String,
    param_val_1: &'a String,
    param_val_2: &'a String,
) -> Ast<'a> {
    Ast {
        ast_type: "Program",
        body: vec![AstNode {
            node_type: AstNodeType::ExpressionStatement,
            expression: Some(Box::new(AstNode {
                node_type: AstNodeType::CallExpression,
                callee: Some(AstNodeCallee {
                    callee_type: NodeCalleeType::Identifier,
                    name: &expression_name,
                }),
                params: Some(vec![
                    AstNode {
                        node_type: AstNodeType::NumberLiteral,
                        value: Some(&param_val_1),
                        callee: None,
                        params: None,
                        expression: None,
                    },
                    AstNode {
                        node_type: AstNodeType::NumberLiteral,
                        value: Some(&param_val_2),
                        callee: None,
                        params: None,
                        expression: None,
                    },
                ]),
                value: None,
                expression: None,
            })),
            value: None,
            callee: None,
            params: None,
        }],
    }
}

pub fn create_rust_code_string_add<'a>(
    expression_name: &'a String,
    param_val_1: &'a String,
    param_val_2: &'a String,
) -> String {
    concat_strs!(
        "fn main() {\n",
        expression_name,
        "(",
        param_val_1,
        ", ",
        param_val_2,
        ");",
        "\n}"
    )
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
