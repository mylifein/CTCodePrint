using DBUtility;
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
    public class UserDao
    {

        /// <summary>
        /// 根據用戶和密碼驗證登陸
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool exists(string username,string password)
        {
            bool loginJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_user");
            strSql.Append(" where username=@username and password=@password");
            MySqlParameter[] parameters = {
                new MySqlParameter("@username", MySqlDbType.VarChar, 900),
                new MySqlParameter("@password", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = username.Trim();
            parameters[1].Value = password.Trim();
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(),parameters).ToString().Trim());
            if(rows > 0)
            {
                loginJudge = true;
            }
            return loginJudge;
        }


        public bool saveLoginInfo(string userId,string ipAddress)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_login_info (uuid,user_id,ip_address,create_time) values(@uuid,@userId,@ipAddress,@createTime)");
          
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@userId", MySqlDbType.VarChar, 900),
                new MySqlParameter("@ipAddress", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createTime", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = Auxiliary.Get_UUID();
            parameters[1].Value = userId;
            parameters[2].Value = ipAddress;
            parameters[3].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

        public Model.User queryUser(string username)
        {
            {
                Model.User user = null;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT uuid,user_id,username,password,userdesc,department,prodline_id,op_user,create_time FROM t_user where username=@username");
                MySqlParameter[] parameters = {
                new MySqlParameter("@username", MySqlDbType.VarChar, 900),
            };
                parameters[0].Value = username;
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    user = new Model.User();
                    user.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                    user.Userid = ds.Tables[0].Rows[0]["user_id"].ToString();
                    user.Username = ds.Tables[0].Rows[0]["username"].ToString();
                    user.Password = ds.Tables[0].Rows[0]["password"].ToString();
                    user.Userdesc = ds.Tables[0].Rows[0]["userdesc"].ToString();
                    user.Department = ds.Tables[0].Rows[0]["department"].ToString();
                    user.ProdLine = ds.Tables[0].Rows[0]["prodline_id"] == null ? null : ds.Tables[0].Rows[0]["prodline_id"].ToString();
                    user.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                    user.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                }
                return user;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool saveUser(User user)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_user (uuid,username,password,userdesc,department,op_user,create_time,prodline_id)");
            strSql.Append("values(@uuid,@username,@password,@userdesc,@department,@opuser,@createtime,@prodId)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@username", MySqlDbType.VarChar, 900),
                new MySqlParameter("@password", MySqlDbType.VarChar, 900),
                new MySqlParameter("@userdesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@department", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900),
                new MySqlParameter("@prodId", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = user.Uuid;
            parameters[1].Value = user.Username;
            parameters[2].Value = user.Password;
            parameters[3].Value = user.Userdesc;
            parameters[4].Value = user.Department;
            parameters[5].Value = user.Opuser;
            parameters[6].Value = user.Createtime;
            parameters[7].Value = user.ProdLine;
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


        public User queryUserById(string uuid)
        {
            User user = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,user_id,username,password,userdesc,department,op_user,create_time FROM t_user where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                user = new User();
                user.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                user.Userid = ds.Tables[0].Rows[0]["user_id"].ToString();
                user.Username = ds.Tables[0].Rows[0]["username"].ToString();
                user.Password = ds.Tables[0].Rows[0]["password"].ToString();
                user.Userdesc = ds.Tables[0].Rows[0]["userdesc"].ToString();
                user.Department = ds.Tables[0].Rows[0]["department"].ToString();
                user.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                user.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return user;
        }
    }
}
