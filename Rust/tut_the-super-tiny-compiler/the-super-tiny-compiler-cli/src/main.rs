use the_super_tiny_compiler_lib::{Ast, CodeGenerator, Lexer};

fn main() {
    let all_tokens = Lexer::run("");
    let ast = Ast::from(&all_tokens);
    let generator = CodeGenerator::run(&ast);
}
