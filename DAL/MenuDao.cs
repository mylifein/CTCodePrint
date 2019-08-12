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
    public class MenuDao
    {
        /// <summary>
        /// 保存菜單信息
        /// </summary>
        /// <param name="menuInfo"></param>
        /// <returns></returns>
        public bool saveMenuInfo(MenuInfo menuInfo)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_menu_permission (uuid,menu_name,menu_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@menuname,@menudesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@menuname", MySqlDbType.VarChar, 900),
                new MySqlParameter("@menudesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = menuInfo.Uuid;
            parameters[1].Value = menuInfo.MenuName;
            parameters[2].Value = menuInfo.MenuDescription;
            parameters[3].Value = menuInfo.Opuser;
            parameters[4].Value = menuInfo.CreateTime;
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


        public MenuInfo queryMenuInfoById(string uuid)
        {
            MenuInfo menuInfo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_menu_permission where uuid=@uuid");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                menuInfo = new MenuInfo();
                menuInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                menuInfo.MenuNo = ds.Tables[0].Rows[0]["menu_no"].ToString();
                menuInfo.MenuName = ds.Tables[0].Rows[0]["menu_name"].ToString();
                menuInfo.MenuDescription = ds.Tables[0].Rows[0]["menu_desc"].ToString();
                menuInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                menuInfo.CreateTime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return menuInfo;
        }


        public List<MenuInfo> queryMenuInfoList(string menuNo)
        {

            List<MenuInfo> menuInfoInfoList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_menu_permission where menu_no like @menuNo and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@menuNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + menuNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                menuInfoInfoList = new List<MenuInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MenuInfo menuInfo = new MenuInfo();
                    menuInfo.Uuid = dr["uuid"].ToString();
                    menuInfo.MenuNo = dr["menu_no"].ToString();
                    menuInfo.MenuName = dr["menu_name"].ToString();
                    menuInfo.MenuDescription = dr["menu_desc"].ToString();
                    menuInfo.Opuser = dr["op_user"].ToString();
                    menuInfo.CreateTime = dr["create_time"].ToString();
                    menuInfoInfoList.Add(menuInfo);
                }

            }
            return menuInfoInfoList;
        }
    }
}
