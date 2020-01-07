using DBUtility;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CusRuleDao
    {

        public bool exists(CusRule cusRule)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_cus_codrule where cus_no=@cusNo and del_matno=@delMatno and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cusRule.Cusno;
            parameters[1].Value = cusRule.Delmatno;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }
    }
}
