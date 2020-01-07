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
    public class ManRelFieldTypeDao
    {

        
        public bool saveManRelFieldType(ManRelFieldType manRelFieldType)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_mand_relfieldtype (uuid,man_no,field_no,op_user,create_time)");
            strSql.Append("values(@uuid,@manNo,@fieldNo,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fieldNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = manRelFieldType.Uuid;
            parameters[1].Value = manRelFieldType.ManNo;
            parameters[2].Value = manRelFieldType.FieldNo;
            parameters[3].Value = manRelFieldType.OpUser;
            parameters[4].Value = manRelFieldType.CreateTime;
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

        public ManRelFieldType queryManRelFieldTypeById(string uuid)
        {
            ManRelFieldType manRelFieldType = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_mand_relfieldtype where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                manRelFieldType = new ManRelFieldType();
                manRelFieldType.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                manRelFieldType.ManNo = ds.Tables[0].Rows[0]["man_no"].ToString();
                manRelFieldType.FieldNo = ds.Tables[0].Rows[0]["field_no"].ToString();
                manRelFieldType.OpUser = ds.Tables[0].Rows[0]["op_user"].ToString();
                manRelFieldType.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
                manRelFieldType.UpdateTime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return manRelFieldType;
        }

        public bool deleteManRelFieldType(string uuid)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_mand_relfieldtype set del_flag = '1' where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = uuid;
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

        public List<MandUnionFieldType> queryMandUnionFieldTypeList(string manNo)
        {

            List<MandUnionFieldType> mandUnionFieldTypeList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.uuid,A.man_no,B.man_desc,A.field_no,C.field_name,C.field_value,C.field_desc,A.op_user,A.create_time,A.update_time FROM t_mand_relfieldtype as A,t_mandatory_info as B,t_field_type as C where A.man_no=@manNo and A.man_no = B.man_no AND A.field_no = C.field_no and A.del_flag is null order by A.create_time ASC");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = manNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mandUnionFieldTypeList = new List<MandUnionFieldType>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MandUnionFieldType mandUnionFieldType = new MandUnionFieldType();
                    mandUnionFieldType.Uuid = dr["uuid"].ToString();
                    mandUnionFieldType.ManNo = dr["man_no"].ToString();
                    mandUnionFieldType.ManDesc = dr["man_desc"].ToString();
                    mandUnionFieldType.FieldNo = dr["field_no"].ToString();
                    mandUnionFieldType.FieldName = dr["field_name"].ToString();
                    mandUnionFieldType.FieldValue = dr["field_value"].ToString();
                    mandUnionFieldType.FieldDesc = dr["field_desc"].ToString();
                    mandUnionFieldType.OpUser = dr["op_user"].ToString();
                    mandUnionFieldType.CreateTime = dr["create_time"].ToString();
                    mandUnionFieldType.UpdateTime = dr["update_time"].ToString();
                    mandUnionFieldTypeList.Add(mandUnionFieldType);
                }

            }
            return mandUnionFieldTypeList;
        }

        /// <summary>
        /// 檢查是否存在
        /// </summary>
        /// <param name="manRelFieldType"></param>
        /// <returns></returns>
        public bool exists(ManRelFieldType manRelFieldType)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_mand_relfieldtype where man_no=@manNo and field_no=@fieldNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@manNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@fieldNo", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = manRelFieldType.ManNo;
            parameters[1].Value = manRelFieldType.FieldNo;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }
    }
}
