/* QUERIES: */

-- GET country_id WITH THE country_name:
SELECT country_id
FROM country
USE INDEX(country_name_idx)
WHERE country_name = 'United States';

-- GET USER ID WITH THE USERNAME:
SELECT user_id
FROM auth
USE index(user_name_idx)
WHERE user_name = 'afteptelle';

-- ALL USERS INFORMATION:
SELECT user.user_id, CONCAT_WS(' ', first_name, last_name) AS name, user.create_time, gender.gender_name, user.birth_date,
       user.phone, country.country_name, user.billing_address
FROM user
INNER JOIN gender
    ON user.gender_id = gender.gender_id
INNER JOIN country
    USE INDEX(country_name_idx)
    ON user.country_id = country.country_id;

-- ALL USERS WITH A 91... PHONE NUMBER:
SELECT user.user_id, CONCAT_WS(' ', first_name, last_name) AS name, user.create_time, gender.gender_name, user.birth_date,
       user.phone, country.country_name, user.billing_address
FROM user
INNER JOIN gender
    ON user.gender_id = gender.gender_id
INNER JOIN country
    USE INDEX(country_name_idx)
    ON user.country_id = country.country_id
WHERE user.phone LIKE ('91%');

-- ALL USERS WITH A 96... PHONE NUMBER:
SELECT user.user_id, CONCAT_WS(' ', first_name, last_name) AS name, user.create_time, gender.gender_name, user.birth_date,
       user.phone, country.country_name, user.billing_address
FROM user
INNER JOIN gender
    ON user.gender_id = gender.gender_id
INNER JOIN country
    USE INDEX(country_name_idx)
    ON user.country_id = country.country_id
WHERE user.phone LIKE ('96%');

-- ALL INFORMATION OF USERS FROM PORTUGAL:
SELECT user.user_id, CONCAT_WS(' ', first_name, last_name) AS name, user.create_time, gender.gender_name, user.birth_date,
       user.phone, country.country_name, user.billing_address
FROM user
INNER JOIN gender
    ON user.gender_id = gender.gender_id
INNER JOIN country
    USE INDEX(country_name_idx)
    ON user.country_id = 177 AND country.country_id = 177;

-- ALL USERS FROM UNITED STATES:
SELECT user.user_id, CONCAT_WS(' ', first_name, last_name) AS name, user.create_time, gender.gender_name, user.birth_date,
       user.phone, country.country_name, user.billing_address
FROM user
INNER JOIN gender
    ON user.gender_id = gender.gender_id
INNER JOIN country
    USE INDEX(country_name_idx)
    ON user.country_id = 230 AND country.country_id = 230;

-- ALL USER INFORMATION OF A SPECIFIC USER_ID:
SELECT user.user_id, CONCAT_WS(' ', first_name, last_name) AS name, user.create_time, gender.gender_name, user.birth_date,
       user.phone, country.country_name, user.billing_address
FROM user
INNER JOIN gender
    ON user.gender_id = gender.gender_id
INNER JOIN country
    USE INDEX(country_name_idx)
    ON user.country_id = country.country_id
WHERE user.user_id = 2;

-- ALL auth INFORMATION:
SELECT user_id, auth.user_email, auth.user_name, auth.user_pass
FROM auth
USE INDEX(user_name_idx);

-- AUTH INFORMTION FROM A SPECIFIC USER_NAME:
SELECT user_id, auth.user_email, auth.user_name, auth.user_pass
FROM auth
USE INDEX(user_name_idx)
WHERE auth.user_name = 'marthaR';

-- ALL USERNAMES AND RESPECTIVE EMAILS:
SELECT auth.user_name, auth.user_email
FROM auth
USE INDEX(user_name_idx);

-- USERNAME + PASSWORD FROM A SPECIFIC USER_NAME:
SELECT auth.user_name, auth.user_pass
FROM auth
USE INDEX(user_name_idx)
WHERE auth.user_name = 'marthaR';

-- USERNAME + PASSWORD FROM A SPECIFIC USER_ID:
SELECT auth.user_name, auth.user_pass
FROM auth
USE INDEX(user_name_idx)
WHERE auth.user_id = 2;

-- ALL IP'S USED BY A SPECIFIC USER_NAME:
SELECT DISTINCT auth.user_name, logs.user_ip, logs.log_timestamp AS timestamp
FROM logs
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON logs.user_id = auth.user_id
WHERE auth.user_name = 'marthaR';

-- ALL IP'S USED BY A SPECIFIC USER_ID:
SELECT DISTINCT auth.user_name, (logs.user_ip)
FROM logs
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON logs.user_id = 2 AND auth.user_id = 2;

-- ALL USER_NAME'S AND USER_ID'S OF MALE USERS:
SELECT user.user_id, auth.user_name
FROM user
INNER JOIN gender
    ON user.gender_id = 1 AND gender.gender_id = 1
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON user.user_id = auth.user_id;

-- ALL FEMALE USERS:
SELECT user.user_id, auth.user_name
FROM user
INNER JOIN gender
    ON user.gender_id = 2 AND gender.gender_id = 2
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON user.user_id = auth.user_id;

