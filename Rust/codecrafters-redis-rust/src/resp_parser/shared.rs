pub struct RespCommandNames {}

impl RespCommandNames {
    pub const PING: &str = "PING";
    pub const ECHO: &str = "ECHO";
    pub const GET: &str = "GET";
    pub const SET: &str = "SET";
}

pub struct RespCommandSetOptions {}

impl RespCommandSetOptions {
    pub const EXPIRY: &str = "PX";
}

pub struct RespDataTypesFirstByte {}

impl RespDataTypesFirstByte {
    pub const ARRAYS_STR: &str = "*";

    pub const BULK_STRINGS_CHAR: char = '$';
}

#[derive(Debug)]
pub enum RespData {
    BulkString { size: u32, value: String },
}

impl RespData {
    pub fn get_bulk_string_value(&self) -> &String {
        let RespData::BulkString { size: _, value } = self;

        value
    }
}

#[derive(Debug)]
pub struct RespCommand {
    pub name: String,

    pub parameters: Vec<String>,
}
