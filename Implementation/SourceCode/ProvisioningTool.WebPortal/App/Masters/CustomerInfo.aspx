<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerInfo.aspx.cs" Inherits="MastersCustomerInfo " %>

<%@ Register Src="~/includes/UserControls/common/CustomerMaster.ascx" TagName="CustomerMaster" TagPrefix="ProvisioningTool" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server" class="iframeBodyClass">
    <title>Customers</title>
    <ProvisioningTool:CustomerMaster ID="CustomerMaster" runat="server" />
    <script type="text/javascript">
        var siteID = $.cookie("siteID");
        if (siteID == "" || siteID == null) {
            siteID = 0;
            $('#headerCustomer').hide();
        }

        var gridWidth = "";
        var gridName = "#grdCustomerMasters";
        var gridName2 = "#grdAutoTaskCustomerMasters";
        var gridPager = "#grdCustomerpager";
        var gridPager2 = "#grdAutoTaskCustomerpager";
        var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
        var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
        var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
        var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
        var gridHeight = "250"
        var gridSortOrder = "asc"
        var gridpageSize = "10"
        var gridListName = "CustomerList"
        var pageSizeOption = ["10", "20", "40", "60"];
        var paperSize = "a2";
        var paperOrientation = "p"; // p - portriat : l - landscap

        function InitializeGrid(caption, webUrl) {

            //To define the Grid for the page on the design time
            var colname = ["Customer ID", "Company ID", "Customer Code", "Customer Name", "Address", "Phone Number", "Email Address", "Account Rep", "Primary Engineer"];
            var colmodel = [
                               { name: 'CustomerID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                               { name: 'CompanyID', sortable: true, align: "left", hidedlg: true, hidden: true, editable: true },
                               //{ name: 'CompanyName', sortable: true, hidden: false, editable: true, search: false },
                               { name: 'CustomerCode', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'CustomerName', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'Address', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'PhoneNumber', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'EmailAddress', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'AccountRepName', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'PrimaryEngineerName', sortable: true, align: "center", hidden: false, editable: false, search: false }
            ];
            //Default SortColumn
            var sortName = "CustomerID";
            var gridCaption = caption;

            var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETECUSTOMERBYCUSTOMERID";
            var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETECUSTOMERBYCUSTOMERID";


            //Calling the webservices and the desgining the Grid at Runtime 
            var objGridList = new oData(
                                            gridName, // Table or Grid name in the page
                                            webUrl,//Web Service URL
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
                                            false, //is New page required for Add
                                            "CustomerInfo.aspx?search=1&mode=a&do=a", // Add URL
                                            true, //is New page required for Edit
                                            "CustomerInfo.aspx?search=1&do=e&mode=v&id=", //Edit URL
                                            deleteWebServiceURL,
                                            paperSize,
                                            paperOrientation,
                                            true, //is New page required for view
                                            "CustomerInfo.aspx?do=m&mode=s&id=" //view URL
                                         );
            return objGridList;
        };

        function InitializeGrid2(caption, gridServiceURL) {

            //To define the Grid for the page on the design time
            var colname = ["Action", "Customer ID", "Customer Code", "Customer Name", "Address", "Phone Number"];
            var colmodel = [

                               { name: 'View', sortable: true, align: "center", hidedlg: false, hidden: false, editable: false, search: false },
                               { name: 'CustomerCode', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                               { name: 'CustomerCode', sortable: true, align: "center", hidden: false, editable: false, search: true },
                               { name: 'CustomerName', sortable: true, align: "center", hidden: false, editable: false, search: true },
                               { name: 'Address', sortable: true, align: "center", hidden: false, editable: false, search: true },
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
                                            "CustomerInfo.aspx?search=1&do=a", // Add URL
                                            false, //is New page required for Edit
                                            "CustomerInfo.aspx?search=1&do=e&id=", //Edit URL
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

            $("#ClientButton").click(function () {
                $("#dialog-message").show();

                $("#dialog-message").dialog({
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
                $("#relatedRecordDiv1").html("Please select a Row !");
                //return true;
            });

            if (getQueryStringByName("do") != "m") {
                //VALIDATION CONTROL jquery.form-validator
                $.validate({
                    form: '#Custmain',
                    modules: 'sweden,security',
                    language: myLanguage
                });
            }
            //implement Edit button start
            $("#btnEdit").hide();
            $("#ClientButton").hide();
            if (getQueryStringByName("do") == "m") {
                $("#btnEdit").show();
            }
            //end
            var caption = "Customers";
            var caption2 = "Customers - AutoTask";

            if (getQueryStringByName("do") == "a") {
                $("#lblHeader").html("Create Customer");
                $("#ClientButton").show();
            }
            else if (getQueryStringByName("do") == "e") {
                $("#lblHeader").html("Modify Customer");
                $("#ClientButton").show();
            }

            $("#aCustomer").addClass("active");
            $("#aCustomer").next().find('#Master_a1').addClass("active");;
            $("#aCustomer").next().show();

            $('#grdCustomerMasters').jqGrid('GridUnload');

            if (getQueryStringByName("search") == 1) {
                var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLCUSTOMERS/" + caption + "/" + siteID + "/AccountName";
                jqGridGenerator(InitializeGrid(caption, getWebServiceURL));
            }
            else { $("#customSearch").hide(); }

            if ($("#CrudCustomer").length > 0) { $("#customSearch").hide(); }

            $('.ui-jqgrid-title').text(caption);

            $("#del_grdCustomerMasters").insertAfter(".ui-pg-button:nth(3)");

            $('#lnkAutoTask').click(function () {
                $('body').addClass('scrollHide');
                $('.ui-icon-excel').hide();
                $('.ui-icon-pdf').hide();
            });

            $('#cboxClose').live('click', function () {
                $('body').removeClass('scrollHide');
            });

            $(document).keyup(function (e) {
                if (e.keyCode == 27) {
                    $('body').removeClass('scrollHide');
                }
            });

            $(".inline").colorbox({
                rel: 'inline',
                inline: true,
                href: $(this).attr('href'),
                height: '85%',
                transition: 'none'
            });

            $('#loadingDiv').hide();

            $("#btnSearch").click(function () {
                $("#grdAutoTaskCustomer").hide();
                $('#grdAutoTaskCustomerMasters').jqGrid('GridUnload');
                var caption2 = "Customers";
                var CustomerName = $("#txtCustName").val();
                
                if (CustomerName == "") { CustomerName = "all";}
                var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLCUSTOMERSAUTOTASK/" + CustomerName + "/" + siteID + "/AccountName";
                jqGridGenerator(InitializeGrid2(caption2, getWebServiceURL));

                $('.ui-icon-excel').hide();
                $('.ui-icon-pdf').hide();
                return false;
            });

            if (getQueryStringByName("autotask") == 's') {
                //$("#divMessage").css("display", "block");
                $("#lblErrorMessage").html(getQueryStringByName("message"));
            }

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

            $("#btnSearchSubmit").click(function () {

                $('#grdCustomerMasters').jqGrid('GridUnload');

                var txtSearch = $("#txtSearch").val();
                var gridSearchURL = "";
                //Check if the value is empty if so all the data should be fetched
                if (txtSearch == "")
                    txtSearch = "all";

                if (txtSearch == "all") {
                    gridSearchURL = baseServiceURL + masterServiceName + "GetCustomerBySearchKey/all/all/all/all/" + caption;

                    jqGridGenerator(InitializeGrid(caption, gridSearchURL));
                }
                else {
                    gridSearchURL = baseServiceURL + masterServiceName + "GetCustomerBySearchKey/" + txtSearch + "/all/all/all/" + caption;
                    jqGridGenerator(InitializeGrid(caption, gridSearchURL));
                }

                //This is hidded because there is no requirment from client
                hideGridfooter();
                return false;
            });

            $("#btnSearchClear").click(function () {
                $("#txtSearch").val('');
                $("#btnSearchSubmit").click();
                return false;
            });

            //implement Edit button start
            $("#btnEdit").click(function () {
                var ID = getQueryStringByName("id");
                if (ID != null) {
                    var RedirectURL = 'CustomerInfo.aspx?search=1&do=e&mode=v&id=' + ID;
                    window.location = RedirectURL;
                }
                return false;
            });
            //end

            hideGridfooter();
            return false;
        });


        function DisplayDialog() {
            $(function () {
                $("#dialog-message").show();
                $("#dialog-message").dialog({
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
                $("#relatedRecordDiv1").html("Do you want to Submit!");
                return true;
            });
        };

        function ShowPopup(message) {
            $(function () {
                var autoTaskID = $("#hidAutoTaskID").val();
                var customerCode = $("#txtCustomerCode").val();
                var buttonName;

                $("#dialog-confirm").html(message);

                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 200,
                    modal: true,
                    buttons: {
                        OK: function () {
                            $.ajax({
                                type: 'POST',
                                url: "../Masters/CustomerInfo.aspx/InsertSitesandUsers",
                                data: '{"AutoTaskID":"' + autoTaskID + '","CustomerCode":"' + customerCode + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: false,
                                success: function (data) {
                                    //console.log(data.d);
                                    //if (data.d == "")
                                    $("#dialog-confirm").dialog("close");
                                    window.location = 'CustomerInfo.aspx?autotask=s&message=' + data.d;
                                }
                            });
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });
        };

        function ShowPopup2(message) {
            $(function () {
                $("#hidIsAutoTaskCheckRequried").val('False');

                $("#dialog-confirm2").html(message);
                $("#dialog-confirm2").dialog({
                    resizable: false,
                    height: 400,
                    widht: 400,
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

        $(window).load(function () {
            var docHeight = $(window).height();
            var bodyHeight = $('body').height();
            if (docHeight < bodyHeight) {
                $('footer').css('position', 'relative');
            }
            else {
                $('footer').css('position', 'fixed');
            }

        });
    </script>

</head>
<body id="PageBody" runat="server" class="">
    <form id="Custmain" runat="server">
        <div class="divMessage" id="divMessage" runat="server">
            <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        </div>
        <div class="fullWidthGrid">
            <div class="leftMenuWrapper">
                <ul>
                    <li>
                        <div class="largeNav" id="iSearchCustomer">
                            <img src="../../includes/UI/images/searchCustomerLargeIcon.png" />
                            <h3>Search</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iCreateCustomer">
                            <img src="../../includes/UI/images/createCustomerLargeIcon.png" />
                            <h3>Create</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iModifyCustomer">
                            <img src="../../includes/UI/images/modifyCustomerLargeIcon.png" />
                            <h3>Modify</h3>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="rightContent">
                <div id="dialog-message" title="Warning">
                    <%--<div>Do you want to Submit</div>--%>
                    <div id="relatedRecordDiv1"></div>
                </div>
                <div id="dialogr" title="Warning" > <%--class="dialogr"--%>
                    <div id="relatedRecordDiv"></div>
                </div>

                <div id="FormPage" runat="server">
                    <div id="CrudCustomer" runat="server" class="siteDetail clearedContent">
                        <div class="contentDetail contentLoadWrap" runat="server" id="cusomerDetail" style="margin-left: 10px">
                            <asp:HiddenField runat="server" ID="hidCompanyID" />
                            <h1 id="lblHeader">Customer </h1>
                            <%--implement Edit button start--%>
                            <asp:Button ID="btnEdit" CssClass="actionEditBtn" runat="server" Text="Edit" />
                            <%--end--%>
                            <div class="autotask">
                                <%-- <asp:Image runat="server" ID="imgAutoTask" ImageUrl="~/includes/css/images/autotask-logo.gif" Width="120px" Height="70px" CssClass="AutoTask" />--%>

                                <asp:LinkButton runat="server" ID="lnkAutoTask" href="#divAutoTask" CssClass="forgotPass inline" ClientIDMode="Static"></asp:LinkButton>
                            </div>
                            <div class="connectWise">
                                <%-- <asp:Image runat="server" ID="imgAutoTask" ImageUrl="~/includes/css/images/autotask-logo.gif" Width="120px" Height="70px" CssClass="AutoTask" />--%>
                                <asp:LinkButton runat="server" ID="lnkConnectWise" href="#divConnectWise" CssClass="forgotPass inline"></asp:LinkButton>
                            </div>


                            <div class="clear"></div>

                            <div class="inlineProperty">
                                <label>
                                    Customer Name
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtCustomerName" runat="server"
                            ControlToValidate="txtCustomerName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                            ValidationGroup="Req">*</asp:RequiredFieldValidator></label>

                                <asp:TextBox Text="" ID="txtCustomerName" class="watermark custclass" placeholder="Customer Name" data-validation="alphanumeric" data-validation-allowing="-+()_.," data-validation-optional="true"
                                    runat="server" MaxLength="50"></asp:TextBox>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Customer Code
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtCustomerCode" runat="server"
                            ControlToValidate="txtCustomerCode" Display="Dynamic" ErrorMessage="*" InitialValue=""
                            ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:TextBox Text="" ID="txtCustomerCode" class="watermark custclass" placeholder="Customer Code" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                                    runat="server" MaxLength="10"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                            <div class="inlineProperty doubleColumn">
                                <label>
                                    Address
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAddress" runat="server"
                            ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="*" InitialValue=""
                            ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:TextBox Text="" ID="txtAddress" MaxLength="250" class="watermark custclass" placeholder="Address" data-validation="alphanumeric" data-validation-allowing="-+()_.#@!&*,/" data-validation-optional="true"
                                    runat="server"></asp:TextBox>
                            </div>

                            <div class="inlineProperty">
                                <label>Email Address</label>
                                <asp:TextBox Text="" ID="txtEmailAddress" MaxLength="62" TextMode="Email" class="watermark" placeholder="Email Address" data-validation="email" data-validation-optional="true"
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


                            <div class="inlineProperty  zipCodeProp">
                                <label>
                                    Zip Code
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtZipCode" runat="server"
                            ControlToValidate="txtZipCode" Display="Dynamic" ErrorMessage="*"
                            ValidationGroup="Req">*</asp:RequiredFieldValidator></label>

                                <asp:TextBox Text="" ID="txtZipCode" MaxLength="8" class="watermark custclass" placeholder="Zip Code" data-validation="alphanumeric" data-validation-allowing="-" data-validation-optional="true"
                                    runat="server"></asp:TextBox>

                            </div>

                            <div class="inlineProperty countryProp">
                                <label>
                                    Country
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCountry" runat="server"
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
                                <asp:TextBox Text="" ID="txtPhoneNumber" MaxLength="14" TextMode="Phone" class="watermark custclass" placeholder="(000) 000-0000" data-validation="usphone" data-validation-optional="true"
                                    runat="server" data-validation-allowing="()-"></asp:TextBox>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Alternate Phone Number
                                </label>
                                <asp:TextBox Text="" ID="txtAltPhoneNumber" MaxLength="14" TextMode="Phone" class="watermark" placeholder="(000) 000-0000" data-validation="usphone" data-validation-optional="true"
                                    runat="server" data-validation-allowing="()-"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                            <div class="inlineProperty">
                                <label>
                                    Fax Number
                                </label>
                                <asp:TextBox Text="" ID="txtFax" TextMode="Phone" class="watermark" placeholder="Fax Number" data-validation="usphone" data-validation-optional="true"
                                    runat="server" MaxLength="8" data-validation-allowing="()-"></asp:TextBox>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Account Representative
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlAccountRep" runat="server"
                            ControlToValidate="ddlAccountRep" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                            ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlAccountRep" runat="server" class="selector custddl"></asp:DropDownList>
                            </div>

                            <div class="inlineProperty">
                                <label>
                                    Primary Engineer
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlPrimaryEngineer" runat="server"
                            ControlToValidate="ddlPrimaryEngineer" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                            ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlPrimaryEngineer" runat="server" class="selector custddl"></asp:DropDownList>
                            </div>
                            <div class="clear"></div>

                            <div class="inlineProperty threeColumn">
                                <label>Notes</label>
                                <asp:TextBox Text="" ID="txtNotes" MaxLength="250" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>

                            <div class="clear"></div>

                            <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnPopupSubmit_Click" />
                            <%--OnClientClick="return ConfirmRemoveDialog();"  --%>
                            <asp:Button ID="btnSubmithid" CssClass="actionBtn" runat="server" Text="Submit" Style="display: none" ValidationGroup="Req" OnClick="btnSubmit_Click" />


                            <%--           <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit"  Style="display: none" ValidationGroup="Req" OnClick="btnSubmit_Click" />                            
                            <button ID="ClientButton" class="ClientButton" type="button">Submit</button>--%>
                            <asp:Button ID="btnBack" CssClass="actionBtn" runat="server" Text="Back" OnClick="btnBack_Click" />

                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hidIsAutoTask" />
                    <asp:HiddenField runat="server" ID="hidIsAutoTaskCheckRequried" Value="True" />
                    <asp:HiddenField runat="server" ID="hidAutoTaskID" />

                </div>

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

                <div id="grdCustomer" runat="server">
                    <div class="noBrdr">
                        <table id="grdCustomerMasters"></table>
                        <div id="grdCustomerpager"></div>
                    </div>
                </div>

                <div style="display: none;">
                    <div id="divAutoTask" class="popupSearch">
                        <article class="widget">
                            <div class="headerSection">
                                <h1>Auto Task Search - Customer</h1>
                            </div>

                            <div class="widgetContentWrap">
                                <div class="widgetContent">
                                    <div class="inlineProperty">
                                        <asp:TextBox Text="" ID="txtCustName" class="watermark" placeholder="Customer Name" data-validation-optional="false"
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
                <div id="dialog-confirm2" title="Autotask" style="display: none;">
                    <p>
                        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
                    </p>
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
