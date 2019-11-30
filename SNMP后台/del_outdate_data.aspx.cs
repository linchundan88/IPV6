using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class del_outdate_data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["p_manage"] == null || Session["p_manage"].ToString() != "1")
        {
            //Response.Write("<script language=javascript>alert('您没有这个权限！');window.top.document.location.href='Login.aspx';</script>");
            Server.Transfer("error.aspx");
            return;
        }

        RadDatePicker1.SelectedDate = DateTime.Now.AddDays(-365);
    }

    protected void btn_delete_outdate_Click(object sender, EventArgs e)
    {        
        string connectionString = WebConfigurationManager.ConnectionStrings["network_managementConnectionString"].ConnectionString;
        MySqlConnection myCon = new MySqlConnection(connectionString);
        try
        {
            myCon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.CommandTimeout = 400; //默认30秒
            mySqlCommand.Connection = myCon;
            mySqlCommand.CommandType = CommandType.StoredProcedure;
            mySqlCommand.CommandText = "delete_outdate_data";
            mySqlCommand.Parameters.AddWithValue("outdate_datetime", RadDatePicker1.SelectedDate.Value);
 
            mySqlCommand.ExecuteNonQuery();

            Response.Write("<script languge='javascript'>alert('清除历史数据成功!')</script>");

        }
        catch (Exception ex)
        {
            //有时候会timeout “Deadlock found when trying to get lock; try restarting transaction”
            Response.Write("<script languge='javascript'>alert('清除历史数据失败!')</script>");
            return;
        }
        finally
        {
            myCon.Close();
            myCon.Dispose();
        }
    }
}