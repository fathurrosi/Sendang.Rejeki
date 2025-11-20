using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace DataObject
{

    public class PurchaseOrder : IDataMapper<PurchaseOrder>
    {
        //[Column("PurchaseOrderCode",100)] 
        public string PurchaseOrderCode { get; set; }

        //[Column("OrderDate")]
        public DateTime OrderDate { get; set; }

        //[Column("SupplierCode",50)] 
        public string SupplierCode { get; set; }

        //[Column("TotalAmount")]
        public decimal TotalAmount { get; set; }

        //[Column("Status",50)] 
        public string Status { get; set; }

        //[Column("Delivery",1000)] 
        public string Delivery { get; set; }

        //[Column("DeliveryDate")]
        public DateTime? DeliveryDate { get; set; }

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
        public string Payment { get; set; }

        public string CreatedBy { get; set; }

        //[Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        //[Column("ModifiedBy",50)] 
        public string ModifiedBy { get; set; }

        //[Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        public string SupplierName { get; private set; }
        public string StatusDesc { get; private set; }
        public List<PurchaseOrderDetail> Details { get; set; }

        public PurchaseOrder Map(System.Data.IDataReader reader)
        {
            var helper = new PurchaseOrder();
            helper.PurchaseOrderCode = string.Format("{0}", reader["PurchaseOrderCode"]);
            helper.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
            helper.SupplierCode = string.Format("{0}", reader["SupplierCode"]);
            helper.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
            helper.Status = string.Format("{0}", reader["Status"]);
            helper.Delivery = string.Format("{0}", reader["Delivery"]);
            helper.DeliveryDate = (reader["DeliveryDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["DeliveryDate"]);
            helper.Remark = string.Format("{0}", reader["Remark"]);
            helper.Attn = string.Format("{0}", reader["Attn"]);
            helper.Shipment = string.Format("{0}", reader["Shipment"]);
            helper.To = string.Format("{0}", reader["To"]);
            helper.Tradeterm = string.Format("{0}", reader["Tradeterm"]);
            helper.CreatedBy = string.Format("{0}", reader["CreatedBy"]);
            helper.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            helper.ModifiedBy = string.Format("{0}", reader["ModifiedBy"]);
            helper.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            helper.Payment = string.Format("{0}", reader["Payment"]);
            helper.SupplierName = string.Format("{0}", reader["SupplierName"]);
            helper.StatusDesc = string.Format("{0}", reader["StatusDesc"]);
            return helper;
        }
    }
}
