﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Pallet
    {
        private string _uuid;
        private string _palletNo;
        private int _palletQty;
        private string _workno;
        private string _ruleno;
        private string _cusno;
        private string _cusname;
        private string _cuspo;
        private string _cusmatno;
        private List<Carton> _cartonList;
        private string _delmatno;
        private string _modelno;
        private string _soOrder;
        private string _vehicleNo;
        private string _opuser;
        private string _createtime;
        private string _updateser;
        private string _updatetime;
        private string _delflag;
        private string _capacityNo;
        private string _batchNo;

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

        public string PalletNo
        {
            get
            {
                return _palletNo;
            }

            set
            {
                _palletNo = value;
            }
        }

        public int PalletQty
        {
            get
            {
                return _palletQty;
            }

            set
            {
                _palletQty = value;
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

        public List<Carton> CartonList
        {
            get
            {
                return _cartonList;
            }

            set
            {
                _cartonList = value;
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

        public string VehicleNo
        {
            get
            {
                return _vehicleNo;
            }

            set
            {
                _vehicleNo = value;
            }
        }
    }
}
