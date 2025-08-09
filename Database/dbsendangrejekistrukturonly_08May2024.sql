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
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dbsendangrejeki` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `dbsendangrejeki`;

/*Table structure for table `catalog` */

DROP TABLE IF EXISTS `catalog`;

CREATE TABLE `catalog` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) DEFAULT NULL,
  `Description` text,
  `Notes` text,
  `Photo` longblob,
  `CreatedBy` varchar(20) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(20) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `Unit` varchar(10) DEFAULT NULL,
  `type` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=446 DEFAULT CHARSET=latin1;

/*Table structure for table `catalogstock` */

DROP TABLE IF EXISTS `catalogstock`;

CREATE TABLE `catalogstock` (
  `CatalogID` int(11) DEFAULT NULL,
  `Stock` decimal(18,2) DEFAULT NULL,
  `StockDate` date DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `colly` decimal(18,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `customer` */

DROP TABLE IF EXISTS `customer`;

CREATE TABLE `customer` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` varchar(100) DEFAULT NULL,
  `Address` text,
  `Phone` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(50) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1715 DEFAULT CHARSET=latin1;

/*Table structure for table `dailygrossprofit` */

DROP TABLE IF EXISTS `dailygrossprofit`;

CREATE TABLE `dailygrossprofit` (
  `TransDate` datetime NOT NULL,
  `CatalogID` int(11) NOT NULL,
  `Quantity` decimal(18,2) DEFAULT NULL,
  `Purchase` decimal(18,2) DEFAULT NULL,
  `Sale` decimal(18,2) DEFAULT NULL,
  `GrossProfit` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`TransDate`,`CatalogID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `hpp` */

DROP TABLE IF EXISTS `hpp`;

CREATE TABLE `hpp` (
  `TransDate` date NOT NULL,
  `TotalHPP` decimal(18,2) DEFAULT NULL,
  `CatalogId` bigint(20) NOT NULL,
  `PrevStock` decimal(18,2) DEFAULT NULL,
  `PrevHPP` decimal(18,2) DEFAULT NULL,
  `TotalQty` decimal(18,2) DEFAULT NULL,
  `TotalPrice` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`TransDate`,`CatalogId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `log` */

DROP TABLE IF EXISTS `log`;

CREATE TABLE `log` (
  `LogDate` datetime DEFAULT NULL,
  `ComputerName` varchar(100) DEFAULT NULL,
  `IPAddress` varchar(15) DEFAULT NULL,
  `LogType` varchar(50) DEFAULT NULL,
  `LogMessage` text,
  `Username` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `menu` */

DROP TABLE IF EXISTS `menu`;

CREATE TABLE `menu` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(100) DEFAULT NULL,
  `Name` varchar(500) DEFAULT NULL,
  `ParentID` int(11) DEFAULT NULL,
  `Sequence` int(11) DEFAULT NULL,
  `Ico` blob,
  `Description` text,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=latin1;

/*Table structure for table `previllage` */

DROP TABLE IF EXISTS `previllage`;

CREATE TABLE `previllage` (
  `MenuID` int(11) NOT NULL,
  `RoleID` int(11) NOT NULL,
  `AllowCreate` bit(1) DEFAULT NULL,
  `AllowRead` bit(1) DEFAULT NULL,
  `AllowUpdate` bit(1) DEFAULT NULL,
  `AllowDelete` bit(1) DEFAULT NULL,
  `AllowPrint` bit(1) DEFAULT NULL,
  PRIMARY KEY (`MenuID`,`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `purchase` */

DROP TABLE IF EXISTS `purchase`;

CREATE TABLE `purchase` (
  `PurchaseNo` varchar(100) NOT NULL,
  `PurchaseDate` datetime DEFAULT NULL,
  `Notes` text,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `SupplierCode` varchar(50) DEFAULT NULL,
  `TotalQty` decimal(18,2) DEFAULT NULL,
  `TotalPrice` decimal(18,2) DEFAULT NULL,
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Table structure for table `purchasedetail` */

DROP TABLE IF EXISTS `purchasedetail`;

CREATE TABLE `purchasedetail` (
  `PurchaseNo` varchar(100) NOT NULL,
  `CatalogID` int(11) NOT NULL,
  `Qty` decimal(18,2) DEFAULT NULL,
  `PricePerUnit` decimal(18,2) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `ID` bigint(11) NOT NULL AUTO_INCREMENT,
  `TotalPrice` decimal(18,2) DEFAULT NULL,
  `Unit` varchar(50) DEFAULT NULL,
  `coli` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=382 DEFAULT CHARSET=latin1;

/*Table structure for table `reconcile` */

DROP TABLE IF EXISTS `reconcile`;

CREATE TABLE `reconcile` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ProccessDate` datetime DEFAULT NULL,
  `Description` varchar(5000) DEFAULT NULL,
  `purchaseno` varchar(100) DEFAULT NULL,
  `transactionid` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `reconciledetail` */

DROP TABLE IF EXISTS `reconciledetail`;

CREATE TABLE `reconciledetail` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CatalogID` int(11) DEFAULT NULL,
  `CatalogQty` decimal(18,2) DEFAULT NULL,
  `CatalogPrice` decimal(18,2) DEFAULT NULL,
  `ProductID` int(11) DEFAULT NULL,
  `ProductPrice` decimal(18,2) DEFAULT NULL,
  `ProductQty` decimal(18,2) DEFAULT NULL,
  `CreatedBy` varchar(100) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(100) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ReconcileID` bigint(20) DEFAULT NULL,
  `CatalogPriceDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(20) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Table structure for table `sale` */

DROP TABLE IF EXISTS `sale`;

CREATE TABLE `sale` (
  `TransactionID` varchar(50) NOT NULL,
  `TotalPrice` decimal(18,2) DEFAULT NULL,
  `TotalQty` varchar(1000) DEFAULT NULL,
  `TransactionDate` datetime DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `MemberID` int(11) DEFAULT NULL,
  `Terminal` varchar(50) DEFAULT NULL,
  `TotalPayment` decimal(18,2) DEFAULT NULL,
  `TotalPaymentReturn` decimal(18,2) DEFAULT NULL,
  `Notes` text,
  `PaymentType` int(11) DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  `ExpiredDate` datetime DEFAULT NULL,
  PRIMARY KEY (`TransactionID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `saledetail` */

DROP TABLE IF EXISTS `saledetail`;

CREATE TABLE `saledetail` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TransactionID` varchar(50) DEFAULT NULL,
  `CatalogID` int(11) DEFAULT NULL,
  `Price` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Quantity` decimal(18,2) DEFAULT NULL,
  `TotalPrice` decimal(18,2) DEFAULT NULL,
  `Sequence` int(11) DEFAULT NULL,
  `coli` decimal(18,2) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Table structure for table `supplier` */

DROP TABLE IF EXISTS `supplier`;

CREATE TABLE `supplier` (
  `Code` varchar(50) NOT NULL,
  `Name` varchar(500) DEFAULT NULL,
  `Address` text,
  `Phone` varchar(20) DEFAULT NULL,
  `CellPhone` varchar(20) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `Username` varchar(50) NOT NULL,
  `Password` varchar(1000) DEFAULT NULL,
  `LastLogin` datetime DEFAULT NULL,
  `IsLogin` bit(1) DEFAULT NULL,
  `IPAddress` varchar(15) DEFAULT NULL,
  `MachineName` varchar(100) DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `userrole` */

DROP TABLE IF EXISTS `userrole`;

CREATE TABLE `userrole` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(100) DEFAULT NULL,
  `RoleID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
