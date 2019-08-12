using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RoleUnionUser
    {
        private string _uuid;
        private string _roleno;
        private string _rolename;
        private string _username;
        private string _userdesc;
        private string _opuser;
        private string _createtime;
        private string _updatetime;

        public string Uuid
        {
            get
            {
                return _uuid;
            }

            set
            {
                _uuid = value;
            }
        }

        public string Roleno
        {
            get
            {
                return _roleno;
            }

            set
            {
                _roleno = value;
            }
        }

        public string Rolename
        {
            get
            {
                return _rolename;
            }

            set
            {
                _rolename = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
            }
        }

        public string Userdesc
        {
            get
            {
                return _userdesc;
            }

            set
            {
                _userdesc = value;
            }
        }

        public string Opuser
        {
            get
            {
                return _opuser;
            }

            set
            {
                _opuser = value;
            }
        }

        public string Createtime
        {
            get
            {
                return _createtime;
            }

            set
            {
                _createtime = value;
            }
        }

        public string Updatetime
        {
            get
            {
                return _updatetime;
            }

            set
            {
                _updatetime = value;
            }
        }
    }
}