-- ALL USERS YONGER THAN 60:
SELECT user_id, CONCAT_WS(' ', first_name, last_name), phone, billing_address
FROM user
WHERE YEAR(CURDATE()) - YEAR(birth_date) < 60 AND user_id != 1;

-- ALL USERS OLDER THAN 70:
SELECT user_id, CONCAT_WS(' ', first_name, last_name), phone, billing_address
FROM user
WHERE YEAR(CURDATE()) - YEAR(birth_date) > 70;


-- ALL PRODUCTS:
SELECT *
FROM product
USE INDEX(product_name_idx);

-- PRICE FROM A SPECIFIC PRODUCT_ID:
SELECT product.product_id, product.name, product.price
FROM product
USE INDEX(product_name_idx)
WHERE product.product_id = 7;

-- INVENTORY OF A SPECIFIC PRODUCT:
SELECT product.product_id, product.name, product.inventory
FROM product
USE INDEX(product_name_idx)
WHERE product.product_id = 7;

--

/* SIMULATING ORDERS: */
-- This part is unnecessary, it was only made for the purposes of this university project.

-- 1:
    -- Insert the order:
INSERT INTO orders VALUES
    (1, 4, CURRENT_TIMESTAMP, 4, 5);

    -- Insert the details of that order:
INSERT INTO order_detail VALUES
    (1, 1, 7, 29999, 0),
    (2, 1, 4, 1, 0);

    -- Update price of the details:
UPDATE order_detail
INNER JOIN product
    ON order_detail.product_id = product.product_id
SET order_detail.price = (order_detail.quantity * product.price)
WHERE order_detail.detail_id = 1;

UPDATE order_detail
INNER JOIN product
    ON order_detail.product_id = product.product_id
SET order_detail.price = (order_detail.quantity * product.price)
WHERE order_detail.detail_id = 2;

    -- Update inventory of the products that where bought:
UPDATE product
    SET inventory = inventory - 29999
WHERE product_id = 7;

UPDATE product
    SET inventory = inventory - 1
WHERE product_id = 4;

-- 2:
    -- Insert the order:
INSERT INTO orders VALUES
    (2, 6, CURRENT_TIMESTAMP, 5, 7);

    -- Insert the details of that order:
INSERT INTO order_detail VALUES
    (3, 2, 3, 1, 0),
    (4, 2, 7, 1, 0);

    -- Update price of the details:
UPDATE order_detail
INNER JOIN product
    ON order_detail.product_id = product.product_id
SET order_detail.price = (order_detail.quantity * product.price)
WHERE order_detail.detail_id = 3;

UPDATE order_detail
INNER JOIN product
    ON order_detail.product_id = product.product_id
SET order_detail.price = (order_detail.quantity * product.price)
WHERE order_detail.detail_id = 4;

    -- Update inventory of the products that where bought:
UPDATE product
    SET inventory = inventory - 1
WHERE product_id = 3;

UPDATE product
    SET inventory = inventory - 1
WHERE product_id = 7;

-- 3:
    -- Insert the order:
INSERT INTO orders VALUES
    (3, 4, TIMESTAMPADD(DAY, 7, CURRENT_TIMESTAMP), 6, 5);

    -- Insert the details of that order:
INSERT INTO order_detail VALUES
    (5, 3, 1, 2, 0);

    -- Update price of the details:
UPDATE order_detail
INNER JOIN product
    ON order_detail.product_id = product.product_id
SET order_detail.price = (order_detail.quantity * product.price)
WHERE order_detail.detail_id = 5;

    -- Update inventory of the products that where bought:
UPDATE product
    SET inventory = inventory - 2
WHERE product_id = 1;

--

-- ALL ORDERS:
SELECT orders.order_id, orders.user_id, orders.order_date, orders.pay_method_id, shipping_address.address_data, SUM(order_detail.price) AS total_price
FROM orders
INNER JOIN shipping_address
    ON orders.shipping_address_id = shipping_address.shipping_address_id
INNER JOIN order_detail
    ON orders.order_id = order_detail.order_id
GROUP BY order_detail.order_id;

-- ORDER DETAILS FROM A SPECIFIC ORDER:
SELECT order_detail.detail_id, order_detail.order_id, auth.user_name, order_detail.product_id, product.name AS product_name, order_detail.quantity, order_detail.price
FROM order_detail
INNER JOIN orders
    ON order_detail.order_id = 1 AND orders.order_id = 1
INNER JOIN auth
    ON orders.user_id = auth.user_id
INNER JOIN product
    ON order_detail.product_id = product.product_id;

-- ALL ORDERS FROM A SPECIFIC USER_ID:
SELECT orders.order_id, orders.user_id, auth.user_name, orders.order_date, orders.pay_method_id, shipping_address.address_data, SUM(order_detail.price) AS total_price
FROM orders
INNER JOIN auth
    ON orders.user_id = 4 AND auth.user_id = 4
INNER JOIN shipping_address
    ON orders.shipping_address_id = shipping_address.shipping_address_id
INNER JOIN order_detail
    ON orders.order_id = order_detail.order_id
GROUP BY order_detail.order_id;

