using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;
using DataAccessLayer;
using System.Data;

namespace DataLayer
{
    public class InvoiceItem
    {

        public static List<Invoice> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetInvoicePaging";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Invoice> list = DBUtil.ExecuteMapper<Invoice>(context, new Invoice(), out totalRecord);
            return list;
        }


        public static Invoice Insert(Invoice item, List<InvoiceDetail> details)
        {
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"Usp_InsertInvoice";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@CustomerID", item.CustomerID);
                //ictx.AddParameter("@InvoiceNo", item.InvoiceNo);
                ictx.AddParameter("@InvoiceDate", item.InvoiceDate);
                ictx.AddParameter("@DueDate", item.DueDate);
                ictx.AddParameter("@Total", item.Total);
                ictx.AddParameter("@Status", item.Status);
                ictx.AddParameter("@Delivery", item.Delivery);
                ictx.AddParameter("@Remark", item.Remark);
                ictx.AddParameter("@Attn", item.Attn);
                ictx.AddParameter("@Shipment", item.Shipment);
                ictx.AddParameter("@To", item.To);
                ictx.AddParameter("@Tradeterm", item.Tradeterm);
                ictx.AddParameter("@Payment", item.Payment);
                ictx.AddParameter("@Paid", item.Paid);
                ictx.AddParameter("@TotalDetail", item.TotalDetail);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);

                Invoice result = DBUtil.ExecuteMapper<Invoice>(ictx, new Invoice()).FirstOrDefault();
                if (result != null)
                {
                    foreach (InvoiceDetail detail in details)
                    {
                        ictx.CommandType = CommandType.StoredProcedure;
                        ictx.CommandText = @"Usp_InsertInvoiceDetail";
                        ictx.AddParameter("@InvoiceID", result.InvoiceID);
                        ictx.AddParameter("@TransactionID", detail.TransactionID);
                        ictx.AddParameter("@CatalogID", detail.CatalogID);
                        ictx.AddParameter("@Price", detail.Price);
                        ictx.AddParameter("@Discount", detail.Discount);
                        ictx.AddParameter("@Quantity", detail.Quantity);
                        ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                        ictx.AddParameter("@coli", detail.coli);
                        ictx.AddParameter("@Sequence", detail.RowIndex);
                        ictx.AddParameter("@PrintDate", detail.PrintDate);
                        ictx.AddParameter("@NoNota", detail.NoNota);
                        ictx.AddParameter("@CreatedBy", item.CreatedBy);
                        int detailResult = DBUtil.ExecuteNonQuery(ictx);
                        if (detailResult <= 0)
                        {
                            ictx.RollbackTransaction();
                            return null;
                        }
                    }
                    ictx.CommitTransaction();
                }

                result.Details = InvoiceDetailItem.GetByInvoiceNo(result.InvoiceNo);
                return result;
            }
            catch (Exception ex)
            {
                ictx.RollbackTransaction();
                LogItem.Error(ex);
            }
            return null;
        }

        public static Invoice Update(Invoice item)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdateInvoice";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@InvoiceNo", item.InvoiceNo);
            context.AddParameter("@DueDate", item.DueDate);
            //context.AddParameter("@Total", item.Total);
            //context.AddParameter("@Status", item.Status);
            context.AddParameter("@Delivery", item.Delivery);
            context.AddParameter("@Remark", item.Remark);
            context.AddParameter("@Attn", item.Attn);
            context.AddParameter("@Shipment", item.Shipment);
            context.AddParameter("@To", item.To);
            context.AddParameter("@Tradeterm", item.Tradeterm);
            context.AddParameter("@Payment", item.Payment);
            context.AddParameter("@CreatedBy", item.CreatedBy);
            Invoice result = DBUtil.ExecuteMapper<Invoice>(context, new Invoice()).FirstOrDefault();
            if (result != null)
            {
                result.Details = InvoiceDetailItem.GetByInvoiceNo(result.InvoiceNo);
            }

            return result;
        }

        public static int UpdatePayment(Invoice item)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_InsertInvoice";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@CustomerID", item.CustomerID);
            context.AddParameter("@InvoiceNo", item.InvoiceNo);
            context.AddParameter("@InvoiceDate", item.InvoiceDate);
            context.AddParameter("@DueDate", item.DueDate);
            context.AddParameter("@Total", item.Total);
            context.AddParameter("@Status", item.Status);
            context.AddParameter("@Delivery", item.Delivery);
            context.AddParameter("@Remark", item.Remark);
            context.AddParameter("@Attn", item.Attn);
            context.AddParameter("@Shipment", item.Shipment);
            context.AddParameter("@To", item.To);
            context.AddParameter("@Tradeterm", item.Tradeterm);
            context.AddParameter("@Payment", item.Payment);
            int result = DBUtil.ExecuteNonQuery(context);

            return result;
        }

        public static Invoice GetOptionsByKey(string invoiceNo)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@InvoiceNo", invoiceNo);
            context.CommandText = "Usp_GetInvoiceByNo";
            context.CommandType = CommandType.StoredProcedure;
            Invoice result = DBUtil.ExecuteMapper<Invoice>(context, new Invoice()).FirstOrDefault();
            if (result != null)
            {
                result.Details = InvoiceDetailItem.GetByInvoiceNo(invoiceNo);
            }

            return result;
        }


        public static int Delete(string invoiceNo)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@InvoiceNo", invoiceNo);
            context.CommandText = "Usp_DeleteInvoice";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }


    }
}
