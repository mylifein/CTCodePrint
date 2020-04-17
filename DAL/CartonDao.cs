﻿using DBUtility;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CartonDao
    {
        /// <summary>
        /// TODO 保存箱號信息
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool saveCarton(Carton carton)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_carton_info (uuid,cartonNo,cartonQty,prodline_id,carton_status,capacity_no,rule_no,work_no,cus_no,cus_name,cus_po,po_qty,cus_matno,del_matno,offi_no,ver_no,wo_quantity,completed_qty,model_no,op_user,create_time,so_order,packType,box_No)");
            strSql.Append("values(@uuid,@cartonNo,@cartonQty,@prodlineId,@cartonStatus,@capacityNo,@ruleno,@workno,@cusno,@cusName,@cuspo,@poqty,@cusmatno,@delmatno,@offino,@verno,@woquantity,@completedQty,@model_no,@opuser,@createtime,@soOrder,@packType,@boxNo)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cartonQty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cartonStatus", MySqlDbType.VarChar, 900),
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cuspo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@poqty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@offino", MySqlDbType.VarChar, 900),
                new MySqlParameter("@verno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woquantity", MySqlDbType.VarChar, 900),
                new MySqlParameter("@completedQty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@model_no", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@soOrder", MySqlDbType.VarChar, 900),
                new MySqlParameter("@prodlineId", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusName", MySqlDbType.VarChar, 900),
                new MySqlParameter("@packType", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boxNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = carton.Uuid;
            parameters[1].Value = carton.CartonNo;
            parameters[2].Value = carton.CartonQty;
            parameters[3].Value = carton.CartonStatus;
            parameters[4].Value = carton.CapacityNo;
            parameters[5].Value = carton.Ruleno;
            parameters[6].Value = carton.Workno;
            parameters[7].Value = carton.Cusno;
            parameters[8].Value = carton.Cuspo;
            parameters[9].Value = carton.Orderqty;
            parameters[10].Value = carton.Cusmatno;
            parameters[11].Value = carton.Delmatno;
            parameters[12].Value = carton.Offino;
            parameters[13].Value = carton.Verno;
            parameters[14].Value = carton.Woquantity;
            parameters[15].Value = carton.Completedqty;
            parameters[16].Value = carton.Modelno;
            parameters[17].Value = carton.Opuser;
            parameters[18].Value = carton.Createtime;
            parameters[19].Value = carton.SoOrder;
            parameters[20].Value = carton.ProdLine.ProdlineId;
            parameters[21].Value = carton.Cusname;
            parameters[22].Value = carton.PackType;
            parameters[23].Value = carton.BoxNo;
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

        public string getMaxCartonNoByWO(string workNo)
        {
            string maxCtCode = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(cartonNo) from t_carton_info where work_no=@workNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workNo; ;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }

        public DataSet querygetCartonsInfoByWorkNo(string workNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_carton_info where work_no =@workno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


        public DataSet querygetCartonsInfoByCartonNo(string cartonNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_carton_info where cartonNo =@cartonNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cartonNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 保存CT碼和Carton的關係
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool saveCartonRelation(CtRelCarton ctRelCarton)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_ct_carton (uuid,ct_code,cartonNo,op_user,create_time)");
            strSql.Append("values(@uuid,@ctCode,@cartonNo,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ctCode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@soOrder", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = ctRelCarton.Uuid;
            parameters[1].Value = ctRelCarton.Ctcode;
            parameters[2].Value = ctRelCarton.CartonNo;
            parameters[3].Value = ctRelCarton.Opuser;
            parameters[4].Value = ctRelCarton.Createtime;
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
        /// TODO 查詢CT碼 是否綁定裝箱單號
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public bool exists(String ctCode)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_ct_carton where ct_code=@ctCode and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ctCode", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = ctCode;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }



        /// <summary>
        /// TODO 根据箱号前缀查询是否存在最大得流水号
        /// </summary>
        /// <param name="prefCartonNo"></param>
        /// <returns></returns>
        public string queryMaxCartonNo(string prefCartonNo)
        {
            string maxCtCode = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(cartonNo) from t_carton_info where cartonNo like @cartonNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + prefCartonNo + "%"; ;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }


        /// <summary>
        /// TODO 根據工單查詢 已裝箱數量
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public string getCartonQtyByWO(string workno)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(cartonQty) from t_carton_info where work_no=@workno and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workno;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }else
            {
                countNo = "0";
            }
            return countNo;
        }

        /// <summary>
        /// 根據箱號查詢裝箱單信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public Carton queryCartonByCartonNo(string cartonNo)
        {
            Carton carton = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_carton_info where cartonNo=@cartonNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = cartonNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                carton = new Carton();
                carton.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                carton.CartonNo = ds.Tables[0].Rows[0]["cartonNo"].ToString();
                carton.CartonQty = (int)ds.Tables[0].Rows[0]["cartonQty"];
                //carton.ProdLine.ProdlineId = ds.Tables[0].Rows[0]["prodline_id"].ToString();                 //查詢線別
                carton.CartonStatus = ds.Tables[0].Rows[0]["carton_status"].ToString();
                carton.CapacityNo = ds.Tables[0].Rows[0]["capacity_no"].ToString();
                carton.PackType = ds.Tables[0].Rows[0]["packType"].ToString();
                carton.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                carton.Workno = ds.Tables[0].Rows[0]["work_no"].ToString();
                carton.Cusno = ds.Tables[0].Rows[0]["cus_no"].ToString();
                carton.Cusname = ds.Tables[0].Rows[0]["cus_name"].ToString();
                carton.Cuspo = ds.Tables[0].Rows[0]["cus_po"].ToString();
                carton.Orderqty = ds.Tables[0].Rows[0]["po_qty"].ToString();
                carton.Cusmatno = ds.Tables[0].Rows[0]["cus_matno"].ToString();
                carton.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                carton.Offino = ds.Tables[0].Rows[0]["offi_no"].ToString();
                carton.Verno = ds.Tables[0].Rows[0]["ver_no"].ToString();
                carton.Woquantity = ds.Tables[0].Rows[0]["wo_quantity"].ToString();
                carton.Completedqty = ds.Tables[0].Rows[0]["completed_qty"].ToString();
                carton.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                carton.SoOrder = ds.Tables[0].Rows[0]["so_order"].ToString();
                carton.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                carton.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                carton.Updateser = ds.Tables[0].Rows[0]["update_user"].ToString();
                carton.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return carton;
        }


        /// <summary>
        /// 更新裝箱單狀態, 1是已綁定棧板，2是入庫 3是出庫 
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool updateCartonStatus(Carton carton,int status)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_carton_info set carton_status=@cartonStatus,update_user=@updateUser,update_time=@updateTime where cartonNo=@cartonNo");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cartonStatus", MySqlDbType.Int32, 900),
                new MySqlParameter("@updateUser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@updateTime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = carton.CartonNo;
            parameters[1].Value = status;
            parameters[2].Value = carton.Updateser;
            parameters[3].Value = carton.Updatetime;
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
        /// 查詢文件模板號
        /// </summary>
        /// <param name="prefCartonNo"></param>
        /// <returns></returns>
        public string queryFileNo(string cusNo,string delMatno,string boundType)
        {
            string fileNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select file_no from t_file_reldel where cus_no=@cusNo AND del_matno=@delMatno AND bound_type=@boundType AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@boundType", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = cusNo;
            parameters[1].Value = delMatno;
            parameters[2].Value = boundType;
            Object fileNumber = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (fileNumber != null && fileNumber != DBNull.Value)
            {
                fileNo = fileNumber.ToString();
            }
            return fileNo;
        }

        /// <summary>
        /// 根據裝箱單查詢最大裝箱單號
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <param name="workNo"></param>
        /// <param name="cusPo"></param>
        /// <returns></returns>
        public string queryInspurCartonNo(string cartonNo, string workNo, String cusPo)
        {
            string maxCtCode = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(cartonNo) from t_carton_info where cartonNo like @cartonNo AND work_no=@workNo AND cus_po=@cusPo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusPo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + cartonNo + "%";
            parameters[1].Value = workNo;
            parameters[2].Value = cusPo;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }


        public string queryBatchNoWorkNo(string cartonNo, string workNo)
        {
            string maxCtCode = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(cartonNo) from t_carton_info where cartonNo like @cartonNo AND work_no=@workNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + cartonNo + "%";
            parameters[1].Value = workNo;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }

        public string queryInspurMaxBox(string cartonNo, String cusNo)
        {
            string maxCtCode = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(cartonNo) from t_carton_info where cartonNo like @cartonNo AND cus_no=@cusNo AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cartonNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = "%" + cartonNo + "%";
            parameters[1].Value = cusNo;
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (maxCode != null && maxCode != DBNull.Value)
            {
                maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }

        /// <summary>
        /// 根據工單 計算當前箱數
        /// </summary>
        /// <param name="roleRelMenu"></param>
        /// <returns></returns>

        public int queryCurrentBoxQty(String workNo)
        {
  
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_carton_info where work_no=@workNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workNo;

            int boxNo = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());

            return boxNo;
        }


        

    }
}