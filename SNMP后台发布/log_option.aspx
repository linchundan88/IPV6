<%@ page language="C#" autoeventwireup="true" inherits="log_option, App_Web_2iomidgz" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>设置记录日志的选项</title>
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
        <uc1:WebUserControl_menu ID="WebUserControl_menu1" runat="server" />
    <div>
    
    </div>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="SELECT * FROM log_option" UpdateCommand="UPDATE log_option  SET log_arp_success  = ?, log_arp_failure  = ?, log_mac_address_success  = ?, log_mac_address_failure  = ?, log_iftable_success  = ?, log_iftable_failure  = ?, log_sysobjectid_success  = ?, log_ysobjectid_failure  = ? WHERE id  = ?">
                <UpdateParameters>
                <asp:Parameter Name="log_arp_success" Type="Object" />
                <asp:Parameter Name="log_arp_failure" Type="Object" />
                <asp:Parameter Name="log_mac_address_success" Type="Object" />
                <asp:Parameter Name="log_mac_address_failure" Type="Object" />
                <asp:Parameter Name="log_iftable_success" Type="Object" />
                <asp:Parameter Name="log_iftable_failure" Type="Object" />
                <asp:Parameter Name="log_sysobjectid_success" Type="Object" />
                <asp:Parameter Name="log_ysobjectid_failure" Type="Object" />
                <asp:Parameter Name="id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>


        <br />
            <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="#0000CC" Text="1表示记录日志，0表示不记录日志"></asp:Label>


        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"  AllowPaging="True"   AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" DataSourceID="SqlDataSource1" GroupPanelPosition="Top" PageSize="25" Skin="Outlook"   CellSpacing="-1" GridLines="Both" Width="1224px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView CommandItemDisplay="None" DataKeyNames="id" DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
         
                <Columns>
                    <telerik:GridBoundColumn DataField="log_sysobjectid_success"   FilterControlAltText="Filter log_sysobjectid_success column" HeaderText="获取设备信息成功" SortExpression="log_sysobjectid_success" UniqueName="log_sysobjectid_success" DataType="System.SByte">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_ysobjectid_failure" DataType="System.SByte" FilterControlAltText="Filter log_ysobjectid_failure column" HeaderText="获取设备信息失败" SortExpression="log_ysobjectid_failure" UniqueName="log_ysobjectid_failure">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_iftable_success"   FilterControlAltText="Filter log_iftable_success column" HeaderText="获取设备接口成功" SortExpression="log_iftable_success" UniqueName="log_iftable_success" DataType="System.SByte">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_iftable_failure"   FilterControlAltText="Filter log_iftable_failure column" HeaderText="获取设备接口失败" SortExpression="log_iftable_failure" UniqueName="log_iftable_failure" DataType="System.SByte">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_arp_success"   FilterControlAltText="Filter log_arp_success column" HeaderText="获取ARP表成功" SortExpression="log_arp_success" UniqueName="log_arp_success" DataType="System.SByte">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_arp_failure"   FilterControlAltText="Filter log_arp_failure column" HeaderText="获取ARP表失败" SortExpression="log_arp_failure" UniqueName="log_arp_failure" DataType="System.SByte">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_mac_address_success"   FilterControlAltText="Filter log_mac_address_success column" HeaderText="获取MAC地址表成功" SortExpression="log_mac_address_success" UniqueName="log_mac_address_success" DataType="System.SByte">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_mac_address_failure"   FilterControlAltText="Filter log_mac_address_failure column" HeaderText="获取MAC地址表失败" SortExpression="log_mac_address_failure" UniqueName="log_mac_address_failure" DataType="System.SByte">
                    </telerik:GridBoundColumn>
                    
                    <telerik:GridEditCommandColumn EditText="修改" ButtonType="ImageButton" UniqueName="EditCommandColumn" CancelText="取消" UpdateText="保存" />                   
 
                </Columns>

                <EditFormSettings InsertCaption="新增">
                    <EditColumn  InsertText="保存" CancelText="取消" UpdateText="保存" FilterControlAltText="Filter EditCommandColumn1 column">               
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>


    </form>
</body>
</html>
