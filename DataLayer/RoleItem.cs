using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{

    public class RoleItem
    {


        public static List<Role> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetRolePaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Role> list = DBUtil.ExecuteMapper<Role>(context, new Role(), out totalRecord);
            return list;
        }

 
        //        public static int GetRecordCount(string text)
        //        {
        //            int result = 0;
        //            IDBHelper context = new DBHelper();
        //            context.CommandText = @" 
        //SELECT count(*) from Role WHERE Name LIKE concat ('%', @text ,'%')   
        //            ";
        //            context.AddParameter("@Text", text);
        //            context.CommandType = CommandType.StoredProcedure;
        //            object obj = DBUtil.ExecuteScalar(context);
        //            if (obj != null)
        //                int.TryParse(obj.ToString(), out result);
        //            return result;
        //        }



        public static List<Role> GetByUsername(string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetRoleByUsername";
            context.AddParameter("@Username", Username);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Role>(context, new Role());
        }

        public static List<Role> GetRoles()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetRole";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Role>(context, new Role());
        }

        public static Role GetRoleByID(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetRoleByID";
            context.AddParameter("@ID", ID);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Role>(context, new Role()).FirstOrDefault();
        }



        public static int Insert(string Name, string Description, string Username, bool IsSuperAdmin)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@CreatedBy", Username);

            context.AddParameter("@IsSuperAdmin", IsSuperAdmin);
            context.CommandText = @"Usp_InsertRole";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }


        public static int Update(int ID, string Name, string Description, string Username, bool IsSuperAdmin)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdateRole";
            context.AddParameter("@ID", ID);
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@ModifiedBy", Username);
            context.AddParameter("@IsSuperAdmin", IsSuperAdmin);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }


        public static int Delete(int id)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_DeleteRole";
            context.AddParameter("@ID", id);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }
    }
}
