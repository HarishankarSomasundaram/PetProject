<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SoftwareInfo.ascx.cs" Inherits="UserControlsSoftwareInfo" %>

<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>



<script type="text/javascript">

    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) { siteID = 0 }

    var searchFilter = $.cookie("SearchUser");
    if (searchFilter == "" || searchFilter == null) { searchFilter = 0 }

    var gridWidth = "";
    var gridName = "#grdSoftwareInfo";
    var gridPager = "#grdSoftwareInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "SoftwareList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time

        var colname = ["SoftwareID", "Application", "Path", "Description", "Version", "Installed On"];

        var colmodel = [
                           { name: 'SoftwareID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: false, hidedlg: true },
                           { name: 'Application', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'PathID', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'SoftwareDescription', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'Version', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'InstalledOn', sortable: true, align: "left", hidden: false, editable: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'ShortDate' } }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidden: false, editable: false, search: false },


        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLSOFTWARES/" + caption + "/" + siteID + "/" + searchFilter;
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETESOFTWAREBYSOFTWAREID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETESOFTWAREBYSOFTWAREID";
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
                                        "CustomerInfo.aspx?do=a&nav=Softwares", // Add URL
                                        true, //is New page required for Edit
                                        "CustomerInfo.aspx?do=e&nav=Softwares&id=", //Edit URL
                                        deleteWebServiceURL,
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for view
                                        "CustomerInfo.aspx?do=m&nav=Softwares&id=" //View URL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        $('#grdSoftwareInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Softwares"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $("#del_grdSoftwareInfo").insertAfter(".ui-pg-button:nth(3)");

        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
        $("#txtNotes_tag").attr("tabindex", "9");
    });

</script>

<div>
    <div class="innerTabContent">
        <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
            <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
            <asp:HiddenField ID="hidEditID" runat="server" ClientIDMode="Static" />
        </p>

        <div id="CrudSoftware" runat="server" class="siteDetail" style="padding-top: 0px; margin-left: 10px">

            <div id="divSoftwareDetail" runat="server" class="contentDetail scrollabow" name="top" style="height: 400px;">
                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div3" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Softwares&HistoryFieldName=Application&ISForward=0&elemrntId=txtApplication" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Application
                          <asp:RequiredFieldValidator ID="rfgApplication" runat="server"
                              ControlToValidate="txtApplication" Display="Dynamic" ErrorMessage="*" InitialValue=""
                              ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtApplication" TabIndex="1" class="watermark" placeholder="Application" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="256"></asp:TextBox>

                </div>
                <div class="inlineProperty ">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div1" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Softwares&HistoryFieldName=SoftwareDescription&ISForward=0&elemrntId=txtDescription" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Description 
                          <asp:RequiredFieldValidator ID="rfvDescription" runat="server"
                              ControlToValidate="txtDescription" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                              ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtDescription" TabIndex="2" class="watermark" placeholder="Description"
                        runat="server" MaxLength="250"></asp:TextBox>

                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div2" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Softwares&HistoryFieldName=LicenseKey&ISForward=0&elemrntId=txtLicense" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            License key
                        <asp:RequiredFieldValidator ID="rfvLicense" runat="server"
                            ControlToValidate="txtLicense" Display="Dynamic" ErrorMessage="*" InitialValue=""
                            ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtLicense" TabIndex="3" class="watermark" placeholder="License key" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="32"></asp:TextBox>

                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div4" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Softwares&HistoryFieldName=Server&ISForward=0&elemrntId=txtServer" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Server
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtServer" TabIndex="4" class="watermark" placeholder="Server" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="256"></asp:TextBox>

                </div>
                
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div5" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Softwares&HistoryFieldName=PathID&ISForward=0&elemrntId=txtPath" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Path
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtPath" TabIndex="5" class="watermark" placeholder="Path" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="260"></asp:TextBox>

                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div6" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Softwares&HistoryFieldName=Version&ISForward=0&elemrntId=txtVersion" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Version
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtVersion" TabIndex="6" class="watermark" placeholder="Version" data-validation="number" data-validation-allowing="float" data-validation-decimal-separator="." data-validation-optional="true"
                        runat="server" MaxLength="10"></asp:TextBox>

                </div>
                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div18" class=" actionPanel divIframeOperations" runat="server">
                            <span class="tabActionAdd"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Users&iframe=1&iframedo=a&isColorBox=yes" style="color: blue;" class="iframe SoftwareUserInfo"></a></span>
                            <span class="tabActionEdit"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe SoftwareUserInfo"></a></span>
                            <span class="tabActionClose"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?nav=Users&opp=SH&iframe=1&iframedo=e&isColorBox=yes" style="color: blue;" class="iframe SoftwareUserInfo"></a></span>
                        </div>
                        <%} %>
                        <label>
                            Assigned User
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlAssignedUsers" runat="server" CssClass="requiredField"
                        ControlToValidate="ddlAssignedUsers" Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="true"
                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>

                    <asp:DropDownList ID="ddlAssignedUsers" TabIndex="7" runat="server" class="chosen-select-width AssignedUsers" data-placeholder="Choose Assigned Users" multiple></asp:DropDownList>
                    <asp:HiddenField ID="hidAssignedUsers" runat="server" ClientIDMode="Static" />
                </div>

                <div class="inlineProperty">
                    <div class="clearfix">
                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                          {%>
                        <div id="Div7" class=" actionPanel  divIframeOperations" runat="server">
                            <span class="infoSiteIcon"><a href="#?HistoryTrackerID=0&HistoryMasterName=Softwares&HistoryFieldName=InstalledOn&ISForward=0&elemrntId=txtInstalledDate" style="color: blue;" class="TrackHistory"></a></span>
                            <div class="tooltip-popup"></div>
                        </div>
                        <%} %>
                        <label>
                            Installed On
                         <asp:RequiredFieldValidator ID="rfvInstalledDate" runat="server"
                             ControlToValidate="txtInstalledDate" Display="Dynamic" ErrorMessage="*"
                             ValidationGroup="Req">*</asp:RequiredFieldValidator>
                        </label>
                    </div>
                    <asp:TextBox Text="" ID="txtInstalledDate" TabIndex="8" class="watermark installedDate" placeholder="Installed On"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="clear"></div>
                <div class="inlineProperty" id="inlineNotes" runat="server">
                    <label>Notes</label>
                    <asp:TextBox Text="" ID="txtNotes" class="watermark multiText" TabIndex="9" placeholder="Notes" ClientIDMode="Static"
                        runat="server" MaxLength="2000"></asp:TextBox>
                </div>
                <div class="clear"></div>

                <asp:Button ID="btnSubmit" CssClass="actionBtn" TabIndex="10" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" />
                <asp:Button ID="btnBack" CssClass="actionBtn" TabIndex="11" runat="server" Text="Back" OnClick="btnBack_Click" />
            </div>

        </div>

        <div id="divGrdSoftwareInfo" runat="server" class="innerGrdFullWidth">
            <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%">
                <table id="grdSoftwareInfo"></table>
                <div id="grdSoftwareInfopager"></div>
            </div>
        </div>

    </div>
</div>
