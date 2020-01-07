using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SubMatInfoService
    {
        private readonly SubMatInfoDao subMatInfoDao = new SubMatInfoDao();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="subMatInfo"></param>
        /// <returns></returns>
        public SubMatInfo saveSubMatInfoDao(SubMatInfo subMatInfo)
        {
            SubMatInfo reSubMatInfo = null;
            subMatInfo.Uuid = Auxiliary.Get_UUID();
            subMatInfo.Opuser = Auxiliary.loginName;
            subMatInfo.Createtime = Auxiliary.Get_CurrentTime();
            if (subMatInfoDao.saveSubMatInfoDao(subMatInfo))
            {
                reSubMatInfo = subMatInfoDao.querySubMatInfoById(subMatInfo.Uuid);
            }
            return reSubMatInfo;
        }

        public List<SubMatInfo> querySubMatInfoList(string delmatno)
        {
            return subMatInfoDao.querySubMatInfoList(delmatno);
        }

        public bool checkAdd(SubMatInfo subMatInfo)
        {
            return subMatInfoDao.exists(subMatInfo);
        }
    }
}
