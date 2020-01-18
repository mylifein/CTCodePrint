using DAL;
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


        /// <summary>
        /// 根據功能類型 查詢編碼規則列表（0: CT編碼規則，1：箱號編碼規則,2:棧板編碼規則）
        /// </summary>
        /// <param name="funcType"></param>
        /// <returns></returns>
        public List<CodeRule> queryRulesByType(string funcType)
        {
            return codeRuleDao.queryRulesByType(funcType);
        }

        /// <summary>
        /// TODO 根據規則編號，查詢規則詳細信息
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public CodeRule queryRuleById(string ruleNo)
        {
            return codeRuleDao.queryRuleById(ruleNo);
        }

    }
}
