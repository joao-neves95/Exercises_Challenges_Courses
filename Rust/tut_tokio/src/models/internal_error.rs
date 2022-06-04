use core::fmt::write;
use std::{error::Error, fmt::Display};

#[derive(Debug)]
pub struct InternalError {
    pub message: String,
    pub inner_error: Option<String>,
}

impl InternalError {
    #[allow(dead_code)]
    pub fn new(message: &str) -> InternalError {
        InternalError {
            message: message.to_string(),
            inner_error: None,
        }
    }

    pub fn new_inner(message: &str, inner_error_message: &str) -> InternalError {
        InternalError {
            message: message.to_string(),
            inner_error: Some(inner_error_message.to_string()),
        }
    }
}

impl Display for InternalError {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let mut message = format!("Error: {}", self.message);

        if let Some(inner_error) = self.inner_error.as_ref() {
            message.push_str(format!("Inner Error: {}", inner_error).as_str());
        }

        write(f, format_args!("{}", message))
    }
}

impl Error for InternalError {}
