using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FileRelDel
    {
        private string _uuid;
        private string _fileNo;
        private string _cusNo;
        private string _delMatno;
        private string _opUser;
        private string _createTime;
        private string _updateTime;
        private string _delFlag;

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

        public string FileNo
        {
            get
            {
                return _fileNo;
            }

            set
            {
                _fileNo = value;
            }
        }

        public string CusNo
        {
            get
            {
                return _cusNo;
            }

            set
            {
                _cusNo = value;
            }
        }

        public string DelMatno
        {
            get
            {
                return _delMatno;
            }

            set
            {
                _delMatno = value;
            }
        }

        public string OpUser
        {
            get
            {
                return _opUser;
            }

            set
            {
                _opUser = value;
            }
        }

        public string CreateTime
        {
            get
            {
                return _createTime;
            }

            set
            {
                _createTime = value;
            }
        }

        public string UpdateTime
        {
            get
            {
                return _updateTime;
            }

            set
            {
                _updateTime = value;
            }
        }

        public string DelFlag
        {
            get
            {
                return _delFlag;
            }

            set
            {
                _delFlag = value;
            }
        }
    }
}
