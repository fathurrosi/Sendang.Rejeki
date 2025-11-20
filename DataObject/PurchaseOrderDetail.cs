using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace DataObject
{

    public class PurchaseOrderDetail : IDataMapper<PurchaseOrderDetail>
    {
        //[Key(true)]
        //[Column("PurchaseOrderItemID")]
        public Int32 PurchaseOrderItemID { get; set; }

        //[Column("PurchaseOrderCode",100)] 
        public string PurchaseOrderCode { get; set; }

        //[Column("CatalogID")]
        public Int32 CatalogID { get; set; }

        //[Column("Quantity")]
        public decimal Quantity { get; set; }

        //[Column("UnitPrice")]
        public decimal UnitPrice { get; set; }

        //[Column("TotalPrice")]
        public decimal TotalPrice { get; set; }

        //[Column("CreatedBy",50)] 
        public string CreatedBy { get; set; }

        //[Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        //[Column("ModifiedBy",50)] 
        public string ModifiedBy { get; set; }

        //[Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        

        //[Column("Sequence")]
        public Int32? Sequence { get; set; }


        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public decimal Coli { get; set; }
        public Guid UniqueID { get; set; }


        public string DisplayPrice
        {
            get
            {
                return string.Format("Rp. {0:N0}/{1}", UnitPrice, Unit);
            }
        }
        public string DisplayQuantity
        {
            get
            {
                return string.Format("{0:N0} {1}", Quantity, Unit);
            }
        }
        public string DisplayAmount
        {
            get
            {
                return string.Format("Rp. {0:N0}", TotalPrice);
            }
        }

        public string DisplayDeliveryDate
        {
            get
            {
                return string.Format("{0:dd-MMM-yy / hh:mm tt}", DeliveryDate);
            }
        }

        public PurchaseOrderDetail Map(System.Data.IDataReader reader)
        {
            var helper = new PurchaseOrderDetail();
            helper.UniqueID = new Guid();
            helper.PurchaseOrderItemID = (reader["PurchaseOrderItemID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["PurchaseOrderItemID"]);
            helper.PurchaseOrderCode = string.Format("{0}", reader["PurchaseOrderCode"]);
            helper.CatalogID = (reader["CatalogID"] is System.DBNull) ? 0 : Convert.ToInt32(reader["CatalogID"]);
            helper.Quantity = Convert.ToDecimal(reader["Quantity"]);
            helper.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
            helper.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);
            helper.CreatedBy = string.Format("{0}", reader["CreatedBy"]);
            helper.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            helper.ModifiedBy = string.Format("{0}", reader["ModifiedBy"]);
            helper.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            helper.Sequence = (reader["Sequence"] is System.DBNull) ? 0 : Convert.ToInt32(reader["Sequence"]);

            helper.DeliveryDate = (reader["DeliveryDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["DeliveryDate"]);
            
            
            helper.CatalogName = string.Format("{0}", reader["CatalogName"]);
            helper.Unit = string.Format("{0}", reader["Unit"]);
            
            return helper;
        }


    }
}
