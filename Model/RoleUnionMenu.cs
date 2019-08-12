using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RoleUnionMenu
    {
        private string _uuid;
        private string _roleno;
        private string _rolename;
        private string _menuno;
        private string _menuname;
        private string _menudesc;
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

        public string Menuno
        {
            get
            {
                return _menuno;
            }

            set
            {
                _menuno = value;
            }
        }

        public string Menudesc
        {
            get
            {
                return _menudesc;
            }

            set
            {
                _menudesc = value;
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

        public string Menuname
        {
            get
            {
                return _menuname;
            }

            set
            {
                _menuname = value;
            }
        }
    }
}
