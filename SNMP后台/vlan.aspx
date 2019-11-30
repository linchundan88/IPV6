<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vlan.aspx.cs" Inherits="device" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>VLAN</title>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" DeleteCommand="DELETE FROM iftable WHERE device_ip = ? AND ifIndex = ?" InsertCommand="INSERT INTO iftable (device_ip, ifIndex, ifDescr, PhysAddress, ifSpeed, ifAdminStatus, ifOperStatus, timestamp) VALUES (?, ?, ?, ?, ?, ?, ?, ?)" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="SELECT * FROM vlan" UpdateCommand="UPDATE iftable SET ifDescr = ?, PhysAddress = ?, ifSpeed = ?, ifAdminStatus = ?, ifOperStatus = ?, timestamp = ? WHERE device_ip = ? AND ifIndex = ?">
            <DeleteParameters>
                <asp:Parameter Name="device_ip" Type="String" />
                <asp:Parameter Name="ifIndex" Type="Int16" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="device_ip" Type="String" />
                <asp:Parameter Name="ifIndex" Type="Int16" />
                <asp:Parameter Name="ifDescr" Type="String" />
                <asp:Parameter Name="PhysAddress" Type="String" />
                <asp:Parameter Name="ifSpeed" Type="Int64" />
                <asp:Parameter Name="ifAdminStatus" Type="Int16" />
                <asp:Parameter Name="ifOperStatus" Type="Int16" />
                <asp:Parameter Name="timestamp" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="ifDescr" Type="String" />
                <asp:Parameter Name="PhysAddress" Type="String" />
                <asp:Parameter Name="ifSpeed" Type="Int64" />
                <asp:Parameter Name="ifAdminStatus" Type="Int16" />
                <asp:Parameter Name="ifOperStatus" Type="Int16" />
                <asp:Parameter Name="timestamp" Type="DateTime" />
                <asp:Parameter Name="device_ip" Type="String" />
                <asp:Parameter Name="ifIndex" Type="Int16" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:CheckBox ID="CheckBox_filter" runat="server" AutoPostBack="True" ForeColor="#0000CC" OnCheckedChanged="CheckBox_filter_CheckedChanged" Text="启用过滤查询" />
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" DataSourceID="SqlDataSource1" GroupPanelPosition="Top" Skin="Outlook" CellSpacing="-1" GridLines="Both" Width="623px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView DataKeyNames="device_ip,vlan" DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="设备IP地址" ReadOnly="True" SortExpression="device_ip" UniqueName="device_ip">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="vlan" FilterControlAltText="Filter vlan column" HeaderText="VLAN号" SortExpression="vlan" UniqueName="vlan" DataType="System.Int32" ReadOnly="True">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name" FilterControlAltText="Filter name column" HeaderText="VLAN名称" SortExpression="name" UniqueName="name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="type" FilterControlAltText="Filter type column" HeaderText="vtpVlan类别" SortExpression="type" UniqueName="type" DataType="System.Int16" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" SortExpression="timestamp" UniqueName="timestamp" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}">
                    </telerik:GridBoundColumn>                          
                </Columns>
                <EditFormSettings>
                </EditFormSettings>

            </MasterTableView>
        </telerik:RadGrid>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
    </body>
</html>
