/*
SQLyog Community Edition- MySQL GUI v8.17 
MySQL - 5.5.5-10.1.36-MariaDB : Database - dbsendangrejeki
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/dbsendangrejeki /*!40100 DEFAULT CHARACTER SET latin1 */;

USE dbsendangrejeki;

/*Data for the table user */
--SET IDENTITY_INSERT [user] ON
insert  into [user] (Username,Password,LastLogin,IsLogin,IPAddress,MachineName,IsActive) values ('admin','bN6682ib1fSzSzDEJjvwQgA=','2024-05-08 20:27:19',0,'192.168.1.11','DESKTOP-5BJROPJ',NULL),('Direksi','jUDeevfONr6t5VZ8QQn5Ds1AtYOXSqx62DD5r+JPVDMA','2024-02-15 18:49:30',0,'192.168.1.33','LAPTOP-E0S7P5JM',0),('lidya','os1vVpMaJZGjotgn0WAkCQA=','2024-02-14 09:56:23',0,'192.168.137.4','SRB-03',0);
--SET IDENTITY_INSERT [user] OFF
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
