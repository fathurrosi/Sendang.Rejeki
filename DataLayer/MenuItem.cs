using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class MenuItem
    {
        public static List<Menu> GetMenus()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetMenu]";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Menu>(context, new Menu());
        }

        public static List<CstmMenu> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"[Usp_GetMenuPaging]";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<CstmMenu> result = DBUtil.ExecuteMapper<CstmMenu>(context, new CstmMenu(), out totalRecord);
            return result;
        }

        //        public static int GetRecordCount(string text)
        //        {
        //            int result = 0;
        //            IDBHelper context = new DBHelper();
        //            context.CommandText = @" 
        //SELECT count(1) from Menu m
        //left join Menu parent on parent.ID = m.ParentID
        //WHERE 
        //m.Name LIKE concat ('%', @text ,'%') 
        //OR m.Description LIKE concat ('%', @text ,'%')  
        //or parent.Name LIKE concat ('%', @text ,'%') 
        //            ";
        //            context.AddParameter("@Text", text);
        //            context.CommandType =  CommandType.StoredProcedure;
        //            object obj = DBUtil.ExecuteScalar(context);
        //            if (obj != null)
        //                int.TryParse(obj.ToString(), out result);
        //            return result;
        //        }

        public static Menu GetMenuByID(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_GetMenuByID";
            context.AddParameter("@ID", ID);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Menu>(context, new Menu()).FirstOrDefault();
        }

        public static int Insert(string Code, string Name, string Description, int ParentID, int Sequence, byte[] Ico, string Username)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@Code", Code);
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@ParentID", ParentID);
            context.AddParameter("@Sequence", Sequence);
            //context.AddParameter("@Ico", Ico);
            context.AddParameter("@CreatedBy", Username);
            context.CommandText = @"Usp_InsertMenu";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Update(int ID, string Code, string Name, string Description, int ParentID, int Sequence, byte[] Ico, string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_UpdateMenu";
            context.AddParameter("@ID", ID);
            context.AddParameter("@Code", Code);
            context.AddParameter("@Name", Name);
            context.AddParameter("@Description", Description);
            context.AddParameter("@ParentID", ParentID);
            context.AddParameter("@Sequence", Sequence);
            //context.AddParameter("@Ico", Ico);
            context.AddParameter("@ModifiedBy", Username);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Delete(int ID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_DeleteMenu";
            context.AddParameter("@ID", ID);
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteNonQuery(context);
        }

    }
}
