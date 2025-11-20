using DataAccessLayer;
using DataObject;
using System.Collections.Generic;
using System.Data;

namespace DataLayer
{
    public class PurchaseOrderNotesItem
    {
        public static List<PurchaseOrderNotes> GetByCode(string PurchaseOrderCode)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetPurchaseOrderNotes";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@PurchaseOrderCode", PurchaseOrderCode);
            return DBUtil.ExecuteMapper(context, new PurchaseOrderNotes());
        }
    }
}
