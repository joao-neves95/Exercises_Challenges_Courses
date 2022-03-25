use actix_web::{http::header::ContentType, web, HttpResponse};
use anyhow::Context;
use bson::DateTime;
use bson::{doc, Document};
use futures::stream::StreamExt;
use mongodb::{options::FindOptions, Client, Collection};
use serde::Deserialize;
use std::sync::Mutex;

const MONGO_DB: &'static str = "logs_1";
const MONGO_DB_COLL_LOGS: &'static str = "logs";

#[derive(Deserialize)]
pub struct NewLog {
    pub id: String,
    pub message: String,
}

pub fn service_config(cfg: &mut web::ServiceConfig) {
    cfg.service(
        web::resource("logs")
            .route(web::get().to(get_logs))
            .route(web::post().to(post_log)),
    );
}

async fn get_logs(data: web::Data<Mutex<Client>>) -> HttpResponse {
    let logs_collection = get_logs_collection(data);

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
            _ => return HttpResponse::InternalServerError().body("ERROR"),
        }
    }

    HttpResponse::Ok()
        .content_type(ContentType::json())
        .body(serde_json::to_string(&results).unwrap())
}

async fn post_log(data: web::Data<Mutex<Client>>, new_log: web::Json<NewLog>) -> HttpResponse {
    let logs_collection = get_logs_collection(data);

    match logs_collection
        .insert_one(
            doc! { "deviceId": &new_log.id, "message": &new_log.message, "createdOn": DateTime::now() },
            None,
        )
        .await
    {
        Ok(db_result) => {
            if let Some(new_id) = db_result.inserted_id.as_object_id() {
                println!("New document inserted with id \"{}\"", new_id);
            }

            return HttpResponse::Created().json(db_result.inserted_id);
        }
        Err(err) => {
            println!("Failed! {:#?}", err);
            return HttpResponse::InternalServerError().finish();
        }
    }
}

fn get_logs_collection(data: web::Data<Mutex<Client>>) -> Collection<Document> {
    data.lock()
        .unwrap()
        .database(MONGO_DB)
        .collection(MONGO_DB_COLL_LOGS)
}
