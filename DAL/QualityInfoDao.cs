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
    public class QualityInfoDao
    {
        /// <summary>
        /// TODO 保存质量信息
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool saveQualityInfo(QualityInfo qualityInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_qualityInfo (uuid,qualiatyNo,woNo,status,op_user,create_time,deliverMan)");
            strSql.Append("values(@uuid,@qualiatyNo,@woNo,@status,@opuser,@createtime,@deliverMan)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@qualiatyNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@status", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@deliverMan", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = qualityInfo.Uuid;
            parameters[1].Value = qualityInfo.QualiatyNo;
            parameters[2].Value = qualityInfo.WoNo;
            parameters[3].Value = qualityInfo.Status;
            parameters[4].Value = qualityInfo.Opuser;
            parameters[5].Value = qualityInfo.Createtime;
            parameters[6].Value = qualityInfo.DeliverMan;
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
        /// TODO 根据质量编号模糊查询最大的质量号
        /// </summary>
        /// <param name="preQualityNo"></param>
        /// <returns></returns>
        public string queryMaxQualityNo(string preQualityNo)
        {
            string maxQualityNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(qualiatyNo) from t_qualityInfo where qualiatyNo like @qualiatyNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@qualiatyNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + preQualityNo + "%"; ;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxQualityNo = maxCode.ToString();
            }
            return maxQualityNo;
        }


        /// <summary>
        /// 根據箱號查詢裝箱單信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public QualityInfo queryQualityInfoByNo(string qualityNo)
        {
            QualityInfo qualityInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,qualiatyNo,woNo,startTime,endTime,duringTime,deliverMan,status,op_user,create_time FROM t_qualityInfo where qualiatyNo=@qualityNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@qualityNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = qualityNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                qualityInfo = new QualityInfo();
                qualityInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                qualityInfo.QualiatyNo = ds.Tables[0].Rows[0]["qualiatyNo"].ToString();
                qualityInfo.WoNo = ds.Tables[0].Rows[0]["woNo"].ToString();
                if(ds.Tables[0].Rows[0]["startTime"] != null && !(ds.Tables[0].Rows[0]["startTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.StartTime = (DateTime)ds.Tables[0].Rows[0]["startTime"];
                }
                if (ds.Tables[0].Rows[0]["endTime"] != null && !(ds.Tables[0].Rows[0]["endTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.EndTime = (DateTime)ds.Tables[0].Rows[0]["endTime"];
                }
                if (ds.Tables[0].Rows[0]["duringTime"] != null && !(ds.Tables[0].Rows[0]["duringTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.DuringTime = (long)ds.Tables[0].Rows[0]["duringTime"];
                }
                qualityInfo.DeliverMan = ds.Tables[0].Rows[0]["deliverMan"].ToString();
                qualityInfo.Status = ds.Tables[0].Rows[0]["status"].ToString();
                qualityInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                qualityInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return qualityInfo;
        }


        /// <summary>
        /// 根據UUID查詢裝箱單信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public QualityInfo queryQualityInfoById(string uuid)
        {
            QualityInfo qualityInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,qualiatyNo,woNo,startTime,endTime,duringTime,deliverMan,status,op_user,create_time FROM t_qualityInfo where uuid=@uuid and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                qualityInfo = new QualityInfo();
                qualityInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                qualityInfo.QualiatyNo = ds.Tables[0].Rows[0]["qualiatyNo"].ToString();
                qualityInfo.WoNo = ds.Tables[0].Rows[0]["woNo"].ToString();
                if (ds.Tables[0].Rows[0]["startTime"] != null && !(ds.Tables[0].Rows[0]["startTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.StartTime = (DateTime)ds.Tables[0].Rows[0]["startTime"];
                }
                if (ds.Tables[0].Rows[0]["endTime"] != null && !(ds.Tables[0].Rows[0]["endTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.EndTime = (DateTime)ds.Tables[0].Rows[0]["endTime"];
                }
                if (ds.Tables[0].Rows[0]["duringTime"] != null && !(ds.Tables[0].Rows[0]["duringTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.DuringTime = (long)ds.Tables[0].Rows[0]["duringTime"];
                }
                qualityInfo.DeliverMan = ds.Tables[0].Rows[0]["deliverMan"].ToString();
                qualityInfo.Status = ds.Tables[0].Rows[0]["status"].ToString();
                qualityInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                qualityInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return qualityInfo;
        }

        public QualityInfo queryQualityInfoByWoNo(string woNo)
        {
            QualityInfo qualityInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,qualiatyNo,woNo,startTime,endTime,duringTime,deliverMan,status,op_user,create_time FROM t_qualityInfo where woNo=@woNo and del_flag is null order by qualiatyNo desc limit 1");
            MySqlParameter[] parameters = {
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = woNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                qualityInfo = new QualityInfo();
                qualityInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                qualityInfo.QualiatyNo = ds.Tables[0].Rows[0]["qualiatyNo"].ToString();
                qualityInfo.WoNo = ds.Tables[0].Rows[0]["woNo"].ToString();
                if (ds.Tables[0].Rows[0]["startTime"] != null && !(ds.Tables[0].Rows[0]["startTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.StartTime = (DateTime)ds.Tables[0].Rows[0]["startTime"];
                }
                if (ds.Tables[0].Rows[0]["endTime"] != null && !(ds.Tables[0].Rows[0]["endTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.EndTime = (DateTime)ds.Tables[0].Rows[0]["endTime"];
                }
                if (ds.Tables[0].Rows[0]["duringTime"] != null && !(ds.Tables[0].Rows[0]["duringTime"].ToString().Trim().Equals("")))
                {
                    qualityInfo.DuringTime = (long)ds.Tables[0].Rows[0]["duringTime"];
                }
                qualityInfo.DeliverMan = ds.Tables[0].Rows[0]["deliverMan"].ToString();
                qualityInfo.Status = ds.Tables[0].Rows[0]["status"].ToString();
                qualityInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                qualityInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return qualityInfo;
        }



        /// <summary>
        /// 更新质检状态开始
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool updateStartQualityInfo(QualityInfo qualityInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_qualityInfo set startTime=@startTime,status=@status,update_user=@updateUser,update_time=@updateTime where qualiatyNo=@qualiatyNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@startTime", MySqlDbType.DateTime,600),
                new MySqlParameter("@status", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateTime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@qualiatyNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = qualityInfo.StartTime;
            parameters[1].Value = qualityInfo.Status;
            parameters[2].Value = qualityInfo.Updateser;
            parameters[3].Value = qualityInfo.Updatetime;
            parameters[4].Value = qualityInfo.QualiatyNo;
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
        /// 质检结束更新
        /// </summary>
        /// <param name="qualityInfo"></param>
        /// <returns></returns>
        public bool updateEndQualityInfo(QualityInfo qualityInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_qualityInfo set endTime=@endTime,duringTime=@duringTime,status=@status,remark=@remark,update_user=@updateUser,update_time=@updateTime where qualiatyNo=@qualiatyNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@endTime", MySqlDbType.DateTime,600),
                new MySqlParameter("@duringTime", MySqlDbType.Int32,600),
                new MySqlParameter("@status", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateTime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@qualiatyNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@remark", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = qualityInfo.EndTime;
            parameters[1].Value = qualityInfo.DuringTime;
            parameters[2].Value = qualityInfo.Status;
            parameters[3].Value = qualityInfo.Updateser;
            parameters[4].Value = qualityInfo.Updatetime;
            parameters[5].Value = qualityInfo.QualiatyNo;
            parameters[6].Value = qualityInfo.Remark;
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
