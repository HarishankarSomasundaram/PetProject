<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HardDisk.ascx.cs" Inherits="UserControlsHardDisk" %>

<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<script type="text/javascript">
    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) {
        siteID = 0;
        $('#headerCustomer').hide();
    }

    var isColorBox = "no";
    if (getQueryStringByName("isColorBox") == "yes")
        isColorBox = getQueryStringByName("isColorBox");

    var gridWidth = "";
    var gridName = "#grdHardDisk";
    var gridPager = "#grdHardDiskpager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "250";
    var gridSortOrder = "asc";
    var gridpageSize = "10";;
    var gridListName = "HardDiskList";
    var pageSizeOption = ["10", "20", "30"];

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time
        var colname = ["HardDiskID", "Hard Drive Name", "Size", "Size Unit"];

        var colmodel = [
                           { name: 'SystemHardDiskID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                           { name: 'HardDiskName', sortable: true, align: "left", hidden: false, editable: false, search: true },
                           { name: 'Size', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'SizeUnit', sortable: true, align: "left", hidden: false, editable: false, search: true }
        ];
        //Default SortColumn
        var sortName = "HardDiskID";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLHARDDISK/" + caption + "/" + siteID + "/SearchText";
        var AddUrl, EditUrl;

        if (isColorBox == "yes") {
            AddUrl = "SystemLocaleSettings.aspx?navPage=hard drive&do=a&isColorBox=" + isColorBox; // Add URL
            EditUrl = "SystemLocaleSettings.aspx?navPage=hard drive&do=e&isColorBox=" + isColorBox + "&id="; //Edit URL
        }
        else {
            AddUrl = "SystemLocaleSettings.aspx?navPage=hard drive&do=a"; // Add URL
            EditUrl = "SystemLocaleSettings.aspx?navPage=hard drive&do=e&id="; //Edit URL
        }

        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEHARDDISK";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEHARDDISK";
        //Calling the webservices and the desgining the Grid at Runtime 
        var objGridList = new oData(
                                        gridName, // Table or Grid name in the page
                                        getWebServiceURL,//Web Service URL
                                        "json", // Default dont change
                                        "GET", // Webservice Mode
                                        gridCaption, //Grid Caption
                                        gridpageSize, //  Number of records in each page
                                        pageSizeOption, //Page Size Option 10 20 30[this will be available in the Grid Dropdown]
                                        sortName, //Default Sortname
                                        gridSortOrder, //Sort Type - desc or asc
                                        gridWidth, // Grid width
                                        gridHeight, // Grid height
                                        crudWebServiceURL, // Add
                                        gridPager, //div name in the page (gridpager1)
                                        false, // is Edit Button visiable
                                        false, // is Add Button visiable
                                        true, // is Delete Button visiable
                                        false, // is Search Button visiable
                                        false, // is Refresh Button visiable
                                        false, // is Search TextBox Visiable/Enabled
                                        false, //
                                        colname, // Grid Column names
                                        colmodel, // Grid Column names and properties
                                        false, // Select All options on the header and this will give checkbox for each row
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        0, //
                                        gridListName, //Result Set name which is availabe in the Service
                                        true, //is New page required for Add
                                        AddUrl, // Add URL
                                        true, //is New page required for Edit
                                        EditUrl, //Edit URL
                                        deleteWebServiceURL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        //VALIDATION CONTROL jquery.form-validator
        $.validate({
            form: '#frmHD',
            modules: 'sweden,security',
            language: myLanguage
        });


        $('#txtDriveDetail_tag').keypress(function (e) {
            e.preventDefault();
        });

        $('#txtDriveDetail_tag').live("cut copy paste", function (e) {
            e.preventDefault();
        });

        $("#btnAddHD").live("click", function () {
            //Declartion
            var SU = $("[id*='ddlHDSizeUnit'] :selected").text();

            var Size = $("#txtHDSize").val();
            var Drive = $("#txtDrive").val().toUpperCase();
            var TotlSU = $("[id*='ddlSizeUnit'] :selected").text();
            var TotSize = parseFloat($("#txtSize").val(), 10);
            var DriveName = Drive + " Drive";
            var txt = $("#txtDriveDetail").val();
            var CalSize = CurrentSize = 0;

            //console.log(Drive);

            //Check for Required Field
            if (Drive == "") {
                alert('Please enter Drive Character');
                return;
            }
            if (Size == "") {
                alert('Please enter Drive Size');
                return;
            }
            if (SU == "Select") {
                alert('Please enter Drive Size Unit');
                return;
            }

            //Calcuate Hard Drive Size
            if (TotlSU == 'GB') {
                CalSize = TotSize;
            }
            else if (TotlSU == 'TB') {
                CalSize = TotSize * 1000;
            }

            //Calcuate the Text Drive Size
            if (txt != "") {
                var des = $("#txtDriveDetail").val().split(';');

                for (var i = 0; i < des.length; i++) {
                    var des1 = des[i].split(':');
                    var unit = des1[1].substring(des1[1].length - 2, des1[1].length);
                    var sSize = des1[1].substring(0, des1[1].length - 3);
                    if (unit == 'GB')
                        CurrentSize = parseFloat(CurrentSize) + parseFloat(sSize);
                    else if (unit == 'TB')
                        CurrentSize = parseFloat(CurrentSize) + parseFloat(sSize) * 1000;
                }
                if (SU == 'GB') {
                    CurrentSize = parseFloat(CurrentSize) + parseFloat(Size);
                }
                else if (SU == 'TB') {
                    CurrentSize = parseFloat(CurrentSize) + parseFloat(Size) * 1000;
                }
            }
            else {
                if (SU == 'GB') {
                    CurrentSize = Size;
                }
                else if (SU == 'TB') {
                    CurrentSize = parseFloat(Size) * 1000;
                }

            }


            //Checking for Drive Name in Disk text box
            if (txt.indexOf(DriveName) > -1) {
                alert('Drive name already exists');
            }
            else {
                if (Size != '') {
                    if (CurrentSize <= CalSize) {
                        $("#txtDriveDetail").addTag(Drive + " Drive : " + Size + " " + SU);
                    }
                    else {
                        alert('Hard Drive does not have space');
                    }
                }
                else {
                    alert('Please enter Hard Drive details');
                }
            }

            $('#txtDrive').focus();

            //$("#ddlHDSizeUnit").attr('selectedIndex', 0);
            $("#ddlHDSizeUnit").select2("val", 0); //set the value
            $("#txtHDSize").val("");
            $("#txtDrive").val("");

            return false;
        });

        var caption = "Hard Drive";
        $('#grdHardDisk').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid(caption));
        $('.ui-jqgrid-title').text(caption);

        if ($.cookie("isIframe") != null) {
            $('#Master_gridMasterNav').hide();
            $('.contentWrapText').css({ 'background': '#fff!important' });
            $('.form1').css('background', '#fff');
        }

        $("#del_grdHardDisk").insertAfter(".ui-pg-button:nth(3)");

        return false;

    });

    function ButtonEvent(caption, gridColumnName, gridTitle) {
        return true;
    }
