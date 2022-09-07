use std::slice::Iter;

use crate::{
    constants::{AstNodeType, TokenTypeName},
    models::Token,
};

pub struct Ast<'a> {
    pub tree_type: &'a str,
    pub body: Vec<AstNode<'a>>,
}

pub struct AstNode<'a> {
    pub node_type: AstNodeType,
    pub name: String,
    pub value: Option<&'a String>,
    pub params: Option<Vec<AstNode<'a>>>,
}

impl<'a> From<&'a mut Iter<'a, Token>> for Ast<'a> {
    fn from(tokens_iter: &'a mut Iter<'a, Token>) -> Self {
        let mut ast = Ast {
            tree_type: "LispProgram",
            body: Vec::new(),
        };

        ast.build_ast_recursive(tokens_iter);
        ast
    }
}

impl<'a> Ast<'a> {
    fn build_ast_iterative() {
        todo!()
    }

    pub fn build_ast_recursive(&mut self, tokens_iter: &'a mut Iter<Token>) -> &Self {
        let count = tokens_iter.count();

        // Move the iterator into the heap.
        let mut token_iter_box = Box::new(tokens_iter);

        for _ in 0..count {
            self.body.push(walk_recursive(&mut token_iter_box));
        }

        self
    }
}

fn walk_recursive<'a>(tokens_iter: &mut Box<&'a mut Iter<Token>>) -> AstNode<'a> {
    let current_token = tokens_iter.next();
    let mut current_token = current_token.unwrap();

    return match current_token.type_name {
        TokenTypeName::OpenParenthesis => {
            // Skip the OpenParenthesis token into the FunctionName.
            current_token = tokens_iter.next().unwrap();

            let mut new_node = AstNode {
                node_type: AstNodeType::CallExpression,
                name: current_token.value.to_owned(),
                value: None,
                params: Some(Vec::new()),
            };

            // Skip the FunctionName and move into the parameters of the function.
            current_token = tokens_iter.next().unwrap();

            // Iterate through the CallExpression parameters.
            while current_token.type_name != TokenTypeName::CloseParenthesis {
                new_node
                    .params
                    .as_mut()
                    .unwrap()
                    .push(walk_recursive(tokens_iter));
            }

            // Skip the CloseParenthesis.
            tokens_iter.next();

            return new_node;
        }
        // TokenTypeName::CloseParenthesis => todo!(),
        TokenTypeName::Number => AstNode {
            node_type: AstNodeType::NumberLiteral,
            name: current_token.value.to_owned(),
            value: None,
            params: None,
        },
        TokenTypeName::String => AstNode {
            node_type: AstNodeType::StringLiteral,
            name: current_token.value.to_owned(),
            value: None,
            params: None,
        },
        TokenTypeName::FunctionName => todo!(),

        _ => panic!("Unsupported node type: '{}'", current_token.type_name),
    };
}
