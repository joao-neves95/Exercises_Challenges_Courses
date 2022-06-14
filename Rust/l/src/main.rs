use fs_utils::FsUtils;

mod fs_utils;

// TODO: Error handling.
fn main() -> () {
    let all_entries = FsUtils::read_dir();

    for entry in all_entries {
        println!(
            "{}",
            match entry.path().file_name() {
                Some(file_name) => file_name.to_str().unwrap_or("").to_owned(),
                None => "..".to_owned(),
            }
        );
    }
}
