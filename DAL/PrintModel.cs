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
        /// 獲得當前規則最大的ct碼
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public string queryCodeNo(string ctCode)
        {
            string maxCtCode = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(ct_code) from t_code_info where ct_code like @ctCode");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ctCode", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + ctCode + "%"; ;
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
