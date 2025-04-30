-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cook-eat
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `address` (
  `address_id` int NOT NULL AUTO_INCREMENT,
  `street_number` int DEFAULT NULL,
  `street_address` varchar(100) NOT NULL,
  `city` varchar(100) NOT NULL,
  `postcode` varchar(5) NOT NULL,
  `nearest_metro_station` varchar(50) DEFAULT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`address_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `address_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user_account` (`user_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (1,12,'Rue de la Paix','Paris','75001','Concorde',1),(2,45,'Avenue des Champs-Élysées','Paris','75008','Champs-Élysées - Clemenceau',2),(3,78,'Boulevard Saint-Germain','Paris','75006','Saint-Germain-des-Prés',3),(4,23,'Rue de Rivoli','Paris','75004','Louvre - Rivoli',4),(5,56,'Rue du Faubourg Saint-Honoré','Paris','75008','Madeleine',5),(6,89,'Rue de Vaugirard','Paris','75015','Vaugirard',6),(7,34,'Rue de Rennes','Paris','75006','Rennes',7),(8,67,'Avenue de la République','Paris','75011','République',8),(9,90,'Rue de la Roquette','Paris','75011','Bastille',9),(10,11,'Rue de Charonne','Paris','75011','Charonne',10),(11,22,'Rue de la Pompe','Paris','75016','La Muette',1),(12,33,'Rue de Passy','Paris','75016','Passy',2),(13,44,'Rue de la Faisanderie','Paris','75016','Porte Dauphine',3),(14,55,'Rue de la Tour','Paris','75016','Trocadéro',4),(15,66,'Rue de l\'Université','Paris','75007','Invalides',5),(16,77,'Rue de Grenelle','Paris','75007','Rue du Bac',6),(17,88,'Rue de Sèvres','Paris','75007','Duroc',7),(18,99,'Rue de Babylone','Paris','75007','Sèvres - Babylone',8),(19,10,'Rue de Varenne','Paris','75007','Varenne',9),(20,20,'Rue de Montalembert','Paris','75007','Rue du Bac',10);
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `business`
--

DROP TABLE IF EXISTS `business`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `business` (
  `business_id` int NOT NULL AUTO_INCREMENT,
  `business_name` varchar(50) NOT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`business_id`),
  UNIQUE KEY `user_id` (`user_id`),
  CONSTRAINT `business_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user_account` (`user_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `business`
--

LOCK TABLES `business` WRITE;
/*!40000 ALTER TABLE `business` DISABLE KEYS */;
INSERT INTO `business` VALUES (1,'Chez Cassandra',1),(2,'Melissa\'s Kitchen',2),(3,'Erika\'s Bistro',3),(4,'Amy\'s Sushi',4),(5,'Anthony\'s Pizzeria',5),(6,'Russell\'s Salad Bar',6),(7,'Thomas\' Seafood',7),(8,'Jonathan\'s Veggie Delight',8),(9,'Jose\'s Desserts',9),(10,'Laura\'s Fine Dining',10);
/*!40000 ALTER TABLE `business` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `client_id` int NOT NULL AUTO_INCREMENT,
  `client_username` varchar(50) NOT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`client_id`),
  UNIQUE KEY `client_username` (`client_username`),
  UNIQUE KEY `user_id` (`user_id`),
  CONSTRAINT `client_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user_account` (`user_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (1,'cassandra.benson',1),(2,'melissa.webb',2),(3,'erika.barnett',3),(4,'amy.alvarez',4),(5,'anthony.tucker',5),(6,'russell.hill',6),(7,'thomas.jones',7),(8,'jonathan.cochran',8),(9,'jose.tucker',9),(10,'laura.gallagher',10),(11,'robert.gomez',11),(12,'kristopher.fisher',12),(13,'rachel.hines',13),(14,'hector.jones',14),(15,'shelley.melton',15),(16,'katelyn.thornton',16),(17,'brooke.martinez',17),(18,'amy.salas',18),(19,'andrew.rodriguez',19),(20,'bethany.wiggins',20),(21,'karen.harris',21),(22,'zachary.herring',22),(23,'daniel.kelly',23),(24,'brian.jacobs',24),(25,'jennifer.rivers',25),(26,'melinda.ellis',26),(27,'ryan.mathews',27),(28,'michael.vega',28),(29,'bryce.reynolds',29),(30,'todd.phillips',30),(31,'sara.manning',31),(32,'jennifer.boone',32),(33,'jonathan.hanson',33),(34,'david.smith',34),(35,'shawn.young',35);
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client_order`
--

DROP TABLE IF EXISTS `client_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client_order` (
  `order_id` int NOT NULL AUTO_INCREMENT,
  `order_date` date NOT NULL,
  `transaction_id` int NOT NULL,
  `address_id` int NOT NULL,
  `preparation_id` int NOT NULL,
  PRIMARY KEY (`order_id`),
  UNIQUE KEY `preparation_id` (`preparation_id`),
  KEY `transaction_id` (`transaction_id`),
  KEY `address_id` (`address_id`),
  CONSTRAINT `client_order_ibfk_1` FOREIGN KEY (`transaction_id`) REFERENCES `client_transaction` (`transaction_id`) ON DELETE CASCADE,
  CONSTRAINT `client_order_ibfk_2` FOREIGN KEY (`address_id`) REFERENCES `address` (`address_id`) ON DELETE CASCADE,
  CONSTRAINT `client_order_ibfk_3` FOREIGN KEY (`preparation_id`) REFERENCES `dish_preparation` (`preparation_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client_order`
--

LOCK TABLES `client_order` WRITE;
/*!40000 ALTER TABLE `client_order` DISABLE KEYS */;
INSERT INTO `client_order` VALUES (1,'2023-10-02',8,1,1),(2,'2023-10-03',2,2,2),(3,'2023-10-04',3,3,3),(4,'2023-10-05',4,4,4),(5,'2023-10-06',5,5,5),(6,'2023-10-07',6,6,6),(7,'2023-10-08',7,7,7),(8,'2023-10-09',8,8,8),(9,'2023-10-10',9,9,9),(10,'2023-10-11',10,10,10),(11,'2023-10-12',11,1,11),(12,'2023-10-13',12,2,12),(13,'2023-10-14',13,3,13),(14,'2023-10-15',14,4,14),(15,'2023-10-16',15,5,15);
/*!40000 ALTER TABLE `client_order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client_transaction`
--

DROP TABLE IF EXISTS `client_transaction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client_transaction` (
  `transaction_id` int NOT NULL AUTO_INCREMENT,
  `order_date` datetime NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `is_delivered` tinyint(1) NOT NULL,
  `client_id` int NOT NULL,
  PRIMARY KEY (`transaction_id`),
  KEY `client_id` (`client_id`),
  CONSTRAINT `client_transaction_ibfk_1` FOREIGN KEY (`client_id`) REFERENCES `client` (`client_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client_transaction`
--

LOCK TABLES `client_transaction` WRITE;
/*!40000 ALTER TABLE `client_transaction` DISABLE KEYS */;
INSERT INTO `client_transaction` VALUES (1,'2023-10-02 00:00:00',15.99,1,1),(2,'2023-10-03 00:00:00',18.50,1,2),(3,'2023-10-04 00:00:00',22.75,0,3),(4,'2023-10-05 00:00:00',25.00,1,4),(5,'2023-10-06 00:00:00',12.99,0,5),(6,'2023-10-07 00:00:00',9.99,1,6),(7,'2023-10-08 00:00:00',30.00,0,7),(8,'2023-10-09 00:00:00',14.50,1,8),(9,'2023-10-10 00:00:00',8.99,0,9),(10,'2023-10-11 00:00:00',10.99,1,10),(11,'2023-10-12 00:00:00',16.50,1,1),(12,'2023-10-13 00:00:00',19.99,0,2),(13,'2023-10-14 00:00:00',24.00,1,3),(14,'2023-10-15 00:00:00',27.50,0,4),(15,'2023-10-16 00:00:00',13.99,1,5);
/*!40000 ALTER TABLE `client_transaction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cook`
--

DROP TABLE IF EXISTS `cook`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cook` (
  `cook_id` int NOT NULL AUTO_INCREMENT,
  `cook_username` varchar(50) NOT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`cook_id`),
  UNIQUE KEY `cook_username` (`cook_username`),
  UNIQUE KEY `user_id` (`user_id`),
  CONSTRAINT `cook_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user_account` (`user_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cook`
--

LOCK TABLES `cook` WRITE;
/*!40000 ALTER TABLE `cook` DISABLE KEYS */;
INSERT INTO `cook` VALUES (1,'cassandra.benson',1),(2,'melissa.webb',2),(3,'erika.barnett',3),(4,'amy.alvarez',4),(5,'anthony.tucker',5),(6,'russell.hill',6),(7,'thomas.jones',7),(8,'jonathan.cochran',8),(9,'jose.tucker',9),(10,'laura.gallagher',10),(11,'robert.gomez',11),(12,'kristopher.fisher',12),(13,'rachel.hines',13),(14,'hector.jones',14),(15,'shelley.melton',15),(16,'katelyn.thornton',16),(17,'brooke.martinez',17),(18,'amy.salas',18),(19,'andrew.rodriguez',19),(20,'bethany.wiggins',20),(21,'karen.harris',21),(22,'zachary.herring',22),(23,'daniel.kelly',23),(24,'brian.jacobs',24),(25,'jennifer.rivers',25),(26,'melinda.ellis',26),(27,'ryan.mathews',27),(28,'michael.vega',28),(29,'bryce.reynolds',29),(30,'todd.phillips',30),(31,'sara.manning',31),(32,'jennifer.boone',32),(33,'jonathan.hanson',33),(34,'david.smith',34),(35,'shawn.young',35),(36,'matthew.perry',36),(37,'jennifer.orozco',37),(38,'joshua.mcclure',38),(39,'nicholas.cantu',39),(40,'scott.robinson',40),(41,'jason.mitchell',41),(42,'eric.elliott',42),(43,'david.bryan',43),(44,'amanda.nelson',44),(45,'joseph.fitzgerald',45),(46,'joseph.bean',46),(47,'justin.nguyen',47),(48,'lisa.coleman',48),(49,'sara.mooney',49),(50,'jessica.oneill',50);
/*!40000 ALTER TABLE `cook` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dish`
--

DROP TABLE IF EXISTS `dish`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dish` (
  `dish_id` int NOT NULL AUTO_INCREMENT,
  `dish_name` varchar(100) NOT NULL,
  `cook_id` int NOT NULL,
  PRIMARY KEY (`dish_id`),
  KEY `cook_id` (`cook_id`),
  CONSTRAINT `dish_ibfk_1` FOREIGN KEY (`cook_id`) REFERENCES `cook` (`cook_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dish`
--

LOCK TABLES `dish` WRITE;
/*!40000 ALTER TABLE `dish` DISABLE KEYS */;
INSERT INTO `dish` VALUES (1,'Spaghetti Bolognese',1),(2,'Chicken Tikka Masala',2),(3,'Beef Bourguignon',3),(4,'Sushi Platter',4),(5,'Margherita Pizza',5),(6,'Caesar Salad',6),(7,'Lobster Bisque',7),(8,'Vegetable Stir Fry',8),(9,'Chocolate Mousse',9),(10,'Crème Brûlée',10),(11,'Pasta Carbonara',1),(12,'Butter Chicken',2),(13,'Coq au Vin',3),(14,'Tempura',4),(15,'Pepperoni Pizza',5),(16,'Grilled Salmon',6),(17,'Tomato Soup',7),(18,'Fried Rice',8),(19,'Tiramisu',9),(20,'Cheesecake',10);
/*!40000 ALTER TABLE `dish` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dish_preparation`
--

DROP TABLE IF EXISTS `dish_preparation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dish_preparation` (
  `preparation_id` int NOT NULL AUTO_INCREMENT,
  `is_published` tinyint(1) NOT NULL,
  `preparation_date` datetime NOT NULL,
  `peremption_date` date NOT NULL,
  `price` decimal(15,2) NOT NULL,
  `number_of_person` int NOT NULL,
  `dish_id` int NOT NULL,
  PRIMARY KEY (`preparation_id`),
  KEY `dish_id` (`dish_id`),
  CONSTRAINT `dish_preparation_ibfk_1` FOREIGN KEY (`dish_id`) REFERENCES `dish` (`dish_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dish_preparation`
--

LOCK TABLES `dish_preparation` WRITE;
/*!40000 ALTER TABLE `dish_preparation` DISABLE KEYS */;
INSERT INTO `dish_preparation` VALUES (1,1,'2023-10-01 12:00:00','2023-10-03',15.99,2,1),(2,1,'2023-10-02 14:00:00','2023-10-04',18.50,3,2),(3,0,'2023-10-03 16:00:00','2023-10-05',22.75,4,3),(4,1,'2023-10-04 18:00:00','2023-10-06',25.00,2,4),(5,0,'2023-10-05 20:00:00','2023-10-07',12.99,1,5),(6,1,'2023-10-06 10:00:00','2023-10-08',9.99,2,6),(7,0,'2023-10-07 12:00:00','2023-10-09',30.00,3,7),(8,1,'2023-10-08 14:00:00','2023-10-10',14.50,4,8),(9,0,'2023-10-09 16:00:00','2023-10-11',8.99,2,9),(10,1,'2023-10-10 18:00:00','2023-10-12',10.99,1,10),(11,1,'2023-10-11 12:00:00','2023-10-13',16.50,2,11),(12,0,'2023-10-12 14:00:00','2023-10-14',19.99,3,12),(13,1,'2023-10-13 16:00:00','2023-10-15',24.00,4,13),(14,0,'2023-10-14 18:00:00','2023-10-16',27.50,2,14),(15,1,'2023-10-15 20:00:00','2023-10-17',13.99,1,15),(16,0,'2023-10-11 12:00:00','2023-10-13',16.50,2,11),(17,0,'2023-10-11 12:00:00','2023-10-13',16.50,2,11);
/*!40000 ALTER TABLE `dish_preparation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dish_review`
--

DROP TABLE IF EXISTS `dish_review`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dish_review` (
  `review_id` int NOT NULL AUTO_INCREMENT,
  `review_text` text,
  `rating` int NOT NULL,
  `review_date` datetime NOT NULL,
  `client_id` int NOT NULL,
  `dish_id` int NOT NULL,
  PRIMARY KEY (`review_id`),
  KEY `client_id` (`client_id`),
  KEY `dish_id` (`dish_id`),
  CONSTRAINT `dish_review_ibfk_1` FOREIGN KEY (`client_id`) REFERENCES `client` (`client_id`) ON DELETE CASCADE,
  CONSTRAINT `dish_review_ibfk_2` FOREIGN KEY (`dish_id`) REFERENCES `dish` (`dish_id`) ON DELETE CASCADE,
  CONSTRAINT `dish_review_chk_1` CHECK ((`rating` between 1 and 5))
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dish_review`
--

LOCK TABLES `dish_review` WRITE;
/*!40000 ALTER TABLE `dish_review` DISABLE KEYS */;
INSERT INTO `dish_review` VALUES (1,'Delicious and well-prepared!',5,'2023-10-02 13:00:00',1,1),(2,'A bit too spicy for my taste.',3,'2023-10-03 15:00:00',2,2),(3,'Perfectly cooked and flavorful.',5,'2023-10-04 17:00:00',3,3),(4,'Fresh and tasty sushi!',4,'2023-10-05 19:00:00',4,4),(5,'The pizza was a bit undercooked.',2,'2023-10-06 21:00:00',5,5),(6,'Great salad, very fresh.',4,'2023-10-07 11:00:00',6,6),(7,'The bisque was too salty.',3,'2023-10-08 13:00:00',7,7),(8,'Excellent stir fry, very healthy.',5,'2023-10-09 15:00:00',8,8),(9,'The mousse was too sweet.',2,'2023-10-10 17:00:00',9,9),(10,'Perfect crème brûlée!',5,'2023-10-11 19:00:00',10,10);
/*!40000 ALTER TABLE `dish_review` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ingredient`
--

DROP TABLE IF EXISTS `ingredient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredient` (
  `ingredient_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`ingredient_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ingredient`
--

LOCK TABLES `ingredient` WRITE;
/*!40000 ALTER TABLE `ingredient` DISABLE KEYS */;
INSERT INTO `ingredient` VALUES (1,'Spaghetti\r'),(2,'Tomato Sauce\r'),(3,'Ground Beef\r'),(4,'Chicken Breast\r'),(5,'Tikka Masala Sauce\r'),(6,'Beef\r'),(7,'Red Wine\r'),(8,'Sushi Rice\r'),(9,'Salmon\r'),(10,'Pizza Dough\r');
/*!40000 ALTER TABLE `ingredient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `is_favorite`
--

DROP TABLE IF EXISTS `is_favorite`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `is_favorite` (
  `client_id` int NOT NULL,
  `cook_id` int NOT NULL,
  PRIMARY KEY (`client_id`,`cook_id`),
  KEY `cook_id` (`cook_id`),
  CONSTRAINT `is_favorite_ibfk_1` FOREIGN KEY (`client_id`) REFERENCES `client` (`client_id`) ON DELETE CASCADE,
  CONSTRAINT `is_favorite_ibfk_2` FOREIGN KEY (`cook_id`) REFERENCES `cook` (`cook_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `is_favorite`
--

LOCK TABLES `is_favorite` WRITE;
/*!40000 ALTER TABLE `is_favorite` DISABLE KEYS */;
INSERT INTO `is_favorite` VALUES (10,1),(1,2),(2,3),(3,4),(4,5),(5,6),(6,7),(7,8),(8,9),(9,10);
/*!40000 ALTER TABLE `is_favorite` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `is_made_up_of`
--

DROP TABLE IF EXISTS `is_made_up_of`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `is_made_up_of` (
  `dish_id` int NOT NULL,
  `ingredient_id` int NOT NULL,
  PRIMARY KEY (`dish_id`,`ingredient_id`),
  KEY `ingredient_id` (`ingredient_id`),
  CONSTRAINT `is_made_up_of_ibfk_1` FOREIGN KEY (`dish_id`) REFERENCES `dish` (`dish_id`) ON DELETE CASCADE,
  CONSTRAINT `is_made_up_of_ibfk_2` FOREIGN KEY (`ingredient_id`) REFERENCES `ingredient` (`ingredient_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `is_made_up_of`
--

LOCK TABLES `is_made_up_of` WRITE;
/*!40000 ALTER TABLE `is_made_up_of` DISABLE KEYS */;
INSERT INTO `is_made_up_of` VALUES (1,1),(6,1),(1,2),(6,2),(1,3),(7,3),(2,4),(7,4),(2,5),(8,5),(3,6),(8,6),(3,7),(9,7),(4,8),(9,8),(4,9),(10,9),(5,10),(10,10);
/*!40000 ALTER TABLE `is_made_up_of` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_account`
--

DROP TABLE IF EXISTS `user_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_account` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `phone_number` varchar(15) DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  `is_business` tinyint(1) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_account`
--

LOCK TABLES `user_account` WRITE;
/*!40000 ALTER TABLE `user_account` DISABLE KEYS */;
INSERT INTO `user_account` VALUES (1,'Cassandra','Benson','1573101560','cassandra.benson@yahoo.com','F#gMsyu7*O31',0),(2,'Melissa','Webb','9591195775','melissa.webb@hotmail.com','&7Iv5mLWdlgt',0),(3,'Erika','Barnett','562.748.9787x77','erika.barnett@hotmail.com','3PfKQHGkI+2G',0),(4,'Amy','Alvarez','718-900-6293','amy.alvarez@yahoo.com','i1yDx4zm^)8D',1),(5,'Anthony','Tucker','600-486-8932','anthony.tucker@gmail.com','p!xkXNju1r7%',0),(6,'Russell','Hill','-10169','russell.hill@gmail.com','TqiBQXz5H#Y9',1),(7,'Thomas','Jones','796-631-5511','thomas.jones@yahoo.com','AKT9bMtQP(Kn',0),(8,'Jonathan','Cochran','-5568','jonathan.cochran@gmail.com','I2@KmT(Z783^',1),(9,'Jose','Tucker','(212)766-0874','jose.tucker@gmail.com','XpyF7QIv4#qD',1),(10,'Laura','Gallagher','(236)353-9010x7','laura.gallagher@yahoo.com','3n$s0XpcQjJ9',0),(11,'Robert','Gomez','375-198-1693','robert.gomez@hotmail.com','(9Cf*+%sx7UQ',0),(12,'Kristopher','Fisher','028.425.6423','kristopher.fisher@yahoo.com','^!$MPaHHQ8rJ',1),(13,'Rachel','Hines','683.840.4638x59','rachel.hines@yahoo.com','Y*14#UXwNB_b',1),(14,'Hector','Jones','369.505.5192x66','hector.jones@gmail.com','Pvn)eQ22%7uC',1),(15,'Shelley','Melton','(914)314-6583x8','shelley.melton@hotmail.com','%1GVyntp&sUn',0),(16,'Katelyn','Thornton','001-624-330-011','katelyn.thornton@yahoo.com','_sOYAe)bv9TS',0),(17,'Brooke','Martinez','7989832114','brooke.martinez@hotmail.com','#y!C+pYVDf4F',1),(18,'Amy','Salas','(799)561-3672x2','amy.salas@gmail.com',')4O_u&G@!2Q4',1),(19,'Andrew','Rodriguez','(468)545-6248x7','andrew.rodriguez@hotmail.com','$6sWd2x!CgyM',1),(20,'Bethany','Wiggins','001-750-877-174','bethany.wiggins@hotmail.com','RpyS@mJoJ_2Q',1),(21,'Karen','Harris','588-697-1826x98','karen.harris@gmail.com','PJ8LxGZr!yfj',1),(22,'Zachary','Herring','459-088-7401x30','zachary.herring@hotmail.com','L^6isOiqhX92',1),(23,'Daniel','Kelly','(638)722-5535x8','daniel.kelly@gmail.com','c&Cmg7iV&5^J',1),(24,'Brian','Jacobs','(837)237-7823','brian.jacobs@yahoo.com','#NAME?',0),(25,'Jennifer','Rivers','(089)109-3279x3','jennifer.rivers@yahoo.com','tfTYQC2d_8jG',1),(26,'Melinda','Ellis','472.576.7432x99','melinda.ellis@yahoo.com','+j)YHKqh6i$w',1),(27,'Ryan','Mathews','-968','ryan.mathews@hotmail.com','Q6jT6Atp(35+',0),(28,'Michael','Vega','-8887','michael.vega@gmail.com','7I$$3iLoo^x)',1),(29,'Bryce','Reynolds','001-106-638-395','bryce.reynolds@gmail.com','th*t2By3nDqI',0),(30,'Todd','Phillips','(334)636-2192x5','todd.phillips@yahoo.com','XUd8JvDm+azP',1),(31,'Sara','Manning','-5088','sara.manning@gmail.com','w^*66YLodvBD',0),(32,'Jennifer','Boone','001-554-839-094','jennifer.boone@hotmail.com','U@j*k)Vua01I',1),(33,'Jonathan','Hanson','(073)129-8878','jonathan.hanson@yahoo.com','Ox5!Nuhwf&X!',0),(34,'David','Smith','943-075-2626','david.smith@gmail.com','yh4E6q*g#(KT',1),(35,'Shawn','Young','-2602','shawn.young@hotmail.com','fUBa^d!@_y9t',1),(36,'Matthew','Perry','893.419.0005x42','matthew.perry@hotmail.com','&OUVSr*skP2n',0),(37,'Jennifer','Orozco','(725)002-2598','jennifer.orozco@gmail.com','c1b0SlbYpw_T',1),(38,'Joshua','Mcclure','178-941-1574','joshua.mcclure@gmail.com','1*O!CVy$GqT9',1),(39,'Nicholas','Cantu','(526)140-2613','nicholas.cantu@gmail.com','v0Hc9z^a+LA#',0),(40,'Scott','Robinson','2231420122','scott.robinson@yahoo.com','y37N(Gtr)U%2',0),(41,'Jason','Mitchell','001-003-033-916','jason.mitchell@gmail.com','^EPS@D@_M6Wz',1),(42,'Eric','Elliott','192.595.4521','eric.elliott@yahoo.com','FZ+c_8t5WC9B',0),(43,'David','Bryan','493-016-1809','david.bryan@gmail.com','%)(98Z!fWY(1',0),(44,'Amanda','Nelson','(567)010-2797x2','amanda.nelson@hotmail.com','BjQE2OEsi&LA',1),(45,'Joseph','Fitzgerald','740.395.5479x13','joseph.fitzgerald@gmail.com','!2TO*B%SDKw!',0),(46,'Joseph','Bean','538.154.7891x94','joseph.bean@hotmail.com','I^*_9tWl$0+0',1),(47,'Justin','Nguyen','001-052-560-342','justin.nguyen@hotmail.com','(_W9CccAr*6(',0),(48,'Lisa','Coleman','956.250.5916x79','lisa.coleman@gmail.com','E^6O51ikXUMN',0),(49,'Sara','Mooney','053-816-4688x48','sara.mooney@gmail.com','UO^GV*Sx2+*7',1),(50,'Jessica','Oneill','834.836.5080','jessica.oneill@yahoo.com','^&EfLKq%A7P$',1);
/*!40000 ALTER TABLE `user_account` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-04 17:48:13
