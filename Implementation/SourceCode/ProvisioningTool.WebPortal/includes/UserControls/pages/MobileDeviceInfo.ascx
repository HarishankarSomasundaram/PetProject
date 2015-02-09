<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MobileDeviceInfo.ascx.cs" Inherits="UserControlsMobileDeviceInfo" %>
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
    var gridName = "#grdMobileDeviceInfo";
    var gridPager = "#grdMobileDeviceInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "MobileDeviceList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap


    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time

        var colname = ["MobileDeviceID", "Host Name", "Type", "Manufacture", "Model", "Assigned Users", "Installed On"];

        var colmodel = [
                           { name: 'MobileDeviceID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: false, hidedlg: true },
                           { name: 'Hostname', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'MobileDeviceType.MasterValue', sortable: true, hidedlg: true, hidden: true, editable: false, search: true },
                           { name: 'MobileDeviceManufacture.MasterValue', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'MobileDeviceModel.MasterValue', sortable: false, align: "left", hidden: false, editable: false, search: true },
                           { name: 'AssignedUser.UserName', sortable: false, align: "left", hidden: false, editable: false, search: true },
                           { name: 'InstalledOn', sortable: true, align: "left", hidden: false, editable: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } },
        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLMOBILEDEVICES/" + caption + "/" + siteID + "/" + searchFilter;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEMOBILEDEVICEBYMOBILEDEVICEID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEMOBILEDEVICEBYMOBILEDEVICEID";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "HardwareSettings.aspx?navPage=Mobile Devices&do=a&isColorBox=yes&provisioning=" + provisioning + "&nav=Mobile Devices"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Mobile Devices&do=e&isColorBox=yes&provisioning=" + provisioning + "&nav=Mobile Devices&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Mobile Devices&do=m&isColorBox=yes&provisioning=" + provisioning + "&nav=Mobile Devices&id=";
        }
        else {
            AddUrl = "HardwareSettings.aspx?navPage=Mobile Devices&do=a&nav=Mobile Devices"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Mobile Devices&do=e&nav=Mobile Devices&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Mobile Devices&do=m&nav=Mobile Devices&id=";
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
                                        ViewUrl //Edit URL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        $('#grdMobileDeviceInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Mobile Devices"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $("#del_grdMobileDeviceInfo").insertAfter(".ui-pg-button:nth(3)");
        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
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
        <asp:DropDownList ID="ddldeviceList" TabIndex="9" runat="server" class="selector" ClientIDMode="Static"></asp:DropDownList>
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
            <a style="text-decoration: underline; color: blue;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Mobile%20Devices&provUser=1&iframe=1&opp=S&iframedo=a&isColorBox=yes" id="addDevice">Add Another Device</a> |
            <a style="text-decoration: underline; color: blue;" target="_self" href="javascript:parent.$.fn.colorbox.close();" id="closeDiv">Close</a>
        </div>

        <div id="CrudMobileDevice" runat="server" class="siteDetail" style="padding-top: 0px; margin-left: 10px">
            <div id="divMobileDeviceDetail" runat="server" class="contentDetail scrollabow" name="top" style="height: 400px;">
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=MobileDevices&HistoryFieldName=Hostname&ISForward=0&elemrntId=txtHostName" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Host Name
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtHostName" runat="server" CssClass="requiredField"
                        ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtHostName" TabIndex="1" class="watermark" placeholder="Host Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>
                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div1" class=" actionPanel divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=MobileDevices&HistoryFieldName=MobileDeviceTypeID&ISForward=1&elemrntId=ddlType" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Types&iTitle=Types&isColorBox=yes" style="color: blue;" class="iframe MDType"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Types&iTitle=Types&isColorBox=yes" style="color: blue;" class="iframe MDType"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Types&iTitle=Types&isColorBox=yes" style="color: blue;" class="iframe MDType"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Type
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlType" runat="server" CssClass="requiredField"
                        ControlToValidate="ddlType" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlType" TabIndex="2" ClientIDMode="Static" runat="server" class="selector"></asp:DropDownList>
                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div2" class=" actionPanel divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=MobileDevices&HistoryFieldName=MobileDeviceManufactureID&ISForward=1&elemrntId=ddlManufacture" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Manufacturers&iTitle=Manufacturers&isColorBox=yes" style="color: blue;" class="iframe MDManuf"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Manufacturers&iTitle=Manufacturers&isColorBox=yes" style="color: blue;" class="iframe MDManuf"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Manufacturers&iTitle=Manufacturers&isColorBox=yes" style="color: blue;" class="iframe MDManuf"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Manufacture
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlManufacture" runat="server" CssClass="requiredField"
                        ControlToValidate="ddlManufacture" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlManufacture" TabIndex="3" ClientIDMode="Static" runat="server" class="selector"></asp:DropDownList>
                </div>
                <div class="clear"></div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div3" class=" actionPanel divIframeOperations" runat="server">
                            <div style="position: relative; width: 17px; display: inline-block;"><span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=MobileDevices&HistoryFieldName=MobileDeviceModelID&ISForward=1&elemrntId=ddlModel" style="color: blue;" class="TrackHistory"></a></span>
                                <div class="tooltip-popup"></div>
                            </div>
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Models&iTitle=Models&isColorBox=yes" style="color: blue;" class="iframe MDModel"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Models&iTitle=Models&isColorBox=yes" style="color: blue;" class="iframe MDModel"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>Masters/GlobalMaster.aspx?iframe=Mobile Device Models&iTitle=Models&isColorBox=yes" style="color: blue;" class="iframe MDModel"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Model
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlModel" runat="server" CssClass="requiredField"
                        ControlToValidate="ddlModel" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlModel" TabIndex="4" ClientIDMode="Static" runat="server" class="selector"></asp:DropDownList>
                </div>

                <div class="inlineProperty" id="inlineAssignedUser" runat="server">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div18" class=" actionPanel divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=MobileDevices&HistoryFieldName=AssignedUserID&ISForward=0&elemrntId=ddlAssignedUser" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Users&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe MDIAssignedUser"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe MDIAssignedUser"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&opp=SH&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe MDIAssignedUser"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Assigned User
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlAssignedUser" runat="server" CssClass="requiredField"
                        ControlToValidate="ddlAssignedUser" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:DropDownList ID="ddlAssignedUser" TabIndex="5" ClientIDMode="Static" runat="server" class="selector"></asp:DropDownList>
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div15" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=MobileDevices&HistoryFieldName=InstalledOn&ISForward=0&elemrntId=txtInstalledOn" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Installed On
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtInstalledOn" runat="server" CssClass="requiredField"
                        ControlToValidate="txtInstalledOn" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtInstalledOn" TabIndex="6" class="watermark installedDate" placeholder="MM/DD/YYYY" ClientIDMode="Static"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="clear"></div>

                <asp:Button ID="btnSubmit" CssClass="actionBtn" TabIndex="7" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" />
                <asp:Button ID="btnBack" CssClass="actionBtn" TabIndex="8" runat="server" Text="Back" OnClick="btnBack_Click" ClientIDMode="Static" />
            </div>

        </div>

        <div id="divGrdMobileDeviceInfo" runat="server" class="innerGrdFullWidth">
            <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
                <table id="grdMobileDeviceInfo"></table>
                <div id="grdMobileDeviceInfopager"></div>
            </div>
        </div>

    </div>
</div>


