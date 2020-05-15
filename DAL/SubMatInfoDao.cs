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
    public class SubMatInfoDao
    {

        public bool saveSubMatInfoDao(SubMatInfo subMatInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_submat_info (uuid,del_matno,sub_matno,op_user,create_time)");
            strSql.Append("values(@uuid,@delmatno,@submatno,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@submatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = subMatInfo.Uuid;
            parameters[1].Value = subMatInfo.Delmatno;
            parameters[2].Value = subMatInfo.Submatno;
            parameters[3].Value = subMatInfo.Opuser;
            parameters[4].Value = subMatInfo.Createtime;
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

        public SubMatInfo querySubMatInfoById(string uuid)
        {
            SubMatInfo subMatInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,del_matno,sub_matno,op_user,create_time FROM t_submat_info where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                subMatInfo = new SubMatInfo();
                subMatInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                subMatInfo.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                subMatInfo.Submatno = ds.Tables[0].Rows[0]["sub_matno"].ToString();
                subMatInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                subMatInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return subMatInfo;
        }



        public List<SubMatInfo> querySubMatInfoList(string delmatno)
        {

            List<SubMatInfo> subMatInfoList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,del_matno,sub_matno,op_user,create_time FROM t_submat_info WHERE del_matno=@delmatno AND del_flag is null ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = delmatno;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                subMatInfoList = new List<SubMatInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SubMatInfo subMatInfo = new SubMatInfo();
                    subMatInfo.Uuid = dr["uuid"].ToString();
                    subMatInfo.Delmatno = dr["del_matno"].ToString();
                    subMatInfo.Submatno = dr["sub_matno"].ToString();
                    subMatInfo.Opuser = dr["op_user"].ToString();
                    subMatInfo.Createtime = dr["create_time"].ToString();
                    subMatInfoList.Add(subMatInfo);
                }

            }
            return subMatInfoList;
        }

        public bool exists(SubMatInfo subMatInfo)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_submat_info where del_matno=@delmatno and sub_matno=@submatno and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@submatno", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = subMatInfo.Delmatno;
            parameters[1].Value = subMatInfo.Submatno;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }





    }
}
