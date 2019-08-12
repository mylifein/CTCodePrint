using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public static class Auxiliary
    {
        public static string loginName;

        public static string RoleNo;
        public static string Get_SQLConnStr()
        {
            string sqlConnStr = ConfigurationManager.ConnectionStrings["ConnectionMySqlString"].ConnectionString.ToString();
            return sqlConnStr;
        }

        public static string Get_OracleSQLConnStr()
        {
            string sqlConnStr = ConfigurationManager.ConnectionStrings["ConnectionOracleString"].ConnectionString.ToString();
            return sqlConnStr;
        }

        public static string Get_UUID()
        {
            string uuid = System.Guid.NewGuid().ToString("N");
            return uuid;
        }


        public static string Get_MyUserName()
        {
            string username = ConfigurationManager.AppSettings["MyUserName"].ToString().Trim();             
            return username.Trim();
        }

        public static string Get_MyPassword()
        {
            string password = ConfigurationManager.AppSettings["MyPassword"].ToString().Trim();
            return password;
        }

        public static string Get_CurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


    }
}
