using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Department
    {
        private string _uuid;
        private string _deptId;
        private string _deptName;
        private string _deptDesc;
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

        public string DeptId
        {
            get
            {
                return _deptId;
            }

            set
            {
                _deptId = value;
            }
        }

        public string DeptName
        {
            get
            {
                return _deptName;
            }

            set
            {
                _deptName = value;
            }
        }

        public string DeptDesc
        {
            get
            {
                return _deptDesc;
            }

            set
            {
                _deptDesc = value;
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
