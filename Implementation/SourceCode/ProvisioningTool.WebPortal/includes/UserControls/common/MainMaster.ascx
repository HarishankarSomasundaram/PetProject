<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainMaster.ascx.cs" Inherits="includes_UserControls_common_MainMaster" %>
<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>

<ProvisioningTool:Header ID="Header" runat="server" />
<ProvisioningTool:Footer ID="Footer" runat="server" />
<script type="text/javascript">

    function HardwareEvent(ucPath, msg, selectedId, hidglobal) {
        $('#hidTabname').val(msg);
        $('#hidMaster').val(ucPath);
        $('#hidglobal').val(hidglobal);
        $.cookie('lefttab', selectedId);
        //$("[id*='btnCustomerSubmit']").click();
        window.location = "Settings.aspx?trans=1&ucPath=" + ucPath + "&tabName=" + msg + "&hidglobal=" + hidglobal;
        return false;
    }

</script>
<div id="htmlTable" style="display: block;"></div>
<div id="pdfContainer" style="display: block;"></div>
<div style="display: none;"><a id="exportCsv" href="#" style="color: #0026ff"><span></span></a></div>
<div id="gridMasterNav" runat="server">
    <div class="fullWidthGrid contentWrapText noMrginLeft">
        <div class="leftMenuWrapper">
            <ul>
                <li>
                    <div class="largeNav" id="iCustomer">
                        <img src="../../includes/UI/images/customerLarge.png" />
                        <h3>Customers</h3>
                    </div>
                </li>
                <li>
                    <div class="largeNav" id="iUsers">
                        <img src="../../includes/UI/images/userLargeIcon.png" />
                        <h3>Users</h3>
                    </div>
                </li>
                <li>
                    <div class="largeNav" id="iSettings">
                        <img src="../../includes/UI/images/settingslargeIcon.png" />
                        <h3>Settings</h3>
                    </div>
                </li> 
                <li>
                    <div class="largeNav" id="iCustomerSites">
                        <img src="../../includes/UI/images/customerSites.png" />
                        <h3>Customer Sites</h3>
                    </div>
                </li>
            </ul>

        </div>
    </div>
</div>
