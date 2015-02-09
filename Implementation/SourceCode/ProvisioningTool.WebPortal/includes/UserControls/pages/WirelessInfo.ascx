<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WirelessInfo.ascx.cs" Inherits="UserControlsWirelessInfo" %>

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
    var gridName = "#grdWirelessInfo";
    var gridPager = "#grdWirelessInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "WirelessList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time

        var colname = ["WirelessID", "Host Name", "Manufacture", "Model", "SSID", "Authentication", "Encryption", "Installed On"];

        var colmodel = [
                           { name: 'WirelessID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: false, hidedlg: true },
                           { name: 'Hostname', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'WirelessManufacture.MasterValue', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'WirelessModel.MasterValue', sortable: false, align: "left", hidden: false, editable: false, search: true },
                           { name: 'SSID', sortable: true, hidedlg: true, hidden: true, editable: false, search: true },
                           { name: 'Authentication', sortable: false, align: "left", hidden: false, editable: false, search: true },
                           { name: 'WirelessEncryption', sortable: false, align: "left", hidden: false, editable: false, search: true },
                           { name: 'InstalledOn', sortable: true, align: "left", hidden: false, editable: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidden: false, editable: false, search: false },
        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLWIRELESSES/" + caption + "/" + siteID + "/" + searchFilter;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEWIRELESSBYWIRELESSID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEWIRELESSBYWIRELESSID";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "CustomerInfo.aspx?navPage=Wireless&do=a&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Wireless"; // Add URL
            EditUrl = "CustomerInfo.aspx?navPage=Wireless&do=e&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Wireless&id="; //Edit URL
            ViewUrl = "CustomerInfo.aspx?navPage=Wireless&do=m&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Wireless&id="; //View URL
        }
        else {
            AddUrl = "CustomerInfo.aspx?navPage=Wireless&do=a&nav=Wireless"; // Add URL
            EditUrl = "CustomerInfo.aspx?navPage=Wireless&do=e&nav=Wireless&id="; //Edit URL
            ViewUrl = "CustomerInfo.aspx?navPage=Wireless&do=m&nav=Wireless&id="; //View URL
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
                                        deleteWebServiceURL,
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for view
                                        ViewUrl //View URL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        $('#grdWirelessInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Wireless"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();
        //Date range Validaiton 
        //$(".installedDate").datepicker({ minDate: 0, maxDate: "+1M +10D" });
        //$(".expiryDate").datepicker({ minDate: -20, maxDate: "+1M +10D" });

        $("#del_grdWirelessInfo").insertAfter(".ui-pg-button:nth(3)");

        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }

        $("#txtNotes_tag").attr("tabindex", "16");

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
    <div id="divExistingUser" class="inlineProperty" style="display: none; width: auto;">
        <asp:DropDownList ID="ddldeviceList" TabIndex="9" runat="server" class="selector" ClientIDMode="Static" AutoPostBack="false"></asp:DropDownList>
        <asp:Button ID="btnFill" CssClass="actionBtn" runat="server" Text="Copy" OnClick="btnFill_Click" Style="float: left; margin-left: 10px;" />
    </div>
    <asp:HiddenField ID="HiddenColorBox" runat="server" ClientIDMode="Static" Value="0" />
</div>

<div>
    <div class="innerTabContent">
        <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
            <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
            <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
        </p>

        <div id="provClose" class="provClose" runat="server">
            <a style="text-decoration: underline; color: blue;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Wireless&provUser=1&iframe=1&opp=S&iframedo=a&isColorBox=yes" id="addDevice">Add Another Device</a> |
            <a style="text-decoration: underline; color: blue;" target="_self" href="javascript:parent.$.fn.colorbox.close();" id="closeDiv">Close</a>
        </div>

        <div id="CrudWireless" runat="server" class="siteDetail" style="padding-top: 0px; margin-left: 10px">

            <div id="divWirelessDetail" runat="server" class="contentDetail scrollabow" name="top" style="height: 400px;">
                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtHostName" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Host Name
                          <asp:RequiredFieldValidator ID="rfgHostName" runat="server"
                              ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                              ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtHostName" TabIndex="1" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>

                </div>
                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div1" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=WirelessModelID&ISForward=1&elemrntId=ddlModel" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Wireless Models&iTitle=Wireless Model&isColorBox=yes" style="color: blue;" class="iframe WirelessModel"></a></span>
                            <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Wireless Models&iTitle=Wireless Model&isColorBox=yes" style="color: blue;" class="iframe WirelessModel"></a></span>
                            <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Wireless Models&iTitle=Wireless Model&isColorBox=yes" style="color: blue;" class="iframe WirelessModel"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Model
                          <asp:RequiredFieldValidator ID="rfvModel" runat="server"
                              ControlToValidate="ddlModel" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                              ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlModel" TabIndex="2" runat="server" ClientIDMode="Static" class="selector"></asp:DropDownList>
                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div2" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=SerialNumber&ISForward=0&elemrntId=txtSerialNo" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Serial Number
                        <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server"
                            ControlToValidate="txtSerialNo" Display="Dynamic" ErrorMessage="*" InitialValue=""
                            ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtSerialNo" TabIndex="3" class="watermark" placeholder="Serial Number" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="12"></asp:TextBox>

                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div3" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=InstalledOn&ISForward=0&elemrntId=txtInstalledDate" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Installed On
                         <asp:RequiredFieldValidator ID="rfvInstalledDate" runat="server"
                             ControlToValidate="txtInstalledDate" Display="Dynamic" ErrorMessage="*" InitialValue=""
                             ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtInstalledDate" TabIndex="4" class="watermark installedDate" placeholder="Installed On"
                        runat="server" MaxLength="10"></asp:TextBox>

                </div>

                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div5" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=WirelessDeviceTypeID&ISForward=0&elemrntId=txtType" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Device Type
                         <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtType" runat="server"
                             ControlToValidate="txtType" Display="Dynamic" ErrorMessage="*" InitialValue=""
                             ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtType" TabIndex="5" class="watermark" placeholder="Type" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>

                </div>
                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div6" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=WirelessManufactureID&ISForward=1&elemrntId=ddlManufacture" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Wireless Manufacturers&iTitle=Wireless Manufacturers&isColorBox=yes" style="color: blue;" class="iframe WirelessManufacture"></a></span>
                            <span class="editSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Wireless Manufacturers&iTitle=Wireless Manufacturers&isColorBox=yes" style="color: blue;" class="iframe WirelessManufacture"></a></span>
                            <span class="closeSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Wireless Manufacturers&iTitle=Wireless Manufacturers&isColorBox=yes" style="color: blue;" class="iframe WirelessManufacture"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Manufacture
                          <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlManufacture" runat="server" CssClass="requiredField"
                              ControlToValidate="ddlManufacture" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                              ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlManufacture" TabIndex="6" runat="server" ClientIDMode="Static" class="selector"></asp:DropDownList>
                </div>
                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div7" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=WarrantyExpiresOn&ISForward=0&elemrntId=txtWarrantyExpires" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Warranty Expires On
                           <asp:RequiredFieldValidator ID="rfvWarrantyExpires" runat="server"
                               ControlToValidate="txtWarrantyExpires" Display="Dynamic" ErrorMessage="*" InitialValue=""
                               ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtWarrantyExpires" TabIndex="7" class="watermark  expiryDate" placeholder="Warranty Expires"
                        runat="server" MaxLength="10"></asp:TextBox>

                </div>
                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div8" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=IPAddress&ISForward=0&elemrntId=txtIPAddress" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <div id="Div30" class=" actionPanel  divIframeOperations" runat="server">
                            <asp:CheckBox runat="server" ID="chkDHCP" ClientIDMode="Static" />DHCP
                        </div>
                        <label>
                            IP Address
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                 ControlToValidate="txtIPAddress"
                                 ErrorMessage="Invalid IP Address"
                                 ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                 ValidationGroup="Req">
                             </asp:RegularExpressionValidator>
                            <%--   <asp:RequiredFieldValidator ID="rfvIPAddress" runat="server"
                              ControlToValidate="txtIPAddress" Display="Dynamic" ErrorMessage="*" InitialValue=""
                              ValidationGroup="Req">*</asp:RequiredFieldValidator>--%>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtIPAddress" TabIndex="8" class="watermark ipaddress" placeholder="IP Address"
                        runat="server" MaxLength="15" ClientIDMode="Static"></asp:TextBox>

                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div9" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=Subnet&ISForward=0&elemrntId=txtSubnet" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Subnet
                        <asp:RequiredFieldValidator ID="rfvSubnet" runat="server"
                            ControlToValidate="txtSubnet" Display="Dynamic" ErrorMessage="*" InitialValue=""
                            ValidationGroup="Req">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtSubnet"
                                ErrorMessage="Invalid Subnet"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="Req">
                            </asp:RegularExpressionValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtSubnet" TabIndex="9" class="watermark ipaddress" placeholder="Subnet"
                        runat="server" MaxLength="15"></asp:TextBox>

                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div10" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=Gateway&ISForward=0&elemrntId=txtGateway" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Gateway
                            <asp:RequiredFieldValidator ID="rfvGateway" runat="server"
                                ControlToValidate="txtGateway" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                ValidationGroup="Req">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="txtGateway"
                                ErrorMessage="Invalid Gateway"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="Req">
                            </asp:RegularExpressionValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtGateway" TabIndex="10" class="watermark ipaddress" placeholder="Gateway"
                        runat="server" MaxLength="15"></asp:TextBox>

                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div11" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=AdminUserName&ISForward=0&elemrntId=txtAdminUsername" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Admin Username
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtAdminUsername" TabIndex="11" class="watermark Username" placeholder="Admin Username" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="20"></asp:TextBox>

                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div12" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=AdminPassword&ISForward=0&elemrntId=txtPassword" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Password

                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtPassword" TabIndex="12" class="watermark" placeholder="Password"
                        runat="server" MaxLength="20"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div13" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=SSID&ISForward=0&elemrntId=txtSSID" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        SSID
                       
                    </label>
                    <asp:TextBox Text="" ID="txtSSID" class="watermark" TabIndex="13" placeholder="SSID" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>

                </div>

                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div14" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=Authentication&ISForward=0&elemrntId=txtAuthentication" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>Authentication</label>
                    </div>
                    <asp:TextBox Text="" ID="txtAuthentication" TabIndex="14" class="watermark" placeholder="Authentication" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div15" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Wirelesses&HistoryFieldName=WirelessEncryption&ISForward=0&elemrntId=txtEncryption" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>Encryption</label>
                    </div>
                    <asp:TextBox Text="" ID="txtEncryption" TabIndex="15" class="watermark" placeholder="Encryption" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>

                </div>
                <div class="clear"></div>

                <div class="inlineProperty" id="inlineNotes" runat="server">
                    <label>Notes</label>
                    <asp:TextBox Text="" ID="txtNotes" TabIndex="16" class="watermark multiText" placeholder="Notes" ClientIDMode="Static"
                        runat="server" MaxLength="2000"></asp:TextBox>
                </div>

            </div>
            <asp:Button ID="btnSubmit" CssClass="actionBtn" TabIndex="17" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" />
            <asp:Button ID="btnBack" CssClass="actionBtn" TabIndex="18" runat="server" Text="Back" OnClick="btnBack_Click" ClientIDMode="Static" />
        </div>

        <div id="divGrdWirelessInfo" runat="server" class="innerGrdFullWidth">
            <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%">
                <table id="grdWirelessInfo"></table>
                <div id="grdWirelessInfopager"></div>
            </div>
        </div>

    </div>
</div>


