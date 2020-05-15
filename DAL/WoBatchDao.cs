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
    public class WoBatchDao
    {
        public bool saveWoBatch(WoBatch woBatch)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_wo_batchno (uuid,work_no,batchNo,batchType,op_user,create_time)");
            strSql.Append("values(@uuid,@workno,@batchNo,@batchType,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@batchNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@batchType", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = woBatch.Uuid;
            parameters[1].Value = woBatch.Workno;
            parameters[2].Value = woBatch.BatchNo;
            parameters[3].Value = woBatch.BatchType;
            parameters[4].Value = woBatch.Opuser;
            parameters[5].Value = woBatch.Createtime;
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
        /// 根据工单号查询装箱单批次.
        /// </summary>
        /// <param name="workNo"></param>
        /// <returns></returns>
        public string getBatchNoByWO(string workNo)
        {
            string batchNo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select batchNo from t_wo_batchno where work_no=@workNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workNo; ;
            Object batchNoObj = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (batchNoObj != null && batchNoObj != DBNull.Value)
            {
                batchNo = batchNoObj.ToString();
            }
            return batchNo;
        }


        /// <summary>
        /// 根据浪潮日期码模糊查询 浪潮批次号
        /// </summary>
        /// <param name="batchCond"></param>
        /// <returns></returns>
        public List<string> queryBatchNos(string batchCond)
        {
            List<string> batchNos = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  batchNo FROM t_wo_batchno where batchNo like @batchNo and del_flag is null order by create_time");
            MySqlParameter[] parameters = {
                new MySqlParameter("@batchNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + batchCond + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                batchNos = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    batchNos.Add(dr["batchNo"].ToString());
                }
            }
            return batchNos;
        }
    }
}
