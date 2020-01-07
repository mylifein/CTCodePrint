using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public static class Auxiliary
    {
        public static string loginName;

        public static string RoleNo;
        public static string Get_SQLConnStr()
        {
            string sqlConnStr = ConfigurationManager.ConnectionStrings["ConnectionMySqlString"].ConnectionString.ToString();
            return sqlConnStr;
        }

        public static string Get_OracleSQLConnStr()
        {
            string sqlConnStr = ConfigurationManager.ConnectionStrings["ConnectionOracleString"].ConnectionString.ToString();
            return sqlConnStr;
        }

        public static string Get_UUID()
        {
            string uuid = System.Guid.NewGuid().ToString("N");
            return uuid;
        }


        public static string Get_MyUserName()
        {
            string username = ConfigurationManager.AppSettings["MyUserName"].ToString().Trim();             
            return username.Trim();
        }

        public static string Get_MyPassword()
        {
            string password = ConfigurationManager.AppSettings["MyPassword"].ToString().Trim();
            return password;
        }

        public static string Get_CurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string downloadModelFile(ModelFile modelFile)
        {
            string templateFile = System.IO.Directory.GetCurrentDirectory() + "\\" + modelFile.Filename;
            if (File.Exists(templateFile))
            {
                Process[] pcs = Process.GetProcesses();
                foreach (Process p in pcs)
                {
                    //确定文件进程名
                    if (p.ProcessName == "lppa")
                    {
                        //确认文件

                        //p.Kill();//结束进程

                        return templateFile;
                    }
                }
                File.Delete(templateFile);
            }
            FileStream fs = new FileStream(templateFile, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(modelFile.Fileaddress, 0, modelFile.Fileaddress.Length); //用文件流生成一个文件
            bw.Close();
            fs.Close();
            bw.Dispose();
            fs.Dispose();
            return templateFile;
        }

    }
}
