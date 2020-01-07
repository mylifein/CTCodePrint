using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ModelInfoService
    {
        private readonly ModelInfoDao modelInfoDao = new ModelInfoDao();

        /// <summary>
        /// 根據fileNo查詢模板文件信息
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public ModelFile queryModelFileByNo(string fileNo)
        {
            return modelInfoDao.queryModelFileByNo(fileNo);
        }

        /// <summary>
        /// 模糊查詢模板文件信息
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public List<ModelFile> queryModelFileList(string fileNo)
        {
            return modelInfoDao.queryModelFileList(fileNo);
        }
    }
}
