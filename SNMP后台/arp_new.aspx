<%@ Page Language="C#" AutoEventWireup="true" CodeFile="arp_new.aspx.cs" Inherits="arp_new" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>MAC-IP（包括IPV6）</title>
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
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0000CC" Text="请输入MAC地址或者IPV6地址，然后点击查询，支持部分匹配。" Visible="False"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#0000CC" Text="MAC："></asp:Label>
        <asp:TextBox ID="TextBox_MAC" runat="server" Width="106px"></asp:TextBox>
        &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#0000CC" Text="IPV6地址："></asp:Label>
        <asp:TextBox ID="TextBox_IP" runat="server" Width="121px"></asp:TextBox>
&nbsp;<asp:Button ID="BtnOk" runat="server" OnClick="BtnOk_Click" Text="查询" Font-Size="Medium" ForeColor="#0000CC" />
        </p>
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" GroupPanelPosition="Top" Skin="Outlook" CellSpacing="-1" GridLines="Both" OnNeedDataSource="RadGrid1_NeedDataSource" Width="989px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView CommandItemDisplay="Top">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="ipNetToPhysicalNetAddress" FilterControlAltText="Filter ipNetToPhysicalNetAddress column" HeaderText="IP地址" SortExpression="ipNetToPhysicalNetAddress" UniqueName="ipNetToPhysicalNetAddress">
                    </telerik:GridBoundColumn>
                    <telerik:GridHyperLinkColumn  DataNavigateUrlFields="ipNetToPhysicalPhysAddress" DataNavigateUrlFormatString="mac_address.aspx?mac={0}"  HeaderText="MAC地址" DataTextField="ipNetToPhysicalPhysAddress"  SortExpression="ipNetToPhysicalPhysAddress"  FilterControlAltText="Filter ipNetToMediaPhysAddress column" UniqueName="ipNetToPhysicalPhysAddress">
                    </telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn DataField="ipNetToPhysicalType" DataType="System.SByte" FilterControlAltText="Filter ipNetToPhysicalType column" HeaderText="ipNetToPhysicalType" SortExpression="ipNetToPhysicalType" UniqueName="ipNetToPhysicalType">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="ipNetToPhysicalState" DataType="System.SByte" FilterControlAltText="Filter ipNetToPhysicalType column" HeaderText="ipNetToPhysicalState" SortExpression="ipNetToPhysicalState" UniqueName="ipNetToPhysicalState">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="ipNetToPhysicalRowStatus" DataType="System.SByte" FilterControlAltText="Filter ipNetToPhysicalType column" HeaderText="ipNetToPhysicalRowStatus" SortExpression="ipNetToPhysicalRowStatus" UniqueName="ipNetToPhysicalRowStatus">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="timestamp" DataType="System.DateTime" FilterControlAltText="Filter timestamp column" HeaderText="日期时间" SortExpression="timestamp" UniqueName="timestamp" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}">
                    </telerik:GridBoundColumn>
             
                </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    <p>
        &nbsp;</p>
    </form>
    <p>
        &nbsp;</p>
    </body>
</html>
