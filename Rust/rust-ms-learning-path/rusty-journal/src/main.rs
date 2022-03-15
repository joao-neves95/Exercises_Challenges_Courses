use structopt::StructOpt;

mod cli;
mod task_file_db;
mod tasks;

use cli::Action;
use tasks::Task;

fn main() {
    // let cli::CommandLineArgs {
    //     action,
    //     journal_file,
    // } = cli::CommandLineArgs::from_args();

    let args = cli::CommandLineArgs::from_args();
    let journal_path = args
        .journal_file
        .or_else(Task::find_default_db_path)
        .expect("Failed to find the DB file");

    match args.action {
        Action::Add { task } => Task::add_task(journal_path, Task::new(task)),
        Action::Done { position } => Task::complete_task(journal_path, position),
        Action::List => tasks::Task::print_tasks(journal_path),
    }
    .expect("Failed to run the command");
}
