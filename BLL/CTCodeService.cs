using DAL;
using DBUtility;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CTCodeService
    {
        CTCodeDao cTCodeDao = new CTCodeDao();

        public CTCode queryCTCodeByCtcode(string ctcode)
        {
            return cTCodeDao.queryCTCodeByCtcode(ctcode);
        }

        public bool saveCTCodeInfo(CTCode ctCode, MySqlConnection conn, MySqlTransaction mysqlTrans)
        {
            ctCode.Uuid = Auxiliary.Get_UUID();
            ctCode.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ctCode.Opuser = Auxiliary.loginName;
            return cTCodeDao.saveCTInfo(ctCode, conn,mysqlTrans);
        }

        public bool saveCTCodeList(List<CTCode> ctCodeList)
        {
            //1.获得数据库连接.
            MySqlConnection conn = new MySqlConnection(SQLHelper.ConnectionString);

            //2.打开数据库连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //3.开启事务.
            MySqlTransaction mysqlTrans = conn.BeginTransaction();
            foreach (CTCode ctCode in ctCodeList)
            {
                if (!this.saveCTCodeInfo(ctCode, conn, mysqlTrans))
                {                 
                    conn.Close();
                    return false;
                }
            }
            mysqlTrans.Commit();
            conn.Close();
            return true;
        }


        public string getGeneratedCTCount(string workno)
        {
            return cTCodeDao.getCTCount(workno);
        }


        public string getGeneratedCTCountByPO(string workno, string po)
        {
            return cTCodeDao.getCTCountByPO(workno, po);
        }


        public string getCTQtyByWoAndCusPo(string workno, string cusPo)
        {
            return cTCodeDao.getCTQtyByWoAndCusPo(workno,cusPo);
        }


    }
}
