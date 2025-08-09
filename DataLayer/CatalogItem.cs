using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CatalogItem
    {
        public static List<Catalog> GetAllStockedCatalog()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalog";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }

        public static List<Catalog> GetAll()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalog";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }

        public static List<Catalog> GetItems()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalogItem";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }

        public static List<DataObject.Catalog> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetCatalogPaging";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Catalog> list = DBUtil.ExecuteMapper<Catalog>(context, new Catalog(), out totalRecord);
            return list;
        }

        public static List<Catalog> GetByName(string text)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetCatalogByName";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());
        }

        //        public static int GetRecordCount(string text)
        //        {
        //            int result = 0;
        //            IDBHelper context = new DBHelper();
        //            context.CommandText = @" SELECT COUNT(*) FROM catalog 
        //WHERE NAME LIKE concat ('%', @text ,'%') 
        //            ";
        //            context.AddParameter("@Text", text);
        //            context.AddParameter("@pageSize", pageSize);
        //            context.AddParameter("@offset", offset);
        //            context.CommandType =   CommandType.StoredProcedure;;
        //            object obj = DBUtil.ExecuteScalar(context);
        //            if (obj != null)
        //                int.TryParse(obj.ToString(), out result);
        //            return result;
        //        }

        public static Catalog GetByID(int ID)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@ID", ID);
            context.CommandText = "Usp_GetCatalogByID";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }

        public static int Delete(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_DeleteCatalogByID";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ID", ID);

            return DBUtil.ExecuteNonQuery(context);
        }

        public static Catalog Update(int ID, string name, string unit, string desc, string note, byte[] productImage, string Username, string type)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdateCatalog";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ID", ID);
            context.AddParameter("@Name", name);
            context.AddParameter("@Unit", unit);
            context.AddParameter("@ModifiedBy", Username);
            context.AddParameter("@Type", type);

            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }


        public static Catalog GetCatalog(string name, string unit)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetCatalogByNameUnit";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Name", name);
            context.AddParameter("@Unit", unit);

            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }

        public static Catalog Insert(string name, string unit, string desc, string note, byte[] productImage, string Username, string type)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_InserCatalog";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Name", name.Trim());
            context.AddParameter("@Unit", unit.Trim());
            context.AddParameter("@CreatedBy", Username);
            context.AddParameter("@Type", type);
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog()).FirstOrDefault();
        }
    }
}
