using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MenuService
    {
        private readonly MenuDao menuDao = new MenuDao();

        public MenuInfo saveMenuInfo(MenuInfo menuInfo)
        {
            MenuInfo reMenuInfo = null;
            menuInfo.Uuid = Auxiliary.Get_UUID();
            menuInfo.Opuser = Auxiliary.loginName;
            menuInfo.CreateTime = Auxiliary.Get_CurrentTime();
            if (menuDao.saveMenuInfo(menuInfo))
            {
                reMenuInfo = menuDao.queryMenuInfoById(menuInfo.Uuid);
            }
            return reMenuInfo;
        }

        public List<MenuInfo> queryMenuInfoList(string menuNo)
        {
            return menuDao.queryMenuInfoList(menuNo);
        }
    }
}
