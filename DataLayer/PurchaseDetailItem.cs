using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class PurchaseDetailItem
    {
        public static List<CstmPurchasesPerSupplierMonthly> GetTotalPurchasesMonthly(DateTime month)
        {
            //            string sql = @"
            //SELECT c.Name AS Item ,SUM(sd.Qty) AS Quantity, SUM(sd.TotalPrice) AS TotalPurchasesAmount 
            //FROM purchasedetail sd
            //INNER JOIN purchase s ON s.PurchaseNo=sd.PurchaseNo
            //INNER JOIN Catalog c ON c.ID = sd.CatalogID
            //WHERE DATE_FORMAT(s.PurchaseDate, '%m%Y') = DATE_FORMAT(@currentDate,'%m%Y') and c.TYPE ='item'
            //GROUP BY c.Name ASC 
            //";


            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetTotalPurchasesMonthly";

            context.AddParameter("@currentDate", month);
            context.CommandType = CommandType.StoredProcedure; 
            return DBUtil.ExecuteMapper<CstmPurchasesPerSupplierMonthly>(context, new CstmPurchasesPerSupplierMonthly());
        }

        public static List<CstmPurchasesPerSupplierDaily> GetTotalPurchasesPerSupplierDaily(string supplierCode, DateTime from, DateTime to)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetTotalPurchasesPerSupplierDaily]";
            context.AddParameter("@SupplierCode", supplierCode);
            context.AddParameter("@from", from);
            context.AddParameter("@to", to);
            context.CommandType = CommandType.StoredProcedure; 
            return DBUtil.ExecuteMapper<CstmPurchasesPerSupplierDaily>(context, new CstmPurchasesPerSupplierDaily());
        }

        public static List<CstmPurchasesPerSupplierMonthly> GetTotalPurchasesPerSupplierMonthly(string supplierCode, DateTime month)
        {
            //            string sql = @"
            //SELECT c.Name AS Item ,SUM(sd.Qty) AS Quantity, SUM(sd.TotalPrice) AS TotalPurchasesAmount 
            //FROM purchasedetail sd
            //INNER JOIN purchase s ON s.PurchaseNo=sd.PurchaseNo
            //INNER JOIN Catalog c ON c.ID = sd.CatalogID
            //WHERE s.SupplierCode =@SupplierCode
            //AND  DATE_FORMAT(s.PurchaseDate, '%m%Y') = DATE_FORMAT(@currentDate,'%m%Y')
            //GROUP BY c.Name ASC 
            //";


            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetTotalPurchasesPerSupplierMonthly";
            context.AddParameter("@SupplierCode", supplierCode);
            context.AddParameter("@currentDate", month);
            context.CommandType = CommandType.StoredProcedure; 
            return DBUtil.ExecuteMapper<CstmPurchasesPerSupplierMonthly>(context, new CstmPurchasesPerSupplierMonthly());
        }

        public static List<PurchaseDetail> GetByPurchaseNo(string purchaseNo)
        {
            IDBHelper ictx = new DBHelper();
            //            ictx.CommandText = @"
            //SELECT d.*, c.Name AS CatalogName FROM PurchaseDetail d
            //INNER JOIN catalog c ON c.ID = d.CatalogID
            //
            //  where PurchaseNo =@PurchaseNo
            //";
            ictx.CommandText = "Usp_GetPurchaseDetailByPurchaseNo";
            ictx.CommandType = CommandType.StoredProcedure;
            ictx.AddParameter("@PurchaseNo", purchaseNo);
            return DBUtil.ExecuteMapper<PurchaseDetail>(ictx, new PurchaseDetail());
        }

        public static List<PurchaseDetail> GetAll()
        {
            IDBHelper ictx = new DBHelper();
            //            ictx.CommandText = @"
            //SELECT d.*, c.Name AS CatalogName FROM PurchaseDetail d
            //INNER JOIN catalog c ON c.ID = d.CatalogID
            //";
            ictx.CommandText = "Usp_GetPurchaseDetail";
            ictx.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<PurchaseDetail>(ictx, new PurchaseDetail());
        }
    }
}
