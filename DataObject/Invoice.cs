using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    public class Invoice : IDataMapper<Invoice>
    {
        //[Key(true)]
        //[Column("InvoiceID")]
        public Int32 InvoiceID { get; set; }

        //[Key()]
        //[Column("CustomerID")]
        public Int32 CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

        //[Column("InvoiceNo",50)] 
        public string InvoiceNo { get; set; }

        //[Column("InvoiceDate")]
        public DateTime InvoiceDate { get; set; }

        //[Column("DueDate")]
        public DateTime DueDate { get; set; }

        //[Column("Total")]
        public decimal Total { get; set; }

        //[Column("Status",50)] 
        public string Status { get; set; }
        public string StatusDesc { get; set; }

        //[Column("Delivery",1000)] 
        public string Delivery { get; set; }

        //[Column("Remark",1000)] 
        public string Remark { get; set; }

        //[Column("Attn",200)] 
        public string Attn { get; set; }

        //[Column("Shipment",200)] 
        public string Shipment { get; set; }

        //[Column("To",200)] 
        public string To { get; set; }

        //[Column("Tradeterm",200)] 
        public string Tradeterm { get; set; }

        //[Column("Payment",200)] 
        public string Payment { get; set; }

        public decimal Paid { get; set; }

        public decimal TotalDetail { get; set; }

        public List<InvoiceDetail> Details { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<Invoice> DatasetToDto(System.Data.DataSet ds)
        {
            List<Invoice> results = new List<Invoice>();
            if (ds != null)
            {
                System.Data.DataTable tb = ds.Tables[0];
                foreach (System.Data.DataRow dr in tb.Rows)
                {
                    var helper = new Invoice();
                    helper.InvoiceID = (dr["InvoiceID"] != null) ? Convert.ToInt32(dr["InvoiceID"]) : 0;
                    helper.CustomerID = (dr["CustomerID"] != null) ? Convert.ToInt32(dr["CustomerID"]) : 0;
                    helper.CustomerCode = string.Format("{0}", dr["CustomerCode"]);
                    helper.CustomerName = string.Format("{0}", dr["CustomerName"]);
                    helper.InvoiceNo = string.Format("{0}", dr["InvoiceNo"]);
                    helper.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                    helper.DueDate = Convert.ToDateTime(dr["DueDate"]);
                    helper.Total = Convert.ToDecimal(dr["Total"]);
                    helper.Status = string.Format("{0}", dr["Status"]);
                    helper.Delivery = string.Format("{0}", dr["Delivery"]);
                    helper.Remark = string.Format("{0}", dr["Remark"]);
                    helper.Attn = string.Format("{0}", dr["Attn"]);
                    helper.Shipment = string.Format("{0}", dr["Shipment"]);
                    helper.To = string.Format("{0}", dr["To"]);
                    helper.Tradeterm = string.Format("{0}", dr["Tradeterm"]);
                    helper.Payment = string.Format("{0}", dr["Payment"]);
                    helper.Paid = (dr["Paid"] != null) ? 0 : Convert.ToDecimal(dr["Paid"]);
                    helper.TotalDetail = (dr["TotalDetail"] != null) ? 0 : Convert.ToDecimal(dr["TotalDetail"]);
                    results.Add(helper);
                }
            }

            return results;
        }


        public Invoice Map(System.Data.IDataReader reader)
        {
            var helper = new Invoice();
            helper.InvoiceID = (reader["InvoiceID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["InvoiceID"]);
            helper.CustomerID = (reader["CustomerID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["CustomerID"]);
            helper.CustomerCode = string.Format("{0}", reader["CustomerCode"]);
            helper.CustomerName = string.Format("{0}", reader["CustomerName"]);
            helper.InvoiceNo = string.Format("{0}", reader["InvoiceNo"]);
            helper.InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);
            helper.DueDate = Convert.ToDateTime(reader["DueDate"]);
            helper.Total = Convert.ToDecimal(reader["Total"]);
            helper.Status = string.Format("{0}", reader["Status"]);
            helper.StatusDesc = string.Format("{0}", reader["StatusDesc"]);
            
            helper.Delivery = string.Format("{0}", reader["Delivery"]);
            helper.Remark = string.Format("{0}", reader["Remark"]);
            helper.Attn = string.Format("{0}", reader["Attn"]);
            helper.Shipment = string.Format("{0}", reader["Shipment"]);
            helper.To = string.Format("{0}", reader["To"]);
            helper.Tradeterm = string.Format("{0}", reader["Tradeterm"]);
            helper.Payment = string.Format("{0}", reader["Payment"]);
            helper.Paid = (reader["Paid"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Paid"]);
            helper.TotalDetail = (reader["TotalDetail"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalDetail"]);
            helper.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["CreatedDate"]);
            helper.CreatedBy = reader["CreatedBy"].ToString();
            helper.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            helper.ModifiedBy = reader["ModifiedBy"].ToString();

            return helper;
        }


    }


}
