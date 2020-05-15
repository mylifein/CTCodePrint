using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class QualityInfoService
    {
        private readonly QualityInfoDao qualityInfoDao = new QualityInfoDao();


        /// <summary>
        /// TODO 保存箱号信息
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public QualityInfo saveQualityInfo(QualityInfo qualityInfo)
        {
            QualityInfo reQualityInfo = null;
            qualityInfo.Uuid = Auxiliary.Get_UUID();
            qualityInfo.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            qualityInfo.Opuser = Auxiliary.loginName;
            if (qualityInfoDao.saveQualityInfo(qualityInfo))
            {
                reQualityInfo = qualityInfoDao.queryQualityInfoById(qualityInfo.Uuid);
            }
            return reQualityInfo;
        }


        public QualityInfo queryQualityInfoByNo(string qualityNo)
        {
            return qualityInfoDao.queryQualityInfoByNo(qualityNo);
        }

        public QualityInfo queryQualityInfoById(string uuid)
        {
            return qualityInfoDao.queryQualityInfoById(uuid);
        }


        public string queryMaxQualityNo(string preQualityNo)
        {
            return qualityInfoDao.queryMaxQualityNo(preQualityNo);
        }


    }
}
