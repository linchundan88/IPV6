<%@ Page Language="C#" AutoEventWireup="true" CodeFile="device_import.aspx.cs" Inherits="device_import" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>网络设备批量导入</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
    <div>
    
        <uc1:WebUserControl_menu ID="WebUserControl_menu1" runat="server" />
        <p></p>
    </div>
        <telerik:RadButton ID="RadButton_download" runat="server" OnClick="RadButton_download_Click" Skin="Outlook" style="position: relative;" Text="下载模版">
        </telerik:RadButton>
&nbsp;<br />
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<telerik:RadButton ID="RadButton_import" runat="server" OnClick="RadButton_import_Click" Skin="Outlook" Text="导入文件">
        </telerik:RadButton>
        <br />
        <br />
        
        <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0000CC" Text="说明：请下载模版，一条条填写记录，每一条包括设备IP地址、SNMP只读、SNMP读写字符串、SNMP端口、设备类型（可以不填）、是否获取ARP表（0和1，空白表示0）、是否获取MAC地址表（0和1，空白表示0），然后上传进行批量导入。"></asp:Label>
        </p>
        
    </form>
</body>
</html>
