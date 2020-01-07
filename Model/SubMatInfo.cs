using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SubMatInfo
    {
        private string _uuid;
        private string _delmatno;
        private string _submatno;
        private string _reldesc;
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

        public string Delmatno
        {
            get
            {
                return _delmatno;
            }

            set
            {
                _delmatno = value;
            }
        }

        public string Submatno
        {
            get
            {
                return _submatno;
            }

            set
            {
                _submatno = value;
            }
        }

        public string Reldesc
        {
            get
            {
                return _reldesc;
            }

            set
            {
                _reldesc = value;
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
