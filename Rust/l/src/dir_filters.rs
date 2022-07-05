use std::fs::DirEntry;

use crate::{cli_args::CliArgs, fs_utils::FsUtils};

#[derive(Debug)]
pub struct DirFilters {}

impl DirFilters {
    pub fn build_filters(cli_args: &CliArgs) -> Vec<Box<dyn FnMut(&DirEntry) -> bool>> {
        let mut dir_filters: Vec<Box<dyn FnMut(&DirEntry) -> bool>> = Vec::new();

        if !cli_args.all {
            dir_filters.push(Box::new(filter_out_hidden_files));
        }

        dir_filters
    }
}

fn filter_out_hidden_files(dir_entry: &DirEntry) -> bool {
    !FsUtils::get_dir_entry_file_name(dir_entry).starts_with('.')
}
