use the_super_tiny_compiler_lib::{Ast, CodeGenerator, Lexer};

fn main() {
    let all_tokens = Lexer::parse("");
    let mut all_tokens_iter = all_tokens.iter();

    let ast = Ast::from(&mut all_tokens_iter);

    let generator = CodeGenerator::run(&ast);
}
