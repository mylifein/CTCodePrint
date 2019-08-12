using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class RoleRelUserService
    {
        private readonly RoleRelUserDao roleRelUserDao = new RoleRelUserDao();

        /// <summary>
        /// 保存roleRelUser
        /// </summary>
        /// <param name="roleRelUser"></param>
        /// <returns></returns>
        public bool saveRoleRelUser(RoleRelUser roleRelUser)
        {
            RoleRelUser reRoleRelUser = null;
            roleRelUser.Uuid = Auxiliary.Get_UUID();
            roleRelUser.Opuser = Auxiliary.loginName;
            roleRelUser.Createtime = Auxiliary.Get_CurrentTime();
            return roleRelUserDao.saveRoleRelUser(roleRelUser);
        }

        /// <summary>
        /// 刪除用戶對應的菜單
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool deleteRoleRelMenu(string uuid)
        {
            return roleRelUserDao.deleteRoleRelUser(uuid);
        }

        /// <summary>
        /// 根據userId 查詢聯合表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RoleUnionUser> queryRoleUionUserList(string userId)
        {
            return roleRelUserDao.queryRoleUnionUserList(userId);
        }

        /// <summary>
        /// 檢查該用戶角色是否存在
        /// </summary>
        /// <param name="roleRelUser"></param>
        /// <returns></returns>
        public bool checkAdd(RoleRelUser roleRelUser)
        {
            return roleRelUserDao.exists(roleRelUser);
        }

    }
}
