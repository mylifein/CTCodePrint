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
            strSql.Append("select count(1) from t_cus_codrule where cus_no=@cusNo and del_matno=@delMatno and bound_type=@boundType and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cusRule.Cusno;
            parameters[1].Value = cusRule.Delmatno;
            parameters[2].Value = cusRule.Boundtype;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }


        public bool saveRuleRelation(CusRule cusRule)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_cus_codrule (uuid,cus_no,del_matno,op_user,create_time,bound_type,rule_no)");
            strSql.Append("values(@uuid,@cusno,@delmatno,@opuser,@createtime,@boundType,@ruleno)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cusRule.Uuid;
            parameters[1].Value = cusRule.Cusno;
            parameters[2].Value = cusRule.Delmatno;
            parameters[3].Value = cusRule.Opuser;
            parameters[4].Value = cusRule.Createtime;
            parameters[5].Value = cusRule.Boundtype;
            parameters[6].Value = cusRule.Ruleno;
            int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                saveMark = true;
            }
            else
            {
                saveMark = false;
            }
            return saveMark;
        }


        /// <summary>
        /// 獲得機種類型 通過客戶編號
        /// </summary>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public CusRule queryCusRuleByConds(string cusNo, string delmatno, string boundType)
        {

            CusRule cusRule = null; 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select uuid,cus_no,del_matno,rule_no,bound_type,op_user,create_time,update_user,update_time from t_cus_codrule where cus_no=@cusNo and del_matno=@delmatno and bound_type=@BoundType and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@BoundType", MySqlDbType.VarChar, 900)
            };

            parameters[0].Value = cusNo;
            parameters[1].Value = delmatno;
            parameters[2].Value = boundType;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cusRule = new CusRule();
                cusRule.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                cusRule.Cusno = ds.Tables[0].Rows[0]["cus_no"].ToString();
                cusRule.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                cusRule.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                cusRule.Boundtype = ds.Tables[0].Rows[0]["bound_type"].ToString();
                cusRule.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                cusRule.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                cusRule.Updateuser = ds.Tables[0].Rows[0]["update_user"].ToString();
                cusRule.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return cusRule;
        }
    }
}
