using DAL;
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
        /// 根据流水码之前的编码查找最大流水号
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public string getMaxCTCode(string ctCode)
        {
            return printM.queryCodeNo(ctCode);
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
                modelFile.Updateuser = ds.Tables[0].Rows[0]["update_user"].ToString();
                modelFile.Updatetime = ds.Tables[0].Rows[0]["update_time"].ToString();
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
