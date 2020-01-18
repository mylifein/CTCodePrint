using DBUtility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MacTypeDao
    {
        public DataSet queryDepartmentById(string typeNo)
        {
      
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mactype_info where mactypeno=@typeNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@typeNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = typeNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

    }
}
