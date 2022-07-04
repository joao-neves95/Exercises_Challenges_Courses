use std::fs::DirEntry;

use crate::cli_args::CliArgs;

#[derive(Debug)]
pub struct DirFilters {}

impl DirFilters {
    pub fn filter(cli_args: CliArgs) -> Vec<Box<dyn FnMut(&DirEntry) -> bool>> {
        let mut dir_filters: Vec<Box<dyn FnMut(&DirEntry) -> bool>> = Vec::new();

        if !cli_args.all {
            dir_filters.push(Box::new(filter_out_hidden_files));
        }

        dir_filters
    }
}

fn filter_out_hidden_files(dir_entry: &DirEntry) -> bool {
    !match dir_entry.path().file_name() {
        Some(name) => name.to_str().unwrap_or(""),
        None => "..",
    }
    .starts_with('.')
}
