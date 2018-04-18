-- CREATE DATABASE:
CREATE DATABASE online_store_db;

USE online_store_db;

SHOW DATABASES;

-- CREATE TABLES:
CREATE TABLE country(
    country_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    country_code varchar(6) NOT NULL UNIQUE,
    country_name varchar(150) NOT NULL,
    Primary Key(country_id)
);

CREATE TABLE gender(
    gender_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    gender_name VARCHAR(30) NOT NULL UNIQUE,
    Primary Key(gender_id)
);

CREATE TABLE user(
    user_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    gender_id INT UNSIGNED NOT NULL,
    birth_date DATE NOT NULL,
    phone VARCHAR(20) NULL UNIQUE,
    country_id INT UNSIGNED NOT NULL,
    billing_address VARCHAR(150) NOT NULL,
    Primary Key(user_id),
    Foreign Key(gender_id) REFERENCES gender(gender_id),
    Foreign Key(country_id) REFERENCES country(country_id)
);

CREATE TABLE auth(
    auth_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    user_id INT UNSIGNED NOT NULL UNIQUE,
    user_email VARCHAR(70) NOT NULL UNIQUE,
    user_name VARCHAR(50) NOT NULL UNIQUE,
    user_pass VARCHAR(128) NOT NULL,
    Primary Key(auth_id),
    Foreign Key(user_id) REFERENCES user(user_id)
);

CREATE TABLE events(
    event_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    event_name VARCHAR(30) NOT NULL UNIQUE,
    Primary Key(event_id)
);

CREATE TABLE logs(
    log_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    user_id INT UNSIGNED NOT NULL DEFAULT 1,
    user_ip VARCHAR(15) NOT NULL,
    log_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    event_id INT UNSIGNED NOT NULL,
    Primary Key(log_id),
    Foreign Key(user_id) REFERENCES user(user_id),
    Foreign Key(event_id) REFERENCES events(event_id)
);

CREATE TABLE shipping_address(
    shipping_address_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    user_id INT UNSIGNED NOT NULL,
    address_data VARCHAR(150) NOT NULL,
    Primary Key(shipping_address_id),
    Foreign Key(user_id) REFERENCES user(user_id)
);

CREATE TABLE accepted_method(
    method_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    method_name VARCHAR(20) NOT NULL UNIQUE,
    Primary Key(method_id)
);

CREATE TABLE pay_method(
    pay_method_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    user_id INT UNSIGNED NOT NULL,
    method_id INT UNSIGNED NOT NULL,
    method_data VARCHAR(70) NOT NULL UNIQUE,
    cvv varchar(3) NULL,
    exp VARCHAR(10) NULL,
    Primary Key(pay_method_id),
    Foreign Key(user_id) REFERENCES user(user_id),
    Foreign Key(method_id) REFERENCES accepted_method(method_id)
);

CREATE TABLE product(
    product_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    name VARCHAR(45) NOT NULL,
    description VARCHAR(500) NULL DEFAULT '',
    price FLOAT NOT NULL DEFAULT 0,
    inventory INT NOT NULL DEFAULT 0,
    Primary Key(product_id)
);

CREATE TABLE orders(
    order_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    user_id INT UNSIGNED NOT NULL,
    order_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    pay_method_id INT UNSIGNED NOT NULL,
    shipping_address_id INT UNSIGNED NOT NULL,
    Primary Key(order_id),
    Foreign Key(user_id) REFERENCES user(user_id),
    Foreign Key(pay_method_id) REFERENCES pay_method(pay_method_id),
    Foreign Key(shipping_address_id) REFERENCES shipping_address(shipping_address_id)
);

CREATE TABLE order_detail(
    detail_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    order_id INT UNSIGNED NOT NULL,
    product_id INT UNSIGNED NOT NULL,
    quantity INT UNSIGNED NOT NULL,
    price DECIMAL(20,4) UNSIGNED NOT NULL DEFAULT 0,
    Primary Key(detail_id),
    Foreign Key(order_id) REFERENCES orders(order_id),
    Foreign Key(product_id) REFERENCES product(product_id)
);

SHOW TABLES;

/* INDEXES */
-- USER_NAME'S:
CREATE INDEX user_name_idx
    ON auth(user_name);
-- COUNTRY_NAME:
CREATE INDEX country_name_idx
    ON country(country_name);
-- PRODUCT_NAME:
CREATE INDEX product_name_idx
    ON product(name);
