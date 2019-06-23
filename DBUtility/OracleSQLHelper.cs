using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public class OracleSQLHelper
    {
        public static readonly string ConnectionString = Auxiliary.Get_OracleSQLConnStr();

        public static OracleConnection GetDBConnection()
        {
            OracleConnection conn = new OracleConnection();

            conn.ConnectionString = ConnectionString;

            return conn;

        }

    }
}
