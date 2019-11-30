using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMP
{
    class IpNetToPhysical
    {
        public string oID { get; set; }

        public string ipNetToPhysicalPhysAddress { get; set; } = "";
        public int ipNetToPhysicalType { get; set; } = 0;
        public int ipNetToPhysicalState { get; set; } = 0;
        public int ipNetToPhysicalRowStatus { get; set; } = 0;

        public string IP { get; set; } 
        public string MAC { get; set; } 

        public IpNetToPhysical()
        {
            oID = ""; ipNetToPhysicalPhysAddress = "";
            ipNetToPhysicalType = 0; ipNetToPhysicalState = 0;  ipNetToPhysicalRowStatus = 0;

            IP = ""; MAC = "";
        }

    }

}
