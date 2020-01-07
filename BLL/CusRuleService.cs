using DAL;
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
    }
}
