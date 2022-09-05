mod constants;
mod models;

mod lexer;
pub use lexer::Lexer;

mod ast;
pub use ast::Ast;

mod code_generator;
pub use code_generator::CodeGenerator;

#[cfg(test)]
mod tests {
    #[test]
    fn it_works() {
        let result = 2 + 2;
        assert_eq!(result, 4);
    }
}
