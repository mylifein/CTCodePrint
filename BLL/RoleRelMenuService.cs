using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class RoleRelMenuService
    {
        private readonly RoleRelMenuDao roleRelMenuDao = new RoleRelMenuDao();


        public RoleRelMenu saveRoleRelMenu(RoleRelMenu roleRelMenu)
        {
            RoleRelMenu reRoleRelMenu = null;
            roleRelMenu.Uuid = Auxiliary.Get_UUID();
            roleRelMenu.Opuser = Auxiliary.loginName;
            roleRelMenu.Createtime = Auxiliary.Get_CurrentTime();
            if (roleRelMenuDao.saveRoleRelMenu(roleRelMenu))
            {
                reRoleRelMenu = roleRelMenuDao.queryMenuInfoById(roleRelMenu.Uuid);
            }
            return reRoleRelMenu;
        }

        /// <summary>
        /// 根據uuid 刪除角色對應的菜單
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool deleteRoleRelMenu(string uuid)
        {
            return roleRelMenuDao.deleteRoleRelMenu(uuid);
        }
        /// <summary>
        /// 根據roleNo查詢
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        public List<RoleRelMenu> queryRoleRelMenuList(string roleNo)
        {
            return roleRelMenuDao.queryRoleRelMenuList(roleNo);
        }

        /// <summary>
        /// 根據roleNo查詢聯合表
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        public List<RoleUnionMenu> queryRoleUionMenuList(string roleNo)
        {
            return roleRelMenuDao.queryRoleUnionMenuList(roleNo);
        }

        /// <summary>
        /// 檢查該菜單是否已經添加
        /// </summary>
        /// <param name="roleRelMenu"></param>
        /// <returns></returns>
        public bool checkAdd(RoleRelMenu roleRelMenu)
        {
            return roleRelMenuDao.exists(roleRelMenu);
        }
    }
}
