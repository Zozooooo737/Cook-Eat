-- CREATE SCHEMA `cook-eat` ;

CREATE TABLE user_account(
   user_id INT AUTO_INCREMENT,
   first_name VARCHAR(50) NOT NULL,
   last_name VARCHAR(50) NOT NULL,
   phone_number VARCHAR(15),
   email VARCHAR(100) NOT NULL UNIQUE,
   password VARCHAR(255) NOT NULL,
   is_business BOOLEAN NOT NULL,
   PRIMARY KEY(user_id)
);

CREATE TABLE client(
   client_id INT AUTO_INCREMENT,
   client_username VARCHAR(50) NOT NULL UNIQUE,
   user_id INT NOT NULL UNIQUE,
   PRIMARY KEY(client_id),
   FOREIGN KEY(user_id) REFERENCES user_account(user_id) ON DELETE CASCADE
);

CREATE TABLE cook(
   cook_id INT AUTO_INCREMENT,
   cook_username VARCHAR(50) NOT NULL UNIQUE,
   user_id INT NOT NULL UNIQUE,
   PRIMARY KEY(cook_id),
   FOREIGN KEY(user_id) REFERENCES user_account(user_id) ON DELETE CASCADE
);

CREATE TABLE address(
   address_id INT AUTO_INCREMENT,
   street_number INT,
   street_address VARCHAR(100) NOT NULL,
   city VARCHAR(100) NOT NULL,
   postcode VARCHAR(5) NOT NULL,
   nearest_metro_station VARCHAR(50),
   user_id INT NOT NULL,
   PRIMARY KEY(address_id),
   FOREIGN KEY(user_id) REFERENCES user_account(user_id) ON DELETE CASCADE
);

CREATE TABLE dish(
   dish_id INT AUTO_INCREMENT,
   dish_name VARCHAR(100) NOT NULL,
   cook_id INT NOT NULL,
   PRIMARY KEY(dish_id),
   FOREIGN KEY(cook_id) REFERENCES cook(cook_id) ON DELETE CASCADE
);

CREATE TABLE client_transaction(
   transaction_id INT AUTO_INCREMENT,
   order_date DATETIME NOT NULL,
   amount DECIMAL(10,2) NOT NULL,
   is_delivered BOOLEAN NOT NULL,
   client_id INT NOT NULL,
   PRIMARY KEY(transaction_id),
   FOREIGN KEY(client_id) REFERENCES client(client_id) ON DELETE CASCADE
);

CREATE TABLE dish_preparation(
   preparation_id INT AUTO_INCREMENT,
   is_published BOOLEAN NOT NULL,
   preparation_date DATETIME NOT NULL,
   peremption_date DATE NOT NULL,
   price DECIMAL(15,2) NOT NULL,
   number_of_person INT NOT NULL,
   dish_id INT NOT NULL,
   PRIMARY KEY(preparation_id),
   FOREIGN KEY(dish_id) REFERENCES dish(dish_id) ON DELETE CASCADE
);

CREATE TABLE dish_review(
   review_id INT AUTO_INCREMENT,
   review_text TEXT,
   rating INT NOT NULL CHECK (rating BETWEEN 1 AND 5),
   review_date DATETIME NOT NULL,
   client_id INT NOT NULL,
   dish_id INT NOT NULL,
   PRIMARY KEY(review_id),
   FOREIGN KEY(client_id) REFERENCES client(client_id) ON DELETE CASCADE,
   FOREIGN KEY(dish_id) REFERENCES dish(dish_id) ON DELETE CASCADE
);

CREATE TABLE ingredient(
   ingredient_id INT AUTO_INCREMENT,
   name VARCHAR(50) NOT NULL,
   PRIMARY KEY(ingredient_id)
);

CREATE TABLE business(
   business_id INT AUTO_INCREMENT,
   business_name VARCHAR(50) NOT NULL,
   user_id INT NOT NULL UNIQUE,
   PRIMARY KEY(business_id),
   FOREIGN KEY(user_id) REFERENCES user_account(user_id) ON DELETE CASCADE
);

CREATE TABLE client_order(
   order_id INT AUTO_INCREMENT,
   order_date DATE NOT NULL,
   transaction_id INT NOT NULL,
   address_id INT NOT NULL,
   preparation_id INT NOT NULL UNIQUE,
   PRIMARY KEY(order_id),
   FOREIGN KEY(transaction_id) REFERENCES client_transaction(transaction_id) ON DELETE CASCADE,
   FOREIGN KEY(address_id) REFERENCES address(address_id) ON DELETE CASCADE,
   FOREIGN KEY(preparation_id) REFERENCES dish_preparation(preparation_id) ON DELETE CASCADE
);

CREATE TABLE is_made_up_of(
   dish_id INT NOT NULL,
   ingredient_id INT NOT NULL,
   PRIMARY KEY(dish_id, ingredient_id),
   FOREIGN KEY(dish_id) REFERENCES dish(dish_id) ON DELETE CASCADE,
   FOREIGN KEY(ingredient_id) REFERENCES ingredient(ingredient_id) ON DELETE CASCADE
);

CREATE TABLE is_favorite(
   client_id INT NOT NULL,
   cook_id INT NOT NULL,
   PRIMARY KEY(client_id, cook_id),
   FOREIGN KEY(client_id) REFERENCES client(client_id) ON DELETE CASCADE,
   FOREIGN KEY(cook_id) REFERENCES cook(cook_id) ON DELETE CASCADE
);

SELECT user_id FROM user_account WHERE password = 'hugo';

SELECT client_id FROM client WHERE user_id = 51;