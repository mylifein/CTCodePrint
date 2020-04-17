﻿using DAL;
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
        private readonly SelectControl selectC = new SelectControl();
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
            if(dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
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
            if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
            ctCode.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ctCode.Opuser = Auxiliary.loginName;
            return printM.saveCTInfo(ctCode);
        }
        

      

        /// <summary>
        /// 根据流水码之前的编码查找最大流水号
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public string getMaxCTCode(string ctCode,string delMatno)
        {
            return printM.queryCodeNo(ctCode, delMatno);
        }

        /// <summary>
        /// 根据工单、PO、CT码中间码查询最大的CT码
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="workNo"></param>
        /// <param name="cusPo"></param>
        /// <returns></returns>
        public string queryInspurCodeNo(string ctCode, string workNo, String cusPo)
        {
            return printM.queryInspurCodeNo(ctCode, workNo, cusPo);
        }

        /// <summary>
        /// 根据客户PO 和CT 模糊查询最大的CT码
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="cusPo"></param>
        /// <returns></returns>
        public string queryInspurMaxCode(string ctCode, String cusPo)
        {
            return printM.queryInspurMaxCode(ctCode, cusPo);
        }


        /// <summary>
        /// 根據工單號查詢已經生成的CT碼數量
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public string getGeneratedCTCount(string workno)
        {
            return printM.getCTCount(workno);
        }

        /// <summary>
        /// 根據workno和subMaterial 查詢CT碼數量
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        public string getCTCountBySubMat(string workno, string subMaterial)
        {
            return printM.getCTCountBySubMat(workno, subMaterial);
        }

        public string getGeneratedCTCountByPO(string workno,string po)
        {
            return printM.getCTCountByPO(workno,po);
        }

        /// <summary>
        /// 保存打印CT碼記錄
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool savePrintRecord(CTCode ctCode)
        {
            ctCode.Uuid = Auxiliary.Get_UUID();
            ctCode.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ctCode.Opuser = Auxiliary.loginName;
            return printM.savePrintRecord(ctCode);
        }

        public bool savePrintRecordList(List<CTCode> ctCodeList)
        {
            foreach (CTCode ctCode in ctCodeList)
            {
                if (!this.savePrintRecord(ctCode))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通過CT碼或者工單號查詢CT碼信息
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="conditionV"></param>
        /// <returns></returns>
        public DataSet getCTInfo(string condition,string conditionV)
        {
            DataSet ds = null;
            if (condition == "1")
            {
                ds = printM.queryCodeInfoByWorkNo(conditionV);
            }
            else
            {
                ds = printM.queryCodeInfoByCT(conditionV);
            }

            return ds;
        }

        /// <summary>
        /// 保存編碼規則
        /// </summary>
        /// <param name="codeRule"></param>
        /// <returns></returns>
        public string saveRuleInfo(CodeRule codeRule)
        {
            bool mark = false;
            string saveResult = null;
            codeRule.Uuid = Auxiliary.Get_UUID();
            codeRule.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            codeRule.Opuser = Auxiliary.loginName;
            if (printM.saveRuleInfo(codeRule))
            {
                string RuleNo = printM.queryRuleByID(codeRule.Uuid);
                foreach(RuleItem item in codeRule.RuleItem){
                    item.Uuid = Auxiliary.Get_UUID();
                    item.Ruleno = RuleNo;
                    item.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    item.Opuser = Auxiliary.loginName;
                    mark = printM.saveSaveRuleItem(item);
                }
            }
            if (mark)
            {
                saveResult = printM.queryRuleByID(codeRule.Uuid);
            }
            return saveResult;
        }



        /// <summary>
        /// 根據規則號模糊查詢
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public DataSet queryCodeInfo(string ruleNo)
        {
            return printM.queryRules(ruleNo);
        }

        /// <summary>
        /// 根據規則號查詢規則信息
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public CodeRule queryCodeByNo(string ruleNo)
        {
            CodeRule codeR = null;
            DataSet ds = printM.queryRule(ruleNo);
            if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                codeR = new CodeRule();
                codeR.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                codeR.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                codeR.RuleDesc = ds.Tables[0].Rows[0]["rule_desc"].ToString();
                codeR.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                codeR.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
                DataSet dsRules = selectC.getRulesByRuleNo(codeR.Ruleno);
                if (dsRules != null && dsRules.Tables.Count > 0 && dsRules.Tables[0].Rows.Count > 0)
                {
                    List<RuleItem> itemList = new List<RuleItem>();
                    foreach(DataRow dr in dsRules.Tables[0].Rows)
                    {
                        RuleItem item = new RuleItem();
                        item.Uuid = dr["uuid"].ToString();
                        item.Ruleno = dr["rule_no"].ToString();
                        item.Seqno = dr["seq_no"].ToString();
                        item.Ruletype = dr["rule_type"].ToString();
                        item.Rulevalue = dr["rule_value"].ToString();
                        item.Rulelength = int.Parse(dr["rule_length"].ToString());
                        item.Opuser = dr["op_user"].ToString();
                        item.Createtime = dr["create_time"].ToString();
                        item.Updatetime = dr["update_time"].ToString();
                        itemList.Add(item);
                    }
                    codeR.RuleItem = itemList;
                }
            }
            return codeR;
        }

        /// <summary>
        /// 保存客戶幾種編碼規則
        /// </summary>
        /// <param name="cusRule"></param>
        /// <returns></returns>
        public bool saveCusCodeRule(CusRule cusRule)
        {
            cusRule.Uuid = Auxiliary.Get_UUID();
            cusRule.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cusRule.Opuser = Auxiliary.loginName;
            return printM.saveRuleRelation(cusRule);
        }

        /// <summary>
        /// 保存CT碼必填字段
        /// </summary>
        /// <param name="mandatoryF"></param>
        /// <returns></returns>
        public MandatoryInfo saveManField(MandatoryInfo mandatoryF)
        {
            MandatoryInfo reMandatory = null;
            mandatoryF.Uuid = Auxiliary.Get_UUID();
            mandatoryF.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mandatoryF.Opuser = Auxiliary.loginName;
            if (printM.saveMandatory(mandatoryF))
            {
                reMandatory = queryRemandatoryInfo(mandatoryF.Uuid);
            }
            return reMandatory;
        }
        
        public MandatoryInfo queryRemandatoryInfo(string uuid)
        {
            MandatoryInfo mandatoryInfo = null;
            DataSet ds = printM.queryMandatoryById(uuid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mandatoryInfo = new MandatoryInfo();
                mandatoryInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                mandatoryInfo.Manno = ds.Tables[0].Rows[0]["man_no"].ToString();
                mandatoryInfo.Mandesc = ds.Tables[0].Rows[0]["man_desc"].ToString();
                mandatoryInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                mandatoryInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return mandatoryInfo;
        }

        /// <summary>
        /// 模糊查詢所有必填字段規則
        /// </summary>
        /// <returns></returns>
        public DataSet queryMandatory()
        {
            return printM.queryMandatory("");
        }

        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public ModelInfo saveModelInfo(ModelInfo modelInfo)
        {
            ModelInfo reModel = null;
            modelInfo.Uuid = Auxiliary.Get_UUID();
            modelInfo.Createtime = DateTime.Now.ToString();
            modelInfo.Opuser = Auxiliary.loginName;
            if (printM.saveModelInfo(modelInfo))
            {
                reModel = queryModelInfo(modelInfo.Uuid);
            }
            return reModel;
        }


        /// <summary>
        /// 查詢打印模板信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public ModelInfo queryModelInfo(string uuid)
        {
            ModelInfo modelInfo = null;
            DataSet ds = printM.queryModelById(uuid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelInfo = new ModelInfo();
                modelInfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                modelInfo.Modelno = ds.Tables[0].Rows[0]["model_no"].ToString();
                modelInfo.Modelname = ds.Tables[0].Rows[0]["model_name"].ToString();
                modelInfo.Modeldesc = ds.Tables[0].Rows[0]["model_desc"].ToString();
                modelInfo.Manno = ds.Tables[0].Rows[0]["man_no"].ToString();
                modelInfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                modelInfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return modelInfo;
        }

        public MacTypeInfo saveMacTypeInfo(MacTypeInfo mactypeinfo)
        {
            MacTypeInfo reMacType = null;
            mactypeinfo.Uuid = Auxiliary.Get_UUID();
            mactypeinfo.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mactypeinfo.Opuser = Auxiliary.loginName;
            if (printM.saveMacTypeInfo(mactypeinfo))
            {
                reMacType = queryMacTypeInfo(mactypeinfo.Uuid);
            }
            return reMacType;
        }

        /// <summary>
        /// 根據ID 查詢機種類型信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public MacTypeInfo queryMacTypeInfo(string uuid)
        {
            MacTypeInfo mactypeinfo = null;
            DataSet ds = printM.queryMacTypeById(uuid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mactypeinfo = new MacTypeInfo();
                mactypeinfo.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                mactypeinfo.Mactypeno = ds.Tables[0].Rows[0]["mactypeno"].ToString();
                mactypeinfo.Mactypename = ds.Tables[0].Rows[0]["mactypename"].ToString();
                mactypeinfo.Mactypedesc = ds.Tables[0].Rows[0]["mactypedesc"].ToString();
                mactypeinfo.Ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
                mactypeinfo.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                mactypeinfo.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return mactypeinfo;
        }



        /// <summary>
        /// 模糊查詢所有模板信息
        /// </summary>
        /// <returns></returns>
        public DataSet queryModelInfo()
        {
            return printM.queryModelByModelNo("");
        }

        /// <summary>
        /// 保存打印模板关系信息
        /// </summary>
        /// <param name="modelRelation"></param>
        /// <returns></returns>
        public bool saveBoundModel(ModelRelation modelRelation)
        {
            modelRelation.Uuid = Auxiliary.Get_UUID();
            modelRelation.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            modelRelation.Opuser = Auxiliary.loginName;
            return printM.saveModelRelation(modelRelation);
        }

        public bool queryRepeatInModelRel(string cusno,string delmatno)
        {
            bool queryMark = false;
            string result = printM.getModelRelationCount(cusno, delmatno);
            if(result != null && result != "")
            {
                if(result == "0")
                {
                    queryMark = true;
                }
            }
            return queryMark;
        }

        /// <summary>
        /// uuid查詢ModelFile
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public ModelFile queryModelFileByUUID(string uuid)
        {
            ModelFile modelFile = null;
            DataSet ds = printM.queryModelFile(uuid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelFile = new ModelFile();
                modelFile.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                modelFile.Fileno = ds.Tables[0].Rows[0]["file_no"].ToString();
                modelFile.Filename = ds.Tables[0].Rows[0]["fileName"].ToString();
                modelFile.Filedescription = ds.Tables[0].Rows[0]["fileDescription"].ToString();
                //modelFile.Fileaddress = (byte[])ds.Tables[0].Rows[0]["fileAddress"];
                modelFile.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                modelFile.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return modelFile;
        }

        /// <summary>
        /// 通過模板號查詢modelfile
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public ModelFile queryModelFileByFileNo(string fileNo)
        {
            ModelFile modelFile = null;
            DataSet ds = printM.queryModelFileByNo(fileNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelFile = new ModelFile();
                modelFile.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                modelFile.Fileno = ds.Tables[0].Rows[0]["file_no"].ToString();
                modelFile.Filename = ds.Tables[0].Rows[0]["fileName"].ToString();
                modelFile.Filedescription = ds.Tables[0].Rows[0]["fileDescription"].ToString();
                modelFile.Fileaddress = (byte[])ds.Tables[0].Rows[0]["fileAddress"];
                modelFile.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                modelFile.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return modelFile;
        }

        public ModelFile queryModelFileByExactFileNo(string fileNo)
        {
            ModelFile modelFile = null;
            DataSet ds = printM.queryModelFileByExactNo(fileNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelFile = new ModelFile();
                modelFile.Uuid = ds.Tables[0].Rows[0]["uuid"].ToString();
                modelFile.Fileno = ds.Tables[0].Rows[0]["file_no"].ToString();
                modelFile.Filename = ds.Tables[0].Rows[0]["fileName"].ToString();
                modelFile.Filedescription = ds.Tables[0].Rows[0]["fileDescription"].ToString();
                modelFile.Fileaddress = (byte[])ds.Tables[0].Rows[0]["fileAddress"];
                modelFile.Opuser = ds.Tables[0].Rows[0]["op_user"].ToString();
                modelFile.Createtime = ds.Tables[0].Rows[0]["create_time"].ToString();
            }
            return modelFile;
        }


        public ModelFile saveModelFile(ModelFile modelFile)
        {
            ModelFile reModelFile = null;
            modelFile.Uuid = Auxiliary.Get_UUID();
            modelFile.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            modelFile.Opuser = Auxiliary.loginName;
            if (printM.saveModelFile(modelFile))
            {
                reModelFile = queryModelFileByUUID(modelFile.Uuid);
            }
            return reModelFile;
        }


        public ModelFile updateModelFile(ModelFile modelFile)
        {
            ModelFile reModelFile = null;
          
            modelFile.Updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            modelFile.Updateuser = Auxiliary.loginName;
            if (printM.updateModelFile(modelFile))
            {
                reModelFile = queryModelFileByExactFileNo(modelFile.Fileno);
            }
            return reModelFile;
        }
    }
}
