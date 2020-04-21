using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MandRelDel
    {
        private string _uuid;
        private string _manNo;
        private string _cusNo;
        private string _delMatno;
        private string _boundType;                  //value = 0 ,CT绑定.  1,装箱单绑定, 2,栈板绑定
        private string _opUser;
        private string _createTime;
        private string _updateUser;
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

        public string BoundType
        {
            get
            {
                return _boundType;
            }

            set
            {
                _boundType = value;
            }
        }

        public string UpdateUser
        {
            get
            {
                return _updateUser;
            }

            set
            {
                _updateUser = value;
            }
        }
    }
}
