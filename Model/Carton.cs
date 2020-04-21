using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Carton
    {

        private string _uuid;
        private string _cartonNo;
        private int _cartonQty;
        private string _cartonStatus;           // 狀態標識; 0 剛產生， 1，入庫 2，OQC ok， 3. OQC NG 4.出庫
        private string _capacityNo;
        private string _capacityQty;
        private string _batchNo;                //浪潮批次號
        private int _boxNo;                     //工单箱数
        private string _ruleno;
        private string _workno;
        private string _cusno;
        private string _cusname;
        private string _cuspo;
        private string _orderqty;
        private string _cusmatno;
        private string _cusmatDesc;             //客户料号描述
        private string _unionField;             //顺达联合字段
        private string _delmatno;
        private string _offino;
        private string _verno;
        private string _woquantity;
        private string _completedqty;
        private string _modelno;
        private string _soOrder;
        private string _packType;                   //包裝類型 ：0 機箱 整機。  1.單出
        private string _opuser;
        private string _createtime;
        private string _updateser;
        private string _updatetime;
        private string _delflag;
        private ProdLine _prodLine;
        private List<String> ctCodeList;
        private string _datecode;
        private string _ct1;
        private string _ct2;
        private string _ct3;
        private string _ct4;
        private string _ct5;
        private string _ct6;
        private string _ct7;
        private string _ct8;
        private string _ct9;
        private string _ct10;
        private string specialField;
        private string _prodLineVal;
        


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

        public string CartonNo
        {
            get
            {
                return _cartonNo;
            }

            set
            {
                _cartonNo = value;
            }
        }

        public int CartonQty
        {
            get
            {
                return _cartonQty;
            }

            set
            {
                _cartonQty = value;
            }
        }

        public string CartonStatus
        {
            get
            {
                return _cartonStatus;
            }

            set
            {
                _cartonStatus = value;
            }
        }

        public string CapacityNo
        {
            get
            {
                return _capacityNo;
            }

            set
            {
                _capacityNo = value;
            }
        }

        public string Ruleno
        {
            get
            {
                return _ruleno;
            }

            set
            {
                _ruleno = value;
            }
        }

        public string Workno
        {
            get
            {
                return _workno;
            }

            set
            {
                _workno = value;
            }
        }

        public string Cusno
        {
            get
            {
                return _cusno;
            }

            set
            {
                _cusno = value;
            }
        }

        public string Cuspo
        {
            get
            {
                return _cuspo;
            }

            set
            {
                _cuspo = value;
            }
        }

        public string Orderqty
        {
            get
            {
                return _orderqty;
            }

            set
            {
                _orderqty = value;
            }
        }

        public string Cusmatno
        {
            get
            {
                return _cusmatno;
            }

            set
            {
                _cusmatno = value;
            }
        }

        public string Delmatno
        {
            get
            {
                return _delmatno;
            }

            set
            {
                _delmatno = value;
            }
        }


        public string Offino
        {
            get
            {
                return _offino;
            }

            set
            {
                _offino = value;
            }
        }

        public string Verno
        {
            get
            {
                return _verno;
            }

            set
            {
                _verno = value;
            }
        }

        public string Woquantity
        {
            get
            {
                return _woquantity;
            }

            set
            {
                _woquantity = value;
            }
        }

        public string Completedqty
        {
            get
            {
                return _completedqty;
            }

            set
            {
                _completedqty = value;
            }
        }

        public string Modelno
        {
            get
            {
                return _modelno;
            }

            set
            {
                _modelno = value;
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

        public string Updateser
        {
            get
            {
                return _updateser;
            }

            set
            {
                _updateser = value;
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

        public ProdLine ProdLine
        {
            get
            {
                return _prodLine;
            }

            set
            {
                _prodLine = value;
            }
        }

        public string Ct1
        {
            get
            {
                return _ct1;
            }

            set
            {
                _ct1 = value;
            }
        }

        public string Ct2
        {
            get
            {
                return _ct2;
            }

            set
            {
                _ct2 = value;
            }
        }

        public string Ct3
        {
            get
            {
                return _ct3;
            }

            set
            {
                _ct3 = value;
            }
        }

        public string Ct4
        {
            get
            {
                return _ct4;
            }

            set
            {
                _ct4 = value;
            }
        }

        public string Ct5
        {
            get
            {
                return _ct5;
            }

            set
            {
                _ct5 = value;
            }
        }

        public string Ct6
        {
            get
            {
                return _ct6;
            }

            set
            {
                _ct6 = value;
            }
        }

        public string Ct7
        {
            get
            {
                return _ct7;
            }

            set
            {
                _ct7 = value;
            }
        }

        public string Ct8
        {
            get
            {
                return _ct8;
            }

            set
            {
                _ct8 = value;
            }
        }

        public string Ct9
        {
            get
            {
                return _ct9;
            }

            set
            {
                _ct9 = value;
            }
        }

        public string Ct10
        {
            get
            {
                return _ct10;
            }

            set
            {
                _ct10 = value;
            }
        }

        public string Cusname
        {
            get
            {
                return _cusname;
            }

            set
            {
                _cusname = value;
            }
        }

        public List<string> CtCodeList
        {
            get
            {
                return ctCodeList;
            }

            set
            {
                ctCodeList = value;
            }
        }

        public string PackType
        {
            get
            {
                return _packType;
            }

            set
            {
                _packType = value;
            }
        }

        public string Datecode
        {
            get
            {
                return _datecode;
            }

            set
            {
                _datecode = value;
            }
        }

        public string CapacityQty
        {
            get
            {
                return _capacityQty;
            }

            set
            {
                _capacityQty = value;
            }
        }

        public string BatchNo
        {
            get
            {
                return _batchNo;
            }

            set
            {
                _batchNo = value;
            }
        }


        public int BoxNo
        {
            get
            {
                return _boxNo;
            }

            set
            {
                _boxNo = value;
            }
        }

        public string CusmatDesc
        {
            get
            {
                return _cusmatDesc;
            }

            set
            {
                _cusmatDesc = value;
            }
        }

        public string UnionField
        {
            get
            {
                return _unionField;
            }

            set
            {
                _unionField = value;
            }
        }

        public string SpecialField
        {
            get
            {
                return specialField;
            }

            set
            {
                specialField = value;
            }
        }

        public string ProdLineVal
        {
            get
            {
                return _prodLineVal;
            }

            set
            {
                _prodLineVal = value;
            }
        }
    }
}
