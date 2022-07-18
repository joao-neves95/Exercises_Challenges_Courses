use std::{
    env,
    fs::{self, DirEntry, ReadDir},
};

use anyhow::Result;

pub struct FsUtils {}

impl FsUtils {
    pub fn read_current_dir() -> Result<ReadDir> {
        Ok(fs::read_dir(env::current_dir()?)?)
    }

    pub fn filter_dir_entries<P>(dir: ReadDir, mut dir_entry_filters: Vec<P>) -> Vec<DirEntry>
    where
        P: FnMut(&DirEntry) -> bool,
    {
        dir.filter(|dir_entry| {
            match dir_entry {
                Ok(entry) => {
                    for filter in &mut dir_entry_filters {
                        if !filter(&entry) {
                            return false;
                        }
                    }
                }
                _ => return false,
            };

            true
        })
        .map(|dir_entry| dir_entry.unwrap())
        .collect::<Vec<DirEntry>>()
    }

    pub fn get_dir_entry_file_name(dir_entry: &DirEntry) -> String {
        match dir_entry.path().file_name() {
            Some(name) => name.to_str().unwrap_or(""),
            None => "..",
        }
        .to_owned()
    }
}

#[cfg(test)]
mod test {
    use super::FsUtils;
    use crate::{cli_args::CliArgs, dir_filters::DirFilters};

    use std::fs::{self, File};

    use anyhow::{Ok, Result};
    use tempfile::tempdir;

    const FILE_NAMES: [&str; 3] = [".hiddenfile", "anotherfile.txt", "code.rs"];

    #[test]
    fn should_filter_dir_entries() -> Result<(), anyhow::Error> {
        let temp_test_dir = tempdir()?;

        let mut temp_test_files = Vec::new();
        for file_name in FILE_NAMES {
            let new_test_file = File::create(temp_test_dir.path().join(file_name))?;
            temp_test_files.push(new_test_file);
        }

        let filtered_dir_entries = FsUtils::filter_dir_entries(
            fs::read_dir(temp_test_dir.path())?,
            DirFilters::build_filters(&CliArgs {
                all: false,
                list: true,
            }),
        );

        assert_eq!(filtered_dir_entries.len(), FILE_NAMES.len() - 1);
        assert!(!filtered_dir_entries
            .iter()
            .any(|file_entry| file_entry.path().starts_with(".")));

        for test_temp_file in temp_test_files {
            drop(test_temp_file);
        }
        temp_test_dir.close()?;

        Ok(())
    }
}
