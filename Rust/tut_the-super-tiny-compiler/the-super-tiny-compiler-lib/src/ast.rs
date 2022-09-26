use crate::{
    constants::{AstNodeType, NodeCalleeType},
    intermediate_ast::IntermediateAstNode,
    IntermediateAst,
};

#[derive(Debug, PartialEq)]
pub struct Ast<'a> {
    pub ast_type: &'a str,
    pub body: Vec<AstNode<'a>>,
}

#[derive(Debug, PartialEq)]
pub struct AstNode<'a> {
    pub node_type: AstNodeType,

    pub expression: Option<Box<AstNode<'a>>>,
    pub callee: Option<AstNodeCallee<'a>>,
    pub value: Option<&'a String>,
    pub params: Option<Vec<AstNode<'a>>>,
}

#[derive(Debug, PartialEq)]
pub struct AstNodeCallee<'a> {
    pub callee_type: NodeCalleeType,
    pub name: &'a String,
}

impl<'a> From<&'a IntermediateAst<'a>> for Ast<'a> {
    fn from(intermediate_ast: &'a IntermediateAst<'a>) -> Self {
        let mut ast = Ast {
            ast_type: "Program",
            body: Vec::new(),
        };

        ast.build_recursive(intermediate_ast);
        ast
    }
}

impl<'a> Ast<'a> {
    fn build_recursive(&mut self, intermediate_ast: &'a IntermediateAst<'a>) -> &Self {
        for i in 0..intermediate_ast.body.len() {
            match walk_ast_vec_recursive(&intermediate_ast.body, i) {
                Some(unwrapped_expression) => {
                    let new_statement = AstNode {
                        node_type: AstNodeType::ExpressionStatement,
                        expression: Some(Box::new(unwrapped_expression)),
                        callee: None,
                        value: None,
                        params: None,
                    };

                    self.body.push(new_statement);
                }
                None => break,
            };
        }

        self
    }
}

fn walk_ast_vec_recursive<'a>(
    node_vec: &'a Vec<IntermediateAstNode<'a>>,
    current_index: usize,
) -> Option<AstNode<'a>> {
    let intermediate_node = match node_vec.get(current_index) {
        Some(unwrapped_node) => unwrapped_node,
        None => return None,
    };

    match intermediate_node.node_type {
        AstNodeType::NumberLiteral => Some(AstNode {
            node_type: AstNodeType::NumberLiteral,
            value: intermediate_node.value,
            callee: None,
            params: None,
            expression: None,
        }),
        AstNodeType::StringLiteral => Some(AstNode {
            node_type: AstNodeType::StringLiteral,
            value: intermediate_node.value,
            callee: None,
            params: None,
            expression: None,
        }),
        AstNodeType::CallExpression => {
            let mut new_call_expression_node = AstNode {
                node_type: AstNodeType::CallExpression,
                callee: Some(AstNodeCallee {
                    callee_type: NodeCalleeType::Identifier,
                    name: intermediate_node.name.unwrap(),
                }),
                value: None,
                params: Some(Vec::new()),
                expression: None,
            };

            let intermediate_node_params = match &intermediate_node.params {
                Some(unwrapped_params) => unwrapped_params,
                None => panic!("A {} must have parameters", AstNodeType::CallExpression),
            };

            for i_params in 0..intermediate_node_params.len() {
                match walk_ast_vec_recursive(&intermediate_node_params, i_params) {
                    Some(unwrapped_new_node) => {
                        new_call_expression_node
                            .params
                            .as_mut()
                            .unwrap()
                            .push(unwrapped_new_node);
                    }
                    None => break,
                };
            }

            Some(new_call_expression_node)
        }

        _ => panic!("Unsupported node type '{}'", intermediate_node.node_type),
    }
}

// TODO: Add test for `(add 2 (subtract 4 2))`
// TODO: Add test for `(concat "Hello" "Rust")`
#[cfg(test)]
mod tests {
    use crate::{
        mock_data::{create_ast_add, create_intermediate_ast_add},
        Ast,
    };

    use pretty_assertions::assert_eq;

    #[test]
    fn transform_intermediate_ast_add_passes() {
        let expression = "add".to_owned();
        let param_val_1 = "123".to_owned();
        let param_val_2 = "456".to_owned();
        let expected = create_ast_add(&expression, &param_val_1, &param_val_2);

        let intermediate_ast = create_intermediate_ast_add(&expression, &param_val_1, &param_val_2);
        let result = Ast::from(&intermediate_ast);

        assert_eq!(result, expected);
    }
}
