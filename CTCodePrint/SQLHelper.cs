using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace DBUtility
{
    public  abstract class SQLHelper
    {
        //public static readonly string ConnectionStringBC = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
        public static readonly string ConnectionString = Auxiliary.Get_SQLConnStr();





        /// <summary>
        /// 對字符串轉換成64位字節碼
        /// </summary>
        /// <param name="sSourceString">待轉換字符串</param>
        /// <returns>返回轉換成功的字節碼(字符串形式)</returns>
        public static string EncodingString(string sSourceString)
        {
            string sTargetString = string.Empty;
            try
            {
                byte[] bData = System.Text.ASCIIEncoding.ASCII.GetBytes(sSourceString); ;
                sTargetString = Convert.ToBase64String(bData);
            }
            catch
            {
                sTargetString = "";
            }
            return sTargetString;
        }

        /// <summary>
        ///把64位字節碼解碼為字符串
        /// </summary>
        /// <param name="sSourceString">待轉換的字節表</param>
        /// <returns>返回轉換成功的字符串</returns>
        public static string DecodingString(string sSourceString)
        {
            string sTargetString = string.Empty;
            try
            {
                byte[] bData = Convert.FromBase64String(sSourceString);
                sTargetString = System.Text.ASCIIEncoding.ASCII.GetString(bData);
            }
            catch
            {
                sTargetString = "";
            }
            return sTargetString;
        }

        //public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        //{
        //    parmCache[cacheKey] = commandParameters;
        //}

        public static DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            DataSet CS_1_0000;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                CS_1_0000 = ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return CS_1_0000;
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return val;
            }
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlDataReader CS_1_0000;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                CS_1_0000 = rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                //
            }
            return CS_1_0000;
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                //connection.Close();
                return val;
            }
        }

        //public static SqlParameter[] GetCachedParameters(string cacheKey)
        //{
        //    SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
        //    if (cachedParms == null)
        //    {
        //        return null;
        //    }
        //    SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
        //    int i = 0;
        //    int j = cachedParms.Length;
        //    while (i < j)
        //    {
        //        clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
        //        i++;
        //    }
        //    return clonedParms;
        //}

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        //陳剛增加
        public static object ExecuteNonQuery_cg(string connectionString, CommandType cmdType, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
                conn.Close();
                return cmd.Parameters["@amount"].Value;
            }
        }

        //陳剛增加2,執行一個存儲過程,返回存儲過程執行完后的返回值,0為成功,其它為SP執行失敗
        public static object ExecuteNonQuery_cg2(string connectionString, CommandType cmdType, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();                            //得到受影響的行數
                object ob2 = cmd.Parameters["@returnValue"].Value;
                cmd.Parameters.Clear();
                conn.Close();
                return ob2;                //得到存儲過程執行完后的返回值,0為成功,其它為失敗
            }
        }
        
        //陳剛增加3,執行一個存儲過程,得到輸出參數和結果集
        public static DataSet ExecuteDataset(out int mark,string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            DataSet CS_1_0000;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);


                //得到輸出參數的值
                object mk = cmd.Parameters["@mark"].Value;

                if ((mk == null) || (mk == DBNull.Value))
                {
                    mark = -30;        //未知錯誤
                }
                else
                {
                    mark = int.Parse(mk.ToString().Trim());     //返回正常值                    
                }
                

                cmd.Parameters.Clear();
                CS_1_0000 = ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return CS_1_0000;
        }
    }
}
