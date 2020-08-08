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
    public class CTCodeDao
    {


        public CTCode queryCTCodeByCtcode(string ctcode)
        {
            CTCode cTCode = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,ct_code,quantity,rule_no,work_no,cus_no,cus_name,cus_po,po_qty,cus_matno,del_matno,offi_no,ver_no,wo_quantity,model_no,so_order,op_user,create_time FROM t_code_info where ct_code=@ctcode");
            MySqlParameter[] parameters = {
                new MySqlParameter("@ctcode", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = ctcode;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cTCode = new CTCode();
                cTCode.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                cTCode.Ctcode = ds.Tables[0].Rows[0]["ct_code"].ToString(); 
                cTCode.Quantity = (int)ds.Tables[0].Rows[0]["quantity"];
                cTCode.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                cTCode.Workno = ds.Tables[0].Rows[0]["work_no"].ToString();
                cTCode.Cusno = ds.Tables[0].Rows[0]["cus_no"].ToString();
                cTCode.Cusname = ds.Tables[0].Rows[0]["cus_name"].ToString();
                cTCode.Cuspo = ds.Tables[0].Rows[0]["cus_po"].ToString();
                cTCode.Orderqty = ds.Tables[0].Rows[0]["po_qty"].ToString();
                cTCode.Cusmatno = ds.Tables[0].Rows[0]["cus_matno"].ToString();
                cTCode.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                cTCode.Offino = ds.Tables[0].Rows[0]["offi_no"].ToString();
                cTCode.Verno = ds.Tables[0].Rows[0]["ver_no"].ToString();
                cTCode.Woquantity = ds.Tables[0].Rows[0]["wo_quantity"].ToString();
                cTCode.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                cTCode.SoOrder = ds.Tables[0].Rows[0]["so_order"].ToString();
                cTCode.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                cTCode.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return cTCode;
        }

        public bool saveCTInfo(CTCode ctCode, MySqlConnection conn, MySqlTransaction mysqlTrans)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_code_info (uuid,ct_code,rule_no,work_no,cus_no,cus_name,cus_po,po_qty,cus_matno,del_matno,offi_no,ver_no,wo_quantity,model_no,op_user,create_time,so_order,quantity,capacity_no)");
            strSql.Append("values(@uuid,@ctcode,@ruleno,@workno,@cusno,@cusname,@cuspo,@poqty,@cusmatno,@delmatno,@offino,@verno,@woquantity,@model_no,@opuser,@createtime,@soOrder,@quantity,@capacityNo)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ctcode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cuspo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@offino", MySqlDbType.VarChar, 900),
                new MySqlParameter("@verno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woquantity", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@model_no", MySqlDbType.VarChar, 900),
                new MySqlParameter("@poqty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@soOrder", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusname", MySqlDbType.VarChar, 900),
                new MySqlParameter("@quantity", MySqlDbType.Int32, 900),
                new MySqlParameter("@capacityNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = ctCode.Uuid;
            parameters[1].Value = ctCode.Ctcode;
            parameters[2].Value = ctCode.Ruleno;
            parameters[3].Value = ctCode.Workno;
            parameters[4].Value = ctCode.Cusno;
            parameters[5].Value = ctCode.Cuspo;
            parameters[6].Value = ctCode.Cusmatno;
            parameters[7].Value = ctCode.Delmatno;
            parameters[8].Value = ctCode.Offino;
            parameters[9].Value = ctCode.Verno;
            parameters[10].Value = ctCode.Woquantity;
            parameters[11].Value = ctCode.Opuser;
            parameters[12].Value = ctCode.Createtime;
            parameters[13].Value = ctCode.Modelno;
            parameters[14].Value = ctCode.Orderqty;
            parameters[15].Value = ctCode.SoOrder;
            parameters[16].Value = ctCode.Cusname;
            parameters[17].Value = ctCode.Quantity;
            parameters[18].Value = ctCode.CapacityNo;
            int rows = SQLHelper.ExecuteNonQueryTrans(conn, mysqlTrans, CommandType.Text, strSql.ToString(), parameters);
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


        public string getCTCount(string workno)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_code_info where work_no =@workno");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workno;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }
            return countNo;
        }


        /// <summary>
        /// 根據工單和PO查詢已經產生CT數量
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="po"></param>
        /// <returns></returns>
        public string getCTCountByPO(string workno, string po)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_code_info where work_no=@workno and cus_po=@po");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@po", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workno;
            parameters[1].Value = po;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }
            return countNo;
        }


        public string getCTQtyByWoAndCusPo(string workno, string cusPo)
        {
            string countNo = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(quantity) from t_code_info where work_no=@workno and cus_po=@cusPo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusPo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = workno;
            parameters[1].Value = cusPo;
            Object countDB = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (countDB != null && countDB != DBNull.Value)
            {
                countNo = countDB.ToString();
            }
            else
            {
                countNo = "0";
            }
            return countNo;
        }
    }
}
