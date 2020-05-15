using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WoBatch
    {
        private string _uuid;
        private string _workno;
        private string _batchNo;
        private string _batchType;
        private string _opuser;
        private string _createtime;
        private string _updateser;
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

        public string BatchNo
        {
            get
            {
                return _batchNo;
            }

            set
            {
                _batchNo = value;
            }
        }

        public string BatchType
        {
            get
            {
                return _batchType;
            }

            set
            {
                _batchType = value;
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

        public string Updateser
        {
            get
            {
                return _updateser;
            }

            set
            {
                _updateser = value;
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

        public string Workno
        {
            get
            {
                return _workno;
            }

            set
            {
                _workno = value;
            }
        }
    }
}
