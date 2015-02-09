function jqGridGenerator1(jsonObj) {

    var sortorder = jsonObj.sortorder;

    if (sortorder == null || sortorder == "undefined" || sortorder == "" || sortorder != "desc") {
        sortorder = "asc"
    }
    else {
        sortorder = "desc"
    }


    var Data;
    var userid;
    userid = 1;
    jQuery.extend(jQuery.jgrid.edit, {
        //If set to true this closes the edit dialog after the user apply a edit - i.e. click on Find button
        savekey: [true, 13],
        closeOnEscape: true,
        recreateForm: true,
        ajaxEditOptions: { contentType: "application/json" },
        reloadAfterSubmit: true,

        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the postData array.
        serializeEditData: function (postData) {
            //var postdata = { 'data': postData };
            
            var paraUrl = jsonObj.editurl;

            if (postData.hasOwnProperty('CreatedOn')) {//Check if poperty exist then remove
                delete postData.CreatedOn;
            }
            if (postData.hasOwnProperty('MasterDetailID')) {//Check if poperty exist then remove
                delete postData.MasterDetailID;
            }
            if (postData.hasOwnProperty('ModifiedBy')) {//Check if poperty exist then remove
                delete postData.ModifiedBy;
            }


            
            if (postData.oper == "add") {
                if (postData.hasOwnProperty('id')) {//Check if poperty exist then remove
                    delete postData.id;
                }
                if (postData.hasOwnProperty('SiteID')) {//Check if poperty exist then remove
                    delete postData.SiteID;
                }
                if (postData.hasOwnProperty('ProviderID')) {//Check if poperty exist then remove
                    delete postData.ProviderID;
                }
                
                if (postData.hasOwnProperty('DomainID')) {//Check if poperty exist then remove
                    postData.DomainID = 0;
                }
                if (postData.hasOwnProperty('WebHostID')) {//Check if poperty exist then remove
                    postData.WebHostID = 0;
                }
                if (postData.hasOwnProperty('EmailHostID')) {//Check if poperty exist then remove
                    postData.EmailHostID = 0;
                }
            }
            var obj = JSON.stringify(postData);
            fnGridCrud(obj);
            return JSON.stringify(postData);
        },
        closeAfterEdit: true,
        closeAfterAdd: true,
        afterSubmit: function (response, postdata) {
            //console.log(jQuery.parseJSON(response.responseText).isSuccess);
            if (jQuery.parseJSON(response.responseText).isSuccess == false) {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                return [false, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }
    });

    function fnGridCrud(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.editurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    function fnGridDelete(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.deleteurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    // extended the del option to post the data through serialization
    jQuery.extend(jQuery.jgrid.del, {
        //set contentType
        reloadAfterSubmit: false,
        ajaxDelOptions: { contentType: "application/json" },
        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the data array.

        serializeDelData: function (data) {
            delete data.CreatedOn;
            var obj = JSON.stringify(data);
            fnGridDelete(obj);
            return JSON.stringify(data);
        },

        afterSubmit: function (response, postdata) {
            console.log(response.responseText);
            if (jQuery.parseJSON(response.responseText).isSuccess != true) {
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }

    });

    // recreate the filter every time so that the new templates have effect
    $.extend(
        $.jgrid.search,
        {
            //If set to true this activates the advanced searching
            multipleSearch: jsonObj.multipleSearch,
            //If set to true this activates the advanced searching with a possibilities to define a complex conditions
            multipleGroup: jsonObj.multipleGroup,
            //if you want the jqgrid search box to be cleared as well.
            recreateFilter: jsonObj.recreateFilter,
            //If this option is set to 0 the overlay in grid is disabled and the user can interact with the grid while search dialog is active
            overlay: jsonObj.overlay
        }
    );

    $.extend($.jgrid.defaults,
    {
        datatype: 'json',
        ignoreCase: true
    }
    );


    $(jsonObj.htmlContent).jqGrid({
        url: jsonObj.url,
        datatype: jsonObj.datatype,
        mtype: jsonObj.mtype,
        pager: jsonObj.navPager,
        serializeGridData: function (data) {
            // the function is DUMMY. it MUST be replaced
            //return '{"SearchCriteria": {"keyword":"emai","orderByField":"userName","sortOrder":"DESC","pagination":{"pageSize":"10","pageNumber":"2"}}}';
            return JSON.stringify(data);
        },
        //ajaxGridOptions: { contentType: "application/json; charset=utf-8" },
        colNames: jsonObj.paracolName,
        colModel: jsonObj.paracolModel,
        jsonReader: {
            //root: "rows",
            root: jsonObj.rows,
            page: "page",
            records: "total",
            repeatitems: false
        },
        //autoencode: true,
        //ignoreCase: true,
        //hidegrid: false

        // Sets how many records we want to view in the grid. 
        // This parameter is passed to the url for use by the server routine retrieving the data
        rowNum: jsonObj.rowNum,
        //An array to construct a select box element in the pager in which we can change the number of the visible rows
        rowList: jsonObj.rowList,
        sortname: jsonObj.sortName,
        sortorder: jsonObj.sortorder,
        //Defines that we want to use a pager bar to navigate through the records.
        pager: jsonObj.navPager,
        //Defines whether we want to display the number of total records from the query in the pager bar.
        viewrecords: true,
        //What will be the result if we insert all the data at once? Yes, this can be done with a help of gridview option (set it to true). The result is a grid that is 5 to 10 times faster. Of course, when this option is set to true we have some limitations. 
        //If set to true we can not use treeGrid, subGrid, or the afterInsertRow event. If you do not use these three options in the grid you can set this option to true and enjoy the speed.
        gridview: true,
        height: 'auto',
        //If this flag is set to true, the grid loads the data from the server only once (using the appropriate datatype). After the first request, the datatype parameter is automatically changed to local and all further manipulations are done on the client side. The functions of the pager (if present) are disabled.
        loadonce: true,
        //Defines the url for inline and form editing. May be set to clientArray to manually post data to server
        editurl: jsonObj.editurl,
        loadui: 'disable',
        emptyrecords: "No Record Found",
        caption: jsonObj.caption,
        multiselect: jsonObj.multiselect,
        multiSort: jsonObj.multiSort,
        autowidth: true,
        ondblClickRow: function () {
            var grid = $(jsonObj.htmlContent);
            var rowid = grid.jqGrid('getGridParam', 'selrow');
            if (jsonObj.navViewButton) {
                window.location = jsonObj.navViewURL + rowid;
            }
        }

        //The Navigator is a user interface feature that allows easy accessibility to record actions such as Find or Edit. The user can activate a grid action by pressing the appropriate icon button in the Navigation layer.
    }).jqGrid('navGrid', jsonObj.navPager, { edit: jsonObj.navEditFlag, add: jsonObj.navAddFlag, del: jsonObj.navDelFlag, search: jsonObj.navSearchFlag, refresh: jsonObj.navRefreshFlag });

    //This method construct searching creating input elements just below the header elements of the grid. When the header elements are re sized the input search elements are also re sized according to the new width.
    //The method uses the url option in grid to perform a search to the server. 
    $(jsonObj.htmlContent).jqGrid('filterToolbar', { searchOperators: jsonObj.searchOperators, searchOnEnter: jsonObj.searchOnEnter, enableClear: jsonObj.enableClear });


    // 
    if (jsonObj.navAddButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-plus",
            title: "Add New Row",
            onClickButton: function () {
                window.location = jsonObj.navAddURL;
            }
        });
    }

    if (jsonObj.navEditButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-pencil",
            title: "Edit Row",
            onClickButton: function () {
                var grid = $(jsonObj.htmlContent);
                var rowid = grid.jqGrid('getGridParam', 'selrow');
                if (rowid == null || rowid == undefined) {
                    //alert("Please, Select Row to Modify")
                    //$('#alertmod_grdUserInfo').show();
                    $(".dialog").dialog("open");
                    $(".ui-dialog-title").html('Warning');
                    $(".ui-dialog-title").html('Warning');
                }
                else {
                    window.location = jsonObj.navEditURL + rowid;
                }
            }
        });
    }

    jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-calculator",
        title: "Choose Columns",
        onClickButton: function () {
            jQuery(jsonObj.htmlContent).jqGrid('columnChooser');
        }
    });

   
    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-excel",
        title: "Export to Excel",
        onClickButton: function () {
            var htmlValue = exportGridToExcel(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            //window.open('data:application/vnd.ms-excel,' + htmlValue);
            tableToExcel(htmlValue, jsonObj.caption)
        }
    });
    
    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-pdf",
        title: "Export to PDF",
        onClickButton: function () {
            var htmlValue = exportGridToPdf(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            var $myDiv = $("#pdfContainer");
            $myDiv.empty();
            $("#pdfContainer").show();
            $myDiv.html(htmlValue);
            demoFromHTML(jsonObj.caption + ".pdf", jsonObj.paperSize, jsonObj.paperOrientation);
            $myDiv.hide();
        }
    });

}

