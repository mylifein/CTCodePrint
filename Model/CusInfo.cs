using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CusInfo
    {
        private string _cusNo;
        private string _cusName;

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

        public string CusName
        {
            get
            {
                return _cusName;
            }

            set
            {
                _cusName = value;
            }
        }
    }
}
