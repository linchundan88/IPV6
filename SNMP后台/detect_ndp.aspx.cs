using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class device : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        //有翻页等 ，不只是自己定义的
        if ( e.CommandName=="view_ip" )
        {
            GridDataItem item = (GridDataItem)e.Item;
            string mac = item["ipNetToPhysicalPhysAddress"].Text; // access the value in column using 'UniqueName' of that column 
            Server.Transfer("arp_new.aspx?mac=" + mac);
        }
        if (e.CommandName=="view_port")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string mac = item["ipNetToPhysicalPhysAddress"].Text; // access the value in column using 'UniqueName' of that column 
            Server.Transfer("mac_address.aspx?mac=" + mac);

        }        

    }
}