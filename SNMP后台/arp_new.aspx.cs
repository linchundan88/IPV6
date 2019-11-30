using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class arp_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["mac"] != null)  //检测ARP传递过来MAC
            {
                TextBox_MAC.Text = Request["mac"];          
            }
            else
                Session["init"] = 1;

            
        }
    }


    private void LoadDataForRadGrid1()
    {
        if (Session["init"] != null && Session["init"].ToString() == "1")
            return;

        string macAddress = TextBox_MAC.Text.Trim();
        string ipAddress = TextBox_IP.Text.Trim();

        //network_managementConnectionString
        string connString = ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString();
        MySqlConnection myCon = new MySqlConnection(connString);

        try
        {
            myCon.Open();
            string commandText = "";

            commandText = "select ipNetToPhysicalPhysAddress,ipNetToPhysicalNetAddress,ipNetToPhysicalType,ipNetToPhysicalState,ipNetToPhysicalRowStatus,timestamp from ipnettophysicalphysaddress where 1=1 ";

            if (ipAddress != "")
            {
                commandText = commandText + string.Format(" and ipNetToPhysicalNetAddress like '%{0}%'", ipAddress);
            }
            if (macAddress != "")
            {
                commandText = commandText + string.Format(" and ipNetToPhysicalPhysAddress like '%{0}%'", macAddress);
            }

            MySqlCommand mySqlCommand = new MySqlCommand(commandText, myCon);
            MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand);

            DataTable dt = new DataTable();
            myDa.Fill(dt);

            RadGrid1.DataSource = dt;
        }
        finally
        {
            myCon.Clone();
        }
    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        if (Session["init"] != null)
            Session.Remove("init");

        string macAddress = TextBox_MAC.Text.Trim();
        macAddress = macAddress.Replace("-", ""); macAddress = macAddress.Replace(":", "");
        macAddress = macAddress.Replace("'", ""); macAddress = macAddress.Replace("\"", "");
        macAddress = macAddress.ToUpper();
        TextBox_MAC.Text = macAddress;
        string ipAddress = TextBox_IP.Text.Trim();
        
        LoadDataForRadGrid1();
        RadGrid1.DataBind();

    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadDataForRadGrid1();
    }
}