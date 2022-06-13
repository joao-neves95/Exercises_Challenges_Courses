use std::{env, fs};

fn main() {
    // TODO: Error handling.
    let all_entries = fs::read_dir(env::current_dir().unwrap())
        .unwrap()
        .map(|entry_res| match entry_res.unwrap().path().file_name() {
            Some(name) => name.to_os_string().into_string().unwrap(),
            None => "..".to_owned(),
        })
        .collect::<Vec<String>>();

    for entry in all_entries {
        println!("{}", entry);
    }
}