-- ALL ORDERS FROM A SPECIFIC USER_NAME:
SELECT orders.order_id, orders.user_id, auth.user_name, orders.order_date, orders.pay_method_id, shipping_address.address_data, SUM(order_detail.price) AS total_price
FROM orders
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON orders.user_id = auth.user_id
INNER JOIN shipping_address
    ON orders.shipping_address_id = shipping_address.shipping_address_id
INNER JOIN order_detail
    ON orders.order_id = order_detail.order_id
WHERE auth.user_name = 'ursinho1957'
GROUP BY order_detail.order_id;

-- ALL PAYMENT METHODS REGISTRATIONS:
SELECT pay_method.pay_method_id, pay_method.user_id, auth.user_name, accepted_method.method_name, pay_method.method_data, pay_method.cvv, pay_method.exp
FROM pay_method
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON pay_method.user_id = auth.user_id
INNER JOIN accepted_method
    ON pay_method.method_id = accepted_method.method_id
ORDER BY user_id;

-- ALL PAYMENT METHODS REGISTRATIONS USED BY A SPECIFIC USER:
SELECT pay_method.pay_method_id, pay_method.user_id, auth.user_name, accepted_method.method_name, pay_method.method_data, pay_method.cvv, pay_method.exp
FROM pay_method
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON pay_method.user_id = 4 AND auth.user_id = 4
INNER JOIN accepted_method
    ON pay_method.method_id = accepted_method.method_id;

-- ALL SHIPPING ADDRESSES REGISTRATIONS:
SELECT shipping_address.shipping_address_id, shipping_address.user_id, auth.user_name, shipping_address.address_data
FROM shipping_address
INNER JOIN auth
    USE INDEX(user_name_idx)
    ON shipping_address.user_id = auth.user_id;

-- ALL SHIPPING METHODS USED BY A SPECIFIC USER:
SELECT shipping_address.user_id, auth.user_name, shipping_address.shipping_address_id, shipping_address.address_data
FROM shipping_address
INNER JOIN auth
    ON shipping_address.user_id = 5 AND auth.user_id = 5;

--

/* Stored Procedures: */
DELIMITER $$

CREATE PROCEDURE max_age()
BEGIN
	SELECT max(YEAR(CURDATE()) - YEAR(birth_date))
    FROM user
    WHERE user_id != 1;
END $$

DELIMITER ;

CALL max_age();

--

DELIMITER $$

CREATE PROCEDURE total_money_made()
BEGIN
	SELECT SUM(price)
    FROM order_detail;
END $$

DELIMITER ;

CALL total_money_made();

--

DELIMITER $$

CREATE PROCEDURE total_users()
BEGIN
    SELECT COUNT(*)
    FROM user
    WHERE user_id != 1;
END $$

DELIMITER ;

CALL total_users();

--

/* Function: */
DELIMITER $$

CREATE FUNCTION who_is (username VARCHAR(50))
-- DETERMINISTIC because the same input will always return the same result.
RETURNS VARCHAR(500) DETERMINISTIC
BEGIN
    DECLARE first_name VARCHAR(50);
    DECLARE last_name VARCHAR(50);
    DECLARE user_id TEXT;
    DECLARE user_gender VARCHAR(30);
    DECLARE user_country VARCHAR(150);
    DECLARE user_birth CHAR(10);
    DECLARE user_email VARCHAR(70);

    SET first_name = (
        SELECT user.first_name
        FROM auth
        USE INDEX(user_name_idx)
        INNER JOIN user
            ON user.user_id = auth.user_id
        WHERE auth.user_name = username
    );

    SET last_name = (
        SELECT user.last_name
        FROM auth
        USE INDEX(user_name_idx)
        INNER JOIN user
            ON user.user_id = auth.user_id
        WHERE auth.user_name = username
    );

    SET user_id = (
        SELECT CAST(auth.user_id AS CHAR)
        FROM auth
        USE INDEX(user_name_idx)
        WHERE auth.user_name = username
    );

    SET user_gender = (
        SELECT gender.gender_name
        FROM gender
        INNER JOIN user
            ON user.gender_id = gender.gender_id
        INNER JOIN auth
            USE INDEX(user_name_idx)
            ON user.user_id = auth.user_id
        WHERE auth.user_name = username
    );

    SET user_country = (
        SELECT country.country_name
        FROM country
        USE INDEX(country_name_idx)
        INNER JOIN user
            ON user.country_id = country.country_id
        INNER JOIN auth
            USE INDEX(user_name_idx)
            ON user.user_id = auth.user_id
        WHERE auth.user_name = username
    );

    SET user_birth = (
        SELECT CAST(user.birth_date AS CHAR(10))
        FROM user
        INNER JOIN auth
            USE INDEX(user_name_idx)
            ON user.user_id = auth.user_id
        WHERE auth.user_name = username
    );

    RETURN CONCAT(first_name, ' ', last_name, ' ID is the nº ', user_id, '. He/She is a ', user_gender, ', from ', user_country, ', and was born in ', user_birth, '.');

END$$

delimiter ;

SELECT who_is('ursinho1957');
SELECT who_is('salgaduXXX');
SELECT who_is('marthaR');
--
