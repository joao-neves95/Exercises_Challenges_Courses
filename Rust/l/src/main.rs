use anyhow::{Ok, Result};
use clap::Parser;
use cli_args::CliArgs;
use dir_filters::DirFilters;
use dir_printer::DirPrinter;
use fs_utils::FsUtils;

mod cli_args;
mod dir_filters;
mod dir_printer;
mod fs_utils;

fn main() -> Result<()> {
    let cli_args = CliArgs::parse();
    let dir_entries = FsUtils::read_dir(DirFilters::build_filters(&cli_args))?;

    Ok(DirPrinter::new(&cli_args, &dir_entries).print())
}
