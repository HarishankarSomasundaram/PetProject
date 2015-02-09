<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserInfo.ascx.cs" Inherits="UserControlsUserInfo" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<script type="text/javascript">

    var isColorBox = "no";
    var provUser = 0;
    var provisioning = 0;

    provUser = getQueryStringByName("provUser");
    provisioning = getQueryStringByName("provisioning");

    if (getQueryStringByName("isColorBox") == "yes") {
        isColorBox = getQueryStringByName("isColorBox");
        $(".autotask").css("display", "none");
        if (provUser == 1) {
            $("#hTab-1").prepend($("#provUser"));
            $("#provUser").css("display", "block");
        }
    }

    var siteID = "";
    if ($.cookie("siteID") == null) {
        siteID = $('.customerSite option:selected').val();
    }
    else {
        siteID = $.cookie("siteID");
    }

    if (siteID == "" || siteID == null) { siteID = 0 }
    var sessionCustomerId = $.cookie("sessionCustomerId");
    if (sessionCustomerId == "" || sessionCustomerId == null) { sessionCustomerId = 0 }

    var searchFilter = $.cookie("SearchUser");
    if (searchFilter == "" || searchFilter == null) { searchFilter = 0 }

    var gridWidth = ""
    var gridName = "#grdUserInfo";
    var gridName2 = "#grdAutoTaskCustomerMasters";
    var gridPager = "#grdUserInfopager";
    var gridPager2 = "#grdAutoTaskCustomerpager";
    var IFrameBaseURL = '<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>';
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220";
    var gridSortOrder = "asc";
    var gridpageSize = "10";;
    var gridListName = "UserList";
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {
        //To define the Grid for the page on the design time
        //var colname = ["MasterDetailID", "Master Name", "Master Description", "ModifiedBy", "Created By", "ModifiedOn"];
        var colname = ["UserID", "First Name", "Last Name", "Username", "Title", "Department", "Email", "Phone1", "Phone2"];

        var colmodel = [
                           { name: 'UserID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: false, hidedlg: true },
                           { name: 'FirstName', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'LastName', sortable: true, hidedlg: false, hidden: false, editable: false, search: true },
                           { name: 'UserName', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'TitleName', sortable: false, align: "left", hidden: false, editable: false, search: false },
                           { name: 'DepartmentName', sortable: false, align: "left", hidden: false, editable: false, search: false },
                           { name: 'Email', sortable: true, align: "left", hidden: false, editable: false },
                           { name: 'Phone1', sortable: true, align: "left", hidden: false, editable: false },
                           { name: 'Phone2', sortable: true, align: "left", hidden: false, editable: false }
                           //{ name: 'View', width: 40, sortable: true, align: "left", hidden: false, editable: false, search: false },

        ];
        //Default SortColumn
        var sortName = "FirstName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLUSERS/" + caption + "/" + siteID + "/" + searchFilter;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEUSERBYUSERID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEUSERBYUSERID";
        //Calling the webservices and the desgining the Grid at Runtime 
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "CustomerInfo.aspx?do=a&isColorBox=" + isColorBox + "&nav=Users"; // Add URL
            EditUrl = "CustomerInfo.aspx?do=e&isColorBox=" + isColorBox + "&nav=Users&id="; //Edit URL
            ViewUrl = "CustomerInfo.aspx?do=m&isColorBox=" + isColorBox + "&nav=Users&id="; //View URL
        }
        else {
            AddUrl = "CustomerInfo.aspx?do=a&nav=Users"; // Add URL
            EditUrl = "CustomerInfo.aspx?do=e&nav=Users&id="; //Edit URL
            ViewUrl = "CustomerInfo.aspx?do=m&nav=Users&id="; //View URL
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
                                        true, //is New page required for Add
                                        AddUrl,
                                        true, //is New page required for Edit
                                        EditUrl,
                                        deleteWebServiceURL,
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for view
                                        ViewUrl //View URL
                                     );

        return objGridList;
    };


    function InitializeGrid2(caption, gridServiceURL) {

        //To define the Grid for the page on the design time
        //var colname = ["Customer ID", "Company ID", "Company Name", "Customer Code", "Customer Name", "Address", "City ID", "City Name", "State ID", "State Name", "Country ID", "Country Name", "Zip Code", "Phone Number", "Alternate Phone No", "Fax", "Email Address", "Account Rep ID", "Account Rep Name", "PrimaryEngineerID", "Primary Engineer Name", "Notes", "Created On", "Created By", "Created By Name", "Last Modified On", "Last Modified By Name"];
        var colname = ["Action", "", "First Name", "Last Name", "Email", "Phone", "Alternate Phone"];
        var colmodel = [

                           { name: 'View', width: 80, sortable: true, align: "center", hidedlg: false, hidden: false, editable: false, search: false },
                           { name: 'FirstName', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                           { name: 'FirstName', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'LastName', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'Email', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'Phone1', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'Phone2', sortable: true, align: "center", hidden: false, editable: false, search: true },
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
                                        10, //  Number of records in each page
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
                                        "CustomerInfo.aspx?do=a", // Add URL
                                        false, //is New page required for Edit
                                        "CustomerInfo.aspx?do=e&id=", //Edit URL
                                        deleteWebServiceURL,
                                        paperSize,
                                        paperOrientation,
                                        false, //is New page required for view
                                        "" //view URL
                                     );
        return objGridList;
    };
    $(document).ready(function () {
        $('#grdUserInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Users"));
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();
        $("#del_grdUserInfo").insertAfter(".ui-pg-button:nth(3)");

        // var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLUSERAUTOTASK/Users/" + siteID + "/S";
        // jqGridGenerator(InitializeGrid2("Users", getWebServiceURL));

        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
        $("#txtNotes_tag").attr("tabindex", "20");

        $(".inline").colorbox({
            rel: 'inline',
            inline: true,
            href: $(this).attr('href'),
            height: '85%',
            transition: 'none'
        });
        $('#loadingDiv').hide();
        $("#btnUserSearch").bind("click", function () {

            $('#grdAutoTaskCustomerMasters').jqGrid('GridUnload');
            var caption2 = "Users";
            var Users = $("#txtUserNameAuto").val();
            var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLUSERAUTOTASK/" + Users + "/" + siteID + "/FIRSTNAME";
            jqGridGenerator(InitializeGrid2(caption2, getWebServiceURL));
            $('.ui-icon-excel').hide();
            $('.ui-icon-pdf').hide();

            return false;
        });

        $("#txtUserNameAuto").bind('keypress', function (e) {
            if (e.which === 13) {
                $('#btnUserSearch').trigger('click');
            }
        });

        $("#existingUser").click(function () {
            if (isColorBox == "yes") {
                $("#divExistingUser").css("display", "block");
            }
            else {
                $("#divExistingUser").css("display", "none");
            }
        });

        if (provisioning == 1) {
            $('.ui-icon-plus').hide();
            $('.ui-icon-trash').hide();
            $('#btnBack').hide();
            $('.autotask').hide();

        }

        $("#lnkAutoTask").click(function () {
            $("#txtUserNameAuto").val("");
            $("#grdAutoTaskCustomer").css("display", "none");
        });

        $("#txtUserNameAuto").keyup(function () {
            if (!this.value) {
                $("#grdAutoTaskCustomer").css("display", "none");
            }
        });
    });

    function ShowPopup(message) {
        $(function () {
            //$("#hidIsAutoTaskCheckRequried").val(0);

            $("#dialog-confirm").html(message);
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 200,
                modal: true,
                buttons: {
                    'Submit': function () {

                        <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSubmit))%>;
                    },
                    'Cancel': function () {
                        //$("#hidIsAutoTaskCheckRequried").text('1');
                        $("#hidIsAutoTaskCheckRequried").val('1');
                        $(this).dialog('close');
                    }
                }
            });

        });
    };

