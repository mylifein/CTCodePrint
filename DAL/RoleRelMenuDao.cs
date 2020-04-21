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
    public class RoleRelMenuDao
    {
        /// <summary>
        /// 增加角色對應的菜單
        /// </summary>
        /// <param name="roleRelMenu"></param>
        /// <returns></returns>
        public bool saveRoleRelMenu(RoleRelMenu roleRelMenu)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_role_relmemu (uuid,role_no,menu_name,menu_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@roleno,@menuName,@menuDesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@roleno", MySqlDbType.VarChar, 900),
                new MySqlParameter("@menuName", MySqlDbType.VarChar, 900),
                new MySqlParameter("@menuDesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = roleRelMenu.Uuid;
            parameters[1].Value = roleRelMenu.Roleno;
            parameters[2].Value = roleRelMenu.MenuName;
            parameters[3].Value = roleRelMenu.MenuDesc;
            parameters[4].Value = roleRelMenu.Opuser;
            parameters[5].Value = roleRelMenu.Createtime;
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
        /// 根據id查詢 roleRelMenu
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public RoleRelMenu queryMenuInfoById(string uuid)
        {
            RoleRelMenu roleRelMenu = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_role_relmemu where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleRelMenu = new RoleRelMenu();
                roleRelMenu.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                roleRelMenu.Roleno = ds.Tables[0].Rows[0]["role_no"].ToString();
                roleRelMenu.MenuName = ds.Tables[0].Rows[0]["menu_name"].ToString();
                roleRelMenu.MenuDesc = ds.Tables[0].Rows[0]["menu_desc"].ToString();
                roleRelMenu.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                roleRelMenu.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                roleRelMenu.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
            }
            return roleRelMenu;
        }

        /// <summary>
        /// 根據roleNo 查詢角色對應的菜單
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        public List<RoleRelMenu> queryRoleRelMenuList(string roleNo)
        {

            List<RoleRelMenu> roleRelMenuList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_role_relmemu where role_no=@roleNo where del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@roleNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = roleNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleRelMenuList = new List<RoleRelMenu>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RoleRelMenu roleRelMenu = new RoleRelMenu();
                    roleRelMenu.Uuid = dr["uuid"].ToString();
                    roleRelMenu.Roleno = dr["role_no"].ToString();
                    roleRelMenu.MenuName = dr["menu_name"].ToString();
                    roleRelMenu.MenuDesc = dr["menu_desc"].ToString();
                    roleRelMenu.Opuser = dr["op_user"].ToString();
                    roleRelMenu.Createtime = dr["create_time"].ToString();
                    roleRelMenu.Updatetime = dr["update_time"].ToString();
                    roleRelMenuList.Add(roleRelMenu);
                }

            }
            return roleRelMenuList;
        }

        /// <summary>
        /// 根據uuid刪除
        /// </summary>
        /// <param name="roleRelMenu"></param>
        /// <returns></returns>
        public bool deleteRoleRelMenu(string uuid)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_role_relmemu set del_flag = '1' where uuid=@uuid");
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
        /// 根據roleNo 查詢聯合表信息
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        public List<RoleUnionMenu> queryRoleUnionMenuList(string roleNo)
        {

            List<RoleUnionMenu> roleUnionMenuList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.uuid,A.role_no,B.role_name,A.menu_name,A.menu_desc,A.op_user,A.create_time,A.update_time FROM t_role_relmemu AS A,t_role_info AS B WHERE A.role_no = @roleNo AND A.role_no = B.role_no AND A.del_flag IS NULL ORDER BY A.create_time ASC");
            MySqlParameter[] parameters = {
                new MySqlParameter("@roleNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = roleNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                roleUnionMenuList = new List<RoleUnionMenu>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RoleUnionMenu roleUnionMenu = new RoleUnionMenu();
                    roleUnionMenu.Uuid = dr["uuid"].ToString();
                    roleUnionMenu.Roleno = dr["role_no"].ToString();
                    roleUnionMenu.Rolename = dr["role_name"].ToString();
                    roleUnionMenu.Menuname = dr["menu_name"].ToString();
                    roleUnionMenu.Menudesc = dr["menu_desc"].ToString();
                    roleUnionMenu.Opuser = dr["op_user"].ToString();
                    roleUnionMenu.Createtime = dr["create_time"].ToString();
                    roleUnionMenu.Updatetime = dr["update_time"].ToString();
                    roleUnionMenuList.Add(roleUnionMenu);
                }

            }
            return roleUnionMenuList;
        }

        /// <summary>
        /// 檢查該角色是否存在該菜單
        /// </summary>
        /// <param name="menuNo"></param>
        /// <returns></returns>
        public bool exists(RoleRelMenu roleRelMenu)
        {
            bool repeatJudge = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_role_relmemu where role_no=@roleNo and menu_name=@menuName and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@roleNo", MySqlDbType.VarChar, 900),
                new MySqlParameter("@menuName", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = roleRelMenu.Roleno;
            parameters[1].Value = roleRelMenu.MenuName;
            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters).ToString().Trim());
            if (rows > 0)
            {
                repeatJudge = true;
            }
            return repeatJudge;
        }
    }
}
