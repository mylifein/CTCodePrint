using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WorkOrderInfo
    {
        private string _organizationId;
        private string _customerItemNumber;
        private string _workNo;
        private string _startQty;               //工单数量
        private string _completedQty;           //完工数量
        private string _soOrder;
        private string _itemCode;
        private string _itemDesc;
        private string _custPONuber;
        private string _custId;
        private string _custName;
        private string _orderQty;

        public string OrganizationId
        {
            get
            {
                return _organizationId;
            }

            set
            {
                _organizationId = value;
            }
        }

        public string CustomerItemNumber
        {
            get
            {
                return _customerItemNumber;
            }

            set
            {
                _customerItemNumber = value;
            }
        }

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

        public string CustPONuber
        {
            get
            {
                return _custPONuber;
            }

            set
            {
                _custPONuber = value;
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
    }
}
