using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MenuInfo
    {
        private string _uuid;
        private string _menuNo;
        private string _menuName;
        private string _menuDescription;
        private string _opuser;
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

        public string MenuNo
        {
            get
            {
                return _menuNo;
            }

            set
            {
                _menuNo = value;
            }
        }

        public string MenuName
        {
            get
            {
                return _menuName;
            }

            set
            {
                _menuName = value;
            }
        }

        public string MenuDescription
        {
            get
            {
                return _menuDescription;
            }

            set
            {
                _menuDescription = value;
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
