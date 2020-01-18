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
        /// 獲得機種類型 通過客戶編號
        /// </summary>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public DataSet getMacTypeByCus(string cusNo,string delmatno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_cus_codrule where cus_no=@cusNo and del_matno=@delmatno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900)
            };

            parameters[0].Value = cusNo;
            parameters[1].Value = delmatno;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

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

        /// <summary>
        /// 通過客戶編號搜索客戶
        /// </summary>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public DataSet getCusInfoByCusNo(string cusNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_cus_info where cus_no like '%"+ cusNo.Trim() +"%'");
            MySqlParameter[] parameters = {
                new MySqlParameter("@CusNo", MySqlDbType.VarChar, 900)
            };
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 通過客戶名搜索客戶
        /// </summary>
        /// <param name="cusName"></param>
        /// <returns></returns>
        public DataSet getCusInfoByCusName(string cusName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_cus_info where cus_name like '%" + cusName.Trim() + "%'");
            MySqlParameter[] parameters = {
                new MySqlParameter("@CusName", MySqlDbType.VarChar, 900)
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
