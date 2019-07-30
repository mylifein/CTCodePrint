using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ModelFile
    {
        private string _uuid;
        private string _fileno;
        private string _filename;
        private string _filedescription;
        private byte[] _fileaddress;
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

        public string Fileno
        {
            get
            {
                return _fileno;
            }

            set
            {
                _fileno = value;
            }
        }

        public string Filename
        {
            get
            {
                return _filename;
            }

            set
            {
                _filename = value;
            }
        }

        public string Filedescription
        {
            get
            {
                return _filedescription;
            }

            set
            {
                _filedescription = value;
            }
        }

        public byte[] Fileaddress
        {
            get
            {
                return _fileaddress;
            }

            set
            {
                _fileaddress = value;
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
