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
    public class CapacityDao
    {
        /// <summary>
        /// TODO 保存容量信息
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool saveCapacity(Capacity capacity)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_capacity (uuid,capacity_qty,capacity_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@capacityQty,@capacityDesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityQty", MySqlDbType.Int16, 900),
                new MySqlParameter("@capacityDesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = capacity.Uuid;
            parameters[1].Value = capacity.Capacityqty;
            parameters[2].Value = capacity.Capacitydesc;
            parameters[3].Value = capacity.Opuser;
            parameters[4].Value = capacity.Createtime;
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


        public DataSet queryCapacityAll(string capacityNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_capacity where capacity_no like @capacityNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + capacityNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        public Capacity queryCapacityById(string uuid)
        {
            Capacity capacity = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_capacity where uuid=@uuid and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                capacity = new Capacity();
                capacity.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                capacity.Capacityno = ds.Tables[0].Rows[0]["capacity_no"].ToString();
                capacity.Capacityqty = (int)ds.Tables[0].Rows[0]["capacity_qty"];
                capacity.Capacitydesc = ds.Tables[0].Rows[0]["capacity_desc"].ToString();
                capacity.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                capacity.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                capacity.Updateser = ds.Tables[0].Rows[0]["update_user"].ToString();
                capacity.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return capacity;
        }


        /// <summary>
        /// 保存容量與出貨料號關係
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool saveCapacityRelCus(CapacityRelCus capacityRelCus)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_cap_relcus (uuid,capacity_no,cus_no,del_matno,capacity_type,op_user,create_time)");
            strSql.Append("values(@uuid,@capacityNo,@cusNo,@delMatno,@capacityType,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityType", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = capacityRelCus.Uuid;
            parameters[1].Value = capacityRelCus.CapacityNo;
            parameters[2].Value = capacityRelCus.CusNo;
            parameters[3].Value = capacityRelCus.DelMatno;
            parameters[4].Value = capacityRelCus.CapacityType;
            parameters[5].Value = capacityRelCus.OpUser;
            parameters[6].Value = capacityRelCus.CreateTime;
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
        /// TODO 查詢容量 是否綁定
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public bool exists(CapacityRelCus capacityRelCus)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_cap_relcus where cus_no=@cusNo and del_matno=@delMatno and capacity_type=@capacityType and capacity_no=@capacityNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityType", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = capacityRelCus.CusNo;
            parameters[1].Value = capacityRelCus.DelMatno;
            parameters[2].Value = capacityRelCus.CapacityType;
            parameters[3].Value = capacityRelCus.CapacityNo;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }


        public string queryCapacityNo(CapacityRelCus capacityRelCus)
        {
            string result = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select capacity_no from t_cap_relcus where cus_no=@cusNo and del_matno=@delMatno and capacity_type=@capacityType and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = capacityRelCus.CusNo;
            parameters[1].Value = capacityRelCus.DelMatno;
            parameters[2].Value = capacityRelCus.CapacityType;
            Object obj = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj != null)
            {
                result = obj.ToString();
            }
            return result;
        }




        /// <summary>
        /// 根據箱號查詢裝箱單信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public Capacity queryCapacityByNo(string capacityNo)
        {
            Capacity capacity = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_capacity where capacity_no=@capacityNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = capacityNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                capacity = new Capacity();
                capacity.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                capacity.Capacityno = ds.Tables[0].Rows[0]["capacity_no"].ToString();
                capacity.Capacityqty = (int)ds.Tables[0].Rows[0]["capacity_qty"];
                capacity.Capacitydesc = ds.Tables[0].Rows[0]["capacity_desc"].ToString();
                capacity.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                capacity.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                capacity.Updateser = ds.Tables[0].Rows[0]["update_user"].ToString();
                capacity.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return capacity;
        }





    }
}
