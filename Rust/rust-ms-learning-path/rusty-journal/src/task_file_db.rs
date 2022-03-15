use std::{
    fs::{File, OpenOptions},
    io::{Error, Seek, SeekFrom},
    path::PathBuf,
};

use crate::tasks::Task;

pub fn db_name() -> String {
    String::from(".rusty-journal.json")
}

pub fn find_default_db_path() -> Option<PathBuf> {
    let mut dir_path = home::home_dir()?;
    dir_path.push(db_name());

    Some(dir_path)
}

pub fn add_task(journal_path: PathBuf, task: Task) -> Result<(), Error> {
    let mut file = open_file_db(journal_path, Some(true))?;
    let mut tasks = read_all_tasks_from_file(&mut file)?;

    tasks.push(task);
    serde_json::to_writer(file, &tasks)?;

    Ok(())
}

pub fn complete_task(journal_path: PathBuf, task_position: usize) -> Result<(), Error> {
    let mut file = open_file_db(journal_path, None)?;
    let mut tasks = read_all_tasks_from_file(&mut file)?;

    if task_position == 0 || task_position > tasks.len() {
        return Err(Error::new(
            std::io::ErrorKind::InvalidInput,
            "Invalid task ID",
        ));
    }

    tasks.remove(task_position - 1);
    file.set_len(0)?;
    serde_json::to_writer(file, &tasks)?;

    Ok(())
}

pub fn get_all_tasks(journal_path: PathBuf) -> Result<Vec<Task>, Error> {
    Ok(read_all_tasks_from_file(&mut open_file_db(
        journal_path,
        None,
    )?)?)
}

/// create_if_no_exist: Option<bool> - Defaults to false.
fn open_file_db(journal_path: PathBuf, create_if_no_exist: Option<bool>) -> Result<File, Error> {
    OpenOptions::new()
        .read(true)
        .write(true)
        .create(create_if_no_exist.unwrap_or(false))
        .open(journal_path)
}

// fn read_all_tasks_from_file(mut file: &File) -> Result<Vec<Task>, Error> {
fn read_all_tasks_from_file(file: &mut File) -> Result<Vec<Task>, Error> {
    file.seek(SeekFrom::Start(0))?;

    let tasks: Vec<Task> = match serde_json::from_reader(&*file) {
        Ok(tasks) => tasks,
        Err(err) if err.is_eof() => Vec::new(),
        Err(err) => Err(err)?,
    };

    file.rewind()?;

    Ok(tasks)
}
