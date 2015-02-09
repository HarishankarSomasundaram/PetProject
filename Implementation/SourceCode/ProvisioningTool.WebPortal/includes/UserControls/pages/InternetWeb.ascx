<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InternetWeb.ascx.cs" Inherits="UserControlsInternetWeb" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>


<script type="text/javascript">
    //INTERNET PROVIDER GRID
    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) { siteID = 0 }

    var gridProviderWidth = "";
    var gridProviderName = "#grdInternetProviderInfo";
    var gridProviderPager = "#grdInternetProviderInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
    var gridProviderHeight = "100"
    var gridProviderSortOrder = "asc"
    var gridProviderpageSize = "20"
    var gridProviderListName = "InternetProviderList"
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a4";
    var paperOrientation = "p"; // p - portriat : l - landscap
    var getWebServiceURLProvider = baseServiceURL + masterServiceName + getService + "GETALLINTERNETPROVIDERS/Mastername/" + siteID + "/0";
    var crudWebServiceURLProvider = baseServiceURL + masterServiceName + postService + "DELETEINTERNETPROVIDERBYINTERNETPROVIDERID";
    var deleteWebServiceURLProvider = baseServiceURL + masterServiceName + postService + "DELETEINTERNETPROVIDERBYINTERNETPROVIDERID";

    function InitializeGridProvider(caption) {
        //To define the Grid for the page on the design time
        //var colnameProvder = ["ProviderID", "Provider", "Circuit ID", "Account Number", "Type", "Bandwidth", "Network ID", "Static IP Address", "Subnet", "Gateway", "Phone"];
        var colnameProvder = ["ProviderID", "Provider", "Circuit ID", "Account Number", "Type", "Bandwidth", "Network ID", "Static IP Address"];

        var colmodelProvder = [
                           { name: 'ProviderID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'Provider', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'CircutID', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'AccountNumber', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'ProviderType', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'BrandWidth', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'NetworkID', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'StaticIPAddress', sortable: true, align: "left", hidden: false, editable: true }
                           //{ name: 'Subnet',  sortable: true, align: "left", hidden: false, editable: true },
                           //{ name: 'Gateway',  sortable: true, align: "left", hidden: false, editable: true },
                           //{ name: 'Phone', sortable: true, align: "left", hidden: false, editable: true }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidden: false, editable: true }
        ];
        //Default SortColumn
        var sortNameProvider = "Provider";
        var gridProviderCaption = caption;

        //Calling the webservices and the desgining the Grid at Runtime 
        var objProviderGridList = new oDataClone(
                                        gridProviderName, // Table or Grid name in the page
                                        getWebServiceURLProvider,//Web Service URL
                                        "json", // Default dont change
                                        "GET", // Webservice Mode
                                        gridProviderCaption, //Grid Caption
                                        gridProviderpageSize, //  Number of records in each page
                                        pageSizeOption, //Page Size Option 10 20 30[this will be available in the Grid Dropdown]
                                        sortNameProvider, //Default Sortname
                                        gridProviderSortOrder, //Sort Type - desc or asc
                                        gridProviderWidth, // Grid width
                                        gridProviderHeight, // Grid height
                                        crudWebServiceURLProvider, // Add
                                        gridProviderPager, //div name in the page (gridProviderpager1)
                                        false, // is Edit Button visiable
                                        false, // is Add Button visiable
                                        true, // is Delete Button visiable
                                        false, // is Search Button visiable
                                        false, // is Refresh Button visiable
                                        false, // is Search TextBox Visiable/Enabled
                                        false, //
                                        colnameProvder, // Grid Column names
                                        colmodelProvder, // Grid Column names and properties
                                        false, // Select All options on the header and this will give checkbox for each row
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        0, //
                                        gridProviderListName, //Result Set name which is availabe in the Service
                                        true, //is New page required for Add
                                        "CustomerInfo.aspx?do=a&nav=Internet/Web-Provider", // Add URL
                                        true, //is New page required for Edit
                                        "CustomerInfo.aspx?do=e&nav=Internet/Web-Provider&id=", //Edit URL
                                        deleteWebServiceURLProvider,
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for Edit
                                        "CustomerInfo.aspx?do=m&nav=Internet/Web-Provider&id=" //View URL
                                     );
        return objProviderGridList;
    };


    //INTERNET DOMAIN GRID
    var gridDomainWidth = "";
    var gridDomainName = "#grdInternetDomainInfo";
    var gridDomainPager = "#grdInternetDomainInfopager";
    var gridDomainHeight = "100"
    var gridDomainSortOrder = "asc"
    var gridDomainpageSize = "20"
    var gridDomainListName = "InternetDomainList"
    var getWebServiceURLDomain = baseServiceURL + masterServiceName + getService + "GETALLINTERNETDOMAINS/Mastername/" + siteID + "/0";
    var crudWebServiceURLDomain = baseServiceURL + masterServiceName + postService + "DELETEINTERNETDOMAINBYINTERNETDOMAINID";
    var deleteWebServiceURLDomain = baseServiceURL + masterServiceName + postService + "DELETEINTERNETDOMAINBYINTERNETDOMAINID";


    function InitializeGridDomain(caption) {
        //To define the Grid for the page on the design time
        //var colnameDomain = ["DomainID", "Internet Domain", "Registrar", "Account ID", "Password", "Expiration", "Admin Panel", "DNS Managed", "Name Server", "Phone"];
        var colnameDomain = ["DomainID", "Internet Domain", "Registrar", "Account ID", "Password", "Expiration", "Admin Panel", "DNS Managed", "Server Name", "Phone"];
        var colmodelDomain = [
                           { name: 'DomainID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'Domain', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Registrar', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'AccountID', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'DomainPassword', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Expiration', sortable: true, hidden: false, align: "center", editable: true, search: false, formatter: 'date', formatoptions: { newformat: 'm-d-Y' } },
                           { name: 'AdminPanel', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'DNSManaged', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Server',  sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Phone', sortable: true, align: "left", hidden: false, editable: true }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidden: false, editable: true }
        ];
        //Default SortColumn
        var sortNameDomain = "Registrar";
        var gridDomainCaption = caption;

        //Calling the webservices and the desgining the Grid at Runtime 
        var objDomainGridList = new oDataClone(
                                        gridDomainName, // Table or Grid name in the page
                                        getWebServiceURLDomain,//Web Service URL
                                        "json", // Default dont change
                                        "GET", // Webservice Mode
                                        gridDomainCaption, //Grid Caption
                                        gridDomainpageSize, //  Number of records in each page
                                        pageSizeOption, //Page Size Option 10 20 30[this will be available in the Grid Dropdown]
                                        sortNameDomain, //Default Sortname
                                        gridDomainSortOrder, //Sort Type - desc or asc
                                        gridDomainWidth, // Grid width
                                        gridDomainHeight, // Grid height
                                        crudWebServiceURLDomain, // Add
                                        gridDomainPager, //div name in the page (gridDomainpager1)
                                        false, // is Edit Button visiable
                                        false, // is Add Button visiable
                                        true, // is Delete Button visiable
                                        false, // is Search Button visiable
                                        false, // is Refresh Button visiable
                                        false, // is Search TextBox Visiable/Enabled
                                        false, //
                                        colnameDomain, // Grid Column names
                                        colmodelDomain, // Grid Column names and properties
                                        false, // Select All options on the header and this will give checkbox for each row
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        0, //
                                        gridDomainListName, //Result Set name which is availabe in the Service
                                        true, //is New page required for Add
                                        "CustomerInfo.aspx?do=a&nav=Internet/Web-Domain", // Add URL
                                        true, //is New page required for Edit
                                       "CustomerInfo.aspx?do=e&nav=Internet/Web-Domain&id=", //Edit URL
                                        deleteWebServiceURLDomain,
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for Edit
                                       "CustomerInfo.aspx?do=m&nav=Internet/Web-Domain&id=" //View URL
                                     );
        return objDomainGridList;
    };


    //INTERNET WEBHOST GRID
    var gridWebHostWidth = "";
    var gridWebHostName = "#grdInternetWebHostInfo";
    var gridWebHostPager = "#grdInternetWebHostInfopager";
    var gridWebHostHeight = "100"
    var gridWebHostSortOrder = "asc"
    var gridWebHostpageSize = "20"
    var gridWebHostListName = "InternetWebHostList"
    var getWebServiceURLWebHost = baseServiceURL + masterServiceName + getService + "GETALLINTERNETWEBHOSTS/Mastername/" + siteID + "/0";
    var crudWebServiceURLWebHost = baseServiceURL + masterServiceName + postService + "DELETEINTERNETWEBHOSTBYINTERNETWEBHOSTID";
    var deleteWebServiceURLWebHost = baseServiceURL + masterServiceName + postService + "DELETEINTERNETWEBHOSTBYINTERNETWEBHOSTID";


    function InitializeGridWebHost(caption) {
        //To define the Grid for the page on the design time
        //var colnameWebHost = ["WebHostID", "Web Host", "Provider", "Account ID", "Password", "IP Address", "Admin Panel", "DNS Managed", "Name Server", "Phone"];
        var colnameWebHost = ["WebHostID", "Web Host", "Provider", "Account ID", "Password", "IP Address", "Admin Panel", "DNS Managed", "Server Name", "Phone"];
        var colmodelWebHost = [
                           { name: 'WebHostID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'WebHost', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Provider', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'AccountID', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'WebHostPassword', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'IPAddress', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'AdminPanel', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'DNSManaged', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'NameServer',  sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Phone', sortable: true, align: "left", hidden: false, editable: true }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidden: false, editable: true }
        ];
        //Default SortColumn
        var sortNameWebHost = "WebHost";
        var gridWebHostCaption = caption;
        //Calling the webservices and the desgining the Grid at Runtime 
        var objWebHostGridList = new oDataClone(
                                        gridWebHostName, // Table or Grid name in the page
                                        getWebServiceURLWebHost,//Web Service URL
                                        "json", // Default dont change
                                        "GET", // Webservice Mode
                                        gridWebHostCaption, //Grid Caption
                                        gridWebHostpageSize, //  Number of records in each page
                                        pageSizeOption, //Page Size Option 10 20 30[this will be available in the Grid Dropdown]
                                        sortNameWebHost, //Default Sortname
                                        gridWebHostSortOrder, //Sort Type - desc or asc
                                        gridWebHostWidth, // Grid width
                                        gridWebHostHeight, // Grid height
                                        crudWebServiceURLWebHost, // Add
                                        gridWebHostPager, //div name in the page (gridWebHostpager1)
                                        false, // is Edit Button visiable
                                        false, // is Add Button visiable
                                        true, // is Delete Button visiable
                                        false, // is Search Button visiable
                                        false, // is Refresh Button visiable
                                        false, // is Search TextBox Visiable/Enabled
                                        false, //
                                        colnameWebHost, // Grid Column names
                                        colmodelWebHost, // Grid Column names and properties
                                        false, // Select All options on the header and this will give checkbox for each row
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        0, //
                                        gridWebHostListName, //Result Set name which is availabe in the Service
                                        true, //is New page required for Add
                                        "CustomerInfo.aspx?do=a&nav=Internet/Web-WebHost", // Add URL
                                        true, //is New page required for Edit
                                       "CustomerInfo.aspx?do=e&nav=Internet/Web-WebHost&id=", //Edit URL
                                        deleteWebServiceURLWebHost,
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for Edit
                                       "CustomerInfo.aspx?do=m&nav=Internet/Web-WebHost&id=" //view URL
                                     );
        return objWebHostGridList;
    };


    //INTERNET EMAILHOST GRID
    var gridEmailHostWidth = "";
    var gridEmailHostName = "#grdInternetEmailHostInfo";
    var gridEmailHostPager = "#grdInternetEmailHostInfopager";
    var gridEmailHostHeight = "100"
    var gridEmailHostSortOrder = "asc"
    var gridEmailHostpageSize = "20"
    var gridEmailHostListName = "InternetEmailHostList"
    var getWebServiceURLEmailHost = baseServiceURL + masterServiceName + getService + "GETALLINTERNETEMAILHOSTS/Mastername/" + siteID + "/0";
    var crudWebServiceURLEmailHost = baseServiceURL + masterServiceName + postService + "DELETEINTERNETEMAILHOSTBYINTERNETEMAILHOSTID";
    var deleteWebServiceURLEmailHost = baseServiceURL + masterServiceName + postService + "DELETEINTERNETEMAILHOSTBYINTERNETEMAILHOSTID";

    function InitializeGridEmailHost(caption) {
        //To define the Grid for the page on the design time
        var colnameEmail = ["EmailHostID", "Email Host", "Provider", "Account Login", "Password", "IP Address", "Admin Panel", "DNS Managed", "Server Name", "Phone"];

        var colmodelEmail = [
                           { name: 'EmailHostID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'EmailHosting', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Provider',  sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'AccountLogin', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'EmailPassword', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'IPAddress', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'AdminPanel', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'DNSManaged', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'NameServers', sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Phone', sortable: true, align: "left", hidden: false, editable: true }
                           //{ name: 'View', width: 50, sortable: true, align: "left", hidden: false, editable: true }
        ];
        //Default SortColumn
        var sortNameEmailHost = "EmailHosting";
        var gridEmailHostCaption = caption;

        //Calling the webservices and the desgining the Grid at Runtime 
        var objEmailGridList = new oDataClone(
                                        gridEmailHostName, // Table or Grid name in the page
                                        getWebServiceURLEmailHost,//Web Service URL
                                        "json", // Default dont change
                                        "GET", // Webservice Mode
                                        gridEmailHostCaption, //Grid Caption
                                        gridEmailHostpageSize, //  Number of records in each page
                                        pageSizeOption, //Page Size Option 10 20 30[this will be available in the Grid Dropdown]
                                        sortNameEmailHost, //Default Sortname
                                        gridEmailHostSortOrder, //Sort Type - desc or asc
                                        gridEmailHostWidth, // Grid width
                                        gridEmailHostHeight, // Grid height
                                        crudWebServiceURLEmailHost, // Add
                                        gridEmailHostPager, //div name in the page (gridEmailHostpager1)
                                        false, // is Edit Button visiable
                                        false, // is Add Button visiable
                                        true, // is Delete Button visiable
                                        false, // is Search Button visiable
                                        false, // is Refresh Button visiable
                                        false, // is Search TextBox Visiable/Enabled
                                        false, //
                                        colnameEmail, // Grid Column names
                                        colmodelEmail, // Grid Column names and properties
                                        false, // Select All options on the header and this will give checkbox for each row
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        false, //
                                        0, //
                                        gridEmailHostListName, //Result Set name which is availabe in the Service
                                        true, //is New page required for Add
                                         "CustomerInfo.aspx?do=a&nav=Internet/Web-Email", // Add URL
                                        true, //is New page required for Edit
                                       "CustomerInfo.aspx?do=e&nav=Internet/Web-Email&id=", //Edit URL
                                        deleteWebServiceURLEmailHost,
                                        paperSize,
                                        paperOrientation,
                                        true, //is New page required for view
                                       "CustomerInfo.aspx?do=m&nav=Internet/Web-Email&id=" //View URL
                                     );
        return objEmailGridList;
    };

    $(document).ready(function () {
        $('#grdInternetProviderInfo').jqGrid('GridUnload');
        $('#grdInternetDomainInfo').jqGrid('GridUnload');
        $('#grdInternetWebHostInfo').jqGrid('GridUnload');
        $('#grdInternetEmailHostInfo').jqGrid('GridUnload');
        jqGridGenerator1(InitializeGridProvider("Provider"));
        $("#del_grdInternetProviderInfo").insertAfter("#pg_grdInternetProviderInfopager .ui-pg-button:nth(3)");
        jqGridGenerator2(InitializeGridDomain("Domain"));
        $("#del_grdInternetDomainInfo").insertAfter("#pg_grdInternetDomainInfopager .ui-pg-button:nth(3)");
        jqGridGenerator3(InitializeGridWebHost("Web Host"));
        $("#del_grdInternetWebHostInfo").insertAfter("#pg_grdInternetWebHostInfopager .ui-pg-button:nth(3)");
        jqGridGenerator4(InitializeGridEmailHost("Email Host"));
        //This is hidded because there is no requirment from client
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();
        $("#del_grdInternetEmailHostInfo").insertAfter("#pg_grdInternetEmailHostInfopager .ui-pg-button:nth(3)");

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

<div class="innerTabContent">
    <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
    </p>

    <div id="CrudInternetWeb" runat="server" class="siteDetail" style="padding-top: 0px; margin-left: 10px">
        <div id="CrudProvider" runat="server" class="contentDetail" style="padding-top: 0px; margin-left: 10px">

            <div id="divProviderDetail" runat="server" class="scrollabow" name="top">
                <div class="inlineProperty ">
                    <label>
                        Provider
                             <asp:RequiredFieldValidator ID="rfvProvider" runat="server"
                                 ControlToValidate="txtProvider" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                 ValidationGroup="IWReq">*</asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox Text="" ID="txtProvider" TabIndex="1" class="watermark" placeholder="Provider" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="256"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Circuit ID
                    </label>
                    <asp:TextBox Text="" ID="txtCircutID" TabIndex="2" class="watermark" placeholder="Circuit ID" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Account Number
                    </label>
                    <asp:TextBox Text="" ID="txtAccountNumber" TabIndex="3" class="watermark" placeholder="Account Number" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Provider Type
                    </label>
                    <asp:TextBox Text="" ID="txtProviderType" TabIndex="4" class="watermark" placeholder="Provider Type" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>

                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        Bandwidth
                    </label>
                    <asp:TextBox Text="" ID="txtBrandWidth" TabIndex="5" class="watermark" placeholder="Bandwidth" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Network ID
                    </label>
                    <asp:TextBox Text="" ID="txtNetworkID" TabIndex="6" class="watermark" placeholder="Network ID" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Static IP Address<asp:RegularExpressionValidator ID="revStaticIP" runat="server"
                                ControlToValidate="txtStaticIPAddress" 
                                ErrorMessage="Invalid IP Address"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="IWReq">
                            </asp:RegularExpressionValidator>
                    </label>
                    <asp:TextBox Text="" ID="txtStaticIPAddress" TabIndex="7" class="watermark ipaddress" placeholder="Static IP Address"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Subnet<asp:RegularExpressionValidator ID="revSubnet" runat="server"
                                ControlToValidate="txtSubnet" 
                                ErrorMessage="Invalid Subnet"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="IWReq">
                            </asp:RegularExpressionValidator>
                    </label>
                    <asp:TextBox Text="" ID="txtSubnet" TabIndex="8" class="watermark ipaddress" placeholder="Subnet"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>
                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        Gateway
                      <asp:RegularExpressionValidator ID="revGateway" runat="server"
                                ControlToValidate="txtGateway" 
                                ErrorMessage="Invalid Gateway"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="IWReq">
                            </asp:RegularExpressionValidator>   
                    </label>
                    <asp:TextBox Text="" ID="txtGateway" TabIndex="9" class="watermark ipaddress" placeholder="Gateway"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Phone
                         
                    </label>
                    <asp:TextBox Text="" ID="txtPhone" TabIndex="10" class="watermark IntegerValidation" TextMode="Phone" placeholder="(000) 000-0000" data-validation="usphone" data-validation-allowing="()-" data-validation-optional="true"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="clear"></div>
            </div>
            <asp:Button ID="btnSubmitProvider" TabIndex="11" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="IWReq" OnClick="btnSubmitProvider_Click" href="#top" />
            <asp:Button ID="btnBackProvider" TabIndex="12" CssClass="actionBtn" runat="server" Text="Back" OnClick="btnBackProvider_Click" />
            <div class="clear"></div>
        </div>

        <div id="CrudDomain" runat="server" class="siteDetail" style="padding-top: 0px; margin-left: 10px">

            <div id="divDomainDetail" runat="server" class="contentDetail scrollabow" name="top">
                <div class="inlineProperty ">
                    <label>
                        Domain
                             <asp:RequiredFieldValidator ID="rfvDomain" runat="server"
                                 ControlToValidate="txtDomain" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                 ValidationGroup="DDReq">*</asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox Text="" ID="txtDomain" TabIndex="1" class="watermark" placeholder="Domain" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="253"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Registrar
                         
                    </label>
                    <asp:TextBox Text="" ID="txtRegistrar" TabIndex="2" class="watermark" placeholder="Registrar" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="253"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Account ID
                    </label>
                    <asp:TextBox Text="" ID="txtAccountID" TabIndex="3" class="watermark" placeholder="Account ID" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Domain Password
                    </label>
                    <asp:TextBox Text="" ID="txtDomainPassword" TabIndex="4" class="watermark" placeholder="Domain Password"
                        runat="server" MaxLength="28"></asp:TextBox>
                </div>

                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        Expiration
                      
                    </label>
                    <asp:TextBox Text="" ID="txtExpiration" TabIndex="5" class="watermark expiryDate" placeholder="Expiration"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Admin Panel
                         
                    </label>
                    <asp:TextBox Text="" ID="txtAdminPanel" TabIndex="6" class="watermark" placeholder="Admin Panel" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>

                <div class="inlineProperty radioCotent" style="min-width: 122px!important;">
                    <label>DNS Managed</label>
                    <%--<asp:CheckBox Text="" ID="chkPOE" runat="server" />--%>
                    <asp:RadioButtonList runat="server" TabIndex="7" ID="txtDNSManaged" RepeatDirection="Horizontal">
                        <asp:ListItem Value="true" Text="Yes" />
                        <asp:ListItem Value="false" Text="No" />
                    </asp:RadioButtonList>

                </div>

                <div class="inlineProperty ">
                    <label>
                        Server Name
                         
                    </label>
                    <asp:TextBox Text="" ID="txtServer" TabIndex="8" class="watermark" placeholder="Server Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        Phone
                      
                    </label>
                    <asp:TextBox Text="" ID="txtDomainPhone" TabIndex="9" class="watermark IntegerValidation" TextMode="Phone" placeholder="(000) 000-0000" data-validation="usphone" data-validation-allowing="()-" data-validation-optional="true"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="clear"></div>
            </div>
            <asp:Button ID="btnSubmitDomain" CssClass="actionBtn" TabIndex="10" runat="server" Text="Submit" ValidationGroup="DDReq" OnClick="btnSubmitDomain_Click" href="#top" />
            <asp:Button ID="btnBackDomain" CssClass="actionBtn" TabIndex="11" runat="server" Text="Back" OnClick="btnBackDomain_Click" />
        </div>

        <div id="CrudEmail" runat="server" class="siteDetail" style="padding-top: 0px; margin-left: 10px">

            <div id="divEmailDetail" runat="server" class="contentDetail scrollabow" name="top">
                <div class="inlineProperty ">
                    <label>
                        Email Host
                             <asp:RequiredFieldValidator ID="rfvEHosting" runat="server"
                                 ControlToValidate="txtEmailHosting" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                 ValidationGroup="EDReq">*</asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox Text="" ID="txtEmailHosting" TabIndex="1" class="watermark" placeholder="Email Host" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="255"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Provider
                         
                    </label>
                    <asp:TextBox Text="" ID="txtEmailProvider" TabIndex="2" class="watermark" placeholder="Provider" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Account Login
                       
                    </label>
                    <asp:TextBox Text="" ID="txtAccountLogin" TabIndex="3" class="watermark" placeholder="Account Login" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Email Password
                       
                    </label>
                    <asp:TextBox Text="" ID="txtEmailPassword" TabIndex="4" class="watermark" placeholder="Email Password"
                        runat="server" MaxLength="20"></asp:TextBox>
                </div>

                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        IP Address<asp:RegularExpressionValidator ID="revIPEmail" runat="server"
                                ControlToValidate="txtIPAddress" 
                                ErrorMessage="Invalid IP Address"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="EDReq">
                            </asp:RegularExpressionValidator>                      
                    </label>
                    <asp:TextBox Text="" ID="txtIPAddress" TabIndex="5" class="watermark ipaddress" placeholder="IP Address"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Admin Panel
                      
                    </label>
                    <asp:TextBox Text="" ID="txtEmailAdminPanel" TabIndex="6" class="watermark" placeholder="Admin Panel" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>
                <div class="inlineProperty radioCotent" style="min-width: 122px!important;">
                    <label>DNS Managed</label>

                    <asp:RadioButtonList runat="server" TabIndex="7" ID="txtEmailDNSManaged" RepeatDirection="Horizontal">
                        <asp:ListItem Value="true" Text="Yes" />
                        <asp:ListItem Value="false" Text="No" />
                    </asp:RadioButtonList>

                </div>
                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        Server Name
                    </label>
                    <asp:TextBox Text="" ID="txtNameServers" TabIndex="8" class="watermark" placeholder="Server Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Phone
                    </label>
                    <asp:TextBox Text="" ID="txtEmailPhone" TabIndex="9" class="watermark IntegerValidation" TextMode="Phone" placeholder="(000) 000-0000" data-validation="usphone" data-validation-allowing="()-" data-validation-optional="true"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="clear"></div>
            </div>
            <asp:Button ID="btnSubmitEmail" CssClass="actionBtn" TabIndex="10" runat="server" Text="Submit" ValidationGroup="EDReq" OnClick="btnSubmitEmail_Click" href="#top" />
            <asp:Button ID="btnBackEmail" CssClass="actionBtn" TabIndex="11" runat="server" Text="Back" OnClick="btnBackEmail_Click" />
        </div>

        <div id="CrudWebHost" runat="server" class="siteDetail" style="padding-top: 0px; margin-left: 10px">

            <div id="divWebHostDetail" runat="server" class="contentDetail scrollabow" name="top">
                <div class="inlineProperty ">
                    <label>
                        Web Host
                             <asp:RequiredFieldValidator ID="rfvWHost" runat="server"
                                 ControlToValidate="txtWebHost" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                 ValidationGroup="WHDReq">*</asp:RequiredFieldValidator>

                    </label>
                    <asp:TextBox Text="" ID="txtWebHost" TabIndex="1" class="watermark" placeholder="Web Host" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="255"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Provider
                    </label>
                    <asp:TextBox Text="" ID="txtWebHostProvider" TabIndex="2" class="watermark" placeholder="Provider" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="64"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Account ID
                       
                    </label>
                    <asp:TextBox Text="" ID="txtWebHostAccountID" TabIndex="3" class="watermark" placeholder="Account ID" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Web Host Password
                       
                    </label>
                    <asp:TextBox Text="" ID="txtWebHostPassword" TabIndex="4" class="watermark" placeholder="Web Host Password"
                        runat="server" MaxLength="25"></asp:TextBox>
                </div>

                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        IP Address
                      <asp:RegularExpressionValidator ID="revWebIPAddress" runat="server"
                                ControlToValidate="txtWebHostIPAddress" 
                                ErrorMessage="Invalid IP Address"
                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                ValidationGroup="WHDReq">
                            </asp:RegularExpressionValidator>    
                    </label>
                    <asp:TextBox Text="" ID="txtWebHostIPAddress" TabIndex="5" class="watermark ipaddress" placeholder="IP Address"
                        runat="server" MaxLength="15"></asp:TextBox>
                </div>
                <div class="inlineProperty ">
                    <label>
                        Admin Panel
                         
                    </label>
                    <asp:TextBox Text="" ID="txtWebhostAdminPanel" TabIndex="6" class="watermark" placeholder="Admin Panel" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="inlineProperty radioCotent" style="min-width: 122px!important;">
                    <label>DNS Managed</label>
                    <%--<asp:CheckBox Text="" ID="chkPOE" runat="server" />--%>
                    <asp:RadioButtonList runat="server" ID="txtWebHostDNSManaged" TabIndex="7" RepeatDirection="Horizontal">
                        <asp:ListItem Value="true" Text="Yes" />
                        <asp:ListItem Value="false" Text="No" />
                    </asp:RadioButtonList>

                </div>
                <div class="inlineProperty ">
                    <label>
                        Server Name
                         
                    </label>
                    <asp:TextBox Text="" ID="txtWebHostNameServer" class="watermark" TabIndex="8" placeholder="Server Name" data-validation="alphanumeric" data-validation-allowing="-+()_" data-validation-optional="true"
                        runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="clear"></div>
                <div class="inlineProperty ">
                    <label>
                        Phone
                      
                    </label>
                    <asp:TextBox Text="" ID="txtWebHostPhone" class="watermark IntegerValidation" TabIndex="9" TextMode="Phone" placeholder="(000) 000-0000" data-validation="usphone" data-validation-allowing="()-" data-validation-optional="true"
                        runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="clear"></div>
            </div>
            <asp:Button ID="btnSubmitWebHost" CssClass="actionBtn" TabIndex="10" runat="server" Text="Submit" ValidationGroup="WHDReq" OnClick="btnSubmitWebHost_Click" href="#top" />
            <asp:Button ID="btnBackWebHost" CssClass="actionBtn" TabIndex="11" runat="server" Text="Back" OnClick="btnBackWebHost_Click" />
        </div>

    </div>

        <div id="divGrdInternetProviderInfo" runat="server" class="innerGrdFullWidth">
            <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
                <table id="grdInternetProviderInfo"></table>
                <div id="grdInternetProviderInfopager"></div>
            </div>
        </div>

        <div id="divGrdInternetDomainInfo" runat="server" class="innerGrdFullWidth">
            <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
                <table id="grdInternetDomainInfo"></table>
                <div id="grdInternetDomainInfopager"></div>
            </div>
        </div>


        <div id="divGrdInternetWebHostInfo" runat="server" class="innerGrdFullWidth">
            <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
                <table id="grdInternetWebHostInfo"></table>
                <div id="grdInternetWebHostInfopager"></div>
            </div>
        </div>

        <div id="divGrdInternetEmailHostInfo" runat="server" class="innerGrdFullWidth">
            <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
                <table id="grdInternetEmailHostInfo"></table>
                <div id="grdInternetEmailHostInfopager"></div>
            </div>
        </div>
    
</div>

