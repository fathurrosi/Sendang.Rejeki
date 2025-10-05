using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataObject.Cstm;

namespace DataLayer
{
    public class SaleItem
    {
        public static List<CstmSales> GetTotalSales(DateTime date)
        {
            //SET @CurrentDate ='2017/11/1';
            //            string query =@"
            //
            //SELECT  DATE_FORMAT( s.TransactionDate, '%m-%Y') AS Bulan,
            //sd.CatalogID, SUM( sd.Price * sd.Quantity) AS Sales FROM SaleDetail sd
            //INNER JOIN Sale s ON s.TransactionId = sd.TransactionID
            //INNER JOIN Catalog c ON c.ID = sd.CatalogID
            //WHERE  DATE_FORMAT( s.TransactionDate, '%m-%Y') =  DATE_FORMAT( @CurrentDate, '%m-%Y')
            //AND c.Type ='Item'
            //GROUP BY DATE_FORMAT( s.TransactionDate, '%m-%Y'),sd.CatalogID
            //";
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = "[Usp_GetTotalSalesPerMonth]";
            ictx.AddParameter("@CurrentDate", date);
            ictx.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<CstmSales>(ictx, new CstmSales());
        }

        public static List<CstmTotalPurchase> GetAllPurchases()
        {
            //            string sql = @"
            //SELECT sd.totalprice AS TotalPrice,
            //sd.PricePerUnit,
            //sd.Qty AS Quantity,
            //CAST(s.PurchaseDate AS DATE) AS PurchaseDate,
            //c.name AS CatalogName, 
            //sd.CatalogID  AS CatalogID, c.Unit
            //FROM purchasedetail sd
            //INNER JOIN Purchase s ON s.PurchaseNo= sd.PurchaseNo
            //INNER JOIN Catalog c ON sd.CatalogID= c.ID                                                                  
            //WHERE  c.Type ='Item'
            //ORDER BY CAST(s.PurchaseDate AS DATE) ,sd.CatalogID
            //";
            List<CstmTotalPurchase> result = new List<CstmTotalPurchase>();

            try
            {
                IDBHelper ictx = new DBHelper();
                ictx.CommandText = "Usp_GetAllPurchases";
                ictx.CommandType = CommandType.StoredProcedure;
                result = DBUtil.ExecuteMapper<CstmTotalPurchase>(ictx, new CstmTotalPurchase());
            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
            }
            return result.OrderBy(t => t.TotalPrice).ToList();
        }
        //public static List<cstmHPP> GetTotalHPH(DateTime start, DateTime end)
        //{
        //    List<cstmHPP> list = new List<cstmHPP>();

        //    List<CstmTotalPurchase> purchaseList = GetAllPurchases(start.AddDays(-1), end);
        //    DateTime _start = purchaseList.Select(t => t.PurchaseDate).Min();
        //    DateTime _end = purchaseList.Select(t => t.PurchaseDate).Max();
        //    for (; _start <= end; _start.AddDays(1))
        //    {
        //        DateTime counterDate = _start;
        //        List<int> catalogIdList = purchaseList.Where(t => t.PurchaseDate.Date == counterDate.Date).Select(t => t.CatalogID).Distinct().ToList();
        //        foreach (int catalogID in catalogIdList)
        //        {
        //            CstmTotalPurchase pNow = purchaseList.Where(t => t.PurchaseDate.Date == counterDate.Date && t.CatalogID == catalogID).FirstOrDefault();
        //            if (pNow != null)
        //            {
        //                CstmTotalPurchase pNext = purchaseList.Where(t => t.PurchaseDate.Date < counterDate.Date && t.CatalogID == catalogID).FirstOrDefault();
        //                if (pNext != null)
        //                {
        //                    //HPP --> harga pokok penjualan
        //                    // HPP = totalharga / totalpenjualan
        //                    decimal nowTotalQty = pNow.Quantity;
        //                    decimal nowTotalPrice = pNow.TotalPrice;
        //                    decimal prevTotalQty = pNext.Quantity;
        //                    decimal provTotalPrice = pNext.TotalPrice;
        //                    //C5 = stok hari ini
        //                    //C7 = stok kemarin
        //                    //E7 = stok kermain * (harga_rata2_kemarin atau hpp_kemarin)
        //                    //E5 = total harga hari ini
        //                    //HPP =(E7+E5)/(C5+C7)
        //                    //----------------------------
        //                    //(Qty kemarin)
        //                }
        //            }
        //        }
        //    }

