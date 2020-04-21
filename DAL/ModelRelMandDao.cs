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
    public class MandRelDelDao
    {
        public bool saveMandRelDel(MandRelDel mandRelDel)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_mand_reldel (uuid,man_no,cus_no,del_matno,bound_type,op_user,create_time)");
            strSql.Append("values(@uuid,@manNo,@cusNo,@delMatno,@boundType,@opUser,@createTime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createTime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = mandRelDel.Uuid;
            parameters[1].Value = mandRelDel.ManNo;
            parameters[2].Value = mandRelDel.CusNo;
            parameters[3].Value = mandRelDel.DelMatno;
            parameters[4].Value = mandRelDel.BoundType;
            parameters[5].Value = mandRelDel.OpUser;
            parameters[6].Value = mandRelDel.CreateTime;
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

        public MandRelDel queryManNoByDel(MandRelDel mandRelDel)
        {
            MandRelDel reMandRelDel = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mand_reldel where cus_no=@cusNo and del_matno=@delMatno and bound_type=@boundType and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = mandRelDel.CusNo;
            parameters[1].Value = mandRelDel.DelMatno;
            parameters[2].Value = mandRelDel.BoundType;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                reMandRelDel = new MandRelDel();
                reMandRelDel.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                reMandRelDel.ManNo = ds.Tables[0].Rows[0]["man_no"].ToString();
                reMandRelDel.CusNo = ds.Tables[0].Rows[0]["cus_no"].ToString();
                reMandRelDel.DelMatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                reMandRelDel.BoundType = ds.Tables[0].Rows[0]["bound_type"].ToString();
                reMandRelDel.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                reMandRelDel.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
                reMandRelDel.UpdateUser = ds.Tables[0].Rows[0]["update_user"].ToString();
                reMandRelDel.UpdateTime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return reMandRelDel;
        }

        public MandRelDel queryMandRelDelById(string uuid)
        {
            MandRelDel mandRelDel = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mand_reldel where uuid=@uuid and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mandRelDel = new MandRelDel();
                mandRelDel.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                mandRelDel.ManNo = ds.Tables[0].Rows[0]["man_no"].ToString();
                mandRelDel.CusNo = ds.Tables[0].Rows[0]["cus_no"].ToString();
                mandRelDel.DelMatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                mandRelDel.BoundType = ds.Tables[0].Rows[0]["bound_type"].ToString();
                mandRelDel.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                mandRelDel.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
                mandRelDel.UpdateUser = ds.Tables[0].Rows[0]["update_user"].ToString();
                mandRelDel.UpdateTime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return mandRelDel;
        }




        /// <summary>
        /// 查找当前客户和出货料号是否绑定字段
        /// </summary>
        /// <param name="mandRelDel"></param>
        /// <returns></returns>
        public bool exists(MandRelDel mandRelDel)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_mand_reldel where cus_no=@CusNo and del_matno=@delMatno and bound_type=@boundType and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@CusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = mandRelDel.CusNo;
            parameters[1].Value = mandRelDel.DelMatno;
            parameters[2].Value = mandRelDel.BoundType;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }

    }
}
