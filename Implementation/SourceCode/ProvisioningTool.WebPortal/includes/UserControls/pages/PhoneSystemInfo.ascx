<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhoneSystemInfo.ascx.cs" Inherits="UserControlsPhoneSystemInfo" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<script type="text/javascript">

    var isColorBox = "no";
    var provUser = 0;
    var provisioning = 0;
    provUser = getQueryStringByName("provUser");
    provisioning = getQueryStringByName("provisioning");


    if (getQueryStringByName("isColorBox") == "yes") {
        isColorBox = getQueryStringByName("isColorBox");
        if (provUser == 1) {
            $("#hTab-1").prepend($("#provUser"));
            $("#provUser").css("display", "block");
            $("#HiddenColorBox").val(1);
        }
    }

    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) { siteID = 0 }

    var searchFilter = $.cookie("SearchUser");
    if (searchFilter == "" || searchFilter == null) { searchFilter = 0 }

    var gridWidth = "";
    var gridName = "#grdPhoneInfo";
    var gridPager = "#grdPhoneInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "PhoneSystemList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time
        var colname = ["PhoneSystemID", "Host Name", "Manufacture", "Model", "IPAddress", "OSVersion", "Firmware", "Installed On"];

        var colmodel = [
                           { name: 'PhoneSystemID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'Hostname', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Manufacture', sortable: true, hidedlg: true, hidden: false, editable: true, search: true },
                           { name: 'PhoneSystemModel.MasterValue', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'IPAddress', sortable: false, hidden: false, align: "center", editable: false, search: true },
                           { name: 'OSVersion.MasterValue', sortable: false, align: "left", hidden: false, editable: false, search: true },
                           { name: 'Firmware', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'InstalledOn', sortable: true, hidedlg: true, hidden: false, editable: true, search: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidedlg: true, hidden: false, editable: true, search: false }

        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLPHONESYSTEMS/" + caption + "/" + siteID + "/" + searchFilter;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEPHONESYSTEMBYPHONESYSTEMD";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEPHONESYSTEMBYPHONESYSTEMD";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "HardwareSettings.aspx?navPage=Phone System&do=a&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Phone System"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Phone System&do=e&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Phone System&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Phone System&do=m&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Phone System&id="; //View URL
        }
        else {
            AddUrl = "HardwareSettings.aspx?navPage=Phone System&do=a&nav=Phone System"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Phone System&do=e&nav=Phone System&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Phone System&do=m&nav=Phone System&id="; //View URL
        }

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
                                        "",
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for view
                                        ViewUrl //View URL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        $('#grdPhoneInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Phone System"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $("#del_grdPhoneInfo").insertAfter(".ui-pg-button:nth(3)");
        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
        $("#includes_usercontrols_pages_phonesysteminfo_ascx_txtInterfaces_tag").attr("tabindex", "16");
        $("#includes_usercontrols_pages_phonesysteminfo_ascx_txtNotes_tag").attr("tabindex", "19");

        if (getQueryStringByName("do") == "e" || getQueryStringByName("do") == "m") {
            var phoneSystemType = $('#ddlType option:selected').val();
            if (getQueryStringByName("do") == "e")
                $("#lblHeader").text("Phone System - " + phoneSystemType + " -Edit");
            else
                $("#lblHeader").text("Phone System - " + phoneSystemType);
        }

        $("#existingUser").click(function () {
            if (isColorBox == "yes" && provUser == 1)
                $("#divExistingUser").css("display", "block");
            else
                $("#divExistingUser").css("display", "none");
        });

        if (provisioning == 1) {
            $('.ui-icon-plus').hide();
            $('.ui-icon-trash').hide();
            $('#btnBack').hide();
        }

        if ($('.provClose').css('display') == "block") {
            $('.provUser').css('display', "none");
        }

    });

</script>

<div id="provUser" class="provUser" style="padding-top: 10px; padding-bottom: 10px; margin-left: 10px; display: none;">
    Enter new device information <a style="text-decoration: underline; color: blue;" target="_self" href="#" id="existingUser">Copy from an existing Device</a>
    <div id="divExistingUser" class="inlineProperty" style="display: none;">
        <asp:DropDownList ID="ddldeviceList" runat="server" class="selector" ClientIDMode="Static"></asp:DropDownList>
        <asp:Button ID="btnFill" CssClass="actionBtn" runat="server" Text="Copy" OnClick="btnFill_Click" Style="float: left; margin-left: 10px;" />
    </div>
    <asp:HiddenField ID="HiddenColorBox" runat="server" ClientIDMode="Static" Value="0" />
</div>

