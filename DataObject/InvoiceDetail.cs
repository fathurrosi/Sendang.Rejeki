using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class InvoiceDetail : IDataMapper<InvoiceDetail>
    {
        //[Key(true)]
        //[Column("InvoiceDetailID")]
        public Int32 InvoiceDetailID { get; set; }

        //[Key()]
        //[Column("InvoiceID")]
        public Int32 InvoiceID { get; set; }

        //[Column("TransactionID",50)] 
        public string TransactionID { get; set; }

        //[Column("CatalogID")]
        public Int32? CatalogID { get; set; }

        //[Column("Price")]
        public decimal Price { get; set; }

        //[Column("Discount")]
        public decimal Discount { get; set; }

        //[Column("Quantity")]
        public decimal Quantity { get; set; }

        //[Column("TotalPrice")]
        public decimal TotalPrice { get; set; }

        //[Column("coli")]
        public decimal coli { get; set; }

        //[Column("Sequence")]
        public Int32? Sequence { get; set; }


        public List<InvoiceDetail> DatasetToDto(System.Data.DataSet ds)
        {
            List<InvoiceDetail> results = new List<InvoiceDetail>();
            if (ds != null)
            {
                System.Data.DataTable tb = ds.Tables[0];
                foreach (System.Data.DataRow dr in tb.Rows)
                {
                    var helper = new InvoiceDetail();
                    helper.InvoiceDetailID = (dr["InvoiceDetailID"] != null) ? Convert.ToInt32(dr["InvoiceDetailID"]) : 0;
                    helper.InvoiceID = (dr["InvoiceID"] != null) ? Convert.ToInt32(dr["InvoiceID"]) : 0;
                    helper.TransactionID = string.Format("{0}", dr["TransactionID"]);
                    helper.CatalogID = Convert.ToInt32(dr["CatalogID"]);
                    helper.Price = (dr["Price"] != null) ? Convert.ToDecimal(dr["Price"]) : 0;
                    helper.Discount = (dr["Discount"] != null) ? Convert.ToDecimal(dr["Discount"]) : 0;
                    helper.Quantity = (dr["Quantity"] != null) ? Convert.ToDecimal(dr["Quantity"]) : 0;
                    helper.TotalPrice = (dr["TotalPrice"] != null) ? Convert.ToDecimal(dr["TotalPrice"]) : 0;
                    helper.coli = (dr["coli"] != null) ? Convert.ToDecimal(dr["coli"]) : 0;
                    helper.Sequence = Convert.ToInt32(dr["Sequence"]);
                    results.Add(helper);
                }
            }

            return results;
        }


        public InvoiceDetail Map(System.Data.IDataReader reader)
        {
            var helper = new InvoiceDetail();
            helper.InvoiceDetailID = (reader["InvoiceDetailID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["InvoiceDetailID"]);
            helper.InvoiceID = (reader["InvoiceID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["InvoiceID"]);
            helper.TransactionID = string.Format("{0}", reader["TransactionID"]);
            helper.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            helper.Price = (reader["Price"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Price"]);
            helper.Discount = (reader["Discount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Discount"]);
            helper.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Quantity"]);
            helper.TotalPrice = (reader["TotalPrice"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
            helper.coli = (reader["coli"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["coli"]);
            helper.Sequence = Convert.ToInt32(reader["Sequence"]);

            return helper;
        }


    }

}
