<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerSites.aspx.cs" Inherits="MastersSiteInfo" %>

<%@ Register Src="~/includes/UserControls/common/Master.ascx" TagName="Master" TagPrefix="ProvisioningTool" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Sites</title>
    <ProvisioningTool:Master ID="Master" runat="server" />

    <script type="text/javascript">
        var siteID = $.cookie("siteID");
        if (siteID == "" || siteID == null) {
            siteID = 0;
            $('#headerCustomer').hide();
        }
        var customerID = 0;
        var siteDelete = "no";
        var delsiteID = 0;

        var isColorBox = getQueryStringByName('isColorBox');

        if (isColorBox == "yes") {
            siteDelete = getQueryStringByName('siteDelete');
            delsiteID = getQueryStringByName('delsiteID');
            customerID = $.cookie("sessionCustomerId");
        }
        else {
            customerID = getQueryStringByName('CID');
            if ($.cookie("CID") == "" || $.cookie("CID") == null) {
                $.cookie("CID", customerID);
                customerID = $.cookie("CID");
            }
            else {
                customerID = $.cookie("CID");
            }
        }

        var gridWidth = "";
        var gridName = "#grdSiteMasters";
        var gridName2 = "#grdAutoTaskCustomerMasters";
        var gridCustomerName = "#grdAutoTaskCustomerMasters";

        var gridPager = "#grdSitepager";
        var gridPager2 = "#grdAutoTaskCustomerpager";
        var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
        var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
        var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
        var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
        var gridHeight = "250";
        var gridSortOrder = "asc";
        var gridpageSize = "10";;
        var gridListName = "SiteList";
        var pageSizeOption = ["10", "20", "30"];
        var paperSize = "a2";
        var paperOrientation = "l"; // p - portriat : l - landscape


        function InitializeGrid(caption, webUrl) {

            //To define the Grid for the page on the design time
            //var colname = ["Site ID", "Customer ID", "Site Name", "Site Code", "Address 1", "Address 2", "City ID", "City Name", "State ID", "State Name", "Country ID", "Country Name", "Zip Code", "Phone", "Web Site", "Primary Contact ID", "Primary Contact Name", "Primary Contact Phone1", "Primary Contact Title ID", "Primary Contact Email", "Account Rep ID", "Account Rep Name", "Primary Engineer ID", "Primary Engineer Name"];//, "Created On", "Created By", "Created By Name", "Modified On", "Modified By"];
            var colname = ["Site ID", "Customer ID", "Site", "Site Code", "Address 1", "Phone", "Customer", "Primary Contact ID", "Primary Contact Name", "Primary Contact Phone", "Primary Contact Title ID", "Account Rep ID", "Account Rep", "Primary Engineer ID", "Primary Engineer"];

            var colmodel = [
                               { name: 'SiteID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                               { name: 'Customer.CustomerID', sortable: true, align: "left", hidedlg: true, hidden: true, editable: true },
                               { name: 'SiteName', sortable: true, hidden: false, editable: true, search: false },
                               { name: 'SiteCode', sortable: true, hidden: false, editable: true, search: false },
                               { name: 'Address1', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'PhoneNumber', sortable: true, align: "center", hidden: false, editable: false, search: false },

                               { name: 'Customer.CustomerName', sortable: true, hidden: false, editable: true, search: false },

                               { name: 'PrimaryContactID', sortable: true, align: "center", hidedlg: true, hidden: true, editable: false, search: false },
                               { name: 'PrimaryContactName', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'PrimaryContactPhone', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'PrimaryContactTitleID', sortable: true, align: "center", hidedlg: true, hidden: true, editable: false, search: false },

                               { name: 'AccountRepID', sortable: true, align: "center", hidedlg: true, hidden: true, editable: false, search: false },
                               { name: 'AccountRepName', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'PrimaryEngineeID', sortable: true, align: "center", hidedlg: true, hidden: true, editable: false, search: false },
                               { name: 'PrimaryEngineerName', sortable: true, align: "center", hidden: false, editable: false, search: false }
            ];
            //Default SortColumn
            var sortName = "SiteName";
            var gridCaption = caption;
            var getWebServiceURL = "";


            if (webUrl != "")
                getWebServiceURL = baseServiceURL + masterServiceName + webUrl;
            else
                getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLSITES/" + caption + "/" + customerID + "/" + delsiteID;


            var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETESITEBYSITEID";
            var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETESITEBYSITEID";
            //Calling the webservices and the desgining the Grid at Runtime 

            var AddURL, EditURL, ViewURL, IsAddRequired;
            if (isColorBox == 'yes') {
                AddURL = "CustomerSites.aspx?do=a&isColorBox=yes";
                EditURL = "CustomerSites.aspx?do=e&isColorBox=yes&id="
                ViewURL = "CustomerSites.aspx?do=m&isColorBox=ye&id=";
                IsAddRequired = false;
            }
            else {
                AddURL = "CustomerSites.aspx?search=1&do=a";
                EditURL = "CustomerSites.aspx?search=1&do=e&id=";
                ViewURL = "CustomerSites.aspx?search=1&do=m&id=";
                IsAddRequired = true;
            }
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
                                            IsAddRequired, //is New page required for Add
                                            AddURL, // Add URL
                                            true, //is New page required for Edit
                                            EditURL, //Edit URL
                                            deleteWebServiceURL,
                                            paperSize,
                                            paperOrientation,
                                            true, //is New page required for View
                                            ViewURL //view URL
                                         );
            return objGridList;
        };

        function InitializeGrid2(caption, gridServiceURL) {

            //To define the Grid for the page on the design timevar 
            //colname = ["Action", "Site ID", "Site Code", "Site Name", "Address", "Phone Number"];
            colname = ["Action", "SiteCode", "Site Code", "Site Name", "Address", "Phone Number"];
            var colmodel = [

                               { name: 'View', width: 80, sortable: true, align: "center", hidedlg: false, hidden: false, editable: false, search: false },
                               //{ name: 'SiteID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                               { name: 'SiteCode', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                               { name: 'SiteCode', sortable: true, align: "center", hidden: false, editable: false, search: true },
                               { name: 'SiteName', sortable: true, align: "center", hidden: false, editable: false, search: true },
                               { name: 'Address1', sortable: true, align: "center", hidden: false, editable: false, search: true },
                               { name: 'PhoneNumber', sortable: true, align: "center", hidden: false, editable: false, search: true },
            ];
            //Default SortColumn
            var sortName = "CustomerCode";
            var gridCaption = caption;

            var deleteWebServiceURL;// = baseServiceURL + masterServiceName + postService + "DELETECUSTOMERBYCUSTOMERID";
            var crudWebServiceURL;// = baseServiceURL + masterServiceName + postService + "DELETECUSTOMERBYCUSTOMERID";
            //Calling the webservices and the desgining the Grid at Runtime 
            var objGridList = new oData(
                                            gridName2, // Table or Grid name in the page
                                            gridServiceURL,//Web Service URL
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
                                            gridPager2, //div name in the page (gridpager1)
                                            false, // is Edit Button visiable
                                            false, // is Add Button visiable
                                            false, // is Delete Button visiable
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
                                            false, //is New page required for Add
                                            "CustomerSites.aspx?do=a", // Add URL
                                            false, //is New page required for Edit
                                            "CustomerSites.aspx?do=e&id=", //Edit URL
                                            deleteWebServiceURL,
                                            paperSize,
                                            paperOrientation,
                                            false, //is New page required for view
                                            "" //view URL
                                         );
            return objGridList;
        };


        $(document).ready(function () {
            $(".custddl").change(function () {
                if ($(this).val() === '0') {
                    var id = '#s2id_' + this.id;
                    $(id).find('.select2-choice').css({ "background": "#ffebef" });
                    //$(id).find('.select2-choice').css({ "border-color": "red" });
                    //$(id).find('.select2-choice').css("background": "#ffebef !important");
                    $(id).find('.select2-choice').css('border', '1px solid red !important');
                    $(id).addClass("errorBox");
                }
                else {
                    var id = '#s2id_' + this.id;
                    $(id).find('.select2-choice').css({ "background": "#fff" });
                    $(id).removeClass("errorBox");
                }

            });

            $("#ddlCustomer").val(customerID);
            if (getQueryStringByName("do") != "m") {
                //VALIDATION CONTROL jquery.form-validator
                $.validate({
                    form: '#form1',
                    modules: 'sweden,security',
                    language: myLanguage
                });
            }

            //hari start
            $("#btnEdit").hide();

            if (getQueryStringByName("do") == "m")
                $("#btnEdit").show();

            $("#btnEdit").click(function () {

                var ID = getQueryStringByName("id");
                if (ID != null) {
                    var RedirectURL = 'CustomerSites.aspx?search=1&do=e&id=' + ID;
                    window.location = RedirectURL;
                }
                return false;
            });
            //hari end


            var caption = "Customer Site";

            if (getQueryStringByName("do") == "a")
                $("#lblHeader").html("Create Customer Sites");
            else if (getQueryStringByName("do") == "e")
                $("#lblHeader").html("Modify Customer Sites");

            $('#grdSiteMasters').jqGrid('GridUnload');

            if (getQueryStringByName("search") == 1) {
                //gridServiceURL = "GetCustomerBySearchKey/" + CustomerCode + "/all/all/all/" + caption;
                jqGridGenerator(InitializeGrid(caption, ""));
                //$('#customSearch').show();
                hideGridfooter();
            } else {
                $('#customSearch').hide();
            }

            if ($("#CrudSite").length > 0) { $("#customSearch").hide(); }

            if ($.cookie("isIframe") != null) {
                $('#Master_gridMasterNav').hide();
                $('.contentWrapText').css({ 'background': '#fff!important' });
                $('#form1').css('background', '#fff');
            }

            $('#loadingDiv').hide();

            $("#btnSearch").click(function () {
                var caption2 = "Customers - AutoTask";
                var CustomerName = $("#txtCustName").val();
                if (CustomerName == "") { CustomerName = "all"; }
                var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLCUSTOMERSAUTOTASK/" + CustomerName + "/" + siteID + "/AccountName";
                debugger;
                jqGridGenerator(InitializeGrid2(caption2, getWebServiceURL));
                $('.ui-icon-excel').hide();
                $('.ui-icon-pdf').hide();
                return false;
            });

            $("#del_grdSiteMasters").insertAfter(".ui-pg-button:nth(6)");
            //$(".ui-icon-trash").before($(".ui-icon-pencil"));

            if (siteDelete == "yes") {
                $('.ui-icon-excel').hide();
                $('.ui-icon-pdf').hide();
                $('.ui-icon-pencil').hide();
            }

            $(".inline").colorbox({
                rel: 'inline',
                inline: true,
                href: $(this).attr('href'),
                height: '85%',
                transition: 'none'
            });

            $("#txtCustName").bind('keypress', function (e) {
                if (e.which === 13) {
                    $('#btnSearch').trigger('click');
                }
            });

            $("#lnkAutoTask").click(function () {
                $("#txtCustName").val("");
                $("#grdAutoTaskCustomer").css("display", "none");
            });

            $("#txtCustName").keyup(function () {
                if (!this.value) {
                    $("#grdAutoTaskCustomer").css("display", "none");
                }
            });

            //Search button 
            $("#btnSearchSubmit").click(function () {

                $('#grdSiteMasters').jqGrid('GridUnload');

                var txtSearch = $("#txtSearch").val();

                //Check if the value is empty if so all the data should be fetched
                var gridSearchURL = "";

                if (txtSearch == "") {
                    gridSearchURL = "SEARCHSITEBYKEY/" + caption + "/all";
                }
                else {
                    gridSearchURL = "SEARCHSITEBYKEY/" + caption + "/" + txtSearch;
                }

                jqGridGenerator(InitializeGrid(caption, gridSearchURL));

                hideGridfooter();
                return false;
            });



            $("#btnSearchClear").click(function () {
                $("#txtSearch").val('');
                $("#btnSearchSubmit").click();
                return false;
            });


            hideGridfooter();
            return false;
        });


        function DisplayDialog() {
            $(function () {
                $("#dialog-confirmSubmit").show();
                $("#dialog-confirmSubmit").dialog({
                    modal: true,
                    buttons: {
                        Ok: function () {
                            $("[id*=btnSubmit]").click();
                        },
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });
                $(".ui-dialog-title").html('Information !');
                return true;
            });
        };


        function ShowPopup(message) {
            $(function () {
                $("#hidIsAutoTaskCheckRequried").val('False');

                $("#dialog-confirm").html(message);
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 200,
                    modal: true,
                    buttons: {
                        'Yes': function () {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSubmit))%>;
                        },
                        'No': function () {
                            $("#hidIsAutoTaskCheckRequried").val('True');
                            $(this).dialog('close');
                        }
                    }
                });

            });
        };

    </script>
