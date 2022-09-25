use std::slice::Iter;

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

        let expected = create_ast_add(&expression, &param_val_1, &param_val_2);

        let result = Ast::from(&intermediate_ast);

        assert_eq!(result, expected);
    }
}
