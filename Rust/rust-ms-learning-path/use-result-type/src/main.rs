use std::{
    fs::File,
    io::{Error, Read},
    path::PathBuf,
};

fn read_file_contents(path: PathBuf) -> Result<String, Error> {
    let mut file_contents = String::new();

    let mut file: File = match File::open(path) {
        Ok(file_handle) => file_handle,
        Err(io_error) => return Err(io_error),
    };

    match file.read_to_string(&mut file_contents) {
        Ok(_) => (),
        Err(io_error) => return Err(io_error),
    };

    return Ok(file_contents);
}

fn main() {
    println!("Let's read the contents of this file and handle errors with `Result`.");

    if read_file_contents(PathBuf::from("src/main.rs")).is_ok() {
        println!("The program found this file.");
    }

    if read_file_contents(PathBuf::from("non-existent-file.txt")).is_err() {
        println!("The program raised an error for the non-exitent file.");
    }
}
