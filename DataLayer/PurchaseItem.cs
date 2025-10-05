using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public partial class PurchaseItem
    {
        public static List<Purchase> GetAll()
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @"[Usp_GetPurchase]";
            ictx.CommandType = CommandType.StoredProcedure;

            return DBUtil.ExecuteMapper<Purchase>(ictx, new Purchase());
        }

        public static int GetLastID()
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @"[Usp_GetAutoIDPurchase]";
            ictx.CommandType = CommandType.StoredProcedure;
            int lastIndex = 1;
            object item = DBUtil.ExecuteScalar(ictx);
            if (item != null)
            {
                int.TryParse(item.ToString(), out lastIndex);
                return lastIndex + 1;
            }
            return 1;
        }

        public static Purchase GetByCode(string purchaseNo)
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @"[Usp_GetPurchaseByPurchaseNo]";
            ictx.CommandType = CommandType.StoredProcedure;
            if (purchaseNo != null) ictx.AddParameter("@PurchaseNo", purchaseNo);
            Purchase item = DBUtil.ExecuteMapper<Purchase>(ictx, new Purchase()).FirstOrDefault();
            if (item != null)
            {
                item.Details = PurchaseDetailItem.GetByPurchaseNo(item.PurchaseNo);
            }
            return item;
        }

        public static List<CstmPurchasePriceRate> GetHargaBeliRata(string catalogID, DateTime transDate)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[GetHargaBeliRata]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@catalogID", catalogID);
            context.AddParameter("@PurchaseDate", transDate);
            List<CstmPurchasePriceRate> result = DBUtil.ExecuteMapper<CstmPurchasePriceRate>(context, new CstmPurchasePriceRate());
            return result;

        }

        public static CstmPurchaseDetail GetCatalogReconcilePrice(int catalogID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalogReconcilePrice";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@catalogID", catalogID);
            List<CstmPurchaseDetail> result = DBUtil.ExecuteMapper<CstmPurchaseDetail>(context, new CstmPurchaseDetail());

            return result.FirstOrDefault();
        }


        public static List<CstmPurchase> GetCstmPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCstmPurchasePaging";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<CstmPurchase> result = DBUtil.ExecuteMapper<CstmPurchase>(context, new CstmPurchase(), out totalRecord);

            return result;
        }

        public static int Save(Purchase item)
        {
            Purchase existing = GetByCode(item.PurchaseNo);
            if (existing != null)
                throw new Exception(string.Format("{0} already exist!", item.PurchaseNo));
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"[Usp_InsertPurchase]";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@PurchaseDate", item.PurchaseDate);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);
                ictx.AddParameter("@CreatedDate", DateTime.Now);
                ictx.AddParameter("@SupplierCode", item.SupplierCode);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {

                    ictx.CommandType = CommandType.StoredProcedure;
                    foreach (PurchaseDetail itemDetail in item.Details)
                    {
                        ictx.CommandText = @"[Usp_InsertPurchaseDetail]";
                        ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                        ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                        ictx.AddParameter("@Qty", itemDetail.Qty);
                        ictx.AddParameter("@PricePerUnit", itemDetail.PricePerUnit);
                        ictx.AddParameter("@CreatedDate", DateTime.Now);
                        ictx.AddParameter("@CreatedBy", itemDetail.CreatedBy);
                        ictx.AddParameter("@TotalPrice", itemDetail.TotalPrice);
                        ictx.AddParameter("@Unit", itemDetail.Unit);
                        ictx.AddParameter("@Coli", itemDetail.Coli);
                        int detailResult = DBUtil.ExecuteNonQuery(ictx);


                        #region Update HargaBeli
                        //update harga jual
                        ictx.CommandText = @"[Usp_GetCatalogPriceByCatalogID]";
                        ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                        CatalogPrice priceItem = DBUtil.ExecuteMapper<CatalogPrice>(ictx, new CatalogPrice()).FirstOrDefault();
                        if (priceItem != null && priceItem.PriceDate.Date == DateTime.Today)
                        {
                            //update
                            ictx.CommandText = @"Usp_UpdateCatalogprice";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@BuyPricePerUnit", itemDetail.PricePerUnit);
                            ictx.AddParameter("@PriceDate", DateTime.Today);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            ictx.AddParameter("@SupplierCode", item.SupplierCode);
                            DBUtil.ExecuteNonQuery(ictx);

                        }
                        else
                        {
                            // insert
                            ictx.CommandText = @"Usp_InsertCatalogprice";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            ictx.AddParameter("@SellPrice", priceItem == null ? 0 : priceItem.SellPrice);
                            ictx.AddParameter("@PriceDate", DateTime.Today);
                            ictx.AddParameter("@BuyPricePerUnit", itemDetail.PricePerUnit);
                            ictx.AddParameter("@SupplierCode", item.SupplierCode);
                            DBUtil.ExecuteNonQuery(ictx);
                        }
                        #endregion

                        #region UPdateStock
                        ictx.CommandText = @"[Usp_GetCatalogStockByCatalogID]";
                        ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                        CatalogStock stockItem = DBUtil.ExecuteMapper<CatalogStock>(ictx, new CatalogStock()).FirstOrDefault();
                        if (stockItem != null && stockItem.StockDate.Date == DateTime.Today)
                        {
                            //                            ictx.CommandText = @"           
                            //Update catalogstock 
                            //Set Stock = @Stock,
                            //CreatedBy = @Username 
                            //Where CatalogID =@CatalogID 
                            //AND StockDate =@StockDate
                            //";
                            ictx.CommandText = "[Usp_UpdateTodayCatalogStock]";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@Stock", itemDetail.Qty + stockItem.Stock);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            ictx.AddParameter("@StockDate", DateTime.Today);

                            DBUtil.ExecuteNonQuery(ictx);
                        }
                        else
                        {
                            ictx.CommandText = @"[Usp_InsertCatalogStock";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@Stock", (stockItem == null) ? itemDetail.Qty : (itemDetail.Qty + stockItem.Stock));
                            ictx.AddParameter("@StockDate", DateTime.Today);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            DBUtil.ExecuteNonQuery(ictx);
                        }
                        #endregion
                    }

                    ictx.CommitTransaction();
                }

            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
                //throw ex;
            }

            return itemResult;
        }


        public static int SaveStock(Purchase item)
        {
            int itemResult = -1;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"Usp_InsertPurchase";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@PurchaseDate", item.PurchaseDate);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);
                ictx.AddParameter("@CreatedDate", DateTime.Now);
                ictx.AddParameter("@SupplierCode", item.SupplierCode);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                foreach (PurchaseDetail itemDetail in item.Details)
                {
                    ictx.CommandType = CommandType.StoredProcedure;
                    ictx.CommandText = @"Usp_InsertPurchaseDetail";
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                    ictx.AddParameter("@Qty", itemDetail.Qty);
                    ictx.AddParameter("@PricePerUnit", itemDetail.PricePerUnit);
                    ictx.AddParameter("@CreatedDate", DateTime.Now);
                    ictx.AddParameter("@CreatedBy", itemDetail.CreatedBy);
                    ictx.AddParameter("@TotalPrice", itemDetail.TotalPrice);
                    ictx.AddParameter("@Unit", itemDetail.Unit);
                    ictx.AddParameter("@Coli", itemDetail.Coli);
                    int detailResult = DBUtil.ExecuteNonQuery(ictx);
                }

                ictx.CommitTransaction();
                itemResult = 1;

            }
            catch (Exception ex)
            {
                itemResult = -1;
                LogItem.Error(ex);
            }

            return itemResult;
        }


        public static int EditStock(Purchase item)
        {
            int itemResult = -1;
            Purchase existingItem = PurchaseItem.GetByCode(item.PurchaseNo);
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"Usp_UpdatePurchase";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                ictx.AddParameter("@PurchaseDate", item.PurchaseDate);
                ictx.AddParameter("@SupplierCode", item.SupplierCode);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    itemResult = 0;
                    ictx.CommandText = "Usp_DeletePurchaseDetail";
                    ictx.CommandType = CommandType.StoredProcedure;
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    itemResult = DBUtil.ExecuteNonQuery(ictx);
                }

                foreach (PurchaseDetail detail in item.Details)
                {
                    ictx.CommandType = CommandType.StoredProcedure;
                    ictx.CommandText = @"Usp_InsertPurchaseDetail";
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    ictx.AddParameter("@CatalogID", detail.CatalogID);
                    ictx.AddParameter("@Qty", detail.Qty);
                    ictx.AddParameter("@PricePerUnit", detail.PricePerUnit);
                    ictx.AddParameter("@CreatedDate", DateTime.Now);
                    ictx.AddParameter("@CreatedBy", detail.CreatedBy);
                    ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                    ictx.AddParameter("@Unit", detail.Unit);
                    ictx.AddParameter("@Coli", detail.Coli);
                    int detailResult = DBUtil.ExecuteNonQuery(ictx);
                }

                ictx.CommitTransaction();
                itemResult = 1;

            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
                itemResult = -1;
            }

            return itemResult;
        }

        public static int DeleteStock(string PurchaseNo)
        {
            int itemResult = -1;
            Purchase item = PurchaseItem.GetByCode(PurchaseNo);
            IDBHelper ictx = new DBHelper();
            try
            {
                int index = GetLastID();
                ictx.BeginTransaction();
                ictx.CommandText = "Usp_DeletePurchaseDetail";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    // balikin stok dulu
                    itemResult = 0;
                    ictx.CommandText = @"Usp_DeletePurchase";
                    ictx.CommandType = CommandType.StoredProcedure;
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    itemResult = DBUtil.ExecuteNonQuery(ictx);
                }

                ictx.CommitTransaction();
                itemResult = 1;

            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
                itemResult = -1;
            }

            return itemResult;
        }


        public static List<CstmDailyPurchaseDetail> GetTotalDailyPurchaseDetail(DateTime start, DateTime end)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetTotalDailyPurchaseDetail]";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmDailyPurchaseDetail> result = DBUtil.ExecuteMapper<CstmDailyPurchaseDetail>(context, new CstmDailyPurchaseDetail());
            return result.OrderByDescending(t => t.Tgl).ToList();

        }
    }
}
