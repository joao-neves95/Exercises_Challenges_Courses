use std::{
    env,
    fs::{self, DirEntry},
};

pub struct FsUtils {}

impl FsUtils {
    pub fn read_dir<P>(mut dir_entry_filters: Vec<P>) -> Vec<DirEntry>
    where
        P: FnMut(&DirEntry) -> bool,
    {
        fs::read_dir(env::current_dir().unwrap())
            .unwrap()
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
            .collect::<Vec<DirEntry>>()
    }
}
