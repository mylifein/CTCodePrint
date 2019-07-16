using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MandatoryInfo
    {
        private string _uuid;
        private string _manno;
        private string _mandesc;
        private string _ctcodem;
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

        public string Manno
        {
            get
            {
                return _manno;
            }

            set
            {
                _manno = value;
            }
        }

        public string Mandesc
        {
            get
            {
                return _mandesc;
            }

            set
            {
                _mandesc = value;
            }
        }

        public string Ctcodem
        {
            get
            {
                return _ctcodem;
            }

            set
            {
                _ctcodem = value;
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
