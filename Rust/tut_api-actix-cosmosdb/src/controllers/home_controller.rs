use actix_web::{web, Responder};

pub fn service_config(cfg: &mut web::ServiceConfig) {
    cfg.service(web::resource("/").route(web::get().to(hello_endpoint)));
}

async fn hello_endpoint() -> impl Responder {
    String::from("Hello from the Rust server!")
}
