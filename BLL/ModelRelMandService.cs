using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ModelRelMandService
    {
        private readonly ModelRelMandDao modelRelMandDao = new ModelRelMandDao();

        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="modelRelMand"></param>
        /// <returns></returns>
        public ModelRelMand saveModelRelMand(ModelRelMand modelRelMand)
        {
            ModelRelMand reModelRelMand = null;
            modelRelMand.Uuid = Auxiliary.Get_UUID();
            modelRelMand.OpUser = Auxiliary.loginName;
            modelRelMand.CreateTime = Auxiliary.Get_CurrentTime();
            if (modelRelMandDao.saveModelRelMand(modelRelMand))
            {
                reModelRelMand = modelRelMandDao.queryMenuInfoById(modelRelMand.Uuid);
            }
            return reModelRelMand;
        }

        /// <summary>
        /// 根據文件編號查詢信息
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public ModelRelMand queryMenuInfoByFileNo(string fileNo)
        {
            return modelRelMandDao.queryMenuInfoByFileNo(fileNo);
        }
        public bool checkAdd(string fileNo)
        {
            return modelRelMandDao.exists(fileNo);
        }

    }
}
