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
    public class DepartDao
    {

        /// <summary>
        /// TODO  保存部門
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public bool saveDepart(Department department)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_department (uuid,dept_name,dept_desc,op_user,create_time)");
            strSql.Append("values(@uuid,@deptName,@deptDesc,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@deptName", MySqlDbType.VarChar, 900),
                new MySqlParameter("@deptDesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = department.Uuid;
            parameters[1].Value = department.DeptName;
            parameters[2].Value = department.DeptDesc;
            parameters[3].Value = department.Opuser;
            parameters[4].Value = department.Createtime;
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
        /// TODO 根據uuid 查詢部門信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public Department queryDepartmentById(string uuid)
        {
            Department department = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,dept_id,dept_name,dept_desc,op_user,create_time FROM t_department where uuid=@uuid AND del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                department = new Department();
                department.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                department.DeptId = ds.Tables[0].Rows[0]["dept_id"].ToString();
                department.DeptName = ds.Tables[0].Rows[0]["dept_name"].ToString();
                department.DeptDesc = ds.Tables[0].Rows[0]["dept_desc"].ToString();
                department.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                department.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return department;
        }

        /// <summary>
        /// TODO 根據部門ID 模糊查詢所有部門
        /// </summary>
        /// <param name="departNo"></param>
        /// <returns></returns>
        public List<Department> queryDepartmentList(string departNo)
        {

            List<Department> departmentList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,dept_id,dept_name,dept_desc,op_user,create_time FROM t_department WHERE dept_id LIKE @dept_id AND del_flag is null order by create_time");
            MySqlParameter[] parameters = {
                new MySqlParameter("@dept_id", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + departNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                departmentList = new List<Department>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Department department = new Department();
                    department.Uuid = dr["uuid"].ToString();
                    department.DeptId = dr["dept_id"].ToString();
                    department.DeptName = dr["dept_name"].ToString();
                    department.DeptDesc = dr["dept_desc"].ToString();
                    department.Opuser = dr["op_user"].ToString();
                    department.Createtime = dr["create_time"].ToString();
                    departmentList.Add(department);
                }

            }
            return departmentList;
        }


    }
}
