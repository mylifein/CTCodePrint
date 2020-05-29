using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class WoInfoService
    {
        private readonly WoInfoDao woInfoDao = new WoInfoDao();



        /// <summary>
        /// 保存工单信息
        /// </summary>
        /// <param name="woInfo"></param>
        /// <returns></returns>
        public bool saveWoInfo(WoInfo woInfo)
        {
            woInfo.Uuid = Auxiliary.Get_UUID();
            woInfo.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            woInfo.Opuser = Auxiliary.loginName;
            woInfo.Status = "0";
            woInfo.CheckTimes = 0;
            return woInfoDao.saveWoInfo(woInfo);
        }


        public WoInfo queryWoInfoByNo(string woNo)
        {
            return woInfoDao.queryWoInfoByNo(woNo);
        }

        /// <summary>
        /// 查询工单信息是否存在
        /// </summary>
        /// <param name="woNo"></param>
        /// <returns></returns>
        public bool exists(string woNo)
        {
            return woInfoDao.exists(woNo);
        }


        /// <summary>
        /// 更新工单的状态及检验次数
        /// </summary>
        /// <param name="woInfo"></param>
        /// <returns></returns>
        public bool updateWoInfoStatusAndTimes(WoInfo woInfo)
        {
            woInfo.Updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            woInfo.Updateser = Auxiliary.loginName;
            return woInfoDao.updateWoInfoStatusAndTimes(woInfo);
        }


        public bool updateWoInfoStatus(WoInfo woInfo)
        {
            woInfo.Updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            woInfo.Updateser = Auxiliary.loginName;
            return woInfoDao.updateWoInfoStatus(woInfo);
        }

    }
}
