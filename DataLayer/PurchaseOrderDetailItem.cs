using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PurchaseOrderDetailItem
    {
        public static List<PurchaseOrderDetail> GetDetailByPurchaseOrderNo(string PurchaseOrderNo)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetPurchaseOrderDetailByPONo";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@PurchaseOrderCode", PurchaseOrderNo);
            return DBUtil.ExecuteMapper(context, new PurchaseOrderDetail());
        }

    }
}
