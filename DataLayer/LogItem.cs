
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Net;

namespace DataLayer
{
    public class LogItem
    {

        public static int Insert(string ComputerName
             , string IPAddress
             , string LogType
             , string LogMessage
             , string Username)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_InsertLog";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ComputerName", ComputerName);
            context.AddParameter("@IPAddress", IPAddress);
            context.AddParameter("@LogType", LogType);
            context.AddParameter("@LogMessage", LogMessage);
            context.AddParameter("@Username", Username);
            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Error(Exception ex)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = "Usp_InsertLog";
            context.CommandType = CommandType.StoredProcedure;
            context.AddParameter("@ComputerName", GetComputerName());
            context.AddParameter("@IPAddress", GetIpAddress());
            context.AddParameter("@LogType", "ERROR");
            context.AddParameter("@LogMessage", ex.ToString());
            context.AddParameter("@Username", "System");
            return DBUtil.ExecuteNonQuery(context);
        }

        public static string Crop(string text, int length)
        {
            if (text == null) text = string.Empty;
            if (text.Length > length)
            {
                text = text.Substring(0, length);
            }
            return text;
        }

        public static string GetComputerName()
        {
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            return hostEntry.HostName;
        }

        public static string GetIpAddress()
        {
            string IpAddress = string.Empty;
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in hostEntry.AddressList)
            {
                if (item.AddressFamily.ToString() == "InterNetwork")
                {
                    IpAddress = item.ToString();
                }
            }

            return Crop(IpAddress, 15);
        }
    }
}
