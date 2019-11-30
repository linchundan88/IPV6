<%@ page language="C#" autoeventwireup="true" inherits="device, App_Web_2iomidgz" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>二层设备的MAC地址表</title>
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
        &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#0000CC" Text="三层网络设备："></asp:Label>
        &nbsp;<asp:DropDownList ID="DropDownList_device" runat="server">
        </asp:DropDownList>
&nbsp;&nbsp; <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#0000CC" Text="MAC地址："></asp:Label>
        <asp:TextBox ID="TextBox_MAC" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="BtnOk" runat="server" OnClick="BtnOk_Click" Text="查询" Font-Size="Medium" ForeColor="#0000CC" />
        <br />
        <br />
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" PageSize="25" AllowSorting="True" AutoGenerateColumns="False" Culture="zh-CN" GroupPanelPosition="Top" Skin="Outlook" CellSpacing="-1" GridLines="Both" OnNeedDataSource="RadGrid1_NeedDataSource" Width="1153px">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView DataKeyNames="ifindex">
                <CommandItemSettings AddNewRecordText="新增" RefreshText="刷新" SaveChangesText="保存" CancelChangesText="取消" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridHyperLinkColumn  HeaderText="MAC地址" DataNavigateUrlFields="dot1dTpFdbAddress" DataNavigateUrlFormatString="detect_mac_switchport.aspx?mac={0}" DataTextField="dot1dTpFdbAddress" SortExpression="dot1dTpFdbAddress" FilterControlAltText="Filter dot1dTpFdbAddress column" UniqueName="dot1dTpFdbAddress">
                    </telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn DataField="device_ip" FilterControlAltText="Filter device_ip column" HeaderText="设备IP地址" SortExpression="device_ip" UniqueName="device_ip">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sysName" FilterControlAltText="Filter sysName column" HeaderText="设备名称" SortExpression="sysName" UniqueName="sysName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifindex" DataType="System.Int16" FilterControlAltText="Filter ifindex column" HeaderText="接口索引" ReadOnly="True" SortExpression="ifindex" UniqueName="ifindex">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ifDescr" FilterControlAltText="Filter ifDescr column" HeaderText="接口描述" SortExpression="ifDescr" UniqueName="ifDescr">
                    </telerik:GridBoundColumn>
             
                    <telerik:GridBoundColumn DataField="dot1dTpFdbPort" FilterControlAltText="Filter dot1dTpFdbPort column" HeaderText="桥端口号" SortExpression="dot1dTpFdbPort" UniqueName="dot1dTpFdbPort" DataType="System.Int16">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="dot1dTpFdbStatus" FilterControlAltText="Filter dot1dTpFdbStatus column" HeaderText="MAC地址项状态" SortExpression="dot1dTpFdbStatus" UniqueName="dot1dTpFdbStatus" DataType="System.Int16">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="vlan" DataType="System.Int16" FilterControlAltText="Filter vlan column" HeaderText="VLAN号" SortExpression="vlan" UniqueName="vlan">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Timestamp" FilterControlAltText="Filter Timestamp column" HeaderText="日期时间" SortExpression="Timestamp" UniqueName="Timestamp" DataType="System.DateTime" DataFormatString="{0:yyyy年/MM月/dd日 HH时mm分ss秒}">
                    </telerik:GridBoundColumn>
             
                </Columns>
                
                <EditFormSettings>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#0000CC" Text="说明： MAC地址项状态 other(1) invalid(2) learned(3) self(4) mgmt(5)"></asp:Label>
        </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </form>
    </body>
</html>
