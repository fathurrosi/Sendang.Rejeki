﻿using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CatalogStockItem
    {

//        public static int GetRecordCount(string text)
//        {
//            int result = 0;
//            IDBHelper context = new DBHelper();
//            context.CommandText = @" 
//SELECT COUNT(1) AS total
//
//FROM (
//SELECT DISTINCT CatalogDate, Catalog.ID AS CatalogID , Catalog.Name AS CatalogName, Catalog.Unit FROM (
//SELECT pricedate AS CatalogDate FROM catalogPrice
//UNION ALL
//SELECT stockDate AS CatalogDate FROM catalogStock
//) dates
//JOIN Catalog ON Catalog.ID != dates.CatalogDate) MAIN
//LEFT JOIN catalogstock cs ON Main.CatalogID =cs.CatalogID AND cs.StockDate = MAIN.CatalogDate
//
//
//LEFT JOIN (
//SELECT SUM(sd.Quantity) AS TotalSale,  DATE_FORMAT(s.TransactionDate, '%Y-%m-%d') AS TransactionDate, sd.CatalogID FROM  SaleDetail sd
//INNER JOIN Sale s ON sd.TransactionID =s.TransactionID 
//GROUP BY DATE_FORMAT(s.TransactionDate, '%Y-%m-%d'), sd.CatalogID
//) s ON s.CatalogID =Main.CatalogID  AND s.TransactionDate=MAIN.CatalogDate
//LEFT JOIN (
//
//SELECT SUM(pd.Qty) AS TotalPurchase,  DATE_FORMAT(p.PurchaseDate, '%Y-%m-%d') AS PurchaseDate, pd.CatalogID FROM  PurchaseDetail pd
//INNER JOIN Purchase p ON pd.PurchaseNo =p.PurchaseNo
//GROUP BY DATE_FORMAT(p.PurchaseDate, '%Y-%m-%d'), pd.CatalogID
//) p ON p.CatalogID =Main.CatalogID AND p.PurchaseDate=MAIN.CatalogDate
//WHERE Main.CatalogName LIKE concat ('%', @text ,'%') 
//
//            ";
//            context.AddParameter("@Text", text);
//            context.CommandType = CommandType.StoredProcedure;
//            object obj = DBUtil.ExecuteScalar(context);
//            if (obj != null)
//                int.TryParse(obj.ToString(), out result);
//            return result;
//        }

        public static List<CstmCatalogStock> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            //                      string query = @"
            //
            //SELECT MAIN.CatalogID,MAIN.CatalogName, Main.Unit, CatalogDate AS StockDate,CASE WHEN cs.Stock IS NULL THEN 0 ELSE cs.Stock END AS Stock, 
            //
            //
            //CASE WHEN s.TotalSale IS NULL THEN 0 ELSE s.TotalSale END AS TotalSale, CASE WHEN  p.TotalPurchase IS NULL THEN 0 ELSE  p.TotalPurchase  END AS TotalPurchase  
            //FROM (
            //SELECT DISTINCT CatalogDate, Catalog.ID AS CatalogID , Catalog.Name AS CatalogName, Catalog.Unit FROM (
            //SELECT pricedate AS CatalogDate FROM catalogPrice
            //UNION ALL
            //SELECT stockDate AS CatalogDate FROM catalogStock
            //) dates
            //JOIN Catalog ON Catalog.ID != dates.CatalogDate) MAIN
            //LEFT JOIN catalogstock cs ON Main.CatalogID =cs.CatalogID AND cs.StockDate = MAIN.CatalogDate
            //
            //
            //LEFT JOIN (
            //SELECT SUM(sd.Quantity) AS TotalSale,  DATE_FORMAT(s.TransactionDate, '%Y-%m-%d') AS TransactionDate, sd.CatalogID FROM  SaleDetail sd
            //INNER JOIN Sale s ON sd.TransactionID =s.TransactionID 
            //GROUP BY DATE_FORMAT(s.TransactionDate, '%Y-%m-%d'), sd.CatalogID
            //) s ON s.CatalogID =Main.CatalogID  AND s.TransactionDate=MAIN.CatalogDate
            //LEFT JOIN (
            //
            //SELECT SUM(pd.Qty) AS TotalPurchase,  DATE_FORMAT(p.PurchaseDate, '%Y-%m-%d') AS PurchaseDate, pd.CatalogID FROM  PurchaseDetail pd
            //INNER JOIN Purchase p ON pd.PurchaseNo =p.PurchaseNo
            //GROUP BY DATE_FORMAT(p.PurchaseDate, '%Y-%m-%d'), pd.CatalogID
            //) p ON p.CatalogID =Main.CatalogID AND p.PurchaseDate=MAIN.CatalogDate
            //WHERE Main.CatalogName LIKE concat ('%', @text ,'%') 
            //ORDER BY MAIN.CatalogDate DESC, Main.CatalogID
            //
            //LIMIT  @pageSize OFFSET @offset
            //";
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetCatalogStockPaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<CstmCatalogStock> list = DBUtil.ExecuteMapper<CstmCatalogStock>(context, new CstmCatalogStock(), out totalRecord);

            return list;
        }
        public static List<StockSummary> GetAllStock()
        {
            List<StockSummary> list = new List<StockSummary>();
            //            string sql = @"
            //SELECT c.ID AS CatalogID, c.Name AS CatalogName ,c.Unit
            //FROM Catalog c
            // ";

            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalogStockSummary";
            context.CommandType = CommandType.StoredProcedure;
            list = DBUtil.ExecuteMapper<StockSummary>(context, new StockSummary());
            for (int i = 0; i < list.Count; i++)
            {
                CatalogStock stock = GetActiveByCatalogID(list[i].CatalogID);
                if (stock != null)
                {
                    list[i].Stock = stock.Stock;
                }
            }
            return list;
        }

        public static List<CstmStock> GetStockReport()
        {
//            string sql = @"
//
//SELECT c.ID AS CatalogID, c.Name AS Catalog, 
//c.Unit AS Satuan
//,  (CASE WHEN k.Qty IS NULL THEN 0 ELSE k.Qty END ) AS Input
//, (CASE WHEN t.Qty IS NULL THEN 0 ELSE t.Qty END ) AS Output
// ,0 +(CASE WHEN cs.Stock  IS NULL THEN 0 ELSE cs.Stock  END ) +  (CASE WHEN k.Qty IS NULL THEN 0 ELSE k.Qty END ) - (CASE WHEN t.Qty IS NULL THEN 0 ELSE t.Qty END ) AS Sisa 
//
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
//
//ORDER BY c.Name 
//
//";
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetStockReport";
            context.CommandType = CommandType.StoredProcedure;
            List<CstmStock> list = DBUtil.ExecuteMapper<CstmStock>(context, new CstmStock());

            return list;
        }

        public static List<CstmCatalogStock> GetAll(DateTime start, DateTime end)
        {

//            string query = @"
//
//SELECT 
//MAIN.CatalogID, 
//MAIN.CatalogName, 
//Main.Unit, 
//CatalogDate AS StockDate,
//
//0 + (CASE WHEN cs.Stock  IS NULL THEN 0 ELSE cs.Stock  END )  +  (CASE WHEN k.Qty IS NULL THEN 0 ELSE k.Qty END ) - (CASE WHEN t.Qty IS NULL THEN 0 ELSE t.Qty END )  AS Stock
//
//,CASE WHEN s.TotalSale IS NULL THEN 0 ELSE s.TotalSale END AS TotalSale, 0 + (CASE WHEN  p.TotalPurchase IS NULL THEN 0 ELSE  p.TotalPurchase  END) + (CASE WHEN cs.Stock  IS NULL THEN 0 ELSE cs.Stock  END )  AS TotalPurchase  
//
//FROM (
//SELECT DISTINCT CatalogDate, Catalog.ID AS CatalogID , Catalog.Name AS CatalogName, Catalog.Unit FROM 
//(
//SELECT DISTINCT * FROM (
//SELECT CAST(s.TransactionDate AS DATE) AS CatalogDate FROM sale s
//WHERE s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile)
//UNION ALL
//SELECT CAST(PurchaseDate AS DATE) AS CatalogDate FROM Purchase
//WHERE PurchaseNo NOT IN (SELECT DISTINCT PurchaseNo FROM reconcile)
//UNION ALL
//SELECT CAST(stockDate AS DATE) AS CatalogDate FROM CatalogStock
//where stock >0
//) t
//) dates
//JOIN Catalog ON Catalog.ID != dates.CatalogDate) MAIN
//LEFT JOIN catalogstock cs ON Main.CatalogID =cs.CatalogID AND cs.StockDate = MAIN.CatalogDate
//
///*************************************************/
//LEFT JOIN (
//
//SELECT SUM(pd.Qty) AS Qty, CatalogID,  CAST(p.PurchaseDate AS DATE)  AS StockDate FROM purchasedetail pd
//INNER JOIN Purchase p ON p.PurchaseNo= pd.PurchaseNo
//WHERE p.PurchaseNo NOT IN (SELECT DISTINCT PurchaseNo FROM reconcile)
//GROUP BY CatalogID , CAST(p.PurchaseDate AS DATE) 
//) k ON k.CatalogID=  Main.CatalogID AND  CAST(MAIN.CatalogDate AS DATE)= k.StockDate
///*************************************************/
//
///*************************************************/
//LEFT JOIN (
//
//SELECT SUM(pd.Quantity) AS Qty, CatalogID , CAST(s.TransactionDate  AS DATE)   AS StockDate  FROM saledetail pd
//INNER JOIN Sale s ON s.TransactionID = pd.TransactionID
//WHERE s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile)
//GROUP BY CatalogID, CAST(s.TransactionDate AS DATE) 
//) t ON t.CatalogID=  Main.CatalogID AND CAST(MAIN.CatalogDate AS DATE)= t.StockDate
///*************************************************/
//
//
//LEFT JOIN (
//SELECT SUM(sd.Quantity) AS TotalSale,  CAST(s.TransactionDate AS DATE) AS TransactionDate, sd.CatalogID FROM  SaleDetail sd
//INNER JOIN Sale s ON sd.TransactionID =s.TransactionID 
//WHERE s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile)
//
//GROUP BY CAST(s.TransactionDate AS DATE), sd.CatalogID
//) s ON s.CatalogID =Main.CatalogID  AND s.TransactionDate=MAIN.CatalogDate
//LEFT JOIN (
//
//SELECT SUM(pd.Qty) AS TotalPurchase,  CAST(p.PurchaseDate AS DATE) AS PurchaseDate, pd.CatalogID FROM  PurchaseDetail pd
//INNER JOIN Purchase p ON pd.PurchaseNo =p.PurchaseNo
//WHERE p.PurchaseNo NOT IN (SELECT DISTINCT PurchaseNo FROM reconcile)
//GROUP BY CAST(p.PurchaseDate AS DATE), pd.CatalogID
//) p ON p.CatalogID =Main.CatalogID AND p.PurchaseDate=MAIN.CatalogDate
//
//
//WHERE MAIN.CatalogDate BETWEEN @startDate AND @endDate
//ORDER BY MAIN.CatalogDate DESC, Main.CatalogID
//";
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalogStockByRange";
            context.AddParameter("@startDate", start.Date);
            context.AddParameter("@endDate", end.Date);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmCatalogStock> list = DBUtil.ExecuteMapper<CstmCatalogStock>(context, new CstmCatalogStock());

            return list;
        }



