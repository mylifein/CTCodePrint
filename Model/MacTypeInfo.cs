using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MacTypeInfo
    {
        private string _uuid;
        private string _mactypeno;
        private string _mactypename;
        private string _mactypedesc;
        private string _ruleno;
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

        public string Mactypeno
        {
            get
            {
                return _mactypeno;
            }

            set
            {
                _mactypeno = value;
            }
        }

        public string Mactypename
        {
            get
            {
                return _mactypename;
            }

            set
            {
                _mactypename = value;
            }
        }

        public string Mactypedesc
        {
            get
            {
                return _mactypedesc;
            }

            set
            {
                _mactypedesc = value;
            }
        }

        public string Ruleno
        {
            get
            {
                return _ruleno;
            }

            set
            {
                _ruleno = value;
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
