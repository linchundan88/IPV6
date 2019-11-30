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

    class SnmpWorker
    {
        private int snmp_operate_request_id { get; set; }
        private string ip { get; set; }
        private int port { get; set; }
        private string community { get; set; }
        private string connString { get; set; }

        private string op_type { get; set; }
        public int snmpTimeOut = 5000;
        public int snmpMaxRepetitions = 20;
        private int MinPoolSize = 1;

        public void init_Worker(string v_ip, int v_port, string v_community, string v_connString,
             int v_snmpTimeOut = 5000, int v_snmpMaxRepetitions = 20)
        {            
            ip = v_ip; port = v_port; community = v_community; connString = v_connString;
            snmpTimeOut = v_snmpTimeOut;
            snmpMaxRepetitions = v_snmpMaxRepetitions;
        }

        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
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

            #region 重新获取 ifTable 分多次获取SNMP  虚拟接口 有些数据项可能没有
           
                try
                {
                    #region 发送SNMP请求，获取返回结果  
                    //ifIndex
                    var result1 = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.1"),
                        result1,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    if (result1.Count == 0) return;

                    //ifDescr
                    var result2 = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2"),
                        result2,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //ifSpeed
                    var result5 = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.5"),
                        result5,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //PhysAddress
                    var result6 = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.6"),
                        result6,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //ifAdminStatus
                    var result7 = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.7"),
                        result7,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //ifOpenStatus
                    var result8 = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.8"),
                        result8,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    List<IfEntry> listIfEntry = new List<IfEntry>();
                    for (int i = 0; i < result1.Count; i++)
                    {
                        try
                        {
                            IfEntry ifEntry = new IfEntry();
                            ifEntry.ifIndex = int.Parse(result1[i].Data.ToString());
                            listIfEntry.Add(ifEntry);
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    //ifDescr
                    for (int i = 0; i < result2.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result2[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.2.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifDescr = result2[i].Data.ToString();
                                    break; //找到了，跳出循环
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    //ifSpeed
                    for (int i = 0; i < result5.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result5[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.5.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifSpeed = long.Parse(result5[i].Data.ToString());
                                    break; //找到了，跳出循环
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    //ifPhysAddress
                    for (int i = 0; i < result6.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result6[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.6.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    if (result6[i].Data.ToString() != "")
                                    {
                                        byte[] mac_bytes = result6[i].Data.ToBytes();  //MAC地址
                                                                                       //string 的length是6， ToBytes变成8  0406 
                                        string temp_PhysAddress = byteToHexStr(mac_bytes);
                                        //ifEntry.PhysAddress = temp_PhysAddress;
                                        ifEntry.PhysAddress = temp_PhysAddress.Substring(4);
                                    }
                                    break; //找到了，跳出循环
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    //ifAdminStatus
                    for (int i = 0; i < result7.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result7[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.7.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifAdminStatus = int.Parse(result7[i].Data.ToString());
                                    break; //找到了，跳出循环
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    //ifOperStatus
                    for (int i = 0; i < result8.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result8[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.8.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifOpenStatus = int.Parse(result8[i].Data.ToString());
                                    break; //找到了，跳出循环
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    #endregion

                    #region 根据SNMP结果生成SQL语句列表

                    //接口 和 VLAN 先delete，然后insert就ok了，不用每一个判断exists update 
                    //后来因为增加历史数据删除，预留指定交换机级联口功能，编写了存储过程
                    //temp_sql = string.Format("delete from iftable where device_ip='{0}';", ip);
                    //sqlList.Add(temp_sql);

                    foreach (var ifEntry in listIfEntry)
                    {
                        //temp_sql = string.Format("insert into iftable (device_ip,ifIndex,ifDescr,ifSpeed,PhysAddress,ifAdminStatus,ifOperStatus) values('{0}',{1},'{2}',{3},'{4}',{5},{6} ); ",
                        // ip, ifEntry.ifIndex.ToString(), ifEntry.ifDescr, ifEntry.ifSpeed.ToString(), ifEntry.PhysAddress,
                        // ifEntry.ifAdminStatus.ToString(), ifEntry.ifOpenStatus.ToString());

                        temp_sql = string.Format("call insert_ifTable  ('{0}',{1},'{2}',{3},'{4}',{5},{6} ); ",
                            ip, ifEntry.ifIndex.ToString(), ifEntry.ifDescr, ifEntry.ifSpeed.ToString(), ifEntry.PhysAddress,
                             ifEntry.ifAdminStatus.ToString(), ifEntry.ifOpenStatus.ToString());
                        sqlList.Add(temp_sql);
                    }

                    temp_sql = string.Format("insert into  log(device_ip,log_content) values  ('{0}','{1}'); ",
                        ip, "获取网络设备接口--成功");
                    sqlList.Add(temp_sql);

                    #endregion

                    execute_StringSqlList(sqlList, 50, false); //批量执行SQL语句
                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',102); ", ip);
                sqlList.Clear();
                sqlList.Add(temp_sql);

                execute_StringSqlList(sqlList); //批量执行SQL语句 
                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }
          
            #endregion
        }        
    }
}