<div class="innerTabContent">
    <p class="divMessage" runat="server" id="divMessage">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
    </p>
    <div id="provClose" class="provClose" runat="server">
        <a style="text-decoration: underline; color: blue;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Phone%20System&provUser=1&iframe=1&do=a&iframedo=a&isColorBox=yes" id="addDevice">Add Another Device</a> |
        <a style="text-decoration: underline; color: blue;" target="_self" href="javascript:parent.$.fn.colorbox.close();" id="closeDiv">Close</a>
    </div>
    <div id="CrudPhone" runat="server" class="siteDetail">
        <div class="contentDetail" runat="server" id="divPhoneSystemDetails" style="padding-top: 0px; margin-left: 10px">
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div16" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=PhoneType&ISForward=1&elemrntId=ddlType" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Type
                <asp:RequiredFieldValidator ID="rfvType" runat="server"
                    ControlToValidate="ddlType" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlType" TabIndex="1" runat="server" class="selector" ClientIDMode="Static">
                    <asp:ListItem>Cloud</asp:ListItem>
                    <asp:ListItem>Hosted</asp:ListItem>
                    <asp:ListItem>On Premise</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtHostName" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Host Name
                <asp:RequiredFieldValidator ID="rfvgHostName" runat="server"
                    ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtHostName" TabIndex="2" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="64"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div5" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=Manufacture&ISForward=0&elemrntId=txtManufacture" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Manufacture
                <asp:RequiredFieldValidator ID="rfvManufacture" runat="server"
                    ControlToValidate="txtManufacture" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtManufacture" TabIndex="3" class="watermark" placeholder="Manufacture" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="40"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div6" class=" actionPanel  divIframeOperations" runat="server">
                        <div style="position: relative; width: 17px; display: inline-block;">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=ModelID&ISForward=1&elemrntId=ddlModel" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System Models&iTitle=Model&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemModelMaster"></a></span>
                        <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System Models&iTitle=Model&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemModelMaster"></a></span>
                        <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System Models&iTitle=Model&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemModelMaster"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Model
                <asp:RequiredFieldValidator ID="rfvModel" runat="server"
                    ControlToValidate="ddlModel" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlModel" TabIndex="4" ClientIDMode="Static" runat="server" class="selector"></asp:DropDownList>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div7" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=Memory&ISForward=0&elemrntId=txtMemory" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Memory
                <asp:RequiredFieldValidator ID="rfvMemory" runat="server"
                    ControlToValidate="txtMemory" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtMemory" TabIndex="5" class="watermark" placeholder="Memory" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div8" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=SerialNumber&ISForward=0&elemrntId=txtSerialNo" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Serial Number
                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server"
                    ControlToValidate="txtSerialNo" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSerialNo" TabIndex="6" class="watermark" placeholder="Serial Number" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="12"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div9" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=InstalledOn&ISForward=0&elemrntId=txtInstalledDate" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Installed On
                <asp:RequiredFieldValidator ID="rfvInstalledDate" runat="server"
                    ControlToValidate="txtInstalledDate" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtInstalledDate" TabIndex="7" class="watermark installedDate" placeholder="Installed On"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div10" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=WarrantyExpiresOn&ISForward=0&elemrntId=txtWarrantyExpires" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Warranty Expires On
                <asp:RequiredFieldValidator ID="rfvWarrantyExpires" runat="server"
                    ControlToValidate="txtWarrantyExpires" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtWarrantyExpires" TabIndex="8" class="watermark expiryDate" placeholder="Warranty Expires On"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div11" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=IPAddress&ISForward=0&elemrntId=txtIPAddress" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        IP Address
                <asp:RequiredFieldValidator ID="rfvIPAddress" runat="server"
                    ControlToValidate="txtIPAddress" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtIPAddress" TabIndex="9" class="watermark ipaddress" placeholder="IP Address"
                    runat="server" MaxLength="15"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div12" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=Subnet&ISForward=0&elemrntId=txtSubnet" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Subnet
                <asp:RequiredFieldValidator ID="rfvSubnet" runat="server"
                    ControlToValidate="txtSubnet" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSubnet" TabIndex="10" class="watermark ipaddress" placeholder="Subnet"
                    runat="server" MaxLength="15"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div13" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=Gateway&ISForward=0&elemrntId=txtGateway" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Gateway
                <asp:RequiredFieldValidator ID="rfvGateway" runat="server"
                    ControlToValidate="txtGateway" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtGateway" TabIndex="11" class="watermark ipaddress" placeholder="Gateway"
                    runat="server" MaxLength="15"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div14" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=AdminUserName&ISForward=0&elemrntId=txtAdminUsername" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Admin Username
                <asp:RequiredFieldValidator ID="rfvAdminUsername" runat="server"
                    ControlToValidate="txtAdminUsername" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtAdminUsername" TabIndex="12" class="watermark Username" placeholder="Admin Username" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div15" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=AdminPassword&ISForward=0&elemrntId=txtPassword" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Password
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtPassword" TabIndex="13" class="watermark" placeholder="Password"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div1" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=OSVersionID&ISForward=1&elemrntId=ddlOSVersion" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                        <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System OS versions&iTitle=OS version&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemOSversion"></a></span>
                        <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System OS versions&iTitle=OS version&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemOSversion"></a></span>
                        <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System OS versions&iTitle=OS version&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemOSversion"></a></span>
                    </div>
                    <%} %>
                    <label>
                        OS Version
                <asp:RequiredFieldValidator ID="rfvOSVersion" runat="server"
                    ControlToValidate="ddlOSVersion" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlOSVersion" TabIndex="14" ClientIDMode="Static" runat="server" class="selector"></asp:DropDownList>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div17" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=PhoneSystems&HistoryFieldName=Firmware&ISForward=0&elemrntId=txtFirmware" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Firmware
                <asp:RequiredFieldValidator ID="rfvFirmware" runat="server"
                    ControlToValidate="txtFirmware" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtFirmware" TabIndex="15" class="watermark" placeholder="Firmware" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="50"></asp:TextBox>
            </div>

            <div class="clear"></div>
            <div class="inlineProperty" id="inlineInterface" runat="server">
                <label>
                    Interfaces
                </label>
                <div class="keywords">
                    <span class="field">
                        <asp:TextBox Text="" ID="txtInterfaces" TabIndex="16" class="watermark multiText" placeholder="Interfaces" MaxLength="500"
                            runat="server"></asp:TextBox>
                    </span>
                </div>
            </div>
            <div class="inlineProperty firstColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div2" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System Modules&iTitle=Module&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemModules"></a></span>
                        <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System Modules&iTitle=Module&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemModules"></a></span>
                        <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Phone System Modules&iTitle=Module&isColorBox=yes" style="color: blue;" class="iframe PhoneSystemModules"></a></span>
                    </div>
                    <%} %>
                    <label>Modules</label>
                </div>
                <asp:DropDownList ID="ddlModules" runat="server" TabIndex="17" class="chosen-select Modules" Width="100%" multiple data-placeholder="Select Modules"></asp:DropDownList>
                <asp:HiddenField ID="hidModuleID" runat="server" ClientIDMode="Static" />
            </div>
            <div class="clear"></div>
            <div class="inlineProperty secondColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div3" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe UserInfo"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe UserInfo"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe UserInfo"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Assigned Users
                    </label>
                </div>
                <asp:DropDownList ID="ddlAUsers" runat="server" TabIndex="18" class="chosen-select Users" Width="100%" multiple data-placeholder="Select Users" ClientIDMode="Static"></asp:DropDownList>
                <asp:HiddenField ID="hidAssignedUserID" runat="server" ClientIDMode="Static" />
            </div>
            <div class="inlineProperty" id="inlineNotes" runat="server">
                <label>
                    Notes
                </label>
                <div class="keywords">
                    <span class="field">
                        <asp:TextBox Text="" ID="txtNotes" TabIndex="19" class="watermark multiText" placeholder="Notes" MaxLength="2000"
                            runat="server"></asp:TextBox>
                    </span>
                </div>
            </div>
            <div class="clear"></div>
              <% if (CurrentAction != ProvisioningTool.Entity.ActionType.MoreView)
               { %>
            <div class="inlineProperty uploadConfig">
                <label>Upload Configuration</label>
                <asp:FileUpload ID="fileUpload" runat="server" class="upload valTextbox" ClientIDMode="Static" onchange="this.style.width = '100%';" />
            </div>

            <div class="inlineProperty"></div>
            <%} %>
            <div class="inlineProperty">
                <label>View Uploaded Configuration</label>
                <asp:LinkButton ID="DwnldLink" runat="server" ClientIDMode="Static" OnClick="DwnldLink_Click" ForeColor="Blue">Download uploaded configuration</asp:LinkButton>
                <asp:HiddenField ID="HiddenFieldDwnldLink" runat="server" ClientIDMode="Static" />
                <asp:Label runat="server" ID="noimg"></asp:Label>
            </div>
            <div class="clear"></div>
            <div>
                <asp:Button ID="btnSubmit" CssClass="actionBtn" TabIndex="20" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" ClientIDMode="Static" />
                <asp:Button ID="btnBack" CssClass="actionBtn" TabIndex="21" runat="server" Text="Back" OnClick="btnBack_Click" ClientIDMode="Static"/>
            </div>
        </div>
    </div>

    <div id="divGrdPhoneInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center;">
            <table id="grdPhoneInfo"></table>
            <div id="grdPhoneInfopager"></div>
        </div>
    </div>
</div>

