﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;
using DataAccessLayer;
using System.Data;

namespace DataLayer
{
    public class CurrentStockItem
    {

        #region New Method


        public static List<ColiPerCatalog> GetAllColiSales()
        {
//            string query = @"
//SELECT SUM( IF(Coli IS NULL,0,Coli)  ) AS TotalColi, catalogID FROM saledetail
//GROUP BY catalogID
//";
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetAllColiSales";
            context.CommandType =   CommandType.StoredProcedure;;
            return DBUtil.ExecuteMapper<ColiPerCatalog>(context, new ColiPerCatalog());

        }

        public static List<ColiPerCatalog> GetAllColiPurchase()
        {
//            string query = @"
//SELECT SUM( IF(Coli IS NULL,0,Coli)  ) AS TotalColi, catalogID FROM Purchasedetail
//GROUP BY catalogID
//";
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetAllColiPurchase";
            context.CommandType =   CommandType.StoredProcedure;;
            return DBUtil.ExecuteMapper<ColiPerCatalog>(context, new ColiPerCatalog());

        }

        #endregion
        public static CatalogStock GetCatalogStock(int catalogID)
        {
//            string query = @"
//SELECT * FROM CatalogStock
//WHERE catalogID = @catalogID
//AND IsActive =1 ";
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetActiveCatalogStockByCatalogID]";
            context.AddParameter("@catalogID", catalogID);
            context.CommandType =   CommandType.StoredProcedure;;
            return DBUtil.ExecuteMapper<CatalogStock>(context, new CatalogStock()).FirstOrDefault();

        }

        public static CurrentStock GetStock(int catalogID, DateTime TransDate)
        {

            List<CurrentStock> list = new List<CurrentStock>();

            //SET @TransDate ='2017-10-12 00:00:00';
            //SET @CatalogID=1;
//            string sql = @"
//
//SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
//c.Unit, 0 +(CASE WHEN cs.Stock  IS NULL THEN 0 ELSE cs.Stock  END ) +  (CASE WHEN k.Qty IS NULL THEN 0 ELSE k.Qty END ) - (CASE WHEN t.Qty IS NULL THEN 0 ELSE t.Qty END )AS Stock , cs.StockDate,
//0 +(CASE WHEN cs.Colly  IS NULL THEN 0 ELSE cs.Colly  END ) +  (CASE WHEN k.coli IS NULL THEN 0 ELSE k.coli END ) - (CASE WHEN t.coli IS NULL THEN 0 ELSE t.coli END )AS Coli 
//FROM Catalog c 
//LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
//AND  CAST(cs.StockDate AS DATE) <=@TransDate
//AND cs.CatalogID =@CatalogID
//AND IsActive = 1
//LEFT JOIN (
//SELECT SUM(pd.Qty) AS Qty, pd.coli, CatalogID FROM purchasedetail pd
//INNER JOIN Purchase p ON p.PurchaseNo =pd.PurchaseNo
//WHERE CAST(p.PurchaseDate AS DATE) <=@TransDate
//AND CatalogID =@CatalogID
//GROUP BY CatalogID
//) k ON k.CatalogID= c.ID
//
//LEFT JOIN (
//
//SELECT SUM(sd.Quantity) AS Qty, sd.Coli, CatalogID FROM saledetail sd
//INNER JOIN Sale s ON s.TransactionID = sd.TransactionID
//WHERE CAST(s.TransactionDate AS DATE) <=@TransDate
//AND CatalogID =@CatalogID
//GROUP BY CatalogID
//) t ON t.CatalogID= c.ID
//WHERE c.ID =@CatalogID
//
//";
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCurrentStockByCatalogIDAndDate";
            context.AddParameter("@TransDate", TransDate.Date);
            context.AddParameter("@CatalogID", catalogID);

            context.CommandType =   CommandType.StoredProcedure;;
            list = DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock());
            return list.FirstOrDefault();
        }

        public static List<CurrentStock> GetCurrentStockPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            List<CurrentStock> list = new List<CurrentStock>();
