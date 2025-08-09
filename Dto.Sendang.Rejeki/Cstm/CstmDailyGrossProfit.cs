using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class CstmTotalSalePerCustomer : IDataMapper<CstmTotalSalePerCustomer>
    {
        public string CustomerName { get; set; }

        public decimal TotalSale { get; set; }
        public string MonthYear { get; set; }

        public DateTime TransDate { get; set; }

        public CstmTotalSalePerCustomer Map(System.Data.IDataReader reader)
        {
            CstmTotalSalePerCustomer obj = new CstmTotalSalePerCustomer();
            obj.CustomerName = string.Format("{0}", reader["CustomerName"]);
            obj.TotalSale = reader["TOTAL_SALE"] is DBNull ? 0 : Convert.ToDecimal(reader["TOTAL_SALE"]);
            obj.MonthYear = reader["MONTH_YEAR"].ToString();
            obj.TransDate = Convert.ToDateTime(reader["YEAR_MONTH_DAY"]);

            return obj;
        }

    }


    public class CstmTotalSalePerMonth : IDataMapper<CstmTotalSalePerMonth>
    {
        public string CatalogName { get; set; }
        public int CatalogID { get; set; }
        public decimal TotalSale { get; set; }
        public string MonthYear { get; set; }

        public DateTime TransDate { get; set; }

        public CstmTotalSalePerMonth Map(System.Data.IDataReader reader)
        {
            CstmTotalSalePerMonth obj = new CstmTotalSalePerMonth();
            obj.CatalogName = string.Format("{0}", reader["CATALOG_NAME"]);
            obj.CatalogID = Convert.ToInt32(reader["CATALOG_ID"]);
            obj.TotalSale = reader["TOTAL_SALE"] is DBNull ? 0 : Convert.ToDecimal(reader["TOTAL_SALE"]);
            obj.MonthYear = reader["MONTH_YEAR"].ToString();
            obj.TransDate = Convert.ToDateTime(reader["YEAR_MONTH_DAY"]);

            return obj;
        }

    }

    public class CstmMonthlyGrossProfit : IDataMapper<CstmMonthlyGrossProfit>
    {

        //SELECT c.ID, c.Name, c.MONTH_YEAR ,  tt.TotalSale, yy.TotalPurchase ,
        //(tt.TotalSale - yy.TotalPurchase) AS MonthlyGrossProfit
        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalPurchase { get; set; }
        public string MONTH_YEAR { get; set; }
        public DateTime TempMonth { get; set; }

        public decimal MonthlyGrossProfit { get; set; }

        public CstmMonthlyGrossProfit Map(System.Data.IDataReader reader)
        {
            CstmMonthlyGrossProfit obj = new CstmMonthlyGrossProfit();
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            obj.Unit = string.Format("{0}", reader["Unit"]);
            obj.TotalSale = reader["TotalSale"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalSale"]);
            obj.TotalPurchase = reader["TotalPurchase"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalPurchase"]);
            obj.MonthlyGrossProfit = reader["MonthlyGrossProfit"] is DBNull ? 0 : Convert.ToDecimal(reader["MonthlyGrossProfit"]);
            obj.MONTH_YEAR = string.Format("{0}", reader["MONTH_YEAR"]);
            obj.TempMonth = reader["TempMonth"] is DBNull ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TempMonth"]);
            return obj;
        }

    }
    public class CstmDailyGrossProfit : IDataMapper<CstmDailyGrossProfit>
    {
        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime CatalogDate { get; set; }

        public decimal Stock { get; set; }
        public decimal DailyGrossProfit { get; set; }

        public CstmDailyGrossProfit Map(System.Data.IDataReader reader)
        {
            CstmDailyGrossProfit obj = new CstmDailyGrossProfit();
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            obj.Unit = string.Format("{0}", reader["Unit"]);
            obj.SellPrice = reader["SellPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["SellPrice"]);
            obj.BuyPrice = reader["BuyPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["BuyPrice"]);
            obj.CatalogDate = (reader["CatalogDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["CatalogDate"]);
            obj.Stock = reader["Stock"] is DBNull ? 0 : Convert.ToDecimal(reader["Stock"]);



            return obj;
        }

    }
}
