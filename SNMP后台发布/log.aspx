<%@ page language="C#" autoeventwireup="true" inherits="log, App_Web_jw0czbgv" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>自动数据采集日志</title>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="SELECT * FROM log where 1&gt;2">

        </asp:SqlDataSource>
        <asp:Label ID="Label1" runat="server" Text="开始日期："></asp:Label>
        <telerik:RadDateTimePicker ID="RadDateTimePicker1" Runat="server" Skin="Outlook">
<TimeView CellSpacing="-1" Culture="zh-CN"></TimeView>

<TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="zh-CN" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Outlook"></Calendar>

<DateInput DisplayDateFormat="yyyy/M/d" DateFormat="yyyy/M/d" LabelWidth="40%">
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
        </telerik:RadDateTimePicker>
&nbsp;<asp:Label ID="Label2" runat="server" Text="结束日期："></asp:Label>
        <telerik:RadDateTimePicker ID="RadDateTimePicker2" Runat="server" Skin="Outlook">
<TimeView CellSpacing="-1" Culture="zh-CN"></TimeView>

<TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="zh-CN" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Outlook"></Calendar>

<DateInput DisplayDateFormat="yyyy/M/d" DateFormat="yyyy/M/d" LabelWidth="40%">
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
        </telerik:RadDateTimePicker>
        <asp:Button ID="BtnOk" runat="server" OnClick="BtnOk_Click" Text="查询" Font-Size="Medium" ForeColor="#0000CC" />
        <br />
        <br />
        <asp:CheckBox ID="CheckBox_filter" runat="server" AutoPostBack="True" ForeColor="#0000CC" OnCheckedChanged="CheckBox_filter_CheckedChanged" Text="启用过滤查询" />
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN"  Skin="Outlook" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" Width="633px" OnNeedDataSource="RadGrid1_NeedDataSource">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView DataKeyNames="id">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="设备IP地址" SortExpression="device_ip" UniqueName="device_ip">
                        <HeaderStyle Width="150px" />
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
