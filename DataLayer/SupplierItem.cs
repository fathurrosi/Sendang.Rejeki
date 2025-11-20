using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class SupplierItem
    {
        public static List<Supplier> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetSupplierPaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Supplier> list = DBUtil.ExecuteMapper<Supplier>(context, new Supplier(), out totalRecord);
            return list;
        }

        public static Supplier GetByCode(string Code)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@Code", Code);
            context.CommandText = "[Usp_GetSupplierByCode]";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Supplier>(context, new Supplier()).FirstOrDefault();
        }

        public static int Delete(string Code)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_DeleteSupplier]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Code", Code);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Insert(string code, string name, string address, string phone, string cellPhone, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_InsertSupplier";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Name", name);
            context.AddParameter("@Code", code);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@CellPhone", cellPhone);
            context.AddParameter("@CreatedBy", Username);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static Supplier Insert(string code, string name, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_InsertAndGetSupplier";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Name", name);
            context.AddParameter("@Code", code);
            context.AddParameter("@CreatedBy", Username);
            return DBUtil.ExecuteMapper<Supplier>(context, new Supplier()).FirstOrDefault();
        }

        public static int Update(string Code, string name, string address, string phone, string cellPhone, string Username, string oldCode)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdateSupplier";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@NewCode", Code);
            context.AddParameter("@Name", name);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@CellPhone", cellPhone);
            context.AddParameter("@ModifiedBy", Username);
            context.AddParameter("@Code", oldCode);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static List<Supplier> GetAll()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetSupplier]";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Supplier>(context, new Supplier());
        }
    }
}
