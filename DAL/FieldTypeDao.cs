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
    public class FieldTypeDao
    {
        /// <summary>
        /// 保存規則類型信息
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public bool saveFieldType(FieldType fieldType)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_field_type (uuid,field_name,field_value,field_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@field_name,@field_value,@field_desc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@field_name", MySqlDbType.VarChar, 900),
                new MySqlParameter("@field_value", MySqlDbType.VarChar, 900),
                new MySqlParameter("@field_desc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = fieldType.Uuid;
            parameters[1].Value = fieldType.FieldName;
            parameters[2].Value = fieldType.FieldValue;
            parameters[3].Value = fieldType.FieldDesc;
            parameters[4].Value = fieldType.OpUser;
            parameters[5].Value = fieldType.CreateTime;

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
        /// 根據ID查詢字段類型信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public FieldType queryFieldTypeById(string uuid)
        {
            FieldType fieldType = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,field_no,field_name,field_value,field_desc,op_user,create_time FROM t_field_type where uuid=@uuid AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                fieldType = new FieldType();
                fieldType.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                fieldType.FieldNo = ds.Tables[0].Rows[0]["field_no"].ToString();
                fieldType.FieldName = ds.Tables[0].Rows[0]["field_name"].ToString();
                fieldType.FieldValue = ds.Tables[0].Rows[0]["field_value"].ToString();
                fieldType.FieldDesc = ds.Tables[0].Rows[0]["field_desc"].ToString();
                fieldType.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                fieldType.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return fieldType;
        }

        /// <summary>
        /// 根據字段號模糊查詢
        /// </summary>
        /// <param name="fieldNo"></param>
        /// <returns></returns>
        public List<FieldType> queryFieldTypeList(string fieldNo)
        {

            List<FieldType> fieldTypeList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,field_no,field_name,field_value,field_desc,op_user,create_time FROM t_field_type WHERE field_no LIKE @field_no AND del_flag is null order by create_time");
            MySqlParameter[] parameters = {
                new MySqlParameter("@field_no", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + fieldNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                fieldTypeList = new List<FieldType>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    FieldType fieldType = new FieldType();
                    fieldType.Uuid = dr["uuid"].ToString();
                    fieldType.FieldNo = dr["field_no"].ToString();
                    fieldType.FieldName = dr["field_name"].ToString();
                    fieldType.FieldValue = dr["field_value"].ToString();
                    fieldType.FieldDesc = dr["field_desc"].ToString();
                    fieldType.OpUser = dr["op_user"].ToString();
                    fieldType.CreateTime = dr["create_time"].ToString();
                    fieldTypeList.Add(fieldType);
                }

            }
            return fieldTypeList;
        }

    }
}
