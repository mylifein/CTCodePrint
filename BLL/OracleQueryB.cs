using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OracleQueryB
    {
        private readonly OracleQuery oracleQ = new OracleQueryB();

        /// <summary>
        /// 查詢工單信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataSet getWorkInfoByNo(string workno)
        {
            return oracleQ.getWorkNoInofo(workno);
        }

        public DataSet getRevisionInfo(string workno)
        {
            DataSet ds = oracleQ.getWorkNoInofo(workno);
            DataSet resultDS = null;
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    string itemCode = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                    resultDS = oracleQ.getRevisionByDel(itemCode);
                }
            }
            return resultDS;
        }

        public DataSet getCusMatInfo(string workno)
        {
            DataSet ds = oracleQ.getWorkNoInofo(workno);
            DataSet resultDS = null;
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string cusNo = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                    string delNo = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                    resultDS = oracleQ.getCusMatByDel(cusNo,delNo);
                }
            }
            return resultDS;
        }
    }
}
