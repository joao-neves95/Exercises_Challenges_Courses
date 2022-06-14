use std::{
    env,
    fs::{self, DirEntry},
};

pub struct FsUtils {}

impl FsUtils {
    pub fn read_dir() -> Vec<DirEntry> {
        fs::read_dir(env::current_dir().unwrap())
            .unwrap()
            .map(|dir_entry| dir_entry.unwrap())
            .collect::<Vec<DirEntry>>()
    }
}
