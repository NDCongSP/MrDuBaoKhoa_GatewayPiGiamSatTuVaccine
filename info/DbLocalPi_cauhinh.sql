CREATE DATABASE  IF NOT EXISTS `cauhinh` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `cauhinh`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: 45.119.212.41    Database: cauhinh
-- ------------------------------------------------------
-- Server version	5.7.26-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `gioihannhietdo`
--

DROP TABLE IF EXISTS `gioihannhietdo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `gioihannhietdo` (
  `muccao` varchar(50) NOT NULL,
  `mucthap` varchar(45) DEFAULT NULL,
  `sdtsms` varchar(1000) DEFAULT NULL,
  `sdtcall` varchar(50) DEFAULT NULL,
  `email` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`muccao`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gioihannhietdo`
--

LOCK TABLES `gioihannhietdo` WRITE;
/*!40000 ALTER TABLE `gioihannhietdo` DISABLE KEYS */;
INSERT INTO `gioihannhietdo` VALUES ('40','10','0909167655','1234','soft@atpro.com.vn');
/*!40000 ALTER TABLE `gioihannhietdo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `idds18b20`
--

DROP TABLE IF EXISTS `idds18b20`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `idds18b20` (
  `id` int(11) NOT NULL,
  `DCID` varchar(100) DEFAULT NULL,
  `tencb` varchar(100) DEFAULT NULL,
  `offset` float DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `idds18b20`
--

LOCK TABLES `idds18b20` WRITE;
/*!40000 ALTER TABLE `idds18b20` DISABLE KEYS */;
INSERT INTO `idds18b20` VALUES (1,'28-3c01b5569653','Tu 1',0),(2,'28-3c01b5569653','Tu 2',0),(3,'28-3c01b5569653','Tu 3',0),(4,'28-3c01b5569653','Tu 4',0),(5,'28-3c01b5569653','Tu 5',0),(6,'idnguon','Nguon Dien',0);
/*!40000 ALTER TABLE `idds18b20` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tendonvi`
--

DROP TABLE IF EXISTS `tendonvi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tendonvi` (
  `ten` varchar(100) NOT NULL,
  PRIMARY KEY (`ten`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tendonvi`
--

LOCK TABLES `tendonvi` WRITE;
/*!40000 ALTER TABLE `tendonvi` DISABLE KEYS */;
INSERT INTO `tendonvi` VALUES ('trung tâm');
/*!40000 ALTER TABLE `tendonvi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `thoigiancapnhat`
--

DROP TABLE IF EXISTS `thoigiancapnhat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `thoigiancapnhat` (
  `thoigian` int(11) NOT NULL,
  `thoigiancoi` int(11) DEFAULT NULL,
  PRIMARY KEY (`thoigian`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `thoigiancapnhat`
--

LOCK TABLES `thoigiancapnhat` WRITE;
/*!40000 ALTER TABLE `thoigiancapnhat` DISABLE KEYS */;
INSERT INTO `thoigiancapnhat` VALUES (2,1);
/*!40000 ALTER TABLE `thoigiancapnhat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cauhinh'
--

--
-- Dumping routines for database 'cauhinh'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-08-04  9:36:42
