using BLL;
using DAL;
using Model;
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
        private readonly OracleQuery oracleQ = new OracleQuery();

        /// <summary>
        /// 查詢工單信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public List<WorkOrderInfo> getWorkInfoByNo(string workno)
        {
            return oracleQ.getWorkNoInofo(workno);
        }

        public List<WoRevision> getRevisionInfo(string workno)
        {
            List<WorkOrderInfo> workOrderInfos = oracleQ.getWorkNoInofo(workno);
            List<WoRevision> reWoRevisions = null;
            if (workOrderInfos.Count > 0)
            {
                string itemCode = workOrderInfos[0].ItemCode;
                reWoRevisions = oracleQ.getRevisionByDel(itemCode);

            }
            return reWoRevisions;
        }

        public List<CusMatInfo> getCusMatInfo(string workno)
        {
            List<WorkOrderInfo> workOrderInfos = oracleQ.getWorkNoInofo(workno);
            List<CusMatInfo> cusMatInfos = null;
            if (workOrderInfos.Count > 0)
            {

                string cusNo = workOrderInfos[0].CustId;
                string delNo = workOrderInfos[0].ItemCode;
                cusMatInfos = oracleQ.getCusMatByDel(cusNo, delNo);

            }
            return cusMatInfos;
        }

        /// <summary>
        /// 獲得客戶ID和客戶名稱
        /// </summary>
        /// <returns></returns>
        public List<CusInfo> getCusInfo()
        {
            return oracleQ.getCusInfo();
        }
    }
}
