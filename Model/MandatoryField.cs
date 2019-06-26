using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MandatoryField
    {
        private string _ctcodem;

        public string Ctcodem
        {
            get
            {
                return _ctcodem;
            }

            set
            {
                _ctcodem = value;
            }
        }
    }
}
