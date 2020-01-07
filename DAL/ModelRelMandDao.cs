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
    public class ModelRelMandDao
    {
        public bool saveModelRelMand(ModelRelMand modelRelMand)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_model_relMand (uuid,file_no,man_no,op_user,create_time)");
            strSql.Append("values(@uuid,@fileNo,@manNo,@opUser,@createTime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createTime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = modelRelMand.Uuid;
            parameters[1].Value = modelRelMand.FileNo;
            parameters[2].Value = modelRelMand.ManNo;
            parameters[3].Value = modelRelMand.OpUser;
            parameters[4].Value = modelRelMand.CreateTime;
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

        public ModelRelMand queryMenuInfoById(string uuid)
        {
            ModelRelMand modelRelMand = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_relMand where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelRelMand = new ModelRelMand();
                modelRelMand.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                modelRelMand.FileNo = ds.Tables[0].Rows[0]["file_no"].ToString();
                modelRelMand.ManNo = ds.Tables[0].Rows[0]["man_no"].ToString();
                modelRelMand.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                modelRelMand.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return modelRelMand;
        }

        public ModelRelMand queryMenuInfoByFileNo(string fileNo)
        {
            ModelRelMand modelRelMand = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_relMand where file_no=@fileNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = fileNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelRelMand = new ModelRelMand();
                modelRelMand.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                modelRelMand.FileNo = ds.Tables[0].Rows[0]["file_no"].ToString();
                modelRelMand.ManNo = ds.Tables[0].Rows[0]["man_no"].ToString();
                modelRelMand.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                modelRelMand.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return modelRelMand;
        }

        public bool exists(string fileNo)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_model_relMand where file_no=@fileNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = fileNo;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }

    }
}
