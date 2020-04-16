using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RuleItem
    {
        private string _uuid;
        private string _ruleno;
        private string _seqno;
        private string _ruletype;
        private string _rulevalue;
        private int _rulelength;
        private string _opuser;
        private string _createtime;
        private string _updateUser;
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

        public string Seqno
        {
            get
            {
                return _seqno;
            }

            set
            {
                _seqno = value;
            }
        }

        public string Ruletype
        {
            get
            {
                return _ruletype;
            }

            set
            {
                _ruletype = value;
            }
        }

        public string Rulevalue
        {
            get
            {
                return _rulevalue;
            }

            set
            {
                _rulevalue = value;
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

        public string UpdateUser
        {
            get
            {
                return _updateUser;
            }

            set
            {
                _updateUser = value;
            }
        }

        public int Rulelength
        {
            get
            {
                return _rulelength;
            }

            set
            {
                _rulelength = value;
            }
        }
    }
}
