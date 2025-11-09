using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    public class ReconcileItem
    {
        public static Reconcile GetByID(int reconcileID)
        {

            IDBHelper context = new DBHelper();
            //            context.CommandText = @"SELECT * FROM reconcile
            //WHERE ID =@ReconcileID
            // ";
            context.CommandText = "Usp_GetReconcileByID";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ReconcileID", reconcileID);
            return DBUtil.ExecuteMapper<Reconcile>(context, new Reconcile()).FirstOrDefault();

        }

        public static List<ReconcileDetail> GetDetail(int reconcileID)
        {
            List<ReconcileDetail> items = new List<ReconcileDetail>();
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetReconcileDetailByReconcileID";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ReconcileID", reconcileID);
            items = DBUtil.ExecuteMapper<ReconcileDetail>(context, new ReconcileDetail());

            return items;
        }



        public static List<Reconcile> GetPagingItemToItem(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetReconcileItemPaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Reconcile> list = DBUtil.ExecuteMapper<Reconcile>(context, new Reconcile(), out totalRecord);
            return list;
        }


        public static List<Reconcile> GetPagingItemToProduct(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetReconcilePaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Reconcile> list = DBUtil.ExecuteMapper<Reconcile>(context, new Reconcile(), out totalRecord);
            return list;
        }

        public static Reconcile Insert(DateTime ProccessDate, string Description, ReconcileDetail detail, int reconType)
        {
            Reconcile reconcile = null;
            IDBHelper ictx = new DBHelper();
            try
            {

                ictx.BeginTransaction();
                ictx.CommandText = "Usp_InsertReconcile";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@Description", Description);
                ictx.AddParameter("@ProccessDate", ProccessDate);
                reconcile = DBUtil.ExecuteMapper<Reconcile>(ictx, new Reconcile()).FirstOrDefault();
                if (reconcile != null)
                {
                    ictx.CommandText = "Usp_InsertReconcileDetail";
                    ictx.CommandType = CommandType.StoredProcedure;
                    ictx.AddParameter("@ReconcileID", reconcile.ID);
                    ictx.AddParameter("@CatalogID", detail.CatalogID);
                    ictx.AddParameter("@CatalogQty", detail.CatalogQty);
                    ictx.AddParameter("@CatalogPrice", detail.CatalogPrice);
                    ictx.AddParameter("@ProductID", detail.ProductID);
                    ictx.AddParameter("@ProductPrice", detail.ProductPrice);
                    ictx.AddParameter("@ProductQty", detail.ProductQty);
                    ictx.AddParameter("@CreatedBy", detail.CreatedBy);
                    ictx.AddParameter("@CreatedDate", detail.CreatedDate);
                    ictx.AddParameter("@CatalogPriceDate", detail.CatalogPriceDate);
                    ictx.AddParameter("@hpp", detail.hpp);
                    ictx.AddParameter("@biayaproduksi", detail.biayaproduksi);
                    ictx.AddParameter("@penyusutan", detail.penyusutan);
                    ictx.AddParameter("@biayapenyusutan", detail.biayapenyusutan);

                    int result = DBUtil.ExecuteNonQuery(ictx);
                    if (result > 0)
                    {
                        string creator = "system";
                        Purchase purchaseItem = new Purchase();
                        purchaseItem.PurchaseDate = ProccessDate;
                        int index = PurchaseItem.GetLastID();
                        purchaseItem.PurchaseNo = string.Format("{0}-{1}", string.Format("{0:yyyyMMddHHmmss}", purchaseItem.PurchaseDate), index);
                        purchaseItem.TotalQty = detail.ProductQty;
                        purchaseItem.TotalPrice = detail.ProductQty * detail.ProductPrice;
                        purchaseItem.Notes = string.Format("{0:N2}({1})", detail.ProductQty, detail.ProductUnit);
                        purchaseItem.CreatedBy = creator;
                        purchaseItem.CreatedDate = detail.CreatedDate;
                        List<PurchaseDetail> pDetail = new List<PurchaseDetail>();
                        pDetail.Add(new PurchaseDetail()
                        {
                            CatalogID = detail.ProductID,
                            PricePerUnit = detail.ProductPrice,
                            Qty = detail.ProductQty,
                            PurchaseNo = purchaseItem.PurchaseNo,
                            Unit = detail.ProductUnit,
                            TotalPrice = detail.ProductQty * detail.ProductPrice,
                            CreatedBy = creator,
                            CreatedDate = detail.CreatedDate
                        });
                        purchaseItem.Details = pDetail;

                        PurchaseItem.SaveStock(purchaseItem);

                        string TransNo = "";
                        if (reconType != 1)
                        {
                            TransNo = string.Format("{0:yyyyMMddHHmmssffff}{1}", DateTime.Now, reconcile.ID);
                            Sale saleItem = new Sale();
                            saleItem.TransactionID = TransNo;
                            saleItem.TransactionDate = ProccessDate;

                            List<SaleDetail> saleDetails = new List<SaleDetail>();
                            SaleDetail saleDetail = new SaleDetail();

                            saleDetail.Discount = 0;
                            saleDetail.Price = detail.CatalogPrice;
                            saleDetail.Quantity = detail.CatalogQty;
                            saleDetail.TotalPrice = detail.CatalogPrice * detail.CatalogQty;
                            saleDetail.TransactionID = TransNo;
                            saleDetail.CatalogID = detail.CatalogID;
                            saleDetail.Sequence = 1;

                            saleDetail.Unit = detail.CatalogUnit;
                            saleDetails.Add(saleDetail);
                            saleItem.TotalQty = string.Format("{0:N2}({1})", detail.CatalogQty, detail.CatalogUnit);
                            saleItem.Tax = 0;
                            saleItem.Username = creator;
                            saleItem.TransactionDate = ProccessDate;
                            saleItem.TotalPrice = saleDetail.TotalPrice;
                            saleItem.TotalPaymentReturn = 0;
                            saleItem.TotalPayment = saleDetail.TotalPrice;

                            saleItem.Details = saleDetails;
                            Sale saleItemResult = SaleItem.InsertReconcile(saleItem);
                            TransNo = saleItemResult.TransactionID;
                        }
                        else
                        {
                            KurangiStok(detail.CatalogID, detail.CatalogQty, detail.CreatedBy);
                        }

                        ictx.CommandText = "Usp_UpdateReconcile";
                        ictx.CommandType = CommandType.StoredProcedure;
                        ictx.AddParameter("@transactionid", TransNo);
                        ictx.AddParameter("@purchaseno", purchaseItem.PurchaseNo);
                        ictx.AddParameter("@reconType", reconType);
                        ictx.AddParameter("@ID", reconcile.ID);
                        DBUtil.ExecuteNonQuery(ictx);
                        ictx.CommitTransaction();

                    }
                }
            }
            catch (Exception ex)
            {
                //reconcile = -1;
                ictx.RollbackTransaction();
                LogItem.Error(ex);
            }
            return reconcile;
        }

        static void KurangiStok(int catalogId, decimal stock, string username)
        {
            decimal CurrentStock = 0;
            decimal CurrentColly = 0;
            CatalogStock cs = CatalogStockItem.GetActiveByCatalogID(catalogId);
            if (cs != null)
            {
                CurrentStock = cs.Stock - stock;
                CurrentColly = cs.Colly;
            }
            else
            {
                CurrentStock = CurrentColly - stock;
                CurrentColly = 0;
            }

            int result = CatalogStockItem.UppdateStock(catalogId, CurrentStock, CurrentColly, username);
            if (result > 0)
            {
                //LogItem.Insert(string.Format("{0}-{1}", this.Text, string.Format("Update stock catalogid : {0}, stock :{1}", CatalogID, stock)));           
            }

        }

        public static int Delete(string ReconcileID)
        {
            int result = -1;
            try
            {

                IDBHelper ictx = new DBHelper();
                //                string sql = @"
                //
                //DELETE FROM purchasedetail
                //WHERE PurchaseNo IN (SELECT purchaseno FROM reconcile WHERE ID=@ReconcileID);
                //
                //DELETE FROM purchase
                //WHERE PurchaseNo IN (SELECT purchaseno FROM reconcile WHERE ID=@ReconcileID);
                //
                //DELETE FROM saledetail
                //WHERE TransactionID IN (SELECT transactionid FROM reconcile WHERE ID=@ReconcileID);
                //
                //DELETE FROM sale
                //WHERE TransactionID IN (SELECT transactionid FROM reconcile WHERE ID=@ReconcileID);
                //
                //DELETE FROM reconciledetail WHERE ReconcileID =@ReconcileID;
                //
                //DELETE FROM reconcile WHERE ID =@ReconcileID;
                //";

                ictx.CommandText = "Usp_DeleteTransactionByReconcileID";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@ReconcileID", ReconcileID);
                DBUtil.ExecuteNonQuery(ictx);
                result = 1;
            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
                result = -1;
            }

            return result;
        }
    }
}
