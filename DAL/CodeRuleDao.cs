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
    public class CodeRuleDao
    {

        /// <summary>
        /// TODO 保存編碼規則信息
        /// </summary>
        /// <param name="codeRule"></param>
        /// <returns></returns>
        public bool saveCodeRule(CodeRule codeRule)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_rule_info (uuid,rule_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@ruleDesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleDesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = codeRule.Uuid;
            parameters[1].Value = codeRule.RuleDesc;
            parameters[2].Value = codeRule.Opuser;
            parameters[3].Value = codeRule.Createtime;
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
        /// TODO 模糊查詢編碼規則列表信息
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public List<CodeRule> queryRulesByType(string ruleNo)
        {
            List<CodeRule> codeRuleList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,rule_no,rule_desc,op_user,create_time,update_user,update_time FROM t_rule_info WHERE rule_no like @ruleNo AND del_flag is null ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ruleNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + ruleNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                codeRuleList = new List<CodeRule>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CodeRule codeRule = new CodeRule();
                    codeRule.Uuid = dr["uuid"].ToString();
                    codeRule.Ruleno = dr["rule_no"].ToString();
                    codeRule.RuleDesc = dr["rule_desc"].ToString();
                    codeRule.Opuser = dr["op_user"].ToString();
                    codeRule.Createtime = dr["create_time"].ToString();
                    codeRule.UpdateUser = dr["update_user"].ToString();
                    codeRule.Updatetime = dr["update_time"].ToString();
                    codeRuleList.Add(codeRule);
                }

            }
            return codeRuleList;
        }


        /// <summary>
        /// TODO 查詢規則編號，查詢詳細信息
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public CodeRule queryRuleById(string ruleNo)
        {
            CodeRule codeRule = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,rule_no,rule_desc,op_user,create_time,update_user,update_time FROM t_rule_info where rule_no=@ruleNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ruleNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = ruleNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                codeRule = new CodeRule();
                codeRule.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                codeRule.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                codeRule.RuleDesc = ds.Tables[0].Rows[0]["rule_desc"].ToString();
                codeRule.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                codeRule.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                codeRule.UpdateUser = ds.Tables[0].Rows[0]["update_user"].ToString();
                codeRule.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            if (codeRule != null)
            {
                List<RuleItem> ruleItems = null;
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("SELECT uuid,rule_no,seq_no,rule_type,rule_value,rule_length,op_user,create_time,update_user,update_time FROM t_code_rule WHERE rule_no = @ruleNo AND del_flag is null ORDER BY seq_no ASC");
                MySqlParameter[] parameters1 = {
                new MySqlParameter("@ruleNo", MySqlDbType.VarChar, 900),
                };
                parameters1[0].Value = ruleNo;
                DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql1.ToString(), parameters1);
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    ruleItems = new List<RuleItem>();
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        RuleItem ruleItem = new RuleItem();
                        ruleItem.Uuid = dr["uuid"].ToString();
                        ruleItem.Ruleno = dr["rule_no"].ToString();
                        ruleItem.Seqno = dr["seq_no"].ToString();
                        ruleItem.Ruletype = dr["rule_type"].ToString();
                        ruleItem.Rulevalue = dr["rule_value"].ToString();
                        ruleItem.Rulelength = int.Parse(dr["rule_length"].ToString());
                        ruleItem.Opuser = dr["op_user"].ToString();
                        ruleItem.Createtime = dr["create_time"].ToString();
                        ruleItem.UpdateUser = dr["update_user"].ToString();
                        ruleItem.Updatetime = dr["update_time"].ToString();
                        ruleItems.Add(ruleItem);
                    }

                }
                codeRule.RuleItem = ruleItems;
            }
            return codeRule;
        }


        public List<RuleType> queryAllRuleType()
        {
            List<RuleType> ruleTypes = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,type_no, type_desc,op_user,create_time,update_user,update_time FROM t_rule_type where del_flag is null ORDER BY type_no ASC ");
            MySqlParameter[] parameters = {

            };
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ruleTypes = new List<RuleType>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RuleType ruleType = new RuleType();
                    ruleType.Uuid = dr["uuid"].ToString();
                    ruleType.TypeNo = dr["type_no"].ToString();
                    ruleType.TypeDesc = dr["type_desc"].ToString();
                    ruleType.Opuser = dr["op_user"].ToString();
                    ruleType.Createtime = dr["create_time"].ToString();
                    ruleType.UpdateUser = dr["update_user"].ToString();
                    ruleType.Updatetime = dr["update_time"].ToString();
                    ruleTypes.Add(ruleType);
                }

            }

            return ruleTypes;
        }


        public string queryRuleByID(string uuid)
        {
            string ruleNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT rule_no FROM t_rule_info where uuid =@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ruleNo = ds.Tables[0].Rows[0]["rule_no"].ToString();
            }
            return ruleNo;
        }


        /// <summary>
        /// 保存規則條目
        /// </summary>
        /// <param name="ruleItem"></param>
        /// <returns></returns>
        public bool saveSaveRuleItem(RuleItem ruleItem)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_code_rule (uuid,rule_no,seq_no,rule_type,rule_value,rule_length,op_user,create_time)");
            strSql.Append("values(@uuid,@ruleno,@seqno,@ruletype,@rulevalue,@rulelength,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@seqno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruletype", MySqlDbType.VarChar, 900),
                new MySqlParameter("@rulevalue", MySqlDbType.VarChar, 900),
                new MySqlParameter("@rulelength", MySqlDbType.Int16, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = ruleItem.Uuid;
            parameters[1].Value = ruleItem.Ruleno;
            parameters[2].Value = ruleItem.Seqno;
            parameters[3].Value = ruleItem.Ruletype;
            parameters[4].Value = ruleItem.Rulevalue;
            parameters[5].Value = ruleItem.Rulelength;
            parameters[6].Value = ruleItem.Opuser;
            parameters[7].Value = ruleItem.Createtime;
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

    }
}
