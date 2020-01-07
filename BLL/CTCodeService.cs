using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CTCodeService
    {
        CTCodeDao cTCodeDao = new CTCodeDao();

        public CTCode queryCTCodeByCtcode(string ctcode)
        {
            return cTCodeDao.queryCTCodeByCtcode(ctcode);
        }

        public bool saveCTCodeInfo(CTCode ctCode)
        {
            ctCode.Uuid = Auxiliary.Get_UUID();
            ctCode.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ctCode.Opuser = Auxiliary.loginName;
            return cTCodeDao.saveCTInfo(ctCode);
        }

        public bool saveCTCodeList(List<CTCode> ctCodeList)
        {
            foreach (CTCode ctCode in ctCodeList)
            {
                if (!this.saveCTCodeInfo(ctCode))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