function jqGridGenerator2(jsonObj) {

    var sortorder = jsonObj.sortorder;

    if (sortorder == null || sortorder == "undefined" || sortorder == "" || sortorder != "desc") {
        sortorder = "asc"
    }
    else {
        sortorder = "desc"
    }


    var Data;
    var userid;
    userid = 1;
    jQuery.extend(jQuery.jgrid.edit, {
        //If set to true this closes the edit dialog after the user apply a edit - i.e. click on Find button
        savekey: [true, 13],
        closeOnEscape: true,
        recreateForm: true,
        ajaxEditOptions: { contentType: "application/json" },
        reloadAfterSubmit: false,

        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the postData array.
        serializeEditData: function (postData) {
            //var postdata = { 'data': postData };

            var paraUrl = jsonObj.editurl;

            if (postData.hasOwnProperty('CreatedOn')) {//Check if poperty exist then remove
                delete postData.CreatedOn;
            }
            if (postData.hasOwnProperty('MasterDetailID')) {//Check if poperty exist then remove
                delete postData.MasterDetailID;
            }
            if (postData.hasOwnProperty('ModifiedBy')) {//Check if poperty exist then remove
                delete postData.ModifiedBy;
            }



            if (postData.oper == "add") {
                if (postData.hasOwnProperty('id')) {//Check if poperty exist then remove
                    delete postData.id;
                }
                if (postData.hasOwnProperty('SiteID')) {//Check if poperty exist then remove
                    delete postData.SiteID;
                }
                if (postData.hasOwnProperty('ProviderID')) {//Check if poperty exist then remove
                    delete postData.ProviderID;
                }

                if (postData.hasOwnProperty('DomainID')) {//Check if poperty exist then remove
                    postData.DomainID = 0;
                }
                if (postData.hasOwnProperty('WebHostID')) {//Check if poperty exist then remove
                    postData.WebHostID = 0;
                }
                if (postData.hasOwnProperty('EmailHostID')) {//Check if poperty exist then remove
                    postData.EmailHostID = 0;
                }
            }
            var obj = JSON.stringify(postData);
            fnGridCrud(obj);
            return JSON.stringify(postData);
        },
        closeAfterEdit: true,
        closeAfterAdd: true,
        afterSubmit: function (response, postdata) {
            //console.log(jQuery.parseJSON(response.responseText).isSuccess);
            if (jQuery.parseJSON(response.responseText).isSuccess == false) {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                return [false, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }
    });

    function fnGridCrud(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.editurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    function fnGridDelete(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.deleteurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    // extended the del option to post the data through serialization
    jQuery.extend(jQuery.jgrid.del, {
        //set contentType
        reloadAfterSubmit: false,
        ajaxDelOptions: { contentType: "application/json" },
        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the data array.

        serializeDelData: function (data) {
            delete data.CreatedOn;
            var obj = JSON.stringify(data);
            fnGridDelete(obj);
            return JSON.stringify(data);
        },

        afterSubmit: function (response, postdata) {
            console.log(response.responseText);
            if (jQuery.parseJSON(response.responseText).isSuccess != true) {
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }

    });

    // recreate the filter every time so that the new templates have effect
    $.extend(
        $.jgrid.search,
        {
            //If set to true this activates the advanced searching
            multipleSearch: jsonObj.multipleSearch,
            //If set to true this activates the advanced searching with a possibilities to define a complex conditions
            multipleGroup: jsonObj.multipleGroup,
            //if you want the jqgrid search box to be cleared as well.
            recreateFilter: jsonObj.recreateFilter,
            //If this option is set to 0 the overlay in grid is disabled and the user can interact with the grid while search dialog is active
            overlay: jsonObj.overlay
        }
    );

    $.extend($.jgrid.defaults,
    {
        datatype: 'json',
        ignoreCase: true
    }
    );


    $(jsonObj.htmlContent).jqGrid({
        url: jsonObj.url,
        datatype: jsonObj.datatype,
        mtype: jsonObj.mtype,
        pager: jsonObj.navPager,
        serializeGridData: function (data) {
            // the function is DUMMY. it MUST be replaced
            //return '{"SearchCriteria": {"keyword":"emai","orderByField":"userName","sortOrder":"DESC","pagination":{"pageSize":"10","pageNumber":"2"}}}';
            return JSON.stringify(data);
        },
        //ajaxGridOptions: { contentType: "application/json; charset=utf-8" },
        colNames: jsonObj.paracolName,
        colModel: jsonObj.paracolModel,
        jsonReader: {
            //root: "rows",
            root: jsonObj.rows,
            page: "page",
            records: "total",
            repeatitems: false
        },
        //autoencode: true,
        //ignoreCase: true,
        //hidegrid: false

        // Sets how many records we want to view in the grid. 
        // This parameter is passed to the url for use by the server routine retrieving the data
        rowNum: jsonObj.rowNum,
        //An array to construct a select box element in the pager in which we can change the number of the visible rows
        rowList: jsonObj.rowList,
        sortname: jsonObj.sortName,
        sortorder: jsonObj.sortorder,
        //Defines that we want to use a pager bar to navigate through the records.
        pager: jsonObj.navPager,
        //Defines whether we want to display the number of total records from the query in the pager bar.
        viewrecords: true,
        //What will be the result if we insert all the data at once? Yes, this can be done with a help of gridview option (set it to true). The result is a grid that is 5 to 10 times faster. Of course, when this option is set to true we have some limitations. 
        //If set to true we can not use treeGrid, subGrid, or the afterInsertRow event. If you do not use these three options in the grid you can set this option to true and enjoy the speed.
        gridview: true,
        height: 'auto',
        //If this flag is set to true, the grid loads the data from the server only once (using the appropriate datatype). After the first request, the datatype parameter is automatically changed to local and all further manipulations are done on the client side. The functions of the pager (if present) are disabled.
        loadonce: true,
        //Defines the url for inline and form editing. May be set to clientArray to manually post data to server
        editurl: jsonObj.editurl,
        loadui: 'disable',
        emptyrecords: "No Record Found",
        caption: jsonObj.caption,
        multiselect: jsonObj.multiselect,
        multiSort: jsonObj.multiSort,
        autowidth: true,
        ondblClickRow: function () {
            var grid = $(jsonObj.htmlContent);
            var rowid = grid.jqGrid('getGridParam', 'selrow');
            if (jsonObj.navViewButton) {
                window.location = jsonObj.navViewURL + rowid;
            }
        }

        //The Navigator is a user interface feature that allows easy accessibility to record actions such as Find or Edit. The user can activate a grid action by pressing the appropriate icon button in the Navigation layer.
    }).jqGrid('navGrid', jsonObj.navPager, { edit: jsonObj.navEditFlag, add: jsonObj.navAddFlag, del: jsonObj.navDelFlag, search: jsonObj.navSearchFlag, refresh: jsonObj.navRefreshFlag });

    //This method construct searching creating input elements just below the header elements of the grid. When the header elements are re sized the input search elements are also re sized according to the new width.
    //The method uses the url option in grid to perform a search to the server. 
    $(jsonObj.htmlContent).jqGrid('filterToolbar', { searchOperators: jsonObj.searchOperators, searchOnEnter: jsonObj.searchOnEnter, enableClear: jsonObj.enableClear });


    // 
    if (jsonObj.navAddButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-plus",
            title: "Add New Row",
            onClickButton: function () {
                window.location = jsonObj.navAddURL;
            }
        });
    }

    if (jsonObj.navEditButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-pencil",
            title: "Edit Row",
            onClickButton: function () {
                var grid = $(jsonObj.htmlContent);
                var rowid = grid.jqGrid('getGridParam', 'selrow');
                if (rowid == null || rowid == undefined) {
                    //alert("Please, Select Row to Modify")
                    //$('#alertmod_grdUserInfo').show();
                    $(".dialog").dialog("open");
                    $(".ui-dialog-title").html('Warning');
                    $(".ui-dialog-title").html('Warning');
                }
                else {
                    window.location = jsonObj.navEditURL + rowid;
                }
            }
        });
    }

    //jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
    //    cloneToTop: false,
    //    caption: "",
    //    buttonicon: "ui-icon-calculator",
    //    title: "Choose Columns",
    //    onClickButton: function () {
    //        jQuery(jsonObj.htmlContent).jqGrid('columnChooser');
    //    }
    //});


    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-excel",
        title: "Export to Excel",
        onClickButton: function () {
            var htmlValue = exportGridToExcel(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            //window.open('data:application/vnd.ms-excel,' + htmlValue);
            tableToExcel(htmlValue, jsonObj.caption)
        }
    });

    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-pdf",
        title: "Export to PDF",
        onClickButton: function () {
            var htmlValue = exportGridToPdf(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            var $myDiv = $("#pdfContainer");
            $myDiv.empty();
            $("#pdfContainer").show();
            $myDiv.html(htmlValue);
            demoFromHTML(jsonObj.caption + ".pdf", jsonObj.paperSize, jsonObj.paperOrientation);
            $myDiv.hide();
        }
    });

}

