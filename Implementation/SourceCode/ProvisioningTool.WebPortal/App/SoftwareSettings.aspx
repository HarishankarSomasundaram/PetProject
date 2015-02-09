<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SoftwareSettings.aspx.cs" EnableViewState="false" Inherits="Settings " %>

<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/SoftwareSettingsMaster.ascx" TagName="SoftwareSettingsMaster" TagPrefix="ProvisioningTool" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" class="iframeBodyClass">
    <title>Settings</title>
    <ProvisioningTool:Includes ID="Includes" runat="server" />
    <ProvisioningTool:SoftwareSettingsMaster ID="SoftwareSettingsMaster" runat="server" />

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

            $(".sub-menu").show();

            return false;
        });

    </script>

</head>
<body id="PageBody" runat="server" class="adminContent">
    <form id="Custmain" runat="server">
        <div class="contentWrapText">
            <div class="divMessage" id="divMessage" style="padding-top: 25px; padding-left: 10px; text-align: center; width: 100%; display: none;" runat="server">
                <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
            </div>
            <div id="innerTab12" runat="server">
                <asp:PlaceHolder ID="ControlContainerSetting" runat="server"></asp:PlaceHolder>
            </div>

            <div id="CrudCustomer" runat="server" class="siteDetail">
                <div class="contentDetail contentLoadWrap" runat="server" id="cusomerDetail" style="padding-top: 25px; margin-left: 10px">
                    <asp:Button ID="btnCustomerSubmit" runat="server" Text="" Style="display: none" OnClick="btnCustomerSubmit_Click"></asp:Button>
                    <asp:HiddenField ID="hidTabname" runat="server" />
                    <asp:HiddenField ID="hidMaster" runat="server" />
                    <asp:HiddenField ID="hidglobal" runat="server" />
                    <asp:HiddenField ID="hidIsIframe" runat="server" />
                    <asp:HiddenField ID="hidIsIframedo" runat="server" />
                </div>
            </div>
        </div>

        <div class="clear"></div>
        <footer>
            <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
        </footer>
    </form>

    <script>
        $(document).ready(function () {
            var docHeight = $(window).height();
            var bodyHeight = $('body').height();
            if (docHeight < bodyHeight) {
                $('footer').css('position', 'relative');
            }
            else {
                $('footer').css('position', 'fixed');
            }

            if ($.cookie('SettingsTab') != null) {
                $("#SettingsMaster_Header_aheaderSettings").css("color", "#fff")
            }

            $('#SettingsMaster_Header_aheaderSettings').live('click', function () {
                $.cookie('SettingsTab', 1);
            });

        });
    </script>
</body>
</html>
