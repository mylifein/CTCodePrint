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
                    ctRelCarton.Uuid = Auxiliary.Get_UUID();
                    ctRelCarton.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ctRelCarton.Opuser = Auxiliary.loginName;
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
    }
}
