use serde::{self, Deserialize};

#[derive(Deserialize)]
pub struct DeleteLogRequest {
    pub log_id: String,
}
