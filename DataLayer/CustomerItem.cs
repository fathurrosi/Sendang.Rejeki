using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CustomerItem
    {
        public static List<Customer> GetAll()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetCustomer]";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Customer>(context, new Customer());
        }

        public static int Update(int id,string code, string name, string address, string phone, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_UpdateCustomer]";
            context.AddParameter("@ID", id);
            context.AddParameter("@Code", code);
            context.AddParameter("@FullName", name);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@ModifiedBy", Username);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }

        public static Customer Insert(string name, string address, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_InsertAndGetCustomer]";
            context.AddParameter("@Address", address);
            context.AddParameter("@FullName", name);
            context.AddParameter("@CreatedBy", Username);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Customer>(context, new Customer()).FirstOrDefault();
        }


        public static int Insert(string name, string address, string phone, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_InsertCustomer]";
            context.AddParameter("@FullName", name);
            context.AddParameter("@Address", address);
            context.AddParameter("@Phone", phone);
            context.AddParameter("@CreatedBy", Username);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }


        public static Customer GetByID(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetCustomerByID]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ID", ID);
            return DBUtil.ExecuteMapper<Customer>(context, new Customer()).FirstOrDefault();
        }


        public static List<Customer> GetByName(string fullname)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetCustomerByFullname]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@fullname", fullname);
            return DBUtil.ExecuteMapper<Customer>(context, new Customer());
        }

        public static int Delete(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_DeleteCustomer]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ID", ID);
            return DBUtil.ExecuteNonQuery(context);
        }



        public static List<Customer> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetCustomerPaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Customer> list = DBUtil.ExecuteMapper<Customer>(context, new Customer(), out totalRecord);
            return list;
        }

        //        public static List<Customer> GetPaging(string text, int offset, int pageSize)
        //        {

        //            string query = @"
        //SELECT *
        //  FROM Customer 
        //
        //WHERE Fullname LIKE concat ('%', @text ,'%') 
        //OR Address LIKE concat ('%', @text ,'%') 
        //OR Phone LIKE concat ('%', @text ,'%') 
        //LIMIT  @pageSize OFFSET @offset
        //";

        //            IDBHelper context = new DBHelper();
        //            context.CommandText = "[Usp_GetCustomerPaging]";
        //            context.CommandType = CommandType.StoredProcedure;
        //            context.AddParameter("@text", text);
        //            context.AddParameter("@pageSize", pageSize);
        //            context.AddParameter("@offset", offset);
        //            List<Customer> list = DBUtil.ExecuteMapper<Customer>(context, new Customer());

        //            return list;
        //        }

        //        public static int GetRecordCount(string text)
        //        {
        //            int result = 0;
        //            IDBHelper context = new DBHelper();
        //            context.CommandText = @" SELECT COUNT(*) FROM Customer 
        //WHERE Fullname LIKE concat ('%', @text ,'%') 
        //OR Address LIKE concat ('%', @text ,'%') 
        //OR Phone LIKE concat ('%', @text ,'%') 
        //
        //  
        //            ";
        //            context.AddParameter("@Text", text);
        //            context.CommandType = CommandType.StoredProcedure;
        //            object obj = DBUtil.ExecuteScalar(context);
        //            if (obj != null)
        //                int.TryParse(obj.ToString(), out result);
        //            return result;
        //        }
    }
}
