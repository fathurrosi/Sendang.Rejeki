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

/*Data for the table role */

SET IDENTITY_INSERT [role] ON
insert  into [role](ID,Name,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) values (1,'Super Admin','Super Admin',NULL,NULL,'2018-10-14 02:25:53','fathur'),(2,'Admin','Admin',NULL,NULL,'2018-10-14 02:25:39','fathur'),(3,'Operator','Operator','2018-10-14 02:25:22','fathur',NULL,NULL);

SET IDENTITY_INSERT [role] OFF


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
