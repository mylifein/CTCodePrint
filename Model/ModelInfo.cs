using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModelInfo
    {
        private string _uuid;
        private string _modelno;
        private string _modelname;
        private string _modeldesc;
        private string _modelpath;
        private string _manno;
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

        public string Modelno
        {
            get
            {
                return _modelno;
            }

            set
            {
                _modelno = value;
            }
        }

        public string Modelname
        {
            get
            {
                return _modelname;
            }

            set
            {
                _modelname = value;
            }
        }

        public string Modeldesc
        {
            get
            {
                return _modeldesc;
            }

            set
            {
                _modeldesc = value;
            }
        }

        public string Modelpath
        {
            get
            {
                return _modelpath;
            }

            set
            {
                _modelpath = value;
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
