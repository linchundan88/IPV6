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
        public Form1()
        {
            InitializeComponent();
        }

        Boolean is_busy = false;

        private void set_port()
        {
            if (is_busy)
                return;

            is_busy = true;

            string connString = ConfigurationManager.ConnectionStrings["connectingStringMySQL"].ToString();
            MySqlConnection myCon = new MySqlConnection(connString);

            MySqlCommand mySqlCommand1 = new MySqlCommand();
            mySqlCommand1.Connection = myCon;
            mySqlCommand1.CommandTimeout = 10;
            DataTable myDt = null;

            try
            {
                myCon.Open();
                //预防操作失败的一直进行 清除八分钟以前
                mySqlCommand1.CommandText = "delete from snmp_operate_request where timestamp<DATE_ADD(now(), INTERVAL - 8 MINUTE)";
                mySqlCommand1.ExecuteNonQuery();

                mySqlCommand1.CommandText = "select r.id, r.device_ip,r.ifindex,r.ifDescr,r.op_type,d.community_read,d.community_write,d.snmp_port from snmp_operate_request as r INNER JOIN device as d on r.device_ip = d.device_ip";
                MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand1);
                myDt = new DataTable();
                myDa.Fill(myDt);

                foreach (DataRow dr in myDt.Rows)
                {
                    int id = int.Parse(dr["id"].ToString());
                    string device_ip = dr["device_ip"].ToString();
                   
                    string community_string = "";
                    if (dr["community_write"] == null || dr["community_write"].ToString() == "")
                    {
                        community_string = dr["community_read"].ToString();
                    }
                    else
                    {
                        community_string = dr["community_write"].ToString();
                    }

                    string op_type = dr["op_type"].ToString();
                    int ifindex = int.Parse(dr["ifindex"].ToString() );
                    string ifDescr = dr["ifDescr"].ToString();
                    int snmp_port =int.Parse(dr["snmp_port"].ToString());

                    #region 删除已经捕获的 端口操作请求
                    mySqlCommand1.CommandText = "delete from snmp_operate_request where id=" + id.ToString();
                    mySqlCommand1.ExecuteNonQuery(); 
                    #endregion

                    if (Helper_snmp_port.op_port(device_ip, community_string, snmp_port, ifindex, op_type))
                    {
                        #region 操作端口日志  成功
                        mySqlCommand1.CommandText = string.Format("insert into  log_operate_port (device_ip,ifindex,ifDescr,log_content) values ('{0}',{1},'{2}','{3}')",
                           device_ip, ifindex.ToString(), ifDescr,op_type + "_成功");
                        mySqlCommand1.ExecuteNonQuery();
                        #endregion

                        #region 新线程 重新获取端口 开关状态
                        SnmpWorker[] workerObject = new SnmpWorker[1];
                        Thread[] workerThread = new Thread[1];
                        for (int i = 0; i < 1; i++)
                        {
                            workerObject[i] = new SnmpWorker();
                            workerThread[i] = new Thread(workerObject[i].doWork);
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            workerObject[i].init_Worker(device_ip, snmp_port, community_string, connString);
                            workerThread[i].Start();
                        }
                        #endregion
                    }
                    else
                    {
                        #region 操作端口日志 失败
                        mySqlCommand1.CommandText = string.Format("insert into  log_operate_port (device_ip,ifindex,ifDescr,log_content) values ('{0}',{1},'{2}','{3}')",
                            device_ip, ifindex.ToString(), ifDescr, op_type + "_失败");
                        mySqlCommand1.ExecuteNonQuery(); 
                        #endregion
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string error = ex.ToString();  //timeout
                is_busy = false;
                return;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                is_busy = false;
                return;
            }
            finally
            {
                myCon.Close();
            }

            is_busy = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;btn_stop.Visible = true;
            timer1.Enabled = true;
            label1.Text = "后台操作交换机端口正在运行！";
            label1.ForeColor = Color.Green;
                
            MessageBox.Show("操作网络设备端口的后台服务已经启动！");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            set_port();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            btn_stop.Visible = false;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            btnStart.Visible = true; btn_stop.Visible = false;
            timer1.Enabled = false;
            label1.Text = "后台操作交换机端口没有运行！";
            label1.ForeColor = Color.Red;

            MessageBox.Show("操作网络设备端口的后台服务已经停止！");
        }
    }
}
