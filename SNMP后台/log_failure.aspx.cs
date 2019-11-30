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

public partial class log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            RadDateTimePicker1.SelectedDate = DateTime.Now.AddDays(-1);
            RadDateTimePicker2.SelectedDate = DateTime.Now;
        }
    }

    protected void CheckBox_filter_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_filter.Checked)
        {
            RadGrid1.AllowFilteringByColumn = true;           
        }
        else
        {
            RadGrid1.AllowFilteringByColumn = false;            
        }
        needDataSource();
        RadGrid1.DataBind();
    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        needDataSource();
        RadGrid1.DataBind();
    }

    public void needDataSource()
    {
        string connString = ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString();
        MySqlConnection myCon = new MySqlConnection(connString);
     
        try
        {
            myCon.Open();
            string commandText = "";

            commandText = string.Format("select * from view_log where timestamp>@start_date and timestamp<@end_date and log_type_id>100");
                      
            MySqlCommand mySqlCommand = new MySqlCommand(commandText, myCon);
            mySqlCommand.Parameters.AddWithValue("@start_date",RadDateTimePicker1.SelectedDate.Value);
            mySqlCommand.Parameters.AddWithValue("@end_date", RadDateTimePicker2.SelectedDate.Value);

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

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        needDataSource();
    }
}