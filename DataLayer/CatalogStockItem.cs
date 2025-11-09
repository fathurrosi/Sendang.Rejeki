using DataAccessLayer;
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

        public static List<CstmCatalogStock> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {           
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
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetStockReport";
            context.CommandType = CommandType.StoredProcedure;
            List<CstmStock> list = DBUtil.ExecuteMapper<CstmStock>(context, new CstmStock());

            return list;
        }

        public static List<CstmCatalogStock> GetAll(DateTime start, DateTime end)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalogStockByRange";
            context.AddParameter("@startDate", start.Date);
            context.AddParameter("@endDate", end.Date);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmCatalogStock> list = DBUtil.ExecuteMapper<CstmCatalogStock>(context, new CstmCatalogStock());

            return list;
        }

        public static CatalogStock GetActiveByCatalogID(int catalogID)
        {
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
