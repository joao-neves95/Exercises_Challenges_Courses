use crate::resp_parser::shared::RespCommand;

use std::{collections::HashMap, sync::Arc, time::SystemTime};

use anyhow::{Error, Result};
use tokio::sync::Mutex;

#[derive(Debug)]
pub struct AppContext<'a> {
    mem_db: &'a Arc<Mutex<HashMap<String, InMemoryRecord>>>,

    request: Request<'a>,

    pub response: Option<Response>,
}

#[derive(Debug)]
pub struct Request<'a> {
    pub raw_command: &'a str,
    pub resp_command: Option<RespCommand>,
}

#[derive(Debug)]
pub struct Response {
    pub command_response: String,
}

impl<'a> AppContext<'a> {
    pub fn new(
        mem_db: &'a Arc<Mutex<HashMap<String, InMemoryRecord>>>,
        raw_request_buffer: &'a [u8],
    ) -> Result<Self, Error> {
        Ok(AppContext {
            mem_db,
            request: Request {
                raw_command: std::str::from_utf8(&raw_request_buffer)?,
                resp_command: None,
            },
            response: None,
        })
    }

    pub fn get_mem_db_ref(&self) -> &'a Arc<Mutex<HashMap<String, InMemoryRecord>>> {
        &self.mem_db
    }

    pub fn get_request_ref(&self) -> &Request<'a> {
        &self.request
    }

    pub fn set_request_resp_command(&mut self, resp_command: RespCommand) -> &Self {
        if self.request.resp_command.is_none() {
            self.request.resp_command = Some(resp_command);
        }

        self
    }

    pub fn get_request_resp_command_ref(&self) -> Option<&RespCommand> {
        self.request.resp_command.as_ref()
    }

    pub fn set_response_command_response(&mut self, command_response: String) -> &Self {
        self.response = Some(Response { command_response });

        self
    }

    pub fn unwrap_response_command_response(&self) -> &String {
        &self.response.as_ref().unwrap().command_response
    }

    pub fn format_request_info(&self, include_mem_db: bool) -> String {
        format!(
            "request: {:?}, mem_db: {:?}",
            self.get_request_resp_command_ref().unwrap(),
            if include_mem_db {
                self.mem_db.to_owned()
            } else {
                Arc::new(Mutex::new(HashMap::<String, InMemoryRecord>::new()))
            }
        )
    }
}

#[derive(Debug)]
pub struct InMemoryRecord {
    pub value: String,
    pub last_update_time: SystemTime,
    pub expire_milli: Option<u128>,
}

impl InMemoryRecord {
    pub fn new(value: String, expire_milli: Option<u128>) -> Self {
        InMemoryRecord {
            value,
            // No need for UTC. This is just an internal date.
            last_update_time: SystemTime::now(),
            expire_milli,
        }
    }

    pub fn has_expired(&self) -> Result<bool, Error> {
        Ok(self.expire_milli.is_some()
            && SystemTime::now()
                .duration_since(self.last_update_time)?
                .as_millis()
                > self.expire_milli.unwrap())
    }
}

#[cfg(test)]
mod tests {
    use std::{thread, time::Duration};

    use super::InMemoryRecord;

    #[test]
    fn has_expired_passes() -> Result<(), anyhow::Error> {
        let expires = InMemoryRecord::new("".to_owned(), Some(100));
        let does_not_expire = InMemoryRecord::new("".to_owned(), Some(130));

        thread::sleep(Duration::from_millis(101));

        assert_eq!(expires.has_expired()?, true);
        assert_eq!(does_not_expire.has_expired()?, false);

        Ok(())
    }
}
