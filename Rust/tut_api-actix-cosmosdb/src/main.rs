use actix_web::{web, App, HttpServer};
use anyhow::{anyhow, Context};
use dotenv::dotenv;
use mongodb::{options::ClientOptions, Client};
use std::env;
use std::sync::*;

mod controllers;
mod models;

struct MongoDbOptions<'l> {
    connection_string: &'l String,
    app_name: &'l String,
}

#[actix_rt::main]
async fn main() -> anyhow::Result<(), anyhow::Error> {
    let addr = "127.0.0.1:8080";
    std::env::set_var("RUST_LOG", "actix_web=debug");

    println!("Starting the HTTP server...");

    dotenv().ok();

    let mongodb_args = MongoDbOptions {
        connection_string: &env::var("mongodb_connection_string").unwrap_or("".to_string()),
        app_name: &env::var("app_name").unwrap(),
    };

    let mut mongodb_client_options = ClientOptions::parse(mongodb_args.connection_string)
        .await
        .with_context(|| {
            format!(
                "Invalid MongoDB connection string: \"{}\".",
                mongodb_args.connection_string
            )
        })?;

    mongodb_client_options.app_name = Some(mongodb_args.app_name.to_string());
    let mongodb_client = web::Data::new(Mutex::new(Client::with_options(mongodb_client_options)?));

    Ok(match HttpServer::new(move || {
        App::new()
            .app_data(mongodb_client.clone())
            .configure(app_configuration)
    })
    .bind(addr)
    {
        Ok(http_server) => {
            println!("The HTTP server started. Listening on {}...", addr);
            http_server
        }
        Err(err) => {
            return Err(anyhow!(
                "An error occurred while starting the HTTP server:\n{:#?}",
                err
            ));
        }
    }
    .run()
    .await?)
}

fn app_configuration(cfg: &mut web::ServiceConfig) {
    controllers::home_controller::service_config(cfg);
    controllers::logs_controller::service_config(cfg);
}