        //    return list;
        //}
        public static List<Piutang> GetPiutang(DateTime start, DateTime end)
        {
            //            string sql = @"
            //SELECT cust.ID CustomerID, 
            //cust.Fullname AS CustomerName, 
            // s.TransactionID,
            //s.transactiondate AS TransDate,
            //s.TotalPrice,
            //s.TotalPayment,
            //CASE WHEN (s.TotalPrice - s.TotalPayment) >0 THEN (s.TotalPrice - s.TotalPayment) ELSE 0 END AS Piutang,
            //s.ExpiredDate
            //FROM  Sale s 
            //LEFT JOIN Customer cust ON cust.ID =s.MemberID 
            //
            //WHERE s.transactiondate BETWEEN @startDate AND @endDate
            //and (s.TotalPrice - s.TotalPayment) > 0
            //";

            List<Piutang> result = new List<Piutang>();

            try
            {
                IDBHelper ictx = new DBHelper();
                ictx.CommandText = "Usp_GetPiutang";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@startDate", start);
                ictx.AddParameter("@endDate", end);
                result = DBUtil.ExecuteMapper<Piutang>(ictx, new Piutang());
            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
            }
            return result.OrderBy(t => t.TransDate).ToList();

        }
        public static int Update(string TransactionID, DateTime ExpiredDate)
        {
            int result = -1;
            try
            {
                IDBHelper ictx = new DBHelper();
                ictx.CommandText = "Usp_UpdateExpiredDate";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@TransactionID", TransactionID);
                ictx.AddParameter("@ExpiredDate", ExpiredDate);
                result = DBUtil.ExecuteNonQuery(ictx);
            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
                result = -1;
            }
            return result;
        }
        public static int Update(Sale item)
        {
            List<SaleDetail> existingDetailList = SaleDetailItem.GetTransID(item.TransactionID);
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = "Usp_UpdateSale";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@TransactionID", item.TransactionID);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@UpdatedBy", item.Username);
                ictx.AddParameter("@Terminal", item.Terminal);
                ictx.AddParameter("@TotalPayment", item.TotalPayment);
                ictx.AddParameter("@TotalPaymentReturn", item.TotalPaymentReturn);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@PaymentType", item.PaymentType);
                ictx.AddParameter("@UpdatedDate", DateTime.Now);

                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    foreach (SaleDetail detail in item.Details)
                    {
                        int result = -1;
                        SaleDetail existingDetail = existingDetailList.Where(t => t.ID == detail.ID).FirstOrDefault();
                        if (existingDetail != null)
                        {
                            ictx.CommandType = CommandType.StoredProcedure;
                            ictx.CommandText = @"Usp_UpdateSaleDetail";
                            ictx.AddParameter("@CatalogID", detail.CatalogID);
                            ictx.AddParameter("@Price", detail.Price);
                            ictx.AddParameter("@Discount", detail.Discount);
                            ictx.AddParameter("@Quantity", detail.Quantity);
                            ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                            ictx.AddParameter("@Sequence", detail.Sequence);
                            ictx.AddParameter("@ID", detail.ID);
                            result = DBUtil.ExecuteNonQuery(ictx);
                        }
                        else
                        {
                            ictx.CommandText = @"Usp_InsertSaleDetail";
                            ictx.CommandType = CommandType.StoredProcedure;
                            ictx.AddParameter("@TransactionID", item.TransactionID);
                            ictx.AddParameter("@CatalogID", detail.CatalogID);
                            ictx.AddParameter("@Price", detail.Price);
                            ictx.AddParameter("@Discount", detail.Discount);
                            ictx.AddParameter("@Quantity", detail.Quantity);
                            ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                            ictx.AddParameter("@Sequence", detail.Sequence);
                            result = DBUtil.ExecuteNonQuery(ictx);
                        }

                        if (result == -1)
                        {
                            ictx.RollbackTransaction();
                        }
                        else
                        {
                            #region Update Current Stock
                            ictx.CommandType = CommandType.StoredProcedure;
                            //                            ictx.CommandText = @" 
                            //
                            //SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
                            //c.Unit, cs.Stock, cs.StockDate
                            //FROM Catalog c 
                            //LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
                            //AND IsActive = 1
                            //where cs.CatalogID=@CatalogID
                            //";
                            ictx.CommandText = "Usp_GetAllCatalogStockByCatalogID";
                            ictx.AddParameter("@CatalogID", detail.CatalogID);
                            CurrentStock realStock = DBUtil.ExecuteMapper<CurrentStock>(ictx, new CurrentStock()).FirstOrDefault();

                            #endregion

                            #region Update Catalog Stock
                            // update stock
                            //                            ictx.CommandText = @" 
                            //SELECT * FROM catalogstock WHERE CatalogID=@CatalogID AND StockDate=@StockDate
                            //                            ";
                            ictx.CommandType = CommandType.StoredProcedure;
                            ictx.CommandText = "Usp_GetCatalogStockByDate";
                            ictx.AddParameter("@CatalogID", detail.CatalogID);
                            ictx.AddParameter("@StockDate", DateTime.Today);
                            CatalogStock stockItem = DBUtil.ExecuteMapper<CatalogStock>(ictx, new CatalogStock()).FirstOrDefault();
                            if (stockItem != null)
                            {
                                ictx.CommandType = CommandType.StoredProcedure;
                                ictx.CommandText = @"Usp_UpdateCatalogStockByDate";
                                ictx.AddParameter("@CatalogID", detail.CatalogID);
                                ictx.AddParameter("@Username", item.Username);
                                ictx.AddParameter("@StockDate", item.TransactionDate.Date);
                                ictx.AddParameter("@Stock", realStock != null ? realStock.Stock - detail.Quantity : 0 - detail.Quantity);
                                DBUtil.ExecuteNonQuery(ictx);
                            }
                            else
                            {
                                ictx.CommandType = CommandType.StoredProcedure;
                                ictx.CommandText = @"Usp_InsertCatalogStock";
                                ictx.AddParameter("@CatalogID", detail.CatalogID);
                                ictx.AddParameter("@Username", item.Username);
                                ictx.AddParameter("@StockDate", item.TransactionDate.Date);
                                ictx.AddParameter("@Stock", realStock != null ? realStock.Stock - detail.Quantity : 0 - detail.Quantity);
                                DBUtil.ExecuteNonQuery(ictx);
                            }
                            #endregion

                        }
                    }