function jqGridGenerator3(jsonObj) {

    var sortorder = jsonObj.sortorder;

    if (sortorder == null || sortorder == "undefined" || sortorder == "" || sortorder != "desc") {
        sortorder = "asc"
    }
    else {
        sortorder = "desc"
    }


    var Data;
    var userid;
    userid = 1;
    jQuery.extend(jQuery.jgrid.edit, {
        //If set to true this closes the edit dialog after the user apply a edit - i.e. click on Find button
        savekey: [true, 13],
        closeOnEscape: true,
        recreateForm: true,
        ajaxEditOptions: { contentType: "application/json" },
        reloadAfterSubmit: false,

        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the postData array.
        serializeEditData: function (postData) {
            //var postdata = { 'data': postData };

            var paraUrl = jsonObj.editurl;

            if (postData.hasOwnProperty('CreatedOn')) {//Check if poperty exist then remove
                delete postData.CreatedOn;
            }
            if (postData.hasOwnProperty('MasterDetailID')) {//Check if poperty exist then remove
                delete postData.MasterDetailID;
            }
            if (postData.hasOwnProperty('ModifiedBy')) {//Check if poperty exist then remove
                delete postData.ModifiedBy;
            }



            if (postData.oper == "add") {
                if (postData.hasOwnProperty('id')) {//Check if poperty exist then remove
                    delete postData.id;
                }
                if (postData.hasOwnProperty('SiteID')) {//Check if poperty exist then remove
                    delete postData.SiteID;
                }
                if (postData.hasOwnProperty('ProviderID')) {//Check if poperty exist then remove
                    delete postData.ProviderID;
                }

                if (postData.hasOwnProperty('DomainID')) {//Check if poperty exist then remove
                    postData.DomainID = 0;
                }
                if (postData.hasOwnProperty('WebHostID')) {//Check if poperty exist then remove
                    postData.WebHostID = 0;
                }
                if (postData.hasOwnProperty('EmailHostID')) {//Check if poperty exist then remove
                    postData.EmailHostID = 0;
                }
            }
            var obj = JSON.stringify(postData);
            fnGridCrud(obj);
            return JSON.stringify(postData);
        },
        closeAfterEdit: true,
        closeAfterAdd: true,
        afterSubmit: function (response, postdata) {
            //console.log(jQuery.parseJSON(response.responseText).isSuccess);
            if (jQuery.parseJSON(response.responseText).isSuccess == false) {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                return [false, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }
    });

    function fnGridCrud(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.editurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    function fnGridDelete(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.deleteurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    // extended the del option to post the data through serialization
    jQuery.extend(jQuery.jgrid.del, {
        //set contentType
        reloadAfterSubmit: false,
        ajaxDelOptions: { contentType: "application/json" },
        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the data array.

        serializeDelData: function (data) {
            delete data.CreatedOn;
            var obj = JSON.stringify(data);
            fnGridDelete(obj);
            return JSON.stringify(data);
        },

        afterSubmit: function (response, postdata) {
            console.log(response.responseText);
            if (jQuery.parseJSON(response.responseText).isSuccess != true) {
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }

    });

    // recreate the filter every time so that the new templates have effect
    $.extend(
        $.jgrid.search,
        {
            //If set to true this activates the advanced searching
            multipleSearch: jsonObj.multipleSearch,
            //If set to true this activates the advanced searching with a possibilities to define a complex conditions
            multipleGroup: jsonObj.multipleGroup,
            //if you want the jqgrid search box to be cleared as well.
            recreateFilter: jsonObj.recreateFilter,
            //If this option is set to 0 the overlay in grid is disabled and the user can interact with the grid while search dialog is active
            overlay: jsonObj.overlay
        }
    );

    $.extend($.jgrid.defaults,
    {
        datatype: 'json',
        ignoreCase: true
    }
    );


    $(jsonObj.htmlContent).jqGrid({
        url: jsonObj.url,
        datatype: jsonObj.datatype,
        mtype: jsonObj.mtype,
        pager: jsonObj.navPager,
        serializeGridData: function (data) {
            // the function is DUMMY. it MUST be replaced
            //return '{"SearchCriteria": {"keyword":"emai","orderByField":"userName","sortOrder":"DESC","pagination":{"pageSize":"10","pageNumber":"2"}}}';
            return JSON.stringify(data);
        },
        //ajaxGridOptions: { contentType: "application/json; charset=utf-8" },
        colNames: jsonObj.paracolName,
        colModel: jsonObj.paracolModel,
        jsonReader: {
            //root: "rows",
            root: jsonObj.rows,
            page: "page",
            records: "total",
            repeatitems: false
        },
        //autoencode: true,
        //ignoreCase: true,
        //hidegrid: false

        // Sets how many records we want to view in the grid. 
        // This parameter is passed to the url for use by the server routine retrieving the data
        rowNum: jsonObj.rowNum,
        //An array to construct a select box element in the pager in which we can change the number of the visible rows
        rowList: jsonObj.rowList,
        sortname: jsonObj.sortName,
        sortorder: jsonObj.sortorder,
        //Defines that we want to use a pager bar to navigate through the records.
        pager: jsonObj.navPager,
        //Defines whether we want to display the number of total records from the query in the pager bar.
        viewrecords: true,
        //What will be the result if we insert all the data at once? Yes, this can be done with a help of gridview option (set it to true). The result is a grid that is 5 to 10 times faster. Of course, when this option is set to true we have some limitations. 
        //If set to true we can not use treeGrid, subGrid, or the afterInsertRow event. If you do not use these three options in the grid you can set this option to true and enjoy the speed.
        gridview: true,
        height: 'auto',
        //If this flag is set to true, the grid loads the data from the server only once (using the appropriate datatype). After the first request, the datatype parameter is automatically changed to local and all further manipulations are done on the client side. The functions of the pager (if present) are disabled.
        loadonce: true,
        //Defines the url for inline and form editing. May be set to clientArray to manually post data to server
        editurl: jsonObj.editurl,
        loadui: 'disable',
        emptyrecords: "No Record Found",
        caption: jsonObj.caption,
        multiselect: jsonObj.multiselect,
        multiSort: jsonObj.multiSort,
        autowidth: true,
        ondblClickRow: function () {
            var grid = $(jsonObj.htmlContent);
            var rowid = grid.jqGrid('getGridParam', 'selrow');
            if (jsonObj.navViewButton) {
                window.location = jsonObj.navViewURL + rowid;
            }
        }

        //The Navigator is a user interface feature that allows easy accessibility to record actions such as Find or Edit. The user can activate a grid action by pressing the appropriate icon button in the Navigation layer.
    }).jqGrid('navGrid', jsonObj.navPager, { edit: jsonObj.navEditFlag, add: jsonObj.navAddFlag, del: jsonObj.navDelFlag, search: jsonObj.navSearchFlag, refresh: jsonObj.navRefreshFlag });

    //This method construct searching creating input elements just below the header elements of the grid. When the header elements are re sized the input search elements are also re sized according to the new width.
    //The method uses the url option in grid to perform a search to the server. 
    $(jsonObj.htmlContent).jqGrid('filterToolbar', { searchOperators: jsonObj.searchOperators, searchOnEnter: jsonObj.searchOnEnter, enableClear: jsonObj.enableClear });


    // 
    if (jsonObj.navAddButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-plus",
            title: "Add New Row",
            onClickButton: function () {
                window.location = jsonObj.navAddURL;
            }
        });
    }

    if (jsonObj.navEditButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-pencil",
            title: "Edit Row",
            onClickButton: function () {
                var grid = $(jsonObj.htmlContent);
                var rowid = grid.jqGrid('getGridParam', 'selrow');
                if (rowid == null || rowid == undefined) {
                    //alert("Please, Select Row to Modify")
                    //$('#alertmod_grdUserInfo').show();
                    $(".dialog").dialog("open");
                    $(".ui-dialog-title").html('Warning');
                    $(".ui-dialog-title").html('Warning');
                }
                else {
                    window.location = jsonObj.navEditURL + rowid;
                }
            }
        });
    }

    jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-calculator",
        title: "Choose Columns",
        onClickButton: function () {
            jQuery(jsonObj.htmlContent).jqGrid('columnChooser');
        }
    });


    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-excel",
        title: "Export to Excel",
        onClickButton: function () {
            var htmlValue = exportGridToExcel(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            //window.open('data:application/vnd.ms-excel,' + htmlValue);
            tableToExcel(htmlValue, jsonObj.caption)
        }
    });

    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-pdf",
        title: "Export to PDF",
        onClickButton: function () {
            var htmlValue = exportGridToPdf(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            var $myDiv = $("#pdfContainer");
            $myDiv.empty();
            $("#pdfContainer").show();
            $myDiv.html(htmlValue);
            demoFromHTML(jsonObj.caption + ".pdf", jsonObj.paperSize, jsonObj.paperOrientation);
            $myDiv.hide();
        }
    });

}

