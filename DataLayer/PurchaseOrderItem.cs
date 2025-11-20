using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataLayer
{
    public class PurchaseOrderItem
    {
        public static List<PurchaseOrder> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetPurchaseOrderPaging";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<PurchaseOrder> list = DBUtil.ExecuteMapper<PurchaseOrder>(context, new PurchaseOrder(), out totalRecord);
            return list;
        }

        public static PurchaseOrder Insert(PurchaseOrder item, List<PurchaseOrderDetail> details)
        {
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"Usp_InsertPurchaseOrder";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@OrderDate", item.OrderDate);
                ictx.AddParameter("@SupplierCode", item.SupplierCode);
                ictx.AddParameter("@TotalAmount", item.TotalAmount);
                ictx.AddParameter("@Status", item.Status);
                ictx.AddParameter("@Delivery", item.Delivery);
                ictx.AddParameter("@DeliveryDate", item.DeliveryDate);
                ictx.AddParameter("@Remark", item.Remark);
                ictx.AddParameter("@Attn", item.Attn);
                ictx.AddParameter("@Shipment", item.Shipment);
                ictx.AddParameter("@To", item.To);
                ictx.AddParameter("@Tradeterm", item.Tradeterm);
                ictx.AddParameter("@Payment", item.Payment);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);

                PurchaseOrder result = DBUtil.ExecuteMapper(ictx, new PurchaseOrder()).FirstOrDefault();
                if (result != null)
                {
                    foreach (PurchaseOrderDetail detail in details)
                    {
                        ictx.CommandType = CommandType.StoredProcedure;
                        ictx.CommandText = @"Usp_InsertPurchaseOrderDetail";
                        ictx.AddParameter("@PurchaseOrderCode", result.PurchaseOrderCode);
                        ictx.AddParameter("@CatalogID", detail.CatalogID);
                        ictx.AddParameter("@Quantity", detail.Quantity);
                        ictx.AddParameter("@UnitPrice", detail.UnitPrice);
                        ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                        ictx.AddParameter("@Sequence", detail.Sequence);
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

                result.Details = PurchaseOrderDetailItem.GetDetailByPurchaseOrderNo(result.PurchaseOrderCode);
                return result;
            }
            catch (Exception ex)
            {
                ictx.RollbackTransaction();
                LogItem.Error(ex);
            }
            return null;
        }

        /*
	@PurchaseOrderCode [varchar](100) ,
	@OrderDate [date] ,
	@SupplierCode [varchar](50) ,
	@TotalAmount [decimal](18, 2) ,
	@Status [varchar](50) ,
	@Delivery [varchar](1000) =NULL,
	@DeliveryDate [datetime]= NULL,
	@Remark [varchar](1000)= NULL,
	@Attn [varchar](200) =NULL,
	@Shipment [varchar](200)= NULL,
	@To [varchar](200) =NULL,
	@Tradeterm [varchar](200)= NULL,	
	@Payment varchar(200) ,
	@CreatedBy [varchar](50)= NULL
         */
        public static PurchaseOrder Update(PurchaseOrder item)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdatePurchaseOrder";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@PurchaseOrderCode", item.PurchaseOrderCode);
            context.AddParameter("@OrderDate", item.OrderDate);
            context.AddParameter("@SupplierCode", item.SupplierCode);
            //context.AddParameter("@TotalAmount", item.TotalAmount);
            context.AddParameter("@Status", item.Status);
            context.AddParameter("@Delivery", item.Delivery);
            context.AddParameter("@DeliveryDate", item.DeliveryDate);
            context.AddParameter("@Remark", item.Remark);
            context.AddParameter("@Attn", item.Attn);
            context.AddParameter("@Shipment", item.Shipment);
            context.AddParameter("@To", item.To);
            context.AddParameter("@Tradeterm", item.Tradeterm);
            context.AddParameter("@Payment", item.Payment);
            context.AddParameter("@CreatedBy", item.CreatedBy);
            PurchaseOrder result = DBUtil.ExecuteMapper(context, new PurchaseOrder()).FirstOrDefault();
            if (result != null)
            {
                result.Details = PurchaseOrderDetailItem.GetDetailByPurchaseOrderNo(result.PurchaseOrderCode);
            }

            return result;
        }

        //public static int UpdatePayment(PurchaseOrder item)
        //{
        //    IDBHelper context = new DBHelper();
        //    context.CommandText = @"Usp_InsertPurchaseOrder";
        //    context.CommandType = CommandType.StoredProcedure;
        //    context.AddParameter("@CustomerID", item.CustomerID);
        //    context.AddParameter("@PurchaseOrderNo", item.PurchaseOrderNo);
        //    context.AddParameter("@PurchaseOrderDate", item.PurchaseOrderDate);
        //    context.AddParameter("@DueDate", item.DueDate);
        //    context.AddParameter("@Total", item.Total);
        //    context.AddParameter("@Status", item.Status);
        //    context.AddParameter("@Delivery", item.Delivery);
        //    context.AddParameter("@Remark", item.Remark);
        //    context.AddParameter("@Attn", item.Attn);
        //    context.AddParameter("@Shipment", item.Shipment);
        //    context.AddParameter("@To", item.To);
        //    context.AddParameter("@Tradeterm", item.Tradeterm);
        //    context.AddParameter("@Payment", item.Payment);
        //    int result = DBUtil.ExecuteNonQuery(context);

        //    return result;
        //}
        public static PurchaseOrder GetByCode(string PurchaseOrderNo)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@PurchaseOrderCode", PurchaseOrderNo);
            context.CommandText = "Usp_GetPurchaseOrderByNo";
            context.CommandType = CommandType.StoredProcedure;
            PurchaseOrder result = DBUtil.ExecuteMapper(context, new PurchaseOrder()).FirstOrDefault();
            if (result != null)
            {
                result.Details = PurchaseOrderDetailItem.GetDetailByPurchaseOrderNo(result.PurchaseOrderCode);
            }

            return result;
        }
        public static int Delete(string PurchaseOrderNo)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@PurchaseOrderNo", PurchaseOrderNo);
            context.CommandText = "Usp_DeletePurchaseOrder";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }

    }
}
