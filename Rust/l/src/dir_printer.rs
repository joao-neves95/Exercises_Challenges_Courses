use std::{fmt::Display, fs::DirEntry};

pub struct DirPrinter {
    dir_entries: Vec<DirEntry>,
}

impl DirPrinter {
    pub fn new(dir_entries: Vec<DirEntry>) -> DirPrinter {
        DirPrinter { dir_entries }
    }

    pub fn print(&self) {
        print!("{}", self)
    }
}

impl Display for DirPrinter {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        print_dir_list(&self.dir_entries, f)
    }
}

fn print_dir_list(
    dir_entries: &Vec<DirEntry>,
    f: &mut std::fmt::Formatter<'_>,
) -> std::fmt::Result {
    for entry in dir_entries {
        let entry_name = match entry.path().file_name() {
            Some(name) => name.to_str().unwrap_or("").to_owned(),
            None => "..".to_owned(),
        };

        writeln!(f, "{}", entry_name)?;
    }

    Ok(())
}
