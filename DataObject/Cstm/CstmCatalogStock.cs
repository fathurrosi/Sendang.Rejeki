using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class StockSummary : IDataMapper<StockSummary>
    {
        public int CatalogID { get; set; }

        public string CatalogName { get; set; }

        public decimal Stock { get; set; }

        public string Unit { get; set; }

        public StockSummary Map(System.Data.IDataReader reader)
        {
            StockSummary obj = new StockSummary();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Stock = 0;

            obj.CatalogName = reader["CatalogName"].ToString();
            obj.Unit = reader["Unit"].ToString();

            return obj;
        }
    }

    public class CstmStock : IDataMapper<CstmStock>
    {

        public int CatalogID { get; set; }

        public string Catalog{ get; set; }

        public decimal Sisa { get; set; }

        public string Satuan { get; set; }

        public decimal Input { get; set; }


        public decimal Output { get; set; }

        public CstmStock Map(System.Data.IDataReader reader)
        {
            CstmStock obj = new CstmStock();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Sisa = reader["Sisa"] is DBNull ? 0 : Convert.ToDecimal(reader["Sisa"]);
            try
            {
                obj.Input = reader["Input"] is DBNull ? 0 : Convert.ToDecimal(reader["Input"]);
                obj.Output = reader["Output"] is DBNull ? 0 : Convert.ToDecimal(reader["Output"]);
            }
            catch (Exception)
            {

            }

            
            obj.Catalog= reader["Catalog"].ToString();
            obj.Satuan = reader["Satuan"].ToString();

            return obj;
        }
    }


    public class CstmCatalogStock : IDataMapper<CstmCatalogStock>
    {
        public DateTime StockDate { get; set; }

        public int CatalogID { get; set; }

        public string CatalogName { get; set; }

        public decimal Stock { get; set; }

        public string Unit { get; set; }

        public string CreatedBy { get; set; }


        public decimal TotalSale { get; set; }

        public decimal TotalPurchase { get; set; }
        public CstmCatalogStock Map(System.Data.IDataReader reader)
        {
            CstmCatalogStock obj = new CstmCatalogStock();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Stock = reader["Stock"] is DBNull ? 0 : Convert.ToDecimal(reader["Stock"]);
            try
            {
                obj.TotalSale = reader["TotalSale"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalSale"]);
                obj.TotalPurchase = reader["TotalPurchase"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalPurchase"]);
            }
            catch (Exception)
            {

            }

            obj.StockDate = (reader["StockDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["StockDate"]);
            //obj.CreatedBy = reader["CreatedBy"].ToString();
            obj.CatalogName = reader["CatalogName"].ToString();
            obj.Unit = reader["Unit"].ToString();

            return obj;
        }
    }
}
