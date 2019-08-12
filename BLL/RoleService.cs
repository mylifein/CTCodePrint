using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class RoleService
    {
        private readonly RoleDao roleDao = new RoleDao();

        public RoleInfo saveRoleInfo(RoleInfo roleInfo)
        {
            RoleInfo reRoleInfo = null;
            roleInfo.Uuid = Auxiliary.Get_UUID();
            roleInfo.Opuser = Auxiliary.loginName;
            roleInfo.Createtime = Auxiliary.Get_CurrentTime();
            if (roleDao.saveRoleInfo(roleInfo))
            {
                reRoleInfo = roleDao.queryMenuInfoById(roleInfo.Uuid);
            }
            return reRoleInfo;
        }

        /// <summary>
        /// 根據roleNo 模糊查詢Role
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        public List<RoleInfo> queryRoleInfoList(string roleNo)
        {
            return roleDao.queryRoleInfoList(roleNo);
        }
    }
}
