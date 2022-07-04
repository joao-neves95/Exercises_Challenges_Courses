use clap::Parser;
use cli_args::CliArgs;
use dir_filters::DirFilters;
use fs_utils::FsUtils;

mod cli_args;
mod dir_filters;
mod fs_utils;

// TODO: Error handling.
fn main() -> () {
    let cli_args = CliArgs::parse();

    let dir_filters = DirFilters::filter(cli_args);
    let all_entries = FsUtils::read_dir(dir_filters);

    for entry in all_entries {
        let entry_name = match entry.path().file_name() {
            Some(name) => name.to_str().unwrap_or("").to_owned(),
            None => "..".to_owned(),
        };

        println!("{}", entry_name);
    }
}
