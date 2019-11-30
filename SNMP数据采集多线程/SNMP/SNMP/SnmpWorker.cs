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
    public enum Op_type { sysName, iftable, vlan, dot1BasePort, arp, arp_new, mac_address };

    class SnmpWorker
    {
        private LogOption logOption { get; set; }
        private string ip { get; set; }
        private int port { get; set; }
        private string community { get; set; }
        private string connString { get; set; }

        private Op_type op_type { get; set; }

        private int snmpTimeOut = 500;
        private int snmpMaxRepetitions = 20;

        private int MinPoolSize = 1;
        private string sysObjectID = "";

        public void init_Worker(LogOption v_LogOption, string v_ip, int v_port, string v_community, string v_connString, Op_type v_op_type,
            int v_MinPoolSize = 2, string v_sysObjectID = "", int v_snmpTimeOut = 5000, int v_snmpMaxRepetitions = 20)
        {
            logOption = v_LogOption;
            ip = v_ip; port = v_port; community = v_community; connString = v_connString; op_type = v_op_type;
            MinPoolSize = v_MinPoolSize;
            sysObjectID = v_sysObjectID;

            snmpTimeOut = v_snmpTimeOut;
            snmpMaxRepetitions = v_snmpMaxRepetitions;
        }

        string byteToHexStr(byte[] bytes)
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

            //为了效率 ifTable是发送多次SNMP（每次获取一个字段，有些字段没用），而
            //sysName, ARP是发送一次请求整个表
            #region sysName
            if (op_type == Op_type.sysName)
            {
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
                        new OctetString(community),
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

                    temp_sql = string.Format("update device set sysObjectID='{0}',sysContract='{1}',sysName='{2}'  where device_ip='{3}';",
                        sysObjectID, sysContract, sysName, ip);
                    sqlList.Add(temp_sql);
                    //获取网络设备的厂商和设备名称--成功"
                    if (logOption.log_sysobjectid_success == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',1); ", ip);
                        sqlList.Add(temp_sql);
                    }

                    execute_StringSqlList(sqlList); //批量执行SQL语句 

                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    if (logOption.log_sysobjectid_failure == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',101); ", ip);
                        sqlList.Clear();
                        sqlList.Add(temp_sql);

                        execute_StringSqlList(sqlList); //批量执行SQL语句 
                    }

                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }
            }
            #endregion

            #region ifTable 分多次获取SNMP  不一次取全部的数据是为了性能, 虚拟接口 有些数据项可能没有
            if (op_type == Op_type.iftable)
            {
                try
                {
                    #region 发送SNMP请求，获取返回结果  
                    //ifIndex
                    var result_ifIndex = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.1"),
                        result_ifIndex,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    if (result_ifIndex.Count == 0) return;

                    //ifDescr
                    var result_ifDescr = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2"),
                        result_ifDescr,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //ifSpeed
                    var result_ifSpeed = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.5"),
                        result_ifSpeed,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //PhysAddress
                    var result_PhysAddress = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.6"),
                        result_PhysAddress,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //ifAdminStatus
                    var result_ifAdminStatus = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.7"),
                        result_ifAdminStatus,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    //ifOpenStatus
                    var result_ifOpenStatus = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.2.2.1.8"),
                        result_ifOpenStatus,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    #endregion

                    #region  因为是多次获取表的某一个字段，将获取的结果解析，合并
                    List<IfEntry> listIfEntry = new List<IfEntry>();
                    for (int i = 0; i < result_ifIndex.Count; i++)
                    {
                        try
                        {
                            IfEntry ifEntry = new IfEntry();
                            ifEntry.ifIndex = int.Parse(result_ifIndex[i].Data.ToString());
                            listIfEntry.Add(ifEntry);
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    //ifDescr
                    for (int i = 0; i < result_ifDescr.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result_ifDescr[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.2.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifDescr = result_ifDescr[i].Data.ToString();
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
                    for (int i = 0; i < result_ifSpeed.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result_ifSpeed[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.5.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifSpeed = long.Parse(result_ifSpeed[i].Data.ToString());
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
                    for (int i = 0; i < result_PhysAddress.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result_PhysAddress[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.6.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    if (result_PhysAddress[i].Data.ToString() != "")
                                    {
                                        byte[] mac_bytes = result_PhysAddress[i].Data.ToBytes();  //MAC地址
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
                    for (int i = 0; i < result_ifAdminStatus.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result_ifAdminStatus[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.7.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifAdminStatus = int.Parse(result_ifAdminStatus[i].Data.ToString());
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
                    for (int i = 0; i < result_ifOpenStatus.Count; i++)
                    {
                        try
                        {
                            int tempIfIndex = int.Parse(result_ifOpenStatus[i].Id.ToString().Replace(".1.3.6.1.2.1.2.2.1.8.", ""));

                            foreach (var ifEntry in listIfEntry)
                            {
                                if (ifEntry.ifIndex == tempIfIndex)
                                {
                                    ifEntry.ifOpenStatus = int.Parse(result_ifOpenStatus[i].Data.ToString());
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

                    if (logOption.log_iftable_success == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',2); ", ip);
                        sqlList.Add(temp_sql);
                    }

                    #endregion

                    execute_StringSqlList(sqlList, 50, false); //批量执行SQL语句
                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    if (logOption.log_iftable_failure == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',102); ", ip);
                        sqlList.Clear();
                        sqlList.Add(temp_sql);

                        execute_StringSqlList(sqlList); //批量执行SQL语句 
                    }

                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }
            }
            #endregion

            #region VLAN
            if (op_type == Op_type.vlan)
            {
                try
                {
                    #region 发送SNMP请求，获取返回结果，并且生成SQL语句列表

                    temp_sql = string.Format("delete from vlan where device_ip='{0}';", ip);
                    sqlList.Add(temp_sql);

                    if (!sysObjectID.Contains("1.3.6.1.4.1.9."))
                    {
                        #region MIB 非Cisco设备 VLAN dot1qVlanStatic说明
                        //1.3.6.1.2.1.17.7.1.4.3.1.1(dot1qVlanStaticName)
                        //1.3.6.1.2.1.17.7.1.4.3.1.2(dot1qVlanStaticEgressPorts)
                        //1.3.6.1.2.1.17.7.1.4.3.1.3(dot1qVlanForbiddenEgressPorts)
                        //1.3.6.1.2.1.17.7.1.4.3.1.4(dot1qVlanStaticUntaggedPorts)
                        //1.3.6.1.2.1.17.7.1.4.3.1.5(dot1qVlanStaticRowStatus)     
                        #endregion

                        #region   发送SNMP请求，解析数据
                        var result1 = new List<Variable>();
                        Messenger.BulkWalk(VersionCode.V2,
                            new IPEndPoint(IPAddress.Parse(ip), port),
                            new OctetString(community),
                            new ObjectIdentifier("1.3.6.1.2.1.17.7.1.4.3.1.1"),
                            result1,
                            snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                        //接口 和 VLAN 先delete，然后insert就ok了，不用每一个判断exists update     
                        for (int i = 0; i < result1.Count; i++)
                        {
                            //1.3.6.1.2.1.17.7.1.4.3.1.1.58	  最后是VLAN 
                            string dot1qVlanIndex = result1[i].Id.ToString();  //.1.3.6.1.2.1.17.7.1.4.3.1.1
                            dot1qVlanIndex = dot1qVlanIndex.Replace(".1.3.6.1.2.1.17.7.1.4.3.1.1.", "");
                            string dot1qVlanStaticName = result1[i].Data.ToString();
                            dot1qVlanStaticName = dot1qVlanStaticName.ToUpper();
                            dot1qVlanStaticName = dot1qVlanStaticName.Replace(" ", "");
                            //dot1qVlanStaticName = dot1qVlanStaticName.Replace("VLAN", "");
                            //dot1qVlanStaticName = dot1qVlanStaticName.Replace(" ", "");

                            //1.3.6.1.2.1.17.7.1.4.3.1.1   dot1qVlanStaticName   
                            temp_sql = string.Format("insert into vlan (device_ip,vlan,name) values('{0}',{1},'{2}' ); ",
                                ip, dot1qVlanIndex, dot1qVlanStaticName);
                            sqlList.Add(temp_sql);
                        }

                        #endregion
                    }
                    else      //Cisco设备
                    {
                        #region 说明 MIB CISCO-VTP-MIB VtpVlan
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.1(vtpVlanIndex)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.2(vtpVlanState)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.3(vtpVlanType)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.4(vtpVlanName)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.5(vtpVlanMtu)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.6(vtpVlanDot10Said)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.7(vtpVlanRingNumber)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.8(vtpVlanBridgeNumber)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.9(vtpVlanStpType)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.10(vtpVlanParentVlan)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.11(vtpVlanTranslationalVlan1)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.12(vtpVlanTranslationalVlan2)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.13(vtpVlanBridgeType)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.14(vtpVlanAreHopCount)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.15(vtpVlanSteHopCount)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.16(vtpVlanIsCRFBackup)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.17(vtpVlanTypeExt)
                        //1.3.6.1.4.1.9.9.46.1.3.1.1.18(vtpVlanIfIndex)

                        //vtpVlanState 1.3.6.1.4.1.9.9.46.1.3.1.1.2    1 : operational 2 : suspended 3 : mtuTooBigForDevice 4 : mtuTooBigForTrunk
                        //vtpVlanType 1.3.6.1.4.1.9.9.46.1.3.1.1.3    VlanType  1:ethernet  2:fddi  3:tokenRing  4:fddiNet  5:trNet  6:deprecated
                        //vtpVlanName 1.3.6.1.4.1.9.9.46.1.3.1.1.4 

                        #endregion

                        #region Cisco设备 发送SNMP请求，解析数据

                        var resultCisco3 = new List<Variable>();
                        Messenger.BulkWalk(VersionCode.V2,
                            new IPEndPoint(IPAddress.Parse(ip), port),
                            new OctetString(community),
                            new ObjectIdentifier("1.3.6.1.4.1.9.9.46.1.3.1.1.3"),
                            resultCisco3,
                            snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);
                        if (resultCisco3.Count == 0) return;

                        var resultCisco4 = new List<Variable>();
                        Messenger.BulkWalk(VersionCode.V2,
                            new IPEndPoint(IPAddress.Parse(ip), port),
                            new OctetString(community),
                            new ObjectIdentifier("1.3.6.1.4.1.9.9.46.1.3.1.1.4"),
                            resultCisco4,
                            snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                        for (int i = 0; i < resultCisco3.Count; i++)
                        {
                            try
                            {
                                string tempOid = resultCisco3[i].Id.ToString();
                                //下一行可以删除，因为是分开获取MIB，肯定的
                                if (!tempOid.Contains(".1.3.6.1.4.1.9.9.46.1.3.1.1.3.1.")) continue;

                                int vlan3 = int.Parse(tempOid.Replace(".1.3.6.1.4.1.9.9.46.1.3.1.1.3.1.", ""));
                                string vtpVlanName = "";
                                int vtpVlanTye = int.Parse(resultCisco3[i].Data.ToString());
                                for (int j = 0; j < resultCisco4.Count; j++)
                                {
                                    string tempOid4 = resultCisco4[i].Id.ToString();
                                    //下一行可以删除，因为是分开获取MIB，肯定的
                                    if (!tempOid4.Contains(".1.3.6.1.4.1.9.9.46.1.3.1.1.4.1.")) continue;

                                    int vlan4 = int.Parse(tempOid4.Replace(".1.3.6.1.4.1.9.9.46.1.3.1.1.4.1.", ""));
                                    if (vlan3 == vlan4)
                                    {
                                        vtpVlanName = resultCisco4[i].Data.ToString();
                                        break; //找到了，跳出循环
                                    }
                                }

                                temp_sql = string.Format("insert into vlan (device_ip,vlan,vtpVlanTye,name) values('{0}',{1},{2},'{3}'); ",
                                    ip, vlan3.ToString(), vtpVlanTye.ToString(), vtpVlanName);
                                sqlList.Add(temp_sql);

                            }
                            catch (Exception ex)
                            {
                                string error_msg = ex.ToString();
                                continue;
                            }
                        }

                        #endregion
                    }
                    #endregion

                    if (logOption.log_sysobjectid_success == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',3); ", ip);
                        sqlList.Add(temp_sql);
                    }

                    execute_StringSqlList(sqlList, 50, false);  //批量执行SQL语句
                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    if (logOption.log_sysobjectid_failure == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',103); ", ip);
                        sqlList.Clear();
                        sqlList.Add(temp_sql);

                        execute_StringSqlList(sqlList); //批量执行SQL语句 
                    }
                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }
            }
            #endregion

            #region dot1BasePort  分成两次SNMP get-bulk ，不象ARP表 MAC地址表会比较频繁的更新更新
            if (op_type == Op_type.dot1BasePort)
            {
                try
                {
                    #region 发送SNMP请求，获取返回结果
                    //dot1dBasePort
                    var result_dot1dBasePort = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.17.1.4.1.1"),
                        result_dot1dBasePort,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    if (result_dot1dBasePort.Count == 0) return;

                    //dot1dBasePortIfIndex
                    var result_dot1dBasePortIfIndex = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.17.1.4.1.2"),
                        result_dot1dBasePortIfIndex,
                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    #endregion

                    #region 因为是多次获取表的某一个字段，将获取的结果解析，合并
                    if (result_dot1dBasePort.Count != result_dot1dBasePortIfIndex.Count)
                    {
                        //MessageBox.Show("dot1dBasePort 和 dot1dBasePortIfIndex 数目不一致 sparse tables? ");
                        return;
                    }
                    List<Dot1BasePortEntry> listDot1BasePortEntry = new List<Dot1BasePortEntry>();

                    for (int i = 0; i < result_dot1dBasePort.Count; i++)
                    {
                        try
                        {
                            Dot1BasePortEntry dot1BasePortEntry = new Dot1BasePortEntry();
                            dot1BasePortEntry.dot1dBasePort = int.Parse(result_dot1dBasePort[i].Data.ToString());
                            listDot1BasePortEntry.Add(dot1BasePortEntry);
                        }
                        catch (Exception ex)
                        {
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    for (int i = 0; i < result_dot1dBasePortIfIndex.Count; i++)
                    {
                        try
                        {
                            string temp_s = result_dot1dBasePortIfIndex[i].Id.ToString().Replace(".1.3.6.1.2.1.17.1.4.1.2.", "");
                            int temp_ifIndex = int.Parse(temp_s);

                            foreach (var dot1BasePortEntryt in listDot1BasePortEntry)
                            {
                                if (dot1BasePortEntryt.dot1dBasePort == temp_ifIndex)
                                {
                                    dot1BasePortEntryt.dot1dBasePortIfIndex = int.Parse(result_dot1dBasePortIfIndex[i].Data.ToString());
                                    break; //找到了 跳出循环
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
                    temp_sql = string.Format("delete from dot1dBasePort where device_ip='{0}';", ip);
                    sqlList.Add(temp_sql);

                    foreach (var dot1BasePortEntryt in listDot1BasePortEntry)
                    {
                        temp_sql = string.Format("call insert_dot1dBasePort  ('{0}',{1},{2}); ",
                            ip, dot1BasePortEntryt.dot1dBasePort.ToString(), dot1BasePortEntryt.dot1dBasePortIfIndex.ToString());
                        sqlList.Add(temp_sql);
                    }

                    #endregion

                    #region 记录日志
                    if (logOption.log_sysobjectid_success == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',4'); ", ip);
                        sqlList.Add(temp_sql);
                    }
                    #endregion

                    execute_StringSqlList(sqlList, 50, false); //批量执行SQL语句  担心死锁 不用事务了 
                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    if (logOption.log_sysobjectid_failure == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',104); ", ip);
                        sqlList.Clear();
                        sqlList.Add(temp_sql);

                        execute_StringSqlList(sqlList); //批量执行SQL语句
                    }
                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }
            }

            #endregion

            #region ARP 极端情况 一次SNMP Get-Bulk 获取ARP表的过程中发生了变化，表现为数据项数目不一样 不匹配
            #region MIB数据项  ipNetToMediaTable ipNetToMediaEntry 及其解释
            //1.3.6.1.2.1.4.22.1.1(ipNetToMediaIfIndex)  1-2,147,483,647 和c# int一样 -2,147,483,648 到 2,147,483,647
            //1.3.6.1.2.1.4.22.1.2(ipNetToMediaPhysAddress) MAC
            //1.3.6.1.2.1.4.22.1.3(ipNetToMediaNetAddress) IP
            //1.3.6.1.2.1.4.22.1.4(ipNetToMediaType)          
            //other(1),invalid(2),  dynamic(3), static(4) 路由器本身的MAC-IP Static
            #endregion

            if (op_type == Op_type.arp)
            {
                var result_ipNetToMedia = new List<Variable>();
                try
                {
                    int SNMP_result = Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.4.22.1"),
                        result_ipNetToMedia, snmpTimeOut,
                        30, WalkMode.WithinSubtree, null, null);

                    if (result_ipNetToMedia.Count == 0) return;

                    List<IpNetToMediaEntry> listIpNetToMediaEntry = new List<IpNetToMediaEntry>();

                    for (int i = 0; i < result_ipNetToMedia.Count / 4; i++)
                    {
                        //这些用来 匹配 tabular 数据，因为 极端情况 数据更新时候 接收到的 可能不一致                                              
                        try
                        {
                            #region 第一个数据项 获取OID  从OID获取ipNetToMediaNetAddress   
                            // .1.3.6.1.2.1.4.22.1.1.111.10.50.2.1 索引项：111，  IP:10.50.2.1
                            Boolean found_ipNetToMediaPhysAddress = false;

                            if (!result_ipNetToMedia[i].ToString().Contains(".1.3.6.1.2.1.4.22.1.1."))
                                continue;

                            IpNetToMediaEntry ipNetToMediaEntry = new IpNetToMediaEntry();

                            ipNetToMediaEntry.oID = result_ipNetToMedia[i].Id.ToString();
                            //有几个别的 Data字段为空，只能从OID获取接口  ipNetToMediaEntry.ipNetToMediaIfIndex = int.Parse(result1[i].Data.ToString()); 会异常
                            //.1.3.6.1.2.1.4.22.1.2.1.10.23.1.34"
                            string temp_s1 = ipNetToMediaEntry.oID.Replace(".1.3.6.1.2.1.4.22.1.1.", "");
                            string[] tmp_array = temp_s1.Split('.');
                            ipNetToMediaEntry.ipNetToMediaIfIndex = int.Parse(tmp_array[0]);
                            int temp_length = (tmp_array[0] + ".").Length;
                            string temp_ip = temp_s1.Substring(temp_length);
                            ipNetToMediaEntry.ipNetToMediaNetAddress = temp_ip;
                            #endregion

                            #region ipNetToMediaPhysAddress  MAC地址
                            string tempOid2 = result_ipNetToMedia[i + result_ipNetToMedia.Count / 4].Id.ToString();
                            if (!tempOid2.Contains(".1.3.6.1.2.1.4.22.1.2."))
                                continue;

                            tempOid2 = tempOid2.Replace(".1.3.6.1.2.1.4.22.1.2.", ".1.3.6.1.2.1.4.22.1.1.");

                            if (tempOid2 == result_ipNetToMedia[i].Id.ToString())
                            {
                                byte[] mac_bytes = result_ipNetToMedia[i + result_ipNetToMedia.Count / 4].Data.ToBytes();  //MAC地址
                                                                                                                           //string 的length是6， ToBytes变成8  0406 
                                string temp_PhysAddress = byteToHexStr(mac_bytes);
                                ipNetToMediaEntry.ipNetToMediaPhysAddress = temp_PhysAddress.Substring(4);
                                found_ipNetToMediaPhysAddress = true;
                            }
                            else
                            {
                                for (int j = result_ipNetToMedia.Count / 4; j < (result_ipNetToMedia.Count / 4) * 2; j++)
                                {
                                    if (!result_ipNetToMedia[j].ToString().Contains(".1.3.6.1.2.1.4.22.1.2."))
                                        continue;

                                    tempOid2 = result_ipNetToMedia[j].Id.ToString().Replace(".1.3.6.1.2.1.4.22.1.2.", ".1.3.6.1.2.1.4.22.1.1.");
                                    if (tempOid2 == result_ipNetToMedia[i].Id.ToString())
                                    {
                                        byte[] mac_bytes = result_ipNetToMedia[j].Data.ToBytes();  //MAC地址
                                                                                                   //string 的length是6， ToBytes变成8  0406 
                                        string temp_PhysAddress = byteToHexStr(mac_bytes);
                                        ipNetToMediaEntry.ipNetToMediaPhysAddress = temp_PhysAddress.Substring(4);
                                        found_ipNetToMediaPhysAddress = true;
                                        break; //找到了，跳出循环
                                    }
                                }
                            }

                            if (!found_ipNetToMediaPhysAddress)
                                continue;

                            #endregion

                            #region ipNetToMediaType
                            Boolean found_ipNetToMediaType = false;

                            string tempOid4 = result_ipNetToMedia[i + (result_ipNetToMedia.Count / 4) * 3].Id.ToString();
                            if (!tempOid4.Contains(".1.3.6.1.2.1.4.22.1.4."))
                                continue;
                            tempOid4 = tempOid4.Replace(".1.3.6.1.2.1.4.22.1.4.", ".1.3.6.1.2.1.4.22.1.1.");
                            if (tempOid4 == result_ipNetToMedia[i].Id.ToString())
                            {
                                ipNetToMediaEntry.ipNetMediaType = int.Parse(result_ipNetToMedia[i + (result_ipNetToMedia.Count / 4) * 3].Data.ToString());
                                found_ipNetToMediaType = true;
                            }
                            else
                            {
                                for (int j = (result_ipNetToMedia.Count / 4) * 3; j < result_ipNetToMedia.Count; j++)
                                {
                                    if (!result_ipNetToMedia[j].ToString().Contains(".1.3.6.1.2.1.4.22.1.4."))
                                        continue;
                                    tempOid4 = result_ipNetToMedia[j].Id.ToString().Replace(".1.3.6.1.2.1.4.22.1.4.", ".1.3.6.1.2.1.4.22.1.1.");
                                    if (tempOid4 == result_ipNetToMedia[i].Id.ToString())
                                    {
                                        ipNetToMediaEntry.ipNetMediaType = int.Parse(result_ipNetToMedia[j].Data.ToString());
                                        found_ipNetToMediaType = true;
                                        break; //找到了，跳出循环
                                    }
                                }
                            }

                            if (!found_ipNetToMediaType)
                                continue;

                            #endregion

                            listIpNetToMediaEntry.Add(ipNetToMediaEntry); //找不到的 都continue了，不会添加到列表
                        }
                        catch (Exception ex)
                        {
                            //Details = "Lextm.SharpSnmpLib.SnmpException: unsupported data type: 1\r\n   
                            //在 Lextm.SharpSnmpLib.DataFactory.CreateSnmpData(Int32 type, Stream stream)\r\n
                            string error_msg = ex.ToString();
                            continue;
                        }
                    }

                    #region 根据SNMP结果生成SQL语句列表     写入日志    并执行SQL语句列表
                    foreach (var ipNetToMediaEntry in listIpNetToMediaEntry)
                    {
                        temp_sql = string.Format("call insert_ARP  ('{0}','{1}',{2}); ",
                            ipNetToMediaEntry.ipNetToMediaPhysAddress,
                            ipNetToMediaEntry.ipNetToMediaNetAddress,
                            ipNetToMediaEntry.ipNetMediaType.ToString()
                            );
                        sqlList.Add(temp_sql);
                    }

                    if (logOption.log_arp_success == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',5); ", ip);
                        sqlList.Add(temp_sql);
                    }

                    execute_StringSqlList(sqlList, 10, false); //批量执行SQL语句

                    #endregion
                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    if (logOption.log_arp_failure == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',105); ", ip);
                        sqlList.Clear();
                        sqlList.Add(temp_sql);

                        execute_StringSqlList(sqlList); //批量执行SQL语句
                    }

                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }

            }

            #endregion

            #region Mac Address 

            #region  MIB MAC地址表 Q-BRIDGE-MIB
            //Q-BRIDGE-MIB  dot1dTpFdbTable 
            //1.3.6.1.2.1.17.7.1.2.2.1
            //1.3.6.1.2.1.17.7.1.2.2.1.1(dot1qTpFdbAddress) MAC地址
            //1.3.6.1.2.1.17.7.1.2.2.1.2(dot1qTpFdbPort)
            //1.3.6.1.2.1.17.7.1.2.2.1.3(dot1qTpFdbStatus)

            // dot1dTpFdbTable dot1dTpFdbEntry  1.3.6.1.2.1.17.4.3.1 数据项 把q换成d
            //dot1dTpFdbAddress dot1dTpFdbPort dot1TpFdbStatus
            #endregion

            if (op_type == Op_type.mac_address)
            {
                int is_community_index = 0; ////有些@2 会出错，记录日志时候不错

                try
                {
                    List<Dot1dTpFdbEntry> listDot1dTpFdbEntry = new List<Dot1dTpFdbEntry>();

                    //the dot1dTpFdbTable table is populated only with MAC addresses learned on the default VLAN
                    //To see the MAC addresses of all VLANs, specify the dot1qTpFdbTable table(RFC 4363b, Q - Bridge VLAN MIB)

                    #region  H3C这个有 Q-BRIDGE-MIB  802.1q 一次SNMP  Get-Bulk  Cisco没有 不用 community index

                    var resultMBridge1 = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                                            new IPEndPoint(IPAddress.Parse(ip), port),
                                            new OctetString(community),
                                            new ObjectIdentifier("1.3.6.1.2.1.17.7.1.2.2.1"),
                                            resultMBridge1,
                                            snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                    if (resultMBridge1.Count > 0)
                    {
                        //只有两列，dot1qTpFdbAddress为索引     //存在127个 个数不相等的，必须要双重循环 
                        for (int i = 0; i < resultMBridge1.Count / 2; i++)
                        {
                            #region 解析SNMP返回的结果
                            try
                            {
                                Dot1dTpFdbEntry dot1dTpFdbEntry = new Dot1dTpFdbEntry();

                                dot1dTpFdbEntry.dot1dTpFdbPort = int.Parse(resultMBridge1[i].Data.ToString());
                                string Oid2 = resultMBridge1[i].Id.ToString();
                                if (!Oid2.Contains(".1.3.6.1.2.1.17.7.1.2.2.1.2."))
                                    continue;

                                string tempOid2 = Oid2.Replace(".1.3.6.1.2.1.17.7.1.2.2.1.2.", "");
                                string[] arrayOid = tempOid2.Split('.');

                                dot1dTpFdbEntry.vlan = int.Parse(arrayOid[0]);

                                for (int j = 1; j < arrayOid.Length; j++)
                                {
                                    string hexOutput = String.Format("{0:X}", int.Parse(arrayOid[j]));
                                    if (hexOutput.Length == 1)
                                        dot1dTpFdbEntry.dot1dTpFdbAddress = dot1dTpFdbEntry.dot1dTpFdbAddress + "0" + hexOutput;
                                    if (hexOutput.Length == 2)
                                        dot1dTpFdbEntry.dot1dTpFdbAddress = dot1dTpFdbEntry.dot1dTpFdbAddress + hexOutput;
                                }

                                Boolean is_found = false;

                                //可以先定向检测，木有再循环
                                string Oid3 = resultMBridge1[i + resultMBridge1.Count / 2].Id.ToString();
                                if (!Oid3.Contains(".1.3.6.1.2.1.17.7.1.2.2.1.3."))
                                    continue;

                                string tempOid3 = Oid3.Replace(".1.3.6.1.2.1.17.7.1.2.2.1.3.", "");
                                if (tempOid2 == tempOid3)
                                {
                                    dot1dTpFdbEntry.dot1dTpFdbStatus = int.Parse(resultMBridge1[i + resultMBridge1.Count / 2].Data.ToString());
                                    listDot1dTpFdbEntry.Add(dot1dTpFdbEntry);
                                }
                                else
                                {
                                    for (int j = resultMBridge1.Count / 2; j < resultMBridge1.Count; j++)
                                    {
                                        Oid3 = resultMBridge1[j].Id.ToString();
                                        if (!Oid3.Contains(".1.3.6.1.2.1.17.7.1.2.2.1.3."))
                                            continue;
                                        tempOid3 = Oid3.Replace(".1.3.6.1.2.1.17.7.1.2.2.1.3.", "");
                                        if (tempOid2 == tempOid3)
                                        {
                                            dot1dTpFdbEntry.dot1dTpFdbStatus = int.Parse(resultMBridge1[j].Data.ToString());
                                            is_found = true;
                                            break; //找到了，跳出循环
                                        }
                                    }
                                    if (!is_found)
                                        continue;

                                    listDot1dTpFdbEntry.Add(dot1dTpFdbEntry);
                                }
                            }
                            catch (Exception ex)
                            {
                                string error = ex.ToString();
                                continue;
                            }
                            #endregion
                        }

                        foreach (Dot1dTpFdbEntry dot1dTpFdbEntry in listDot1dTpFdbEntry)
                        {
                            temp_sql = string.Format("call insert_dot1dTpFdbtable  ('{0}','{1}',{2},{3},{4}); ",
                                ip, dot1dTpFdbEntry.dot1dTpFdbAddress, dot1dTpFdbEntry.dot1dTpFdbPort.ToString(), dot1dTpFdbEntry.dot1dTpFdbStatus.ToString(), dot1dTpFdbEntry.vlan.ToString());
                            sqlList.Add(temp_sql);
                        }
                        listDot1dTpFdbEntry.Clear();

                        execute_StringSqlList(sqlList, 10, false); //批量执行SQL语句 考虑 连接数据库 过期时间，不宜过多
                        sqlList.Clear();
                    }

                    #endregion

                    #region BRIDGE-MIB dot1dTpFdbTable  dot1dTpFdbEntry

                    List<string> listCommunity = new List<string>();
                    listCommunity.Add(community);

                    #region 从数据看VLAN表  填充Cisco SNMP Community String Indexing
                    if (sysObjectID.Contains("1.3.6.1.4.1.9."))
                    {
                        string connString = ConfigurationManager.ConnectionStrings["connectingStringMySQL"].ToString();
                        MySqlConnection myCon = new MySqlConnection(connString);
                        MySqlCommand mySqlCommandVLAN = new MySqlCommand(string.Format("select * from vlan where device_ip='{0}'", ip),
                            myCon);
                        MySqlDataAdapter myDaVLAN = new MySqlDataAdapter(mySqlCommandVLAN);
                        DataTable myDtVLAN = new DataTable();
                        myDaVLAN.Fill(myDtVLAN);
                        myCon.Close();
                        myCon.Dispose();

                        foreach (DataRow dataRowVLAN in myDtVLAN.Rows)
                        {
                            if (dataRowVLAN["vlan"].ToString() == "1")  //不加已经代表了@1
                                continue;
                            listCommunity.Add(community + "@" + dataRowVLAN["vlan"].ToString());
                        }
                    }
                    #endregion

                    foreach (var community_string in listCommunity)
                    {
                        #region 说明 MIB dot1dTpFdbTable. dot1dTpFdbEntry 
                        //1.3.6.1.2.1.17.4.3.1.1(dot1dTpFdbAddress)   
                        //1.3.6.1.2.1.17.4.3.1.2(dot1dTpFdbPort)
                        //1.3.6.1.2.1.17.4.3.1.3(dot1dTpFdbStatus)
                        #endregion

                        is_community_index = is_community_index + 1;  //有些设备不支持community string indexing， 推断，不用记录失败

                        var result1 = new List<Variable>();
                        Messenger.BulkWalk(VersionCode.V2,
                                                        new IPEndPoint(IPAddress.Parse(ip), port),
                                                        new OctetString(community_string),
                                                        new ObjectIdentifier("1.3.6.1.2.1.17.4.3.1"),
                                                        result1,
                                                        snmpTimeOut, snmpMaxRepetitions, WalkMode.WithinSubtree, null, null);

                        #region 解析 SNMP结果
                        for (int i = 0; i < result1.Count / 3; i++)
                        {
                            try
                            {
                                Dot1dTpFdbEntry dot1dTpFdbEntry = new Dot1dTpFdbEntry();

                                if (community == community_string)
                                    dot1dTpFdbEntry.vlan = 1;
                                else
                                    dot1dTpFdbEntry.vlan = int.Parse(community_string.Replace(community + "@", ""));

                                string Oid1 = result1[i].Id.ToString();
                                if (!Oid1.Contains(".1.3.6.1.2.1.17.4.3.1.1."))
                                    continue;

                                string tempOid1 = Oid1.Replace(".1.3.6.1.2.1.17.4.3.1.1.", "");

                                byte[] mac_bytes = result1[i].Data.ToBytes();  //MAC地址                                                                                            
                                                                               //string 的length是6， ToBytes变成8  0406 
                                string temp_PhysAddress = byteToHexStr(mac_bytes);
                                dot1dTpFdbEntry.dot1dTpFdbAddress = temp_PhysAddress.Substring(4);

                                //1.3.6.1.2.1.17.4.3.1.2 dot1dTpFdbPort
                                Boolean found_dot1dTpFdbPort = false;

                                string Oid2 = result1[i + (result1.Count / 3)].Id.ToString();
                                if (!Oid1.Contains(".1.3.6.1.2.1.17.4.3.1.2."))
                                    continue;
                                string tempOid2 = Oid2.Replace(".1.3.6.1.2.1.17.4.3.1.2.", "");
                                if (tempOid1 == tempOid2)
                                {
                                    dot1dTpFdbEntry.dot1dTpFdbPort = int.Parse(result1[i + (result1.Count / 3)].Data.ToString());
                                    found_dot1dTpFdbPort = true;
                                }
                                else
                                {
                                    for (int j = result1.Count / 3; j < (result1.Count / 3) * 2; j++)
                                    {
                                        Oid2 = result1[j].Id.ToString();
                                        if (!Oid2.Contains(".1.3.6.1.2.1.17.4.3.1.2."))
                                            continue;

                                        tempOid2 = Oid2.Replace(".1.3.6.1.2.1.17.4.3.1.2.", "");
                                        if (tempOid1 == tempOid2)
                                        {
                                            dot1dTpFdbEntry.dot1dTpFdbPort = int.Parse(result1[j].Data.ToString());
                                            found_dot1dTpFdbPort = true;
                                            break; //找到了，跳出循环
                                        }
                                    }
                                }

                                if (!found_dot1dTpFdbPort) continue;

                                //1.3.6.1.2.1.17.4.3.1.3 dot1dTpFdbStatus 
                                Boolean found_dot1dTpFdbStatus = false;

                                string Oid3 = result1[i + (result1.Count / 3) * 2].Id.ToString();
                                if (!Oid3.Contains(".1.3.6.1.2.1.17.4.3.1.3."))
                                    continue;
                                string tempOid3 = Oid3.Replace(".1.3.6.1.2.1.17.4.3.1.3.", "");
                                if (tempOid1 == tempOid3)
                                {
                                    dot1dTpFdbEntry.dot1dTpFdbStatus = int.Parse(result1[i + (result1.Count / 3) * 2].Data.ToString());
                                    found_dot1dTpFdbStatus = true;
                                }
                                else
                                {
                                    for (int j = (result1.Count / 3) * 2; j < (result1.Count); j++)
                                    {
                                        Oid3 = result1[j].Id.ToString();
                                        if (!Oid3.Contains(".1.3.6.1.2.1.17.4.3.1.3."))
                                            continue;

                                        tempOid3 = Oid3.Replace(".1.3.6.1.2.1.17.4.3.1.3.", "");
                                        if (tempOid1 == tempOid3)
                                        {
                                            dot1dTpFdbEntry.dot1dTpFdbStatus = int.Parse(result1[j].Data.ToString());
                                            found_dot1dTpFdbStatus = true;
                                            break; //找到了，跳出循环
                                        }
                                    }
                                }

                                if (found_dot1dTpFdbPort && found_dot1dTpFdbStatus)
                                    listDot1dTpFdbEntry.Add(dot1dTpFdbEntry);
                            }
                            catch (Exception ex)
                            {
                                string error = ex.ToString();
                                continue;
                            }
                        }
                        #endregion

                        #region 根据SNMP结果生成SQL语句列表

                        foreach (Dot1dTpFdbEntry dot1dTpFdbEntry in listDot1dTpFdbEntry)
                        {
                            temp_sql = string.Format("call insert_dot1dTpFdbtable  ('{0}','{1}',{2},{3},{4}); ",
                                ip, dot1dTpFdbEntry.dot1dTpFdbAddress, dot1dTpFdbEntry.dot1dTpFdbPort.ToString(), dot1dTpFdbEntry.dot1dTpFdbStatus.ToString(), dot1dTpFdbEntry.vlan.ToString());
                            sqlList.Add(temp_sql);
                        }
                        listDot1dTpFdbEntry.Clear();
                        #endregion

                        execute_StringSqlList(sqlList); //批量执行SQL语句
                        sqlList.Clear();
                    }

                    #endregion

                    #region 记录日志
                    if (logOption.log_mac_address_success == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',6); ", ip);
                        sqlList.Clear();
                        sqlList.Add(temp_sql);
                    }
                    #endregion

                    execute_StringSqlList(sqlList); //批量执行SQL语句
                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    if (is_community_index == 2)  //很可能 不支持community indexing string
                    {
                        if (logOption.log_mac_address_success == true)
                        {
                            temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',6); ", ip);
                            sqlList.Clear();
                            sqlList.Add(temp_sql);
                            execute_StringSqlList(sqlList); //批量执行SQL语句
                        }
                    }
                    else
                    {
                        if (logOption.log_mac_address_failure == true)
                        {
                            temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',106); ", ip);
                            sqlList.Clear();
                            sqlList.Add(temp_sql);
                            execute_StringSqlList(sqlList); //批量执行SQL语句
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }
            }
            #endregion

            #region ARP_new
            #region MIB数据项  ipNetToMediaTable ipNetToMediaEntry 及其解释            
            //1.3.6.1.2.1.4.35.1.1(ipNetToPhysicalIfIndex)
            //1.3.6.1.2.1.4.35.1.2(ipNetToPhysicalNetAddressType)
            //1.3.6.1.2.1.4.35.1.3(ipNetToPhysicalNetAddress)

            //1.3.6.1.2.1.4.35.1.4(ipNetToPhysicalPhysAddress)

            //1.3.6.1.2.1.4.35.1.5(ipNetToPhysicalLastUpdated)
            //1.3.6.1.2.1.4.35.1.6(ipNetToPhysicalType)
            //1.3.6.1.2.1.4.35.1.7(ipNetToPhysicalState)
            //1.3.6.1.2.1.4.35.1.8(ipNetToPhysicalRowStatus)
            #endregion

            if (op_type == Op_type.arp_new)
            {
                try
                {
                    #region 发送SNMP请求，获取返回结果
                    ip = "10.34.2.254";
                    ip = "10.23.2.254";
                    var result_ipNetToPhysicalPhysAddress = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.4.35.1.4"),                                                
                        result_ipNetToPhysicalPhysAddress,
                        snmpTimeOut, 10, WalkMode.WithinSubtree, null, null);

                    List<IpNetToPhysical> listIpNetToPhysical = new List<IpNetToPhysical>();

                    if (result_ipNetToPhysicalPhysAddress.Count == 0)
                        return;

                    for (int i = 0; i < result_ipNetToPhysicalPhysAddress.Count; i++)
                    {
                        IpNetToPhysical ipNetToPhysicalEntry = new IpNetToPhysical();
                        ipNetToPhysicalEntry.oID = result_ipNetToPhysicalPhysAddress[i].Id.ToString();
                        ipNetToPhysicalEntry.IP = ipNetToPhysicalEntry.oID.Replace(".1.3.6.1.2.1.4.35.1.4.", "");

                        byte[] mac_bytes = result_ipNetToPhysicalPhysAddress[i].Data.ToBytes();  //MAC地址
                                                                                                 //string 的length是6， ToBytes变成8  0406 
                        string temp_PhysAddress = byteToHexStr(mac_bytes);
                        ipNetToPhysicalEntry.ipNetToPhysicalPhysAddress = temp_PhysAddress.Substring(4);

                        listIpNetToPhysical.Add(ipNetToPhysicalEntry);
                    }

                    //jji add 2019_10_31
                    //ipNetToPhysicalType
                    var result_ipNetToPhysicalNetAddress = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        //new ObjectIdentifier("1.3.6.1.2.1.4.35.1.3"),
                        //new ObjectIdentifier("1.3.6.1.2.1.4.34.1"),   
                        //new ObjectIdentifier("1.3.6.1.2.1.55.8"),                        
                        new ObjectIdentifier("1.3.6.1.2.1.55"),
                        result_ipNetToPhysicalNetAddress,
                        snmpTimeOut, 10, WalkMode.WithinSubtree, null, null);

                    if(result_ipNetToPhysicalNetAddress.Count>0)
                    {
                        int iddd = 0;
                    }
                    

                    // jji

                    //ipNetToPhysicalType
                    var result_ipNetToPhysicalType = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.4.35.1.6"),
                        result_ipNetToPhysicalType,
                        snmpTimeOut, 10, WalkMode.WithinSubtree, null, null);

                    for (int i = 0; i < result_ipNetToPhysicalType.Count; i++)
                    {
                        string OID = result_ipNetToPhysicalType[i].Id.ToString();
                        string tempOid = OID.Replace(".1.3.6.1.2.1.4.35.1.6.", ".1.3.6.1.2.1.4.35.1.4.");

                        foreach (var IpNetToPhysical in listIpNetToPhysical)
                        {
                            if (IpNetToPhysical.oID == tempOid)
                            {
                                IpNetToPhysical.ipNetToPhysicalType =
                                     int.Parse(result_ipNetToPhysicalType[i].Data.ToString());
                            }
                        }
                    }

                    //ipNetToPhysicalState
                    var result_ipNetToPhysicalState = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.4.35.1.6"),
                        result_ipNetToPhysicalType,
                        snmpTimeOut, 10, WalkMode.WithinSubtree, null, null);

                    for (int i = 0; i < result_ipNetToPhysicalState.Count; i++)
                    {
                        string OID = result_ipNetToPhysicalState[i].Id.ToString();
                        string tempOid = OID.Replace(".1.3.6.1.2.1.4.35.1.6.", ".1.3.6.1.2.1.4.35.1.4.");

                        foreach (var IpNetToPhysical in listIpNetToPhysical)
                        {
                            if (IpNetToPhysical.oID == tempOid)
                            {
                                IpNetToPhysical.ipNetToPhysicalState =
                                     int.Parse(result_ipNetToPhysicalState[i].Data.ToString());
                            }
                        }
                    }

                    //ipNetToPhysicalRowStatus
                    var result_ipNetToPhysicalRowStatus = new List<Variable>();
                    Messenger.BulkWalk(VersionCode.V2,
                        new IPEndPoint(IPAddress.Parse(ip), port),
                        new OctetString(community),
                        new ObjectIdentifier("1.3.6.1.2.1.4.35.1.6"),
                        result_ipNetToPhysicalType,
                        snmpTimeOut, 10, WalkMode.WithinSubtree, null, null);

                    for (int i = 0; i < result_ipNetToPhysicalRowStatus.Count; i++)
                    {
                        string OID = result_ipNetToPhysicalRowStatus[i].Id.ToString();
                        string tempOid = OID.Replace(".1.3.6.1.2.1.4.35.1.6.", ".1.3.6.1.2.1.4.35.1.4.");

                        foreach (var IpNetToPhysical in listIpNetToPhysical)
                        {
                            if (IpNetToPhysical.oID == tempOid)
                            {
                                IpNetToPhysical.ipNetToPhysicalRowStatus =
                                     int.Parse(result_ipNetToPhysicalRowStatus[i].Data.ToString());
                            }
                        }
                    }
                    #endregion

                    #region 根据SNMP结果生成SQL语句列表     写入日志    并执行SQL语句列表
                    foreach (var IpNetToPhysical in listIpNetToPhysical)
                    {
                        temp_sql = string.Format("call insert_ARP_new  ('{0}','{1}',{2},{3},{4}); ",
                            IpNetToPhysical.ipNetToPhysicalPhysAddress,
                            IpNetToPhysical.IP,
                            IpNetToPhysical.ipNetToPhysicalType,
                            IpNetToPhysical.ipNetToPhysicalState,
                            IpNetToPhysical.ipNetToPhysicalRowStatus
                            );
                        sqlList.Add(temp_sql);
                    }

                    if (logOption.log_arp_success == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',6); ", ip);
                        sqlList.Add(temp_sql);
                    }

                    execute_StringSqlList(sqlList, 10, false); //批量执行SQL语句
                    #endregion

                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    if (logOption.log_arp_failure == true)
                    {
                        temp_sql = string.Format("insert into  log(device_ip,log_type_id) values  ('{0}',106); ", ip);
                        sqlList.Clear();
                        sqlList.Add(temp_sql);

                        execute_StringSqlList(sqlList); //批量执行SQL语句
                    }

                }
                catch (Exception ex)
                {
                    string error_msg = ex.ToString();
                    return;
                }

            }           
          
            #endregion

        }
    }

}
