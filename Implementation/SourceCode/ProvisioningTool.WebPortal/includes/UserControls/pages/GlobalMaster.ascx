<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GlobalMaster.ascx.cs" Inherits="UserControlsGlobalMaster" %>

<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<script type="text/javascript">
    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) {
        siteID = 0;
        $('#headerCustomer').hide();
    }
    var customerID=   $.cookie("sessionCustomerId");

    <%if (currentUser.Role.RoleID == (int)ProvisioningTool.Entity.UserRole.SystemEngineer)
      {%>
    $('#headerMaster').hide();
    $('#headerSysEng').hide();
    <%}%>

    var gridWidth = "";
    var gridName = "#grdGlobalMasters";
    var gridPager = "#grdGlobalMasterpager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
    var GetServiceForColorBox= '<%=ConfigurationManager.AppSettings["GetServiceForColorBox"].ToString() %>';
    var gridHeight = "300";
    var gridSortOrder = "asc";
    var gridpageSize = "10";
    var gridListName = "GlobalMasterDetailList";
    var deleteWebServiceURL = "";
    var pageSizeOption = ["10", "20", "30"];
    var paperSize = "a3";
    var paperOrientation = "p"; // p - portriat : l - landscap
    //var siteWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLSITES/Site Master/" ;

    var strSearch = "";
    var strField = "";
    var strOper = "";
    var gridCaption;
    //
    //  handler: si occupa di creare il contenuto del menu a tendina (form jqGrid)
    //
    var buildSiteOptions = function (dataList) {
        var response = dataList.GlobalMasterDetailList[0].SiteList;
        //Select the site dropdown site id from response object[0]
        var myGrid = $(gridName),
        selectedRowId = myGrid.jqGrid('getGridParam', 'selrow'),
        cellValue = myGrid.jqGrid('getCell', selectedRowId, 'SiteID');
        var option = "";
        if (response && response.length) {
            for (var i = 0, l = response.length; i < l; i++) {
                if (response[i]["SiteID"] == cellValue)
                    option += '<option role="option" selected="selected" value="' + response[i]["SiteID"] + '">' + response[i]["SiteName"] + '</option>';
                else
                    option += '<option role="option" value="' + response[i]["SiteID"] + '">' + response[i]["SiteName"] + '</option>';
            }
        }

        return option;
    };

    var buildManufacturersOptions = function (dataList) {
        var response = dataList.GlobalMasterDetailList;
        //Select the site dropdown site id from response object[0]
        var myGrid = $(gridName),
        selectedRowId = myGrid.jqGrid('getGridParam', 'selrow'),
        cellValue = myGrid.jqGrid('getCell', selectedRowId, 'SubManufacturerID');
        var option = "";
        
        if (response && response.length) {
            for (var i = 0, l = response.length; i < l; i++) {

                if (response[i]["MasterDetailID"] == cellValue)
                    option += '<option role="option" selected="selected" value="' + response[i]["MasterDetailID"] + '">' + response[i]["MasterValue"] + '</option>';
                else
                    option += '<option role="option" value="' + response[i]["MasterDetailID"] + '">' + response[i]["MasterValue"] + '</option>';
            }
        }

        return option;
    };

    
    var buildTypesOptions = function (dataList) {
        var response = dataList.GlobalMasterDetailList;
        //Select the site dropdown site id from response object[0]
        var myGrid = $(gridName),
        selectedRowId = myGrid.jqGrid('getGridParam', 'selrow'),
        cellValue = myGrid.jqGrid('getCell', selectedRowId, 'SubTypeID');
        var option = "";
        
        if (response && response.length) {
            for (var i = 0, l = response.length; i < l; i++) {
                if (response[i]["MasterDetailID"] == cellValue)
                    option += '<option role="option" selected="selected" value="' + response[i]["MasterDetailID"] + '">' + response[i]["MasterValue"] + '</option>';
                else
                    option += '<option role="option" value="' + response[i]["MasterDetailID"] + '">' + response[i]["MasterValue"] + '</option>';
            }
        }
        return option;
    };


    var buildModelOptions = function (dataList) {
        var response = dataList.GlobalMasterDetailList;
        //Select the site dropdown site id from response object[0]
        var myGrid = $(gridName),
        selectedRowId = myGrid.jqGrid('getGridParam', 'selrow'),
        cellValue = myGrid.jqGrid('getCell', selectedRowId, 'SubTypeID');
        var option = "";
        if (response && response.length) {
            for (var i = 0, l = response.length; i < l; i++) {
                if (response[i]["MasterDetailID"] == cellValue)
                    option += '<option role="option" selected="selected" value="' + response[i]["MasterDetailID"] + '">' + response[i]["MasterValue"] + '</option>';
                else
                    option += '<option role="option" value="' + response[i]["MasterDetailID"] + '">' + response[i]["MasterValue"] + '</option>';
            }
        }

        return option;
    };


    function InitializeGrids(caption, gridColumnName, siteReq, manifacurers, Types, searchKey) {
        
        //Default SortColumn
        var ApplicationUserId = <%=currentUser.ApplicationUserID%>;
        var sortName = "MasterValue";

        //var isColorBox = getQueryStringByName('isColorBox');
        var getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETGLOBALMASTERANDDETAILSBYMASTERNAME/" + caption + "/"+ siteID+"/" + searchKey;
       
        var getMasterDetailsURL = baseServiceURL + masterServiceName + getService + "GETGLOBALMASTERANDDETAILSBYMASTERDETAILID/" + Types + "/"+ siteID;
        //if(isColorBox =='yes')
        //    getWebServiceURL = baseServiceURL + masterServiceName + GetServiceForColorBox + "GETGLOBALMASTERANDDETAILSBYMASTERNAME/" + caption + "/"+ siteID+"/searchtext";

        var crudWebServiceURL = baseServiceURL + masterServiceName + "GlobalMasterCrud/" + caption + "/" + ApplicationUserId;
        debugger;
        var isSiteNameRequired = false;
        var isManifacurersRequired = false;
        var isTypesRequired = false;
        var getManufacurersWebServiceURL = "";
        var getTypesWebServiceURL = "";

        if (siteReq == 1) {
            isSiteNameRequired = false;
        }
        else {
            isSiteNameRequired = true;
        }

        if (manifacurers != "" && manifacurers != 'undefined') {
            isManifacurersRequired = false;
            getManufacurersWebServiceURL = baseServiceURL + masterServiceName + getService + "GETGLOBALMASTERANDDETAILSBYMASTERNAME/"+ manifacurers +"/"+ siteID+"/"+ searchKey;
        }
        else {
            isManifacurersRequired = true;
        }

        if (Types != "" && Types != 'undefined') {
            isTypesRequired = false;
            getTypesWebServiceURL = baseServiceURL + masterServiceName + getService + "GETGLOBALMASTERANDDETAILSBYMASTERNAME/"+ Types +"/"+ siteID+"/"+searchKey;
        }
        else {
            isTypesRequired = true;
        }

        if (caption == "CRM ERP" || gridColumnName == "CRM ERP"){
            gridCaption = "CRM/ERP";
            gridColumnName = "CRM/ERP";
        }
        else{
            gridCaption = caption;
            gridColumnName = caption;
        }
        
        var emailValidation = false;

        if(caption == 'Email'){
            emailValidation = false;
            //  emailValidation = true;
        }

        //To define the Grid for the page on the design time
        var colname = ["Master Detail ID", gridColumnName, "Site ID", "Site", "Sub Manufacturer ID", "Manufacturers", "Sub Types ID", "Types", "Created On"];//"Modified By" "Created By", 

        var colmodel = [
                           { name: 'MasterDetailID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'MasterValue', sortable: true, align: "left", hidden: false, editable: true, search:false, editrules: {required: true, email:emailValidation}, editoptions:{size:30, maxlength: 75} },
                           { name: 'SiteID', sortable: true, align: "left", hidedlg: true, hidden: true, editable: true },
                           {
                               name: 'SiteName', index: 'SiteName', sortable: true, align: "left", hidedlg: isSiteNameRequired, hidden: isSiteNameRequired, editable: !isSiteNameRequired,
                               edittype: "select",
                               jsonmap: function (item) {
                                   return item.SiteName;
                               },
                               editoptions: {
                                   dataUrl: getWebServiceURL,
                                   buildSelect: function (data) {
                                       var response = $.parseJSON(data);
                                       var option = buildSiteOptions(response);
                                       var s = '<select>';
                                       s += option;
                                       return s + "</select>";
                                   }
                               }
                           },
                           { name: 'SubManufacturerID', sortable: true, align: "left", hidedlg: true, hidden: true, editable: true },
                           {
                               name: 'Manufacturers', index: 'Manufacturers', sortable: true, align: "left", editrules: {required: !isManifacurersRequired}, hidedlg: isManifacurersRequired, hidden: isManifacurersRequired, editable: !isManifacurersRequired,
                               edittype: "select",
                               jsonmap: function (item) {
                                   return item.Manufacturers;
                               },
                               editoptions: {
                                   dataUrl: getManufacurersWebServiceURL,
                                   buildSelect: function (data) {
                                       var response = $.parseJSON(data);
                                       var option = buildManufacturersOptions(response);
                                       var s = '<select><option></option>';
                                       s += option;
                                       return s + "</select>";
                                   },
                                   dataEvents: [
                                                    {  type: 'change',
                                                        fn: function(e) {
                                                            var thisval = $(e.target).val();
                                                            $.get(getMasterDetailsURL+"/"+thisval, function(data)
                                                            {
                                                                var option = buildModelOptions(data);
                                                                $("#Types").html(option);
                                                            }); // end get
                                                        }
                                                    }
                                                    
                                   ],
                                   style: "width: 150px"
                               }
                           },
                           { name: 'SubTypeID', sortable: true, align: "left", hidedlg: true, hidden: true, editable: true },
                           {
                               name: 'Types', index: 'Types', sortable: true, align: "left", hidedlg: isTypesRequired, editrules: {required: !isTypesRequired}, hidden: isTypesRequired, editable: !isTypesRequired,
                               edittype: "select",
                               jsonmap: function (item) {
                                   return item.Types;
                               },
                               editoptions: {
                                   dataUrl: getTypesWebServiceURL,
                                   buildSelect: function (data) {
                                       var response = $.parseJSON(data);
                                       var option = buildTypesOptions(response);
                                       if (option != ''){
                                           var s = '<select><option></option>';
                                           s += option;
                                           return s + "</select>";
                                       }
                                       else{
                                           return "<select><option></option></select>";
                                       }

                                   },
                                   dataEvents: [ 
                                           {type: 'mouseover',
                                               fn: function(e) {
                                                   var thisval = $("#Manufacturers").val();
                                                   if (thisval != ""){
                                                       $.get(getMasterDetailsURL+"/"+thisval, function(data)
                                                       {
                                                           var option = buildTypesOptions(data);
                                                           var s = '<select>';
                                                           s += option;
                                                           s += "</select>";
                                                           $("#Types").html(s);
                                                       }); // end get
                                                   }
                                                   else{
                                                       return "<select><option></option></select>";
                                                   }
                                               }
                                           }
                                   ],
                                   style: "width: 150px"
                               }
                           },
                           { name: 'CreatedOn', sortable: true, hidden: false, align: "center", editable: false, search: false, formatter: 'date', formatoptions: { newformat: 'm-d-Y' } }

        ];

        delete oData;
        delete objGridList;
        
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
                                        true, // is Edit Button visiable
                                        true, // is Add Button visiable
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
                                        false, //is New page required for Add
                                        "", // Add URL
                                        false, //is New page required for Edit
                                        "", //Edit URL
                                        deleteWebServiceURL,
                                        paperSize,
                                        paperOrientation
                                     );
        return objGridList;
    };


    $(document).ready(function () {
        //Set the Headder name on Add or Update clicked on Grid
        var iframe = GetParameterValues('iframe');
        var ititle = GetParameterValues('iTitle')
        //alert(iframe + '<--ifram');
        if (iframe != '' && iframe != undefined) {
            $('.sideBar').hide();
            $('.nav').hide();
            $('.profileAction').hide();
            $('.logo a').attr('href','#');
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            iframe = iframe.replace("%20", " ");
            ititle = ititle.replace("%20", " ");
            ButtonEvent(iframe,ititle);

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

        //$('.ui-icon-pencil').click(function () {
        //    var grid = $("#grdGlobalMasters");
            
        //    var rowid = grid.jqGrid('getGridParam', 'selrow');
            
        //    if (rowid == null || rowid == undefined) {
        //        //alert("Please, Select Row to Modify.")
        //        $("#dialog-message").show();
        //        $("#dialog-message").dialog({
        //            modal: true,
        //            buttons: {
        //                Ok: function () {
        //                    $(this).dialog("close");
        //                }
        //            }
        //        });
        //        $(".ui-dialog-title").html('Information !');
        //    }
        //});


        var hidglobal = GetParameterValues('hidglobal');
        var hidTabname = GetParameterValues('tabName');
        var types = GetParameterValues('types');
        var models = GetParameterValues('models');
        
        if (typeof types !== "undefined"){
            types = types.replace('-', " ");
            types = types.replace('-', " ");
        }

        if (typeof models !== "undefined"){
            models = models.replace('-', " ");
            models = models.replace('-', " ");
        }

        if (hidglobal == "1"){
            hidTabname = hidTabname.replace("%20", " ");
            hidTabname = hidTabname.replace('-', " ");
            hidTabname = hidTabname.replace('-', " ");
            hidTabname = hidTabname.replace('-', " ");
            hidTabname = hidTabname.replace('-', " ");
            ButtonEvent(hidTabname, hidTabname, hidTabname, types, models);
        }


    });

    var isReloaded = false;

    function ButtonEvent(caption, gridColumnName, gridTitle, manifacurers, Types) {
        $('#customer').hide();
        $('#innerTab').show();
        $('#grdGlobalMasters').jqGrid('GridUnload');
        $("#lblHeader").text(caption);
        $('#hidTabname').val(caption);
        
        var txtSearch = $("#txtSearch").val();
        
        if (txtSearch == "") {
            txtSearch = 0
        }

        jqGridGenerator(InitializeGrids(caption, gridColumnName, 0, manifacurers, Types, txtSearch));
        $('.ui-icon-calculator').hide();

        isReloaded = false;
        return false;
    }

  

    //Search button 
    $("#btnSearchSubmit").click(function () {

        $('#globalMaster').jqGrid('GridUnload');

        var txtSearch = $("#txtSearch").val();
        
        if (txtSearch == "") {
            txtSearch = 0
        }
            
        jqGridGenerator(InitializeGrids(caption, gridColumnName, 0, manifacurers, Types, txtSearch));
        $('.ui-icon-calculator').hide();

    });

</script>

<div id="customSearch" clientidmode="Static" runat="server">
    <div class="inlineProperty">
        <asp:TextBox Text="" ID="txtSearch" placeholder="Search" ClientIDMode="Static"
            runat="server" MaxLength="50"></asp:TextBox>
    </div>

    <div class="inlineProperty">
        <asp:Button ID="btnSearchSubmit" CssClass="actionBtn" runat="server" Text="Search" ClientIDMode="Static" />
        <asp:Button ID="btnSearchClear" CssClass="actionBtn" runat="server" Text="Clear" ClientIDMode="Static" />
    </div>
</div>

<div id="globalMaster" runat="server">
    <table id="grdGlobalMasters"></table>
    <div id="grdGlobalMasterpager"></div>
</div>
