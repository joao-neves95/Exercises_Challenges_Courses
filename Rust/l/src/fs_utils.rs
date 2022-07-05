use std::{
    env,
    fs::{self, DirEntry},
};

use anyhow::Result;

pub struct FsUtils {}

impl FsUtils {
    pub fn read_dir<P>(mut dir_entry_filters: Vec<P>) -> Result<Vec<DirEntry>>
    where
        P: FnMut(&DirEntry) -> bool,
    {
        Ok(fs::read_dir(env::current_dir()?)?
            .filter(|dir_entry| {
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
            .collect::<Vec<DirEntry>>())
    }

    pub fn get_dir_entry_file_name(dir_entry: &DirEntry) -> String {
        match dir_entry.path().file_name() {
            Some(name) => name.to_str().unwrap_or(""),
            None => "..",
        }
        .to_owned()
    }
}
