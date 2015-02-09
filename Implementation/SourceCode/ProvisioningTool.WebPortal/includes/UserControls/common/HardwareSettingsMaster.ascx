<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HardwareSettingsMaster.ascx.cs" Inherits="includes_UserControls_common_Master" %>
<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>


<ProvisioningTool:Header ID="Header" runat="server" />
<ProvisioningTool:Footer ID="Footer" runat="server" />
<script type="text/javascript">

    $(document).ready(function () {
        $("#aHardware").unbind("click");
    });


    function HardwareEvent(ucPath, msg, selectedId, hidglobal) {
        $('#hidTabname').val(msg);
        $('#hidMaster').val(ucPath);
        $('#hidglobal').val(hidglobal);
        $.cookie('lefttab', selectedId);
        window.location = "HardwareSettings.aspx?trans=1&ucPath=" + ucPath + "&tabName=" + msg + "&hidglobal=" + hidglobal;
        return false;
    }

</script>
<div id="htmlTable" style="display: block;"></div>
<div id="pdfContainer" style="display: block;"></div>
<div style="display: none;"><a id="exportCsv" href="#" style="color: #0026ff"><span></span></a></div>
<div id="gridMasterNav" runat="server">
    <div class="fullWidthGrid">
        <div class="sideBar">
            <ul id="nav">

                <li class="main-sub-menu">

                    <div class="leftMenuTitle">
                        <a href="#" id="aHardware">Hardware</a>
                        <a href="/App/Settings.aspx" id="hardwareBack" class="sidebarBackBtn">Back</a>
                    </div>


                    <ul class="sub-menu">
                        <li><a id="aworkStation" runat="server" onclick="HardwareEvent('Workstations', 'Workstations', 1, 0)"><span class="workStationIcon"></span>Workstations</a></li>
                    </ul>
                    <ul class="sub-menu">
                        <li><a id="alapTop" runat="server" onclick="HardwareEvent('Laptops','Laptops', 2, 0)"><span class="lapTopIcon"></span>Laptops</a></li>
                    </ul>
                    <ul class="sub-menu">
                        <li><a id="arouters" runat="server" onclick="HardwareEvent('Routers','Routers', 3, 0)"><span class="routersIcon"></span>Routers</a></li>
                    </ul>
                    <ul class="sub-menu">
                        <li><a id="afirewalls" runat="server" onclick="HardwareEvent('Firewalls','Firewalls', 4, 0)"><span class="firewallsIcon"></span>Firewalls</a></li>
                    </ul>
                    <ul class="sub-menu">
                        <li><a id="aqswitches" runat="server" onclick="HardwareEvent('Network Switches','Network Switches', 5, 0)"><span class="switchesIcon"></span>Network Switches</a></li>
                    </ul>
                    <ul class="sub-menu">

                        <li><a id="aqprinters" runat="server" onclick="HardwareEvent('Printers','Printers', 6, 0)"><span class="printersIcon"></span>Printers</a></li>
                    </ul>
                    <ul class="sub-menu">

                        <li><a id="aqservers" runat="server" onclick="HardwareEvent('Servers','Servers', 7, 0)"><span class="serversIcon"></span>Servers</a></li>
                    </ul>
                    <ul class="sub-menu">

                        <li><a id="amobileDev" runat="server" onclick="HardwareEvent('Mobile Devices','Mobile Devices', 8, 0)"><span class="mobileDevIcon"></span>Mobile Devices</a></li>
                    </ul>
                    <ul class="sub-menu">

                        <li><a id="aqphoneSys" runat="server" onclick="HardwareEvent('Phone System','Phone System', 9, 0)"><span class="phoneSysIcon"></span>Phone System</a></li>
                    </ul>
                    <ul class="sub-menu">

                        <li><a id="aqnetworkShares" runat="server" onclick="HardwareEvent('Network Shares','Network Shares', 10, 0)"><span class="networkSharesIcon"></span>Network Shares</a></li>
                    </ul>
                    <ul class="sub-menu">

                        <li><a id="awireless" runat="server" onclick="HardwareEvent('Wireless','Wireless', 11, 0)"><span class="wirelessIcon"></span>Wireless</a></li>
                    </ul>
                </li>

            </ul>
        </div>
    </div>
</div>
