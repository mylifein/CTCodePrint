using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class FileRelDelService
    {
        private readonly FileRelDelDao fileRelDelDao = new FileRelDelDao();


        public FileRelDel saveFileRelDel(FileRelDel fileRelDel)
        {
            FileRelDel reFileRelDel = null;
            fileRelDel.Uuid = Auxiliary.Get_UUID();
            fileRelDel.OpUser = Auxiliary.loginName;
            fileRelDel.CreateTime = Auxiliary.Get_CurrentTime();
            if (fileRelDelDao.saveFileRelDel(fileRelDel))
            {
                reFileRelDel = fileRelDelDao.queryFileRelDelById(fileRelDel.Uuid);
            }
            return reFileRelDel;
        }

        /// <summary>
        /// 檢查是否存在
        /// </summary>
        /// <param name="fileRelDel"></param>
        /// <returns></returns>
        public bool checkAdd(FileRelDel fileRelDel)
        {
            return fileRelDelDao.exists(fileRelDel);
        }


        public FileRelDel queryFileRelDelCusNo(string cusNo, string delMatNo,string boundType)
        {
            return fileRelDelDao.queryFileRelDelCusNo(cusNo, delMatNo,boundType);
        }
    }
}
