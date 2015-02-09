<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerMaster.ascx.cs" Inherits="includes_UserControls_common_Master" %>
<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>
<ProvisioningTool:Includes ID="Includes" runat="server" />
<ProvisioningTool:Header ID="Header" runat="server" />
<ProvisioningTool:Footer ID="Footer" runat="server" />
<script type="text/javascript">
    var isReloaded = false;

    function ButtonEvent(caption, gridColumnName, gridTitle, isSiteBased) {
        $('#customer').hide();
        $('#innerTab').show();
        $('#grdGlobalMasters').jqGrid('GridUnload');
        $("#lblHeader").text(caption);
        $('#hidTabname').val(caption);
        if (isSiteBased == undefined) {
            isSiteBased = 0;
        }

        jqGridGenerator(InitializeGrids(caption, gridColumnName, isSiteBased));
        $('.ui-icon-calculator').hide();

        if (gridTitle == undefined) {
            $('.ui-jqgrid-title').text(caption);
        }
        else {
            $('.ui-jqgrid-title').text(gridTitle);
        }

        isReloaded = false;
        return false;
    }

    $(document).ready(function () {

        $("input[type=text], input[type=email], input[type=number], input[type=tel], textarea").hover(function () {
            var textValue = $(this).val();
            $(this).attr('title', textValue);
        });

        $(".chosen-container-multi, .select2-container").hover(function () {
            var textValue = $(this).text();
            $(this).attr('title', textValue);
        });

        $("input[type=tel]").mask('(000) 000-0000');

    });

</script>
<div id="htmlTable" style="display: block;"></div>
<div id="pdfContainer" style="display: block;"></div>
<div style="display: none;"><a id="exportCsv" href="#" style="color: #0026ff"><span></span></a></div>
