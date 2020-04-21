using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WorkOrderInfo
    {
        private string _orgId;
        private string _cusItemNum;
        private string _workNo;
        private string _startQty;               //工单数量
        private string _completedQty;           //完工数量
        private string _soOrder;
        private string _itemCode;
        private string _itemDesc;
        private string _custPO;
        private string _custId;
        private string _custName;
        private string _orderQty;





        public string WorkNo
        {
            get
            {
                return _workNo;
            }

            set
            {
                _workNo = value;
            }
        }

        public string StartQty
        {
            get
            {
                return _startQty;
            }

            set
            {
                _startQty = value;
            }
        }

        public string CompletedQty
        {
            get
            {
                return _completedQty;
            }

            set
            {
                _completedQty = value;
            }
        }

        public string SoOrder
        {
            get
            {
                return _soOrder;
            }

            set
            {
                _soOrder = value;
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



        public string CustId
        {
            get
            {
                return _custId;
            }

            set
            {
                _custId = value;
            }
        }

        public string CustName
        {
            get
            {
                return _custName;
            }

            set
            {
                _custName = value;
            }
        }

        public string OrderQty
        {
            get
            {
                return _orderQty;
            }

            set
            {
                _orderQty = value;
            }
        }

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

        public string CusItemNum
        {
            get
            {
                return _cusItemNum;
            }

            set
            {
                _cusItemNum = value;
            }
        }

        public string CustPO
        {
            get
            {
                return _custPO;
            }

            set
            {
                _custPO = value;
            }
        }
    }
}
