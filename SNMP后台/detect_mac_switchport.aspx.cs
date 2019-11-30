using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;


public partial class detect_mac_switchport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["mac"] != null)  //检测ARP传递过来MAC
            {
                TextBox_MAC.Text = Request["mac"];
                //BtnOk_Click(sender, e);
            }
        }

    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        string macAddress = TextBox_MAC.Text.Trim();
        macAddress = macAddress.Replace("-", ""); macAddress = macAddress.Replace(":", "");
        macAddress = macAddress.Replace("'", "");
        macAddress = macAddress.Replace("\"", "");
        macAddress = macAddress.ToUpper();
        TextBox_MAC.Text = macAddress;

        string connString = ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString();
        MySqlConnection myCon = new MySqlConnection(connString);

        //try
        //{
        //    myCon.Open();

        //    MySqlCommand mySqlCommand = new MySqlCommand("detect_mac_switch_port", myCon);
        //    mySqlCommand.CommandType = CommandType.StoredProcedure;
        //    mySqlCommand.Parameters.Add(new MySqlParameter("v_mac", SqlDbType.VarChar) { Value = macAddress });

        //    MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand);

        //    DataTable dt = new DataTable();
        //    myDa.Fill(dt);

        //    RadGrid1.DataSource = dt;
        //}
        //finally
        //{
        //    myCon.Clone();
        //}
    }
}