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


        public bool saveCartonByTrans(Carton carton)
        {
            //1.获得数据库连接.
            MySqlConnection conn = new MySqlConnection(SQLHelper.ConnectionString);

            //2.打开数据库连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //3.开启事务.
            MySqlTransaction mysqlTrans = conn.BeginTransaction();
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
                    ctRelCarton.Uuid = Auxiliary.Get_UUID();
                    ctRelCarton.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ctRelCarton.Opuser = Auxiliary.loginName;
                    mark = cartonDao.saveCartonRelationByTrans(ctRelCarton, conn, mysqlTrans);
                    if (!mark)
                    {
                        conn.Close();
                        return mark;
                    }
                }
            }
            mark = cartonDao.saveCartonByTrans(carton, conn, mysqlTrans);
            if (!mark)
            {
                conn.Close();
                return mark;
            }
            mysqlTrans.Commit();
            conn.Close();
            return mark;
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
        /// TODO 查询同工单，同PO 已装箱数量
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="cusPo"></param>
        /// <returns></returns>
        public string getCartonQtyByWoAndCusPo(string workno,string cusPo)
        {
            return cartonDao.getCartonQtyByWoAndCusPo(workno, cusPo);
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
        /// TODO 根据工单查询已装箱数
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public int getCartonsByWO(string workno)
        {
            return cartonDao.getCartonsByWO(workno);
        }

        /// <summary>
        /// TODO 根据工单和客户PO 查询已装箱数
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="cusPo"></param>
        /// <returns></returns>
        public int getCartonsByWoAndCusPo(string workno, string cusPo)
        {
            return cartonDao.getCartonsByWoAndCusPo(workno,cusPo);
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


        public int currentBoxQtyByCuspo(string cusPo,string delMatno,string woNo)
        {
            return cartonDao.currentBoxQtyByCuspo(cusPo,delMatno,woNo);
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



        public DataSet getCartonsInfo(string condition, string conditionV)
        {
            if (condition == "1")
            {
                return cartonDao.getCartonsDSByWO(conditionV);
            }
            else
            {
                return cartonDao.getCartonDSByWO(conditionV);
            }
        }
    }
}
