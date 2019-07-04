using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CodeRule
    {
        private string _uuid;
        private string _ruleno;
        private string _ruleDesc;
        private string _opuser;
        private string _createtime;
        private string _updatetime;
        private string _delflag;
        private List<RuleItem> _ruleItem;

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

        public string RuleDesc
        {
            get
            {
                return _ruleDesc;
            }

            set
            {
                _ruleDesc = value;
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

        public List<RuleItem> RuleItem
        {
            get
            {
                return _ruleItem;
            }

            set
            {
                _ruleItem = value;
            }
        }
    }
}
