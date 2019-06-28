using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PrintModelQ
    {
        private readonly DAL.PrintModel printM = new DAL.PrintModel();

        /// <summary>
        /// 獲得模板號，通過客戶編號和出貨料號
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="delMatNo"></param>
        /// <returns></returns>
        public string checkPrintModelRel(string cusNo,string delMatNo)
        {
            string modelNo = null;
            DataSet dt = printM.queryModelNo(cusNo, delMatNo);
            if(dt != null && dt.Tables[0].Rows.Count > 0)
            {
                modelNo = dt.Tables[0].Rows[0]["model_No"].ToString();
            }

            return modelNo;
        }



        /// <summary>
        /// 獲得模板信息通過模板號
        /// </summary>
        /// <param name="modelNo"></param>
        /// <returns></returns>
        public DataSet getModelInfo(string modelNo)
        {
            return printM.queryModelInfo(modelNo);
        }

        /// <summary>
        /// 獲得必填信息通過必填編號
        /// </summary>
        /// <param name="mandNo"></param>
        /// <returns></returns>
        public DataSet getMandatoryInfo(string mandNo)
        {
            return printM.queryMandatoryInfo(mandNo);
        }

       /// <summary>
       /// 通过模板号获得必填字段信息
       /// </summary>
       /// <param name="modelNo"></param>
       /// <returns></returns>
        public MandatoryField getMandInfoByMod(string modelNo)
        {
            MandatoryField manF = new MandatoryField();
            DataSet ds = printM.queryModelInfo(modelNo);
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string manNo = ds.Tables[0].Rows[0]["man_no"].ToString();
                ds = printM.queryMandatoryInfo(manNo);
                if(ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    manF.Ctcodem = ds.Tables[0].Rows[0]["ctcode_m"].ToString();
                }
            }

            return manF;
        }

        public bool saveCTCodeInfo(CTCode ctCode)
        {
            ctCode.Uuid = Auxiliary.Get_UUID();
            ctCode.Createtime = DateTime.Now.ToString();
            ctCode.Opuser = Auxiliary.loginName;
            return printM.saveCTInfo(ctCode);
        }

        /// <summary>
        /// 根据流水码之前的编码查找最大流水号
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public string getMaxCTCode(string ctCode)
        {
            return printM.queryCodeNo(ctCode);
        }
    }
}
