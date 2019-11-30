using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMP
{
    class IpNetToMediaEntry
    {
        public int ipNetToMediaIfIndex { get; set; }  //1.1.3.6.1.2.1.4.22.1.1
        public string ipNetToMediaPhysAddress { get; set; }  //1.1.3.6.1.2.1.4.22.1.2
        public string ipNetToMediaNetAddress { get; set; } //1.1.3.6.1.2.1.4.22.1.3
        public int ipNetMediaType { get; set; } //1.1.3.6.1.2.1.4.22.1.4

        public string oID { get; set; }  // .1.3.6.1.2.1.4.22.1.2.29.10.29.2.15  两个key 29, 10.29.2.15 
    }
}
