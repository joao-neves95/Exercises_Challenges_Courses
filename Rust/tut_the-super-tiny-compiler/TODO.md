# TODO

- [IntermediateAst]:
  - [ ] Add test for `(add 2 (subtract 4 2))`
  - [ ] Add test for `(concat "Hello" "Rust")`
  - [ ] Add support for binary operations/expressions like ArithmeticOperator (numeric operators +, -, *, /).
- [Ast]:
  - [ ] Add test for `(add 2 (subtract 4 2))`
  - [ ] Add test for `(concat "Hello" "Rust")`
- [CodeGenerator]:
  - [ ] Implement compile_lisp_to_rust_print_statements()
  - [ ] Add support for one Common Lisp function: `(concatenate 'string "Hello" "Rust" ", from" "Lisp???")`.
- (Global)
  - [ ] Refactor the code (divide into multiple functions, patterns, etc.).
  - [ ] Use bytes instead of String.
