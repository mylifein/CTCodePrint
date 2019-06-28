﻿using DBUtility;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PrintModel
    {

        /// <summary>
        /// 根據客戶編號和出貨料號查詢打印模板號
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="delMatNo"></param>
        /// <returns></returns>
        public DataSet queryModelNo(string cusNo,string delMatNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_rel where cus_no =@cusNo and del_matno =@delMatNo ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@cusNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@delMatNo", MySqlDbType.VarChar, 900)
            };

            parameters[0].Value = cusNo.Trim();
            parameters[1].Value = delMatNo.Trim();
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 根據模板號查詢模板信息
        /// </summary>
        /// <param name="modelNo"></param>
        /// <returns></returns>
        public DataSet queryModelInfo(string modelNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_model_info where model_no like '%" + modelNo.Trim() + "%'");
            MySqlParameter[] parameters = {
                new MySqlParameter("@modelNo", MySqlDbType.VarChar, 900),
            };
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }
        

        /// <summary>
        /// 根據模板號查詢必填信息
        /// </summary>
        /// <param name="manNo"></param>
        /// <returns></returns>
        public DataSet queryMandatoryInfo(string manNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mandatory_info where man_no like '%" + manNo.Trim() + "%'");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900)
            };
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        /// <summary>
        /// 保存生成的ctcode 数据
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public bool saveCTInfo(CTCode ctCode)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_code_info (uuid,ct_code,rule_no,work_no,cus_no,cus_po,cus_matno,del_matno,mac_type,offi_no,ver_no,wo_quantity,op_user,create_time)");
            strSql.Append("values(@uuid,@ctcode,@ruleno,@workno,@cusno,@cuspo,@cusmatno,@delmatno,@mac_type,@offino,@verno,@woquantity,@opuser,@createtime)");
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
        /// 
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public string queryCodeNo(string ctCode)
        {
            string maxCtCode = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(ct_code) from t_code_info where ct_code like '%" + ctCode.Trim() + "%'");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900)
            };
            Object maxCode = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if(maxCode != null && maxCode != DBNull.Value)
            {
                 maxCtCode = maxCode.ToString();
            }
            return maxCtCode;
        }


    }
}