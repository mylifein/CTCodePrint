using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class WoBatchService
    {
        private readonly WoBatchDao woBatchDao = new WoBatchDao();


        /// <summary>
        /// 根据浪潮日期码模糊查询 浪潮批次号
        /// </summary>
        /// <param name="batchCond"></param>
        /// <returns></returns>
        public List<string> queryBatchNos(string batchCond)
        {
            return woBatchDao.queryBatchNos(batchCond);
        }


        /// <summary>
        /// 根据工单号查询装箱单批次.
        /// </summary>
        /// <param name="workNo"></param>
        /// <returns></returns>
        public bool saveWoBatch(WoBatch woBatch)
        {
            woBatch.Uuid = Auxiliary.Get_UUID();
            woBatch.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            woBatch.Opuser = Auxiliary.loginName;
            return woBatchDao.saveWoBatch(woBatch);
        }


        public string getBatchNoByWO(string workNo)
        {
            return woBatchDao.getBatchNoByWO(workNo);
        }
    }
}
