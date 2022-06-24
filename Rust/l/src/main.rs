use std::fs::DirEntry;

use cli_args::CliArgs;
use fs_utils::FsUtils;

mod cli_args;
mod fs_utils;

// TODO: Error handling.
fn main() -> () {
    let _cli_args: CliArgs;

    let dir_filters = vec![filter_out_hidden_files];
    let all_entries = FsUtils::read_dir(dir_filters);

    for entry in all_entries {
        let entry_name = match entry.path().file_name() {
            Some(name) => name.to_str().unwrap_or("").to_owned(),
            None => "..".to_owned(),
        };

        println!("{}", entry_name);
    }
}

fn filter_out_hidden_files(dir_entry: &DirEntry) -> bool {
    !match dir_entry.path().file_name() {
        Some(name) => name.to_str().unwrap_or(""),
        None => "..",
    }
    .starts_with('.')
}
