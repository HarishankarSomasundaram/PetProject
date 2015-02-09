<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RouterInfo.ascx.cs" Inherits="UserControlsRouterInfo" %>
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

    var gridWidth = ""
    var gridName = "#grdRouterInfo";
    var gridPager = "#grdRouterInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "230"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "RouterList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time
        var colname = ["RouterID", "Host Name", "Manufacture", "Model", "Memory", "OS", "Firmware", "Interfaces", "Installed On"];

        var colmodel = [
                           { name: 'RouterID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'Hostname', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Manufacture', sortable: true, hidden: false, editable: true, search: false },
                           { name: 'RouterModel.MasterValue', width: 100, sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'Memory', sortable: false, hidden: false, align: "center", editable: false, search: false },
                           { name: 'OSVersion.MasterValue', sortable: false, align: "left", hidden: false, editable: false, search: false },
                           { name: 'Firmware', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'RouterInterfaces', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'InstalledOn', sortable: true, align: "left", hidden: false, editable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } }
                           //{ name: 'View', width: 40, sortable: true, align: "left", hidden: false, hidedlg: true, editable: true, search: false }

        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLROUTERS/" + caption + "/" + siteID + "/St";

        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEROUTERBYROUTERID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEROUTERBYROUTERID";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "HardwareSettings.aspx?navPage=Routers&do=a&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Routers"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Routers&do=e&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Routers&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Routers&do=m&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Routers&id="; //View
        }
        else {
            AddUrl = "HardwareSettings.aspx?navPage=Routers&do=a&nav=Routers"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Routers&do=e&nav=Routers&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Routers&do=m&nav=Routers&id="; //View URL
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
                                        ViewUrl //view URL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        $('#grdRouterInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Routers"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();
        $("#del_grdRouterInfo").insertAfter(".ui-pg-button:nth(3)");

        $('#txtsitePass').keypress(function (e) {
            e.preventDefault();
        });

        $('#txtsitePass').on("cut copy paste", function (e) {
            e.preventDefault();
        });
        if (getQueryStringByName("do") != "m") {
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
        $("#includes_usercontrols_pages_routerinfo_ascx_txtInterfaces_tag").attr("tabindex", "16");
        $("#includes_usercontrols_pages_routerinfo_ascx_txtNotes_tag").attr("tabindex", "20");

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
<div id="provUser" style="padding-top: 10px; padding-bottom: 10px; margin-left: 10px; display: none;" class="provUser">
    Enter new device information <a style="text-decoration: underline; color: blue;" target="_self" href="#" id="existingUser">Copy from an existing Device</a>
    <div id="divExistingUser" class="inlineProperty" style="display: none;">
         <asp:DropDownList ID="ddldeviceList" TabIndex="9" runat="server" class="selector" ClientIDMode="Static" ></asp:DropDownList>
         <asp:Button ID="btnFill" CssClass="actionBtn" runat="server" Text="Copy" OnClick="btnFill_Click" Style="float:left;margin-left:10px;" />
         <asp:HiddenField ID="HiddenColorBox" runat="server" ClientIDMode="Static" Value="0" />
    </div>
</div>

<div class="innerTabContent">
    <p class="divMessage" style="text-align: center; margin: 0" id="divMessage" runat="server">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
    </p>
    <div id="provClose" runat="server" class="provClose">
        <a style="text-decoration: underline; color: blue;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Routers&provUser=1&iframe=1&iframedo=a&isColorBox=yes" id="addDevice"> Add Another Device</a> |
        <a style="text-decoration: underline; color: blue;" target="_self" href="javascript:parent.$.fn.colorbox.close();" id="closeDiv">Close</a>
    </div>

    <div id="CrudRouter" runat="server" class="siteDetail">
        <div class="contentDetail" id="DivRouterDetail" runat="server" style="padding-top: 0px; margin-left: 10px">
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div13" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtHostName" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Host Name
                <asp:RequiredFieldValidator ID="rfgHostName" runat="server"
                    ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtHostName" TabIndex="1" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="64"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=Manufacture&ISForward=0&elemrntId=txtManufacture" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Manufacture
                <asp:RequiredFieldValidator ID="rfvManufacture" runat="server"
                    ControlToValidate="txtManufacture" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtManufacture" TabIndex="2" class="watermark" placeholder="Manufacture" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="40"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div1" class=" actionPanel divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=ModelID&ISForward=1&elemrntId=ddlModel" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router Models&iTitle=Router Model&isColorBox=yes" style="color: blue;" class="iframe RIModel"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router Models&iTitle=Router Model&isColorBox=yes" style="color: blue;" class="iframe RIModel"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router Models&iTitle=Router Model&isColorBox=yes" style="color: blue;" class="iframe RIModel"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Model
                <asp:RequiredFieldValidator ID="rfvModel" runat="server"
                    ControlToValidate="ddlModel" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlModel" TabIndex="3" runat="server" class="selector"></asp:DropDownList>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div5" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=Memory&ISForward=0&elemrntId=txtMemory" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Memory 
                <asp:RequiredFieldValidator ID="rfvMemory" runat="server"
                    ControlToValidate="txtMemory" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtMemory" TabIndex="4" class="watermark" placeholder="Memory" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div6" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=SerialNumber&ISForward=0&elemrntId=txtSerialNo" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Serial Number
                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server"
                    ControlToValidate="txtSerialNo" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSerialNo" TabIndex="5" class="watermark" placeholder="Serial Number" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="12"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div7" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=InstalledOn&ISForward=0&elemrntId=txtInstalledDate" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Installed On
                <asp:RequiredFieldValidator ID="rfvInstalledDate" runat="server"
                    ControlToValidate="txtInstalledDate" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox ID="txtInstalledDate" TabIndex="6" Text="" class="watermark installedDate" placeholder="Installed On"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div8" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=WarrantyExpiresOn&ISForward=0&elemrntId=txtWarrantyExpires" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Warranty Expires On
                <asp:RequiredFieldValidator ID="rfvWarrantyExpires" runat="server"
                    ControlToValidate="txtWarrantyExpires" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox ID="txtWarrantyExpires" TabIndex="7" Text="" class="watermark expiryDate" placeholder="Warranty Expires"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div9" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=IPAddress&ISForward=0&elemrntId=txtIPAddress" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        IP Address
                <asp:RequiredFieldValidator ID="rfvIPAddress" runat="server"
                    ControlToValidate="txtIPAddress" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtIPAddress" TabIndex="8" class="watermark ipaddress" placeholder="IP Address"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div10" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=Subnet&ISForward=0&elemrntId=txtSubnet" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Subnet
                <asp:RequiredFieldValidator ID="rfvSubnet" runat="server"
                    ControlToValidate="txtSubnet" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSubnet" TabIndex="9" class="watermark ipaddress" placeholder="Subnet"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div11" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=Gateway&ISForward=0&elemrntId=txtGateway" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Gateway
                <asp:RequiredFieldValidator ID="rfvGateway" runat="server"
                    ControlToValidate="txtGateway" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtGateway" TabIndex="10" class="watermark ipaddress" placeholder="Gateway"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div12" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=AdminUserName&ISForward=0&elemrntId=txtAdminUsername" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Admin Username
                <asp:RequiredFieldValidator ID="rfvAdminUsername" runat="server"
                    ControlToValidate="txtAdminUsername" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtAdminUsername" TabIndex="11" class="watermark Username" placeholder="Admin Username" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div14" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=AdminPassword&ISForward=0&elemrntId=txtPassword" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Password
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtPassword" TabIndex="12" class="watermark" placeholder="Password"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div3" class=" actionPanel divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=OSVersionID&ISForward=1&elemrntId=ddlOSVersion" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router OS versions&iTitle=OS Version&isColorBox=yes" style="color: blue;" class="iframe RIOSVersion"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router OS versions&iTitle=OS Version&isColorBox=yes" style="color: blue;" class="iframe RIOSVersion"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router OS versions&iTitle=OS Version&isColorBox=yes" style="color: blue;" class="iframe RIOSVersion"></a></span>
                    </div>
                    <%} %>
                    <label>
                        OS Version
                <asp:RequiredFieldValidator ID="rfvOSVersion" runat="server"
                    ControlToValidate="ddlOSVersion" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlOSVersion" TabIndex="13" runat="server" class="selector"></asp:DropDownList>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div15" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Routers&HistoryFieldName=Firmware&ISForward=0&elemrntId=txtFirmware" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Firmware
                <asp:RequiredFieldValidator ID="rfvFirmware" runat="server"
                    ControlToValidate="txtFirmware" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtFirmware" TabIndex="14" class="watermark" placeholder="Firmware" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="50"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div2" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router Modules&iTitle=Router Module&isColorBox=yes" style="color: blue;" class="iframe RIModules"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router Modules&iTitle=Router Module&isColorBox=yes" style="color: blue;" class="iframe RIModules"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Router Modules&iTitle=Router Module&isColorBox=yes" style="color: blue;" class="iframe RIModules"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Modules
                    </label>
                </div>
                <asp:DropDownList ID="ddlModules" TabIndex="15" runat="server" class="chosen-select-width Modules" Width="100%" multiple data-placeholder="Select Modules"></asp:DropDownList>
                <asp:HiddenField ID="hidModuleID" runat="server" ClientIDMode="Static" />
            </div>
            <div class="clear"></div>
            <div class="inlineProperty">
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
            <div class="clear"></div>

            <% if (CurrentAction != ProvisioningTool.Entity.ActionType.MoreView)
               { %>
            <div class="inlineProperty">
                <label>Site to Site</label>
                <asp:TextBox Text="" ID="txtSitetoSite" class="watermark" TabIndex="17" placeholder="Site to Site" ClientIDMode="Static" data-validation="alphanumeric" data-validation-allowing="-+()_ " data-validation-optional="true"
                    runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <label>Password/Key</label>
                <asp:TextBox Text="" ID="txtKey" class="watermark" TabIndex="18" placeholder="Password/Key" ClientIDMode="Static"
                    runat="server" MaxLength="32"></asp:TextBox>
            </div>
            <asp:Button ID="btnAddR" CssClass="actionBtn" runat="server" TabIndex="19" Text="Add" Style="margin-top: 25px" />
            <div class="clear"></div>
            <%} %>
            <div class="inlineProperty">
                <label>
                    Site to Site & Password/Key
                </label>
                <div class="keywords">
                    <span class="field">
                        <asp:TextBox Text="" ID="txtsitePass" class="watermark multiText" placeholder="Notes" ClientIDMode="Static"
                            runat="server"></asp:TextBox>
                    </span>
                </div>
            </div>

            <div class="clear"></div>
            <div class="inlineProperty" id="inlineNotes" runat="server">
                <label>
                    Notes
                </label>

                <div class="keywords">
                    <span class="field">
                        <asp:TextBox Text="" ID="txtNotes" TabIndex="20" class="watermark multiText" placeholder="Notes" MaxLength="2000"
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
                <asp:Label runat="server" ID="noimg"></asp:Label>
            </div>

            <div class="clear"></div>
            <asp:Button ID="btnSubmit" CssClass="actionBtn" TabIndex="21" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" />
            <asp:Button ID="btnBack" CssClass="actionBtn" TabIndex="22" runat="server" Text="Back" OnClick="btnBack_Click" ClientIDMode="Static" />
        </div>

    </div>

    <div id="divGrdRouterInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
            <table id="grdRouterInfo"></table>
            <div id="grdRouterInfopager"></div>
        </div>
    </div>
</div>
