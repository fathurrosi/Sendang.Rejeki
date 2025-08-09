using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class PrevillageItem
    {
        public static List<Previllage> GetByRoleID(int roleID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetPrevillageByRole]";
            context.AddParameter("@RoleID", roleID);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Previllage>(context, new Previllage());
        }

        public static List<Previllage> GetByRoleAndMenuID(int roleID, int menuID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetPrevillageByRoleMenu]";
            context.AddParameter("@RoleID", roleID);
            context.AddParameter("@MenuID", menuID);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Previllage>(context, new Previllage());
        }

        public static int Update(List<Previllage> list)
        {
            int result = -1;
            IDBHelper ctx = new DBHelper();
            ctx.BeginTransaction();
            ctx.CommandType = CommandType.StoredProcedure;
            try
            {

                foreach (Previllage item in list)
                {
//                    ctx.CommandText = @"
//select * from Previllage
//where   MenuID = @MenuID and  RoleID = @RoleID ";

//                    ctx.AddParameter("@RoleID", item.RoleID);
//                    ctx.AddParameter("@MenuID", item.MenuID);
//                    Previllage tempItem = DBUtil.ExecuteMapper<Previllage>(ctx, new Previllage()).FirstOrDefault();

                    Previllage tempItem = GetByRoleAndMenuID(item.RoleID, item.MenuID).FirstOrDefault();
                    if (tempItem != null)
                    {
                        //Update
                        ctx.CommandType = CommandType.StoredProcedure;
                        ctx.CommandText = @"[Usp_UpdatePrevillage]";
                    }
                    else
                    {
                        ctx.CommandType = CommandType.StoredProcedure;
                        ctx.CommandText = @"[Usp_InsertPrevillage]";
                        item.AllowCreate = true;
                        item.AllowUpdate = true;
                        item.AllowDelete = true;
                        item.AllowPrint = true;
                        item.AllowRead = true;
                    }
                    ctx.AddParameter("@RoleID", item.RoleID);
                    ctx.AddParameter("@MenuID", item.MenuID);
                    ctx.AddParameter("@AllowCreate", item.AllowCreate);
                    ctx.AddParameter("@AllowRead", item.AllowRead);
                    ctx.AddParameter("@AllowUpdate", item.AllowUpdate);
                    ctx.AddParameter("@AllowDelete", item.AllowDelete);
                    ctx.AddParameter("@AllowPrint", item.AllowPrint);

                    result = DBUtil.ExecuteNonQuery(ctx);
                    if (result == -1)
                    {
                        ctx.RollbackTransaction();
                        break;
                    }


                }
                ctx.CommitTransaction();
            }
            catch (Exception)
            {
                ctx.RollbackTransaction();
            }
            return result;
        }

        public static List<Previllage> GetByUsername(string username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetPrevillageByUsername]";
            context.AddParameter("@Username", username);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Previllage>(context, new Previllage());
        }

        public static List<Previllage> GetAll()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "[Usp_GetPrevillage]";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Previllage>(context, new Previllage());
        }
    }
}
