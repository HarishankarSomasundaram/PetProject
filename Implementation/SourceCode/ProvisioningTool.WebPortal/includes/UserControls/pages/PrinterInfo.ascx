<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrinterInfo.ascx.cs" Inherits="UserControlsPrinterInfo" %>
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
    var gridName = "#grdPrinterInfo";
    var gridPager = "#grdPrinterInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "PrinterList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time
        var colname = ["PrinterID", "Host Name", "Manufacture", "Model", "O/S", "Firmware", "Interface", "Installed On"];

        var colmodel = [
                           { name: 'PrinterID', key: true, width: 100, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'Hostname', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Manufacture', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'PrinterModel.MasterValue', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'OSVersion.MasterValue', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Firmware', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'PrinterInterfaces', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'InstalledOn', width: 100, sortable: true, hidden: false, editable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } }
                           //{ name: 'View', width: 40, sortable: true, align: "left", hidden: false, editable: true, search: false },
        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLPRINTERS/" + caption + "/" + 0 + "/" + searchFilter;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEPrinterBYPrinterID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEPrinterBYPrinterID";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "HardwareSettings.aspx?navPage=Printers&do=a&isColorBox=yes&provisioning=" + provisioning + "&nav=Printers"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Printers&do=e&isColorBox=yes&provisioning=" + provisioning + "&nav=Printers&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Printers&do=m&isColorBox=yes&provisioning=" + provisioning + "&nav=Printers&id=";
        }
        else {
            AddUrl = "HardwareSettings.aspx?navPage=Printers&do=a&nav=Printers"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Printers&do=e&nav=Printers&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Printers&do=m&nav=Printers&id=";
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
        $('#grdPrinterInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Printers"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $("#del_grdPrinterInfo").insertAfter(".ui-pg-button:nth(3)");
        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
        $("#includes_usercontrols_pages_printerinfo_ascx_txtInterfaces_tag").attr("tabindex", "16");
        $("#txtNotes_tag").attr("tabindex", "17");

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
        <asp:DropDownList ID="ddldeviceList" TabIndex="9" runat="server" class="selector" ClientIDMode="Static"></asp:DropDownList>
        <asp:Button ID="btnFill" CssClass="actionBtn" runat="server" Text="Copy" OnClick="btnFill_Click" Style="float: left; margin-left: 10px;" />
    </div>
    <asp:HiddenField ID="HiddenColorBox" runat="server" ClientIDMode="Static" Value="0" />
</div>

<div class="innerTabContent">
    <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hidisIframe" runat="server" />
    </p>

    <div id="provClose" class="provClose" runat="server">
        <a style="text-decoration: underline; color: blue;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Printers&provUser=1&iframe=1&opp=S&iframedo=a&isColorBox=yes" id="addDevice">Add Another Device</a> |
        <a style="text-decoration: underline; color: blue;" target="_self" href="javascript:parent.$.fn.colorbox.close();" id="closeDiv">Close</a>
    </div>

    <div id="CrudPrinter" runat="server" class="siteDetail">
        <div id="divPrinterDetail" runat="server" class="contentDetail" style="padding-top: 0px; margin-left: 10px">
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div15" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtHostName" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Host Name
                <asp:RequiredFieldValidator ID="rfgHostName" runat="server"
                    ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" TabIndex="1" ID="txtHostName" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=Manufacture&ISForward=0&elemrntId=txtManufacture" style="color: blue;" class="TrackHistory"></a></span>
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
                        <div style="position: relative; width: 17px; display: inline-block;"><span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=ModelID&ISForward=1&elemrntId=ddlModel" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer Models&iTitle=Printer Models&isColorBox=yes" style="color: blue;" class="iframe PrinterModel"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer Models&iTitle=Printer Models&isColorBox=yes" style="color: blue;" class="iframe PrinterModel"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer Models&iTitle=Printer Models&isColorBox=yes" style="color: blue;" class="iframe PrinterModel"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Model
                <asp:RequiredFieldValidator ID="rfvModel" runat="server"
                    ControlToValidate="ddlModel" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList TabIndex="3" ID="ddlModel" runat="server" class="selector"></asp:DropDownList>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div5" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=SerialNumber&ISForward=0&elemrntId=txtSerialNo" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Serial Number
                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server"
                    ControlToValidate="txtSerialNo" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSerialNo" TabIndex="4" class="watermark" placeholder="Serial Number" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div6" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=InstalledOn&ISForward=0&elemrntId=txtInstalledDate" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Installed On
                <asp:RequiredFieldValidator ID="rfvInstalledDate" runat="server"
                    ControlToValidate="txtInstalledDate" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtInstalledDate" TabIndex="5" class="watermark installedDate" placeholder="Installed On"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div7" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=WarrantyExpiresOn&ISForward=0&elemrntId=txtWarrantyExpires" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Warranty Expires On
                <asp:RequiredFieldValidator ID="rfvWarrantyExpires" runat="server"
                    ControlToValidate="txtWarrantyExpires" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtWarrantyExpires" TabIndex="6" class="watermark expiryDate" placeholder="Warranty Expires"
                    runat="server" MaxLength="10"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div8" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=IPAddress&ISForward=0&elemrntId=txtIPAddress" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        IP Address
                <asp:RequiredFieldValidator ID="rfvIPAddress" runat="server"
                    ControlToValidate="txtIPAddress" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revIPAddress" runat="server"
                        ControlToValidate="txtIPAddress"
                        ErrorMessage="Invalid IP Address"
                        ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                        ValidationGroup="Req"></asp:RegularExpressionValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtIPAddress" TabIndex="7" class="watermark ipaddress" placeholder="IP Address"
                    runat="server" MaxLength="15"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                  {%>
                <div id="Div9" class=" actionPanel  divIframeOperations" runat="server">
                    <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=Subnet&ISForward=0&elemrntId=txtSubnet" style="color: blue;" class="TrackHistory"></a></span>
                    <div class="tooltip-popup"></div>
                </div>
                <%} %>
                <div class="clearfix">
                    <label>
                        Subnet
                <asp:RequiredFieldValidator ID="rfvSubnet" runat="server"
                    ControlToValidate="txtSubnet" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSubnet" runat="server"
                            ControlToValidate="txtSubnet"
                            ErrorMessage="Invalid Subnet"
                            ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                            ValidationGroup="Req"></asp:RegularExpressionValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSubnet" TabIndex="8" class="watermark ipaddress" placeholder="Subnet"
                    runat="server" MaxLength="15"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div10" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=Gateway&ISForward=0&elemrntId=txtGateway" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Gateway
                <asp:RequiredFieldValidator ID="rfvGateway" runat="server"
                    ControlToValidate="txtGateway" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revGateway" runat="server"
                            ControlToValidate="txtGateway"
                            ErrorMessage="Invalid Gateway"
                            ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                            ValidationGroup="Req">
                        </asp:RegularExpressionValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtGateway" TabIndex="9" class="watermark ipaddress" placeholder="Gateway"
                    runat="server" MaxLength="15"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div11" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=AdminUserName&ISForward=0&elemrntId=txtAdminUsername" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Admin Username
                <asp:RequiredFieldValidator ID="rfvAdminUsername" runat="server"
                    ControlToValidate="txtAdminUsername" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtAdminUsername" TabIndex="10" class="watermark Username" placeholder="Admin Username"
                    runat="server" MaxLength="20"></asp:TextBox>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div12" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=AdminPassword&ISForward=0&elemrntId=txtPassword" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Password
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtPassword" TabIndex="11" class="watermark" placeholder="Password"
                    runat="server" MaxLength="40"></asp:TextBox>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div2" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer Modules&iTitle=Printer Modules&isColorBox=yes" style="color: blue;" class="iframe PrinterModules"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer Modules&iTitle=Printer Modules&isColorBox=yes" style="color: blue;" class="iframe PrinterModules"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer Modules&iTitle=Printer Modules&isColorBox=yes" style="color: blue;" class="iframe PrinterModules"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Modules
                <asp:RequiredFieldValidator ID="rfvModules" runat="server"
                    ControlToValidate="ddlModules" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlModules" runat="server" TabIndex="12" class="chosen-select-width Modules" Width="100%" multiple data-placeholder="Select Modules"></asp:DropDownList>
                <asp:HiddenField ID="hidModuleID" runat="server" ClientIDMode="Static" />
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div18" class=" actionPanel divIframeOperations" runat="server">
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Users&iframe=1&iTitle=Users&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe PrinterAUsers"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&iframe=1&iTitle=Users&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe PrinterAUsers"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&opp=SH&iTitle=Users&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe PrinterAUsers"></a></span>
                    </div>
                    <%} %>
                    <label>
                        Assigned Users
                    </label>
                </div>
                <asp:DropDownList ID="ddlAssignedUsers" runat="server" TabIndex="13" class="chosen-select-width AssignedUsers" Width="100%" multiple data-placeholder="Select Assigned Users"></asp:DropDownList>

                <asp:HiddenField ID="hidAssignedUsers" runat="server" ClientIDMode="Static" />
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div3" class=" actionPanel divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=OSVersion&ISForward=1&elemrntId=ddlOSVersion" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                        <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer OS Versions&iTitle=OS Versions&isColorBox=yes" style="color: blue;" class="iframe PrinterOSVersion"></a></span>
                        <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer OS Versions&iTitle=OS Versions&isColorBox=yes" style="color: blue;" class="iframe PrinterOSVersion"></a></span>
                        <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Printer OS Versions&iTitle=OS Versions&isColorBox=yes" style="color: blue;" class="iframe PrinterOSVersion"></a></span>
                    </div>
                    <%} %>
                    <label>
                        OS Version
                <asp:RequiredFieldValidator ID="rfvOSVersion" runat="server"
                    ControlToValidate="ddlOSVersion" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlOSVersion" runat="server" TabIndex="14" class="selector"></asp:DropDownList>
            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div13" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Printers&HistoryFieldName=Firmware&ISForward=0&elemrntId=txtFirmware" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Firmware
                <asp:RequiredFieldValidator ID="rfvFirmware" runat="server"
                    ControlToValidate="txtFirmware" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtFirmware" class="watermark" TabIndex="15" placeholder="Firmware" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="clear"></div>

            <div class="inlineProperty" id="inlineInterface" runat="server">
                <div class="clearfix">
                    <label>
                        Interfaces
                    </label>
                </div>

                <asp:TextBox Text="" ID="txtInterfaces" TabIndex="16" class="watermark multiText" placeholder="Interfaces"
                    runat="server" MaxLength="500"></asp:TextBox>
            </div>

            <div class="inlineProperty" id="inlineNotes" runat="server">
                <label>
                    Notes
                </label>

                <asp:TextBox Text="" ID="txtNotes" TabIndex="17" class="watermark multiText" placeholder="Notes" ClientIDMode="Static"
                    runat="server" MaxLength="2000"></asp:TextBox>
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

            <asp:Button ID="btnPrinterSubmit" CssClass="actionBtn" runat="server" Text="Submit" TabIndex="18" ValidationGroup="Req" OnClick="btnPrinterSubmit_Click" />
            <asp:Button ID="btnPrinterBack" CssClass="actionBtn" runat="server" Text="Back" TabIndex="19" OnClick="btnPrinterBack_Click" ClientIDMode="Static" />

        </div>
    </div>
    <div id="divGrdPrinterInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
            <table id="grdPrinterInfo"></table>
            <div id="grdPrinterInfopager"></div>
        </div>
    </div>
</div>

