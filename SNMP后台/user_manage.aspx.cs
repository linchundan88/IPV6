using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_manage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["p_manage"]==null || Session["p_manage"].ToString() != "1")
        {
            //Response.Write("<script language=javascript>alert('您没有这个权限！');window.top.document.location.href='Login.aspx';</script>");
            Server.Transfer("error.aspx");
            return;
        }
    }
}