function jqGridGenerator4(jsonObj) {

    var sortorder = jsonObj.sortorder;

    if (sortorder == null || sortorder == "undefined" || sortorder == "" || sortorder != "desc") {
        sortorder = "asc"
    }
    else {
        sortorder = "desc"
    }


    var Data;
    var userid;
    userid = 1;
    jQuery.extend(jQuery.jgrid.edit, {
        //If set to true this closes the edit dialog after the user apply a edit - i.e. click on Find button
        savekey: [true, 13],
        closeOnEscape: true,
        recreateForm: true,
        ajaxEditOptions: { contentType: "application/json" },
        reloadAfterSubmit: false,

        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the postData array.
        serializeEditData: function (postData) {
            //var postdata = { 'data': postData };

            var paraUrl = jsonObj.editurl;

            if (postData.hasOwnProperty('CreatedOn')) {//Check if poperty exist then remove
                delete postData.CreatedOn;
            }
            if (postData.hasOwnProperty('MasterDetailID')) {//Check if poperty exist then remove
                delete postData.MasterDetailID;
            }
            if (postData.hasOwnProperty('ModifiedBy')) {//Check if poperty exist then remove
                delete postData.ModifiedBy;
            }



            if (postData.oper == "add") {
                if (postData.hasOwnProperty('id')) {//Check if poperty exist then remove
                    delete postData.id;
                }
                if (postData.hasOwnProperty('SiteID')) {//Check if poperty exist then remove
                    delete postData.SiteID;
                }
                if (postData.hasOwnProperty('ProviderID')) {//Check if poperty exist then remove
                    delete postData.ProviderID;
                }

                if (postData.hasOwnProperty('DomainID')) {//Check if poperty exist then remove
                    postData.DomainID = 0;
                }
                if (postData.hasOwnProperty('WebHostID')) {//Check if poperty exist then remove
                    postData.WebHostID = 0;
                }
                if (postData.hasOwnProperty('EmailHostID')) {//Check if poperty exist then remove
                    postData.EmailHostID = 0;
                }
            }
            var obj = JSON.stringify(postData);
            fnGridCrud(obj);
            return JSON.stringify(postData);
        },
        closeAfterEdit: true,
        closeAfterAdd: true,
        afterSubmit: function (response, postdata) {
            //console.log(jQuery.parseJSON(response.responseText).isSuccess);
            if (jQuery.parseJSON(response.responseText).isSuccess == false) {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                return [false, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }
    });

    function fnGridCrud(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.editurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    function fnGridDelete(dataObj) {
        $.ajax({
            type: "POST", //GET or POST or PUT or DELETE verb
            url: jsonObj.deleteurl, // Location of the service
            data: dataObj, //Data sent to server
            contentType: "application/json; charset=utf-8", // content type sent to server
            dataType: "json", //Expected data format from server
            processdata: false
        });
    }

    // extended the del option to post the data through serialization
    jQuery.extend(jQuery.jgrid.del, {
        //set contentType
        reloadAfterSubmit: false,
        ajaxDelOptions: { contentType: "application/json" },
        //If set this event can serialize the data passed to the ajax request. 
        //The function should return the serialized data. This event can be used when a custom data should be passed to the server - e.g - JSON string, XML string and etc. 
        //To this event we pass the data array.

        serializeDelData: function (data) {
            delete data.CreatedOn;
            var obj = JSON.stringify(data);
            fnGridDelete(obj);
            return JSON.stringify(data);
        },

        afterSubmit: function (response, postdata) {
            console.log(response.responseText);
            if (jQuery.parseJSON(response.responseText).isSuccess != true) {
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, jQuery.parseJSON(response.responseText).Message]
            }
            else {
                $(this).jqGrid('setGridParam',
                  { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add

                return [true, 'Success']
            }
        }

    });

    // recreate the filter every time so that the new templates have effect
    $.extend(
        $.jgrid.search,
        {
            //If set to true this activates the advanced searching
            multipleSearch: jsonObj.multipleSearch,
            //If set to true this activates the advanced searching with a possibilities to define a complex conditions
            multipleGroup: jsonObj.multipleGroup,
            //if you want the jqgrid search box to be cleared as well.
            recreateFilter: jsonObj.recreateFilter,
            //If this option is set to 0 the overlay in grid is disabled and the user can interact with the grid while search dialog is active
            overlay: jsonObj.overlay
        }
    );

    $.extend($.jgrid.defaults,
    {
        datatype: 'json',
        ignoreCase: true
    }
    );


    $(jsonObj.htmlContent).jqGrid({
        url: jsonObj.url,
        datatype: jsonObj.datatype,
        mtype: jsonObj.mtype,
        pager: jsonObj.navPager,
        serializeGridData: function (data) {
            // the function is DUMMY. it MUST be replaced
            //return '{"SearchCriteria": {"keyword":"emai","orderByField":"userName","sortOrder":"DESC","pagination":{"pageSize":"10","pageNumber":"2"}}}';
            return JSON.stringify(data);
        },
        //ajaxGridOptions: { contentType: "application/json; charset=utf-8" },
        colNames: jsonObj.paracolName,
        colModel: jsonObj.paracolModel,
        jsonReader: {
            //root: "rows",
            root: jsonObj.rows,
            page: "page",
            records: "total",
            repeatitems: false
        },
        //autoencode: true,
        //ignoreCase: true,
        //hidegrid: false

        // Sets how many records we want to view in the grid. 
        // This parameter is passed to the url for use by the server routine retrieving the data
        rowNum: jsonObj.rowNum,
        //An array to construct a select box element in the pager in which we can change the number of the visible rows
        rowList: jsonObj.rowList,
        sortname: jsonObj.sortName,
        sortorder: jsonObj.sortorder,
        //Defines that we want to use a pager bar to navigate through the records.
        pager: jsonObj.navPager,
        //Defines whether we want to display the number of total records from the query in the pager bar.
        viewrecords: true,
        //What will be the result if we insert all the data at once? Yes, this can be done with a help of gridview option (set it to true). The result is a grid that is 5 to 10 times faster. Of course, when this option is set to true we have some limitations. 
        //If set to true we can not use treeGrid, subGrid, or the afterInsertRow event. If you do not use these three options in the grid you can set this option to true and enjoy the speed.
        gridview: true,
        height: 'auto', 
        //If this flag is set to true, the grid loads the data from the server only once (using the appropriate datatype). After the first request, the datatype parameter is automatically changed to local and all further manipulations are done on the client side. The functions of the pager (if present) are disabled.
        loadonce: true,
        //Defines the url for inline and form editing. May be set to clientArray to manually post data to server
        editurl: jsonObj.editurl,
        loadui: 'disable',
        emptyrecords: "No Record Found",
        caption: jsonObj.caption,
        multiselect: jsonObj.multiselect,
        multiSort: jsonObj.multiSort,
        autowidth: true,
        ondblClickRow: function () {
            var grid = $(jsonObj.htmlContent);
            var rowid = grid.jqGrid('getGridParam', 'selrow');
            if (jsonObj.navViewButton) {
                window.location = jsonObj.navViewURL + rowid;
            }
        }
        //The Navigator is a user interface feature that allows easy accessibility to record actions such as Find or Edit. The user can activate a grid action by pressing the appropriate icon button in the Navigation layer.
    }).jqGrid('navGrid', jsonObj.navPager, { edit: jsonObj.navEditFlag, add: jsonObj.navAddFlag, del: jsonObj.navDelFlag, search: jsonObj.navSearchFlag, refresh: jsonObj.navRefreshFlag });

    //This method construct searching creating input elements just below the header elements of the grid. When the header elements are re sized the input search elements are also re sized according to the new width.
    //The method uses the url option in grid to perform a search to the server. 
    $(jsonObj.htmlContent).jqGrid('filterToolbar', { searchOperators: jsonObj.searchOperators, searchOnEnter: jsonObj.searchOnEnter, enableClear: jsonObj.enableClear });


    // 
    if (jsonObj.navAddButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-plus",
            title: "Add New Row",
            onClickButton: function () {
                window.location = jsonObj.navAddURL;
            }
        });
    }

    if (jsonObj.navEditButton) {
        jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
            caption: "",
            buttonicon: "ui-icon-pencil",
            title: "Edit Row",
            onClickButton: function () {
                var grid = $(jsonObj.htmlContent);
                var rowid = grid.jqGrid('getGridParam', 'selrow');
                if (rowid == null || rowid == undefined) {
                    //alert("Please, Select Row to Modify")
                    //$('#alertmod_grdUserInfo').show();
                    $(".dialog").dialog("open");
                    $(".ui-dialog-title").html('Warning');
                    $(".ui-dialog-title").html('Warning');
                }
                else {
                    window.location = jsonObj.navEditURL + rowid;
                }
            }
        });
    }

    jQuery(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-calculator",
        title: "Choose Columns",
        onClickButton: function () {
            jQuery(jsonObj.htmlContent).jqGrid('columnChooser');
        }
    });


    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-excel",
        title: "Export to Excel",
        onClickButton: function () {
            var htmlValue = exportGridToExcel(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            //window.open('data:application/vnd.ms-excel,' + htmlValue);
            tableToExcel(htmlValue, jsonObj.caption)
        }
    });

    $(jsonObj.htmlContent).jqGrid('navButtonAdd', jsonObj.navPager, {
        cloneToTop: false,
        caption: "",
        buttonicon: "ui-icon-pdf",
        title: "Export to PDF",
        onClickButton: function () {
            var htmlValue = exportGridToPdf(jsonObj.htmlContent, jsonObj.caption, jsonObj.paracolModel, jsonObj.paracolName);
            var $myDiv = $("#pdfContainer");
            $myDiv.empty();
            $("#pdfContainer").show();
            $myDiv.html(htmlValue);
            demoFromHTML(jsonObj.caption + ".pdf", jsonObj.paperSize, jsonObj.paperOrientation);
            $myDiv.hide();
        }
    });

}

