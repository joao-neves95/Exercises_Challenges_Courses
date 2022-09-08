use crate::{
    constants::{AstNodeType, AstStatementType, NodeCalleeType},
    IntermediateAst,
};

#[derive(Debug, PartialEq)]
pub struct Ast<'a> {
    ast_type: &'a str,
    body: Vec<AstStatement<'a>>,
}

#[derive(Debug, PartialEq)]
pub struct AstStatement<'a> {
    statement_type: AstStatementType,
    expression: AstNode<'a>,
}

#[derive(Debug, PartialEq)]
pub struct AstNode<'a> {
    pub node_type: AstNodeType,
    pub callee: Option<AstNodeCallee<'a>>,
    pub value: Option<&'a String>,
    pub params: Option<Vec<AstNode<'a>>>,
}

#[derive(Debug, PartialEq)]
pub struct AstNodeCallee<'a> {
    callee_type: NodeCalleeType,
    name: &'a str,
}

impl<'a> From<&'a IntermediateAst<'a>> for Ast<'a> {
    fn from(_: &'a IntermediateAst<'a>) -> Self {
        let ast = Ast {
            ast_type: "Program",
            body: Vec::new(),
        };

        ast
    }
}

// TODO: Add test for `(add 2 (subtract 4 2))`
#[cfg(test)]
mod tests {
    use crate::{
        constants::{AstNodeType, AstStatementType, NodeCalleeType},
        mock_data::create_intermediate_ast_add,
        Ast,
    };

    use super::{AstNode, AstNodeCallee, AstStatement};

    #[test]
    fn transform_intermediate_ast_add_passes() {
        let expression = "add".to_owned();
        let param_val_1 = "123".to_owned();
        let param_val_2 = "456".to_owned();

        let intermediate_ast = create_intermediate_ast_add(&expression, &param_val_1, &param_val_2);

        let expected = Ast {
            ast_type: "Program",
            body: vec![AstStatement {
                statement_type: AstStatementType::ExpressionStatement,
                expression: AstNode {
                    node_type: AstNodeType::CallExpression,
                    value: None,
                    callee: Some(AstNodeCallee {
                        callee_type: NodeCalleeType::Identifier,
                        name: "Add",
                    }),
                    params: Some(vec![
                        AstNode {
                            node_type: AstNodeType::NumberLiteral,
                            value: Some(&param_val_1),
                            callee: None,
                            params: None,
                        },
                        AstNode {
                            node_type: AstNodeType::NumberLiteral,
                            value: Some(&param_val_2),
                            callee: None,
                            params: None,
                        },
                    ]),
                },
            }],
        };

        let result = Ast::from(&intermediate_ast);

        assert_eq!(result, expected);
    }
}
