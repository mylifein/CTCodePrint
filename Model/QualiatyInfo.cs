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
        private DateTime startTime;
        private DateTime endTime;
        private long duringTime;
        private string status;
        private string ruleNo;
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

        public DateTime StartTime
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

        public DateTime EndTime
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

        public long DuringTime
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
    }
}
