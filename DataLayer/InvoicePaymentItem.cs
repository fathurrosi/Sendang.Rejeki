using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;
using DataAccessLayer;
using System.Data;

namespace DataLayer
{
    public class InvoicePaymentItem
    {
        public static InvoicePayment UpdateInvoicePayment(string invoiceNo
            , decimal totalPayment
            , decimal totalReturn
            , decimal paymentAmount
            , string createdBy)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_UpdateInvoicePayment";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@InvoiceNo", invoiceNo);
            context.AddParameter("@TotalPayment", totalPayment);
            context.AddParameter("@TotalReturn", totalReturn);
            context.AddParameter("@PaymentAmount", paymentAmount);
            context.AddParameter("@CreatedBy", createdBy);
            return DBUtil.ExecuteMapper<InvoicePayment>(context, new InvoicePayment()).FirstOrDefault();
        }

        public static InvoicePayment GetByInvoiceNo(string invoiceNo)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetInvoicePaymentByInvoiceNo";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@InvoiceNo", invoiceNo);
            return DBUtil.ExecuteMapper<InvoicePayment>(context, new InvoicePayment()).FirstOrDefault();
        }
    }
}