//            string sql = @"
//
//SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
//c.Unit, 
//0 +(CASE WHEN cs.Stock  IS NULL THEN 0 ELSE cs.Stock  END ) +  (CASE WHEN k.Qty IS NULL THEN 0 ELSE k.Qty END ) - (CASE WHEN t.Qty IS NULL THEN 0 ELSE t.Qty END )AS Stock ,
//0 +(CASE WHEN cs.Colly  IS NULL THEN 0 ELSE cs.Colly  END ) +  (CASE WHEN k.Colly IS NULL THEN 0 ELSE k.Colly END ) - (CASE WHEN t.Colly IS NULL THEN 0 ELSE t.Colly END )AS Coli ,
//cs.StockDate
//FROM Catalog c 
//LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
//AND IsActive = 1
//LEFT JOIN (
//
//
//SELECT SUM(pd.Qty) AS Qty, SUM(pd.Coli) AS Colly, CatalogID FROM purchasedetail pd
//GROUP BY CatalogID
//) k ON k.CatalogID= c.ID
//
//LEFT JOIN (
//
//SELECT SUM(pd.Quantity) AS Qty, SUM(pd.COli) AS Colly, CatalogID FROM saledetail pd
//GROUP BY CatalogID
//) t ON t.CatalogID = c.ID
//
//where c.Name LIKE concat ('%', @text ,'%') 
//ORDER BY c.Name 
//LIMIT  @pageSize OFFSET @offset
//
//";
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetCurrentStockPaging]";
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            context.CommandType =   CommandType.StoredProcedure;;
            list = DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock(), out totalRecord);
            return list;
        }

        public static List<CurrentStock> GetAllStock(string text)
        {
            List<CurrentStock> list = new List<CurrentStock>();
//            string sql = @"
//SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
//(CASE WHEN cs.Stock IS NULL THEN 0 ELSE cs.Stock END) AS Stock,
//c.Unit
//FROM Catalog c 
//LEFT JOIN CurrentStock cs ON c.ID = cs.CatalogID
//where c.Name LIKE concat ('%', @text ,'%') 
// ";
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetAllCurrentStock";
            context.AddParameter("@text", text);
            context.CommandType =   CommandType.StoredProcedure;;
            list = DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock());

            //List<TotalSales> totalSales = SaleDetailItem.GetTotalSales();
            //list.ForEach(t =>
            //{
            //    var totalSale = totalSales.Where(x => x.CatalogID == t.CatalogID).FirstOrDefault();
            //    t.TotalSales = totalSale == null ? 0 : totalSale.Total;
            //});


            return list;
        }

//        public static CurrentStock GetStock(int catalogID, int pageIndex, int pageSize, out int totalRecord)
//        {
//            List<CurrentStock> list = new List<CurrentStock>();
//            string sql = @"
//SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
//(CASE WHEN cs.Stock IS NULL THEN 0 ELSE cs.Stock END) AS Stock,
//c.Unit
//
//FROM CurrentStock cs 
//INNER JOIN Catalog c  ON c.ID = cs.CatalogID
//
//WHERE cs.CatalogID =@CatalogID  
//
//
//LIMIT  @pageSize OFFSET @offset
// ";
//            IDBHelper context = new DBHelper();
//            context.CommandText = sql;

//            context.CommandType = CommandType.StoredProcedure; 
//            context.AddParameter("@CatalogID", catalogID);
//            context.AddParameter("@pageSize", pageSize);
//            context.AddParameter("@pageIndex", pageIndex);
//            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
//            List<CurrentStock> list = DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock(), out totalRecord);

//            return DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock()).FirstOrDefault();

//        }

//        public static int GetRecordCount(string text)
//        {

//            int result = 0;

//            string sql = @"
//
//SELECT count(1) as total
//FROM Catalog c 
//LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
//AND IsActive = 1
//LEFT JOIN (
//
//
//SELECT SUM(pd.Qty) AS Qty, CatalogID FROM purchasedetail pd
//GROUP BY CatalogID
//) k ON k.CatalogID= c.ID
//
//LEFT JOIN (
//
//SELECT SUM(pd.Quantity) AS Qty, CatalogID FROM saledetail pd
//GROUP BY CatalogID
//) t ON t.CatalogID= c.ID
//
//
//where c.Name LIKE concat ('%', @text ,'%') 
//ORDER BY c.Name 
//
//
//";
//            IDBHelper context = new DBHelper();
//            context.CommandText = sql;
//            context.AddParameter("@text", text);
//            context.CommandType =   CommandType.StoredProcedure;;
//            object obj = DBUtil.ExecuteScalar(context);
//            if (obj != null)
//                int.TryParse(obj.ToString(), out result);
//            return result;
//        }
    }
}
