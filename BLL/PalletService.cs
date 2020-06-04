using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PalletService
    {
        private readonly PalletDao palletDao = new PalletDao();
        private readonly CartonService cartonService = new CartonService();


        /// <summary>
        /// 保存栈板信息
        /// </summary>
        /// <param name="pallet"></param>
        /// <returns></returns>
        public bool savePallet(Pallet pallet)
        {
            bool mark = true;
            pallet.Uuid = Auxiliary.Get_UUID();
            pallet.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            pallet.Opuser = Auxiliary.loginName;

            foreach (Carton carton in pallet.CartonList)
            {
                CartonRelPallet cartonRelPallet = new CartonRelPallet();
                cartonRelPallet.CartonNo = carton.CartonNo;
                cartonRelPallet.PalletNo = pallet.PalletNo;
                mark = savePalletRelation(cartonRelPallet);
                if (!mark)
                {
                    return mark;
                }
                mark = cartonService.updateCartonStatus(carton, 1);
                if (!mark)
                {
                    return mark;
                }
            }

            return palletDao.savePallet(pallet);
        }






        /// <summary>
        /// 保存栈板和装箱单关系
        /// </summary>
        /// <param name="cartonRelPallet"></param>
        /// <returns></returns>
        public bool savePalletRelation(CartonRelPallet cartonRelPallet)
        {
            cartonRelPallet.Uuid = Auxiliary.Get_UUID();
            cartonRelPallet.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cartonRelPallet.Opuser = Auxiliary.loginName;
            return palletDao.savePalletRelation(cartonRelPallet);
        }

        /// <summary>
        /// 根據PalletNo查詢最大裝箱單號
        /// </summary>
        /// <param name="prefPalletNo"></param>
        /// <returns></returns>
        public string queryMaxPalletNo(string prefPalletNo)
        {
            return palletDao.queryMaxPalletNo(prefPalletNo);
        }


        /// <summary>
        /// 根据工单查询栈板信息
        /// </summary>
        /// <param name="workNo"></param>
        /// <returns></returns>
        public Pallet queryPalletByWorkno(string workNo)
        {
            return palletDao.queryPalletByWorkno(workNo);
        }


        public List<string> queryBatchNos(string batchCond)
        {
            return palletDao.queryBatchNos(batchCond);
        }


        public List<Pallet> queryPalletsByWorkNo(string workNo)
        {
            return palletDao.queryPalletsByWorkNo(workNo);
        }


        public List<Pallet> getPalletsByCond(string condition, string conditionV)
        {
            if (condition == "1")
            {
                return palletDao.queryPalletsByWorkNo(conditionV);
            }
            else
            {
                return palletDao.queryPalletsByPalletkNo(conditionV);
            }
        }



        public Pallet queryPalletByPalletNo(string palletNo)
        {
            return palletDao.queryPalletByPalletNo(palletNo);
        }

    }
}
