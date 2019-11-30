<%@ page language="C#" autoeventwireup="true" inherits="error, App_Web_jw0czbgv" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>错误</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
        <uc1:WebUserControl_menu ID="WebUserControl_menu1" runat="server" />
        
        <p></p>
                
        <h2> 对不起，您没有该操作的权限</h2>
    </div>
    </form>
</body>
</html>
