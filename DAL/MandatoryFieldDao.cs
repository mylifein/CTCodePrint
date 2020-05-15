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
    public class MandatoryFieldDao
    {

        /// <summary>
        /// 保存必填字段信息
        /// </summary>
        /// <param name="mandatoryInfo"></param>
        /// <returns></returns>
        public bool saveMandatoryInfo(MandatoryInfo mandatoryInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_mandatory_info (uuid,man_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@manDesc,@opUser,@createTime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@manDesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createTime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = mandatoryInfo.Uuid;
            parameters[1].Value = mandatoryInfo.Mandesc;
            parameters[2].Value = mandatoryInfo.Opuser;
            parameters[3].Value = mandatoryInfo.Createtime;

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
        /// 根據UUID查詢
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public MandatoryInfo queryMenuInfoById(string uuid)
        {
            MandatoryInfo mandatoryInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,man_no,man_desc,op_user,create_time FROM t_mandatory_info where uuid=@uuid and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mandatoryInfo = new MandatoryInfo();
                mandatoryInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                mandatoryInfo.Manno = ds.Tables[0].Rows[0]["man_no"].ToString();
                mandatoryInfo.Mandesc = ds.Tables[0].Rows[0]["man_desc"].ToString();
                mandatoryInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                mandatoryInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return mandatoryInfo;
        }

        /// <summary>
        /// 根據manNo模糊查詢
        /// </summary>
        /// <param name="manNo"></param>
        /// <returns></returns>
        public List<MandatoryInfo> queryMandatoryInfoList(string manNo)
        {

            List<MandatoryInfo> mandatoryInfoList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,man_no,man_desc,op_user,create_time FROM t_mandatory_info WHERE man_no LIKE @manNo AND del_flag is null order by create_time");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + manNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mandatoryInfoList = new List<MandatoryInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MandatoryInfo mandatoryInfo = new MandatoryInfo();
                    mandatoryInfo.Uuid = dr["uuid"].ToString();
                    mandatoryInfo.Manno = dr["man_no"].ToString();
                    mandatoryInfo.Mandesc = dr["man_desc"].ToString();
                    mandatoryInfo.Opuser = dr["op_user"].ToString();
                    mandatoryInfo.Createtime = dr["create_time"].ToString();
                    mandatoryInfoList.Add(mandatoryInfo);
                }

            }
            return mandatoryInfoList;
        }


        public MandatoryInfo queryMandatoryInfoByNo(string manNo)
        {
            MandatoryInfo mandatoryInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,man_no,man_desc,op_user,create_time FROM t_mandatory_info WHERE man_no=@manNo AND del_flag is null ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = manNo ;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                    mandatoryInfo = new MandatoryInfo();
                    mandatoryInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                    mandatoryInfo.Manno = ds.Tables[0].Rows[0]["man_no"].ToString();
                    mandatoryInfo.Mandesc = ds.Tables[0].Rows[0]["man_desc"].ToString();
                    mandatoryInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                    mandatoryInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return mandatoryInfo;
        }
    }
}
