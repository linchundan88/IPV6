<%@ page language="C#" autoeventwireup="true" inherits="del_outdate_data, App_Web_0dzdmd5p" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>清除过期数据</title>
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
    
    </div>
        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Red" Text="清除该日期之前的数据"></asp:Label>
        <telerik:RadDatePicker ID="RadDatePicker1" Runat="server" Culture="zh-CN">
               
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="zh-CN" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

<DateInput DisplayDateFormat="yyyy年/M月/d日" DateFormat="yyyy/M/d" LabelWidth="40%">
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
               
        </telerik:RadDatePicker>
        <asp:Button ID="btn_delete_outdate" runat="server" Font-Size="Medium" ForeColor="Red" OnClick="btn_delete_outdate_Click" Text="确定清除" />
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="#0000CC" Text="本操作清除ARP表、MAC地址表、日志、网络设备接口等自动采集或者系统自动产生的数据，网络设备等所有手工输入的数据不会清除。"></asp:Label>
        </p>
    </form>
</body>
</html>
