﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class iftable_mac_count : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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