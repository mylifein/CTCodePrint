using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OracleQuery
    {
        /// <summary>
        /// 通過工單號獲取信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataSet getWorkNoInofo(string workno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MWORK.ORGANIZATION_ID,MWORK.WIP_ENTITY_NAME,MWORK.START_QUANTITY,MWORK.QUANTITY_COMPLETED,MWORK.SO_ORDER,MWORK.ITEM_CODE,MWORK.ITEM_DESC,MWORK.CUST_PO_NUMBER,MWORK.CUSTOMER_ID,MWORK.CUST_NAME FROM MES_WORKINFO_V  MWORK WHERE MWORK.ORGANIZATION_ID = '385' AND MWORK.WIP_ENTITY_NAME =:workno");
            OracleParameter[] parameters =
            {
                new OracleParameter(":workno",OracleType.VarChar,900)

            };
            parameters[0].Value = workno;
            DataSet ds = OracleSQLHelper.ExecuteDataset(OracleSQLHelper.ConnectionString,CommandType.Text,strSql.ToString(),parameters);
            return ds;
        }

        public DataSet getRevisionByDel(string delmatno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ORGANIZATION_ID,ITEM_CODE,ITEM_DESC,REVISION,EFFECTIVITY_DATE,IMPLEMENTATION_DATE FROM MES_ITEM_REVISIONS_V MIR WHERE MIR.ORGANIZATION_ID  = '385' AND MIR.ITEM_CODE =:delmatno");
            OracleParameter[] parameters =
            {
                new OracleParameter(":delmatno",OracleType.VarChar,900)

            };
            parameters[0].Value = delmatno;
            DataSet ds = OracleSQLHelper.ExecuteDataset(OracleSQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }

        public DataSet getCusMatByDel(string cusNo,string delNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MCI.CUSTOMER_ID,MCI.CUSTOMER_ITEM_NUMBER,MCI.CUSTOMER_ITEM_DESC,MCI.ITEM_CODE,MCI.ITEM_DESC FROM MES_CUSTOMER_ITEMINFO_V MCI WHERE MCI.CUSTOMER_ID =:cusNo  AND MCI.ITEM_CODE = :delNo");
            OracleParameter[] parameters =
            {
                new OracleParameter(":cusNo",OracleType.VarChar,900),
                new OracleParameter(":delNo",OracleType.VarChar,900),
            };

            parameters[0].Value = cusNo;
            parameters[1].Value = delNo;
            DataSet ds = OracleSQLHelper.ExecuteDataset(OracleSQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }


    }
}
