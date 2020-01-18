using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProdLine
    {
        private string _uuid;
        private string _prodlineId;
        private string _prodlineName;
        private string _prodlineDesc;
        private Department _department;
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

        public string ProdlineName
        {
            get
            {
                return _prodlineName;
            }

            set
            {
                _prodlineName = value;
            }
        }

        public string ProdlineDesc
        {
            get
            {
                return _prodlineDesc;
            }

            set
            {
                _prodlineDesc = value;
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

        public string ProdlineId
        {
            get
            {
                return _prodlineId;
            }

            set
            {
                _prodlineId = value;
            }
        }

        public Department Department
        {
            get
            {
                return _department;
            }

            set
            {
                _department = value;
            }
        }
    }
}
