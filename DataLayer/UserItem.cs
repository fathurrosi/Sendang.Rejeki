using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class UserItem
    {
        public static User GetUser(string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetUser";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Username", Username);
            List<User> result = DBUtil.ExecuteMapper<User>(context, new User());
            User user = result.FirstOrDefault();
            user.Roles = RoleItem.GetByUsername(user.Username);
            return user;
        }

        public static void UpdateLogin(string Username, string machine, string ipAddress)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdateUser";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Username", Username);
            context.AddParameter("@MachineName", machine);
            context.AddParameter("@IPAddress", ipAddress);
            DBUtil.ExecuteNonQuery(context);
        }



        public static int Insert(string Username, string password, List<Role> roles)
        {
            int result = -1;
            try
            {

                IDBHelper context = new DBHelper();
                context.BeginTransaction();
                context.CommandText = @"Usp_InsertUser";
                context.CommandType = CommandType.StoredProcedure;
                context.AddParameter("@Username", Username);
                context.AddParameter("@Password", password);
                result = DBUtil.ExecuteNonQuery(context);
                if (result > 0)
                {
                    roles.ForEach(t =>
                    {
                        context.Clear();
                        context.AddParameter("Username", Username);
                        context.AddParameter("@RoleID", t.ID);
                        context.CommandText = @"Usp_InsertUserRole";
                        context.CommandType = CommandType.StoredProcedure;
                        DBUtil.ExecuteNonQuery(context);
                    });
                    context.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
                result = -1;
            }

            return result;
        }

        //
        public static int Update(string Username, string password, List<Role> roles)
        {
            int result = -1;
            try
            {
                IDBHelper context = new DBHelper();
                context.BeginTransaction();
                context.CommandText = @"Usp_UpdateUserPassword";
                context.CommandType = CommandType.StoredProcedure;
                context.AddParameter("@Username", Username);
                context.AddParameter("@Password", password);
                result = DBUtil.ExecuteNonQuery(context);
                if (result > 0)
                {
                    context.Clear();
                    context.AddParameter("Username", Username);
                    context.CommandText = @"Usp_DeleteUserRole";
                    context.CommandType = CommandType.StoredProcedure;
                    DBUtil.ExecuteNonQuery(context);
                    roles.ForEach(t =>
                    {
                        context.Clear();
                        context.AddParameter("Username", Username);
                        context.AddParameter("@RoleID", t.ID);
                        context.CommandText = @"Usp_InsertUserRole";
                        context.CommandType = CommandType.StoredProcedure;
                        DBUtil.ExecuteNonQuery(context);
                    });
                    context.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                LogItem.Error(ex);
                result = -1;
            }
            return result;
        }

        public static int UpdatePassword(string Username, string password)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdateUserPassword";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Username", Username);
            context.AddParameter("@Password", password);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Delete(string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_DeleteUser";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Username", Username);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static List<User> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetUserPaging";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<User> result = DBUtil.ExecuteMapper<User>(context, new User(), out totalRecord);
            result.ForEach(t => { t.Roles = RoleItem.GetByUsername(t.Username); });
            return result;
        }

//        public static int GetRecordCount(string text)
//        {
//            int result = 0;
//            IDBHelper context = new DBHelper();
//            context.CommandText = @" 
//
//SELECT count(*) from User WHERE Username LIKE concat ('%', @text ,'%') 
//  
//            ";
//            context.AddParameter("@Text", text);
//            context.CommandType =   CommandType.StoredProcedure;;
//            object obj = DBUtil.ExecuteScalar(context);
//            if (obj != null)
//                int.TryParse(obj.ToString(), out result);
//            return result;
//        }

    }
}
