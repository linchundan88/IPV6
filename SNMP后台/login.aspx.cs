using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string connString = ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString();
        MySqlConnection myCon = new MySqlConnection(connString);

        try
        {
            myCon.Open();
            string commandText = "select * from account where user_name=@p_user_name and user_password=@p_user_password ";

            MySqlCommand mySqlCommand = new MySqlCommand(commandText, myCon);
            mySqlCommand.Parameters.AddWithValue("@p_user_name", TextBox_username.Text.Trim());
            mySqlCommand.Parameters.AddWithValue("@p_user_password", TextBox_password.Text.Trim());
            MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand);

            DataTable dt = new DataTable();
            myDa.Fill(dt);
            if(dt.Rows.Count>0)
            {
                Session["username"] = TextBox_username.Text.Trim();
                Session["p_view"] = dt.Rows[0]["p_view"].ToString();
                Session["p_manage"] = dt.Rows[0]["p_manage"].ToString();
                if (dt.Rows[0]["p_manage"].ToString() == "1")
                {
                    Session["p_op_switch_port"] = "1";
                }                
                else
                {
                    Session["p_op_switch_port"] = dt.Rows[0]["p_op_switch_port"].ToString();
                }                    
                //Server.Transfer("iftable.aspx");
                Server.Transfer("detect_ndp.aspx");
            }
            else
            {
                TextBox_password.Text = "";
                Response.Write("<script languge='javascript'>alert('用户帐号或者密码错误')</script>");
            }
 
        }
        finally
        {
            myCon.Clone();
        }

    
    }
}