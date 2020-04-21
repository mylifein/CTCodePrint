using DBUtility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SelectControl
    {


        public DataSet getSelectControl()
        {
            String sql = "select * from t_cus_info";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString,CommandType.Text,sql);
            return ds;
        }

        /// <summary>
        /// 通过客户编号和机种号获得编码规则号
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="macNo"></param>
        /// <returns></returns>
        public DataSet getRuleByCus(string macType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_mactype_info where mactypeno=@macType");
            MySqlParameter[] parameters = {
                new MySqlParameter("@macType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = macType;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text,strSql.ToString(),parameters);

            return ds;
        }




        /// <summary>
        /// 根据规则号，获得客户编码规则
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public DataSet getRuleInfo(string ruleNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_code_rule where rule_no=@ruleNo AND del_flag is null ORDER BY seq_no ASC");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ruleNo", MySqlDbType.VarChar, 900)
            };

            parameters[0].Value = ruleNo.Trim();
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


        /// <summary>
        /// 獲得所有規則類型
        /// </summary>
        /// <returns></returns>
        public DataSet getRuleTypes()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_rule_type ORDER BY type_no ASC");
            MySqlParameter[] parameters = {
                
            };

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }



        public DataSet getRulesByRuleNo(string ruleNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_code_rule where rule_no=@ruleNo ORDER BY seq_no ASC");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ruleNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = ruleNo.Trim();
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


    }
}
