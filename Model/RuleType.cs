using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RuleType
    {
        private string _uuid;
        private string _typeNo;
        private string _typeDesc;
        private string _opuser;
        private string _createtime;
        private string _updateUser;
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

        public string TypeNo
        {
            get
            {
                return _typeNo;
            }

            set
            {
                _typeNo = value;
            }
        }

        public string TypeDesc
        {
            get
            {
                return _typeDesc;
            }

            set
            {
                _typeDesc = value;
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

        public string UpdateUser
        {
            get
            {
                return _updateUser;
            }

            set
            {
                _updateUser = value;
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
