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
            qualityInfo.Status = "0";          
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


        /// <summary>
        /// 检查是否可以产生新的质检标签
        /// </summary>
        /// <param name="woNo"></param>
        /// <returns></returns>
        public bool checkQualNo(string woNo)
        {
            QualityInfo qualInfo = qualityInfoDao.queryQualityInfoByWoNo(woNo);
            if(qualInfo == null)                             //未产生质检号，可以打印新的质检标签
            {
                return true;
            }else
            {
                if(qualInfo.Status.Equals("3"))             //质检不合格，可以打印新的质检标签
                {
                    return true;
                }
                else
                {
                    return false;                           //其他状态值不可以产生新的质检标签
                }
            }
           
        }



        public bool updateEndQualityInfo(QualityInfo qualityInfo)
        {
            qualityInfo.EndTime = DateTime.Now;
            qualityInfo.DuringTime = (long)(qualityInfo.EndTime - qualityInfo.StartTime)?.TotalMinutes;
            qualityInfo.Updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            qualityInfo.Updateser = Auxiliary.loginName;
            return qualityInfoDao.updateEndQualityInfo(qualityInfo);
        }


        public QualityInfo updateStartQualityInfo(QualityInfo qualityInfo)
        {
            QualityInfo reQualityInfo = null;
            qualityInfo.Updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            qualityInfo.Updateser = Auxiliary.loginName;
            if (qualityInfoDao.updateStartQualityInfo(qualityInfo))
            {
                reQualityInfo = qualityInfoDao.queryQualityInfoByNo(qualityInfo.QualiatyNo);
            }
            return reQualityInfo;
        }


        public int queryCheckNum(string woNo)
        {
            return qualityInfoDao.queryCheckNum(woNo);
        }

    }
}
