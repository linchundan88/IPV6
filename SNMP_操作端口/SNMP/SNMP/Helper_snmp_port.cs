using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;
using System.Net;

namespace SNMP
{
    class Helper_snmp_port
    {
        public static  Boolean op_port(string device_ip, string snmp_community_write,
            int snmp_port, int ifIndex, string op_state)
        {
            try
            {
                if (op_state == "Open")
                {
                    var result1 = Messenger.Set(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(device_ip), snmp_port),
                        new OctetString(snmp_community_write),
                        new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.7."+ifIndex.ToString() ),
                        new Integer32(1)) },
                        5000);
                }
                if (op_state == "Close")
                {
                    var result1 = Messenger.Set(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(device_ip), snmp_port),
                        new OctetString(snmp_community_write),
                        new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.7."+ifIndex.ToString() ),
                        new Integer32(2)) },
                        5000);
                }
            }
            catch (Exception ex)  //error in response
            {
                return false;
            }

            return true;

        }
    }
}
