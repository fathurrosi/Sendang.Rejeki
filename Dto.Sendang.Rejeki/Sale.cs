using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{

    public class Sale : IDataMapper<Sale>
    {
        public string TransactionID { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalQty { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Username { get; set; }

        public int? MemberID { get; set; }

        public decimal? Tax { get; set; }

        public string Terminal { get; set; }

        public decimal TotalPayment { get; set; }

        public decimal? TotalPaymentReturn { get; set; }

        public string Notes { get; set; }

        public int PaymentType { get; set; }

        public List<SaleDetail> Details { get; set; }

        public Sale Map(System.Data.IDataReader reader)
        {
            Sale obj = new Sale();
            obj.TransactionID = reader["TransactionID"].ToString();
            obj.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);
            obj.TotalQty = Convert.ToDecimal(reader["TotalQty"]);
            obj.TransactionDate = Convert.ToDateTime(reader["TransactionDate"]);
            obj.Username = reader["Username"].ToString();
            obj.MemberID = (reader["MemberID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["MemberID"]);
            obj.Terminal = reader["Terminal"].ToString();
            obj.TotalPayment = Convert.ToDecimal(reader["TotalPayment"]);
            obj.TotalPaymentReturn = (reader["TotalPaymentReturn"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPaymentReturn"]);
            obj.Notes = reader["Notes"].ToString();
            obj.PaymentType = Convert.ToInt32(reader["PaymentType"]);

            return obj;
        }
    }

}