function oDataClone(htmlContent, url, datatype, mtype, caption, rowNum, rowList, sortName, sortorder, width, height, editurl, navPager, navEditFlag, navAddFlag, navDelFlag, navSearchFlag, navRefreshFlag, searchOnEnter, enableClear, paracolName, paracolModel, multiselect, multiSort, searchOperators,
multipleSearch, multipleGroup, recreateFilter, overlay, rows, navAddButton, navAddURL, navEditButton, navEditURL, deleteurl, paperSize, paperOrientation, navViewButton, navViewURL) {

    this.htmlContent = htmlContent;
    this.url = url;
    this.datatype = datatype;
    this.mtype = mtype;
    this.caption = caption;
    this.rowNum = rowNum;
    this.rowList = rowList;
    this.sortName = sortName;
    this.sortorder = sortorder;
    this.width = width;
    this.height = height;
    this.editurl = editurl;
    this.navPager = navPager;
    this.navEditFlag = navEditFlag;
    this.navAddFlag = navAddFlag;
    this.navDelFlag = navDelFlag;
    this.navSearchFlag = navSearchFlag;
    this.navRefreshFlag = navRefreshFlag;
    this.searchOnEnter = searchOnEnter;
    this.enableClear = enableClear;
    this.paracolName = paracolName;
    this.paracolModel = paracolModel;
    this.multiselect = multiselect;
    this.multiSort = multiSort;
    this.searchOperators = searchOperators;
    this.multipleSearch = multipleSearch;
    this.multipleGroup = multipleGroup;
    this.recreateFilter = recreateFilter;
    this.overlay = overlay;
    this.rows = rows;
    this.navAddButton = navAddButton;
    this.navAddURL = navAddURL;
    this.navEditButton = navEditButton;
    this.navEditURL = navEditURL;
    this.deleteurl = deleteurl;
    this.paperSize = paperSize;
    this.paperOrientation = paperOrientation;
    this.navViewButton = navViewButton;
    this.navViewURL = navViewURL;
}





