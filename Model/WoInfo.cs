using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WoInfo
    {
        private string uuid;
        private string woNo;
        private string delMatno;
        private string woQty;
        private string deptId;
        private string deptCode;
        private string classCode;
        private string completionSub;
        private string modelNo;
        private string delMatnoDesc;
        private string status;                  //工单状态
        private int checkTimes;
        private string opuser;
        private string createtime;
        private string updateser;
        private string updatetime;
        private string delflag;

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

        public string WoQty
        {
            get
            {
                return woQty;
            }

            set
            {
                woQty = value;
            }
        }

        public string DeptId
        {
            get
            {
                return deptId;
            }

            set
            {
                deptId = value;
            }
        }

        public string DeptCode
        {
            get
            {
                return deptCode;
            }

            set
            {
                deptCode = value;
            }
        }

        public string ClassCode
        {
            get
            {
                return classCode;
            }

            set
            {
                classCode = value;
            }
        }

        public string CompletionSub
        {
            get
            {
                return completionSub;
            }

            set
            {
                completionSub = value;
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

        public string DelMatnoDesc
        {
            get
            {
                return delMatnoDesc;
            }

            set
            {
                delMatnoDesc = value;
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

        public string Delflag
        {
            get
            {
                return delflag;
            }

            set
            {
                delflag = value;
            }
        }

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

        public int CheckTimes
        {
            get
            {
                return checkTimes;
            }

            set
            {
                checkTimes = value;
            }
        }
    }
}
