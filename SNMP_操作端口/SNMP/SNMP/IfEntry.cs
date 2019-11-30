using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMP
{
    class IfEntry
    {
        public int ifIndex { get; set; }
        public string ifDescr { get; set; }
        public string PhysAddress { get; set; }
        public long ifSpeed { get; set; }
        public int ifAdminStatus { get; set; }
        public int ifOpenStatus { get; set; }       
            
        public IfEntry()
        {
            ifDescr = ""; PhysAddress = ""; ifSpeed = 0; ifAdminStatus = 0; ifOpenStatus = 0;
        }
    }


}
