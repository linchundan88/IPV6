<%@ control language="C#" autoeventwireup="true" inherits="WebUserControl_menu, App_Web_xlfkemrn" %>
<telerik:RadMenu ID="RadMenu1" Runat="server">
    <Items>
        <telerik:RadMenuItem runat="server" Text="系统管理">
            <Items>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/user_manage.aspx" Text="用户权限管理">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="网络设备管理">
                    <Items>
                        <telerik:RadMenuItem runat="server" NavigateUrl="~/device1.aspx" Text="网络设备管理演示">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" NavigateUrl="~/device.aspx" Text="网络设备管理">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" NavigateUrl="~/device_import.aspx" Text="网络设备批量导入">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/mac_whitelist.aspx" Text="设置ARP检测的白名单">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/del_outdate_data.aspx" Text="清除过期数据">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/log_option.aspx" Text="设置记录日志选项">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="数据查询">
            <Items>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/iftable.aspx" Text="设备接口">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/iftable_mac_count.aspx" Text="设备接口的MAC数">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/vlan.aspx" Text="VLAN">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/arp.aspx" Text="ARP表">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/mac_address.aspx" Text="MAC地址表">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="数据分析">
            <Items>
                <telerik:RadMenuItem runat="server" Text="假冒主机的ARP欺骗" NavigateUrl="~/detect_arp.aspx">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/detect_mac_switchport.aspx" Text="某个MAC的接入端口">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="IPV6">
            <Items>
                <telerik:RadMenuItem runat="server" NavigateUrl="~/arp_new.aspx" Text="查询MAC-IP（IPV6）">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="检测IPV6 NDP欺骗" NavigateUrl="~/detect_ndp.aspx">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="操作日志">
            <Items>
                <telerik:RadMenuItem runat="server" Text="数据采集日志" NavigateUrl="~/log.aspx">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="端口操作日志" NavigateUrl="~/log_operate_port.aspx">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
    </Items>
</telerik:RadMenu>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>

