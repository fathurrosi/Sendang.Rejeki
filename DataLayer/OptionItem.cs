using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using DataObject;
using System.Data;

namespace DataLayer
{
    public class OptionItem
    {

        public static List<DataObject.Options> GetPaging(string text, int pageIndex, int pageSize, out int totalRecord)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_GetOptionsPaging";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@pageIndex", pageIndex);
            context.AddParameter("@totalRecord", 0, ParameterDirection.Output);
            List<Options> list = DBUtil.ExecuteMapper<Options>(context, new Options(), out totalRecord);
            return list;
        }

        public static List<Options> GetOptionsByName(string name)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@Name", name);

            context.CommandText = "Usp_GetOptionsByName";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Options>(context, new Options()).ToList();
        }

        public static Options GetOptionsByKey(string name, string valueMember)
        {
            IDBHelper context = new DBHelper();
            context.AddParameter("@Name", name);
            context.AddParameter("@ValueMember", valueMember);

            context.CommandText = "Usp_GetOptionsByKey";
            context.CommandType = CommandType.StoredProcedure;
            return DBUtil.ExecuteMapper<Options>(context, new Options()).FirstOrDefault();
        }

        public static int Delete(string name, string valueMember)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_DeleteOptions";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Name", name);
            context.AddParameter("@ValueMember", valueMember);

            return DBUtil.ExecuteNonQuery(context);
        }

        public static Options Save(Options item)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"Usp_SaveOptions";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@Name", item.Name.Trim());
            context.AddParameter("@ValueMember", item.ValueMember.Trim());
            context.AddParameter("@CreatedBy", item.CreatedBy);
            context.AddParameter("@DisplayMember", item.DisplayMember);
            context.AddParameter("@Description", item.Description);
            int result = DBUtil.ExecuteNonQuery(context);
            if (result > 0)
            {
                return GetOptionsByKey(item.Name, item.ValueMember);
            }

            return null;
        }
    }
}
