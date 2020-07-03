using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CodeRuleService
    {
        private readonly CodeRuleDao codeRuleDao = new CodeRuleDao();
        private readonly CusRuleDao cusRuleDao = new CusRuleDao();


        /// <summary>
        /// 根據编码规则号模糊  查詢編碼規則列表
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public List<CodeRule> queryRulesByRuleNo(string ruleNo)
        {
            return codeRuleDao.queryRulesByType(ruleNo);
        }

        /// <summary>
        /// TODO 根據規則編號，查詢規則詳細信息，精确查询详细信息
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public CodeRule queryRuleById(string ruleNo)
        {
            return codeRuleDao.queryRuleById(ruleNo);
        }



        public CodeRule queryRuleByCond(string cusNo, string delMatno, string boundType)
        {
            CodeRule codeRule = null;
            CusRule cusRule = cusRuleDao.queryCusRuleByConds(cusNo, delMatno, boundType);
            if(cusRule != null)
            {
                codeRule = codeRuleDao.queryRuleById(cusRule.Ruleno);
            }

            return codeRule;
        }


        /// <summary>
        /// 查询所有的规则类型
        /// </summary>
        /// <returns></returns>
        public List<RuleType> queryAllRuleType()
        {
            return codeRuleDao.queryAllRuleType();
        }


        public string saveRuleInfo(CodeRule codeRule)
        {
            bool mark = false;
            string ruleNo = null;
            codeRule.Uuid = Auxiliary.Get_UUID();
            codeRule.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            codeRule.Opuser = Auxiliary.loginName;
            if (codeRuleDao.saveCodeRule(codeRule))
            {
                ruleNo = codeRuleDao.queryRuleByID(codeRule.Uuid);
                foreach (RuleItem item in codeRule.RuleItem)
                {
                    item.Uuid = Auxiliary.Get_UUID();
                    item.Ruleno = ruleNo;
                    item.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    item.Opuser = Auxiliary.loginName;
                    mark = codeRuleDao.saveSaveRuleItem(item);
                }
            }
            return ruleNo;
        }

    }
}
