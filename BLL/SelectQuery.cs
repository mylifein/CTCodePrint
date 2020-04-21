using System;
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
        public DataSet getRulesByNo(string macNo)
        {
            DataSet ds = null;
            if(macNo != null && macNo.Trim() != "")
            {
                ds = selectControl.getRuleByCus(macNo);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string ruleNo = ds.Tables[0].Rows[0]["rule_no"].ToString();
                    ds = selectControl.getRuleInfo(ruleNo);
                }
            }
            return ds;
        }


        public DataSet getRulesByActualNo(string ruleNo)
        {
            DataSet ds = null;
            ds = selectControl.getRuleInfo(ruleNo);
            return ds;
        }



        public DataSet getRulesByRuleNo(string ruleNo)
        {
            DataSet ds = selectControl.getRulesByRuleNo(ruleNo);
            return ds;
        }

        /// <summary>
        /// 獲得所有規則類型
        /// </summary>
        /// <returns></returns>
        public DataSet getAllRuleTypes()
        {
            DataSet ds = selectControl.getRuleTypes();
            return ds;
        }


    }
}
