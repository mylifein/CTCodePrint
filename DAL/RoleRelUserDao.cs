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
    public class RoleRelUserDao
    {

        public bool saveRoleRelUser(RoleRelUser roleRelUser)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_role_reluser (uuid,role_no,user_id,op_user,create_time)");
            strSql.Append("values(@uuid,@roleno,@userid,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@roleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@userid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = roleRelUser.Uuid;
            parameters[1].Value = roleRelUser.Roleno;
            parameters[2].Value = roleRelUser.Userid;
            parameters[3].Value = roleRelUser.Opuser;
            parameters[4].Value = roleRelUser.Createtime;
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

        public bool deleteRoleRelUser(string uuid)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_role_reluser set del_flag = '1' where uuid=@uuid");
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

        /// <summary>
        /// 根據roleNo 查詢角色聯合用戶
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        public List<RoleUnionUser> queryRoleUnionUserList(string userid)
        {

            List<RoleUnionUser> roleUnionUserList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.uuid,A.role_no,B.role_name,C.username,C.userdesc,A.op_user,A.create_time,A.update_time FROM t_role_reluser as A,t_role_info as B,t_user as C where A.user_id=@userid and A.role_no = B.role_no AND A.user_id = C.user_id and A.del_flag is null order by A.create_time ASC");
            MySqlParameter[] parameters = {
                new MySqlParameter("@userid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = userid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleUnionUserList = new List<RoleUnionUser>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RoleUnionUser roleUnionUser = new RoleUnionUser();
                    roleUnionUser.Uuid = dr["uuid"].ToString();
                    roleUnionUser.Roleno = dr["role_no"].ToString();
                    roleUnionUser.Rolename = dr["role_name"].ToString();
                    roleUnionUser.Username = dr["username"].ToString();
                    roleUnionUser.Userdesc = dr["userdesc"].ToString();
                    roleUnionUser.Opuser = dr["op_user"].ToString();
                    roleUnionUser.Createtime = dr["create_time"].ToString();
                    roleUnionUser.Updatetime = dr["update_time"].ToString();
                    roleUnionUserList.Add(roleUnionUser);
                }

            }
            return roleUnionUserList;
        }

        public bool exists(RoleRelUser roleRelUser)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_role_reluser where role_no=@roleNo and user_id=@userID and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@roleNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@userID", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = roleRelUser.Roleno;
            parameters[1].Value = roleRelUser.Userid;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }


    }
}
