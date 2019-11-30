<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detect_ndp.aspx.cs" Inherits="device" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>检测IPV6的NDP欺骗</title>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" DeleteCommand="DELETE FROM iftable WHERE device_ip = ? AND ifIndex = ?" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="select * from view_detect_ndp">
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
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0000CC" Text="通过MAC-IPV6地址变化规律，检测假冒主机的NDP欺骗"></asp:Label>
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" DataSourceID="SqlDataSource1"  Skin="Outlook" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" Width="517px" OnItemCommand="RadGrid1_ItemCommand">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="ipNetToPhysicalPhysAddress" FilterControlAltText="Filter ipNetToPhysicalPhysAddress column" HeaderText="MAC地址" SortExpression="ipNetToPhysicalPhysAddress" UniqueName="ipNetToPhysicalPhysAddress">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="num" FilterControlAltText="Filter mac_num column" HeaderText="不同IP地址数目" SortExpression="mac_num" UniqueName="mac_num" DataType="System.Int64">
                    </telerik:GridBoundColumn>             
                    <telerik:GridButtonColumn CommandName="view_ip" FilterControlAltText="Filter column column" Text="查看具体IPV6地址" UniqueName="column">
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn CommandName="view_port" FilterControlAltText="Filter column1 column" Text="该MAC的交换机端口" UniqueName="column1">
                    </telerik:GridButtonColumn>             
                </Columns>
                <EditFormSettings>

                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <br />
        <br />
        <br />
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
