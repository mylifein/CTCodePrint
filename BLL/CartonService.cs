using DAL;
using DBUtility;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CartonService
    {
        private readonly CartonDao cartonDao = new CartonDao();

        /// <summary>
        /// TODO 保存箱号信息
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool saveCarton(Carton carton)
        {
            bool mark = true;
            carton.Uuid = Auxiliary.Get_UUID();
            carton.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            carton.Opuser = Auxiliary.loginName;
            if (carton.PackType.Equals("0"))
            {
                foreach (string ctcode in carton.CtCodeList)
                {
                    CtRelCarton ctRelCarton = new CtRelCarton();
                    ctRelCarton.Ctcode = ctcode;
                    ctRelCarton.CartonNo = carton.CartonNo;
                    mark = saveCartonRelation(ctRelCarton);
                    if (!mark)
                    {
                        return mark;
                    }
                }
            }
            return cartonDao.saveCarton(carton);
        }

        /// <summary>
        /// TODO 
        /// </summary>
        /// <param name="prefCartonNo"></param>
        /// <returns></returns>
        public string getMaxCartonNo(string prefCartonNo)
        {
            return cartonDao.queryMaxCartonNo(prefCartonNo);
        }

        /// <summary>
        /// 根据工单查询最大流水号
        /// </summary>
        /// <param name="workNo"></param>
        /// <returns></returns>
        public string getMaxCartonNoByWO(string workNo)
        {
            return cartonDao.getMaxCartonNoByWO(workNo);
        }


        /// <summary>
        /// TODO 保存裝箱單號與CT碼的關係
        /// </summary>
        /// <param name="ctRelCarton"></param>
        /// <returns></returns>
        public bool saveCartonRelation(CtRelCarton ctRelCarton)
        {
            ctRelCarton.Uuid = Auxiliary.Get_UUID();
            ctRelCarton.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ctRelCarton.Opuser = Auxiliary.loginName;
            return cartonDao.saveCartonRelation(ctRelCarton);
        }

        /// <summary>
        /// TODO 根據CT碼查詢是否已經 綁定
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public bool exists(String ctCode)
        {
            return cartonDao.exists(ctCode);
        }

        /// <summary>
        /// TODO 根據工單查詢裝箱數量
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public string getCartonQtyByWO(string workno)
        {
            return cartonDao.getCartonQtyByWO(workno);
        }

        /// <summary>
        /// 根據箱號查詢裝箱單信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public Carton queryCartonByCartonNo(string cartonNo)
        {
            return cartonDao.queryCartonByCartonNo(cartonNo);
        }


        public Carton queryCartonDetailsByNo(string cartonNo)
        {
            Carton carton = cartonDao.queryCartonByCartonNo(cartonNo);
            List<CtRelCarton> ctRelCartons = cartonDao.queryRelCTByCartonNo(cartonNo);
            if(ctRelCartons != null)
            {
                List<String> ctList = new List<string>();
                foreach (CtRelCarton ctRelCarton in ctRelCartons)
                {
                    ctList.Add(ctRelCarton.Ctcode);
                }
                carton.CtCodeList = ctList;
            }
            return carton;
        }

        /// <summary>
        /// 计算已装箱数
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public int getCartonsByWO(string workno)
        {
            return cartonDao.getCartonsByWO(workno);
        }

        /// <summary>
        /// 更新裝箱單狀態, 1是已綁定棧板，2是入庫 3是出庫 
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public bool updateCartonStatus(Carton carton, int status)
        {
            carton.Updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            carton.Updateser = Auxiliary.loginName;
            return cartonDao.updateCartonStatus(carton, status);
        }

        /// <summary>
        /// 查詢模板號
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="delMatno"></param>
        /// <param name="boundType"></param>
        /// <returns></returns>
        public string queryFileNo(string cusNo, string delMatno, string boundType)
        {
            return cartonDao.queryFileNo(cusNo, delMatno, boundType);
        }






        /// <summary>
        /// 根据工单计算批次
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <param name="workno"></param>
        /// <param name="cuspo"></param>
        /// <returns></returns>
        public string queryBatchNoWorkNo(string cartonNo, string workno)
        {
            return cartonDao.queryBatchNoWorkNo(cartonNo, workno);
        }

        public string queryInspurMaxBoxNo(string cartonNo, string cusno)
        {
            return cartonDao.queryInspurMaxBox(cartonNo,cusno);
        }

        public int queryCurrentBoxQty(String workNo)
        {
            return cartonDao.queryCurrentBoxQty(workNo);
        }


        public int currentBoxQtyByCuspo(String cusPo,String delMatno)
        {
            return cartonDao.currentBoxQtyByCuspo(cusPo,delMatno);
        }


        /// <summary>
        /// 根据工单号  查询一条装箱单信息.
        /// </summary>
        /// <param name="workNo"></param>
        /// <returns></returns>
        public Carton queryCartonByWorkno(string workNo)
        {
            return cartonDao.queryCartonByWorkno(workNo);
        }



        public List<Carton> getCartonsInfo(string condition, string conditionV)
        {
            if (condition == "1")
            {
                return cartonDao.querygetCartonsInfoByWorkNo(conditionV);
            }
            else
            {
                return cartonDao.querygetCartonsInfoByCartonNo(conditionV);
            }
        }
    }
}
