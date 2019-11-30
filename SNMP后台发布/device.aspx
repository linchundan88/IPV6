<%@ page language="C#" autoeventwireup="true" inherits="iftable_tree, App_Web_jw0czbgv" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>网络设备 及其接口</title>
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


        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"  AllowPaging="True"   AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" OnDetailTableDataBind="RadGrid1_DetailTableDataBind" DataSourceID="SqlDataSource1" GroupPanelPosition="Top" PageSize="25" Skin="Outlook"   CellSpacing="-1" GridLines="Both" Width="1380px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="id" DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <DetailTables>
                       <telerik:GridTableView DataKeyNames="device_ip,ifIndex" Name="ifTable" Width="100%">
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
                                <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" SortExpression="timestamp" UniqueName="timestamp" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}" >
                                </telerik:GridBoundColumn>
                            </Columns>
                       </telerik:GridTableView>
                 </DetailTables>               

                <Columns>
                    <telerik:GridEditCommandColumn EditText="修改" ButtonType="ImageButton" UniqueName="EditCommandColumn" CancelText="取消" UpdateText="保存" />                   
                    <telerik:GridButtonColumn Text="删除"  ButtonType="ImageButton" ConfirmText="确定删除该设备？" ConfirmDialogType="Classic" CommandName="Delete"/>                    

                    <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="设备IP地址" ReadOnly="False" SortExpression="device_ip" UniqueName="device_ip">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="location" FilterControlAltText="Filter location column" HeaderText="设备位置" ReadOnly="False" SortExpression="location" UniqueName="location">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sysName" FilterControlAltText="Filter sysName column" HeaderText="系统名" SortExpression="sysName" UniqueName="sysName" ReadOnly="True">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="community_read" FilterControlAltText="Filter community_read column" HeaderText="Community只读字符串" SortExpression="community_read" UniqueName="community_read">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="community_write" FilterControlAltText="Filter community_write column" HeaderText="Community读写字符串" SortExpression="community_write" UniqueName="community_write">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="device_type" FilterControlAltText="Filter device_type column" HeaderText="设备类型" SortExpression="device_type" UniqueName="device_type">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="snmp_port" DataType="System.Int16" FilterControlAltText="Filter snmp_port column" HeaderText="SNMP端口号" SortExpression="snmp_port" UniqueName="snmp_port" DefaultInsertValue="161">
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="get_arp" DataType="System.Boolean" FilterControlAltText="Filter column get_arp" HeaderText="获取ARP表" SortExpression="get_arp" StringFalseValue="0" StringTrueValue="1" UniqueName="column_get_arp">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="get_mac_address" DataType="System.Boolean" FilterControlAltText="Filter column get_mac_address" HeaderText="获取MAC地址表" SortExpression="get_mac_address" StringFalseValue="0" StringTrueValue="1" UniqueName="column_get_mac_address">
                    </telerik:GridCheckBoxColumn>             
                    <telerik:GridCheckBoxColumn DataField="enabled" DataType="System.Boolean" FilterControlAltText="Filter column enabled" HeaderText="是否启用" SortExpression="enabled" StringFalseValue="0" StringTrueValue="1" UniqueName="enabled">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" SortExpression="timestamp" UniqueName="timestamp" ReadOnly="True" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}">
                    </telerik:GridBoundColumn>
             
                </Columns>

                <EditFormSettings InsertCaption="新增">
                    <EditColumn  InsertText="保存" CancelText="取消" UpdateText="保存" FilterControlAltText="Filter EditCommandColumn1 column">               
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" DeleteCommand="DELETE FROM device WHERE id= ?" InsertCommand="INSERT INTO device ( device_ip ,  location, enabled, community_read ,  community_write ,  device_type ,    snmp_port ,  get_arp ,  get_mac_address ) VALUES (?, ?, ?, ?, ?, ?, ?, ?)" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="SELECT * FROM device" UpdateCommand="UPDATE device SET  community_read  = ?,  community_write  = ?,  device_type  = ?,   snmp_port  = ?,  get_arp  = ?,  get_mac_address  = ?,device_ip  = ?,location  = ?, enabled  = ? WHERE  id=?">
            <DeleteParameters>
                <asp:Parameter Name="device_ip" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="device_ip" Type="String" />
                <asp:Parameter Name="location"  Type="String" />
                <asp:Parameter Name="enabled" />
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
                <asp:Parameter Name="location"  Type="String"  />
                <asp:Parameter Name="enabled" />
            </UpdateParameters>
        </asp:SqlDataSource>


        <p>
            &nbsp;</p>


    </form>
    

</body>
</html>
