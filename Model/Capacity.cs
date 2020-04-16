using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Capacity
    {

        private string _uuid;
        private string _capacityno;
        private int _capacityqty;
        private string capacitydesc;
        private string _opuser;
        private string _createtime;
        private string _updateser;
        private string _updatetime;
        private string _delflag;

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

        public string Capacityno
        {
            get
            {
                return _capacityno;
            }

            set
            {
                _capacityno = value;
            }
        }

        public int Capacityqty
        {
            get
            {
                return _capacityqty;
            }

            set
            {
                _capacityqty = value;
            }
        }

        public string Capacitydesc
        {
            get
            {
                return capacitydesc;
            }

            set
            {
                capacitydesc = value;
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
    }
}
