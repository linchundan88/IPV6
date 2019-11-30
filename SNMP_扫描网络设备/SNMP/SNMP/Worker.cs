using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;
using System.Net;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace SNMP
{
    public enum Op_type { sysName, iftable, vlan, dot1BasePort, arp, mac_address };

    class Worker
    {
        private string ip { get; set; }
        private int port { get; set; }
        private string community_ro { get; set; }
        private string community_rw { get; set; }
        private string connString { get; set; }
        private Boolean get_arp { get; set; }
        private Boolean get_mac_address { get; set; }

        private Op_type op_type { get; set; }
        private int snmpTimeOut = 4000;
        private int snmpMaxRepetitions = 10;
        private int MinPoolSize = 1;
        

        public void init_Worker(string v_ip, int v_port, string v_community_ro,string v_community_rw,
            Boolean v_get_arp,Boolean v_get_mac_address,
            string v_connString,  DataTable dt_device,
            int v_MinPoolSize = 2, int v_snmpTimeOut= 4000,int v_snmpMaxRepetitions = 10)
        {
            ip = v_ip; port = v_port; community_ro = v_community_ro; community_rw = v_community_rw;
            connString = v_connString; 
            MinPoolSize = v_MinPoolSize;
            get_arp = v_get_arp;
            get_mac_address = v_get_mac_address;

            snmpTimeOut = v_snmpTimeOut;
            snmpMaxRepetitions = v_snmpMaxRepetitions;
        }


        void execute_StringSqlList(List<string> sqlList, int batch_num = 20, Boolean use_transaction = false)
        {
            string temp_connString = connString.Replace("MinPoolSize=1", "MinPoolSize=" + MinPoolSize.ToString());
            MySqlConnection myCon = new MySqlConnection(temp_connString);
            MySqlTransaction myTransaction = null;  //插入arp,mac address 因为要查询最新的纪录，很多条做一个Transaction会DeadLock

            string commandText = "";

            try
            {
                myCon.Open();

                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.CommandTimeout = 100; //默认30秒

                for (int i = 0; i < sqlList.Count; i++)
                {
                    //用iftable为例子， 一次提交记录数 10，20， 性能有比较大的提高
                    //例如iftable 用一次transaction，性能有5倍提高  但是arp,mac address用存储过程，一次transaction会deadlock
                    commandText = commandText + sqlList[i];
                    if ((i != 0) && (i % batch_num == 0) && (commandText != ""))
                    {
                        //MySqlCommand mySqlCommand = new MySqlCommand(commandText, myCon);
                        //下面 不用 每一次创建MySqlCommand 然后等待垃圾回收
                        mySqlCommand.Connection = myCon;
                        mySqlCommand.CommandText = commandText;
                        if (use_transaction == true)
                            myTransaction = myCon.BeginTransaction(IsolationLevel.ReadUncommitted);
                        mySqlCommand.ExecuteNonQuery();
                        if (use_transaction == true)
                            myTransaction.Commit();

                        commandText = "";
                    }
                }
                if (commandText != "")
                {
                    mySqlCommand.Connection = myCon;
                    mySqlCommand.CommandText = commandText;

                    if (use_transaction == true)
                        myTransaction = myCon.BeginTransaction(IsolationLevel.ReadUncommitted);
                    mySqlCommand.ExecuteNonQuery();
                    if (use_transaction == true)
                        myTransaction.Commit();

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            //有时候会timeout “Deadlock found when trying to get lock; try restarting transaction”
            {
                string error = ex.ToString();  //timeout
                return;
            }
            catch (Exception ex)
            {
                //应该没有其它异常，为了保险起见，加上下面异常处理
                string error = ex.ToString();
                return;
            }
            finally
            {
                if (myTransaction != null) myTransaction.Dispose();
                myCon.Close();
                myCon.Dispose();
            }
        }

        public void doWork()
        {
            List<string> sqlList = new List<string>();

            string temp_sql = "";
            
            try
            {
                #region 发送SNMP请求，sysName,sysContract获取返回结果
                //cisco 9,huawei 02011, 14823 aruba, H3C 25506                                            

                string sysObjectID = "";
                string sysName = "";
                string sysContract = "";

                var result0 = new List<Variable>();
                Messenger.BulkWalk(VersionCode.V2,
                    new IPEndPoint(IPAddress.Parse(ip), port),
                    new OctetString(community_ro),
                    new ObjectIdentifier("1.3.6.1.2.1.1"),
                    result0,
                    snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                if (result0.Count == 0) return;
                for (int i = 0; i < result0.Count; i++)
                {
                    if (result0[i].Id.ToString() == ".1.3.6.1.2.1.1.2.0")
                    {
                        sysObjectID = result0[i].Data.ToString();
                        sysObjectID = sysObjectID.Substring(1);
                    }

                    if (result0[i].Id.ToString() == ".1.3.6.1.2.1.1.4.0")
                        sysContract = result0[i].Data.ToString();
                    if (result0[i].Id.ToString() == ".1.3.6.1.2.1.1.5.0")
                        sysName = result0[i].Data.ToString();
                }

                #endregion

                int i_get_arp = 0;
                if (get_arp) i_get_arp = 1;

                int i_get_mac_address = 0;
                if (get_mac_address) i_get_mac_address = 1;

                temp_sql = string.Format("insert into device(device_ip,community_read,community_write,snmp_port,get_arp,get_mac_address,sysName,sysObjectID, sysContract) values ('{0}','{1}','{2}',{3} ,{4},{5},'{6}','{7}','{8}');",
                    ip,community_ro,community_rw, port.ToString(), i_get_arp.ToString(), i_get_mac_address.ToString(), sysName, sysObjectID,sysContract );
                sqlList.Add(temp_sql);

                temp_sql = string.Format("insert into log(device_ip,log_content) values  ('{0}','{1}'); ",
                    ip, "扫描添加网络设备--成功");
                sqlList.Add(temp_sql);
                execute_StringSqlList(sqlList); //批量执行SQL语句 
            }
            catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
            {
                string error_msg = ex.ToString();
                return;
            }
            catch (Exception ex)
            {
                string error_msg = ex.ToString();
                return;
            }
        }

    }
}
