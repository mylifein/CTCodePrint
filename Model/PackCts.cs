using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PackCts
    {
        private string batchNo;
        private List<CTCode> ctList;

        public string BatchNo
        {
            get
            {
                return batchNo;
            }

            set
            {
                batchNo = value;
            }
        }

        public List<CTCode> CtList
        {
            get
            {
                return ctList;
            }

            set
            {
                ctList = value;
            }
        }
    }
}