function exportGridToExcel(pSelecter, caption, colmodel, colHeader) {
    //mya = $(pSelecter).getDataIDs(); // Get All IDs
    mya = $(pSelecter).jqGrid('getDataIDs');
    //var data = $(pSelecter).getRowData(mya[0]); // Get First row to get the
    var data = $(pSelecter).jqGrid('getRowData', mya[0]);
    // labels
    var colNames = new Array();
    var ii = 0;
    for (var i in data) {
        colNames[ii++] = i;
    } // capture col names


    var styleSheet = ".myGrid {background-color:#fff;margin:5px 0px 10px 0px;}";
    styleSheet = styleSheet + ".myGrid td {padding:2px 10px;border: thin solid #cccccc;border-collapse:collapse;}";
    styleSheet = styleSheet + ".myGrid th {padding:5px 20px;color:#fff;background-color:#4297d7;border: thin solid #0e5d9d;border-collapse:collapse;font-size:0.9em;}";
    styleSheet = styleSheet + ".myGrid .alt {background-color:#EFEFEF;}";
    styleSheet = styleSheet + ".myGrid span{background-color:#0e5d9d;}";

    var html = "";
    html = "<html><body><head><style>" + styleSheet + "</style></head><table class='myGrid'><CAPTION><em>" + caption + "</em></CAPTION><thead><tr>";

    for (var k = 0; k < colNames.length; k++) {
        if (!colmodel[k].hidden) {
            if (colHeader[k].search('View') == -1) {
                html = html + "<th>" + colHeader[k] + "</th>";
            }
        }
    }
    html = html + "</tr></thead>"; // Output header with end of line
    for (i = 0; i < mya.length; i++) {
        html = html + "<tr>";
        //data = $(pSelecter).getRowData(mya[i]); // get each row
        data = $(pSelecter).jqGrid('getRowData', mya[i]);
        for (var j = 0; j < colNames.length; j++) {
            if (!colmodel[j].hidden) {
                if (data[colNames[j]].search('</a>') == -1) {
                    html = html + "<td>" + data[colNames[j]] + "</td>"; // output each Row as
                }
                // tab delimited
            }
        }
        html = html + "</tr>"; // output each row with end of line
    }
    html = html + "</table></body></html>"; // end of line at the end
    html = html.replace('&apos;', "'");
    return html;
}

