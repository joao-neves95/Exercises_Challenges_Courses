use std::{fmt::Display, fs::DirEntry};

use crate::cli_args::CliArgs;

pub struct DirPrinter<'a> {
    cli_args: &'a CliArgs,
    dir_entries: &'a Vec<DirEntry>,
}

impl<'a> DirPrinter<'a> {
    pub fn new(cli_args: &'a CliArgs, dir_entries: &'a Vec<DirEntry>) -> DirPrinter<'a> {
        DirPrinter {
            cli_args,
            dir_entries,
        }
    }

    pub fn print(&self) {
        print!("{}", self)
    }
}

impl<'a> Display for DirPrinter<'a> {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        if self.cli_args.list {
            print_dir_list(&self.dir_entries, f)
        } else {
            print_dir_inline(&self.dir_entries, f)
        }
    }
}

fn print_dir_inline(
    dir_entries: &Vec<DirEntry>,
    f: &mut std::fmt::Formatter<'_>,
) -> std::fmt::Result {
    for entry in dir_entries {
        write!(f, "{} ", get_dir_entry_file_name(&entry))?;
    }

    Ok(())
}

fn print_dir_list(
    dir_entries: &Vec<DirEntry>,
    f: &mut std::fmt::Formatter<'_>,
) -> std::fmt::Result {
    for entry in dir_entries {
        writeln!(f, "{}", get_dir_entry_file_name(&entry))?;
    }

    Ok(())
}

fn get_dir_entry_file_name(dir_entry: &DirEntry) -> String {
    match dir_entry.path().file_name() {
        Some(name) => name.to_str().unwrap_or(""),
        None => "..",
    }
    .to_owned()
}
