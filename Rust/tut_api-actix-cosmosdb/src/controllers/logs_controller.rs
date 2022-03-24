use actix_web::{
    error::ErrorInternalServerError,
    web::{self, Json},
    Responder,
};
use anyhow::Context;
use bson::{doc, Document};
use futures::stream::StreamExt;
use mongodb::{options::FindOptions, Client};
use std::sync::Mutex;

const MONGO_DB: &'static str = "logs_1";
const MONGO_DB_COLL_LOGS: &'static str = "logs";

pub fn service_config(cfg: &mut web::ServiceConfig) {
    cfg.service(
        web::resource("logs")
            .route(web::get().to(get_logs))
            .route(web::post().to(post_log)),
    );
}

async fn get_logs(data: web::Data<Mutex<Client>>) -> impl Responder {
    let logs_collection = data
        .lock()
        .unwrap()
        .database(MONGO_DB)
        .collection(MONGO_DB_COLL_LOGS);

    let filter = doc! {};
    let find_options = FindOptions::builder().sort(doc! { "_id": -1i32 }).build();
    let mut cursor = logs_collection
        .find(filter, find_options)
        .await
        .with_context(|| format!("Error while connecting to MongoDB"))
        .unwrap();

    let mut results: Vec<Document> = Vec::new();

    while let Some(result) = cursor.next().await {
        match result {
            Ok(document) => results.push(document),
            _ => return Err(ErrorInternalServerError(format!("ERROR"))),
            // _ => return Ok::<Json<Vec<Document>>, Error>(Json(Vec::new())),
        }
    }

    Ok(Json(results))
}

async fn post_log() -> impl Responder {
    String::from("NOT YET IMPLEMENTED.")
}
