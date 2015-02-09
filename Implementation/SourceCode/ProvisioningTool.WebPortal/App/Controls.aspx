<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Controls.aspx.cs" EnableViewState="false" Inherits="Settings " %>

<%@ Register Src="~/includes/UserControls/common/Master.ascx" TagName="Master" TagPrefix="ProvisioningTool" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" class="iframeBodyClass">
    <title>Settings</title>
    <ProvisioningTool:Master ID="Master" runat="server" />

    <script type="text/javascript">
        var siteID = $.cookie("siteID");
        if (siteID == "" || siteID == null) {
            siteID = 0;
            $('#headerCustomer').hide();
        }

        $(document).ready(function () {
            var menu = getQueryStringByName('menu');
            
            if (menu != "")
            { $("#"+menu).css("display","block"); }
            


           
        });


        function HardwareEvent(ucPath, msg, selectedId, hidglobal, types, models) {
            $('#hidTabname').val(msg);
            $('#hidMaster').val(ucPath);
            $('#hidglobal').val(hidglobal);
            $.cookie('lefttab', selectedId);
            var menu = getQueryStringByName('menu');

            if ((msg == "Applications" && menu == "Software") || menu == "Hardware")
                window.location = "ControlModels.aspx?tabName=" + msg + "&menu=" + msg + "&types=" + types + "&models=" + models;
            else if (menu == "SystemLocale" || menu == "SystemUser" || menu == "Software" || menu == "ServiceProvider")
                window.location = "Controls.aspx?trans=1&ucPath=" + ucPath + "&tabName=" + msg + "&hidglobal=" + hidglobal + "&menu=" + menu + "&types=" + types + "&models=" + models;

            return false;
        }

      
    </script>

</head>
<body id="PageBody" runat="server" class="adminContent">
    <form id="Custmain" runat="server">
        <div class="divMessage" id="divMessage" runat="server">
            <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        </div>
         <div id="dialog-message" title="Warning">
                    <%--<div>Do you want to Submit</div>--%>
                    <div id="relatedRecordDiv1"></div>
                </div>
                <div id="dialogr" title="Warning" > <%--class="dialogr"--%>
                    <div id="relatedRecordDiv"></div>
                </div>
        <div class="fullWidthGrid">
            <div class="leftMenuWrapper">
                <ul id="SystemLocale" class="hideDefault">
                    <li>
                        <div class="largeNav" id="aCity" runat="server" onclick="HardwareEvent('Global','Cities', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSystemLocale/cities.png" />
                            <h3>Cities</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="aStates" runat="server" onclick="HardwareEvent('Global','States', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSystemLocale/state.png" />
                            <h3>States</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="aConutry" runat="server" onclick="HardwareEvent('Global','Countries', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSystemLocale/country.png" />
                            <h3>Countries</h3>
                        </div>
                    </li>
                </ul>

                <ul id="SystemUser" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div1" runat="server" onclick="HardwareEvent('Global','Titles', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSystemUser/title.png" />
                            <h3>Titles</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div2" runat="server" onclick="HardwareEvent('Global','Departments', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSystemUser/departments.png" />
                            <h3>Departments</h3>
                        </div>
                    </li>
                </ul>

                <ul id="Hardware" class="hideDefault hardware-leftMenu">
                    <li>
                        <div class="largeNav" id="Div3" runat="server" onclick="HardwareEvent('Global','Workstations', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/workstations.png" />
                            <h3>Workstations</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div4" runat="server" onclick="HardwareEvent('Global','Router', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/routers.png" />
                            <h3>Router</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div6" runat="server" onclick="HardwareEvent('Global','PhoneSystem', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/phone-system.png" />
                            <h3>Phone System</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div7" runat="server" onclick="HardwareEvent('Global','Phone', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/phone.png" />
                            <h3>Phone</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div8" runat="server" onclick="HardwareEvent('Global','MobileDevice', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/mobile-device.png" />
                            <h3>Mobile Device</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div9" runat="server" onclick="HardwareEvent('Global','Servers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/server.png" />
                            <h3>Servers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div10" runat="server" onclick="HardwareEvent('Global','Laptops', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/laptop.png" />
                            <h3>Laptops</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div11" runat="server" onclick="HardwareEvent('Global','Printer', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/printers.png" />
                            <h3>Printer</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div12" runat="server" onclick="HardwareEvent('Global','Wireless', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/wireless.png" />
                            <h3>Wireless</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div5" runat="server" onclick="HardwareEvent('Global','Firewall', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/firewall.png" />
                            <h3>Firewall</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div13" runat="server" onclick="HardwareEvent('Global','NetworkSwitch', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/network-switch.png" />
                            <h3>Network Switch</h3>
                        </div>
                    </li>
                </ul>

                <ul id="Software" class="hideDefault software-leftMenu">
                    <li>
                        <div class="largeNav" id="Div14" runat="server" onclick="HardwareEvent('Global','Operating-Systems', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftware/operating-sys.png" />
                            <h3>Operating Systems</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div15" runat="server" onclick="HardwareEvent('Global','Antivirus', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftware/anti-virus.png" />
                            <h3>Antivirus</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div16" runat="server" onclick="HardwareEvent('Global','Backup', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftware/backup.png" />
                            <h3>Backup</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div17" runat="server" onclick="HardwareEvent('Global','Remote-Access', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftware/remote-access.png" />
                            <h3>Remote Access</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div18" runat="server" onclick="HardwareEvent('Global','Applications', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftware/appications.png" />
                            <h3>Applications</h3>
                        </div>
                    </li>
                </ul>

                <ul id="ServiceProvider" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div19" runat="server" onclick="HardwareEvent('Global','Providers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsServiceProviders/provider.png" />
                            <h3>Providers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div20" runat="server" onclick="HardwareEvent('Global','Provider-Types', 1, 1)">
                            <img src="../../includes/UI/images/SettingsServiceProviders/provider-type.png" />
                            <h3>Provider Types</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div21" runat="server" onclick="HardwareEvent('Global','Circuit-Types', 1, 1)">
                            <img src="../../includes/UI/images/SettingsServiceProviders/circuit-type.png" />
                            <h3>Circuit Types</h3>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="rightContent">
                <div id="innerTab12" runat="server">
                    <asp:PlaceHolder ID="ControlContainerSetting" runat="server"></asp:PlaceHolder>
                </div>

                <div id="CrudCustomer" runat="server" class="siteDetail">
                    <div class="contentDetail contentLoadWrap" runat="server" id="cusomerDetail">
                        <asp:HiddenField ID="hidTabname" runat="server" />
                        <asp:HiddenField ID="hidMaster" runat="server" />
                        <asp:HiddenField ID="hidglobal" runat="server" />
                        <asp:HiddenField ID="hidIsIframe" runat="server" />
                        <asp:HiddenField ID="hidIsIframedo" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <footer>
            <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
        </footer>
    </form>
   
</body>
</html>