</head>
<body id="PageBody" runat="server" class="adminContent">
    <form id="form1" runat="server">
        <div class="divMessage" id="divMessage" runat="server">
            <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        </div>
        <div id="dialog-confirmSubmit" title="Warning">
            <div>Do you want to Submit</div>
        </div>
        <div class="fullWidthGrid">
            <div class="leftMenuWrapper">
                <ul>
                    <li>
                        <div class="largeNav" id="iSearchCustomerSites">
                            <img src="../../includes/UI/images/searchCustomerSites.png" />
                            <h3>Search</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iCreateCustomerSites">
                            <img src="../../includes/UI/images/createCustomerSites.png" />
                            <h3>Create</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iModifyCustomerSites">
                            <img src="../../includes/UI/images/editCustomerSites.png" />
                            <h3>Modify</h3>
                        </div>
                    </li>
                </ul>
            </div>

            <div class="rightContent">
                <div id="CrudSite" runat="server" class="siteDetail clearedContent">
                    <div class="innerTabContent" runat="server" id="Sitediv">
                        <div class="contentDetail">
                            <h1 id="lblHeader">Customer Sites  </h1>
                            <asp:Button ID="btnEdit" CssClass="actionEditBtn" runat="server" Text="Edit" />
                            <div class="autotask">
                                <%-- <asp:Image runat="server" ID="imgAutoTask" ImageUrl="~/includes/css/images/autotask-logo.gif" Width="120px" Height="70px" CssClass="AutoTask" />--%>
                                <asp:LinkButton runat="server" ID="lnkAutoTask" href="#divAutoTask" CssClass="forgotPass inline" ClientIDMode="Static"></asp:LinkButton>
                            </div>
                            <div class="connectWise">
                                <%-- <asp:Image runat="server" ID="imgAutoTask" ImageUrl="~/includes/css/images/autotask-logo.gif" Width="120px" Height="70px" CssClass="AutoTask" />--%>
                                <asp:LinkButton runat="server" ID="lnkConnectWise" href="#divConnectWise" CssClass="forgotPass inline"></asp:LinkButton>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Customer Site Name
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSiteName" runat="server"
                                ControlToValidate="txtSiteName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:TextBox Text="" ID="txtSiteName" class="watermark custclass" placeholder="Site Name" data-validation="alphanumeric" data-validation-allowing="-+()_," data-validation-optional="true"
                                    runat="server" MaxLength="250"></asp:TextBox>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Customer Site Code
                            <asp:RequiredFieldValidator ID="rfvtxtSiteCode" runat="server"
                                ControlToValidate="txtSiteCode" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:TextBox Text="" ID="txtSiteCode" class="watermark custclass" placeholder="Site Code" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                                    runat="server" MaxLength="25"></asp:TextBox>
                            </div>

                            <%--<div class="inlineProperty">
                                <label>
                                    Customer
                            <asp:RequiredFieldValidator ID="rfvddlCustomer" runat="server"
                                ControlToValidate="ddlCustomer" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlCustomer" runat="server" class="selector"></asp:DropDownList>
                            </div>--%>
                            <asp:HiddenField runat="server" ID="ddlCustomer" ClientIDMode="Static" />

                            <div class="clear"></div>
                            <div class="inlineProperty doubleColumn">
                                <label>
                                    Address 1
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAddress1" runat="server"
                                ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>

                                <asp:TextBox Text="" ID="txtAddress1" class="watermark custclass" placeholder="Address 1" data-validation="alphanumeric" data-validation-allowing="-+()_.#@!&*,/" data-validation-optional="true"
                                    runat="server" MaxLength="250"></asp:TextBox>
                            </div>

                            <div class="clear"></div>
                            <div class="inlineProperty doubleColumn">
                                <label>
                                    Address 2
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAddress2" runat="server"
                                ControlToValidate="txtAddress2" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:TextBox Text="" ID="txtAddress2" MaxLength="250" class="watermark custclass" placeholder="Address 2" data-validation="alphanumeric" data-validation-allowing="-+()_.#@!&*,/" data-validation-optional="true"
                                    runat="server"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                            <div class="inlineProperty">
                                <label>
                                    City
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCity" runat="server"
                                ControlToValidate="ddlCity" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlCity" runat="server" class="selector custddl"></asp:DropDownList>
                            </div>

                            <div class="inlineProperty stateProp">
                                <label>
                                    State
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlState" runat="server"
                                ControlToValidate="ddlState" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlState" runat="server" class="selector custddl"></asp:DropDownList>
                            </div>

                            <div class="inlineProperty zipCodeProp">
                                <label>
                                    Zip Code<asp:RequiredFieldValidator ID="RequiredFieldValidatortxtZipCode" runat="server"
                                        ControlToValidate="txtZipCode" Display="Dynamic" ErrorMessage="*"
                                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>

                                <asp:TextBox Text="" ID="txtZipCode" MaxLength="8" class="watermark custclass" placeholder="Zip Code" data-validation="alphanumeric" data-validation-allowing="-" data-validation-optional="true"
                                    runat="server"></asp:TextBox>

                            </div>

                            <div class="inlineProperty countryProp">
                                <label>
                                    Country<asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCountry" runat="server"
                                        ControlToValidate="ddlCountry" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlCountry" runat="server" class="selector custddl"></asp:DropDownList>

                            </div>
                            <div class="clear"></div>
                            <div class="inlineProperty">
                                <label>
                                    Phone Number 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPhoneNumber" runat="server"
                                    ControlToValidate="txtPhoneNumber" Display="Dynamic" ErrorMessage="*"
                                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:TextBox Text="" ID="txtPhoneNumber" MaxLength="14" TextMode="Phone" class="watermark custclass" placeholder="(000) 000-0000" data-validation="usphone" data-validation-allowing="()-" data-validation-optional="true"
                                    runat="server"></asp:TextBox>

                            </div>

                            <div class="inlineProperty">
                                <label>Website</label>
                                <asp:TextBox Text="" ID="txtwebsite" MaxLength="2083" class="watermark" placeholder="www" data-validation="url" data-validation-optional="true" data-suggestions="www.intelligis.com/"
                                    runat="server"></asp:TextBox>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Account Representative<asp:RequiredFieldValidator ID="RequiredFieldValidatorddlAccountRep" runat="server"
                                        ControlToValidate="ddlAccountRep" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlAccountRep" runat="server" class="selector custddl"></asp:DropDownList>

                            </div>
                            <div class="clear"></div>
                            <div class="inlineProperty">
                                <label>
                                    Primary Engineer
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlPrimaryEngineer" runat="server"
                                ControlToValidate="ddlPrimaryEngineer" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlPrimaryEngineer" runat="server" class="selector custddl"></asp:DropDownList>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Primary Contact
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlPrimaryContact" runat="server"
                                ControlToValidate="ddlPrimaryContact" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlPrimaryContact" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlPrimaryContact_SelectedIndexChanged" class="selector custddl">
                                </asp:DropDownList>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Title
                                </label>
                                <asp:TextBox Text="" ID="txtPrimaryContactTitle" MaxLength="150" class="watermark" placeholder="Title"
                                    runat="server" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                            <div class="inlineProperty">
                                <label>
                                    Phone Number
                                </label>
                                <asp:TextBox Text="" ID="txtPrimaryContactPhone" MaxLength="150" TextMode="phone" class="watermark" placeholder="(000) 000-0000"
                                    runat="server" Enabled="False"></asp:TextBox>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Email Address
                                </label>
                                <asp:TextBox Text="" ID="txtEmail" MaxLength="150" TextMode="Email" class="watermark" placeholder="Email"
                                    runat="server" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <%--<asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" />--%>
                    <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnPopupSubmit_Click" />
                    <asp:Button ID="btnSubmithid" CssClass="actionBtn" runat="server" Text="Submit" Style="display: none" ValidationGroup="Req" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnBack" CssClass="actionBtn" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />



                </div>

                <asp:HiddenField runat="server" ID="hidIsAutoTask" />
                <asp:HiddenField runat="server" ID="hidIsAutoTaskCheckRequried" Value="True" />
                <asp:HiddenField runat="server" ID="hidAutoTaskID" />


                <div id="customSearch" runat="server">
                    <div class="inlineProperty">
                        <asp:TextBox Text="" ID="txtSearch" placeholder="Search"
                            runat="server" MaxLength="50"></asp:TextBox>
                    </div>

                    <div class="inlineProperty">
                        <asp:Button ID="btnSearchSubmit" CssClass="actionBtn" runat="server" Text="Search" />
                        <asp:Button ID="btnSearchClear" CssClass="actionBtn" runat="server" Text="Clear" />
                    </div>
                </div>

                <div id="grdSite" runat="server">
                    <table id="grdSiteMasters"></table>
                    <div id="grdSitepager"></div>
                </div>
            </div>

            <div id="dialog-message" title="Warning">
                <div>Please, select row</div>
            </div>

            <div style="display: none;">
                <div id="divAutoTask" class="popupSearch">
                    <article class="widget">
                        <div class="headerSection">
                            <h1>Auto Task Search - Customer Site</h1>
                        </div>
                        <div class="widgetContentWrap">
                            <div class="widgetContent">
                                <div class="inlineProperty">
                                    <asp:TextBox Text="" ID="txtCustName" class="watermark" placeholder="Site Name"
                                        runat="server" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="inlineProperty">
                                    <asp:Button ID="btnSearch" CssClass="actionBtn" ClientIDMode="Static" runat="server" Text="Search" />
                                </div>
                                <div class="clear"></div>
                                <div id='loadingDiv'></div>
                                <div class="clear"></div>
                                <div id="grdAutoTaskCustomer" runat="server" clientidmode="Static">
                                    <div style="padding: 15px 0px; margin-right: 10px; text-align: center;">
                                        <table id="grdAutoTaskCustomerMasters"></table>
                                        <div id="grdAutoTaskCustomerpager"></div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </article>

                </div>
            </div>

            <div id="dialog-confirm" title="Autotask" style="display: none;">
                <p>
                    <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
                </p>
            </div>
        </div>
        <div class="clear"></div>
    </form>
    <footer>
        <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
    </footer>
</body>
</html>


