<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_operate_port.aspx.cs" Inherits="log" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>端口操作日志</title>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="SELECT * FROM log_operate_port">

        </asp:SqlDataSource>
        <asp:CheckBox ID="CheckBox_filter" runat="server" AutoPostBack="True" ForeColor="#0000CC" OnCheckedChanged="CheckBox_filter_CheckedChanged" Text="启用过滤查询" />
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" DataSourceID="SqlDataSource1"  Skin="Outlook" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" Width="878px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView DataKeyNames="id" DataSourceID="SqlDataSource1">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="设备IP地址" SortExpression="device_ip" UniqueName="device_ip">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifindex" FilterControlAltText="Filter ifindex column" HeaderText="接口索引" SortExpression="ifindex" UniqueName="ifindex">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifDescr" FilterControlAltText="Filter ifDescr column" HeaderText="接口描述" SortExpression="ifDescr" UniqueName="ifDescr">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="log_content" FilterControlAltText="Filter log_content column" HeaderText="日志内容" SortExpression="log_content" UniqueName="log_content">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" SortExpression="timestamp" UniqueName="timestamp" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}">
                        <HeaderStyle Width="200px" />
                    </telerik:GridBoundColumn>
             
                </Columns>
                <EditFormSettings>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>

</body>
</html>
