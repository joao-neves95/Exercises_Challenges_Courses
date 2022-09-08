use std::slice::Iter;

use crate::{
    constants::{AstNodeType, TokenTypeName},
    models::Token,
};

#[derive(Debug, PartialEq)]
pub struct IntermediateAst<'a> {
    pub tree_type: &'a str,
    pub body: Vec<IntermediateAstNode<'a>>,
}

#[derive(Debug, PartialEq)]
pub struct IntermediateAstNode<'a> {
    pub node_type: AstNodeType,
    pub name: Option<&'a String>,
    pub value: Option<&'a String>,
    pub params: Option<Vec<IntermediateAstNode<'a>>>,
}

impl<'a> From<&'a mut Iter<'a, Token>> for IntermediateAst<'a> {
    fn from(tokens_iter: &'a mut Iter<'a, Token>) -> Self {
        let mut ast = IntermediateAst {
            tree_type: "Program",
            body: Vec::new(),
        };

        ast.build_recursive(tokens_iter);
        ast
    }
}

impl<'a> IntermediateAst<'a> {
    fn build_iterative() {
        todo!()
    }

    pub fn build_recursive(&mut self, tokens_iter: &'a mut Iter<Token>) -> &Self {
        // Move the iterator into the heap.
        let mut token_iter_box = Box::new(tokens_iter);

        loop {
            let new_node = walk_recursive(&mut token_iter_box);

            if let Some(node) = new_node {
                self.body.push(node);
            } else {
                break;
            }
        }

        self
    }
}

fn walk_recursive<'a>(
    tokens_iter: &mut Box<&'a mut Iter<Token>>,
) -> Option<IntermediateAstNode<'a>> {
    let current_token = tokens_iter.next();

    let mut token;
    if let Some(token_exists) = current_token {
        token = token_exists;
    } else {
        return None;
    }

    return match token.type_name {
        TokenTypeName::CloseParenthesis => None,
        TokenTypeName::OpenParenthesis => {
            // Skip the OpenParenthesis token into the FunctionName.
            token = tokens_iter.next().unwrap();

            let mut new_node = IntermediateAstNode {
                node_type: AstNodeType::CallExpression,
                name: Some(&token.value),
                value: None,
                params: Some(Vec::new()),
            };

            // Iterate through the CallExpression parameters.
            while token.type_name != TokenTypeName::CloseParenthesis {
                let new_param_node = walk_recursive(tokens_iter);

                if let Some(param_node) = new_param_node {
                    new_node.params.as_mut().unwrap().push(param_node);
                } else {
                    break;
                }
            }

            return Some(new_node);
        }
        TokenTypeName::Number => Some(IntermediateAstNode {
            node_type: AstNodeType::NumberLiteral,
            name: None,
            value: Some(&token.value),
            params: None,
        }),
        TokenTypeName::String => Some(IntermediateAstNode {
            node_type: AstNodeType::StringLiteral,
            name: None,
            value: Some(&token.value),
            params: None,
        }),

        // TokenTypeName::FunctionName => todo!(),
        _ => panic!("Unsupported node type: '{}'", token.type_name),
    };
}

#[cfg(test)]
mod tests {
    use crate::{constants::AstNodeType, create_tokens_vec_add, IntermediateAst};

    use super::IntermediateAstNode;

    #[test]
    fn parse_tokens_into_intermediate_ast_add_passes() {
        let mock_tokens = create_tokens_vec_add();
        let mut mock_tokens_iter = mock_tokens.iter();

        let expression = "add".to_owned();
        let param_val_1 = "123".to_owned();
        let param_val_2 = "456".to_owned();

        let expected = IntermediateAst {
            tree_type: "Program",
            body: vec![IntermediateAstNode {
                node_type: AstNodeType::CallExpression,
                name: Some(&expression),
                value: None,
                params: Some(vec![
                    IntermediateAstNode {
                        node_type: AstNodeType::NumberLiteral,
                        value: Some(&param_val_1),
                        name: None,
                        params: None,
                    },
                    IntermediateAstNode {
                        node_type: AstNodeType::NumberLiteral,
                        value: Some(&param_val_2),
                        name: None,
                        params: None,
                    },
                ]),
            }],
        };

        let result = IntermediateAst::from(&mut mock_tokens_iter);

        assert_eq!(result, expected);
    }
}
