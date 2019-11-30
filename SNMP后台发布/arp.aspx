<%@ page language="C#" autoeventwireup="true" inherits="device, App_Web_grenkvz0" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>MAC-IP</title>
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
    
    </div>
        &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#0000CC" Text="MAC："></asp:Label>
        <asp:TextBox ID="TextBox_MAC" runat="server" Width="106px"></asp:TextBox>
        &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#0000CC" Text="IP："></asp:Label>
        <asp:TextBox ID="TextBox_IP" runat="server" Width="121px"></asp:TextBox>
&nbsp;<asp:Button ID="BtnOk" runat="server" OnClick="BtnOk_Click" Text="查询" Font-Size="Medium" ForeColor="#0000CC" />
        <br />
        <br />
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" GroupPanelPosition="Top" Skin="Outlook" CellSpacing="-1" GridLines="Both" OnNeedDataSource="RadGrid1_NeedDataSource" Width="551px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView CommandItemDisplay="Top">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="ipNetToMediaNetAddress" FilterControlAltText="Filter ipNetToMediaNetAddress column" HeaderText="IP地址" SortExpression="ipNetToMediaNetAddress" UniqueName="ipNetToMediaNetAddress">
                    </telerik:GridBoundColumn>
                    <telerik:GridHyperLinkColumn  DataNavigateUrlFields="ipNetToMediaPhysAddress" DataNavigateUrlFormatString="mac_address.aspx?mac={0}"  HeaderText="MAC地址" DataTextField="ipNetToMediaPhysAddress"  SortExpression="ipNetToMediaPhysAddress"  FilterControlAltText="Filter ipNetToMediaPhysAddress column" UniqueName="ipNetToMediaPhysAddress">
                    </telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn DataField="ipNetMediaType" DataType="System.SByte" FilterControlAltText="Filter ipNetMediaType column" HeaderText="MAC-IP类型" SortExpression="ipNetMediaType" UniqueName="ipNetMediaType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" SortExpression="timestamp" UniqueName="timestamp" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}">
                    </telerik:GridBoundColumn>
             
                </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0000CC" Text="说明： MAC-IP类型 other(1) invalid(2) dynamic(3) static(4)"></asp:Label>
        </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
    </body>
</html>
