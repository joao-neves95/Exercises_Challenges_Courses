use serde::{self, Deserialize};

#[derive(Deserialize)]
pub struct PostLogRequest {
    pub device_id: String,
    pub message: String,
}
