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
            strSql.Append("insert into t_carton_info (uuid,cartonNo,cartonQty,prodline_id,carton_status,capacity_no,rule_no,work_no,cus_no,cus_name,cus_po,po_qty,cus_matno,del_matno,offi_no,ver_no,wo_quantity,completed_qty,model_no,op_user,create_time,so_order,packType)");
            strSql.Append("values(@uuid,@cartonNo,@cartonQty,@prodlineId,@cartonStatus,@capacityNo,@ruleno,@workno,@cusno,@cusName,@cuspo,@poqty,@cusmatno,@delmatno,@offino,@verno,@woquantity,@completedQty,@model_no,@opuser,@createtime,@soOrder,@packType)");
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
                new MySqlParameter("@packType", MySqlDbType.VarChar, 900)
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

    }
}
