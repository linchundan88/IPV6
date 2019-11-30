using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class iftable_tree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["p_manage"] == null || Session["p_manage"].ToString() != "1")
        {
            //Response.Write("<script language=javascript>alert('您没有这个权限！');window.top.document.location.href='Login.aspx';</script>");
            Server.Transfer("error.aspx");
            return;
        }        
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        //if (!e.IsFromDetailTable)
        //{
        //    RadGrid1.DataSource = GetDataTable("SELECT * FROM device");
        //}
    }

    public DataTable GetDataTable(string query)
    {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString(); ;
        MySqlConnection myCon = new MySqlConnection(connString);

        MySqlCommand mySqlCommand = new MySqlCommand();

        DataTable dt = new DataTable();
        try
        {
            myCon.Open();
            mySqlCommand.Connection = myCon;
            mySqlCommand.CommandText = query;
            MySqlDataAdapter da = new MySqlDataAdapter(mySqlCommand);
            da.Fill(dt);
        }
        finally
        {
            myCon.Close();
        }

        return dt;
    }

    protected void RadGrid1_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        string device_id = dataItem.GetDataKeyValue("id").ToString();

        e.DetailTableView.DataSource = GetDataTable("select * from  view_iftable where device_id = '" + device_id + "'");

    }

    protected void CheckBox_filter_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_filter.Checked)
        {            
            RadGrid1.AllowFilteringByColumn = true;
            RadGrid1.DataBind();
        }
        else
        {
            RadGrid1.AllowFilteringByColumn = false;
            RadGrid1.DataBind();
        }
    }
}