//        public static CatalogStock GetByCatalogIDTransDate(int catalogID, DateTime transDate)
//        {

//            string query = @"
//select * from CatalogStock where CatalogID=@CatalogID
//where StockDate =@StockDate
//";
//            IDBHelper context = new DBHelper();
//            context.CommandText = "Usp_GetByCatalogIDTransDate";
//            context.CommandType = CommandType.StoredProcedure;
//            context.AddParameter("@CatalogID", catalogID);
//            context.AddParameter("@StockDate", transDate.Date);
//            return DBUtil.ExecuteMapper<CatalogStock>(context, new CatalogStock()).FirstOrDefault();
//        }

        public static CatalogStock GetActiveByCatalogID(int catalogID)
        {

//            string query = @"
//select * from CatalogStock where CatalogID=@CatalogID and IsActive=1
//order by StockDate DESC
//LIMIT 1
//";

            //AND DATE_FORMAT( StockDate, '%e-%b-%Y') = DATE_FORMAT( getdate(), '%e-%b-%Y')
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetActiveByCatalogID";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@CatalogID", catalogID);
            return DBUtil.ExecuteMapper<CatalogStock>(context, new CatalogStock()).FirstOrDefault();
        }

        public static int UppdateStock(int catalogID, decimal stock, decimal colly, string Username)
        {
            int result = -1;
            CatalogStock item = CurrentStockItem.GetCatalogStock(catalogID);
            string query = string.Empty;
            if (item != null) query = @"Usp_UpdateActiveCatalogstock";
            else query = @"Usp_InserCatalogstock";

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Stock", stock);
            context.AddParameter("@Colly", colly);
            context.AddParameter("@CreatedBy", Username);
            context.AddParameter("@StockDate", (item == null) ? DateTime.Now.Date : item.StockDate);
            context.AddParameter("@CatalogID", catalogID);
            result = DBUtil.ExecuteNonQuery(context);
            return result;
        }
    }
}
