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
    public class PalletDao
    {

        /// <summary>
        /// 保存栈板标签
        /// </summary>
        /// <param name="pallet"></param>
        /// <returns></returns>
        public bool savePallet(Pallet pallet)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_pallet_info (uuid,palletNo,capacity_no,model_no,work_no,rule_no,op_user,create_time,palletQty,batchNo)");
            strSql.Append("values(@uuid,@palletNo,@capacityNo,@model_no,@woNo,@ruleno,@opuser,@createtime,@palletQty,@batchNo)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@model_no", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@palletQty", MySqlDbType.Int32, 900),
                new MySqlParameter("@batchNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = pallet.Uuid;
            parameters[1].Value = pallet.PalletNo;
            parameters[2].Value = pallet.CapacityNo;
            parameters[3].Value = pallet.Modelno;
            parameters[4].Value = pallet.Ruleno;
            parameters[5].Value = pallet.Opuser;
            parameters[6].Value = pallet.Createtime;
            parameters[7].Value = pallet.WoNo;
            parameters[8].Value = pallet.PalletQty;
            parameters[9].Value = pallet.BatchNo;
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
        ///  保存装箱单与栈板关系
        /// </summary>
        /// <param name="cartonRelPallet"></param>
        /// <returns></returns>
        public bool savePalletRelation(CartonRelPallet cartonRelPallet)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_carton_pallet (uuid,cartonNo,palletNo,op_user,create_time)");
            strSql.Append("values(@uuid,@cartonNo,@palletNo,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cartonRelPallet.Uuid;
            parameters[1].Value = cartonRelPallet.CartonNo;
            parameters[2].Value = cartonRelPallet.PalletNo;
            parameters[3].Value = cartonRelPallet.Opuser;
            parameters[4].Value = cartonRelPallet.Createtime;
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
        /// 查詢最大流水碼
        /// </summary>
        /// <param name="prefPalletNo"></param>
        /// <returns></returns>
        public string queryMaxPalletNo(string prefPalletNo)
        {
            string maxCtCode = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(palletNo) from t_pallet_info where palletNo like @palletNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + prefPalletNo + "%"; ;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }


        /// <summary>
        /// 根據棧板編號前綴查詢 棧板信息
        /// </summary>
        /// <param name="prefixPallet"></param>
        /// <returns></returns>
        public Pallet queryPalletByPrefix(string prefixPallet)
        {
            Pallet pallet = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_pallet_info where palletNo like @palletNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + prefixPallet + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                pallet = new Pallet();
                pallet.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                pallet.PalletNo = ds.Tables[0].Rows[0]["palletNo"].ToString();
                pallet.WoNo = ds.Tables[0].Rows[0]["work_no"].ToString();
                pallet.CapacityNo = ds.Tables[0].Rows[0]["capacity_no"].ToString();
                pallet.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                pallet.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                pallet.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                pallet.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                pallet.PalletQty = (int)ds.Tables[0].Rows[0]["palletQty"];
                pallet.BatchNo = ds.Tables[0].Rows[0]["batchNo"].ToString();
            }
            return pallet;
        }
    }
}
