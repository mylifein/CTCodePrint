using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WoRevision             //工单版本信息
    {
        private string _orgId;
        private string _itemCode;
        private string _itemDesc;
        private string _revision;
        private string _effectDate;
        private string _implDate;

        public string OrgId
        {
            get
            {
                return _orgId;
            }

            set
            {
                _orgId = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return _itemCode;
            }

            set
            {
                _itemCode = value;
            }
        }

        public string ItemDesc
        {
            get
            {
                return _itemDesc;
            }

            set
            {
                _itemDesc = value;
            }
        }

        public string Revision
        {
            get
            {
                return _revision;
            }

            set
            {
                _revision = value;
            }
        }

        public string EffectDate
        {
            get
            {
                return _effectDate;
            }

            set
            {
                _effectDate = value;
            }
        }

        public string ImplDate
        {
            get
            {
                return _implDate;
            }

            set
            {
                _implDate = value;
            }
        }
    }
}
