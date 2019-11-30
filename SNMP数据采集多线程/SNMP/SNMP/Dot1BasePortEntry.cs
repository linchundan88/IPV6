using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMP
{
    class Dot1BasePortEntry
    {
        public int dot1dBasePort { get; set; } 
        public int dot1dBasePortIfIndex { get; set; } 

        public Dot1BasePortEntry()
        {
            dot1dBasePort = 0; dot1dBasePortIfIndex = 0;  
        }

    }
}
