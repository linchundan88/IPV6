using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class device_import : System.Web.UI.Page
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

 
    protected void RadButton_import_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string filePath = Server.MapPath("~/UploadFiles/");
            string full_filename = filePath+ Guid.NewGuid().ToString(); // FileUpload1.PostedFile.FileName;
        
            FileUpload1.SaveAs( full_filename);

            DataTable dt_from_file = Helper_excel.ExcelToDataTable(full_filename, true);

            string connString = ConfigurationManager.ConnectionStrings["network_managementConnectionString"].ToString();
            MySqlConnection myCon = new MySqlConnection(connString);

            List<string> list_ip = new List<string>();
            try
            {
                myCon.Open();
                string commandText = "";

                commandText = "select device_ip from device ";

                MySqlCommand mySqlCommand = new MySqlCommand(commandText, myCon);
                MySqlDataAdapter myDa = new MySqlDataAdapter(mySqlCommand);

                DataTable dt_from_db = new DataTable();
                myDa.Fill(dt_from_db);             
            
                foreach(DataRow dr in dt_from_db.Rows)
                {
                    list_ip.Add(dr["device_ip"].ToString() );
                }
                  
                foreach(DataRow dr in dt_from_file.Rows)
                {
                    Boolean is_found = false; //避免IP重复
                    foreach (var ip_item in list_ip)
                    {
                        if (dr["device_ip"].ToString()==ip_item)
                            is_found = true;
                    }
                    if(is_found==false)  //和数据库原有记录不重复
                    {
                        #region 写入数据库，网络设备表
                        string commandText1 = "insert into device(device_ip,community_read,community_write,snmp_port,device_type,get_arp,get_mac_address)" +
                            " values(@device_ip,@community_read,@community_write,@snmp_port,@device_type,@get_arp,@get_mac_address) ";

                        mySqlCommand.Parameters.AddWithValue("@device_ip", dr["device_ip"].ToString());
                        mySqlCommand.Parameters.AddWithValue("@community_read", dr["community_read"].ToString());
                        mySqlCommand.Parameters.AddWithValue("@community_write", dr["community_write"].ToString());
                        mySqlCommand.Parameters.AddWithValue("@device_type", dr["device_type"].ToString());

                        if (dr["snmp_port"].ToString() != null && dr["snmp_port"].ToString().Trim() != "")
                            mySqlCommand.Parameters.AddWithValue("@snmp_port", int.Parse(dr["snmp_port"].ToString()));
                        else
                            mySqlCommand.Parameters.AddWithValue("@snmp_port", 161);

                        if (dr["get_arp"].ToString() != null && dr["get_arp"].ToString().Trim() != "")
                            mySqlCommand.Parameters.AddWithValue("@get_arp", int.Parse(dr["get_arp"].ToString()));
                        else
                            mySqlCommand.Parameters.AddWithValue("@get_arp", 0);

                        if (dr["get_mac_address"].ToString() != null && dr["get_mac_address"].ToString().Trim() != "")
                            mySqlCommand.Parameters.AddWithValue("@get_mac_address", int.Parse(dr["get_mac_address"].ToString()));
                        else
                            mySqlCommand.Parameters.AddWithValue("@get_mac_address", 0);

                        MySqlCommand mySqlCommand1 = new MySqlCommand();
                        mySqlCommand.Connection = myCon;
                        mySqlCommand.CommandText = commandText1;
                        mySqlCommand.ExecuteNonQuery(); 
                        #endregion
                    }
                }             

            }
            catch (Exception ex)
            {
                Response.Write("<script languge='javascript'>alert('数据写入错误，请检查是否有IP重复数据，以及其他数据格式！')</script>");
            }
            finally
            {
                myCon.Clone();
            }

            Response.Write("<script language=javascript>alert('文件导入成功！');window.top.document.location.href='device.aspx';</script>");
 
        }
        else
        {
            Response.Write("<script languge='javascript'>alert('没有选中的文件！')</script>");
        }
       
    }

    protected void RadButton_download_Click(object sender, EventArgs e)
    {
        Response.Redirect("./file/template.xlsx");
    }
}