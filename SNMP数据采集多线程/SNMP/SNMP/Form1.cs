using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;
using System.Net;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SNMP
{
    public partial class Form1 : Form
    {
        int thread_num = 4;
        Boolean busy_flag = false;
        bool stop_flag = false;

        public Form1()
        {
            InitializeComponent();
        }

        //snmp 超时 10秒钟  BulkWalk
        public static int snmpTimeOut = 10000;


        public static string byteToHexStr(byte[] bytes)
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

        void get_snmp(Op_type p_op_type)
        {
            #region 判断是否已经在运行，设置正在运行标记
            if (busy_flag == true)
                return;
            else
                busy_flag = true;

            stop_flag = false; //不强制线程结束 因为 存在 自动/手工 模式切换

            #endregion

            #region 记录日志的选项
            LogOption logOption = new LogOption();
            #endregion

            #region 获取设备一条一条记录，填充 datatable myDt
            string connString = ConfigurationManager.ConnectionStrings["connectingStringMySQL"].ToString();
            MySqlConnection myCon = new MySqlConnection(connString);

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = myCon;
            mySqlCommand.CommandTimeout = 80;
            DataTable myDt = null;

            List<Device_snmp> list_device_snmp = new List<Device_snmp>();

            try
            {
                myCon.Open();

                if (p_op_type == Op_type.arp)
                    mySqlCommand.CommandText = "select * from device where get_arp=1 and enabled=1";
                else if (p_op_type == Op_type.mac_address || p_op_type == Op_type.dot1BasePort)
                    mySqlCommand.CommandText = "select * from device where get_mac_address=1 and enabled=1";
                else
                    mySqlCommand.CommandText = "select * from device where enabled=1";

                //单个调试用
                //mySqlCommand.CommandText = "select * from device where device_ip='10.24.2.25'";

                MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand);
                myDt = new DataTable();
                myDa.Fill(myDt);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string error = ex.ToString();  //timeout
                busy_flag = false;
                return;
            }
            catch (Exception ex)
            {
                //有时候会timeout “Deadlock found when trying to get lock; try restarting transaction”
                string error = ex.ToString();
                busy_flag = false;
                return;
            }
            finally
            {
                myCon.Close();
            }
            #endregion

            #region 填充设备列表
            foreach (DataRow dataRow in myDt.Rows)
            {
                string ip = dataRow["device_ip"].ToString(); // "10.29.2.20"
                int port;
                if (dataRow["snmp_port"].ToString() == "")
                    port = 161;
                else
                    port = int.Parse(dataRow["snmp_port"].ToString());  //161
                string community_read;
                if (dataRow["community_read"].ToString() == "")
                    community_read = "public";
                else
                    community_read = dataRow["community_read"].ToString();  //"read2012@stu+"; 

                Device_snmp device_snmp1 = new Device_snmp();

                device_snmp1.device_ip = ip;
                device_snmp1.community_read = community_read;
                device_snmp1.snmp_port = port;
                device_snmp1.sysObjectID = dataRow["sysObjectID"].ToString();

                list_device_snmp.Add(device_snmp1);

            }
            #endregion

            #region 创建线程类
            SnmpWorker[] workerObject = new SnmpWorker[thread_num];
            Thread[] workerThread = new Thread[thread_num];
            for (int i = 0; i < thread_num; i++)
            {
                workerObject[i] = new SnmpWorker();
                workerThread[i] = new Thread(workerObject[i].doWork);
            }
            #endregion

            if (p_op_type == Op_type.mac_address || p_op_type == Op_type.arp || p_op_type == Op_type.arp_new)
            {
                //避免死锁 需要打乱，特别是对MAC地址表
                while (list_device_snmp.Count > 0)
                {
                    #region 运行时候 强制中断
                    if (stop_flag == true)
                    {
                        busy_flag = false; return;
                    }
                    #endregion

                    Random rnd = new Random();
                    int random_i = rnd.Next(list_device_snmp.Count); //返回一个小于所制定的最大值的非负整数

                    #region 执行线程  从线程数组找运行结束的线程，找到就运行，都找不到则等待
                    while (true)
                    {
                        for (int i = 0; i < thread_num; i++)
                        {
                            if (!workerThread[i].IsAlive)
                            {
                                workerObject[i] = new SnmpWorker();
                                workerThread[i] = new Thread(workerObject[i].doWork);

                                workerObject[i].init_Worker(logOption, list_device_snmp[random_i].device_ip, list_device_snmp[random_i].snmp_port,
                                     list_device_snmp[random_i].community_read, connString, p_op_type,
                                    int.Parse(comboBox_thread_num.Text),list_device_snmp[random_i].sysObjectID);
                                workerThread[i].Start();
                                goto label1;   //该交换机设备的该任务执行完毕，跳出循环
                            }
                        }
                        Thread.Sleep(50);  //延时 等待 其他进程执行完毕 
                    }

                    label1: int noUseInt = 1;
                    #endregion

                    list_device_snmp.RemoveAt(random_i);
                }
            }
            else
            {
                //顺序执行 因为基本不存在死锁风险
                foreach (var device_snmp_item in list_device_snmp)
                {
                    #region 运行时候 强制中断
                    if (stop_flag == true)
                    {
                        busy_flag = false; return;
                    }
                    #endregion

                    #region 执行线程  从线程数组找运行结束的线程，找到就运行，都找不到则等待
                    while (true)
                    {
                        for (int i = 0; i < thread_num; i++)
                        {
                            if (!workerThread[i].IsAlive)
                            {
                                workerObject[i] = new SnmpWorker();
                                workerThread[i] = new Thread(workerObject[i].doWork);

                                workerObject[i].init_Worker(logOption,device_snmp_item.device_ip, device_snmp_item.snmp_port,
                                     device_snmp_item.community_read, connString, p_op_type,
                                    int.Parse(comboBox_thread_num.Text), device_snmp_item.sysObjectID);
                                workerThread[i].Start();
                                goto label1;   //该交换机设备的该任务执行完毕，跳出循环
                            }
                        }
                        Thread.Sleep(50);  //延时 等待 其他进程执行完毕 
                    }

                    label1: int noUseInt = 1;
                    #endregion
                }
            }

            #region 网络设备接口  循环执行每个交换机后，一次性 把三层设备MAC地址 写入ARP欺骗白名单
            if (p_op_type == Op_type.iftable)
            {
                try
                {
                    myCon.Open();
                    mySqlCommand.Connection = myCon;

                    mySqlCommand.CommandText = "mac_whitelist_from_iftable ";
                    mySqlCommand.CommandType = CommandType.StoredProcedure;

                    mySqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    busy_flag = false;
                    return;
                }
                finally
                {
                    myCon.Close();
                }
            }
            #endregion

            #region 执行完毕 清理执行标记
            busy_flag = false;
            /*
            if (radioButton_man.Checked)
            {
                MessageBox.Show("执行完毕！");
            }
            */
            #endregion
        }

        #region 按钮测试用
        private void btn_sysName_Click(object sender, EventArgs e)
        {
            get_snmp(Op_type.sysName);
            MessageBox.Show("获取设备名称执行完毕");
        }

        private void btn_IfTable_Click(object sender, EventArgs e)
        {
            get_snmp(Op_type.iftable);
            MessageBox.Show("获取设备接口执行完毕");
        }

        private void btn_VLAN_Click(object sender, EventArgs e)
        {
            get_snmp(Op_type.vlan);
            MessageBox.Show("获取设备VLAN执行完毕");
        }

        private void btn_ipNetToMediaTable_Click(object sender, EventArgs e)
        {
            get_snmp(Op_type.arp);
            MessageBox.Show("获取设备ARP表执行完毕");
        }

        private void btn_dot1dTpFdbTable_Click(object sender, EventArgs e)
        {
            get_snmp(Op_type.mac_address);
            MessageBox.Show("获取设备MAC地址表执行完毕");
        }

        private void btn_dot1dBase_Click(object sender, EventArgs e)
        {
            get_snmp(Op_type.dot1BasePort);
            MessageBox.Show("获取设备桥端口表执行完毕");
        }

        private void comboBox_thread_num_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_thread_num.Text != "")
                thread_num = int.Parse(comboBox_thread_num.Text);
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (checkBox_sysName.Checked == false &&
                checkBox_arp.Checked == false && checkBox_arp_new.Checked == false
                && checkBox_dot1BasePort.Checked == false &&
                checkBox_ifTable.Checked == false && checkBox_mac_address.Checked == false
                && checkBox_vlan.Checked == false)
            {
                MessageBox.Show("请选择要获取的数据！");
                return;
            }

            if (btnStart.Text == "启动")
            {
                stop_flag = false; //不强制线程结束
                timer1.Enabled = true;
                btnStart.Text = "停止";
            }
            else
            {
                timer1.Enabled = false;
                stop_flag = true; //强制线程结束
                btnStart.Text = "启动";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox_sysName.Checked == true)
                get_snmp(Op_type.sysName);
            if (checkBox_ifTable.Checked == true)
                get_snmp(Op_type.iftable);
            if (checkBox_vlan.Checked == true)
                get_snmp(Op_type.vlan);
            if (checkBox_dot1BasePort.Checked == true)
                get_snmp(Op_type.dot1BasePort);
            if (checkBox_arp.Checked == true)
                get_snmp(Op_type.arp);
            if (checkBox_arp_new.Checked == true)
                get_snmp(Op_type.arp_new);
            if (checkBox_mac_address.Checked == true)
                get_snmp(Op_type.mac_address);
        }

        private void comboBox_timer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_thread_num.Text != "")
                timer1.Interval = 1000 * int.Parse(comboBox_timer.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 212;
            comboBox_timer.SelectedIndex = 2; //定时
            timer1.Interval = 1000 * int.Parse(comboBox_timer.Text);

            comboBox_thread_num.SelectedIndex = 0; //线程            
        }

        private void radioButton_auto_Click(object sender, EventArgs e)
        {
            if (radioButton_auto.Checked)  //切换到 自动执行
            {
                this.Height = 212;
                panel_auto.Visible = true;
                panel_man.Visible = false;
                panel_auto_snmp_type.Visible = true;
                MessageBox.Show("已经切换到自动执行模式！");
            }
        }

        private void radioButton_man_Click(object sender, EventArgs e)
        {
            if (radioButton_man.Checked)  //切换到 手动执行      
            {
                timer1.Enabled = false;
                stop_flag = true; //强制线程结束
                btnStart.Text = "启动";

                this.Height = 212;
                panel_man.Top = 25;
                panel_auto_snmp_type.Visible = false;
                panel_auto.Visible = false;
                panel_man.Visible = true;
                MessageBox.Show("已经切换到手动执行模式！");
            }
        }

        private void btn_arp_new_Click(object sender, EventArgs e)
        {
            get_snmp(Op_type.arp_new);
            MessageBox.Show("获取设备ARP表执行完毕");
        }
    }
}
