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
    public class FileRelDelDao
    {
        public bool saveFileRelDel(FileRelDel fileRelDel)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_file_reldel (uuid,file_no,cus_no,del_matno,op_user,create_time,bound_type)");
            strSql.Append("values(@uuid,@fileNo,@cusNo,@delMatno,@opUser,@createTime,@boundType)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createTime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = fileRelDel.Uuid;
            parameters[1].Value = fileRelDel.FileNo;
            parameters[2].Value = fileRelDel.CusNo;
            parameters[3].Value = fileRelDel.DelMatno;
            parameters[4].Value = fileRelDel.OpUser;
            parameters[5].Value = fileRelDel.CreateTime;
            parameters[6].Value = fileRelDel.BoundType;
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

        public FileRelDel queryFileRelDelById(string uuid)
        {
            FileRelDel fileRelDel = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,file_no,cus_no,del_matno,op_user,create_time FROM t_file_reldel where uuid=@uuid and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                fileRelDel = new FileRelDel();
                fileRelDel.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                fileRelDel.FileNo = ds.Tables[0].Rows[0]["file_no"].ToString();
                fileRelDel.CusNo = ds.Tables[0].Rows[0]["cus_no"].ToString();
                fileRelDel.DelMatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                fileRelDel.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                fileRelDel.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return fileRelDel;
        }

        public bool exists(FileRelDel fileRelDel)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_file_reldel where cus_no=@CusNo and del_matno=@delMatno and bound_type=@boundType and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@CusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                  new MySqlParameter("@boundType", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = fileRelDel.CusNo;
            parameters[1].Value = fileRelDel.DelMatno;
            parameters[2].Value = fileRelDel.BoundType;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }

        /// <summary>
        /// 根據客戶和出貨料號查詢模板
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="delMatNo"></param>
        /// <returns></returns>
        public FileRelDel queryFileRelDelCusNo(string cusNo, string delMatNo,string boundType)
        {
            FileRelDel fileRelDel = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,file_no,cus_no,del_matno,op_user,create_time FROM t_file_reldel where cus_no=@cusNo and del_matno=@delMatNo and bound_type=@boundType and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cusNo;
            parameters[1].Value = delMatNo;
            parameters[2].Value = boundType;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                fileRelDel = new FileRelDel();
                fileRelDel.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                fileRelDel.FileNo = ds.Tables[0].Rows[0]["file_no"].ToString();
                fileRelDel.CusNo = ds.Tables[0].Rows[0]["cus_no"].ToString();
                fileRelDel.DelMatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                fileRelDel.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                fileRelDel.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return fileRelDel;
        }
    }
}