function exportGridToPdf(pSelecter, caption, colmodel, colHeader) {

    //mya = $(pSelecter).getDataIDs(); // Get All IDs
    mya = $(pSelecter).jqGrid('getDataIDs');
    //var data = $(pSelecter).getRowData(mya[0]); // Get First row to get the
    var data = $(pSelecter).jqGrid('getRowData', mya[0]);
    //console.log(data);
    // labels
    var colNames = new Array();
    var ii = 0;

    for (var i in data) {
        colNames[ii++] = i;
    } // capture col names


    var colLength = colNames.length;
    var colgroupWidth = 100 / colLength;

    var html = "";
    //html = "<style>" + styleSheet + "</style><table ><tr><td><h1 style='white-space:nowrap;font-size:20px;'>" + caption + "</h1></td></tr></table><table class='myGrid'><tr>";
    //html = "<style>" + styleSheet + "</style><h2 style='font-size:20px;'>" + caption + "</h2><table class='myGrid'><tr>";
    html = '<h2 style="white-space:nowrap;font-size:20px;">' + caption + '</h2><table width="100%">';
    //<td><table class='myGrid'><tr><td>" + caption + "</td></tr></table></td></tr>
    html = html + "<colgroup>";
    var headerLength = 10;
    for (var k = 0; k < colNames.length; k++) {
        if (!colmodel[k].hidden && colHeader[k].length > 0) {
            if (colHeader[k].search('View') == -1) {
                headerLength = colHeader[k].length - 1;
                html = html + "<col width='" + headerLength + "%'>";
            }
        }
    }

    html = html + "</colgroup><thead><tr>";

    for (var k = 0; k < colNames.length; k++) {
        if (!colmodel[k].hidden && colHeader[k].length > 0) {
            if (colHeader[k].search('View') == -1) {
                html = html + "<th>" + colHeader[k] + "</th>";
            }
        }
    }
    html = html + "</tr></thead><tbody>"; // Output header with end of line

    for (i = 0; i < mya.length; i++) {
        html = html + "<tr>";
        data = $(pSelecter).jqGrid('getRowData', mya[i]);
        for (var j = 0; j < colNames.length; j++) {
            if (!colmodel[j].hidden && data[colNames[j]] != '') {
                if (data[colNames[j]].search('</a>') == -1) {
                    html = html + "<td>" + data[colNames[j]] + "</td>"; // output each Row as
                }
            }
        }
        html = html + "</tr>"; // output each row with end of line
    }

    html = html + "</tbody></table>"; // end of line at the end
    html = html.replace('&apos;', "'");

    return html;
}

// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function exportTableToCSV($table, filename) {

    var $rows = $table.find('tr:has(th,td)'),

        // Temporary delimiter characters unlikely to be typed by keyboard
        // This is to avoid accidentally splitting the actual contents
        tmpColDelim = String.fromCharCode(11), // vertical tab character
        tmpRowDelim = String.fromCharCode(0), // null character

        // actual delimiter characters for CSV format
        colDelim = '","',
        rowDelim = '"\r\n"',

        // Grab text from table into CSV formatted string
        csv = '"' + $rows.map(function (i, row) {
            var $row = $(row),
                $cols = $row.find('th,td');

            return $cols.map(function (j, col) {
                var $col = $(col),
                    text = $col.text();

                return text.replace('"', '""'); // escape double quotes

            }).get().join(tmpColDelim);

        }).get().join(tmpRowDelim)
            .split(tmpRowDelim).join(rowDelim)
            .split(tmpColDelim).join(colDelim) + '"',

        // Data URI
        csvData = 'data:application/octet-stream;charset=utf-8,' + encodeURIComponent(JSON.stringify(csv));


    createDownloadLink("#exportCsv", csv, filename);

    //$("#exportCsv").attr({
    //    'download': filename,
    //    'href': csvData,
    //    'target': '_blank'
    //});

}


var tableToExcel = (function () {
    var uri = 'data:application/octet-stream;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

    return function (table, name) {
        var ctx = { worksheet: name || 'Worksheet', table: table }

        window.location.href = uri + base64(format(template, ctx))
    }
})()


function createDownloadLink(anchorSelector, str, fileName) {
    if (window.navigator.msSaveOrOpenBlob) {
        var fileData = ['\ufeff' + str];
        blobObject = new Blob(fileData);
        $(anchorSelector).click(function () {
            window.navigator.msSaveOrOpenBlob(blobObject, fileName);
        });
    } else {
        var url = "data:text/plain;charset=utf-8,%EF%BB%BF" + encodeURIComponent(str);
        $(anchorSelector).attr("download", fileName);
        $(anchorSelector).attr("href", url);
    }
}
