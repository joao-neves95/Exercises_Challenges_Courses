use std::sync::Arc;

use tokio::sync::Mutex;

pub struct Request {
    pub buffer: [u8; 32],
    pub balance_store: Arc<Mutex<f32>>,
}
