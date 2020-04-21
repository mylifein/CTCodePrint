using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CusRuleService
    {
        private readonly CusRuleDao cusRuleDao = new CusRuleDao();

        /// <summary>
        /// 檢查是否已經存在幾種
        /// </summary>
        /// <param name="cusRule"></param>
        /// <returns></returns>
        public bool checkAdd(CusRule cusRule)
        {
            return cusRuleDao.exists(cusRule);
        }

        public bool saveCusCodeRule(CusRule cusRule)
        {
            cusRule.Uuid = Auxiliary.Get_UUID();
            cusRule.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cusRule.Opuser = Auxiliary.loginName;
            return cusRuleDao.saveRuleRelation(cusRule);
        }


        public CusRule queryCusRuleByCond(string cusNo,string delMatno,string boundType)
        {
            return cusRuleDao.queryCusRuleByConds(cusNo,delMatno,boundType);
        }

    }
}
