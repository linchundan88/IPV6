<%@ Page Language="C#" AutoEventWireup="true" CodeFile="welcome.aspx.cs" Inherits="welcome" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>网络管理系统</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

    <p> </p>

    <h2>汕头大学网络管理系统</h2>
            
    <p>
        欢迎使用汕头大学网络管理系统，本系统由汕头大学开发，本系统包括以下功能：使用SNMP协议自动采集网络设备接口，桥端口表，VLAN，MAC地址表，ARP表（IPV4和IPV6）并写入数据库，可以查看网络设备端口状态（开通、关闭），能够通过Web界面开通关闭端口（例如接入交换机），可以根据MAC查询IP（或者相反），根据MAC查询交换机端口，可以检测假冒其他主机的ARP欺骗等。</p>
    <p>
        本系统的代码（包括SNMP数据采集程序，开通断开网络设备端口后台程序，自动使用SNMP扫描某个IP地址范围自动添加网络设备的程序，和Web网站）和数据库全部开源，
    
        任何组织和个人都可以随意使用。 <a href="http://wechat.stu.edu.cn/code.zip">源代码下载</a>
        此外我们配置了一个虚拟机，已经安装好了发布和开发环境，更加方便使用，由于虚拟机比较大，如有需要请发邮件给我。
    </p>

    <p>
        &nbsp;</p>

    <p>您也可以直接查看演示环境，帐号:guest, 密码:test （可以查看数据，不能增加、管理设备。）</p>
    <h3>
    &nbsp;<a href="login.aspx">登录演示环境</a>&nbsp;        
        &nbsp;</h3>
    <p>
        如有任何问题，请联系 jji@stu.edu.cn
    </p>
    
</body>
</html>
