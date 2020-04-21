using DBUtility;
using Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
        public List<WorkOrderInfo> getWorkNoInofo(string workno)
        {
            //385  572 CIN：833
            List<WorkOrderInfo> workOrders = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MWORK.ORGANIZATION_ID,MWORK.CUSTOMER_ITEM_NUMBER,MWORK.WIP_ENTITY_NAME,MWORK.START_QUANTITY,MWORK.QUANTITY_COMPLETED,MWORK.SO_ORDER,MWORK.ITEM_CODE,MWORK.ITEM_DESC,MWORK.CUST_PO_NUMBER,MWORK.CUSTOMER_ID,MWORK.CUST_NAME,MWORK.ORDER_QTY FROM MES_WORKINFO_V  MWORK WHERE MWORK.WIP_ENTITY_NAME =:workno");
            OracleParameter[] parameters =
            {
                new OracleParameter(":workno",OracleDbType.Varchar2,900)

            };
            parameters[0].Value = workno;
            DataSet ds = OracleSQLHelper.ExecuteDataset(OracleSQLHelper.ConnectionString,CommandType.Text,strSql.ToString(),parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                workOrders = new List<WorkOrderInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WorkOrderInfo workOrderInfo = new WorkOrderInfo();
                    workOrderInfo.OrgId = dr["ORGANIZATION_ID"].ToString();
                    workOrderInfo.CusItemNum = dr["CUSTOMER_ITEM_NUMBER"].ToString();
                    workOrderInfo.WorkNo = dr["WIP_ENTITY_NAME"].ToString();
                    workOrderInfo.StartQty = dr["START_QUANTITY"].ToString();
                    workOrderInfo.CompletedQty = dr["QUANTITY_COMPLETED"].ToString();
                    workOrderInfo.SoOrder = dr["SO_ORDER"].ToString();
                    workOrderInfo.ItemCode = dr["ITEM_CODE"].ToString();
                    workOrderInfo.ItemDesc = dr["ITEM_DESC"].ToString();
                    workOrderInfo.CustPO = dr["CUST_PO_NUMBER"].ToString();
                    workOrderInfo.CustId = dr["CUSTOMER_ID"].ToString();
                    workOrderInfo.CustName = dr["CUST_NAME"].ToString();
                    workOrderInfo.OrderQty = dr["ORDER_QTY"].ToString();
                    workOrders.Add(workOrderInfo);

                }
            }
            return workOrders;
        }



        public List<WoRevision> getRevisionByDel(string delmatno)
        {
            List<WoRevision> woRevisions = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ORGANIZATION_ID,ITEM_CODE,ITEM_DESC,REVISION,EFFECTIVITY_DATE,IMPLEMENTATION_DATE FROM MES_ITEM_REVISIONS_V MIR WHERE MIR.ITEM_CODE =:delmatno");
            OracleParameter[] parameters =
            {
                new OracleParameter(":delmatno",OracleDbType.Varchar2,900)

            };
            parameters[0].Value = delmatno;
            DataSet ds = OracleSQLHelper.ExecuteDataset(OracleSQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                woRevisions = new List<WoRevision>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WoRevision woRevision = new WoRevision();
                    woRevision.OrgId = dr["ORGANIZATION_ID"].ToString();
                    woRevision.ItemCode = dr["ITEM_CODE"].ToString();
                    woRevision.ItemDesc = dr["ITEM_DESC"].ToString();
                    woRevision.Revision = dr["REVISION"].ToString();
                    woRevision.EffectDate = dr["EFFECTIVITY_DATE"].ToString();
                    woRevision.ImplDate = dr["IMPLEMENTATION_DATE"].ToString();
                    woRevisions.Add(woRevision);
                }
            }
            return woRevisions;
        }

        public List<CusMatInfo> getCusMatByDel(string cusNo,string delNo)
        {
            List<CusMatInfo> cusMatInfos = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MCI.CUSTOMER_ID,MCI.CUSTOMER_ITEM_NUMBER,MCI.CUSTOMER_ITEM_DESC,MCI.ITEM_CODE,MCI.ITEM_DESC FROM MES_CUSTOMER_ITEMINFO_V MCI WHERE MCI.CUSTOMER_ID =:cusNo  AND MCI.ITEM_CODE = :delNo");
            OracleParameter[] parameters =
            {
                new OracleParameter(":cusNo",OracleDbType.Varchar2,900),
                new OracleParameter(":delNo",OracleDbType.Varchar2,900),
            };

            parameters[0].Value = cusNo;
            parameters[1].Value = delNo;
            DataSet ds = OracleSQLHelper.ExecuteDataset(OracleSQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cusMatInfos = new List<CusMatInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CusMatInfo cusMatInfo = new CusMatInfo();
                    cusMatInfo.CusId = dr["CUSTOMER_ID"].ToString();
                    cusMatInfo.CusItemCode = dr["CUSTOMER_ITEM_NUMBER"].ToString();
                    cusMatInfo.CusItemDesc = dr["CUSTOMER_ITEM_DESC"].ToString();
                    cusMatInfo.ItemCode = dr["ITEM_CODE"].ToString();
                    cusMatInfo.ItemDesc = dr["ITEM_DESC"].ToString();
                    cusMatInfos.Add(cusMatInfo);
                }
            }
            return cusMatInfos;
        }


        /// <summary>
        /// 獲得客戶編號和名稱
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="delNo"></param>
        /// <returns></returns>
        public List<CusInfo> getCusInfo()
        {
            List<CusInfo> cusInfoList = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT CUST.CUST_ACCOUNT_ID, CUST.PARTY_NAME FROM MES_CUSTINFO_V CUST");
            OracleParameter[] parameters =
            {
            };

            DataSet ds = OracleSQLHelper.ExecuteDataset(OracleSQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cusInfoList = new List<CusInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CusInfo cusInfo = new CusInfo();
                    cusInfo.CusNo = dr["CUST_ACCOUNT_ID"].ToString();
                    cusInfo.CusName = dr["PARTY_NAME"].ToString();
                    cusInfoList.Add(cusInfo);

                }
            }
            return cusInfoList;
        }


    }
}