</script>


<div class="divMessage" style="padding-top: 25px; padding-left: 10px; text-align: center;">
    <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
</div>
<div id="grdHardDisks" runat="server" class="innerGrdFullWidth">

    <div style="padding: 15px 0px; margin-left: 10px; margin-right: 10px; text-align: center;">
        <table id="grdHardDisk"></table>
        <div id="grdHardDiskpager"></div>
    </div>
</div>

<div id="CrudHardDisk" runat="server" class="siteDetail">
    <div class="innerTabContent">
        <div class="contentDetail" style="padding-top: 25px; margin-left: 10px">
            <h1 id="lblHeader">Hard Drive</h1>
            <div class="inlineProperty">
                <label>
                    Hard Drive Name
                            <asp:RequiredFieldValidator ID="rfvHardDiskName" runat="server"
                                ControlToValidate="txtHardDiskName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                <asp:TextBox Text="" ID="txtHardDiskName" class="watermark" placeholder="Hard Drive Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="250" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <label>
                    Size
                            <asp:RequiredFieldValidator ID="rfvSize" runat="server"
                                ControlToValidate="txtSize" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                <asp:TextBox Text="" ID="txtSize" class="watermark IntegerValidation" placeholder="Size" ClientIDMode="Static"
                    runat="server" MaxLength="4"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <label>
                    Size Unit
                            <asp:RequiredFieldValidator ID="rfvSU" runat="server"
                                ControlToValidate="ddlSizeUnit" Display="Dynamic" ErrorMessage="*" InitialValue="Select"
                                ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                <asp:DropDownList runat="server" ID="ddlSizeUnit" CssClass="selector" ClientIDMode="Static">
                    <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                    <asp:ListItem Value="GB" Text="GB"></asp:ListItem>
                    <asp:ListItem Value="TB" Text="TB"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="clear"></div>
            <div class="inlineProperty">
                <label>
                    Drive Character
                            <asp:RequiredFieldValidator ID="rfvDriveCharacter" runat="server"
                                ControlToValidate="txtDrive" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                <asp:TextBox Text="" ID="txtDrive" class="watermark" placeholder="Drive Character" ClientIDMode="Static" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="1"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <label>
                    Size
                            <asp:RequiredFieldValidator ID="rfvHDSize" runat="server"
                                ControlToValidate="txtHDSize" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                <asp:TextBox Text="" ID="txtHDSize" class="watermark IntegerValidation" ClientIDMode="Static" placeholder="Size"
                    runat="server" MaxLength="4"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <label>
                    Size Unit
                            <asp:RequiredFieldValidator ID="rfvHDSizeUnit" runat="server"
                                ControlToValidate="ddlHDSizeUnit" Display="Dynamic" ErrorMessage="*" InitialValue="Select"
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                <asp:DropDownList runat="server" ID="ddlHDSizeUnit" CssClass="selector" class="watermark" placeholder="Size Unit" ClientIDMode="Static">
                    <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                    <asp:ListItem Value="GB" Text="GB"></asp:ListItem>
                    <asp:ListItem Value="TB" Text="TB"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnAddHD" CssClass="actionBtn" runat="server" Text="Add" Style="margin-top: 25px" ClientIDMode="Static" ValidationGroup="Req" />
            <div class="clear"></div>
            <div class="inlineProperty">
                <label>
                    Drive Details
                        <asp:RequiredFieldValidator ID="rfvDriveDetails" runat="server"
                            ControlToValidate="txtDriveDetail" Display="Dynamic" ErrorMessage="*" InitialValue=""
                            ValidationGroup="SReq">*</asp:RequiredFieldValidator>
                </label>
                <div class="keywords">
                    <span class="field">
                        <asp:TextBox Text="" ID="txtDriveDetail" class="watermark multiText" placeholder="" ClientIDMode="Static" MaxLength="2000"
                            runat="server"></asp:TextBox>
                    </span>
                </div>
            </div>
            <div class="clear"></div>
            <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="SReq" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" CssClass="actionBtn" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
</div>
