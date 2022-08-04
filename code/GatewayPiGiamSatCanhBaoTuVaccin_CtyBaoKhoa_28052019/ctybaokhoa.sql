CREATE DATABASE  IF NOT EXISTS `ctybaokhoa` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ctybaokhoa`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: 103.48.195.37    Database: ctybaokhoa
-- ------------------------------------------------------
-- Server version	5.6.16

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
-- Table structure for table `alarm`
--

DROP TABLE IF EXISTS `alarm`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `alarm` (
  `thoigian` datetime NOT NULL,
  `tencb` varchar(100) DEFAULT NULL,
  `giatrithap` varchar(45) DEFAULT NULL,
  `giatrihientai` varchar(45) DEFAULT NULL,
  `giatricao` varchar(45) DEFAULT NULL,
  `canhbao` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`thoigian`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alarm`
--

LOCK TABLES `alarm` WRITE;
/*!40000 ALTER TABLE `alarm` DISABLE KEYS */;
/*!40000 ALTER TABLE `alarm` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Table structure for table `hienthiweb`
--

DROP TABLE IF EXISTS `hienthiweb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hienthiweb` (
  `cb1` varchar(45) NOT NULL,
  `cb2` varchar(45) DEFAULT NULL,
  `cb3` varchar(45) DEFAULT NULL,
  `cb4` varchar(45) DEFAULT NULL,
  `cb5` varchar(45) DEFAULT NULL,
  `cb6` varchar(45) DEFAULT NULL,
  `cb7` varchar(45) DEFAULT NULL,
  `cb8` varchar(45) DEFAULT NULL,
  `cb9` varchar(45) DEFAULT NULL COMMENT 'relay bao mat dien',
  PRIMARY KEY (`cb1`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hienthiweb`
--

LOCK TABLES `hienthiweb` WRITE;
/*!40000 ALTER TABLE `hienthiweb` DISABLE KEYS */;
INSERT INTO `hienthiweb` VALUES ('0','0','0','0','0','0','0','0','GOOD');
/*!40000 ALTER TABLE `hienthiweb` ENABLE KEYS */;
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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `idds18b20`
--

LOCK TABLES `idds18b20` WRITE;
/*!40000 ALTER TABLE `idds18b20` DISABLE KEYS */;
INSERT INTO `idds18b20` VALUES (1,'28-00000b0c6dc6','Tu 1'),(2,'28-00000b0c25eb','Tu 2'),(3,'28-00000b0c225d','Tu 3'),(4,'28-00000b0d40a2','Tu 4'),(5,'28-00000b0e9c4e','Tu 5'),(6,'28-00000b0e20ba','Tu 6'),(7,'28-00000b0e60ec','Tu 7'),(8,'28-00000b0eaa4d','Tu 8'),(9,'idnguon','Nguon Dien');
/*!40000 ALTER TABLE `idds18b20` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `taikhoan`
--

DROP TABLE IF EXISTS `taikhoan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `taikhoan` (
  `user` varchar(100) NOT NULL,
  `pass` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`user`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `taikhoan`
--

LOCK TABLES `taikhoan` WRITE;
/*!40000 ALTER TABLE `taikhoan` DISABLE KEYS */;
INSERT INTO `taikhoan` VALUES ('admin','admin');
/*!40000 ALTER TABLE `taikhoan` ENABLE KEYS */;
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
-- Table structure for table `xuatbaocao`
--

DROP TABLE IF EXISTS `xuatbaocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xuatbaocao` (
  `thoigian` datetime NOT NULL,
  `tencb` varchar(100) DEFAULT NULL,
  `giatri` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`thoigian`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `xuatbaocao`
--

LOCK TABLES `xuatbaocao` WRITE;
/*!40000 ALTER TABLE `xuatbaocao` DISABLE KEYS */;
/*!40000 ALTER TABLE `xuatbaocao` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-06-01 15:07:44
