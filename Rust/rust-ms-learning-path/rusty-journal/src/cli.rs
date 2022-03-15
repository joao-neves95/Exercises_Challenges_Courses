use std::path::PathBuf;
use structopt::StructOpt;

#[derive(Debug, StructOpt)]
pub enum Action {
    /// Add a new task to the journal.
    Add {
        #[structopt()]
        task: String,
    },
    /// Mark a task as complete.
    Done {
        #[structopt()]
        position: usize,
    },
    /// List all tasks currently on the journal.
    List,
}

#[derive(Debug, StructOpt)]
#[structopt(name = "Rusty journal", about = "A to-do CLI app")]
pub struct CommandLineArgs {
    #[structopt(subcommand)]
    pub action: Action,

    #[structopt(parse(from_os_str), short, long)]
    pub journal_file: Option<PathBuf>,
}
