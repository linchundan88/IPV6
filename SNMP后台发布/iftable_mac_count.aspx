<%@ page language="C#" autoeventwireup="true" inherits="iftable_mac_count, App_Web_akfkhorb" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>网络设备接口下面的MAC地址数目</title>
</head>
<body>
    <form id="form2" runat="server">
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


        <asp:CheckBox ID="CheckBox_filter" runat="server" AutoPostBack="True" ForeColor="#0000CC" OnCheckedChanged="CheckBox_filter_CheckedChanged" Text="启用过滤查询" />


        <telerik:RadGrid  ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"  AllowPaging="True"   AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" DataSourceID="SqlDataSource1" GroupPanelPosition="Top" PageSize="25" Skin="Outlook"   CellSpacing="-1" GridLines="Both" Width="688px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView CommandItemDisplay="None" DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <DetailTables>
                       <telerik:GridTableView DataKeyNames="device_ip,ifIndex" Name="ifTable" Width="100%" >
                            <Columns>
                                <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="设备IP地址" ReadOnly="True" SortExpression="device_ip" UniqueName="device_ip">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ifIndex" DataType="System.Int16" FilterControlAltText="Filter ifIndex column" HeaderText="接口索引号" ReadOnly="True" SortExpression="ifIndex" UniqueName="ifIndex">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ifDescr" FilterControlAltText="Filter ifDescr column" HeaderText="ifDescr" SortExpression="接口描述" UniqueName="ifDescr">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PhysAddress" FilterControlAltText="Filter PhysAddress column" HeaderText="接口MAC地址" SortExpression="PhysAddress" UniqueName="PhysAddress">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ifSpeed" DataType="System.Int64" FilterControlAltText="Filter ifSpeed column" HeaderText="接口速度" SortExpression="ifSpeed" UniqueName="ifSpeed">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ifAdminStatus" DataType="System.Int16" FilterControlAltText="Filter ifAdminStatus column" HeaderText="端口管理状态" SortExpression="ifAdminStatus" UniqueName="ifAdminStatus">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ifOperStatus" DataType="System.Int16" FilterControlAltText="Filter ifOperStatus column" HeaderText="端口当前状态" SortExpression="ifOperStatus" UniqueName="ifOperStatus">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" SortExpression="timestamp" UniqueName="timestamp" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn Text="打开"  ButtonType="PushButton" ConfirmText="确定打开该端口？" ConfirmDialogType="Classic" CommandName="Open"/>                    
                                <telerik:GridButtonColumn Text="关闭"  ButtonType="PushButton" ConfirmText="确定关闭该端口？" ConfirmDialogType="Classic" CommandName="Close"/>                    

                            </Columns>
                       </telerik:GridTableView>
                 </DetailTables>               

                <Columns>
                    <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="网络设备IP" ReadOnly="False" SortExpression="device_ip" UniqueName="device_ip">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifindex" FilterControlAltText="Filter ifindex column" HeaderText="ifindex" SortExpression="ifindex" UniqueName="ifindex" DataType="System.Int32">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifDescr" FilterControlAltText="Filter ifDescr column" HeaderText="接口描述" SortExpression="ifDescr" UniqueName="ifDescr">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="mac_count" DataType="System.Int64" FilterControlAltText="Filter mac_count column" HeaderText="MAC地址数目" SortExpression="mac_count" UniqueName="mac_count">
                    </telerik:GridBoundColumn>
                </Columns>

                <EditFormSettings>
                    <EditColumn  InsertText="保存" CancelText="取消" UpdateText="保存" FilterControlAltText="Filter EditCommandColumn1 column">
                    </EditColumn>
                 </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" DeleteCommand="DELETE FROM device WHERE id= ?" InsertCommand="INSERT INTO device ( device_ip ,  community_read ,  community_write ,  device_type ,    snmp_port ,  get_arp ,  get_mac_address ) VALUES (?, ?, ?, ?, ?, ?, ?)" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand=" select * from  view_iftable_mac_count" UpdateCommand="UPDATE device SET  community_read  = ?,  community_write  = ?,  device_type  = ?,   snmp_port  = ?,  get_arp  = ?,  get_mac_address  = ?,device_ip  = ? WHERE  id=?">
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
            <UpdateParameters>
                <asp:Parameter Name="community_read" Type="String" />
                <asp:Parameter Name="community_write" Type="String" />
                <asp:Parameter Name="device_type" Type="String" />
                <asp:Parameter Name="snmp_port" Type="Int16" />
                <asp:Parameter Name="get_arp" Type="Int16" />
                <asp:Parameter Name="get_mac_address" Type="Int16" />
                <asp:Parameter Name="device_ip" Type="String" />
                <asp:Parameter Name="id" />
            </UpdateParameters>
        </asp:SqlDataSource>


        <p>
            &nbsp;</p>


    </form>
    

</body>
</html>
