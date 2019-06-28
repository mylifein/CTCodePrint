using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User
    {
        private readonly DAL.User userDAL = new DAL.User();
        public bool checkLogin(string username,string password)
        {
            return userDAL.exists(username, password);
        }

        public bool saveLoginInfo(string username,string ipAddress)
        {
            return userDAL.saveLoginInfo(username,ipAddress);
        }
    }

}
