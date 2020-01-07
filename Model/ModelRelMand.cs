using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ModelRelMand
    {
        private string _uuid;
        private string _fileNo;
        private string _manNo;
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

        public string ManNo
        {
            get
            {
                return _manNo;
            }

            set
            {
                _manNo = value;
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
