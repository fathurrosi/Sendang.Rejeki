
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{


    public class CstmPurchasesPerSupplierDaily : IDataMapper<CstmPurchasesPerSupplierDaily>
    {
        public string SupplierCode { get; set; }
        public string Supplier { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }

        public CstmPurchasesPerSupplierDaily Map(System.Data.IDataReader reader)
        {
            CstmPurchasesPerSupplierDaily obj = new CstmPurchasesPerSupplierDaily();
            obj.SupplierCode = string.Format("{0}", reader["SupplierCode"]);
            obj.Supplier = string.Format("{0}", reader["Supplier"]);
            obj.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
            obj.TotalAmount = (reader["TotalAmount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalAmount"]);
            return obj;
        }
    }

    public class CstmPurchasesPerSupplierMonthly : IDataMapper<CstmPurchasesPerSupplierMonthly>
    {
        public string Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPurchasesAmount { get; set; }

        public CstmPurchasesPerSupplierMonthly Map(System.Data.IDataReader reader)
        {
            CstmPurchasesPerSupplierMonthly obj = new CstmPurchasesPerSupplierMonthly();
            obj.Item = string.Format("{0}", reader["Item"]);
            obj.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Quantity"]);
            obj.TotalPurchasesAmount = (reader["TotalPurchasesAmount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalPurchasesAmount"]);
            return obj;
        }
    }

    public class CstmSalesPerCustomerMonthly : IDataMapper<CstmSalesPerCustomerMonthly>
    {
        public string Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalSalesAmount { get; set; }

        public CstmSalesPerCustomerMonthly Map(System.Data.IDataReader reader)
        {
            CstmSalesPerCustomerMonthly obj = new CstmSalesPerCustomerMonthly();
            obj.Item = string.Format("{0}", reader["Item"]);
            obj.Quantity = (reader["Quantity"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["Quantity"]);
            obj.TotalSalesAmount = (reader["TotalSalesAmount"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalSalesAmount"]);
            return obj;
        }
    }

    public class CstmSalesPercustomerPerYear : IDataMapper<CstmSalesPercustomerPerYear>
    {

        public decimal TotalQty { get; set; }
        public int CatalogID { get; set; }
        public string CatalogName { get; set; }
        public DateTime Bulan { get; set; }
        public CstmSalesPercustomerPerYear Map(System.Data.IDataReader reader)
        {
            CstmSalesPercustomerPerYear obj = new CstmSalesPercustomerPerYear();
            obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);

            DateTime bulan = new DateTime(1900, 1, 1);
            DateTime.TryParse(string.Format("{0}", reader["Bulan"]), out bulan);
            obj.Bulan = bulan;
            obj.TotalQty = (reader["TotalQty"] is System.DBNull) ? 0 : Convert.ToDecimal(reader["TotalQty"]);
            return obj;
        }
    }

}
