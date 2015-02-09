<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" EnableViewState="false" Inherits="Settings " %>

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
            $(document).keyup(function (e) {
                if (e.keyCode == 27) {
                    $('body').removeClass('scrollHide');
                }
            });

            return false;
        });
    </script>

</head>
<body id="PageBody" runat="server" class="adminContent">
    <form id="Custmain" runat="server">
        <div class="fullWidthGrid">
            <div class="leftMenuWrapper settings-leftMenu">
                <ul>
                    <li>
                        <div class="largeNav" id="iSysLocale">
                            <img src="../../includes/UI/images/Settings/system-locate.png" />
                            <h3>System-Locale</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iSysUser">
                            <img src="../../includes/UI/images/Settings/system-user.png" />
                            <h3>System-User</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iHardware">
                            <img src="../../includes/UI/images/hardwareIcon.png" />
                            <h3>Hardware</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iSoftware">
                            <img src="../../includes/UI/images/softwareIconLarge.png" />
                            <h3>Software</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iServiceProvider">
                            <img src="../../includes/UI/images/Settings/service-provider.png" />
                            <h3>Service Providers</h3>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
        <footer>
            <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
        </footer>
    </form>

</body>
</html>
