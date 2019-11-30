using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace SNMP
{
    class LogOption
    {
        public Boolean log_arp_success { get; set; }
        public Boolean log_arp_failure { get; set; }

        public Boolean log_mac_address_success { get; set; }
        public Boolean log_mac_address_failure { get; set; }

        public Boolean log_iftable_success { get; set; }
        public Boolean log_iftable_failure { get; set; }

        public Boolean log_sysobjectid_success { get; set; }
        public Boolean log_sysobjectid_failure { get; set; }

        public LogOption()
        {
            //防止mysql异常，先初始化值
            log_arp_success = false; log_arp_failure = true;
            log_mac_address_success = false; log_mac_address_failure = true;
            log_iftable_success = false; log_iftable_failure = true;
            log_sysobjectid_success = false; log_sysobjectid_failure = true;

            string connString = ConfigurationManager.ConnectionStrings["connectingStringMySQL"].ToString();
            MySqlConnection myCon = new MySqlConnection(connString);

            string commandText = "select * from log_option";
            MySqlCommand mySqlCommand = new MySqlCommand(commandText, myCon);
            DataTable myDt = null;

            try
            {
                myCon.Open();
                MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand);
                myDt = new DataTable();
                myDa.Fill(myDt);

                if (myDt.Rows[0]["log_arp_success"].ToString() == "1")
                    log_arp_success = true;
                else
                    log_arp_success = false;

                if (myDt.Rows[0]["log_arp_failure"].ToString() == "1")
                    log_arp_failure = true;
                else
                    log_arp_failure = false;

                if (myDt.Rows[0]["log_mac_address_success"].ToString() == "1")
                    log_mac_address_success = true;
                else
                    log_mac_address_success = false;

                if (myDt.Rows[0]["log_mac_address_failure"].ToString() == "1")
                    log_mac_address_failure = true;
                else
                    log_mac_address_failure = false;

                if (myDt.Rows[0]["log_iftable_success"].ToString() == "1")
                    log_iftable_success = true;
                else
                    log_iftable_success = false;

                if (myDt.Rows[0]["log_iftable_failure"].ToString() == "1")
                    log_iftable_failure = true;
                else
                    log_iftable_failure = false;

                if (myDt.Rows[0]["log_sysobjectid_success"].ToString() == "1")
                    log_sysobjectid_success = true;
                else
                    log_sysobjectid_success = false;

                if (myDt.Rows[0]["log_ysobjectid_failure"].ToString() == "1")
                    log_sysobjectid_failure = true;
                else
                    log_sysobjectid_failure = false;
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
            finally
            {
                myCon.Close();
                myCon.Dispose();
            }
        }

    }
}
