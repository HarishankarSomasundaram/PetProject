<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" EnableViewState="false" Inherits="Settings " %>

<%@ Register Src="~/includes/UserControls/common/Master.ascx" TagName="Master" TagPrefix="ProvisioningTool" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" class="iframeBodyClass">
    <title>Administrator</title>

    <ProvisioningTool:Master ID="Master" runat="server" />
    <script type="text/javascript">

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
            //To define the Grid for the page on the design time
            //var colname = ["Action", "Customer Code", "Customer Name", "Address", "Phone Number"]; //"Customer ID", 
            var colname = ["Customer Code", "Customer Name", "Action", "Customer ID"]; //"Customer ID", 

            var colmodel = [
                               //{ name: 'View', sortable: true, align: "center", hidedlg: false, hidden: false, editable: false, search: false },
                               { name: 'CustomerCode', sortable: true, align: "left", hidden: false, editable: false, search: false },
                               { name: 'CustomerName', sortable: true, align: "left", hidden: false, editable: false, search: false },
                               { name: 'View', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               //{ name: 'Address', sortable: true, hidden: false, editable: true, search: false },  hidedlg: false,
                               { name: 'CustomerID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true },
                               ////{ name: 'CompanyName', sortable: true, hidden: false, editable: true, search: true },
                               //{ name: 'PhoneNumber', sortable: true, align: "left", hidden: false, editable: false, search: false }

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
                                            "CustomerSearch.aspx?do=a", // Add URL
                                            false, //is New page required for Edit
                                            "CustomerSearch.aspx?do=e&id=", //Edit URL
                                            deleteWebServiceURL,
                                             "a2",
                                             "p",
                                            true, //is New page required for view
                                            "CustomerSites.aspx?CID=" //view URL
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
            $.cookie('CID', null, { expires: -1 });

            $(".inline").colorbox({
                rel: 'inline',
                inline: true,
                href: $(this).attr('href'),
                height: '85%',
                transition: 'none'
            });

            $("#txtCustomerCode").bind('keypress', function (e) {
                if (e.which === 13) {
                    $('#btnSearch').trigger('click');
                }
            });


            $("#btnSearch").click(function () {
                $('#grdCustomerMasters').jqGrid('GridUnload');

                var CustomerCode = $("#txtCustomerCode").val();

                //Check if the value is empty if so all the data should be fetched
                if (CustomerCode == "")
                    CustomerCode = "all";

                if (CustomerCode == "all")//&& CustomerName == "all" && CompanyName == "all" && PhoneNumber == "all")//If nothing is given then get all the Customer Data
                {
                    gridServiceURL = "GetCustomerBySearchKey/all/all/all/Admin/" + caption; 
                    jqGridGenerator(InitializeGrid(caption, gridServiceURL));

                }
                else {
                    gridServiceURL = "GetCustomerBySearchKey/" + CustomerCode + "/all/all/Admin/" + caption;
                    jqGridGenerator(InitializeGrid(caption, gridServiceURL));
                }
                //This is hidded because there is no requirment from client
                $('.ui-icon-excel').hide();
                $('.ui-icon-pdf').hide();
                return false;
            });




            $('#iCustomerSites').click(function () {
                $('#grdCustomerMasters').jqGrid('GridUnload');
                $('#txtCustomerCode').val('');

                var CustomerCode = "";

                //Check if the value is empty if so all the data should be fetched
                if (CustomerCode == "")
                    CustomerCode = "all";

                if (CustomerCode == "all")//&& CustomerName == "all" && CompanyName == "all" && PhoneNumber == "all")//If nothing is given then get all the Customer Data
                {
                    gridServiceURL = "GetCustomerBySearchKey/all/all/all/Admin/" + caption;
                    jqGridGenerator(InitializeGrid(caption, gridServiceURL));

                }
                else {
                    gridServiceURL = "GetCustomerBySearchKey/" + CustomerCode + "/all/all/Admin/" + caption;
                    jqGridGenerator(InitializeGrid(caption, gridServiceURL));
                }
                //This is hidded because there is no requirment from client
                $('.ui-icon-excel').hide();
                $('.ui-icon-pdf').hide();

            });

        });



    </script>
</head>
<body id="PageBody" runat="server" class="adminContent">
    <form id="Custmain" runat="server">
        <div class="fullWidthGrid">

            <div class="leftMenuWrapper">
                <ul>
                    <li>
                        <div class="largeNav" id="iCustomer">
                            <img src="../../includes/UI/images/customerLarge.png" />
                            <h3>Customers</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iUsers">
                            <img src="../../includes/UI/images/userLargeIcon.png" />
                            <h3>Users</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iSettings">
                            <img src="../../includes/UI/images/settingslargeIcon.png" />
                            <h3>Settings</h3>
                        </div>
                    </li>

                    <li>
                        <asp:LinkButton runat="server" ID="lnksearchCustomer" href="#searchCustomer" CssClass="inline" ClientIDMode="Static">
                        <div class="largeNav" id="iCustomerSites">
                            
                            <img src="../../includes/UI/images/customerSites.png"  />
                            <h3>Customer Sites</h3>
                        </div>
                        </asp:LinkButton>
                    </li>
                </ul>

            </div>
        </div>


        <div style="display: none;">
            <div id="searchCustomer" class="popupSearch">
                <article class="widget">
                    <div class="headerSection">
                        <h1>Customer Search</h1>
                    </div>
                    <div class="widgetContentWrap">
                        <div class="widgetContent">
                            <div class="inlineProperty">
                                <asp:TextBox Text="" ID="txtCustomerCode" class="watermark" placeholder="Search"
                                    runat="server" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="inlineProperty">
                                <asp:Button ID="btnSearch" CssClass="actionBtn" ClientIDMode="Static" runat="server" Text="Search" />
                            </div>
                            <div class="clear"></div>
                            <%--<div id='loadingDiv'></div>--%>
                            <div class="clear"></div>
                            <div id="grdCustomer" runat="server" clientidmode="Static">
                                <div style="padding: 15px 0px; margin-right: 10px; text-align: center;">
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
        <footer>
            <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
        </footer>
    </form>

</body>
</html>