</script>
<div id="provUser" style="padding-top: 10px; padding-bottom: 10px; margin-left: 10px; display: none;">
    Enter the following information for the new user or <a style="text-decoration: underline; color: blue;" target="_self" href="#" id="existingUser">Copy from an Existing User</a>
    <div id="divExistingUser" class="inlineProperty" style="display: none;">
        <label>User List</label>
        <asp:DropDownList ID="ddlAUsers" TabIndex="9" runat="server" class="selector" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlAUsers_Change"></asp:DropDownList>
    </div>
</div>


<div class="innerTabContent" id="innerTabContent">
    <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
    </p>
    <div class="autotask">
        <asp:LinkButton runat="server" ID="lnkAutoTask" href="#divAutoTask" CssClass="forgotPass inline" ClientIDMode="Static"></asp:LinkButton>
    </div>

    <div id="CrudUser" runat="server" class="siteDetail contentDetail" style="padding-top: 30px; margin-left: 10px">
        <div id="divUserDetail" runat="server" class="scrollabow" name="top">
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div18" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=FirstName&ISForward=0&elemrntId=txtFirstName" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup">
                        </div>
                    </div>
                    <%} %>
                    <label>
                        First Name
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFirstName" runat="server" CssClass="requiredField"
                        ControlToValidate="txtFirstName" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtFirstName" TabIndex="1" ClientIDMode="Static" class="watermark" placeholder="First Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="50"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div17" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=LastName&ISForward=0&elemrntId=txtLastName" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Last Name
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtLastName" runat="server" CssClass="requiredField"
                        ControlToValidate="txtLastName" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtLastName" TabIndex="2" ClientIDMode="Static" class="watermark" placeholder="Last Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="50"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div16" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=username&ISForward=0&elemrntId=txtUserName" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Username
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtUserName" runat="server" CssClass="requiredField"
                        ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>

                <asp:TextBox Text="" ID="txtUserName" TabIndex="3" class="watermark Username" placeholder="Username" ClientIDMode="Static" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div15" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=Password&ISForward=0&elemrntId=txtPassword" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Password
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPassword" runat="server" CssClass="requiredField"
                        ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtPassword" TabIndex="4" class="watermark" placeholder="Password" ClientIDMode="Static"
                    runat="server" MaxLength="20"></asp:TextBox>
                <%-- <div class="keywords">
                        <span class="field">
                            <asp:TextBox Text="" ID="tags" class="longinput watermark" placeholder="Key"
                                runat="server"></asp:TextBox>

                        </span>
                    </div>--%>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div14" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=Email&ISForward=0&elemrntId=txtEmail" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Email
                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmail" runat="server" CssClass="requiredField"
                    ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtEmail" TabIndex="5" class="watermark" placeholder="Email" ClientIDMode="Static" data-validation="email" data-validation-optional="true"
                    runat="server" MaxLength="62"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div13" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=Phone1&ISForward=0&elemrntId=txtPhone1" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Phone1
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPhone1" runat="server" CssClass="requiredField"
                        ControlToValidate="txtPhone1" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtPhone1" TabIndex="6" class="watermark" placeholder="(000) 000-0000" TextMode="Phone" ClientIDMode="Static" data-validation="usphone" data-validation-allowing="()-" data-validation-optional="true"
                    runat="server" MaxLength="14"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div12" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=Phone2&ISForward=0&elemrntId=txtPhone2" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Phone2
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPhone2" runat="server" CssClass="requiredField"
                        ControlToValidate="txtPhone2" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtPhone2" class="watermark" TabIndex="7" placeholder="(000) 000-0000" TextMode="Phone" ClientIDMode="Static" data-validation="usphone" data-validation-allowing="()-" data-validation-optional="true"
                    runat="server" MaxLength="14"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div6" class=" actionPanel  divIframeOperations" runat="server">
                        <div style="position: relative; width: 17px; display: inline-block;">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=TitleID&ISForward=1&elemrntId=ddlTitle" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Titles&iTitle=Title&isColorBox=yes" style="color: blue;" class="iframe UserTitle"></a></span>
                        <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Titles&iTitle=Title&isColorBox=yes" style="color: blue;" class="iframe UserTitle"></a></span>
                        <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Titles&iTitle=Title&isColorBox=yes" style="color: blue;" class="iframe UserTitle"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Title
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlTitle" runat="server" CssClass="requiredField" AllowSingleDeselect="true"
                            ControlToValidate="ddlTitle" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                            ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>

                </div>
                <asp:DropDownList ID="ddlTitle" TabIndex="8" runat="server" ClientIDMode="Static" class="selector"></asp:DropDownList>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div7" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Users&HistoryFieldName=DepartmentID&ISForward=1&elemrntId=ddlDepartment" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                        <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Departments&iTitle=Department&isColorBox=yes" style="color: blue;" class="iframe Department"></a></span>
                        <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Departments&iTitle=Department&isColorBox=yes" style="color: blue;" class="iframe Department"></a></span>
                        <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Departments&iTitle=Department&isColorBox=yes" style="color: blue;" class="iframe Department"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Department
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlDepartment" runat="server" CssClass="requiredField"
                            ControlToValidate="ddlDepartment" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                            ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>

                </div>
                <asp:DropDownList ID="ddlDepartment" TabIndex="9" runat="server" class="selector" ClientIDMode="Static"></asp:DropDownList>
            </div>

            <div class="inlineProperty firstColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div5" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Security Groups&iTitle=Security Group&isColorBox=yes" style="color: blue;" class="iframe SecurityGroup"></a></span>
                        <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Security Groups&iTitle=Security Group&isColorBox=yes" style="color: blue;" class="iframe SecurityGroup"></a></span>
                        <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Security Groups&iTitle=Security Group&isColorBox=yes" style="color: blue;" class="iframe SecurityGroup"></a></span>
                    </div>
                    <%} %>
                    <label>Security Group</label>

                </div>
                <asp:DropDownList ID="mulDDlSecurityGroup" TabIndex="10" runat="server" class="chosen-select-width Security" data-placeholder="Security Group" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDDlSecurityGroup" runat="server" />
            </div>
            <div class="inlineProperty secondColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Remote Access&iTitle=Remote Access&isColorBox=yes" style="color: blue;" class="iframe RemoteAccess"></a></span>
                        <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Remote Access&iTitle=Remote Access&isColorBox=yes" style="color: blue;" class="iframe RemoteAccess"></a></span>
                        <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Remote Access&iTitle=Remote Access&isColorBox=yes" style="color: blue;" class="iframe RemoteAccess"></a></span>
                    </div>
                    <%} %>
                    <label>Remote Access</label>

                </div>
                <asp:DropDownList ID="mulDdlRemoteAccess" TabIndex="11" runat="server" class="chosen-select-width Remote" Width="100%" data-placeholder="Remote Access" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlRemoteAccess" runat="server" />
            </div>

            <div class="inlineProperty firstColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div3" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Workstations&iframe=1&iframedo=a&opp=S&isColorBox=yes" style="color: blue;" class="iframe Workstations"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Workstations&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Workstations"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Workstations&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Workstations"></a></span>
                    </div>
                    <%} %>
                    <label>Computer</label>

                </div>
                <asp:DropDownList ID="mulDdlComputer" TabIndex="12" runat="server" class="chosen-select Computer" data-placeholder="Computers" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlComputer" runat="server" />
            </div>

            <div class="inlineProperty secondColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div2" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Laptops&iframe=1&iframedo=a&opp=S&isColorBox=yes" style="color: blue;" class="iframe Laptops"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Laptops&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Laptops"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Laptops&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Laptops"></a></span>
                    </div>
                    <%} %>
                    <label>Laptop</label>

                </div>
                <asp:DropDownList ID="mulDdlLaptop" TabIndex="13" runat="server" class="chosen-select-width Laptop" data-placeholder="Laptops" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlLaptop" runat="server" />
            </div>

            <div class="inlineProperty firstColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div1" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Mobile Devices&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe MobileDevices"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Mobile Devices&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe MobileDevices"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Mobile Devices&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe MobileDevices"></a></span>
                    </div>
                    <%} %>
                    <label>Mobile Phone</label>

                </div>
                <asp:DropDownList ID="mulDdlMobilePhone" TabIndex="14" runat="server" class="chosen-select-width Mobile" data-placeholder="Mobile Phones" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlMobilePhone" runat="server" />
            </div>
            <div class="inlineProperty secondColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="divTablet" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Tablets&iTitle=Tablet&isColorBox=yes" style="color: blue;" class="iframe Tablet"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Tablets&iTitle=Tablet&isColorBox=yes" style="color: blue;" class="iframe Tablet"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Tablets&iTitle=Tablet&isColorBox=yes" style="color: blue;" class="iframe Tablet"></a></span>
                    </div>
                    <%} %>
                    <label>Tablet</label>

                </div>
                <asp:DropDownList ID="mulDdlTablet" runat="server" TabIndex="15" class="chosen-select-width Tablets" data-placeholder="Tablets" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlTablet" runat="server" />
            </div>
            <%--remove Application--%>
            <div style="display: none;">
                <div class="inlineProperty firstColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div8" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Applications&iTitle=Application&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Applications&iTitle=Application&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Applications&iTitle=Application&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                        </div>
                        <%} %>
                        <label>Application</label>

                    </div>
                    <asp:DropDownList ID="mulDdlApps" runat="server" TabIndex="16" class="chosen-select-width Apps" data-placeholder="Application" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulDdlApps" runat="server" />
                </div>
            </div>
              <%--remove Application--%>

            <div class="inlineProperty secondColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div9" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Network Shares&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe NetworkShares"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Network Shares&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe NetworkShares"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Network Shares&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe NetworkShares"></a></span>
                    </div>
                    <%} %>
                    <label>Network Shares</label>

                </div>
                <asp:DropDownList ID="mulDdlNetworkShares" runat="server" TabIndex="17" class="chosen-select-width Network" data-placeholder="Network Shares" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlNetworkShares" runat="server" />

            </div>

            <div class="inlineProperty firstColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div11" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Servers&iframe=1&iframedo=a&opp=S&isColorBox=yes" style="color: blue;" class="iframe Servers"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Servers&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Servers"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Servers&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Servers"></a></span>
                    </div>
                    <%} %>
                    <label>Servers</label>

                </div>
                <asp:DropDownList ID="mulDdlServers" runat="server" TabIndex="18" class="chosen-select-width Servers" data-placeholder="Servers" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlServers" runat="server" />
            </div>
            <div class="inlineProperty secondColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div10" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Printers&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe Printer"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Printers&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Printer"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Printers&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe Printer"></a></span>
                    </div>
                    <%} %>
                    <label>Printers</label>

                </div>
                <asp:DropDownList ID="mulDdlPrinters" runat="server" TabIndex="19" class="chosen-select-width Printers" data-placeholder="Printers" multiple></asp:DropDownList>
                <asp:HiddenField ID="hidmulDdlPrinters" runat="server" />

            </div>

            <div class="inlineProperty">
                <label>Notes</label>
                <asp:TextBox Text="" ID="txtNotes" class="watermark multiText" TabIndex="20" placeholder="Notes" ClientIDMode="Static"
                    runat="server" MaxLength="2000"></asp:TextBox>

            </div>
            <div class="footer-btn">
                <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" TabIndex="21" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" />
                <asp:Button ID="btnBack" CssClass="actionBtn" runat="server" TabIndex="22" Text="Back" OnClick="btnBack_Click" ClientIDMode="Static" />
            </div>
        </div>
    </div>

    <div style="display: none;">
        <div id="divAutoTask">
            <article class="widget" style="width: 100% !important;">
                <div class="headerSection">
                    <h1>Auto Task Search - User</h1>
                </div>
                <div class="widgetContentWrap">
                    <div class="widgetContent popupSearch">
                        <div class="inlineProperty">
                            <asp:TextBox Text="" ID="txtUserNameAuto" class="watermark" placeholder="Name" data-validation-optional="false"
                                runat="server" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="inlineProperty">
                            <asp:Button ID="btnUserSearch" CssClass="actionBtn" runat="server" Text="Search" ClientIDMode="Static" />
                        </div>
                        <div class="clear"></div>
                        <div id='loadingDiv'></div>
                        <div class="clear"></div>

                        <div id="grdAutoTaskCustomer" runat="server" clientidmode="Static">
                            <div style="padding: 15px 0px; margin-left: 10px; margin-right: 10px; text-align: center;">
                                <table id="grdAutoTaskCustomerMasters"></table>
                                <div id="grdAutoTaskCustomerpager"></div>

                            </div>
                        </div>
                    </div>
                </div>
            </article>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidIsAutoTask" />
    <%--<asp:Label runat="server" ID="hidIsAutoTaskCheckRequried" Text="" ClientIDMode="Static"/>--%>
    <asp:HiddenField runat="server" ID="hidIsAutoTaskCheckRequried" Value="1" ClientIDMode="Static" />

    <asp:HiddenField runat="server" ID="hidAutoTaskID" />
    <div id="dialog-confirm" title="Autotask" style="display: none;">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        </p>
    </div>
    <div id="divGrdUserInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center;">
            <table id="grdUserInfo"></table>
            <div id="grdUserInfopager"></div>
        </div>
    </div>
</div>

