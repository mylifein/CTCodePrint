﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SelectQuery
    {
        private readonly DAL.SelectControl selectControl = new DAL.SelectControl();

        //获得客户的下拉选择
        public DataSet getCusSelect()
        {
            return selectControl.getSelectControl();
        }

        /// <summary>
        /// 獲得規則,通過客戶編號和機種類型
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="macNo"></param>
        /// <returns></returns>
        public DataSet getRulesByNo(string cusNo,string macNo)
        {
            DataSet ds = null;
            ds = selectControl.getRuleByCus(cusNo, macNo);
            if(ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)
            {
                string ruleNo = ds.Tables[0].Rows[0]["rule_no"].ToString();
                ds = selectControl.getRuleInfo(ruleNo);
            }
            return ds;
        }
        /// <summary>
        /// 通過客戶編號獲取客戶機種類型
        /// </summary>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public DataSet getMacByCus(string cusNo)
        {
            DataSet ds = selectControl.getMacTypeByCus(cusNo);
            return ds;
        }

        public DataSet getCusByCondition(string condition,string conditionV)
        {
            DataSet ds = null;
            if(condition == "1")
            {
                ds = selectControl.getCusInfoByCusNo(conditionV);
            }
            else
            {
                ds = selectControl.getCusInfoByCusName(conditionV);
            }

            return ds;
        }

        public DataSet getRulesByRuleNo(string ruleNo)
        {
            DataSet ds = selectControl.getRulesByRuleNo(ruleNo);
            return ds;
        }

        public void getOracleConn()
        {
            selectControl.getOracleConnection();
        }
    }
}