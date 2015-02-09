<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SoftwareSettingsMaster.ascx.cs" Inherits="includes_UserControls_common_Master" %>
<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>


<ProvisioningTool:Header ID="Header" runat="server" />
<ProvisioningTool:Footer ID="Footer" runat="server" />
<script type="text/javascript">

    

    $(document).ready(function () {
        $("#aGlobalMaster").unbind("click");
    });

    function HardwareEvent(ucPath, msg, selectedId, hidglobal) {
        $('#hidTabname').val(msg);
        $('#hidMaster').val(ucPath);
        $('#hidglobal').val(hidglobal);
        $.cookie('lefttab', selectedId);
        window.location = "SoftwareSettings.aspx?trans=1&ucPath=" + ucPath + "&tabName=" + msg + "&hidglobal=" + hidglobal;
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
                        <a href="#" id="aGlobalMaster">Software</a>
                        <a href="/App/Settings.aspx" id="hardwareBack" class="sidebarBackBtn">Back</a>
                    </div>

                    <ul class="sub-menu">
                        <li><a id="asite2site" runat="server" onclick="HardwareEvent('Global','Server Roles', 1, 1)"><span class="site2siteIcon"></span>Server Roles</a></li>
                        <li><a id="asoftware" runat="server" onclick="HardwareEvent('Global','Operating Systems', 1, 1)"><span class="softwareIcon"></span>Operating Systems</a></li>
                        <li><a id="ainternet" runat="server" onclick="HardwareEvent('Global','Antivirus', 1, 1)"><span class="internetIcon"></span>AntiVirus</a></li>
                        <li><a id="agroupPolicies" runat="server" onclick="HardwareEvent('Global','Backup Softwares', 1, 1)"><span class="groupPoliciesIcon"></span>Backup Softwares</a></li>
                        <li><a id="asystemAudit" runat="server" onclick="HardwareEvent('Global','Application Softwares', 1, 1)"><span class="systemAuditIcon"></span>Application Softwares</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
