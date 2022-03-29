use actix_web::{http::header::ContentType, web, HttpResponse};
use anyhow::Context;
use bson::DateTime;
use bson::{doc, Document};
use futures::stream::StreamExt;
use mongodb::{options::FindOptions, Client, Collection};
use std::str::FromStr;
use std::sync::Mutex;

use crate::models::{delete_log_request::DeleteLogRequest, post_log_request::PostLogRequest};

const MONGO_DB: &'static str = "logs-1";
const MONGO_DB_COLL_LOGS: &'static str = "logs";

pub fn service_config(cfg: &mut web::ServiceConfig) {
    cfg.service(
        web::resource("logs")
            .route(web::get().to(get_logs))
            .route(web::post().to(post_log)),
    );

    cfg.service(web::resource("logs/{log_id}").route(web::delete().to(delete_log)));
}

async fn get_logs(data: web::Data<Mutex<Client>>) -> HttpResponse {
    let logs_collection = get_logs_collection(data);

    let find_options = FindOptions::builder().sort(doc! { "_id": -1i32 }).build();
    let mut cursor = logs_collection
        .find(doc! {}, find_options)
        .await
        .with_context(|| format!("Error while connecting to MongoDB"))
        // Any possible panic from a failed connection to the DB will be picked up by Anyhow.
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

async fn post_log(
    data: web::Data<Mutex<Client>>,
    new_log: web::Json<PostLogRequest>,
) -> HttpResponse {
    let logs_collection = get_logs_collection(data);

    match logs_collection
        .insert_one(
            doc! { "deviceId": &new_log.device_id, "message": &new_log.message, "createdOn": DateTime::now() },
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

async fn delete_log(
    data: web::Data<Mutex<Client>>,
    log_req: web::Path<DeleteLogRequest>,
) -> HttpResponse {
    let logs_collection = get_logs_collection(data);

    match logs_collection
        .find_one_and_delete(
            doc! {
                "_id": bson::oid::ObjectId::from_str(&log_req.log_id).unwrap()
            },
            None,
        )
        .await
    {
        Ok(db_result) => {
            if let Some(log) = db_result {
                println!("Deleted the document \"{:?}\"", &log_req.log_id);

                return HttpResponse::Ok().json(log);
            } else {
                println!("The document with id \"{}\" was not found", &log_req.log_id);

                return HttpResponse::NotFound().body("NOT FOUND");
            }
        }
        Err(err) => {
            println!("Failed! {:#?}", err);

            return HttpResponse::InternalServerError().finish();
        }
    };
}

fn get_logs_collection(data: web::Data<Mutex<Client>>) -> Collection<Document> {
    data.lock()
        .unwrap()
        .database(MONGO_DB)
        .collection(MONGO_DB_COLL_LOGS)
}
