using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CusRule
    {
        private string _uuid;
        private string _cusno;
        private string _delmatno;
        private string _ruleno;
        private string _boundtype;
        private string _opuser;
        private string _createtime;
        private string _updateuser;
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

        public string Cusno
        {
            get
            {
                return _cusno;
            }

            set
            {
                _cusno = value;
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


        public string Boundtype
        {
            get
            {
                return _boundtype;
            }

            set
            {
                _boundtype = value;
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

        public string Updateuser
        {
            get
            {
                return _updateuser;
            }

            set
            {
                _updateuser = value;
            }
        }
    }
}
