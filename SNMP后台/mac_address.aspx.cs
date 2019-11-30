using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

public partial class device : System.Web.UI.Page
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
            else
            {
                Session["init"] = 1;
            }

           

            string connString = ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString();
            MySqlConnection myCon = new MySqlConnection(connString);
            try
            {
                myCon.Open();
                string commandText = "select device_ip from device order by device_ip ";

                MySqlCommand mySqlCommand = new MySqlCommand(commandText, myCon);
                MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand);

                DataTable dt = new DataTable();
                myDa.Fill(dt);

                DropDownList_device.Items.Clear();
                DropDownList_device.Items.Add("全部");
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownList_device.Items.Add(dr["device_ip"].ToString());
                }
            }
            finally
            {
                myCon.Clone();
            }
        }
    }



    private void LoadDataForRadGrid1()
    {
        if (Session["init"] != null && Session["init"].ToString() == "1")
            return;

        string macAddress = TextBox_MAC.Text.Trim();

        //network_managementConnectionString
        string connString = ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString();
        MySqlConnection myCon = new MySqlConnection(connString);

        try
        {
            myCon.Open();
            string commandText = "";

            if (DropDownList_device.SelectedValue== "全部")
                commandText = "select * from view_mac_address_table where 1=1 ";
            else
                commandText = string.Format("select * from view_mac_address_table where device_ip='{0}'" , DropDownList_device.SelectedValue);
                           
            if (macAddress != "")
            {
                commandText = commandText + string.Format(" and dot1dTpFdbAddress like '%{0}%'", macAddress);
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
        macAddress = macAddress.ToUpper();
        TextBox_MAC.Text = macAddress;

        LoadDataForRadGrid1();
        RadGrid1.DataBind();
    }

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        LoadDataForRadGrid1();
    }
}