<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="App_Masters_Search" %>

<%--<%@ Import Namespace="System.Web.Optimization" %>--%>

<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>
<!DOCTYPE html>
<html>
<head id="search" runat="server">
    <title>Search</title>
    <ProvisioningTool:Includes ID="Includes" runat="server" />
    <ProvisioningTool:Header ID="Header" runat="server" />
    <ProvisioningTool:Footer ID="Footer" runat="server" />

    <script type="text/javascript">

        <%if (currentUser.Role.RoleID == (int)ProvisioningTool.Entity.UserRole.SystemEngineer)
          {%>
        $('#headerMaster').hide();
        $('#headerSysEng').hide();
        <%}  %>

        var gridWidth = ""
        var gridName = "#grdCustomerMasters";
        var gridPager = "#grdCustomerpager";
        var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
        var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
        var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
        var gridHeight = "200"
        var gridSortOrder = "asc"
        var gridpageSize = "10";
        var gridListName = "CustomerList"
        var pageSizeOption = ["10", "20", "30"];

        function InitializeGrid(caption, gridServiceURL) {
            var pageWidth = $("#contentWrap").width();
            //To define the Grid for the page on the design time
            var colname = ["Action", "Customer Code", "Customer Name", "Address","Customer ID", "Phone Number"]; //"Customer ID", 

            var colmodel = [
                               { name: 'View', sortable: true, align: "center", hidedlg: false, hidden: false, editable: false, search: false },
                               { name: 'CustomerCode', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'CustomerName', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'Address', sortable: true, hidden: false, editable: true, search: false },
                               { name: 'CustomerID', key: true,  align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                               //{ name: 'CompanyName', sortable: true, hidden: false, editable: true, search: true },
                               { name: 'PhoneNumber', sortable: true, align: "center", hidden: false, editable: false, search: false }

            ];
            //Default SortColumn
            var sortName = "CustomerName";
            var gridCaption = caption;
            var getWebServiceURL = baseServiceURL + masterServiceName + gridServiceURL;
            var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETECUSTOMERBYCUSTOMERID";
            var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETECUSTOMERBYCUSTOMERID";
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
                                            "CustomerInfo.aspx?do=a", // Add URL
                                            false, //is New page required for Edit
                                            "CustomerInfo.aspx?do=e&id=", //Edit URL
                                            deleteWebServiceURL,
                                            "a2",
                                             "p"
                                            //true, //is New page required for view
                                            //"CustomerInfo.aspx?do=m&mode=s&id=" //view URL
                                         );
            return objGridList;
        };

        $(document).ready(function () {

            var caption = "Customer List";
            var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
            $('#grdCustomerMasters').jqGrid('GridUnload');
            $('.ui-jqgrid-title').text(caption);
            $(gridName).width(gridWidth);
            $('.ui-jqgrid-htable').width(gridWidth);
            $('#headerCustomer').hide();
            $('#headerSearch').hide();
            $.cookie('siteID', null, { expires: 1 });
            $.cookie('lefttab', null, { expires: 1 });

            caption = "Customer List";
            var CustomerCode = $("#txtCustomerCode").val();
            var CustomerName = $("#txtCustomerName").val();
            var CompanyName = "";
            var PhoneNumber = $("#txtPhoneNumber").val();

            //Check if the value is empty if so all the data should be fetched
            if (CustomerCode == "")
                CustomerCode = "all";

            $("#btnSubmit").click(function () {
                $('#grdCustomerMasters').jqGrid('GridUnload');

                var CustomerCode = $("#txtCustomerCode").val();

                //Check if the value is empty if so all the data should be fetched
                if (CustomerCode == "")
                    CustomerCode = "all";

                if (CustomerCode == "all")//&& CustomerName == "all" && CompanyName == "all" && PhoneNumber == "all")//If nothing is given then get all the Customer Data
                {
                    gridServiceURL = "GetCustomerBySearchKey/all/all/all/User/" + caption;
                    jqGridGenerator(InitializeGrid(caption, gridServiceURL));

                }
                else {
                    gridServiceURL = "GetCustomerBySearchKey/" + CustomerCode + "/all/all/User/" + caption;
                    jqGridGenerator(InitializeGrid(caption, gridServiceURL));
                }
                //This is hidded because there is no requirment from client
                $('.ui-icon-excel').hide();
                $('.ui-icon-pdf').hide();
                return false;
            });

            $("#btnClear").click(function () {
                $("#txtCustomerCode").val('');
                $("#txtCustomerName").val('');
                //$("#txtCompanyName").val('');
                $("#txtPhoneNumber").val('');
                $("#btnSubmit").click();
                return false;
            });

            gridServiceURL = "GetCustomerBySearchKey/" + CustomerCode + "/all/all/all/" + caption;

            jqGridGenerator(InitializeGrid(caption, gridServiceURL));
            //This is hidded because there is no requirment from client
            $('.ui-icon-excel').hide();
            $('.ui-icon-pdf').hide();
            $('.ui-icon-calculator').hide();
            return false;
        });


    </script>
    <style type="text/css">
        .inlineProperty {
            min-width: 214px;
            display: inline-block;
            margin: 0 10px 10px 0;
            width: 200px;
            vertical-align: bottom;
        }
    </style>
</head>
<body>
    <form id="frmSearch" runat="server">
        <div id="pdfContainer" style="display: block;"></div>
        <div id="dialog" title="Warning" class="dialog">
            <div>Please, select row</div>
        </div>
        <div class="contentWrapText noMrginLeft  ">
            <section id="contentWrap">
                <div class="container doubleColumn">

                    <div class="hideContent toggle">

                        <div id="widgetContentWrap">
                            <article class="widget singleColHeight" style="width: 100%; min-width: 1100px;">
                                <div class="headerSection">
                                    <h1>Customer Search</h1>
                                </div>

                                <div class="widgetContentWrap">
                                    <div class="widgetContent">
                                        <%--                                        <div class="inlineProperty">
                                            <label style="font-weight: bold;">
                                                Company Name
                                            </label>
                                            <asp:TextBox Text="" ID="txtCompanyName" class="watermark" placeholder="Company Name"
                                                runat="server" MaxLength="100"></asp:TextBox>
                                        </div>--%>
                                        <div class="inlineProperty">
                                            <asp:TextBox Text="" ID="txtCustomerCode" placeholder="Search"
                                                runat="server" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <%--                                        <div class="inlineProperty">
                                            <label style="font-weight: 400;">
                                                Customer Name
                                            </label>
                                            <asp:TextBox Text="" ID="txtCustomerName" class="watermark" placeholder="Customer Name"
                                                runat="server" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="inlineProperty">
                                            <label style="font-weight: 400;">
                                                Phone Number
                                            </label>
                                            <asp:TextBox Text="" ID="txtPhoneNumber" class="watermark" placeholder="Phone Number"
                                                runat="server" MaxLength="100"></asp:TextBox>
                                        </div>--%>

                                        <div class="inlineProperty">
                                            <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Search" ValidationGroup="Req" />
                                            <asp:Button ID="btnClear" CssClass="actionBtn" runat="server" Text="Clear" />
                                        </div>
                                        <div class="clear"></div>

                                        <div id="grdCustomer" runat="server">
                                            <div style="padding-top: 25px; text-align: center; width: 100%">
                                                <table id="grdCustomerMasters"></table>
                                                <div id="grdCustomerpager"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
            </section>
        </div>
        <div class="clear"></div>
        <footer>
            <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
        </footer>
    </form>
</body>
</html>

