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
    public class WoInfoDao
    {
        /// <summary>
        /// TODO 保存质检工单信息
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool saveWoInfo(WoInfo woInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_woInfo (uuid,woNo,delMatno,woQty,deptId,deptCode,classCode,completionSub,modelNo,delMatnoDesc,status,op_user,create_time,checkTimes)");
            strSql.Append("values(@uuid,@woNo,@delMatno,@woQty,@deptId,@deptCode,@classCode,@completionSub,@modelNo,@delMatnoDesc,@status,@opuser,@createtime,@checkTimes)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woQty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@deptId", MySqlDbType.VarChar, 900),
                new MySqlParameter("@deptCode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@classCode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@completionSub", MySqlDbType.VarChar, 900),
                new MySqlParameter("@modelNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatnoDesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@status", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@checkTimes", MySqlDbType.Int16, 900)
            };
            parameters[0].Value = woInfo.Uuid;
            parameters[1].Value = woInfo.WoNo;
            parameters[2].Value = woInfo.DelMatno;
            parameters[3].Value = woInfo.WoQty;
            parameters[4].Value = woInfo.DeptId;
            parameters[5].Value = woInfo.DeptCode;
            parameters[6].Value = woInfo.ClassCode;
            parameters[7].Value = woInfo.CompletionSub;
            parameters[8].Value = woInfo.ModelNo;
            parameters[9].Value = woInfo.DelMatnoDesc;
            parameters[10].Value = woInfo.Status;
            parameters[11].Value = woInfo.Opuser;
            parameters[12].Value = woInfo.Createtime;
            parameters[13].Value = woInfo.CheckTimes;
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
        /// 根據质检编号查詢质检信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public WoInfo queryWoInfoByNo(string woNo)
        {
            WoInfo woInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,woNo,delMatno,woQty,deptId,deptCode,classCode,completionSub,modelNo,delMatnoDesc,status,checkTimes,op_user,create_time FROM t_woInfo where woNo=@woNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = woNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                woInfo = new WoInfo();
                woInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                woInfo.WoNo = ds.Tables[0].Rows[0]["woNo"].ToString();
                woInfo.DelMatno = ds.Tables[0].Rows[0]["delMatno"].ToString();
                woInfo.WoQty = ds.Tables[0].Rows[0]["woQty"].ToString();
                woInfo.DeptId = ds.Tables[0].Rows[0]["deptId"].ToString();
                woInfo.DeptCode = ds.Tables[0].Rows[0]["deptCode"].ToString();
                woInfo.ClassCode = ds.Tables[0].Rows[0]["classCode"].ToString();
                woInfo.CompletionSub = ds.Tables[0].Rows[0]["completionSub"].ToString();
                woInfo.ModelNo = ds.Tables[0].Rows[0]["modelNo"].ToString();
                woInfo.DelMatnoDesc = ds.Tables[0].Rows[0]["delMatnoDesc"].ToString();
                woInfo.Status = ds.Tables[0].Rows[0]["status"].ToString();
                woInfo.CheckTimes = (int)ds.Tables[0].Rows[0]["checkTimes"];
                woInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                woInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return woInfo;
        }


        /// <summary>
        /// 根據UUID查詢工單信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public WoInfo queryWoInfoById(string uuid)
        {
            WoInfo woInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,woNo,delMatno,woQty,deptId,deptCode,classCode,completionSub,modelNo,delMatnoDesc,status,checkTimes,op_user,create_time FROM t_woInfo where uuid=@uuid and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                woInfo = new WoInfo();
                woInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                woInfo.WoNo = ds.Tables[0].Rows[0]["woNo"].ToString();
                woInfo.DelMatno = ds.Tables[0].Rows[0]["delMatno"].ToString();
                woInfo.WoQty = ds.Tables[0].Rows[0]["woQty"].ToString();
                woInfo.DeptId = ds.Tables[0].Rows[0]["deptId"].ToString();
                woInfo.DeptCode = ds.Tables[0].Rows[0]["deptCode"].ToString();
                woInfo.ClassCode = ds.Tables[0].Rows[0]["classCode"].ToString();
                woInfo.CompletionSub = ds.Tables[0].Rows[0]["completionSub"].ToString();
                woInfo.ModelNo = ds.Tables[0].Rows[0]["modelNo"].ToString();
                woInfo.DelMatnoDesc = ds.Tables[0].Rows[0]["delMatnoDesc"].ToString();
                woInfo.Status = ds.Tables[0].Rows[0]["status"].ToString();
                woInfo.CheckTimes = (int)ds.Tables[0].Rows[0]["checkTimes"];
                woInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                woInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return woInfo;
        }


        /// <summary>
        /// 查询工单信息是否保存
        /// </summary>
        /// <param name="woNo"></param>
        /// <returns></returns>
        public bool exists(string woNo)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_woInfo where woNo=@woNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = woNo;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }

        /// <summary>
        /// 更新工单状态
        /// </summary>
        /// <param name="qualityInfo"></param>
        /// <returns></returns>
        public bool updateWoInfoStatus(WoInfo woInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_woInfo set status=@status,update_user=@updateUser,update_time=@updateTime where woNo=@woNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@status", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateTime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = woInfo.Status;
            parameters[1].Value = woInfo.Updateser;
            parameters[2].Value = woInfo.Updatetime;
            parameters[3].Value = woInfo.WoNo;
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
        /// 更新工单状态及检验次数
        /// </summary>
        /// <param name="qualityInfo"></param>
        /// <returns></returns>
        public bool updateWoInfoStatusAndTimes(WoInfo woInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_woInfo set status=@status,checkTimes=@checkTimes,update_user=@updateUser,update_time=@updateTime where woNo=@woNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@status", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateTime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@checkTimes", MySqlDbType.Int16, 900),
            };
            parameters[0].Value = woInfo.Status;
            parameters[1].Value = woInfo.Updateser;
            parameters[2].Value = woInfo.Updatetime;
            parameters[3].Value = woInfo.WoNo;
            parameters[4].Value = woInfo.CheckTimes;
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