                    foreach (SaleDetail existing in existingDetailList)
                    {
                        if (item.Details.Where(t => t.ID == existing.ID).Count() == 0)
                        {
                            ictx.CommandType = CommandType.StoredProcedure;
                            ictx.CommandText = "Usp_DeleteSaleDetail";
                            ictx.AddParameter("@ID", existing.ID);
                            DBUtil.ExecuteNonQuery(ictx);
                        }
                    }

                    ictx.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                itemResult = -1; ictx.RollbackTransaction();
                LogItem.Error(ex);
            }

            return itemResult;
        }

        public static int Update(string transID, DateTime updatedDate, string updatedBy)
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = "Usp_UpdateSaleBySale";
            ictx.CommandType = CommandType.StoredProcedure;
            ictx.AddParameter("@TransactionID", transID);
            ictx.AddParameter("@UpdatedDate", updatedDate);
            ictx.AddParameter("@UpdatedBy", updatedBy);
            return DBUtil.ExecuteNonQuery(ictx);
        }
        public static Sale Insert(Sale item)
        {
            Sale itemResult = null;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = "[Usp_InsertSale]";
                ictx.CommandType = CommandType.StoredProcedure;
                //ictx.AddParameter("@TransactionID", item.TransactionID);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TransactionDate", item.TransactionDate);
                ictx.AddParameter("@Username", item.Username);
                ictx.AddParameter("@MemberID", item.MemberID);
                ictx.AddParameter("@Terminal", item.Terminal);
                ictx.AddParameter("@TotalPayment", item.TotalPayment);
                ictx.AddParameter("@TotalPaymentReturn", item.TotalPaymentReturn);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@PaymentType", item.PaymentType);
                ictx.AddParameter("@ExpiredDate", item.ExpiredDate);
                itemResult = DBUtil.ExecuteMapper<Sale>(ictx, new Sale()).FirstOrDefault();
                if (itemResult != null)
                {

                    foreach (SaleDetail detail in item.Details)
                    {
                        ictx.CommandType = CommandType.StoredProcedure;
                        ictx.CommandText = "[Usp_InsertSaleDetailWithColi]";
                        ictx.AddParameter("@TransactionID", itemResult.TransactionID);
                        ictx.AddParameter("@CatalogID", detail.CatalogID);
                        ictx.AddParameter("@Price", detail.Price);
                        ictx.AddParameter("@Discount", detail.Discount);
                        ictx.AddParameter("@Quantity", detail.Quantity);
                        ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                        ictx.AddParameter("@Sequence", detail.Sequence);
                        ictx.AddParameter("@Coli", detail.Coli);

                        int result = DBUtil.ExecuteNonQuery(ictx);
                    }
                    ictx.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                itemResult = null;
                ictx.RollbackTransaction();
                LogItem.Error(ex);
            }

            return itemResult;
        }

        public static Sale GetByTransID(string transactionID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetSaleByTransactionID]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@TransactionID", transactionID);
            List<Sale> result = DBUtil.ExecuteMapper<Sale>(context, new Sale());
            Sale sale = result.FirstOrDefault();
            if (sale != null)
            {
                sale.Details = SaleDetailItem.GetTransID(sale.TransactionID);
            }
            return sale;
        }
        
        public static List<Sale> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetSalePaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Sale> result = DBUtil.ExecuteMapper<Sale>(context, new Sale(), out totalRecord);
           
            return result;
        }
             
        public static List<CstmMonthlyGrossProfit> GetMonthlyGrossProfit(DateTime start, DateTime end)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetMonthlyGrossProfit";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmMonthlyGrossProfit> result = DBUtil.ExecuteMapper<CstmMonthlyGrossProfit>(context, new CstmMonthlyGrossProfit());
            return result;
        }

        public static List<CstmDailySalesPerCatalog> GetDailySalesPerCatalog(DateTime start, DateTime end)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetDailySalesPerCatalog";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmDailySalesPerCatalog> result = DBUtil.ExecuteMapper<CstmDailySalesPerCatalog>(context, new CstmDailySalesPerCatalog());
            return result.OrderByDescending(t => t.TransDate).ToList();
        }

        public static List<CstmDailySale> GetTotalDailySales(DateTime start, DateTime end)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetTotalDailySales";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmDailySale> result = DBUtil.ExecuteMapper<CstmDailySale>(context, new CstmDailySale());
            return result.OrderByDescending(t => t.Tgl).ToList();

        }



        public static List<CstmDailySale> GetTotalDailySalesDetail(DateTime start, DateTime end)
        {
          
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetTotalDailySalesDetail]";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmDailySale> result = DBUtil.ExecuteMapper<CstmDailySale>(context, new CstmDailySale());
            return result.OrderByDescending(t => t.Tgl).ToList();

        }

        /// <summary>
        /// created : 00.36 22/06/2024
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<CstmTotalSalePerMonth> GetTotalSalesPeritemPerMonth(DateTime start, DateTime end)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetTotalSalesPeritemPerMonth]";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmTotalSalePerMonth> result = DBUtil.ExecuteMapper<CstmTotalSalePerMonth>(context, new CstmTotalSalePerMonth());
            return result;

        }

        public static List<CstmTotalSalePerMonth> GetTotalSalePerCatalog(DateTime start, DateTime end)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetTotalSalePerCatalog";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmTotalSalePerMonth> result = DBUtil.ExecuteMapper<CstmTotalSalePerMonth>(context, new CstmTotalSalePerMonth());
            return result;

        }


        public static List<CstmPerformancePerMonth> GetPerformancePerMonth(DateTime start, DateTime end)
        {
          
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetPerformancePerMonth";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmPerformancePerMonth> result = DBUtil.ExecuteMapper<CstmPerformancePerMonth>(context, new CstmPerformancePerMonth());
            return result;
        }
        
        public static List<CstmTotalSalePerCustomer> GetTotalSalePerCustomer(DateTime start, DateTime end)
        {            
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetTotalSalePerCustomer";
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmTotalSalePerCustomer> result = DBUtil.ExecuteMapper<CstmTotalSalePerCustomer>(context, new CstmTotalSalePerCustomer());
            return result;

        }



        public static List<CstmTotalSalePerDay> GetTotalSales(DateTime start, DateTime end)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetTotalSalePerDay";
            context.AddParameter("@startDate", start.Date);
            context.AddParameter("@endDate", end.Date);
            context.CommandType = CommandType.StoredProcedure;
            List<CstmTotalSalePerDay> result = DBUtil.ExecuteMapper<CstmTotalSalePerDay>(context, new CstmTotalSalePerDay());
            return result;
        }
        public static List<CstmDailyGrossProfit> GetDailyGrossProfit(DateTime start, DateTime end)
        {
            List<CstmDailyGrossProfit> list = new List<CstmDailyGrossProfit>();
            List<CstmTotalSalePerDay> penjualan = GetTotalSales(start, end);
            List<cstmHPP> hppList = HPPItem.GetHPP(start.Date, end.Date);

            foreach (CstmTotalSalePerDay item in penjualan)
            {
                DateTime lastTransDate = item.TransDate;
                CstmDailyGrossProfit dgp = new CstmDailyGrossProfit();
                dgp.Transdate = item.TransDate;
                dgp.TotalSales = item.TotalSales;
                dgp.Qty = item.Qty;
                dgp.Unit = item.Unit;
                dgp.CatalogID = item.CatalogID;
                dgp.CatalogName = item.CatalogName;
                List<CstmPurchasePriceRate> listx = PurchaseItem.GetHargaBeliRata(item.CatalogID.ToString(), item.TransDate);
                decimal price = listx.Count > 0 ? listx.Sum(t => t.Price) / listx.Count : 0;
                dgp.HPP = hppList.Where(t => t.CatalogID == item.CatalogID && t.TransDate.Date == item.TransDate.Date).Select(t => t.HPP).Sum();
                dgp.DailyGrossProfit = dgp.TotalSales - (dgp.Qty * dgp.HPP.Value);
                list.Add(dgp);
            }


            return list;
        }


        public static int Delete(string transactionID)
        {
            Sale item = GetByTransID(transactionID);
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = "Usp_DeleteSaledetailByTransactionID";
                ictx.CommandType = CommandType.StoredProcedure;
                ictx.AddParameter("@TransactionID", transactionID);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    itemResult = 0;
                    ictx.CommandText = "Usp_DeleteSaleByTransactionID";
                    ictx.CommandType = CommandType.StoredProcedure;
                    ictx.AddParameter("@TransactionID", transactionID);
                    itemResult = DBUtil.ExecuteNonQuery(ictx);
                    if (itemResult > 0)
                    {
                        ictx.CommitTransaction();
                        itemResult = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ictx.RollbackTransaction();
                LogItem.Error(ex);
            }

            return itemResult;
        }
        
        public static CstmBuyPrice GetPrevBuyPrices(DateTime start, int catalogID)
        {
            // daftar harga beli       
            //            string sql = @"
            //SELECT CatalogID,c.Name AS CatalogName, c.Unit, 
            //SUM(pd.TotalPrice ) TotalBuyPrice, 
            //SUM(pd.Qty) AS TotalQty, 
            //SUM(pd.PricePerUnit) AS TotalPrice, 
            //SUM(pd.PricePerUnit)/COUNT(1) AS BuyPrice, 
            //CAST(p.PurchaseDate  AS DATE) AS TransDate, COUNT(1) AS TotalItem FROM purchasedetail pd
            //INNER JOIN purchase p ON p.PurchaseNo= pd.PurchaseNo 
            //INNER JOIN Catalog c ON c.ID = pd.CatalogID
            //where 
            //p.PurchaseNo NOT IN (SELECT DISTINCT purchaseno FROM reconcile  )
            //AND CAST(p.PurchaseDate  AS DATE)  < CAST(@startDate  AS DATE)  and pd.CatalogID =@CatalogID
            //GROUP BY CatalogID, TransDate  desc
            //";
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetPrevBuyPrices";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@startDate", start);
            context.AddParameter("@CatalogID", catalogID);

            List<CstmBuyPrice> result = DBUtil.ExecuteMapper<CstmBuyPrice>(context, new CstmBuyPrice());
            return result.FirstOrDefault();
        }


        public static List<CstmBuyPrice> GetBuyPrices(DateTime start, DateTime end)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetBuyPrices";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            List<CstmBuyPrice> result = DBUtil.ExecuteMapper<CstmBuyPrice>(context, new CstmBuyPrice());
            return result;
        }

    }
}
