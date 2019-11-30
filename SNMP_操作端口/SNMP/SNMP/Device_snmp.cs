using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMP
{
    class Device_snmp
    {
        public string device_ip { get; set; }
        public string community_read { get; set; }
        public string community_write { get; set; }
        public int snmp_port { get; set; }
        public string sysObjectID { get; set; }
    }
}
