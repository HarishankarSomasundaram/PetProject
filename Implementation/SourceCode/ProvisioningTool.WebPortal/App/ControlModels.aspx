<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ControlModels.aspx.cs" EnableViewState="false" Inherits="Settings " %>

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
            var tabName = getQueryStringByName('menu');
            
            if (tabName != "")
            {
                $("#" + tabName).css("display", "block");
            }
        });

        function HardwareEvent(ucPath, msg, selectedId, hidglobal, types, models) {
            $('#hidTabname').val(msg);
            $('#hidMaster').val(ucPath);
            $('#hidglobal').val(hidglobal);
            $.cookie('lefttab', selectedId);
            var menu = getQueryStringByName('menu');
            window.location = "ControlModels.aspx?trans=1&ucPath=" + ucPath + "&tabName=" + msg + "&hidglobal=" + hidglobal + "&menu=" + menu + "&types=" + types + "&models="+models;

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
            <div>Please, select row</div>
        </div>
        <div id="dialogr" title="Warning" class="dialogr">
                    <div id="relatedRecordDiv"></div>
                </div>
        <div class="fullWidthGrid">
            <div class="leftMenuWrapper">
                <ul id="Workstations" class="hideDefault">
                    <li>
                        <div class="largeNav" id="aCity" runat="server" onclick="HardwareEvent('Global','Workstation-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="aStates" runat="server" onclick="HardwareEvent('Global','Workstation-Types', 1, 1, 'Workstation-Manufacturers')">
                            <img src="../../includes/UI/images/SettingsHardware/types.png" />
                            <h3>Types</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="aConutry" runat="server" onclick="HardwareEvent('Global','Workstation-Models', 1, 1, 'Workstation-Manufacturers', 'Workstation-Types')">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                </ul>
                <ul id="Router" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div1" runat="server" onclick="HardwareEvent('Global','Router-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div4" runat="server" onclick="HardwareEvent('Global','Router-Models', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardwareRouter/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div2" runat="server" onclick="HardwareEvent('Global','Router-Modules', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/modules.png" />
                            <h3>Modules</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div3" runat="server" onclick="HardwareEvent('Global','Router-OS-Versions', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/os-version.png" />
                            <h3>OS Versions</h3>
                        </div>
                    </li>
                </ul>                
                <ul id="Firewall" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div5" runat="server" onclick="HardwareEvent('Global','Firewall-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div6" runat="server" onclick="HardwareEvent('Global','Firewall-Models', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div7" runat="server" onclick="HardwareEvent('Global','Firewall-Modules', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/modules.png" />
                            <h3>Modules</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div8" runat="server" onclick="HardwareEvent('Global','Firewall-OS-Versions', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/os-version.png" />
                            <h3>OS Versions</h3>
                        </div>
                    </li>
                </ul>
                <ul id="PhoneSystem" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div9" runat="server" onclick="HardwareEvent('Global','Phone-System-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div10" runat="server" onclick="HardwareEvent('Global','Phone-System-Models', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div11" runat="server" onclick="HardwareEvent('Global','Phone-System-Modules', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/modules.png" />
                            <h3>Modules</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div12" runat="server" onclick="HardwareEvent('Global','Phone-System-OS-Versions', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/os-version.png" />
                            <h3>OS Versions</h3>
                        </div>
                    </li>
                </ul>
                <ul id="Phone" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div13" runat="server" onclick="HardwareEvent('Global','Phone-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div14" runat="server" onclick="HardwareEvent('Global','Phone-Models', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div15" runat="server" onclick="HardwareEvent('Global','Phone-Modules', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/modules.png" />
                            <h3>Modules</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div16" runat="server" onclick="HardwareEvent('Global','Phone-OS-versions', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/os-version.png" />
                            <h3>OS Versions</h3>
                        </div>
                    </li>
                </ul>
                <ul id="MobileDevice" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div17" runat="server" onclick="HardwareEvent('Global','Mobile-Device-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div19" runat="server" onclick="HardwareEvent('Global','Mobile-Device-Types', 1, 1, 'Mobile-Device-Manufacturers')">
                            <img src="../../includes/UI/images/SettingsHardware/types.png" />
                            <h3>Types</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div18" runat="server" onclick="HardwareEvent('Global','Mobile-Device-Models', 1, 1, 'Mobile-Device-Manufacturers', 'Mobile-Device-Types')">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                </ul>
                <ul id="Servers" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div21" runat="server" onclick="HardwareEvent('Global','Server-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div22" runat="server" onclick="HardwareEvent('Global','Server-Types', 1, 1, 'Server-Manufacturers')">
                            <img src="../../includes/UI/images/SettingsHardware/types.png" />
                            <h3>Types</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div23" runat="server" onclick="HardwareEvent('Global','Server-Models', 1, 1, 'Server-Manufacturers', 'Server-Types')">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                </ul>
                <ul id="Laptops" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div25" runat="server" onclick="HardwareEvent('Global','Laptop-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div26" runat="server" onclick="HardwareEvent('Global','Laptop-Types', 1, 1, 'Laptop-Manufacturers')">
                            <img src="../../includes/UI/images/SettingsHardware/types.png" />
                            <h3>Types</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div27" runat="server" onclick="HardwareEvent('Global','Laptop-Models', 1, 1, 'Laptop-Manufacturers', 'Laptop-Types')">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                </ul>
                <ul id="Printer" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div29" runat="server" onclick="HardwareEvent('Global','Printer-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div30" runat="server" onclick="HardwareEvent('Global','Printer-Models', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div31" runat="server" onclick="HardwareEvent('Global','Printer-Modules', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/modules.png" />
                            <h3>Modules</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div32" runat="server" onclick="HardwareEvent('Global','Printer-OS-versions', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/os-version.png" />
                            <h3>OS Versions</h3>
                        </div>
                    </li>
                </ul>
                <ul id="Wireless" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div33" runat="server" onclick="HardwareEvent('Global','Wireless-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div35" runat="server" onclick="HardwareEvent('Global','Wireless-Types', 1, 1, 'Wireless-Manufacturers')">
                            <img src="../../includes/UI/images/SettingsHardware/types.png" />
                            <h3>Types</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div34" runat="server" onclick="HardwareEvent('Global','Wireless-Models', 1, 1, 'Wireless-Manufacturers', 'Wireless-Types')">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                </ul>
                <ul id="NetworkSwitch" class="hideDefault">
                    <li>
                        <div class="largeNav" id="Div37" runat="server" onclick="HardwareEvent('Global','Network-Switch-Manufacturers', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/manufacturers.png" />
                            <h3>Manufacturers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div38" runat="server" onclick="HardwareEvent('Global','Network-Switch-Models', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/models.png" />
                            <h3>Models</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div39" runat="server" onclick="HardwareEvent('Global','Network-Switch-Modules', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/modules.png" />
                            <h3>Modules</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div40" runat="server" onclick="HardwareEvent('Global','Network-Switch-OS-versions', 1, 1)">
                            <img src="../../includes/UI/images/SettingsHardware/os-version.png" />
                            <h3>OS Versions</h3>
                        </div>
                    </li>
                </ul>

                <ul id="Applications" class="hideDefault applications-leftMenu">
                    <li>
                        <div class="largeNav" id="Div41" runat="server" onclick="HardwareEvent('Global','Email', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftwareApplications/email.png" />
                            <h3>Email</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div42" runat="server" onclick="HardwareEvent('Global','Accounting', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftwareApplications/accounting.png" />
                            <h3>Accounting</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div43" runat="server" onclick="HardwareEvent('Global','Productivity', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftwareApplications/productivity.png" />
                            <h3>Productivity</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div44" runat="server" onclick="HardwareEvent('Global','Cloud-Apps', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftwareApplications/cloud.png" />
                            <h3>Cloud Apps</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div45" runat="server" onclick="HardwareEvent('Global','CRM-ERP', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftwareApplications/erp.png" />
                            <h3>CRM/ERP</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="Div46" runat="server" onclick="HardwareEvent('Global','Misc', 1, 1)">
                            <img src="../../includes/UI/images/SettingsSoftwareApplications/misc.png" />
                            <h3>Misc</h3>
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
