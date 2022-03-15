use core::fmt;
use std::io::Error;
use std::path::PathBuf;

use chrono::{serde::ts_seconds, DateTime, Utc};
use serde::Deserialize;
use serde::Serialize;

use crate::task_file_db;

#[derive(Debug, Deserialize, Serialize)]
pub struct Task {
    pub text: String,

    #[serde(with = "ts_seconds")]
    pub created_at: DateTime<Utc>,
}

impl fmt::Display for Task {
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        let created_at = self.created_at.naive_local().format("%F %H:%M");

        write!(f, "{:<50} [{}]", self.text, created_at)
    }
}

impl Task {
    pub fn new(text: String) -> Task {
        Task {
            text: text,
            created_at: Utc::now(),
        }
    }

    pub fn print_tasks(journal_path: PathBuf) -> Result<(), Error> {
        let all_tasks = task_file_db::get_all_tasks(journal_path)?;

        for i in 0..all_tasks.len() {
            println!("{}: {}", i + 1, all_tasks[i]);
        }

        Ok(())
    }

    pub fn add_task(journal_path: PathBuf, task: Task) -> Result<(), Error> {
        task_file_db::add_task(journal_path, task)
    }

    pub fn complete_task(journal_path: PathBuf, task_position: usize) -> Result<(), Error> {
        task_file_db::complete_task(journal_path, task_position)
    }

    pub fn find_default_db_path() -> Option<PathBuf> {
        task_file_db::find_default_db_path()
    }
}
