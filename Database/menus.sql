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

/*Data for the table menu */

SET IDENTITY_INSERT menu ON




insert  into menu(ID,Code,Name,ParentID,Sequence,Ico,Description,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) values (1,'File','File',0,1,NULL,'file',NULL,NULL,'2017-01-30 21:19:14','fathur'),(2,'Master','Master',0,2,NULL,'',NULL,NULL,'2017-01-30 21:19:27','fathur'),(3,'Transaction','Transaction',0,4,NULL,'',NULL,NULL,'2017-01-30 21:19:47','fathur'),(4,'report','Report',0,5,NULL,'',NULL,NULL,'2017-01-30 21:20:47','fathur'),(5,'UM','User Management',0,7,NULL,'',NULL,NULL,'2017-07-22 05:46:50','fathur'),(6,'user','User',5,10,NULL,'User',NULL,NULL,'2018-06-03 01:01:59','fathur'),(7,'role','Role',5,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,'Previllage','Previllage',5,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,'exit','Exit',1,2,NULL,'',NULL,NULL,'2016-12-26 09:33:30','fathur'),(10,'menu','Menu',5,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(11,'catalog','Produk',2,0,NULL,'',NULL,NULL,'2018-11-06 17:57:53','lia'),(12,'supplier','Supplier',2,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(15,'salespoint','Penjualan',3,1,NULL,'Form Untuk menjual barang','2016-11-13 03:23:07','fathur','2018-11-14 15:35:40','lia'),(16,'logout','Logout',1,3,NULL,'log out for this applicaition','2016-11-17 05:51:42','fathur','2016-12-26 09:34:50','fathur'),(18,'SaleList','Resume Penjualan',3,2,NULL,'','2016-11-19 09:13:56','fathur','2018-11-07 17:51:08','lia'),(19,'customer','Customer',2,5,NULL,'','2016-11-19 11:03:27','fathur',NULL,NULL),(21,'stock','Detil Stok',28,1,NULL,'Stock Detail/Update','2016-11-21 21:44:49','fathur','2018-11-14 15:37:54','lia'),(25,'salespermonth','Total Penjualan per Item (Bulanan)',34,6,NULL,'Grafik Penjualan Bulanan total','2016-12-16 00:48:19','fathur','2018-11-07 18:32:28','lia'),(27,'lapstockdetail','Rincian Stok',34,2,NULL,'Laporan Stock','2016-12-30 05:12:34','fathur','2018-11-07 18:25:12','lia'),(28,'mstStock','Stock',0,3,NULL,'Stock','2017-03-03 22:04:04','fathur',NULL,NULL),(29,'purchase','Input Stock',28,2,NULL,'Input Stock','2017-03-03 23:57:35','fathur',NULL,NULL),(30,'salesperformancepermonth','Total Penjualan Bulanan',34,9,NULL,'Total Sales Performance','2017-03-06 22:21:34','fathur','2018-11-07 18:28:42','lia'),(31,'polist','Resume Pembelian',28,3,NULL,'Stock List Per Supplier','2017-03-19 17:43:25','fathur','2018-11-14 15:35:17','lia'),(32,'ar','Piutang (AR)',4,4,NULL,'Account Receivable','2017-04-25 12:42:36','lia','2018-11-14 15:37:03','lia'),(33,'recon','Rekonsiliasi Produk',3,5,NULL,'Product Reconcile','2017-05-01 21:58:55','fathur','2018-11-14 15:35:53','lia'),(34,'neraca','Neraca',0,6,NULL,'Neraca','2017-07-22 05:42:07','fathur','2017-07-22 05:46:29','fathur'),(35,'dailysales','Penjualan Harian ( Per Customer)',4,1,NULL,'Daily Sales ( Per Customer)','2017-07-22 23:15:23','fathur','2018-11-14 15:36:33','lia'),(36,'dailysalescatalog','Penjualan Harian ( Per Produk)',4,2,NULL,'Daily Sales ( Per Item)','2017-08-01 20:47:42','fathur','2018-11-14 15:36:47','lia'),(37,'lapstock','Stok Harian',34,1,NULL,'Current Stock','2017-08-05 02:25:37','fathur','2018-11-07 18:24:46','lia'),(38,'hpp','Harga Pokok Penjualan (HPP)',34,3,NULL,'Harga Pokok Penjualan (HPP)','2017-11-28 20:57:42','fathur','2018-10-14 01:53:30','fathur'),(39,'genhpp','Penghitungan HPP',3,6,NULL,'Generate HPP','2018-01-21 18:33:12','fathur','2018-11-14 15:36:11','lia'),(40,'rppc','Item Pembelian (Customer) Bulanan',34,8,NULL,'Item Purchased Per Month','2018-01-24 20:59:49','fathur','2018-11-07 18:23:28','lia'),(41,'frmTSCM','Total Penjualan per Customer',34,7,NULL,'Total Sales per Customer (Monthly)','2018-01-28 21:23:09','fathur','2018-11-07 18:27:02','lia'),(42,'ndgp','Laba Kotor Harian',34,4,NULL,'New Daily Gross Profit','2018-03-04 19:35:37','fathur','2018-11-07 18:25:33','lia'),(45,'tpmonthly','Tonase Pembelian Bulanan',34,10,NULL,'Total Purchase (Monthly)','2018-04-08 20:40:14','fathur','2018-11-07 18:30:17','lia'),(47,'tppsmonthly','Data Pembelian per Supplier',34,11,NULL,'Total Purchase Per Supplier (Monthly)','2018-04-08 23:10:26','fathur','2018-11-07 18:31:35','lia'),(48,'MonthlySales','Penjualan Bulanan',34,5,NULL,'Monthly Sales','2018-04-26 21:34:14','fathur','2018-11-07 18:25:48','lia');

SET IDENTITY_INSERT menu OFF


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
