using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ProdLineService
    {
        private readonly ProdLineDao prodLineDao = new ProdLineDao();

        /// <summary>
        /// TODO 保存线别信息
        /// </summary>
        /// <param name="prodLine"></param>
        /// <returns></returns>
        public ProdLine saveProdLine(ProdLine prodLine)
        {
            ProdLine reProdLine = null;
            prodLine.Uuid = Auxiliary.Get_UUID();
            prodLine.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            prodLine.Opuser = Auxiliary.loginName;
            if (prodLineDao.saveProdLine(prodLine))
            {
                reProdLine = prodLineDao.queryProdLineByUUId(prodLine.Uuid);
            }
            return reProdLine;
        }

        /// <summary>
        /// TODO 查询所有线别信息
        /// </summary>
        /// <param name="prodLineId"></param>
        /// <returns></returns>
        public List<ProdLine> queryProdLineList(string prodLineId)
        {
            return prodLineDao.queryProdLineList(prodLineId);
        }



        /// <summary>
        /// TODO 查询所有线别信息
        /// </summary>
        /// <param name="prodLineId"></param>
        /// <returns></returns>
        public List<ProdLine> queryPLByDeptId(string depId)
        {
            return prodLineDao.queryPLByDeptId(depId);
        }


        /// <summary>
        /// TODO 根据线别ID 查询线别信息
        /// </summary>
        /// <param name="prodLineId"></param>
        /// <returns></returns>
        public ProdLine queryProdLineById(string prodLineId)
        {
            return prodLineDao.queryProdLineById(prodLineId);
        }
    }
}
