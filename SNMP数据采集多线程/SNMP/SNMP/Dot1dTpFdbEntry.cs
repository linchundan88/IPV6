using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMP
{
    class Dot1dTpFdbEntry
    {
        public string dot1dTpFdbAddress { get; set; } = "";
        public int dot1dTpFdbPort { get; set; } = 0;
        public int dot1dTpFdbStatus { get; set; } = 0;
        public int vlan { get; set; } = 0;

        public Dot1dTpFdbEntry()
        {
            dot1dTpFdbAddress = ""; dot1dTpFdbPort = 0; dot1dTpFdbStatus = 0; vlan = 0;
        }

    }
}
