using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;
using DataAccessLayer;
using System.Data;

namespace DataLayer
{
    public class InvoiceDetailItem
    {
        public static List<InvoiceDetail> GetByInvoiceNo(string invoiceNo)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetInvoiceDetailByInvoiceNo";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@InvoiceNo", invoiceNo);
            return DBUtil.ExecuteMapper<InvoiceDetail>(context, new InvoiceDetail());            
        }
    }
}
