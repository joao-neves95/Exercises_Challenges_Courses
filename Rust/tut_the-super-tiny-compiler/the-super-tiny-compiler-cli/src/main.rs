use the_super_tiny_compiler_lib::{CodeGenerator, IntermediateAst, Lexer};

fn main() {
    let all_tokens = Lexer::parse("");
    let mut all_tokens_iter = all_tokens.iter();

    let intermediate_ast = IntermediateAst::from(&mut all_tokens_iter);

    let generator = CodeGenerator::run(&intermediate_ast);
}
