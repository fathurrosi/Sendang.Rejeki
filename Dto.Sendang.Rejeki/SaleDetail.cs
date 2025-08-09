using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class SaleDetail : IDataMapper<SaleDetail>
    {
        public int ID { get; set; }
        
        public string TransactionID { get; set; }

        public string CatalogName { get; set; }
        public int CatalogID { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string Unit { get; set; }


        public SaleDetail Map(System.Data.IDataReader reader)
        {
            SaleDetail obj = new SaleDetail();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.CatalogName = reader["CatalogName"].ToString();
            obj.Unit= reader["Unit"].ToString();
            obj.TransactionID = reader["TransactionID"].ToString();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.Price = (reader["Price"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Price"]);
            obj.Discount = (reader["Discount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Discount"]);
            obj.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Quantity"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            obj.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);

            return obj;
        }
    }

}
