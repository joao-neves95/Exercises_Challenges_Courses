use crate::{
    ast::AstNode,
    constants::{AstNodeType, RustLibs},
    Ast,
};

use std::{fmt::Display, slice::Iter};

use concat_strs::concat_strs;

pub struct CodeGenerator {}

struct RustProgram {
    pub uses: Vec<RustLibs>,
    pub main: Vec<String>,
}

impl Display for RustProgram {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let uses = self
            .uses
            .iter()
            .map(|include| {
                concat_strs!(
                    "use ",
                    match include {
                        RustLibs::StdStrChars => "std::str::Chars",
                    },
                    ";\n"
                )
            })
            .collect::<String>();

        write!(
            f,
            "{}",
            concat_strs!(&uses, "fn main() {\n", &self.main.join(""), "\n}")
        )
    }
}

impl CodeGenerator {
    // TODO: Implement:
    // Done? || Lisp       || Rust
    // [x]   || (add 2 2)  || add(2, 2);
    // [ ]   || (+ 2 2)    || 2 + 2;
    pub fn compile_lisp_to_rust(ast: &Ast) -> String {
        let mut rust_program = RustProgram {
            uses: Vec::new(),
            main: Vec::new(),
        };

        for node in ast.body.iter() {
            rust_program.main.push(generate_code_recursive(node));
        }

        rust_program.to_string()
    }

    // TODO: Implement:
    // Done? || Lisp       || Rust
    // [ ]   || (add 2 2)  || println!("{}", add(2, 2));
    // [ ]   || (+ 2 2)    || println!("{}", 2 + 2);
    pub fn compile_lisp_to_rust_print_statements(ast: &Ast) -> () {
        todo!()
    }
}

// TODO: Add support for one Common Lisp function: `(concatenate 'string "Hello" "Rust" ", from" "Lisp???")`
fn generate_code_recursive<'a>(node: &AstNode) -> String {
    match node.node_type {
        crate::constants::AstNodeType::ExpressionStatement => match &node.expression {
            None => panic!("{} has no expression.", AstNodeType::ExpressionStatement),
            Some(unwrapped_expression) => {
                concat_strs!(&generate_code_recursive(&unwrapped_expression), ";")
            }
        },
        crate::constants::AstNodeType::CallExpression => {
            let identifier_name = match &node.callee {
                None => panic!("{} has no identifier.", AstNodeType::CallExpression),
                Some(unwrapped_callee) => unwrapped_callee.name,
            };

            let arguments = match &node.params {
                None => Vec::new(),
                Some(unwrapped_params) => unwrapped_params
                    .iter()
                    .map(|param_node| generate_code_recursive(param_node))
                    .collect::<Vec<String>>(),
            };

            concat_strs!(identifier_name, '(', &arguments.join(", "), ')')
        }

        crate::constants::AstNodeType::NumberLiteral => match node.value {
            None => panic!("{} has no value.", AstNodeType::NumberLiteral),
            Some(unwrapped_value) => unwrapped_value.to_owned(),
        },
        crate::constants::AstNodeType::StringLiteral => match node.value {
            None => panic!("{} has no value.", AstNodeType::StringLiteral),
            Some(unwrapped_value) => concat_strs!(r#"""#, unwrapped_value, r#"""#),
        },
    }
}

#[cfg(test)]
mod tests {
    use concat_strs::concat_strs;
    use pretty_assertions::assert_eq;

    use crate::{
        mock_data::{create_ast_add, create_rust_code_string_add},
        CodeGenerator,
    };

    #[test]
    fn compile_lisp_to_rust_add_passes() {
        let expression_name = "add".to_owned();
        let param_val_1 = "123".to_owned();
        let param_val_2 = "456".to_owned();
        let input_ast = create_ast_add(&expression_name, &param_val_1, &param_val_2);
        let expected = create_rust_code_string_add(&expression_name, &param_val_1, &param_val_2);

        let result = CodeGenerator::compile_lisp_to_rust(&input_ast);

        assert_eq!(result, expected);
    }

    #[test]
    fn collect_map_concat_into_string_passes() {
        let result = vec!["shfg".to_owned(), "adgfdafg".to_owned(), "adsfg".to_owned()]
            .iter()
            .map(|s| concat_strs!(s, ";"))
            .collect::<String>();

        assert_eq!(result, "shfg;adgfdafg;adsfg;");
    }

    #[test]
    fn concat_raw_str_passes() {
        let result = concat_strs!(r#"""#, "value", r#"""#);

        assert_eq!(result, "\"value\"");
    }

    #[test]
    fn join_empty_vec_passes() {
        let empty_vec: Vec<String> = Vec::new();
        let result = empty_vec.join(",");

        assert_eq!(result, "");
    }
}
