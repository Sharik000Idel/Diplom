-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: localhost    Database: diplomdb
-- ------------------------------------------------------
-- Server version	8.0.27

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
-- Table structure for table `comments`
--

DROP TABLE IF EXISTS `comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comments` (
  `idComment` int NOT NULL,
  `iduserLeaveReview` int DEFAULT NULL,
  `idUserComment` int DEFAULT NULL,
  `estimation` decimal(2,1) DEFAULT NULL,
  `idCommentText` int DEFAULT NULL,
  `Date` date DEFAULT NULL,
  PRIMARY KEY (`idComment`),
  KEY `Comments_CommentText_FK_idx` (`idCommentText`),
  CONSTRAINT `Comments_CommentText_FK` FOREIGN KEY (`idCommentText`) REFERENCES `commenttext` (`idCommentText`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comments`
--

LOCK TABLES `comments` WRITE;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commenttext`
--

DROP TABLE IF EXISTS `commenttext`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commenttext` (
  `idCommentText` int NOT NULL,
  `Text` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idCommentText`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commenttext`
--

LOCK TABLES `commenttext` WRITE;
/*!40000 ALTER TABLE `commenttext` DISABLE KEYS */;
INSERT INTO `commenttext` VALUES (1,'Хороший'),(2,'Лучшие');
/*!40000 ALTER TABLE `commenttext` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `idRole` int NOT NULL,
  `Role` varchar(45) NOT NULL,
  PRIMARY KEY (`idRole`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Пассажир'),(2,'Водитель');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `routes`
--

DROP TABLE IF EXISTS `routes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `routes` (
  `idRout` int NOT NULL,
  `idUser` int NOT NULL,
  `beginRoute` varchar(45) DEFAULT NULL,
  `endRoute` varchar(45) NOT NULL,
  `DataTimeStart` datetime NOT NULL,
  `Cost` int DEFAULT NULL,
  `idCommentText` int DEFAULT NULL,
  PRIMARY KEY (`idRout`),
  KEY `Routes_CommentText_idx` (`idCommentText`),
  KEY `Routes_user_idx` (`idUser`),
  CONSTRAINT `Routes_CommentText` FOREIGN KEY (`idCommentText`) REFERENCES `commenttext` (`idCommentText`),
  CONSTRAINT `Routes_user` FOREIGN KEY (`idUser`) REFERENCES `users` (`idUsers`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `routes`
--

LOCK TABLES `routes` WRITE;
/*!40000 ALTER TABLE `routes` DISABLE KEYS */;
INSERT INTO `routes` VALUES (1,1,'г Уфа','г Казань','2011-04-20 02:00:00',2000,1),(2,1,'г Уфа','г Казань','2009-06-20 23:00:00',1900,1);
/*!40000 ALTER TABLE `routes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userroutes`
--

DROP TABLE IF EXISTS `userroutes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userroutes` (
  `idUserroutes` int NOT NULL,
  `idUser` int NOT NULL,
  `idRout` int NOT NULL,
  PRIMARY KEY (`idUserroutes`),
  KEY `Userroutes_user_FK_idx` (`idUser`),
  KEY `Userroutes_Routes_FK_idx` (`idRout`),
  CONSTRAINT `Userroutes_Routes_FK` FOREIGN KEY (`idRout`) REFERENCES `routes` (`idRout`),
  CONSTRAINT `Userroutes_user_FK` FOREIGN KEY (`idUser`) REFERENCES `users` (`idUsers`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userroutes`
--

LOCK TABLES `userroutes` WRITE;
/*!40000 ALTER TABLE `userroutes` DISABLE KEYS */;
/*!40000 ALTER TABLE `userroutes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `idUsers` int NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `Birthday` date NOT NULL,
  `Lastname` varchar(45) DEFAULT NULL,
  `Email` varchar(45) NOT NULL,
  `Login` varchar(45) DEFAULT NULL,
  `Password` varchar(45) NOT NULL,
  `idRole` int NOT NULL,
  `idCommentText` int DEFAULT NULL,
  `estimation` decimal(2,1) DEFAULT NULL,
  PRIMARY KEY (`idUsers`),
  KEY `Users_Roles_FK_idx` (`idRole`),
  KEY `Users_CommentText_idx` (`idCommentText`),
  CONSTRAINT `Users_CommentText` FOREIGN KEY (`idCommentText`) REFERENCES `commenttext` (`idCommentText`),
  CONSTRAINT `Users_Roles_FK` FOREIGN KEY (`idRole`) REFERENCES `roles` (`idRole`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Idel','Shar','2008-12-20','Raf','Sharik2002.sharipov@yandex.ru','Sharik2002.sharipov@yandex.ru','qwer',1,1,5.0),(2,'Олег','Яковлев','1990-04-12','Петрович','34234@yandex.ru','34234@yandex.ru','1234',2,2,0.0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-04-08  3:13:01
