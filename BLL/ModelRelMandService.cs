using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MandRelDelService
    {
        private readonly MandRelDelDao mandRelDelDao = new MandRelDelDao();


        /// <summary>
        /// 保存字段
        /// </summary>
        /// <param name="MandRelDel"></param>
        /// <returns></returns>
        public MandRelDel saveMandRelDel(MandRelDel modelRelMand)
        {
            MandRelDel reMandRelDel = null;
            modelRelMand.Uuid = Auxiliary.Get_UUID();
            modelRelMand.OpUser = Auxiliary.loginName;
            modelRelMand.CreateTime = Auxiliary.Get_CurrentTime();
            if (mandRelDelDao.saveMandRelDel(modelRelMand))
            {
                reMandRelDel = mandRelDelDao.queryMandRelDelById(modelRelMand.Uuid);
            }
            return reMandRelDel;
        }

        public MandRelDel queryManNoByDel(string cusNo,string delMatno,string boudType)
        {
            MandRelDel mandRelDel = new MandRelDel();
            mandRelDel.CusNo = cusNo;
            mandRelDel.BoundType = boudType;
            mandRelDel.DelMatno = delMatno;
            return mandRelDelDao.queryManNoByDel(mandRelDel);
        }



        public bool checkAdd(MandRelDel mandRelDel)
        {
            return mandRelDelDao.exists(mandRelDel);
        }

    }
}
