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
    public class ModelInfoDao
    {

        /// <summary>
        /// 根據fileNo 查詢模板信息
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public ModelFile queryModelFileByNo(string fileNo)
        {
            ModelFile modelFile = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,file_no,fileName,fileDescription,fileAddress,op_user,create_time FROM t_model_file where file_no=@fileNo AND del_flag is null ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = fileNo;
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelFile = new ModelFile();
                modelFile.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                modelFile.Fileno = ds.Tables[0].Rows[0]["file_no"].ToString();
                modelFile.Filename = ds.Tables[0].Rows[0]["fileName"].ToString();
                modelFile.Filedescription = ds.Tables[0].Rows[0]["fileDescription"].ToString();
                modelFile.Fileaddress = (byte[])ds.Tables[0].Rows[0]["fileAddress"];
                modelFile.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                modelFile.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }

            return modelFile;
        }

        public List<ModelFile> queryModelFileList(string fileNo)
        {

            List<ModelFile> modelFileList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT uuid,file_no,fileName,fileDescription,fileAddress,op_user,create_time FROM t_model_file WHERE file_no like @fileNo AND del_flag is null ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@fileNo", MySqlDbType.VarChar, 900),
            };
            parameters[0].Value = "%" + fileNo + "%";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelFileList = new List<ModelFile>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ModelFile modelFile = new ModelFile();
                    modelFile.Uuid = dr["uuid"].ToString();
                    modelFile.Fileno = dr["file_no"].ToString();
                    modelFile.Filename = dr["fileName"].ToString();
                    modelFile.Filedescription = dr["fileDescription"].ToString();
                    modelFile.Opuser = dr["op_user"].ToString();
                    modelFile.Createtime = dr["create_time"].ToString();
                    modelFileList.Add(modelFile);
                }

            }
            return modelFileList;
        }
    }
}
