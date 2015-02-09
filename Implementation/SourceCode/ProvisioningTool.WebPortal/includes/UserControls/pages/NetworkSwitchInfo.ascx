<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NetworkSwitchInfo.ascx.cs" Inherits="UserControlsNetworkSwitchInfo" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<script type="text/javascript">
    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) { siteID = 0 }

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

    var gridWidth = "";
    var gridName = "#grdNetworkSwitchInfo";
    var gridPager = "#grdNetworkSwitchInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "NetworkSwitchList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time
        var colname = ["NetworkSwitchID", "Host Name", "Model", "O/S", "Firmware", "Interface", "Installed On"];

        var colmodel = [
                           { name: 'NetworkSwitchID', key: true, width: 100, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'Hostname', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'NetworkSwitchModel.MasterValue', width: 200, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'OSVersion.MasterValue', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Firmware', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'NetworkSwitchInterfaces', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'InstalledOn', width: 100, sortable: true, hidden: false, editable: true, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidden: false, editable: true, search: false }

        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLNETWORKSWITCHES/" + caption + "/" + siteID + "/Searchtest";
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETENETWORKSWITCHBYNETWORKSWITCHID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETENETWORKSWITCHBYNETWORKSWITCHID";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "HardwareSettings.aspx?navPage=Network Switches&do=a&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Network Switches"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Network Switches&do=e&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Network Switches&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Network Switches&do=m&isColorBox=" + isColorBox + "&provisioning=" + provisioning + "&nav=Network Switches&id="; //View URL
        }
        else {
            AddUrl = "HardwareSettings.aspx?navPage=Network Switches&do=a&nav=Network Switches"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Network Switches&do=e&nav=Network Switches&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Network Switches&do=m&nav=Network Switches&id="; //View URL
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
        $('#grdNetworkSwitchInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Network Switches"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $("#del_grdNetworkSwitchInfo").insertAfter(".ui-pg-button:nth(3)");
        
        //Fade out the message after 10 sec
        $('#includes_usercontrols_pages_networkswitchinfo_ascx_lblUserControlMessage').fadeIn().delay(2000).fadeOut(function () {
            //This is for URL rewrite with out post back 
            //var stateObj = {};
            //History.pushState(stateObj, "", "CustomerInfo.aspx");
            //return false;
        });

        var mode = GetParameterValues("do");
        $('#txtNotes_tag').keypress(function (e) {
            if (mode != '' && mode != undefined && mode == 'm') {
                e.preventDefault();
                if (e.charCode == 0) {
                    e.preventDefault();
                }
            }

        });

        $('#txtNotes_tag').live("cut copy paste", function (e) {
            if (mode != '' && mode != undefined && mode == 'm') {
                e.preventDefault();
                if (e.charCode == 0) {
                    e.preventDefault();
                }
            }

        });
        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
        //Split the Page Querystring and give the resultant value requred 
        //This is somthing like Request.QueryString["ReqValue"]-->some valin query string
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }

        $("#includes_usercontrols_pages_networkswitchinfo_ascx_txtInterfaces_tag").attr("tabindex", "18");
        $("#txtNotes_tag").attr("tabindex", "19");

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
         <asp:DropDownList ID="ddldeviceList" TabIndex="9" runat="server" class="selector" ClientIDMode="Static"></asp:DropDownList>
         <asp:Button ID="btnFill" CssClass="actionBtn" runat="server" Text="Copy" OnClick="btnFill_Click" Style="float:left;margin-left:10px;" />
    </div>
    <asp:HiddenField ID="HiddenColorBox" runat="server" ClientIDMode="Static" Value="0" />
</div>

<div class="innerTabContent">
    <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
        <asp:Label ID="lblUserControlMessage" runat="server"></asp:Label>
         <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
    </p>
    <div id="provClose" runat="server" class="provClose">
        <a style="text-decoration: underline; color: blue;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Network%20Switches&provUser=1&iframe=1&opp=S&iframedo=a&isColorBox=yes" id="addDevice"> Add Another Device</a> |
        <a style="text-decoration: underline; color: blue;" target="_self" href="javascript:parent.$.fn.colorbox.close();" id="closeDiv">Close</a>
    </div>

    <div id="CrudNetworkSwitch" runat="server" class="siteDetail">
        <div id="divNetworkSwitchDetail" runat="server" class="contentDetail siteDetail" style="padding-top: 0px; margin-left: 10px">
            <div class="inlineProperty ">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div17" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtHostName" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Host Name  
                    <asp:RequiredFieldValidator ID="rfgHostName" runat="server"
                        ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtHostName"  TabIndex="1" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="64"></asp:TextBox>

            </div>
            <div class="inlineProperty ">
                <div class="clearfix">
                <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                  {%>
                <div id="Div1" class=" actionPanel divIframeOperations" runat="server">
                     <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=NetworkSwitchModelID&ISForward=1&elemrntId=ddlModel" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch Models&iTitle=Model&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                    <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch Models&iTitle=Model&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                    <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch Models&iTitle=Model&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                </div>
                <%} %>
                    <label>
                        Model  
                    <asp:RequiredFieldValidator ID="rfvModel" runat="server"
                        ControlToValidate="ddlModel" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:DropDownList ID="ddlModel" runat="server"  TabIndex="2" class="selector"></asp:DropDownList>
            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div16" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=SerialNumber&ISForward=0&elemrntId=txtSerialNo" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Serial Number
                    <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server"
                        ControlToValidate="txtSerialNo" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSerialNo" class="watermark"  TabIndex="3" placeholder="Serial Number" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="12"></asp:TextBox>

            </div>

            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div15" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=InstalledOn&ISForward=0&elemrntId=txtInstalledDate" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Installed On 
                    <asp:RequiredFieldValidator ID="rfvInstalledDate" runat="server"
                        ControlToValidate="txtInstalledDate" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtInstalledDate"  TabIndex="4" class="watermark installedDate" placeholder="Installed On"
                    runat="server" MaxLength="10"></asp:TextBox>

            </div>
            
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div14" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=WarrantyExpiresOn&ISForward=0&elemrntId=txtWarrantyExpires" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Warranty Expires On
                    <asp:RequiredFieldValidator ID="rfvWarrantyExpires" runat="server"
                        ControlToValidate="txtWarrantyExpires" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtWarrantyExpires"  TabIndex="5" class="watermark expiryDate" placeholder="Warranty Expires"
                    runat="server" MaxLength="10"></asp:TextBox>

            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div13" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=Speed&ISForward=0&elemrntId=txtSpeed" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Speed
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSpeed" runat="server" ValidationGroup="Req"
                        ControlToValidate="txtSpeed" Display="Dynamic" ErrorMessage="*" InitialValue="">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtSpeed" class="watermark"  TabIndex="6" placeholder="Speed" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="10"></asp:TextBox>

            </div>
            <div class="inlineProperty radioCotent">
                <label>POE</label>
                <%--<asp:CheckBox Text="" ID="chkPOE" runat="server" />--%>
                <asp:RadioButtonList runat="server" ID="rbtPOE" RepeatDirection="Horizontal">
                    <asp:ListItem Value="true" Text="Yes" />
                    <asp:ListItem Value="false" Text="No" />
                </asp:RadioButtonList>

            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div12" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=Power&ISForward=0&elemrntId=txtPower" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Power 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPower" runat="server" ValidationGroup="Req"
                        ControlToValidate="txtPower" Display="Dynamic" ErrorMessage="*" InitialValue="">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtPower" class="watermark" TabIndex="7" placeholder="Power" Width="212px" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="10"></asp:TextBox>

            </div>
            
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div11" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=IPAddress&ISForward=0&elemrntId=txtIPAddress" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                      <div id="Div30" class=" actionPanel  divIframeOperations" runat="server">
                          <asp:CheckBox runat="server" ID="chkDHCP" ClientIDMode="Static" />DHCP
                          </div>
                    <label>
                        IP Address 
                  <%--  <asp:RequiredFieldValidator ID="rfvIPAddress" runat="server"
                        ControlToValidate="txtIPAddress" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>--%>
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtIPAddress"
                                ErrorMessage="Invalid IP Address"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="SReq">
                            </asp:RegularExpressionValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtIPAddress" TabIndex="8" class="watermark ipaddress" placeholder="IP Address"
                    runat="server" MaxLength="15"></asp:TextBox>

            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div10" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=Subnet&ISForward=0&elemrntId=txtSubnet" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
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
                                ValidationGroup="SReq">
                            </asp:RegularExpressionValidator>
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtSubnet"  TabIndex="9" class="watermark ipaddress" placeholder="Subnet"
                    runat="server" MaxLength="15"></asp:TextBox>

            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div9" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=Gateway&ISForward=0&elemrntId=txtGateway" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
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
                                ValidationGroup="SReq">
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
                    <div id="Div8" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=AdminUserName&ISForward=0&elemrntId=txtAdminUsername" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Admin Username 
                    <asp:RequiredFieldValidator ID="rfvAdminUsername" runat="server"
                        ControlToValidate="txtAdminUsername" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtAdminUsername" TabIndex="11" class="watermark Username" placeholder="Admin Username" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="20"></asp:TextBox>

            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div7" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=AdminPassword&ISForward=0&elemrntId=txtPassword" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Password  
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtPassword" TabIndex="12" class="watermark" placeholder="Password"
                    runat="server" MaxLength="40"></asp:TextBox>

            </div>

            <div class="inlineProperty ">
                <div class="clearfix">
                <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                  {%>
                <div id="Div2" class=" actionPanel divIframeOperations" runat="server">
                     <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=OSVersion&ISForward=1&elemrntId=ddlOSVersion" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch OS Versions&iTitle=OS Version&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                    <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch OS Versions&iTitle=OS Version&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                    <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch OS Versions&iTitle=OS Version&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
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
                    <div id="Div6" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=Firmware&ISForward=0&elemrntId=txtFirmware" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Firmware 
                    <asp:RequiredFieldValidator ID="rfvFirmware" runat="server"
                        ControlToValidate="txtFirmware" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="Req">*</asp:RequiredFieldValidator></label>
                </div>
                <asp:TextBox Text="" ID="txtFirmware" TabIndex="14" class="watermark" placeholder="Firmware" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="50"></asp:TextBox>

            </div>

            
            <div class="inlineProperty">
                <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                  {%>
                <div id="Div3" class=" actionPanel divIframeOperations" runat="server">
                    <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch Modules&iTitle=Modules&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                    <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch Modules&iTitle=Modules&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                    <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Network Switch Modules&iTitle=Modules&isColorBox=yes" style="color: blue;" class="iframe Apps"></a></span>
                </div>
                <%} %>
                <div class="clearfix">
                    <label>
                        Modules
                   </label>
                </div>
                <asp:DropDownList ID="ddlModules" runat="server" TabIndex="15" class="chosen-select-width Modules" Width="100%" multiple data-placeholder="Select Modules"></asp:DropDownList>
                <asp:HiddenField ID="hidModuleID" runat="server" ClientIDMode="Static" />

            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div5" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=VLAN&ISForward=0&elemrntId=txtVLAN" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        VLAN  
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtVLAN" class="watermark" TabIndex="16" placeholder="VLAN" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="32"></asp:TextBox>

            </div>
            <div class="inlineProperty">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkSwitches&HistoryFieldName=SFPType&ISForward=0&elemrntId=txtSFPType" style="color: blue;" class="TrackHistory"></a></span><div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        SFP Type
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtSFPType" class="watermark" TabIndex="17" placeholder="SFP Type" data-validation="alphanumeric" data-validation-allowing="-+()_"  data-validation-optional="true"
                    runat="server" MaxLength="10"></asp:TextBox>

            </div>


            <div class="clear"></div>
            <div class="inlineProperty secondColumn" id="inlineInterface" runat="server">
                <div class="clearfix">
                    <label>
                        Interfaces    
                    </label>
                </div>
                <asp:TextBox Text="" ID="txtInterfaces" TabIndex="18" class="watermark multiText" placeholder="Interfaces"
                    runat="server" MaxLength="500"></asp:TextBox>

            </div>
            <div class="clear"></div>

            <div class="inlineProperty" id="inlineNotes" runat="server">
                <label>
                    Notes 
                </label>
                <asp:TextBox Text="" ID="txtNotes" TabIndex="19" class="watermark multiText" placeholder="Notes" ClientIDMode="Static"
                    runat="server" MaxLength="2000"></asp:TextBox>

            </div>
            <div class="clear"></div>
            <asp:Button ID="btnSubmit" CssClass="actionBtn" TabIndex="20" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" />
            <asp:Button ID="btnBack" CssClass="actionBtn" TabIndex="21" runat="server" Text="Back" OnClick="btnBack_Click" ClientIDMode="Static" />
        </div>
    </div>
    <div id="divGrdNetworkSwitchInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
            <table id="grdNetworkSwitchInfo"></table>
            <div id="grdNetworkSwitchInfopager"></div>
        </div>
    </div>
</div>


