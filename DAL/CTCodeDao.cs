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
            strSql.Append("SELECT * FROM t_code_info where ct_code=@ctcode");
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
                cTCode.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                cTCode.Workno = ds.Tables[0].Rows[0]["work_no"].ToString();
                cTCode.Cusno = ds.Tables[0].Rows[0]["cus_no"].ToString();
                cTCode.Cuspo = ds.Tables[0].Rows[0]["cus_po"].ToString();
                cTCode.Orderqty = ds.Tables[0].Rows[0]["po_qty"].ToString();
                cTCode.Cusmatno = ds.Tables[0].Rows[0]["cus_matno"].ToString();
                cTCode.Delmatno = ds.Tables[0].Rows[0]["del_matno"].ToString();
                cTCode.Mactype = ds.Tables[0].Rows[0]["mac_type"].ToString();
                cTCode.Offino = ds.Tables[0].Rows[0]["offi_no"].ToString();
                cTCode.Verno = ds.Tables[0].Rows[0]["ver_no"].ToString();
                cTCode.Woquantity = ds.Tables[0].Rows[0]["wo_quantity"].ToString();
                cTCode.Completedqty = ds.Tables[0].Rows[0]["completed_qty"].ToString();
                cTCode.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                cTCode.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                cTCode.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return cTCode;
        }

        public bool saveCTInfo(CTCode ctCode)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_code_info (uuid,ct_code,rule_no,work_no,cus_no,cus_po,po_qty,cus_matno,del_matno,mac_type,offi_no,ver_no,wo_quantity,completed_qty,model_no,op_user,create_time,so_order)");
            strSql.Append("values(@uuid,@ctcode,@ruleno,@workno,@cusno,@cuspo,@poqty,@cusmatno,@delmatno,@mac_type,@offino,@verno,@woquantity,@completedQty,@model_no,@opuser,@createtime,@soOrder)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ctcode", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ruleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@workno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cuspo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@cusmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delmatno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@mac_type", MySqlDbType.VarChar, 900),
                new MySqlParameter("@offino", MySqlDbType.VarChar, 900),
                new MySqlParameter("@verno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@woquantity", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@completedQty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@model_no", MySqlDbType.VarChar, 900),
                new MySqlParameter("@poqty", MySqlDbType.VarChar, 900),
                new MySqlParameter("@soOrder", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = ctCode.Uuid;
            parameters[1].Value = ctCode.Ctcode;
            parameters[2].Value = ctCode.Ruleno;
            parameters[3].Value = ctCode.Workno;
            parameters[4].Value = ctCode.Cusno;
            parameters[5].Value = ctCode.Cuspo;
            parameters[6].Value = ctCode.Cusmatno;
            parameters[7].Value = ctCode.Delmatno;
            parameters[8].Value = ctCode.Mactype;
            parameters[9].Value = ctCode.Offino;
            parameters[10].Value = ctCode.Verno;
            parameters[11].Value = ctCode.Woquantity;
            parameters[12].Value = ctCode.Opuser;
            parameters[13].Value = ctCode.Createtime;
            parameters[14].Value = ctCode.Completedqty;
            parameters[15].Value = ctCode.Modelno;
            parameters[16].Value = ctCode.Orderqty;
            parameters[17].Value = ctCode.SoOrder;
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
