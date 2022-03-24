use actix_web::{web, App, HttpServer};
use mongodb::{options::ClientOptions, Client};
use std::{
    io::{Error, ErrorKind},
    sync::*,
};

mod controllers;

struct MongoDbOptions<'l> {
    connection_string: &'l str,
    app_name: &'l str,
}

#[actix_rt::main]
async fn main() -> std::io::Result<()> {
    let addr = "127.0.0.1:8080";
    std::env::set_var("RUST_LOG", "actix_web=debug");

    let mongodb_args = MongoDbOptions {
        connection_string: "",
        app_name: "",
    };

    println!("Starting the HTTP server...");

    // let mut mongodb_client_options =
    //     match ClientOptions::parse(mongodb_args.connection_string).await {
    //         Ok(client_options) => client_options,
    //         Err(err) => {
    //             println!(
    //                 "An error occurred during the MongoDb connection. Connection string: \"{}\"",
    //                 mongodb_args.connection_string
    //             );

    //             return Err(Error::new(ErrorKind::InvalidInput, err));
    //         }
    //     };

    // mongodb_client_options.app_name = Some(mongodb_args.app_name.to_string());
    // let mongodb_client = web::Data::new(Mutex::new(Client::with_options(mongodb_client_options)));

    match HttpServer::new(move || {
        App::new()
            // .app_data(mongodb_client.clone())
            .configure(app_configuration)
    })
    .bind(addr)
    {
        Ok(http_server) => {
            println!("The HTTP server started. Listening on {}...", addr);
            http_server
        }
        Err(err) => {
            println!("An error occurred while starting the HTTP server");
            return Err(Error::new(err.kind(), err));
        }
    }
    .run()
    .await
}

fn app_configuration(cfg: &mut web::ServiceConfig) {
    controllers::home_controller::service_config(cfg);
    controllers::logs_controller::service_config(cfg);
}
