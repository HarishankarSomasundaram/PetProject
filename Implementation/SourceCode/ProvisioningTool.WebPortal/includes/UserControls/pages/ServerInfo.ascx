<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServerInfo.ascx.cs" Inherits="UserControlsServerInfo" %>
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
    //server info
    var gridName = "#grdServerInfo";
    var gridPager = "#grdServerInfopager";
    // server hardware info
    var gridSHName = "#grdServerHardwareInfo";
    var gridSHPager = "#grdServerHardwareInfopager";

    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "ServerInfoList"
    var gridListSHName = "ServerHardwarList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time
        var colname = ["ServerID", "Host Name", "Model", "Manufacture", "Processor", "IP Address", "Serial Number", "Operating System", "Core", "Installed On"];
        //var colname = ["ServerID", "Host Name", ""];

        var colmodel = [
                           { name: 'ServerID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'HostName', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'ServerModelName', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Manufacturer', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'ProcessorName', sortable: true, hidedlg: true, hidden: false, editable: true, search: false },
                           { name: 'IPAddress', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'SerialNumber', sortable: false, hidden: false, align: "center", editable: false, search: false },
                           { name: 'OperationSystemName', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Core', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'InstalledDate', sortable: true, hidedlg: true, hidden: false, editable: true, search: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } }
                           //{ name: 'View', width: 40, sortable: true, align: "left", hidden: false, editable: true, search: false },

        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        //var getWebServiceURL = baseServiceURL + masterServiceName + "GetAllServer";
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLSERVERINFO/MaterName/" + siteID + "/" + searchFilter;

        //alert(getWebServiceURL);
        //var crudWebServiceURL = baseServiceURL + masterServiceName + "GlobalMasterCrud/" + caption;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETESERVERINFOBYSERVERINFOID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETESERVERINFOBYSERVERINFOID";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "CustomerInfo.aspx?navPage=Servers&do=a&isColorBox=yes&nav=Servers&opp=S"; // Add URL
            EditUrl = "CustomerInfo.aspx?navPage=Servers&do=e&isColorBox=yes&nav=Servers&opp=S&id="; //Edit URL
            ViewUrl = "CustomerInfo.aspx?navPage=Servers&do=m&isColorBox=yes&nav=Servers&opp=S&id=";
        }
        else {
            AddUrl = "CustomerInfo.aspx?navPage=Servers&do=a&nav=Servers&opp=S"; // Add URL
            EditUrl = "CustomerInfo.aspx?navPage=Servers&do=e&nav=Servers&opp=S&id="; //Edit URL
            ViewUrl = "CustomerInfo.aspx?navPage=Servers&do=m&nav=Servers&opp=S&id=";
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
        var iframeClick, sHostName;
        var _modelhref = $("#modelAdd").attr("href");

        $('#grdServerInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Servers"));
        $('#grdServerHardwareInfo').jqGrid('GridUnload');
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $('#lnkServerHardwware').css('display', 'none');

        var hostName = $("#txtHostName").val();

        if ($("#txtHostName").val() == '') {
            $('#lnkServerHardwware').css('display', 'none');
        }
        else {
            $('#lnkServerHardwware').css('display', '');

            $("#modelAdd").attr("href", _modelhref + '&HostName=' + hostName);
        }

        ////Fade out the message after 10 sec
        //$('#lblMessage1').fadeIn().delay(2000).fadeOut(function () {
        //    //This is for URL rewrite with out post back
        //    var stateObj = {};
        //    History.pushState(stateObj, "", "CustomerInfo.aspx");
        //    return false;
        //});

        $("#txtHostName").keyup(function () {
            if ($("#txtHostName").val() == '') {
                $('#lnkServerHardwware').css('display', 'none');
            }
            else {
                $('#lnkServerHardwware').css('display', '');
            }
            $("#modelAdd").attr("href", _modelhref + '&HostName=' + $(this).val());
        });


        $("#del_grdServerInfo").insertAfter(".ui-pg-button:nth(3)");
        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
        $("#includes_usercontrols_pages_serverinfo_ascx_txtSNotes_tag").attr("tabindex", "24");
        $("#includes_usercontrols_pages_serverinfo_ascx_txtNotes_tag").attr("tabindex", "18");

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

<div id="provUser"  class="provUser" style="padding-top: 10px; padding-bottom: 10px; margin-left: 10px; display: none;">
    Enter new device information <a style="text-decoration: underline; color: blue;" target="_self" href="#" id="existingUser">Copy from an existing Device</a>
    <div id="divExistingUser" class="inlineProperty" style="display: none;">
         <asp:DropDownList ID="ddldeviceList" TabIndex="9" runat="server" class="selector" ClientIDMode="Static"></asp:DropDownList>
         <asp:Button ID="btnFill" CssClass="actionBtn" runat="server" Text="Copy" OnClick="btnFill_Click" Style="float:left;margin-left:10px;" />
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
        <a style="text-decoration: underline; color: blue;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Servers&provUser=1&iframe=1&opp=S&iframedo=a&isColorBox=yes" id="addDevice"> Add Another Device</a> |
        <a style="text-decoration: underline; color: blue;" target="_self" href="javascript:parent.$.fn.colorbox.close();" id="closeDiv">Close</a>
    </div>

    <div id="CrudServer" runat="server" class="siteDetail">
        <div class="innerTabContent">
            <div class="contentDetail" style="padding-top: 0px; margin-left: 10px">
                <div class="clearfix" style="margin-bottom: 10px;">
                    <asp:LinkButton runat="server" ID="lnkServerHardwware" Text="Add Server Hardware" OnClick="lnkServerHardwware_Click"
                        ClientIDMode="Static" CssClass="AddHardware AddSHardware myLinkButton"></asp:LinkButton>

                    <asp:LinkButton runat="server" ID="lnkViewServerHardware" Text="View Server Hardware" ClientIDMode="Static" CssClass="ViewHardware ViewSHardware myLinkButton" OnClick="lnkViewServerHardware_Click"></asp:LinkButton>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div20" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtHostName" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Host Name
                    <asp:RequiredFieldValidator ID="rfgHostName" runat="server"
                        ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtHostName" TabIndex="1" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div21" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=ServerModelID&ISForward=0&elemrntId=ddlModel" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="tabActionAdd"><a id="modelAdd" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Servers&opp=SH&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe ServerHardware"></a></span>
                            <span class="tabActionEdit"><a id="modelEdit" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Servers&opp=SH&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe ServerHardware"></a></span>
                            <span class="tabActionClose"><a id="modelClose" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Servers&opp=SH&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe ServerHardware"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Model
                    <asp:RequiredFieldValidator ID="rfvModel" runat="server"
                        ControlToValidate="ddlSModel" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlSModel" TabIndex="2" runat="server" class="selector" ClientIDMode="Static"></asp:DropDownList>

                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div17" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=SerialNumber&ISForward=0&elemrntId=txtSerialNo" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Serial Number
                    <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server"
                        ControlToValidate="txtSerialNo" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtSerialNo" TabIndex="3" class="watermark" placeholder="Serial Number" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="12"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div22" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=InstalledDate&ISForward=0&elemrntId=txtInstalledDate" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Installed On
                    <asp:RequiredFieldValidator ID="rfvInstalledDate" runat="server"
                        ControlToValidate="txtInstalledDate" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtInstalledDate" TabIndex="4" class="watermark installedDate" placeholder="Installed On"
                        runat="server" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div23" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=IPAddress&ISForward=0&elemrntId=txtIPAddress" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <div id="Div34" class=" actionPanel  divIframeOperations" runat="server">
                            <asp:CheckBox runat="server" ID="chkDHCP" ClientIDMode="Static" />DHCP
                        </div>
                        <label>
                            IP Address
                    <%--asp:RequiredFieldValidator ID="rfvIPAddress" runat="server"
                        ControlToValidate="txtIPAddress" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="txtIPAddress"
                                ErrorMessage="Invalid IP Address"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="SReq">
                            </asp:RegularExpressionValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtIPAddress" TabIndex="5" class="watermark ipaddress" placeholder="IP Address"
                        runat="server" MaxLength="15" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div24" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=Subnet&ISForward=0&elemrntId=txtSubnet" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Subnet
                    <asp:RequiredFieldValidator ID="rfvSubnet" runat="server"
                        ControlToValidate="txtSubnet" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtSubnet"
                                ErrorMessage="Invalid Subnet"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="SReq">
                            </asp:RegularExpressionValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtSubnet" TabIndex="6" class="watermark ipaddress" placeholder="Subnet"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div25" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=Gateway&ISForward=0&elemrntId=txtGateway" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Gateway
                    <asp:RequiredFieldValidator ID="rfvGateWay" runat="server"
                        ControlToValidate="txtGateway" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtGateway"
                                ErrorMessage="Invalid Gateway"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="SReq">
                            </asp:RegularExpressionValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtGateway" TabIndex="7" class="watermark ipaddress" placeholder="Gateway"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div26" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=WarrantyExpires&ISForward=0&elemrntId=txtWEDate" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Warranty Expires Date
                    <asp:RequiredFieldValidator ID="rfvWEDate" runat="server"
                        ControlToValidate="txtWEDate" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtWEDate" TabIndex="8" class="watermark expiryDate" placeholder="Warranty Expires Date"
                        runat="server" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div27" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=AdminUserName&ISForward=0&elemrntId=txtAUName" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Admin User Name
                    <asp:RequiredFieldValidator ID="rfvAUName" runat="server"
                        ControlToValidate="txtAUName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtAUName" TabIndex="9" class="watermark Username" placeholder="Admin User Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="20"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div28" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=Password&ISForward=0&elemrntId=txtPassword" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Password
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtPassword" TabIndex="10" class="watermark" placeholder="Password"
                        runat="server" MaxLength="20"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <label>
                        Domain or WorkGroup
                    <asp:RequiredFieldValidator ID="rfvDomain" runat="server"
                        ControlToValidate="txtDomain" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>

                    <asp:TextBox Text="" ID="txtDomain" class="watermark" TabIndex="11" placeholder="Domain or WorkGroup" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>


                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div1" class=" actionPanel divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=OperatingSystemID&ISForward=1&elemrntId=ddlOS" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Operating Systems&iTitle=Operating Systems&isColorBox=yes" style="color: blue;" class="iframe OppSystem"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Operating Systems&iTitle=Operating Systems&isColorBox=yes" style="color: blue;" class="iframe OppSystem"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Operating Systems&iTitle=Operating Systems&isColorBox=yes" style="color: blue;" class="iframe OppSystem"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Operation System
                    <asp:RequiredFieldValidator ID="rfvOS" runat="server"
                        ControlToValidate="ddlOS" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlOS" runat="server" TabIndex="12" class="selector" ClientIDMode="Static"></asp:DropDownList>

                    <asp:HiddenField ID="hidmulddlOS" runat="server" />
                </div>

                <div class="inlineProperty ">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div35" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=OperatingSystemLicenseKey&ISForward=0&elemrntId=txtDomain" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        License Key 
                    <asp:RequiredFieldValidator ID="rfvLKOS" runat="server"
                        ControlToValidate="txtLKOS" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>

                    <asp:TextBox Text="" ID="txtLKOS" class="watermark" TabIndex="13" placeholder="License Key " data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>
                </div>

                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div2" class=" actionPanel divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=AntiVirusID&ISForward=1&elemrntId=txtDomain" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Antivirus&iTitle=Antivirus&isColorBox=yes" style="color: blue;" class="iframe AntiVirus"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Antivirus&iTitle=Antivirus&isColorBox=yes" style="color: blue;" class="iframe AntiVirus"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Antivirus&iTitle=Antivirus&isColorBox=yes" style="color: blue;" class="iframe AntiVirus"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Antivirus
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlAV" runat="server" TabIndex="14" class="selector" ClientIDMode="Static"></asp:DropDownList>

                    <asp:HiddenField ID="hidmulddlAV" runat="server" ClientIDMode="Static" />
                </div>

                <div class="inlineProperty ">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div36" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=AntiVirusLicenseKey&ISForward=0&elemrntId=txtDomain" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        License Key

                    </label>
                    <asp:TextBox Text="" ID="txtLKAV" class="watermark" TabIndex="15" placeholder="License Key " data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>
                </div>
                <div class="clear"></div>
                <% if (CurrentAction != ProvisioningTool.Entity.ActionType.MoreView)
                   {%>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div3" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Backup Softwares&iTitle=Backup Software&isColorBox=yes" style="color: blue;" class="iframe BKApplication"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Backup Softwares&iTitle=Backup Software&isColorBox=yes" style="color: blue;" class="iframe BKApplication"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Backup Softwares&iTitle=Backup Software&isColorBox=yes" style="color: blue;" class="iframe BKApplication"></a></span>
                        </div>
                        <%} %>
                        <label>Backup Application</label>
                    </div>
                    <asp:DropDownList ID="ddlBA" runat="server" TabIndex="16" class="selector" ClientIDMode="Static"></asp:DropDownList>

                    <asp:HiddenField ID="hidmulddlBA" runat="server" ClientIDMode="Static" />
                </div>
                <div class="inlineProperty">
                    <label>License Key</label>
                    <asp:TextBox Text="" ID="txtLKBA" class="watermark" TabIndex="17" placeholder="Password/Key" ClientIDMode="Static" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>
                </div>

                <asp:Button ID="btnAdd" CssClass="actionBtn" runat="server" TabIndex="18" Text="Add" Style="margin-top: 25px" ClientIDMode="Static" />
                <%} %>
                <div class="clear"></div>
                <div class="inlineProperty">
                    <label>
                        Backup Application with License Key

                    </label>
                    <div class="keywords">
                        <span class="field">
                            <asp:TextBox Text="" ID="txtBAWLK" class="watermark multiText" placeholder="Notes" ClientIDMode="Static"
                                runat="server"></asp:TextBox>
                        </span>
                    </div>
                </div>
                <% if (CurrentAction != ProvisioningTool.Entity.ActionType.MoreView)
                   {%>
                <div class="clear"></div>
                <div class="inlineProperty">

                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div4" class=" actionPanel divIframeOperations" runat="server" clientidmode="Static">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Application Softwares&iTitle=Application Software&isColorBox=yes" style="color: blue;" class="iframe Application"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Application Softwares&iTitle=Application Software&isColorBox=yes" style="color: blue;" class="iframe Application"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Application Softwares&iTitle=Application Software&isColorBox=yes" style="color: blue;" class="iframe Application"></a></span>
                        </div>
                        <%} %>
                        <label>Application</label>
                    </div>
                    <asp:DropDownList ID="ddlApp" runat="server" TabIndex="19" class="selector"></asp:DropDownList>

                    <asp:HiddenField ID="hidmulddlApp" runat="server" ClientIDMode="Static" />
                </div>
                <div class="inlineProperty">
                    <label>License Key</label>
                    <asp:TextBox Text="" ID="txtLKApp" class="watermark" TabIndex="20" placeholder="Password/Key" ClientIDMode="Static" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>
                </div>

                <asp:Button ID="btnAddAPP" CssClass="actionBtn" runat="server" TabIndex="21" Text="Add" Style="margin-top: 25px" ClientIDMode="Static" />
                <div class="clear"></div>
                <%} %>
                <div class="inlineProperty">
                    <label>
                        Application with License Key
                    </label>
                    <div class="keywords">
                        <span class="field">
                            <asp:TextBox Text="" ID="txtLKWA" class="watermark multiText" placeholder="Notes" ClientIDMode="Static"
                                runat="server"></asp:TextBox>
                        </span>
                    </div>
                </div>
                <div class="clear"></div>

                <div class="inlineProperty firstColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div5" class=" actionPanel divIframeOperations" runat="server" clientidmode="Static">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Roles&iTitle=Role&isColorBox=yes" style="color: blue;" class="iframe SysRoles"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Roles&iTitle=Role&isColorBox=yes" style="color: blue;" class="iframe SysRoles"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Roles&iTitle=Role&isColorBox=yes" style="color: blue;" class="iframe SysRoles"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Server Roles
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlSRoles" runat="server" TabIndex="22" class="chosen-select-width ServerRole" multiple></asp:DropDownList>

                    <asp:HiddenField ID="hidmulddlSRoles" runat="server" ClientIDMode="Static" />
                </div>
                <div class="clear"></div>
                <div class="inlineProperty secondColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div18" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Users&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe UserInfo"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe UserInfo"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&opp=SH&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe UserInfo"></a></span>
                        </div>
                        <%} %>
                        <label>Assigned Users</label>
                    </div>
                    <asp:DropDownList ID="ddlAUsers" runat="server" TabIndex="23" class="chosen-select-width AUser" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlAUsers" runat="server" ClientIDMode="Static" />

                </div>

                <div class="clear"></div>
                <div class="inlineProperty firstColumn" id="inlineNotes" runat="server">
                    <label>Notes </label>
                    <div class="keywords">
                        <span class="field">
                            <asp:TextBox Text="" ID="txtSNotes" TabIndex="24" class="watermark multiText" placeholder="Notes" MaxLength="2000"
                                runat="server"></asp:TextBox>
                        </span>
                    </div>
                </div>
                <div class="clear"></div>
                <asp:Button ID="btnSSubmit" CssClass="actionBtn" TabIndex="25" runat="server" Text="Submit" ValidationGroup="SReq" OnClick="btnSSubmit_Click" />
                <asp:Button ID="btnSBack" CssClass="actionBtn" TabIndex="26" runat="server" Text="Back" OnClick="btnSBack_Click" ClientIDMode="Static" />
            </div>
        </div>
    </div>

    <div id="divGrdServerInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
            <table id="grdServerInfo"></table>
            <div id="grdServerInfopager"></div>
        </div>
    </div>

    <div id="divGrdServerHardwareInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
            <div class="inlineProperty">
                <asp:LinkButton runat="server" ID="lnkBackServerInfo" Text="" OnClick="lnkBack_Click" CssClass=""></asp:LinkButton>
            </div>
            <table id="grdServerHardwareInfo"></table>
            <div id="grdServerHardwareInfopager"></div>
        </div>
    </div>

    <div id="CrudServerHardware" runat="server" class="siteDetail">
        <div class="innerTabContent">

            <div class="contentDetail" style="padding-top: 0px; margin-left: 10px">
                <asp:LinkButton runat="server" ID="lnkBack" Text="" OnClick="lnkBack_Click"></asp:LinkButton>

                <div class="clear"></div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div29" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtServerHost" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Host Name
                    <asp:RequiredFieldValidator ID="rfvServerHost" runat="server"
                        ControlToValidate="txtServerHost" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtServerHost" TabIndex="1" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div30" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=Model&ISForward=0&elemrntId=txtModel" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Model Name
                    <asp:RequiredFieldValidator ID="rfvModelName" runat="server"
                        ControlToValidate="txtModel" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtModel" class="watermark" TabIndex="2" placeholder="Model Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div31" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=SerialNumber&ISForward=0&elemrntId=txtSHSer" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Serial Number / Service Tag
                    <asp:RequiredFieldValidator ID="rfvSHSer" runat="server"
                        ControlToValidate="txtSHSer" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtSHSer" class="watermark" TabIndex="3" placeholder="Serial Number / Service Tag" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="12"></asp:TextBox>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div32" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Servers&HistoryFieldName=Core&ISForward=0&elemrntId=txtCore" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Core
                    <asp:RequiredFieldValidator ID="rfvCore" runat="server"
                        ControlToValidate="txtCore" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtCore" TabIndex="4" class="watermark IntegerValidation" placeholder="Core"
                        runat="server" MaxLength="3"></asp:TextBox>
                </div>


                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div6" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=CPUs&iTitle=CPU&isColorBox=yes" style="color: blue;" class="iframe CPU"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=CPUs&iTitle=CPU&isColorBox=yes" style="color: blue;" class="iframe CPU"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=CPUs&iTitle=CPU&isColorBox=yes" style="color: blue;" class="iframe CPU"></a></span>
                        </div>
                        <%} %>
                        <label>
                            CPU
                    <asp:RequiredFieldValidator ID="rfvCPU" runat="server"
                        ControlToValidate="ddlCPU" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlCPU" runat="server" TabIndex="6" class="selector" ClientIDMode="Static"></asp:DropDownList>
                </div>
                <div class="clear"></div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div7" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=MotherBoards&iTitle=MotherBoard&isColorBox=yes" style="color: blue;" class="iframe Motherboard"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=MotherBoards&iTitle=MotherBoard&isColorBox=yes" style="color: blue;" class="iframe Motherboard"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=MotherBoards&iTitle=MotherBoard&isColorBox=yes" style="color: blue;" class="iframe Motherboard"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Motherboard
                    <asp:RequiredFieldValidator ID="rfvMotherboard" runat="server"
                        ControlToValidate="ddlMotherboard" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlMotherboard" runat="server" TabIndex="8" class="selector" ClientIDMode="Static"></asp:DropDownList>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div8" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Chipsets&iTitle=Chipset&isColorBox=yes" style="color: blue;" class="iframe Chipset"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Chipsets&iTitle=Chipset&isColorBox=yes" style="color: blue;" class="iframe Chipset"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Chipsets&iTitle=Chipset&isColorBox=yes" style="color: blue;" class="iframe Chipset"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Chipset
                    <asp:RequiredFieldValidator ID="rfvChipset" runat="server"
                        ControlToValidate="ddlChipset" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlChipset" runat="server" TabIndex="9" class="selector" ClientIDMode="Static"></asp:DropDownList>
                </div>


                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div15" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Chassis&iTitle=Chassis&isColorBox=yes" style="color: blue;" class="iframe Chasis"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Chassis&iTitle=Chassis&isColorBox=yes" style="color: blue;" class="iframe Chasis"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Chassis&iTitle=Chassis&isColorBox=yes" style="color: blue;" class="iframe Chasis"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Chasis
                    <asp:RequiredFieldValidator ID="rfvChasis" runat="server"
                        ControlToValidate="ddlChasis" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlChasis" runat="server" TabIndex="16" class="selector" ClientIDMode="Static"></asp:DropDownList>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <label>
                            Manufacture
                    <asp:RequiredFieldValidator ID="rfvManufacturer" runat="server"
                        ControlToValidate="txtManufacturer" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:TextBox Text="" ID="txtManufacturer" TabIndex="17" class="watermark" placeholder="Manufacture"
                        runat="server" MaxLength="40"></asp:TextBox>
                </div>

                <div class="clear"></div>
                <% if (CurrentAction != ProvisioningTool.Entity.ActionType.MoreView)
                   { %>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div9" class=" actionPanel divIframeOperations" runat="server" clientidmode="Static">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Memory&iTitle=Memory&isColorBox=yes" style="color: blue;" class="iframe Memory"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Memory&iTitle=Memory&isColorBox=yes" style="color: blue;" class="iframe Memory"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Memory&iTitle=Memory&isColorBox=yes" style="color: blue;" class="iframe Memory"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Memory
                    <asp:RequiredFieldValidator ID="rfvMemory" runat="server"
                        ControlToValidate="ddlMemory" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlMemory" ClientIDMode="Static" TabIndex="5" runat="server" class="chosen-select-width Memory"></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlMemory" runat="server" ClientIDMode="Static" />
                </div>
                <div class="inlineProperty">
                    <label>Quantity </label>
                    <asp:TextBox Text="" ID="txtMemoryQuanity" class="watermark" TabIndex="17" placeholder="Quantity" ClientIDMode="Static" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>
                </div>

                <asp:Button ID="btnMemoryAdd" CssClass="actionBtn" runat="server" TabIndex="18" Text="Add" Style="margin-top: 25px" ClientIDMode="Static" />
                <div class="clear"></div>
                <%} %>

                <div class="inlineProperty" id="Div33" runat="server">
                    <label>
                        Memory and No of Quantity
                        <asp:RequiredFieldValidator runat="server" ID="reqMemoryQuantiy" ControlToValidate="txtTotalMemoryQuantity"
                            ErrorMessage="*" ValidationGroup="Req" Display="Dynamic">*</asp:RequiredFieldValidator>
                    </label>
                    <div class="keywords">
                        <span class="field">
                            <asp:TextBox Text="" ID="txtTotalMemoryQuantity" class="watermark multiText" placeholder="Notes" ClientIDMode="Static"
                                runat="server"></asp:TextBox>

                        </span>
                    </div>
                </div>
                <div class="clear"></div>


                <div class="inlineProperty firstColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div10" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Multimedia&iTitle=Multimedia&isColorBox=yes" style="color: blue;" class="iframe Multimedia"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Multimedia&iTitle=Multimedia&isColorBox=yes" style="color: blue;" class="iframe Multimedia"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Multimedia&iTitle=Multimedia&isColorBox=yes" style="color: blue;" class="iframe Multimedia"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Multimedia
                    <asp:RequiredFieldValidator ID="rfvMultimedia" runat="server"
                        ControlToValidate="ddlMultimedia" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlMultimedia" TabIndex="10" runat="server" class="chosen-select-width Multimedia" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlMultimedia" runat="server" ClientIDMode="Static" />
                </div>

                <div class="inlineProperty secondColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div11" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Ports&iTitle=Port&isColorBox=yes" style="color: blue;" class="iframe Ports"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Ports&iTitle=Port&isColorBox=yes" style="color: blue;" class="iframe Ports"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Ports&iTitle=Port&isColorBox=yes" style="color: blue;" class="iframe Ports"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Ports
                    <asp:RequiredFieldValidator ID="rfvPorts" runat="server"
                        ControlToValidate="ddlPorts" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlPorts" runat="server" TabIndex="11" class="chosen-select-width Port" ClientIDMode="Static" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlPort" runat="server" ClientIDMode="Static" />
                </div>

                <div class="clear"></div>

                <div class="inlineProperty firstColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div12" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Video Cards&iTitle=Video Card&isColorBox=yes" style="color: blue;" class="iframe VCard"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Video Cards&iTitle=Video Card&isColorBox=yes" style="color: blue;" class="iframe VCard"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Video Cards&iTitle=Video Card&isColorBox=yes" style="color: blue;" class="iframe VCard"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Video Card
                    <asp:RequiredFieldValidator ID="rfvVideoCard" runat="server"
                        ControlToValidate="ddlVideoCard" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlVideoCard" runat="server" TabIndex="12" class="chosen-select-width Video" ClientIDMode="Static" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlVideo" runat="server" ClientIDMode="Static" />
                </div>

                <div class="inlineProperty secondColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div13" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Displays&iTitle=Display&isColorBox=yes" style="color: blue;" class="iframe Display"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Displays&iTitle=Display&isColorBox=yes" style="color: blue;" class="iframe Display"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Displays&iTitle=Display&isColorBox=yes" style="color: blue;" class="iframe Display"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Display
                    <asp:RequiredFieldValidator ID="rfvDisplay1" runat="server"
                        ControlToValidate="ddlDisplay1" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlDisplay1" runat="server" TabIndex="13" class="chosen-select-width Display" ClientIDMode="Static" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlDisplay" runat="server" ClientIDMode="Static" />
                </div>
                <div class="clear"></div>

                <div class="inlineProperty firstColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div14" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Slots&iTitle=Slot&isColorBox=yes" style="color: blue;" class="iframe Slots"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Slots&iTitle=Slot&isColorBox=yes" style="color: blue;" class="iframe Slots"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Slots&iTitle=Slot&isColorBox=yes" style="color: blue;" class="iframe Slots"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Slots
                    <asp:RequiredFieldValidator ID="rfvSlots" runat="server"
                        ControlToValidate="ddlSlots" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlSlots" runat="server" TabIndex="14" class="chosen-select-width Slot" ClientIDMode="Static" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlSlot" runat="server" ClientIDMode="Static" />
                </div>

                <div class="inlineProperty secondColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div16" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Powers&iTitle=Power&isColorBox=yes" style="color: blue;" class="iframe Power"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Powers&iTitle=Power&isColorBox=yes" style="color: blue;" class="iframe Power"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Powers&iTitle=Power&isColorBox=yes" style="color: blue;" class="iframe Power"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Power
                    <asp:RequiredFieldValidator ID="rfvPower" runat="server"
                        ControlToValidate="ddlPower" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlPower" runat="server" TabIndex="15" class="chosen-select-width Power" ClientIDMode="Static" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidmulddlPower" runat="server" ClientIDMode="Static" />
                </div>

                <div class="clear"></div>


                <div class="inlineProperty firstColumn">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div19" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/HardDisk.aspx?do=a&iframe=1&iTitle=&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe WorkStationHardware"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/HardDisk.aspx?iframe=1&iTitle=&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe WorkStationHardware"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/HardDisk.aspx?iframe=1&iTitle=&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe WorkStationHardware"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Hard drive
                    <asp:RequiredFieldValidator ID="rfvHardDrive" runat="server"
                        ControlToValidate="ddlHardDrive" Display="Dynamic" ErrorMessage="*" InitialValue="-Select-"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                    </div>
                    <asp:DropDownList ID="ddlHardDrive" runat="server" TabIndex="7" class="chosen-select-width HardDrive" multiple></asp:DropDownList>

                    <asp:HiddenField ID="hidmulddlHardDrive" runat="server" ClientIDMode="Static" />
                </div>

                <div class="inlineProperty">
                    <label>Notes</label>
                    <div class="keywords">
                        <span class="field">
                            <asp:TextBox Text="" ID="txtNotes" TabIndex="18" class="watermark multiText" placeholder="Notes"
                                runat="server"></asp:TextBox>
                        </span>
                    </div>
                </div>

                <div class="clear"></div>
                <asp:Button ID="btnSubmit" CssClass="actionBtn" TabIndex="19" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnBack" CssClass="actionBtn" TabIndex="20" runat="server" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</div>
