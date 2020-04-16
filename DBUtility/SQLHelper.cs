using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public class SQLHelper
    {
        public static readonly string ConnectionString = Auxiliary.Get_SQLConnStr();






        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
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
                foreach (MySqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }



        /**
         * NonQuery Method
         * 
         * */
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return val;
            }
        }

        /**
         * Return DataSet Query
         * 
         * */
        public static DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            DataSet CS_1_0000;
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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


        public static object ExecuteNonQuery_cg(string connectionString, CommandType cmdType, string cmdText, MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
                conn.Close();
                return cmd.Parameters["@amount"].Value;
            }
        }

        //陳剛增加2,執行一個存儲過程,返回存儲過程執行完后的返回值,0為成功,其它為SP執行失敗
        public static object ExecuteNonQuery_cg2(string connectionString, CommandType cmdType, string cmdText, MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();                            //得到受影響的行數
                object ob2 = cmd.Parameters["@returnValue"].Value;
                cmd.Parameters.Clear();
                conn.Close();
                return ob2;                //得到存儲過程執行完后的返回值,0為成功,其它為失敗
            }
        }


        public static DataSet ExecuteDataset(out int mark, string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            DataSet CS_1_0000;
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                //connection.Close();
                return val;
            }
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlDataReader CS_1_0000;
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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


        /// <summary>
        /// 开启事务，批量插入
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommandTrans(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        /// 开启事务保存
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryTrans(MySqlConnection conn, MySqlTransaction mysqlTrans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommandTrans(cmd, conn, mysqlTrans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;

        }

    }
}
