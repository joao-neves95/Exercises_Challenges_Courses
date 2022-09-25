use crate::{
    constants::{AstNodeType, TokenTypeName},
    models::Token,
};

use std::slice::Iter;

#[derive(Debug, PartialEq)]
pub struct IntermediateAst<'a> {
    pub ast_type: &'a str,
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
            ast_type: "IntermediateProgram",
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
            match walk_recursive(&mut token_iter_box) {
                Some(unwrapped_new_node) => {
                    self.body.push(unwrapped_new_node);
                }
                None => break,
            };
        }

        self
    }
}

fn walk_recursive<'a>(
    tokens_iter: &mut Box<&'a mut Iter<Token>>,
) -> Option<IntermediateAstNode<'a>> {
    let current_token = tokens_iter.next();

    let mut token = match current_token {
        Some(unwrapped_token) => unwrapped_token,
        None => return None,
    };

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
                match walk_recursive(tokens_iter) {
                    Some(unwrapped_param) => {
                        new_node.params.as_mut().unwrap().push(unwrapped_param)
                    }
                    None => break,
                };
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

        _ => panic!("Unsupported token type: '{}'", token.type_name),
    };
}

// TODO: Add test for `(add 2 (subtract 4 2))`
#[cfg(test)]
mod tests {
    use crate::{
        intermediate_ast::IntermediateAst,
        mock_data::{create_intermediate_ast_add, create_tokens_vec_add},
    };

    #[test]
    fn parse_tokens_into_intermediate_ast_add_passes() {
        let mock_tokens = create_tokens_vec_add();
        let mut mock_tokens_iter = mock_tokens.iter();

        let expression = "add".to_owned();
        let param_val_1 = "123".to_owned();
        let param_val_2 = "456".to_owned();
        let expected = create_intermediate_ast_add(&expression, &param_val_1, &param_val_2);

        let result = IntermediateAst::from(&mut mock_tokens_iter);

        assert_eq!(result, expected);
    }
}
