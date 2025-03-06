-- ------------------------------------------- --
--            CREATION D'UN COMPTE             --
-- ------------------------------------------- --

-- Vérifie si l'email n'est pas utilisé par un compte utilisateur : "test@gmail.com", "jose.tucker@gmail.com", @Email
SELECT COUNT(*) FROM user_account WHERE email = @Email;

-- Ajout d'un nouveau compte utilisateur. ("Zozo", "Bot", "zo.bo12@gmail.com", "HD4UHDU4", true), (@FirstName, @LastName, @Email, @Password, @IsBusiness)
INSERT INTO user_account (first_name, last_name, email, password, is_business) 
VALUES (@FirstName, @LastName, @Email, @Password, @IsBusiness);

-- Ajout d'un business. "AU PLOT", "zo.bo12@gmail.com", @BusinessName, @Email
INSERT INTO business (business_name, user_id) 
SELECT @BusinessName, user_id 
FROM user_account 
WHERE email = @Email AND is_business = TRUE;

-- Ajout d'un client. "Naruto", @ClientUsername
INSERT INTO client (client_username, user_id)
SELECT @ClientUsername, user_id
FROM user_account
WHERE email = @Email;

-- Ajout d'un cuisinier. "Ron", @CookUsername
INSERT INTO cook (cook_username, user_id)
SELECT @CookUsername, user_id
FROM user_account
WHERE email = @Email;

-- Supprimer un compte utilisateur. "zo.bo12@gmail.com", @Email
DELETE FROM user_account WHERE email = @Email;


SELECT * FROM user_account;