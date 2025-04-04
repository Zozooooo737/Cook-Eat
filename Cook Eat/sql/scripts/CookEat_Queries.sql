-- Récupérer tous les noms des utilisateurs avec leurs coordonnées ou avec leurs identifiants (Table "user Account").

SELECT * FROM client_transaction;
SELECT first_name, last_name FROM user_account ORDER BY last_name ASC;
SELECT * FROM user_account WHERE is_business = 1;
SELECT first_name, last_name, email, phone_number FROM user_account ORDER BY first_name ASC;
SELECT first_name, last_name, email, phone_number FROM user_account WHERE is_business = 1;
SELECT first_name, last_name, email, password FROM user_account;

-- Récupérer toutes les infos de certains clients en fonction de leurs ID (Table "client").

SELECT * FROM client;
SELECT client_username FROM client;
SELECT user_id, client_id FROM client;
SELECT * FROM client WHERE (client_id > 4 AND client_id < 11);

-- Récupérer toutes les infos de certains cuisinier en fonction de leurs ID (Table "cook").

SELECT * FROM cook;
SELECT cook_username FROM cook;
SELECT user_id, cook_id FROM cook;
SELECT * FROM client WHERE (client_id > 14 AND client_id < 21);
SELECT * FROM dish;

-- Permet de récupérer les noms de tous les plats enregistrés du cuisinier (Table "dish").

SELECT dish_name FROM dish WHERE cook_id=5 ORDER BY dish_name ASC;

-- Permet d'avoir toutes les infos importantes de toutes les préparations d'un type de plat (Table "dish_preparation").

SELECT preparation_date, Peremption_Date, price, number_of_person FROM dish_preparation WHERE dish_id = 2;

-- Permet de récupérer la date et l'adresse de toutes les lignes de commande d'une transaction (Table "client_order").

SELECT transaction_id, Order_Date FROM client_order WHERE Transaction_ID = 4;

-- Permet de récupérer les infos principales d'une transaction spécifique (Table "client_transaction").

SELECT Amount, Order_Date FROM client_transaction WHERE Transaction_ID = 4;

-- Permet de récupérer la date et la station de métro la plus proche de l'adresse de toutes les lignes de commande de toutes les transactions effectuées par un client spécifique (Table "client_order").

SELECT a.nearest_metro_station AS nearest_station, co.Order_Date FROM client_order co JOIN client_transaction ct ON co.Transaction_ID = ct.Transaction_ID
JOIN address a ON co.address_id = a.address_id WHERE ct.client_id = 5;

-- Permet de récupérer les infos principales de toutes les préparations effectuées par un cuisinier sans tenir compte du plat de référence (Table "dish_preparation").

SELECT preparation_id, dish_name, price, number_of_person FROM dish_preparation JOIN dish ON dish_preparation.dish_id = dish.dish_id WHERE cook_id = 8;

-- Permet de récupérer toutes les adresses enregistrées sur le compte utilisateur d'un client (Table "Address").

SELECT street_address, city, postcode FROM address JOIN user_account ON address.user_id = user_account.user_id JOIN client ON user_account.user_id = client.user_id WHERE client_id = 1;


SELECT * FROM business;