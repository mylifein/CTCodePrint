using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MandatoryFieldService
    {
        private readonly MandatoryFieldDao mandatoryFieldDao = new MandatoryFieldDao();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mandatoryInfo"></param>
        /// <returns></returns>
        public MandatoryInfo saveRoleInfo(MandatoryInfo mandatoryInfo)
        {
            MandatoryInfo reMandatoryInfo = null;
            mandatoryInfo.Uuid = Auxiliary.Get_UUID();
            mandatoryInfo.Opuser = Auxiliary.loginName;
            mandatoryInfo.Createtime = Auxiliary.Get_CurrentTime();
            if (mandatoryFieldDao.saveMandatoryInfo(mandatoryInfo))
            {
                reMandatoryInfo = mandatoryFieldDao.queryMenuInfoById(mandatoryInfo.Uuid);
            }
            return reMandatoryInfo;
        }

        /// <summary>
        /// 模糊查詢必填字段信息
        /// </summary>
        /// <param name="manNo"></param>
        /// <returns></returns>
        public List<MandatoryInfo> queryMandatoryInfoList(string manNo)
        {
            return mandatoryFieldDao.queryMandatoryInfoList(manNo);
        }

        /// <summary>
        /// 根據manNo精確查詢
        /// </summary>
        /// <param name="manNo"></param>
        /// <returns></returns>
        public MandatoryInfo queryMandatoryInfoByNo(string manNo)
        {
            return mandatoryFieldDao.queryMandatoryInfoByNo(manNo);
        }
    }
}
