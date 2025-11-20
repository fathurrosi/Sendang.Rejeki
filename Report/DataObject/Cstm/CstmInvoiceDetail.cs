using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject.Cstm
{
    public class CstmInvoiceDetail : IDataMapper<CstmInvoiceDetail>
    {
        public int? RowIndex { get; set; }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string TransactionID { get; set; }
        public decimal Price { get; set; }
        public string UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public string UnitQty { get; set; }
        public decimal Amount { get; set; }
        public string TotalAmount { get; set; }
        public string FullName { get; set; }
        public string NoNota { get; set; }
        public string Delivery { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PrintDate { get; set; }
        public decimal TotalPayment { get; set; }

        public CstmInvoiceDetail Map(System.Data.IDataReader reader)
        {
            CstmInvoiceDetail obj = new CstmInvoiceDetail();
            obj.RowIndex = (reader["RowIndex"] is System.DBNull) ? (int?)null : Convert.ToInt32(reader["RowIndex"]);
            obj.CatalogId = (reader["CatalogId"] is System.DBNull) ? 0 : Convert.ToInt32(reader["CatalogId"]);

            obj.Price = (reader["Price"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Price"]);
            obj.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Quantity"]);
            obj.Amount = (reader["Amount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Amount"]);
            obj.TotalPayment = (reader["TotalPayment"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPayment"]);
            obj.DueDate = (reader["DueDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["DueDate"]);
            obj.PrintDate = (reader["PrintDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["PrintDate"]);
            obj.TransactionID = string.Format("{0}", reader["TransactionID"]);
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            obj.UnitPrice = string.Format("{0}", reader["UnitPrice"]);
            obj.UnitQty = string.Format("{0}", reader["UnitQty"]);
            obj.TotalAmount = string.Format("{0}", reader["TotalAmount"]);
            obj.FullName = string.Format("{0}", reader["FullName"]);
            obj.NoNota = string.Format("{0}", reader["NoNota"]);
            obj.Delivery = string.Format("{0}", reader["Delivery"]);

            return obj;
        }
    }
}
