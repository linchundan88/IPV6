<%@ page language="C#" autoeventwireup="true" inherits="detect_mac_switchport, App_Web_cd0ge4yt" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>查询MAC的接入交换机端口</title>
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
    
    </div>
        <uc1:WebUserControl_menu ID="WebUserControl_menu1" runat="server" />
        <p>
&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#0000CC" Text="请输入要查找的MAC地址："></asp:Label>
            <asp:TextBox ID="TextBox_MAC" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="BtnOk" runat="server" OnClick="BtnOk_Click" Text="查询" Font-Size="Medium" ForeColor="#0000CC" />
        </p>
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" DataSourceID="SqlDataSource1"  Skin="Outlook" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" Width="584px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="交换机IP" SortExpression="device_ip" UniqueName="device_ip">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sysName" FilterControlAltText="Filter sysName column" HeaderText="交换机名称" SortExpression="sysName" UniqueName="sysName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifdescr" FilterControlAltText="Filter ifdescr column" HeaderText="交换机端口说明" SortExpression="ifdescr" UniqueName="ifdescr">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifindex" FilterControlAltText="Filter ifindex column" HeaderText="接口索引号" SortExpression="ifindex" UniqueName="ifindex" DataType="System.Int64">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="num" FilterControlAltText="Filter num column" HeaderText="该端口下MAC数目" SortExpression="num" UniqueName="num" DataType="System.Int64">
                    </telerik:GridBoundColumn>             
                </Columns>
                <EditFormSettings>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" DeleteCommand="DELETE FROM device WHERE device_ip = ?" InsertCommand="INSERT INTO device ( device_ip ,  community_read ,  community_write ,  device_type ,    snmp_port ,  get_arp ,  get_mac_address ) VALUES (?, ?, ?, ?, ?, ?, ?)" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="detect_mac_switch_port" UpdateCommand="UPDATE device SET  community_read  = ?,  community_write  = ?,  device_type  = ?,   snmp_port  = ?,  get_arp  = ?,  get_mac_address  = ? WHERE  device_ip  = ?" SelectCommandType="StoredProcedure">
            <DeleteParameters>
                <asp:Parameter Name="device_ip" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="device_ip" Type="String" />
                <asp:Parameter Name="community_read" Type="String" />
                <asp:Parameter Name="community_write" Type="String" />
                <asp:Parameter Name="device_type" Type="String" />
                <asp:Parameter Name="snmp_port" Type="Int16" />
                <asp:Parameter Name="get_arp" Type="Int16" />
                <asp:Parameter Name="get_mac_address" Type="Int16" />
            </InsertParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBox_MAC" Name="v_mac" PropertyName="Text" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="community_read" Type="String" />
                <asp:Parameter Name="community_write" Type="String" />
                <asp:Parameter Name="device_type" Type="String" />
                <asp:Parameter Name="snmp_port" Type="Int16" />
                <asp:Parameter Name="get_arp" Type="Int16" />
                <asp:Parameter Name="get_mac_address" Type="Int16" />
                <asp:Parameter Name="device_ip" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <p>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0000CC" Text="说明：由于交换机级联，一个MAC地址会在多个交换机端口出现，通过计算每个端口的MAC数目，可以自动判断哪个是接入端口"></asp:Label>
        </p>
    </form>
</body>
</html>
