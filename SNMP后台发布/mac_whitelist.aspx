<%@ page language="C#" autoeventwireup="true" inherits="mac_whitelist, App_Web_akfkhorb" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>设置ARP监测的白名单</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:WebUserControl_menu ID="WebUserControl_menu1" runat="server" />
    
    </div>
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


        <asp:CheckBox ID="CheckBox_filter" runat="server" AutoPostBack="True" ForeColor="#0000CC" OnCheckedChanged="CheckBox_filter_CheckedChanged" Text="启用过滤查询" />


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" DeleteCommand="DELETE FROM mac_whitelist WHERE id=?" InsertCommand="INSERT INTO mac_whitelist (PhysAddress, source_type) VALUES (?, '手工输入')" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="SELECT id, PhysAddress, source_type,timestamp FROM mac_whitelist" UpdateCommand="UPDATE mac_whitelist SET PhysAddress =?,source_type = ? WHERE id=?">
            <DeleteParameters>
                <asp:Parameter Name="id" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="PhysAddress" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="PhysAddress" Type="String" />
                <asp:Parameter Name="source_type" Type="String" />
                <asp:Parameter Name="id" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"  AllowPaging="True"   AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" DataSourceID="SqlDataSource1" GroupPanelPosition="Top" PageSize="25" Skin="Outlook" CellSpacing="-1" GridLines="Both" Width="539px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="id" DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridEditCommandColumn EditText="修改" ButtonType="ImageButton" UniqueName="EditCommandColumn" CancelText="取消" UpdateText="保存" />                   
                    <telerik:GridButtonColumn Text="删除"  ButtonType="ImageButton" ConfirmText="确定删除该设备？" ConfirmDialogType="Classic" CommandName="Delete"/>                    
                    <telerik:GridBoundColumn DataField="PhysAddress" FilterControlAltText="Filter PhysAddress column" HeaderText="MAC地址" SortExpression="PhysAddress" UniqueName="PhysAddress">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="source_type" FilterControlAltText="Filter source_type column" HeaderText="MAC地址的来源" SortExpression="source_type" UniqueName="source_type" ReadOnly="True">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" ReadOnly="True" SortExpression="timestamp" UniqueName="timestamp">
                    </telerik:GridBoundColumn>             
                </Columns>
                
                <EditFormSettings InsertCaption="新增">
                    <EditColumn  CancelText="取消"  InsertText="保存" UpdateText="保存" FilterControlAltText="Filter EditCommandColumn1 column">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    <p>
        &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
    </body>
</html>
