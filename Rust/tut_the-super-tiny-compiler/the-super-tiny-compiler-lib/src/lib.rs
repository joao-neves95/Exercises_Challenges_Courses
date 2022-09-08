mod mock_data;
pub use mock_data::create_tokens_vec_add;
pub use mock_data::create_tokens_vec_concat;

mod constants;
mod models;

mod lexer;
pub use lexer::Lexer;

mod intermediate_ast;
pub use intermediate_ast::IntermediateAst;

mod code_generator;
pub use code_generator::CodeGenerator;
