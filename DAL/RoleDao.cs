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
    public class RoleDao
    {
        public bool saveRoleInfo(RoleInfo roleInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_role_info (uuid,role_name,role_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@rolename,@roledesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@rolename", MySqlDbType.VarChar, 900),
                new MySqlParameter("@roledesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = roleInfo.Uuid;
            parameters[1].Value = roleInfo.Rolename;
            parameters[2].Value = roleInfo.Roledesc;
            parameters[3].Value = roleInfo.Opuser;
            parameters[4].Value = roleInfo.Createtime;
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

        public RoleInfo queryMenuInfoById(string uuid)
        {
            RoleInfo roleInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_role_info where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleInfo = new RoleInfo();
                roleInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                roleInfo.Roleno = ds.Tables[0].Rows[0]["role_no"].ToString();
                roleInfo.Rolename = ds.Tables[0].Rows[0]["role_name"].ToString();
                roleInfo.Roledesc = ds.Tables[0].Rows[0]["role_desc"].ToString();
                roleInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                roleInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return roleInfo;
        }

        /// <summary>
        /// 根據roleNo 模糊查詢所有Role
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        public List<RoleInfo> queryRoleInfoList(string roleNo)
        {

            List<RoleInfo> roleInfoList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_role_info WHERE role_no LIKE @roleNo AND del_flag is null order by role_no");
            MySqlParameter[] parameters = {
                new MySqlParameter("@roleNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + roleNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleInfoList = new List<RoleInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RoleInfo roleInfo = new RoleInfo();
                    roleInfo.Uuid = dr["uuid"].ToString();
                    roleInfo.Roleno = dr["role_no"].ToString();
                    roleInfo.Rolename = dr["role_name"].ToString();
                    roleInfo.Roledesc = dr["role_desc"].ToString();
                    roleInfo.Opuser = dr["op_user"].ToString();
                    roleInfo.Createtime = dr["create_time"].ToString();
                    roleInfoList.Add(roleInfo);
                }

            }
            return roleInfoList;
        }

    }
}
