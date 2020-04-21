using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CusMatInfo
    {
        private string _cusId;
        private string _cusItemCode;
        private string _cusItemDesc;
        private string _itemCode;
        private string _itemDesc;

        public string CusId
        {
            get
            {
                return _cusId;
            }

            set
            {
                _cusId = value;
            }
        }

        public string CusItemCode
        {
            get
            {
                return _cusItemCode;
            }

            set
            {
                _cusItemCode = value;
            }
        }

        public string CusItemDesc
        {
            get
            {
                return _cusItemDesc;
            }

            set
            {
                _cusItemDesc = value;
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
    }
}
