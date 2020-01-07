using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FieldType
    {
        private string _uuid;
        private string _fieldNo;
        private string _fieldName;
        private string _fieldValue;
        private string _fieldDesc;
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

        public string FieldNo
        {
            get
            {
                return _fieldNo;
            }

            set
            {
                _fieldNo = value;
            }
        }

        public string FieldValue
        {
            get
            {
                return _fieldValue;
            }

            set
            {
                _fieldValue = value;
            }
        }

        public string FieldDesc
        {
            get
            {
                return _fieldDesc;
            }

            set
            {
                _fieldDesc = value;
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

        public string FieldName
        {
            get
            {
                return _fieldName;
            }

            set
            {
                _fieldName = value;
            }
        }
    }
}
