using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RoleRelMenu
    {
        private string _uuid;
        private string _roleno;
        private string _menuno;
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
