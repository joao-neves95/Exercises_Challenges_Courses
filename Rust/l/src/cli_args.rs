use clap::Parser;

#[derive(Parser, Debug)]
pub struct CliArgs {
    #[clap(short = 'l', value_parser)]
    pub list: bool,
    #[clap(short = 'a', value_parser)]
    pub all: bool,
}
