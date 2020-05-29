using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class QualityInfo
    {
        private string uuid;
        private string qualiatyNo;
        private string woNo;
        private DateTime? startTime;
        private DateTime? endTime;
        private long? duringTime;
        private string status;                                  //0：代表未质检   1：代表质检中   2：代表质检合格   3：代表质检不合格
        private string ruleNo;
        private string deliverMan;
        private string delMatno;
        private string modelNo;
        private string datecode;
        private string remark;
        private string opuser;
        private string createtime;
        private string updateser;
        private string updatetime;

        public string Uuid
        {
            get
            {
                return uuid;
            }

            set
            {
                uuid = value;
            }
        }

        public string QualiatyNo
        {
            get
            {
                return qualiatyNo;
            }

            set
            {
                qualiatyNo = value;
            }
        }

        public string WoNo
        {
            get
            {
                return woNo;
            }

            set
            {
                woNo = value;
            }
        }


        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public string Opuser
        {
            get
            {
                return opuser;
            }

            set
            {
                opuser = value;
            }
        }

        public string Createtime
        {
            get
            {
                return createtime;
            }

            set
            {
                createtime = value;
            }
        }

        public string Updateser
        {
            get
            {
                return updateser;
            }

            set
            {
                updateser = value;
            }
        }

        public string Updatetime
        {
            get
            {
                return updatetime;
            }

            set
            {
                updatetime = value;
            }
        }

        public string RuleNo
        {
            get
            {
                return ruleNo;
            }

            set
            {
                ruleNo = value;
            }
        }

        public string DeliverMan
        {
            get
            {
                return deliverMan;
            }

            set
            {
                deliverMan = value;
            }
        }

        public string DelMatno
        {
            get
            {
                return delMatno;
            }

            set
            {
                delMatno = value;
            }
        }

        public string ModelNo
        {
            get
            {
                return modelNo;
            }

            set
            {
                modelNo = value;
            }
        }

        public string Datecode
        {
            get
            {
                return datecode;
            }

            set
            {
                datecode = value;
            }
        }

        public DateTime? StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
            }
        }

        public long? DuringTime
        {
            get
            {
                return duringTime;
            }

            set
            {
                duringTime = value;
            }
        }

        public string Remark
        {
            get
            {
                return remark;
            }

            set
            {
                remark = value;
            }
        }
    }
}
