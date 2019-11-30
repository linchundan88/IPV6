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
        int thread_num = 1;
        Boolean busy_flag = false;
        bool stop_flag = false;

        public Form1()
        {
            InitializeComponent();
        }
                
        void get_snmp()
        {
            #region 判断是否已经在运行，设置正在运行标记
            if (busy_flag == true)
                return;
            else
                busy_flag = true;

            stop_flag = false; //不强制线程结束 因为 存在 自动/手工 模式切换

            #endregion

            #region 获取现有的网络设备设备，扫描后只添加新的网络设备
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
                mySqlCommand.CommandText = "select * from device ";
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

            List<string> list_ip = new List<string>();
            string snmp_community_ro = textBox_SNMP_community_ro.Text;
            string snmp_community_rw = textBox_SNMP_community_rw.Text;
            int snmp_port = 0;

            Boolean get_arp = checkBox_getarp.Checked;
            Boolean get_macaddress = checkBox_getmacaddress.Checked;

            if (int.TryParse(textBox_SNMP_port.Text,out snmp_port) == false) 
            {
                MessageBox.Show("SNMP端口号错误！");
                stop_flag = true; //强制线程结束
                busy_flag = false;
                btnStart.Text = "开始";
                return;
            }
            
            #region IP地址扫描范围    数据库已有的是否要排除      
            try
            {
                long ip_start = Helper_IP.IpToInt(textBox_start_ip.Text);
                long ip_end = Helper_IP.IpToInt(textBox_end_ip.Text);

                for (long i=ip_start;i<=ip_end;i++)
                {                    
                    string temp_ip = Helper_IP.IntToIp(i); // IP地址数字转换为字符串        

                    foreach(DataRow dr in myDt.Rows)
                    {
                       if (temp_ip== dr["device_ip"].ToString() )
                            break;
                    }
                    list_ip.Add(temp_ip);
                }               
            }
            catch (Exception)
            {
                MessageBox.Show("IP地址格式错误!");
                stop_flag = true; //强制线程结束
                busy_flag = false;
                btnStart.Text = "开始";
                return;
            }

            #endregion

            #region 创建线程类
            Worker[] workerObject = new Worker[thread_num];
            Thread[] workerThread = new Thread[thread_num];
            for (int i = 0; i < thread_num; i++)
            {
                workerObject[i] = new Worker();
                workerThread[i] = new Thread(workerObject[i].doWork);
            }
            #endregion
            
            //顺序执行 因为基本不存在死锁风险
            foreach (var ipaddress in list_ip)
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
                            workerObject[i] = new Worker();
                            workerThread[i] = new Thread(workerObject[i].doWork);

                            workerObject[i].init_Worker(ipaddress, snmp_port,snmp_community_ro, snmp_community_rw,
                                get_arp,get_macaddress, connString, myDt,
                                int.Parse(comboBox_thread_num.Text) );
                            workerThread[i].Start();
                            goto label1;   //该交换机设备的该任务执行完毕，跳出循环
                        }
                    }
                    Thread.Sleep(50);  //延时 等待 其他进程执行完毕 
                }

                label1: int noUseInt = 1;
                #endregion
            }

            #region 执行完毕 清理执行标记
            busy_flag = false;
            btnStart.Text = "开始";
            MessageBox.Show("执行完毕！");

            #endregion
        }
        

        private void comboBox_thread_num_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_thread_num.Text != "")
                thread_num = int.Parse(comboBox_thread_num.Text);
        }
  

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "开始")
            {
                stop_flag = false; //不强制线程结束              
                btnStart.Text = "停止";

                get_snmp();
            }
            else
            {
                stop_flag = true; //强制线程结束
                btnStart.Text = "开始";               
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {        
            comboBox_thread_num.SelectedIndex = 0; //线程            
        }
    }
}
