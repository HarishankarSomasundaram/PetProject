<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NetworkShare.ascx.cs" Inherits="UserControlsNetworkShare" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<script type="text/javascript">

    var isColorBox = "no";
    if (getQueryStringByName("isColorBox") == "yes")
        isColorBox = getQueryStringByName("isColorBox");

    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) { siteID = 0 }

    var searchFilter = $.cookie("SearchUser");
    if (searchFilter == "" || searchFilter == null) { searchFilter = 0 }

    var gridWidth = "";
    //server info
    var gridName = "#grdNetworkShare";
    var gridPager = "#grdNetworkSharepager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "NetworkShareDetailList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time
        var colname = ["NetworkShareDetailID", "Network Share Name", "Mapped", "Path"];
        //var colname = ["ServerID", "Host Name", ""];

        var colmodel = [
                           { name: 'NetworkShareDetailID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'NetworkShareName', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Mapped', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Path', sortable: true, align: "left", hidden: false, editable: true }
                           //{ name: 'View', width: 150, sortable: true, align: "left", hidden: false, editable: true, search: false },

        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        //var getWebServiceURL = baseServiceURL + masterServiceName + "GetAllServer";
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLNETWORKSHARE/MasterName/" + siteID + "/" + searchFilter;

        //alert(getWebServiceURL);
        //var crudWebServiceURL = baseServiceURL + masterServiceName + "GlobalMasterCrud/" + caption;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETENETWORKSHAREDETAILIDBYNETWORKSHAREDETAILID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETENETWORKSHAREDETAILIDBYNETWORKSHAREDETAILID";
        var AddUrl, EditUrl, ViewUrl;

        if (isColorBox == "yes") {
            AddUrl = "HardwareSettings.aspx?navPage=Network Shares&do=a&isColorBox=yes&nav=Network Shares"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Network Shares&do=e&isColorBox=yes&nav=Network Shares&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Network Shares&do=m&isColorBox=yes&nav=Network Shares&id=";
        }
        else {
            AddUrl = "HardwareSettings.aspx?navPage=Network Shares&do=a&nav=Network Shares"; // Add URL
            EditUrl = "HardwareSettings.aspx?navPage=Network Shares&do=e&nav=Network Shares&id="; //Edit URL
            ViewUrl = "HardwareSettings.aspx?navPage=Network Shares&do=m&nav=Network Shares&id=";
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
                                        true, //is New page required for Edit
                                        ViewUrl //Edit URL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        $('#grdNetworkShare').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Network Shares"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $("#del_grdNetworkShare").insertAfter(".ui-pg-button:nth(3)");

        $(".AUser").change(function () {
            $("#hidmulddlAUsers").val($(this).val());

        });
        $(".AUser1").change(function () {
            $("#hidmulddlAUsers1").val($(this).val());
        });
        $(".AUser2").change(function () {
            $("#hidmulddlAUsers2").val($(this).val());
        });
        $(".AUser3").change(function () {
            $("#hidmulddlAUsers3").val($(this).val());
        });
        $(".AUser4").change(function () {
            $("#hidmulddlAUsers4").val($(this).val());
        });
        $(".AUser5").change(function () {
            $("#hidmulddlAUsers5").val($(this).val());
        });
        $(".AUser6").change(function () {
            $("#hidmulddlAUsers6").val($(this).val());
        });
        $(".AUser7").change(function () {
            $("#hidmulddlAUsers7").val($(this).val());
        });
        $(".AUser8").change(function () {
            $("#hidmulddlAUsers8").val($(this).val());
        });
        $(".AUser9").change(function () {
            $("#hidmulddlAUsers9").val($(this).val());
        });
        $(".AUser10").change(function () {
            $("#hidmulddlAUsers10").val($(this).val());
        });

        //Fade out the message after 10 sec
        $('#lblMessage1').fadeIn().delay(2000).fadeOut(function () {
            //This is for URL rewrite with out post back 
            var stateObj = {};
            History.pushState(stateObj, "", "CustomerInfo.aspx");
            return false;
        });
        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
    });

</script>

<div class="innerTabContent" runat="server" id="netTabContent">
    <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hidisIframe" runat="server" />
    </p>

    <div id="divCrudNetworkShare" runat="server" class="siteDetail">
        <div class="contentDetail" id="divNetworkShareDetail" runat="server" style="padding-top: 0px; margin-left: 10px">
            <div class="inlineProperty secondColumn">
                <div class="clearfix">
                    <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                      {%>
                    <div id="Div33" class=" actionPanel  divIframeOperations" runat="server">
                        <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkShares&HistoryFieldName=NetworkShareName&ISForward=0&elemrntId=txtManufacturer" style="color: blue;" class="TrackHistory"></a></span>
                        <div class="tooltip-popup"></div>
                    </div>
                    <%} %>
                    <label>
                        Network Share Name
                <asp:RequiredFieldValidator ID="rfgNwSName" runat="server"
                    ControlToValidate="txtNetworkShareName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                </div>

                <asp:TextBox Text="" TabIndex="1" ID="txtNetworkShareName" class="watermark" placeholder="Network Share Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                    runat="server" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
            </div>
            
            <div id="divDefault" runat="server" style="display:inline-block;">
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="DivdivIframeOperations" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkShares&HistoryFieldName=Mapped&ISForward=0&elemrntId=txtManufacturer" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Mapped
                    <asp:RequiredFieldValidator ID="rfvMapped" runat="server"
                        ControlToValidate="txtMapped" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>

                    <asp:TextBox Text="" ID="txtMapped" TabIndex="2" class="watermark" placeholder="Mapped" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="1" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="DivdivIframeOperations2" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkShares&HistoryFieldName=Path&ISForward=0&elemrntId=txtManufacturer" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Path
                    <asp:RequiredFieldValidator ID="rfvPath" runat="server"
                        ControlToValidate="txtPath" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>

                    <asp:TextBox Text="" ID="txtPath" TabIndex="3" class="watermark" placeholder="Path" runat="server" MaxLength="260" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div3divIframeOperations" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=NetworkShares&HistoryFieldName=NetworkShareDescription&ISForward=0&elemrntId=txtManufacturer" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Description
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server"
                        ControlToValidate="txtDescription" Display="Dynamic" ErrorMessage="*" InitialValue=""
                        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>
                    </div>

                    <asp:TextBox Text="" ID="txtDescription" TabIndex="4" class="watermark" placeholder="Description"
                        runat="server" MaxLength="500" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="clear"></div>

                <div class="inlineProperty firstColumn">
                    <label>Assigned Users</label>
                    <asp:DropDownList ID="ddlAUsers" ClientIDMode="Static" TabIndex="5" runat="server" class="chosen-select-width AUser" multiple></asp:DropDownList>
                    <asp:TextBox ID="hidmulddlAUsers" runat="server" ClientIDMode="Static" Style="display: none" />
                </div>

                <div class="inlineProperty secondColumn">
                    <asp:LinkButton runat="server" Text="Add More" TabIndex="6" ID="lnkAddMore" OnClick="lnkAddMore_Click" CssClass="myLinkButton" Style="margin-top: 25px; min-width: 13px !important" />
                </div>
                <div class="clear"></div>
                <asp:PlaceHolder ID="phControl" runat="server" />
                <div class="clear"></div>
                <asp:Label runat="server" ID="hidControlsCount" Style="display: none;" />
                <asp:Button ID="btnSSubmit" CssClass="actionBtn" TabIndex="7" runat="server" Text="Submit" ValidationGroup="SReq" OnClick="btnSSubmit_Click" />
                <asp:Button ID="btnSBack" CssClass="actionBtn" TabIndex="8" runat="server" Text="Back" OnClick="btnSBack_Click" />
            </div>
        </div>
    </div>


    <div id="divGrdNetworkShare" runat="server" class="innerGrdFullWidth">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
            <table id="grdNetworkShare"></table>
            <div id="grdNetworkSharepager"></div>
        </div>
    </div>
</div>


