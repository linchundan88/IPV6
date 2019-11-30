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

    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Open" || e.CommandName == "Close")
        {
            if (Session["p_op_switch_port"] == null || Session["p_op_switch_port"].ToString() != "1")
            {
                Response.Write("<script language=javascript>alert('您没有操作网络设备端口的权限！');window.top.document.location.href='Login.aspx';</script>");
                return;
            }

            GridDataItem item = (GridDataItem)e.Item;

            string ifAdminStatus = item["ifAdminStatus"].Text;
            string device_ip = item["device_ip"].Text;
            string ifindex = item["ifIndex"].Text;
            string ifDescr = item["ifDescr"].Text;

            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["network_managementConnectionString"].ConnectionString;
            MySqlConnection myCon = new MySqlConnection(connectionString);
            try
            {
                myCon.Open();

                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.CommandTimeout = 5; //默认30秒
                mySqlCommand.Connection = myCon;
                
                mySqlCommand.CommandText = string.Format("insert into snmp_operate_request(user_account,device_ip,ifindex,ifDescr,op_type)  values('{0}','{1}',{2},'{3}','{4}')",
                    Session["username"].ToString(), device_ip, ifindex, ifDescr,e.CommandName);

                mySqlCommand.ExecuteNonQuery();

                Response.Write("<script languge='javascript'>alert('您已经成功发送操作端口请求，十秒钟后查看日志!')</script>");

            }
            catch (Exception ex)
            {
                //有时候会timeout “Deadlock found when trying to get lock; try restarting transaction”
                Response.Write("<script languge='javascript'>alert('写入数据库失败!')</script>");
                return;
            }
            finally
            {
                myCon.Close();
                myCon.Dispose();
            }

        }
    }
}