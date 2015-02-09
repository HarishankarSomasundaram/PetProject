<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProvisioningCheckList.ascx.cs" Inherits="UserControlsProvisioningCheckList" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>
<script type="text/ecmascript">

    function fnCheckAll() {
        if ($("#chkVerifyEmail").prop('checked') && $("#chkUserName").prop('checked') && $("#chkDepartment").prop('checked') &&
            $("#chkSecurityGroups").prop('checked') && $("#chkNetworkShares").prop('checked') && $("#chkEmail").prop('checked') &&
            $("#chkAddUserEmailDistributions").prop('checked') && $("#chkHosted_ServerAntispam").prop('checked') &&
            $("#chkAssignedPrinters").prop('checked') && $("#chkPerformTest").prop('checked') &&
            $("#chkUpdateCustomerLanDiagram").prop('checked') && $("#chkThanksCustomer").prop('checked')
        ) {
            $("#chkTaskCompleted").prop("disabled", false);
        }
        else {
            $("#chkTaskCompleted").prop("checked", false);
            $("#chkTaskCompleted").prop("disabled", true);
            $("#hidTaskCompleted").val(false);
        }
    }

    $(document).ready(function ($) {

        fnCheckAll();

        $('#contentWrap').css({
            'min-height': $(window).height() - $('header').height() - $('footer').height() + 18 + 'px'
        });
        $(".VerifyEmail").change(function () {
            $("#hidchkVerifyEmail").val($("#chkVerifyEmail").prop('checked'));
            fnCheckAll();
        });
        $(".UserName").change(function () {
            $("#hidUserName").val($("#chkUserName").prop('checked'));
            fnCheckAll();
        });
        $(".Department").change(function () {
            $("#hidDepartment").val($("#chkDepartment").prop('checked'));
            fnCheckAll();
        });
        $(".SecurityGroups").change(function () {
            $("#hidSecurityGroups").val($("#chkSecurityGroups").prop('checked'));
            fnCheckAll();
        });
        $(".NetworkShares").change(function () {
            $("#hidNetworkShares").val($("#chkNetworkShares").prop('checked'));
            fnCheckAll();
        });
        $(".Email").change(function () {
            $("#hidEmail").val($("#chkEmail").prop('checked'));
            fnCheckAll();
        });
        $(".AddUserEmailDistributions").change(function () {
            $("#hidAddUserEmailDistributions").val($("#chkAddUserEmailDistributions").prop('checked'));
            fnCheckAll();
        });
        $(".Hosted_ServerAntispam").change(function () {
            $("#hidHosted_ServerAntispam").val($("#chkHosted_ServerAntispam").prop('checked'));
            fnCheckAll();
        });
        $(".AssignedPrinters").change(function () {
            $("#hidAssignedPrinters").val($("#chkAssignedPrinters").prop('checked'));
            fnCheckAll();
        });
        $(".PerformTest").change(function () {
            $("#hidPerformTest").val($("#chkPerformTest").prop('checked'));
            fnCheckAll();
        });
        $(".UpdateCustomerLanDiagram").change(function () {
            $("#hidUpdateCustomerLanDiagram").val($("#chkUpdateCustomerLanDiagram").prop('checked'));
            fnCheckAll();
        });
        $(".ThanksCustomer").change(function () {
            $("#hidThanksCustomer").val($("#chkThanksCustomer").prop('checked'));
            fnCheckAll();
        });
        $(".TaskCompleted").change(function () {
            $("#hidTaskCompleted").val($("#chkTaskCompleted").prop('checked'));
        });



    });


    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) { siteID = 0 }

    var gridWidth = "";
    var gridName = "#grdProvisioningCheckListInfo";
    var gridPager = "#grdProvisioningCheckListInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridHeight = "250"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "ChecklistItems"
    var pageSizeOption = ["10", "20", "30"];

    function InitializeGrid(caption) {

        //To define the Grid for the page on the design time

        var colname = ["UserID", "User Name", "Department", "Email", "Status", "Print"];

        var colmodel = [
                           { name: 'User.UserID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: false, hidedlg: true },
                           { name: 'User.UserName', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'User.DepartmentName', sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'User.Email', sortable: false, align: "left", hidden: false, editable: false, search: true },
                           { name: 'IsChecklistDone', sortable: true, align: "left", hidden: false, editable: false, search: false },
                           { name: 'View', sortable: true, align: "left", hidden: false, editable: false, search: false },



        ];
        //Default SortColumn
        var sortName = "User.UserName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLCHECKLIST/" + caption + "/" + siteID + "/0";
        var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETECHECKLISTBYCHECKLISTID";
        var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETECHECKLISTBYCHECKLISTID";
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
                                        "CustomerInfo.aspx?do=a&nav=Provisioning%20Check%20List", // Add URL
                                        true, //is New page required for Edit
                                        "CustomerInfo.aspx?do=e&nav=Provisioning%20Check%20List&id=", //Edit URL
                                        deleteWebServiceURL
                                     );
        return objGridList;
    };



    $(document).ready(function () {
        $('#grdProvisioningCheckListInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Provisioning Check List"));
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();

        $("#btnPrint").live("click", function () {
            window.print();
        });

        //$("#btnExport2PDF").live("click", function () {
        //    $('#logoContainer').css("display", "block");
        //});


        $("#btnPreview").click(function () {
            //Get the HTML of div
            //var divElements = document.getElementById('CrudProvisioningCheckList').innerHTML;
            ////Get the HTML of whole page
            //var oldPage = document.body.innerHTML;

            ////Reset the page's HTML with div's HTML only
            //document.body.innerHTML =
            //  "<html><head><title></title></head><body>" +
            //  divElements + "</body>";

            //Print Page
            //window.print();

            ////Restore orignal HTML
            //document.body.innerHTML = oldPage;
            //return false;
            //PrintElem();

            // if js is disabled nothing should change and the link will work normally
            var url = "PrintPageLayout.aspx";
            var windowName = "Provisioning Check List";
            window.open(url, windowName, "height=734,width=784");
            return false;
        });


        function PrintElem() {
            Popup($(".siteDetail").html());
        }

        function Popup(data) {
            var mywindow = window.open('', 'new div', 'height=400,width=600');
            mywindow.document.write('<html><head><header><div class="container"><aside class="logo"><img src="../../includes/UI/images/logo.png" alt="" /></a></aside></div></header>');
            mywindow.document.write('<link rel="stylesheet" href="../includes/UI/css/jquery-ui-framework.css" type="text/css" />');
            mywindow.document.write('<link rel="stylesheet" href="../includes/UI/css/style.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');
            mywindow.print();
            return false;
        }

        if (getQueryStringByName("do") != "v") {
            $('#mainTag').css("display", "none");
        }

        $("#Add").click(function () {
            $('#adduser').css("display", "block");
            $('#moveuser').css("display", "none");
            $('#existingDevice').css("display", "none");

        });

        $("#Move").click(function () {
            $('#moveuser').css("display", "block");
            $('#adduser').css("display", "none");
            $('#existingDevice').css("display", "none");
        });

        $("#newUser").click(function () {
            $('#existingDevice').css("display", "none");
        });

        $("#newDevice").click(function () {
            $('#existingDevice').css("display", "block");
            $("#HiddenAddORChange").val(0);
            $('#deviceType').change();
        });

        $("#changeDevice").click(function () {
            $('#existingDevice').css("display", "block");
            $("#HiddenAddORChange").val(1);
            var url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=v&nav=Workstations&provisioning=1&provUser=0&iframe=1&iframedo=v&isColorBox=yes";

            if (url) { // require a URL
                $("#inlineDevice").attr("href", url);
            }
        });


        $('#deviceType').change(function () {
            var deviceType = $(this).val();
            var addORChange = 0;
            var provUser = 1;
            var actionType = "a";
            var option = "&opp=S";

            addORChange = $("#HiddenAddORChange").val();

            if (addORChange == 1) {
                actionType = "v";
                provUser = 0;
                option = "";
            }

            var url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Workstations&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes" + option;

            if (deviceType == 2)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Laptops&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes" + option;
            if (deviceType == 3)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Routers&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes";
            if (deviceType == 4)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Firewalls&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes";
            if (deviceType == 5)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Network%20Switches&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes";
            if (deviceType == 6)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Printers&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes";
            if (deviceType == 7)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Servers&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes" + option;
            if (deviceType == 8)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Mobile%20Devices&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes";
            if (deviceType == 9)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Phone%20System&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes";
            if (deviceType == 10)
                url = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=" + actionType + "&nav=Wireless&provisioning=1&provUser=" + provUser + "&iframe=1&iframedo=" + actionType + "&isColorBox=yes";


            if (url) { // require a URL
                $("#inlineDevice").attr("href", url);
                //$("#inlineDevice").click();
            }
        });
    });
</script>

<div class="innerTabContent2" id="parentTab">
    <%-- Add , Move --%>
    <div style="padding-top: 20px; margin-left: 20px" id="mainTag">
        <a target="_self" href="#hTab-2" id="Add" class="ltGryBtn">Add</a>
        <a target="_self" href="#hTab-2" id="Move" class="ltGryBtn">Move or Change</a>
    </div>
    <%-- Add User / Device --%>
    <div style="padding-top: 15px; margin-left: 20px; display: none;" id="adduser">
        Add <a style="color: #0e5c9c; text-decoration: underline;" id="newUser" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>/CustomerInfo.aspx?do=a&nav=Users&iframe=1&provisioning=1&provUser=1&iframedo=a&isColorBox=yes" class="iframe Site">New User</a> or
        <a style="color: #0e5c9c; text-decoration: underline;" href="#hTab-2" id="newDevice">New Device</a>
    </div>

    <%-- Move or Change User / Device --%>
    <div style="padding-top: 15px; margin-left: 20px; display: none;" id="moveuser">
        Move or Change <a style="text-decoration: underline; color: #0e5c9c;" target="_self" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>/CustomerInfo.aspx?do=v&nav=Users&provisioning=1&iframe=1&iframedo=v&isColorBox=yes" class="iframe Site" id="changeUser">User</a> or
        <a style="text-decoration: underline; color: #0e5c9c;" target="_self" href="#hTab-2" id="changeDevice">Device</a>
    </div>

    <%-- Existing User Message --%>
    <div style="padding-top: 10px; margin-left: 20px; display: none;" id="existingDevice">
        <select id="deviceType">
            <option value="1">Workstations</option>
            <option value="2">Laptops</option>
            <option value="3">Routers</option>
            <option value="4">Firewalls</option>
            <option value="5">Network Switches</option>
            <option value="6">Printers</option>
            <option value="7">Servers</option>
            <option value="8">Mobile Devices</option>
            <option value="9">Phone System</option>
            <option value="10">Wireless</option>
        </select>

        <a style="text-decoration: underline; color: blue;" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerInfo.aspx?do=a&nav=Workstations&opp=S&iframe=1&provisioning=1&provUser=1&iframedo=a&isColorBox=yes" id="inlineDevice" class="iframe Site">
            <img src="../../includes/UI/images/newwindow.png" /></a>
        <!--spn>Enter new device information or <a  style="text-decoration: underline;color: blue;" target="_self" href="#" id="A6">Copy from existing Device</a></spn-->
    </div>
    <asp:HiddenField ID="HiddenAddORChange" runat="server" ClientIDMode="Static" Value="0" />

    <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
    </p>

    <asp:HiddenField ID="hidUserName" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidDepartment" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidSecurityGroups" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidNetworkShares" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidchkVerifyEmail" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidPerformTest" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidAssignedPrinters" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidHosted_ServerAntispam" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidAddUserEmailDistributions" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidEmail" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidUpdateCustomerLanDiagram" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidThanksCustomer" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hidTaskCompleted" ClientIDMode="Static" runat="server" />
    <div id="CrudProvisioningCheckList" runat="server" class="siteDetail" style="padding-top: 25px; margin-left: 10px">
        <h2 class="heading">
            <label class="clearfix">Check List Information</label>
        </h2>

        <asp:Panel ID="Panel1" runat="server" Direction="LeftToRight">
            <div id="divProvisioningCheckListDetail" runat="server" class="scrollabow">
                
                <div class="checkUserName" id="checkUserName">
                    <asp:CheckBox ID="chkUserName" CssClass="UserName chk" ClientIDMode="Static" runat="server" />
                    <asp:Image ID="Image1" runat="server" />

                    Create new or copy from an existing user account in Active Directory (Copy previous user if possible)<br />
                    Username format:
                        <asp:Label CssClass="checklistColor" Text="" ID="lblUserName" runat="server" />
                </div>
                <asp:Image ID="Image14" runat="server"  />
                <div>
                    <asp:Image ID="Image2" runat="server" />
                    <asp:CheckBox ID="chkDepartment" CssClass="Department chk" ClientIDMode="Static" runat="server" />

                    Add user to OU/department:
                        <asp:Label CssClass="checklistColor" Text="" ID="lblDepartment" runat="server" />

                </div>
                <asp:Image ID="Image15" runat="server" />
                <div>
                    <asp:Image ID="Image3" runat="server" />
                    <asp:CheckBox ID="chkSecurityGroups" CssClass="SecurityGroups chk" ClientIDMode="Static" runat="server" />
                    Add user to security groups:
                        <asp:Label CssClass="checklistColor" Text="" ID="lblSecurityGroup" runat="server" />
                </div>
                  <asp:Image ID="Image25" runat="server" />
                <div>

                    <asp:Image ID="Image4" runat="server" />
                    <asp:CheckBox ID="chkNetworkShares" CssClass="NetworkShares chk" ClientIDMode="Static" runat="server" />

                    Add login script and/or home folder for drive mappings to profile (not necessary if copied from previous user):<br />
                    Network Shares: 
                    <asp:Label CssClass="checklistColor" Text="" ID="lblNetworkShares" runat="server" />

                </div>
                <asp:Image ID="Image16" runat="server" />
                <div>

                    <asp:Image ID="Image5" runat="server" />
                    <asp:CheckBox ID="chkVerifyEmail" CssClass="VerifyEmail chk" ClientIDMode="Static" runat="server" />

                    Create email account in Exchange (verify email address created by recipient policy)
                </div>
                <asp:Image ID="Image17" runat="server" />
                <div>

                    <asp:Image ID="Image6" runat="server" />
                    <asp:CheckBox ID="chkEmail" CssClass="Email chk" ClientIDMode="Static" runat="server" />
                    Email address:
                        <asp:Label CssClass="checklistColor" Text="" ID="lblEmail" runat="server" />

                </div>
                <asp:Image ID="Image18" runat="server" />
                <div>


                    <asp:Image ID="Image7" runat="server" />
                    <asp:CheckBox ID="chkAddUserEmailDistributions" CssClass="AddUserEmailDistributions chk" ClientIDMode="Static" runat="server" />
                    Add user to any email distributions lists:
                        <asp:Label CssClass="checklistColor" Text="" ID="lblUserEmailDistribution" runat="server" />

                </div>
                <asp:Image ID="Image19" runat="server" />
                <div>


                    <asp:Image ID="Image8" runat="server" />
                    <asp:CheckBox ID="chkHosted_ServerAntispam" CssClass="Hosted_ServerAntispam chk" ClientIDMode="Static" runat="server" />

                    Hosted Antispam: Add the user’s email address for user to the spam filtering list<br />
                    Server Antispam: Check Antispam license count and disable former users
                    
                </div>
                <asp:Image ID="Image20" runat="server" />
                <div>


                    <asp:Image ID="Image9" runat="server" />
                    <asp:CheckBox ID="chkAssignedPrinters" CssClass="AssignedPrinters chk" ClientIDMode="Static" runat="server" />

                    Printers:
                        <asp:Label CssClass="checklistColor" Text="" ID="lblAssignedPrinters" runat="server" />

                </div>
                <asp:Image ID="Image21" runat="server" />
                <div>


                    <asp:Image ID="Image10" runat="server" />
                    <asp:CheckBox ID="chkPerformTest" CssClass="PerformTest chk" ClientIDMode="Static" runat="server" />

                    Perform a test email from user's account<br />
                    Perform a test reply back from user's account
                </div>
               <asp:Image ID="Image22" runat="server" />
                <div>

                    <asp:Image ID="Image11" runat="server" />
                    <asp:CheckBox ID="chkUpdateCustomerLanDiagram" CssClass="UpdateCustomerLanDiagram chk" ClientIDMode="Static" runat="server" />

                    Update Customer’s LAN diagram
                </div>
                <asp:Image ID="Image23" runat="server" />
                <div>


                    <asp:Image ID="Image12" runat="server" />
                    <asp:CheckBox ID="chkThanksCustomer" CssClass="ThanksCustomer chk" ClientIDMode="Static" runat="server" />

                    Thank the customer for their buisiness

                </div>
                <asp:Image ID="Image24" runat="server" />
                <div>

                    <asp:Image ID="Image13" runat="server" />
                    <asp:CheckBox ID="chkTaskCompleted" CssClass="TaskCompleted chk" ClientIDMode="Static" runat="server" />

                    All Task Completed

                </div>
            </div>
        </asp:Panel>
        <div id="divProvisioningCheckListDetailBase" runat="server" class="classbtnSubmit no-print mainPage">
            <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnPrint" CssClass="actionBtn" ClientIDMode="Static" runat="server" Text="Print" />
            <asp:Button ID="btnExport2PDF" CssClass="actionBtn" ClientIDMode="Static" runat="server" Text="Export to PDF" OnClick="btnExport2PDF_Click" />
            <asp:Button ID="btnBack" CssClass="actionBtn" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>


    <div id="divGrdProvisioningCheckListInfo" runat="server" class="innerGrdFullWidth">
        <div style="padding: 20px 20px 20px; text-align: center; width: 100%;">
            <table id="grdProvisioningCheckListInfo"></table>
            <div id="grdProvisioningCheckListInfopager"></div>
        </div>
    </div>

</div>


