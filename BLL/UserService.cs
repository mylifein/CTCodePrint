using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserService
    {
        private readonly DAL.UserDao userDAL = new DAL.UserDao();
        public bool checkLogin(string username,string password)
        {
            return userDAL.exists(username, password);
        }

        public bool saveLoginInfo(string username,string ipAddress)
        {
            return userDAL.saveLoginInfo(username,ipAddress);
        }

        public Model.User queryByUsername(string username)
        {
            return userDAL.queryUser(username);
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User saveUser(User user)
        {
            User reUser = null;
            user.Uuid = Auxiliary.Get_UUID();
            user.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            user.Opuser = Auxiliary.loginName;
            if (userDAL.saveUser(user))
            {
                reUser = userDAL.queryUserById(user.Uuid);
            }
            return reUser;
        }


        public bool isExist(string username)
        {
            if(userDAL.queryUser(username)!= null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}
