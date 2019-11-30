using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMP
{
    class Dot1dTpFdbEntry
    {
        public string dot1dTpFdbAddress { get; set; }
        public int dot1dTpFdbPort { get; set; }
        public int dot1dTpFdbStatus { get; set; }
        public int vlan { get; set; }

    }
}
