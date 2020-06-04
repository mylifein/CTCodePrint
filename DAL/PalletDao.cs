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
            strSql.Append("insert into t_pallet_info (uuid,palletNo,work_no,palletQty,batchNo,capacity_no,model_no,rule_no,cus_no,cus_name,cus_po,so_order,op_user,create_time,cus_matno,del_matno)");
            strSql.Append("values(@uuid,@palletNo,@workNo,@palletQty,@batchNo,@capacityNo,@modelNo,@ruleno,@cusNo,@cusName,@cusPo,@soOrder,@opuser,@createtime,@cusMatno,@delMatno)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@palletQty", MySqlDbType.Int32, 900),
                new MySqlParameter("@batchNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@modelNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusName", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusPo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@soOrder", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = pallet.Uuid;
            parameters[1].Value = pallet.PalletNo;
            parameters[2].Value = pallet.Workno;
            parameters[3].Value = pallet.PalletQty;
            parameters[4].Value = pallet.BatchNo;
            parameters[5].Value = pallet.CapacityNo;
            parameters[6].Value = pallet.Modelno;
            parameters[7].Value = pallet.Ruleno;
            parameters[8].Value = pallet.Cusno;
            parameters[9].Value = pallet.Cusname;
            parameters[10].Value = pallet.Cuspo;
            parameters[11].Value = pallet.SoOrder;
            parameters[12].Value = pallet.Opuser;
            parameters[13].Value = pallet.Createtime;
            parameters[14].Value = pallet.Cusmatno;
            parameters[15].Value = pallet.Delmatno;
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



        public Pallet queryPalletByWorkno(string workNo)
        {
            Pallet pallet = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,palletNo,palletQty,batchNo,capacity_no,model_no,rule_no,cus_no,cus_matno,del_matno,cus_name,cus_po,so_order,vehicle_No,op_user,create_time,update_user,update_time FROM t_pallet_info where work_no=@workNo and del_flag is null limit 1");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = workNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                pallet = new Pallet();
                pallet.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                pallet.PalletNo = ds.Tables[0].Rows[0]["palletNo"].ToString();
                pallet.PalletQty = (int)ds.Tables[0].Rows[0]["palletQty"];
                pallet.BatchNo = ds.Tables[0].Rows[0]["batchNo"].ToString();
                pallet.CapacityNo = ds.Tables[0].Rows[0]["capacity_no"].ToString();
                pallet.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                pallet.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                pallet.Cusno = ds.Tables[0].Rows[0]["cus_no"].ToString();
                pallet.Cusname = ds.Tables[0].Rows[0]["cus_name"].ToString();
                pallet.Cuspo = ds.Tables[0].Rows[0]["cus_po"].ToString();
                pallet.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                pallet.Cusmatno = ds.Tables[0].Rows[0]["cus_matno"].ToString();
                pallet.SoOrder = ds.Tables[0].Rows[0]["so_order"].ToString();
                pallet.VehicleNo = ds.Tables[0].Rows[0]["vehicle_No"].ToString();
                pallet.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                pallet.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                pallet.Updateser = ds.Tables[0].Rows[0]["update_user"].ToString();
                pallet.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return pallet;
        }


        public string queryBatchNoWorkNo(string palletNo, string workNo)
        {
            string maxPalletNo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(palletNo) from t_pallet_info where palletNo like @palletNo AND work_no=@workNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + palletNo + "%";
            parameters[1].Value = workNo;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxPalletNo = maxCode.ToString();
            }
            return maxPalletNo;
        }



        public List<string> queryBatchNos(string batchCond)
        {
            List<string> batchNos = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT batchNo FROM t_pallet_info where batchNo like @batchNo and del_flag is null order by create_time");
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


        /// <summary>
        /// 根據棧板編號前綴查詢 棧板信息
        /// </summary>
        /// <param name="prefixPallet"></param>
        /// <returns></returns>
        public Pallet queryPalletByPrefix(string prefixPallet)
        {
            Pallet pallet = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,palletNo,palletQty,batchNo,capacity_no,model_no,rule_no,cus_no,cus_matno,del_matno,cus_name,cus_po,so_order,vehicle_No,op_user,create_time,update_user,update_time FROM t_pallet_info where palletNo like @palletNo AND del_flag is null");
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
                pallet.PalletQty = (int)ds.Tables[0].Rows[0]["palletQty"];
                pallet.BatchNo = ds.Tables[0].Rows[0]["batchNo"].ToString();
                pallet.CapacityNo = ds.Tables[0].Rows[0]["capacity_no"].ToString();
                pallet.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                pallet.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                pallet.Cusno = ds.Tables[0].Rows[0]["cus_no"].ToString();
                pallet.Cusname = ds.Tables[0].Rows[0]["cus_name"].ToString();
                pallet.Cuspo = ds.Tables[0].Rows[0]["cus_po"].ToString();
                pallet.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                pallet.Cusmatno = ds.Tables[0].Rows[0]["cus_matno"].ToString();
                pallet.SoOrder = ds.Tables[0].Rows[0]["so_order"].ToString();
                pallet.VehicleNo = ds.Tables[0].Rows[0]["vehicle_No"].ToString();
                pallet.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                pallet.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                pallet.Updateser = ds.Tables[0].Rows[0]["update_user"].ToString();
                pallet.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return pallet;
        }


        /// <summary>
        /// 根据工单号查询所有的栈板
        /// </summary>
        /// <param name="workNo"></param>
        /// <returns></returns>
        public List<Pallet> queryPalletsByWorkNo(string workNo)
        {
            List<Pallet> pallets = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,palletNo,palletQty,batchNo,capacity_no,model_no,rule_no,cus_no,cus_matno,del_matno,cus_name,cus_po,so_order,vehicle_No,op_user,create_time,update_user,update_time FROM t_pallet_info where work_no =@workno and del_flag is null order by create_time");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                pallets = new List<Pallet>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Pallet pallet = new Pallet(); ;
                    pallet.Uuid = dr["uuid"].ToString();
                    pallet.PalletNo = dr["palletNo"].ToString();
                    pallet.PalletQty = (int)dr["palletQty"];
                    pallet.BatchNo = dr["batchNo"].ToString();
                    pallet.CapacityNo = dr["capacity_no"].ToString();
                    pallet.Modelno = dr["model_no"].ToString();
                    pallet.Ruleno = dr["rule_no"].ToString();
                    pallet.Cusno = dr["cus_no"].ToString();
                    pallet.Cusname = dr["cus_name"].ToString();
                    pallet.Cuspo = dr["cus_po"].ToString();
                    pallet.Delmatno = dr["del_matno"].ToString();
                    pallet.Cusmatno = dr["cus_matno"].ToString();
                    pallet.SoOrder = dr["so_order"].ToString();
                    pallet.VehicleNo = dr["vehicle_No"].ToString();
                    pallet.Opuser = dr["op_user"].ToString();
                    pallet.Createtime = dr["create_time"].ToString();
                    pallet.Updateser = dr["update_user"].ToString();
                    pallet.Updatetime = dr["update_time"].ToString();
                    pallets.Add(pallet);

                }
            }
            return pallets;
        }


        public List<Pallet> queryPalletsByPalletkNo(string palletNo)
        {
            List<Pallet> pallets = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,palletNo,palletQty,batchNo,capacity_no,model_no,rule_no,cus_no,cus_matno,del_matno,cus_name,cus_po,so_order,vehicle_No,op_user,create_time,update_user,update_time FROM t_pallet_info where palletNo =@palletNo and del_flag is null order by create_time");
            MySqlParameter[] parameters = {
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = palletNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                pallets = new List<Pallet>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Pallet pallet = new Pallet(); ;
                    pallet.Uuid = dr["uuid"].ToString();
                    pallet.PalletNo = dr["palletNo"].ToString();
                    pallet.PalletQty = (int)dr["palletQty"];
                    pallet.BatchNo = dr["batchNo"].ToString();
                    pallet.CapacityNo = dr["capacity_no"].ToString();
                    pallet.Modelno = dr["model_no"].ToString();
                    pallet.Ruleno = dr["rule_no"].ToString();
                    pallet.Cusno = dr["cus_no"].ToString();
                    pallet.Cusname = dr["cus_name"].ToString();
                    pallet.Cuspo = dr["cus_po"].ToString();
                    pallet.Delmatno = dr["del_matno"].ToString();
                    pallet.Cusmatno = dr["cus_matno"].ToString();
                    pallet.SoOrder = dr["so_order"].ToString();
                    pallet.VehicleNo = dr["vehicle_No"].ToString();
                    pallet.Opuser = dr["op_user"].ToString();
                    pallet.Createtime = dr["create_time"].ToString();
                    pallet.Updateser = dr["update_user"].ToString();
                    pallet.Updatetime = dr["update_time"].ToString();
                    pallets.Add(pallet);

                }
            }
            return pallets;
        }



        public Pallet queryPalletByPalletNo(string palletNo)
        {
            Pallet pallet = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,palletNo,palletQty,batchNo,capacity_no,model_no,rule_no,cus_no,cus_matno,del_matno,cus_name,cus_po,so_order,vehicle_No,op_user,create_time,update_user,update_time FROM t_pallet_info where palletNo=@palletNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@palletNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = palletNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                pallet = new Pallet();
                pallet.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                pallet.PalletNo = ds.Tables[0].Rows[0]["palletNo"].ToString();
                pallet.PalletQty = (int)ds.Tables[0].Rows[0]["palletQty"];
                pallet.BatchNo = ds.Tables[0].Rows[0]["batchNo"].ToString();
                pallet.CapacityNo = ds.Tables[0].Rows[0]["capacity_no"].ToString();
                pallet.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                pallet.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                pallet.Cusno = ds.Tables[0].Rows[0]["cus_no"].ToString();
                pallet.Cusname = ds.Tables[0].Rows[0]["cus_name"].ToString();
                pallet.Cuspo = ds.Tables[0].Rows[0]["cus_po"].ToString();
                pallet.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                pallet.Cusmatno = ds.Tables[0].Rows[0]["cus_matno"].ToString();
                pallet.SoOrder = ds.Tables[0].Rows[0]["so_order"].ToString();
                pallet.VehicleNo = ds.Tables[0].Rows[0]["vehicle_No"].ToString();
                pallet.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                pallet.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                pallet.Updateser = ds.Tables[0].Rows[0]["update_user"].ToString();
                pallet.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return pallet;
        }
    }
}
