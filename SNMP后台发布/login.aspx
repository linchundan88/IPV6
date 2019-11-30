<%@ page language="C#" autoeventwireup="true" inherits="login, App_Web_xlfkemrn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登陆验证</title>
</head>
<body>
    <p>
        <br />
    </p>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="#3333FF" Text="用户帐号："></asp:Label>
        <asp:TextBox ID="TextBox_username" runat="server" Width="100px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="#3333FF" Text="用户密码："></asp:Label>
        <asp:TextBox ID="TextBox_password" runat="server" TextMode="Password" Width="100px"></asp:TextBox>
        <br />
&nbsp;<br />
        <asp:Button ID="btn_ok" runat="server" Font-Size="Medium" OnClick="btn_ok_Click" Text="确定" ForeColor="#3333FF" />
    </form>
</body>
</html>
