using DBUtility;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PrintModel
    {

        /// <summary>
        /// 根據客戶編號和出貨料號查詢打印模板號
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="delMatNo"></param>
        /// <returns></returns>
        public DataSet queryModelNo(string cusNo, string delMatNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_rel where cus_no =@cusNo and del_matno =@delMatNo ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatNo", MySqlDbType.VarChar, 900)
            };

            parameters[0].Value = cusNo.Trim();
            parameters[1].Value = delMatNo.Trim();
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 根據模板號查詢模板信息
        /// </summary>
        /// <param name="modelNo"></param>
        /// <returns></returns>
        public DataSet queryModelInfo(string modelNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_info where model_no like '%" + modelNo.Trim() + "%'");
            MySqlParameter[] parameters = {
                new MySqlParameter("@modelNo", MySqlDbType.VarChar, 900),
            };
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


        /// <summary>
        /// 根據模板號查詢必填信息
        /// </summary>
        /// <param name="manNo"></param>
        /// <returns></returns>
        public DataSet queryMandatoryInfo(string manNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mandatory_info where man_no like '%" + manNo.Trim() + "%'");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900)
            };
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 根據工單查詢CT碼信息
        /// </summary>
        /// <param name="workNo"></param>
        /// <returns></returns>
        public DataSet queryCodeInfoByWorkNo(string workNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_code_info where work_no =@workno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 根據CT碼查詢CT碼信息
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public DataSet queryCodeInfoByCT(string ctCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_code_info where ct_code =@ctCode");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ctCode", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = ctCode;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


        /// <summary>
        /// 保存生成的ctcode 数据
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public bool saveCTInfo(CTCode ctCode)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_code_info (uuid,ct_code,rule_no,work_no,cus_no,cus_po,po_qty,cus_matno,del_matno,offi_no,ver_no,wo_quantity,completed_qty,model_no,op_user,create_time)");
            strSql.Append("values(@uuid,@ctcode,@ruleno,@workno,@cusno,@cuspo,@poqty,@cusmatno,@delmatno,@offino,@verno,@woquantity,@completedQty,@model_no,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ctcode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cuspo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@offino", MySqlDbType.VarChar, 900),
                new MySqlParameter("@verno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woquantity", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@completedQty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@model_no", MySqlDbType.VarChar, 900),
                new MySqlParameter("@poqty", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = ctCode.Uuid;
            parameters[1].Value = ctCode.Ctcode;
            parameters[2].Value = ctCode.Ruleno;
            parameters[3].Value = ctCode.Workno;
            parameters[4].Value = ctCode.Cusno;
            parameters[5].Value = ctCode.Cuspo;
            parameters[6].Value = ctCode.Cusmatno;
            parameters[7].Value = ctCode.Delmatno;
            parameters[8].Value = ctCode.Offino;
            parameters[9].Value = ctCode.Verno;
            parameters[10].Value = ctCode.Woquantity;
            parameters[11].Value = ctCode.Opuser;
            parameters[12].Value = ctCode.Createtime;
            parameters[13].Value = ctCode.Completedqty;
            parameters[14].Value = ctCode.Modelno;
            parameters[15].Value = ctCode.Orderqty;
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
        /// 獲得當前規則最大的ct碼
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public string queryCodeNo(string ctCode, string delMatno)
        {
            string maxCtCode = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(ct_code) from t_code_info where ct_code like @ctCode AND del_matno=@delMatno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ctCode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + ctCode + "%"; ;
            parameters[1].Value = delMatno;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }

        /// <summary>
        /// 根据工单、PO、CT码中间码查询最大的CT码
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="delMatno"></param>
        /// <returns></returns>
        public string queryInspurCodeNo(string ctCode, string workNo, String cusPo)
        {
            string maxCtCode = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(ct_code) from t_code_info where ct_code like @ctCode AND work_no=@workNo AND cus_po=@cusPo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ctCode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusPo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + ctCode + "%";
            parameters[1].Value = workNo;
            parameters[2].Value = cusPo;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }

        /// <summary>
        /// 根据客户PO 和CT 模糊查询最大的CT码
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="cusPo"></param>
        /// <returns></returns>
        public string queryInspurMaxCode(string ctCode, String cusNo)
        {
            string maxCtCode = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(ct_code) from t_code_info where ct_code like @ctCode AND cus_no=@cusNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ctCode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + ctCode + "%";
            parameters[1].Value = cusNo;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }

        /// <summary>
        /// 獲得生成的CT碼數量
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public string getCTCount(string workno)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_code_info where work_no =@workno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workno;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }
            return countNo;
        }

        /// <summary>
        /// 根據子階料號查詢CT數
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        public string getCTCountBySubMat(string workno, string subMaterial)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_code_info where work_no=@workno and del_matno=@delMatno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workno;
            parameters[1].Value = subMaterial;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }
            return countNo;
        }

        /// <summary>
        /// 根據工單和PO查詢已經產生CT數量
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="po"></param>
        /// <returns></returns>
        public string getCTCountByPO(string workno, string po)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_code_info where work_no=@workno and cus_po=@po");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@po", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workno;
            parameters[1].Value = po;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }
            return countNo;
        }


        /// <summary>
        /// 保存打印記錄
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool savePrintRecord(CTCode ctCode)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_print_record (uuid,ct_code,op_user,create_time)");
            strSql.Append("values(@uuid,@ctcode,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ctcode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = ctCode.Uuid;
            parameters[1].Value = ctCode.Ctcode;
            parameters[2].Value = ctCode.Opuser;
            parameters[3].Value = ctCode.Createtime;
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

        /// <summary>
        /// 保存規則信息
        /// </summary>
        /// <param name="codeRule"></param>
        /// <returns></returns>
        public bool saveRuleInfo(CodeRule codeRule)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_rule_info (uuid,rule_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@ruledesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruledesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
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
        /// 根據uuid查詢規則號
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public string queryRuleByID(string uuid)
        {
            string ruleNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_rule_info where uuid =@uuid");
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
        /// 更新上传文件
        /// </summary>
        /// <param name="modelFile"></param>
        /// <returns></returns>
        public bool updateModelFile(ModelFile modelFile)
        {
            bool updateMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_model_file set fileName=@fileName,fileDescription=@fileDescription,fileAddress=@fileAddress,update_user=@updateUser,update_time=@updateTime where file_no=@fileNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@fileName", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileDescription", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileAddress", MySqlDbType.LongBlob, 900),
                new MySqlParameter("@updateUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateTime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900)

            };
            parameters[0].Value = modelFile.Filename;
            parameters[1].Value = modelFile.Filedescription;
            parameters[2].Value = modelFile.Fileaddress;
            parameters[3].Value = modelFile.Updateuser;
            parameters[4].Value = modelFile.Updatetime;
            parameters[5].Value = modelFile.Fileno;
            int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                updateMark = true;
            }
            else
            {
                updateMark = false;
            }
            return updateMark;
        }


        /// <summary>
        /// 根據規則號模糊查詢
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public DataSet queryRules(string ruleNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_rule_info where rule_no like @ruleNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ruleNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + ruleNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 根據規則號精確查詢
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public DataSet queryRule(string ruleNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_rule_info where rule_no=@ruleNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ruleNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = ruleNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 將客戶幾種綁定編碼規則
        /// </summary>
        /// <param name="cusRule"></param>
        /// <returns></returns>
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
        /// 保存打印模板必填字段
        /// </summary>
        /// <param name="mandaInfo"></param>
        /// <returns></returns>
        public bool saveMandatory(MandatoryInfo mandaInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_mandatory_info (uuid,man_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@mandesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@mandesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = mandaInfo.Uuid;
            parameters[1].Value = mandaInfo.Mandesc;
            parameters[2].Value = mandaInfo.Opuser;
            parameters[3].Value = mandaInfo.Createtime;
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
        /// 查詢打印字段規則
        /// </summary>
        /// <param name="manNo"></param>
        /// <returns></returns>
        public DataSet queryMandatory(string manNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mandatory_info where man_no like @manNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + manNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 根據uuid查詢必填字段
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public DataSet queryMandatoryById(string uuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mandatory_info where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public bool saveModelInfo(ModelInfo modelInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_model_info (uuid,model_name,model_desc,man_no,op_user,create_time)");
            strSql.Append("values(@uuid,@modelname,@modeldesc,@manno,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@modelname", MySqlDbType.VarChar, 900),
                new MySqlParameter("@modeldesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@manno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = modelInfo.Uuid;
            parameters[1].Value = modelInfo.Modelname;
            parameters[2].Value = modelInfo.Modeldesc;
            parameters[3].Value = modelInfo.Manno;
            parameters[4].Value = modelInfo.Opuser;
            parameters[5].Value = modelInfo.Createtime;
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
        /// 根據uuid查詢打印模板信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public DataSet queryModelById(string uuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_info where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 根據modelno模糊查詢
        /// </summary>
        /// <param name="modelno"></param>
        /// <returns></returns>
        public DataSet queryModelByModelNo(string modelno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_info where model_no like @modelno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@modelno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + modelno + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


        /// <summary>
        /// 保存機種類型信息
        /// </summary>
        /// <param name="mactypeinfo"></param>
        /// <returns></returns>
        public bool saveMacTypeInfo(MacTypeInfo mactypeinfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_mactype_info (uuid,mactypename,mactypedesc,rule_no,op_user,create_time)");
            strSql.Append("values(@uuid,@mactypename,@mactypedesc,@ruleno,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@mactypename", MySqlDbType.VarChar, 900),
                new MySqlParameter("@mactypedesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = mactypeinfo.Uuid;
            parameters[1].Value = mactypeinfo.Mactypename;
            parameters[2].Value = mactypeinfo.Mactypedesc;
            parameters[3].Value = mactypeinfo.Ruleno;
            parameters[4].Value = mactypeinfo.Opuser;
            parameters[5].Value = mactypeinfo.Createtime;
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
        /// 根據UUID 查詢模板信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public DataSet queryMacTypeById(string uuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mactype_info where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }



        /// <summary>
        /// 保存打印模板關係信息
        /// </summary>
        /// <param name="modelRelation"></param>
        /// <returns></returns>
        public bool saveModelRelation(ModelRelation modelRelation)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_model_rel (uuid,model_no,cus_no,del_matno,op_user,create_time)");
            strSql.Append("values(@uuid,@modelno,@cusno,@delmatno,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@modelno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = modelRelation.Uuid;
            parameters[1].Value = modelRelation.Modelno;
            parameters[2].Value = modelRelation.Cusno;
            parameters[3].Value = modelRelation.Delmatno;
            parameters[4].Value = modelRelation.Opuser;
            parameters[5].Value = modelRelation.Createtime;
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
        /// 根據客戶編號和出貨料號判斷是否已經定義打印模板
        /// </summary>
        /// <param name="cusno"></param>
        /// <param name="delmatno"></param>
        /// <returns></returns>
        public string getModelRelationCount(string cusno, string delmatno)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_model_rel where cus_no=@cusno and del_matno=@delmatno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cusno;
            parameters[1].Value = delmatno;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }
            return countNo;
        }

        /// <summary>
        /// 保存打印模板
        /// </summary>
        /// <param name="modelFile"></param>
        /// <returns></returns>
        public bool saveModelFile(ModelFile modelFile)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_model_file (uuid,fileName,fileDescription,fileAddress,op_user,create_time)");
            strSql.Append("values(@uuid,@fileName,@fileDescription,@fileAddress,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileName", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileDescription", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileAddress", MySqlDbType.LongBlob, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = modelFile.Uuid;
            parameters[1].Value = modelFile.Filename;
            parameters[2].Value = modelFile.Filedescription;
            parameters[3].Value = modelFile.Fileaddress;
            parameters[4].Value = modelFile.Opuser;
            parameters[5].Value = modelFile.Createtime;
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
        /// 通過uuid查詢保存的模板
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public DataSet queryModelFile(string uuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_file where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 通過模板號查詢信息
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public DataSet queryModelFileByNo(string fileNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_file where file_no like @fileNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + fileNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        public DataSet queryModelFileByExactNo(string fileNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_file where file_no=@fileNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = fileNo ;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


    }
}
