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
    public class ProdLineDao
    {

        /// <summary>
        /// TODO 保存线别信息
        /// </summary>
        /// <param name="prodLine"></param>
        /// <returns></returns>
        public bool saveProdLine(ProdLine prodLine)
        {
            bool saveMark = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_prodline (uuid,prodline_name,prodline_desc,dept_id,op_user,create_time)");
            strSql.Append("values(@uuid,@prodlineName,@prodlineDesc,@deptId,@opuser,@createtime)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
                new MySqlParameter("@prodlineName", MySqlDbType.VarChar, 900),
                new MySqlParameter("@prodlineDesc", MySqlDbType.VarChar, 900),
                new MySqlParameter("@deptId", MySqlDbType.VarChar, 900),
                new MySqlParameter("@opuser", MySqlDbType.VarChar, 900),
                new MySqlParameter("@createtime", MySqlDbType.VarChar, 900)
            };
            parameters[0].Value = prodLine.Uuid;
            parameters[1].Value = prodLine.ProdlineName;
            parameters[2].Value = prodLine.ProdlineDesc;
            parameters[3].Value = prodLine.Department.DeptId;
            parameters[4].Value = prodLine.Opuser;
            parameters[5].Value = prodLine.Createtime;
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
        /// 根據uuid 查詢線別信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public ProdLine queryProdLineByUUId(string uuid)
        {
            ProdLine prodLine = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,prodline_id,prodline_name,prodline_desc,dept_id,op_user,create_time FROM t_prodline where uuid=@uuid and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = uuid;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                prodLine = new ProdLine();
                Department department = new Department();
                prodLine.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                prodLine.ProdlineId = ds.Tables[0].Rows[0]["prodline_id"].ToString();
                prodLine.ProdlineName = ds.Tables[0].Rows[0]["prodline_name"].ToString();
                prodLine.ProdlineDesc = ds.Tables[0].Rows[0]["prodline_desc"].ToString();
                department.DeptId = ds.Tables[0].Rows[0]["dept_id"].ToString();
                prodLine.Department = department;
                prodLine.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                prodLine.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return prodLine;
        }

        /// <summary>
        /// TODO 根据线别ID 查询线别信息
        /// </summary>
        /// <param name="prodLineId"></param>
        /// <returns></returns>
        public ProdLine queryProdLineById(string prodLineId)
        {
            ProdLine prodLine = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,prodline_id,prodline_name,prodline_desc,dept_id,op_user,create_time FROM t_prodline where prodline_id=@prodLineId and del_flag is null");
            MySqlParameter[] parameters = {
                new MySqlParameter("@prodLineId", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = prodLineId;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                prodLine = new ProdLine();
                Department department = new Department();
                prodLine.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                prodLine.ProdlineId = ds.Tables[0].Rows[0]["prodline_id"].ToString();
                prodLine.ProdlineName = ds.Tables[0].Rows[0]["prodline_name"].ToString();
                prodLine.ProdlineDesc = ds.Tables[0].Rows[0]["prodline_desc"].ToString();
                department.DeptId = ds.Tables[0].Rows[0]["dept_id"].ToString();
                prodLine.Department = department;
                prodLine.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                prodLine.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return prodLine;
        }

        /// <summary>
        /// TODO 根據線別ID 模糊查詢線別
        /// </summary>
        /// <param name="prodLineId"></param>
        /// <returns></returns>
        public List<ProdLine> queryProdLineList(string prodLineId)
        {

            List<ProdLine> prodLineList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,prodline_id,prodline_name,prodline_desc,op_user,create_time FROM t_prodline WHERE prodline_id LIKE @prodline_id AND del_flag is null ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@prodline_id", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + prodLineId + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                prodLineList = new List<ProdLine>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProdLine prodLine = new ProdLine();
                    prodLine.Uuid = dr["uuid"].ToString();
                    prodLine.ProdlineId = dr["prodline_id"].ToString();
                    prodLine.ProdlineName = dr["prodline_name"].ToString();
                    prodLine.ProdlineDesc = dr["prodline_desc"].ToString();
                    prodLine.Opuser = dr["op_user"].ToString();
                    prodLine.Createtime = dr["create_time"].ToString();
                    prodLineList.Add(prodLine);
                }

            }
            return prodLineList;
        }

        /// <summary>
        /// TODO 根據部門ID 查詢該部門下所有線別
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public List<ProdLine> queryPLByDeptId(string deptId)
        {

            List<ProdLine> prodLineList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,prodline_id,prodline_name,prodline_desc,op_user,create_time FROM t_prodline WHERE dept_id=@depId AND del_flag is null order by create_time");
            MySqlParameter[] parameters = {
                new MySqlParameter("@depId", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = deptId;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                prodLineList = new List<ProdLine>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProdLine prodLine = new ProdLine();
                    prodLine.Uuid = dr["uuid"].ToString();
                    prodLine.ProdlineId = dr["prodline_id"].ToString();
                    prodLine.ProdlineName = dr["prodline_name"].ToString();
                    prodLine.ProdlineDesc = dr["prodline_desc"].ToString();
                    prodLine.Opuser = dr["op_user"].ToString();
                    prodLine.Createtime = dr["create_time"].ToString();
                    prodLineList.Add(prodLine);
                }

            }
            return prodLineList;
        }

    }
}
