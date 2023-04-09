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
-- Dumping data for table `car`
--

LOCK TABLES `car` WRITE;
/*!40000 ALTER TABLE `car` DISABLE KEYS */;
INSERT INTO `car` VALUES (1,'A102AA','Lada Granta',4),(2,'X321ВГ102','Lada Largus',5),(3,'B000BB','Lada Vesta',4);
/*!40000 ALTER TABLE `car` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `comments`
--

LOCK TABLES `comments` WRITE;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `commenttext`
--

LOCK TABLES `commenttext` WRITE;
/*!40000 ALTER TABLE `commenttext` DISABLE KEYS */;
INSERT INTO `commenttext` VALUES (1,'test'),(2,'Лучший'),(3,'dfasdfdascasdc'),(4,'fghhdfdfs'),(5,'Красивая машина'),(6,NULL),(7,NULL);
/*!40000 ALTER TABLE `commenttext` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Пассажир'),(2,'Водитель');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `routes`
--

LOCK TABLES `routes` WRITE;
/*!40000 ALTER TABLE `routes` DISABLE KEYS */;
INSERT INTO `routes` VALUES (1,1,'г Уфа','г Казань','2011-04-20 02:00:00',2000,1,NULL,NULL),(2,1,'г Уфа','г Казань','2009-06-20 23:00:00',1900,1,NULL,NULL),(3,1,'г Уфа ','г Казань ','2023-04-16 00:00:00',1200,6,0,1),(4,3,'г Уфа ','г Москва ','2023-04-10 00:00:00',1200,7,0,1);
/*!40000 ALTER TABLE `routes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `statususerroute`
--

LOCK TABLES `statususerroute` WRITE;
/*!40000 ALTER TABLE `statususerroute` DISABLE KEYS */;
INSERT INTO `statususerroute` VALUES (1,'Одобрен'),(2,'Не одобрен'),(3,'Неизвестно');
/*!40000 ALTER TABLE `statususerroute` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `stausroute`
--

LOCK TABLES `stausroute` WRITE;
/*!40000 ALTER TABLE `stausroute` DISABLE KEYS */;
INSERT INTO `stausroute` VALUES (1,'Набор'),(2,'В пути'),(3,'Закончен'),(4,'Полный');
/*!40000 ALTER TABLE `stausroute` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `userroutes`
--

LOCK TABLES `userroutes` WRITE;
/*!40000 ALTER TABLE `userroutes` DISABLE KEYS */;
INSERT INTO `userroutes` VALUES (1,2,1,1),(2,2,2,1),(3,3,3,1),(4,1,4,1),(5,1,3,3),(6,1,4,3);
/*!40000 ALTER TABLE `userroutes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Идель','Шарипов','2002-12-08','Рафисович','Sharik2002.sharipov@yandex.ru','Sharik2002.sharipov@yandex.ru','qwer',2,1,5.0,1),(2,'Олег','Яковлев','1990-04-12','Петрович','34234@yandex.ru','34234@yandex.ru','1234',2,2,0.0,3),(3,'asdf','asdf','2023-03-30','asdf','32@yandex.ru','32@yandex.ru','1234',2,3,0.0,2);
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

-- Dump completed on 2023-04-10  3:18:40
