<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_manage.aspx.cs" Inherits="user_manage" %>

<%@ Register src="WebUserControl_menu.ascx" tagname="WebUserControl_menu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户权限管理</title>
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


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:network_managementConnectionString %>" DeleteCommand="DELETE FROM mac_whitelist WHERE id=?" InsertCommand="INSERT INTO account(user_name,user_password,p_view,p_manage) VALUES (?,?,?,?)" ProviderName="<%$ ConnectionStrings:network_managementConnectionString.ProviderName %>" SelectCommand="SELECT id,user_name,user_password,p_view,p_manage  FROM account " UpdateCommand="UPDATE account SET user_name=?,user_password = ?,p_view=?,p_manage=? WHERE id=?">
            <DeleteParameters>
                <asp:Parameter Name="id" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="user_name" />
                <asp:Parameter Name="user_password" />
                <asp:Parameter Name="p_view" />
                <asp:Parameter Name="p_manage" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="user_name" Type="String" />
                <asp:Parameter Name="user_password" />
                <asp:Parameter Name="p_view" />
                <asp:Parameter Name="p_manage" />
                <asp:Parameter Name="id" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
            <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="#0000CC" Text="1表示有权限，0标识没有权限"></asp:Label>
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

                    <telerik:GridBoundColumn DataField="id" FilterControlAltText="Filter id column" HeaderText="id" SortExpression="id" UniqueName="id" DataType="System.Int32" ReadOnly="True" >
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="user_name" FilterControlAltText="Filter user_name column" HeaderText="用户帐号" SortExpression="user_name" UniqueName="user_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="user_password" FilterControlAltText="Filter user_password column" HeaderText="用户密码" SortExpression="user_password" UniqueName="user_password">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="p_view" FilterControlAltText="Filter p_view column" HeaderText="查看权限" SortExpression="p_view" UniqueName="p_view">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="p_manage" FilterControlAltText="Filter p_manage column" HeaderText="管理权限" SortExpression="p_manage" UniqueName="p_manage">
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
