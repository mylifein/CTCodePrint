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
    public class CapacityService
    {
        private readonly CapacityDao capacityDao = new CapacityDao();

        /// <summary>
        /// TODO 保存箱号信息
        /// </summary>
        /// <param name="carton"></param>
        /// <returns></returns>
        public Capacity saveCapacity(Capacity capacity)
        {
            Capacity reCapacity = null;
            capacity.Uuid = Auxiliary.Get_UUID();
            capacity.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            capacity.Opuser = Auxiliary.loginName;
            if (capacityDao.saveCapacity(capacity))
            {
                reCapacity = capacityDao.queryCapacityById(capacity.Uuid);
            }
            return reCapacity;
        }



        /// <summary>
        /// TODO 保存容量與出貨料號關係
        /// </summary>
        /// <param name="ctRelCarton"></param>
        /// <returns></returns>
        public bool saveCapacityRelCus(CapacityRelCus capacityRelCus)
        {
            capacityRelCus.Uuid = Auxiliary.Get_UUID();
            capacityRelCus.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            capacityRelCus.OpUser = Auxiliary.loginName;

            return capacityDao.saveCapacityRelCus(capacityRelCus);
        }


        public List<Capacity> queryCapacityAll(string capacityNo)
        {
            return capacityDao.queryCapacityAll(capacityNo);
        }


        /// <summary>
        /// TODO 根據CT碼查詢是否已經 綁定
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public bool exists(CapacityRelCus capacityRelCus)
        {
            return capacityDao.exists(capacityRelCus);
        }

        /// <summary>
        /// 根據容量號查詢裝容量信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public Capacity queryCapacityByNo(string capacityNo)
        {
            return capacityDao.queryCapacityByNo(capacityNo);
        }

        /// <summary>
        /// 根据客户编号和出货料号以及绑定类型查询 容量
        /// </summary>
        /// <param name="capacityRelCus"></param>
        /// <returns></returns>
        public Capacity queryByRelation(string cusNo,string delMatno,string capacityType)
        {
            Capacity result = null;
            string capacityNo = capacityDao.queryCapacityNo(cusNo, delMatno, capacityType);
            if (capacityNo != null)
            {
                result = capacityDao.queryCapacityByNo(capacityNo);
            }
            return result;
        }

    }
}
