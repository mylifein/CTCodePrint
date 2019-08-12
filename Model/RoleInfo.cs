using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RoleInfo
    {
        private string _uuid;
        private string _roleno;
        private string _rolename;
        private string _roledesc;
        private string _opuser;
        private string _createtime;
        private string _updatetime;
        private string _delflag;

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

        public string Roledesc
        {
            get
            {
                return _roledesc;
            }

            set
            {
                _roledesc = value;
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

        public string Delflag
        {
            get
            {
                return _delflag;
            }

            set
            {
                _delflag = value;
            }
        }
    }
}
