use crate::{constants::CStdLibs, Ast};

use std::fmt::Display;

use concat_strs::concat_strs;

pub struct CodeGenerator {}

pub struct CPrintStatements {
    pub includes: Vec<CStdLibs>,
    pub main: Vec<String>,
}

impl Display for CPrintStatements {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let includes = self
            .includes
            .iter()
            .map(|include| {
                concat_strs!(
                    "#include <",
                    match include {
                        CStdLibs::StdioH => "stdio.h",
                    },
                    ">\n"
                )
            })
            .collect::<String>();

        let code = concat_strs!(
            &includes,
            "int main() {\n",
            &self.main.join(""),
            "return 0;\n}"
        );

        write!(f, "{}", code)
    }
}

// TODO: Consider compiling to Rust only.
impl CodeGenerator {
    // Lisp        ||   C
    // (add 2 2)   ||   add(2, 2);
    // (+ 2 2)     ||   2 + 2;
    pub fn compile_lisp_to_c(ast: &Ast) -> () {
        todo!()
    }

    // Lisp        ||   C print statements (inside `main`)
    // (add 2 2)   ||   printf("%f", add(2, 2));
    // (+ 2 2)     ||   printf("%f", 2 + 2);
    pub fn compile_lisp_to_c_print_statements(ast: &Ast) -> () {
        todo!()
    }

    // Lisp        ||   Rust
    // (add 2 2)   ||   add(2, 2);
    // (+ 2 2)     ||   2 + 2;
    pub fn compile_lisp_to_rust(ast: &Ast) -> () {
        todo!()
    }

    // Lisp        ||   Rust print statements (inside `main`)
    // (add 2 2)   ||   println!("{}", add(2, 2));
    // (+ 2 2)     ||   println!("{}", 2 + 2);
    pub fn compile_lisp_to_rust_print_statements(ast: &Ast) -> () {
        todo!()
    }
}
