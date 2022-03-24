use actix_web::{web, Responder};

pub fn service_config(cfg: &mut web::ServiceConfig) {
    cfg.service(
        web::resource("logs")
            .route(web::get().to(get_logs))
            .route(web::post().to(post_log)),
    );
}

async fn get_logs() -> impl Responder {
    String::from("NOT YET IMPLEMENTED.")
}

async fn post_log() -> impl Responder {
    String::from("NOT YET IMPLEMENTED.")
}
