$(document).ready(function () {

    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

    $('input').addClass('watermark');

    $(".dp").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "mm-dd-yy"
    });

    $(".expiryDate").datepicker({
        minDate: 0,
        changeMonth: true,
        changeYear: true,
        dateFormat: "mm-dd-yy"
    });

    $(".installedDate").datepicker({
        maxDate: 0,
        changeMonth: true,
        changeYear: true,
        dateFormat: "mm-dd-yy"
    });


    $("#includes_usercontrols_pages_firewallinfo_ascx_btnAdd").on("click", function () {
        var site = $("#txtSitetoSite").val();
        var key = $("#txtKey").val();
        if (site != '' && $.trim(site).length > 0) {
            $("#txtsitePass_tag").addTag(site + ":" + key);
        }
        
        $('#txtsitePass_tag').trigger(jQuery.Event('keypress', { which: 13 }));
        $("#txtSitetoSite").val("");
        $("#txtKey").val("");
        $('#txtSitetoSite').focus();
        $("#txtSitetoSite").removeClass("valid");
        return false;
    });

    $("#includes_usercontrols_pages_routerinfo_ascx_btnAddR").live("click", function () {

        var site = $("#txtSitetoSite").val();
        var key = $("#txtKey").val();
        
        if (site != '' && $.trim(site).length > 0 ) {
            $("#txtsitePass_tag").addTag(site + ":" + key);
        }

        $('#txtsitePass_tag').trigger(jQuery.Event('keypress', { which: 13 }));
        $("#txtSitetoSite").val("");
        $("#txtKey").val("");
        $('#txtSitetoSite').focus();
        $("#txtSitetoSite").removeClass("valid");
        return false;
    });

    $('.setting a').click(function (e) {
        e.stopPropagation();
        $(this).next('ul').toggle();
    });

    $(document).click(function (e) {
        $('.setting ul').hide();
    });

    $('.setting ul').click(function (e) {
        e.stopPropagation();
    });

    $(".TrackHistory").click(function () {

        var htmlWidth = $(window).width();
        var historyPopup = $(this).offset();
        var historyPopOffset = historyPopup.left + 134;
        var rightValue = htmlWidth - historyPopOffset;

        if (rightValue <= '300') {
            $(this).parent().next('.tooltip-popup').addClass('lastChild');
        }

    });

    $('.toggleIcon').click(function (e) {
        e.stopPropagation();

        if ($(this).parents('.widget').next().children('.popupInfo').is(':visible')) {
            $(this).parents('.widget').next().children('.popupInfo').hide();
        } else if ($(this).parents('.widget').prev().children('.popupInfo').is(':visible')) {
            $(this).parents('.widget').prev().children('.popupInfo').hide();
        }

        $(this).parent().next().toggle();
    });

    $('body').click(function () {
        $('.popupInfo').hide();
    });

    $('.popupInfo').click(function (e) {
        e.stopPropagation();
    });

    setTimeout(function(){
        var innerContentHeight = $('.tabPanelWrap').height();
        $('.innerUl').css('min-height', innerContentHeight);
    }, 600);

    //$('.fullWidthGrid .sideBar, .contentWrapText').css('min-height', leftBarHeight);
   
    setTimeout(function(){
        var longSidebar = $('.fullWidthGrid .sideBar').height();
        var longSidebarContent = $('.contentWrapText').height();
        if (longSidebar > longSidebarContent) {
            $('.contentWrapText').css('min-height', longSidebar);
                
        } else {
            $('.fullWidthGrid .sideBar').css('min-height', longSidebarContent + 10);
        }
        
    }, 400);

    var methodName = getQueryStringByName('do');
    if ($('.siteDetail').css('display') == 'block') {
        if (methodName == 'a')
            $('#lblHeader').append(' - Add');
        else if (methodName == 'e')
            $('#lblHeader').append(' - Modify');
    }

    $('.addSiteIcon').live('click', function () {
        $('#innerTab').addClass('noLM');
    })

    $('#UserMaster_Header_aheaderSysEng').live('click', function () {
        $.cookie('UsersTab', 1);
    });
    
    if ($.cookie('UsersTab') != null) {
        $("#UserMaster_Header_aheaderSysEng").css("color", "#fff");
        $("#Header_aheaderSysEng").css("color", "#fff");
    }


    $('#Master_Header_aheaderCust').live('click', function () {
        $.cookie('CustomerTab', 1);
    });

    if ($.cookie('CustomerTab') != null) {
        $("#Master_Header_aheaderCust").css("color", "#fff");
    }
    
});

$(window).load(function () {
    var docHeight = $(window).height();
    var bodyHeight = $('body').height();
    
    if (docHeight < bodyHeight) {
        $('footer').css('position', 'relative');
    }
    else {
        $('footer').css('position', 'fixed');
    }
    var footerHeight = $('footer').height();
    var sidebarHeight = docHeight - footerHeight - 50;
    $('.adminContent .fullWidthGrid .sideBar, .searchContent ').css('min-height', sidebarHeight);
    $('.singleColHeight').css('min-height', sidebarHeight - 30);
    $('.innerTabContent2').css('min-height', bodyHeight - 160);
});

$(window).resize(function () {
    var docHeight = $(window).height();
    var bodyHeight = $('body').height();

    if (docHeight < bodyHeight) {
        $('footer').css('position', 'relative');
    }
    else {
        $('footer').css('position', 'fixed');
    }
    var footerHeight = $('footer').height();
    var sidebarHeight = docHeight - footerHeight - 50;
    $('.adminContent .fullWidthGrid .sideBar, .searchContent ').css('min-height', sidebarHeight);
    $('.singleColHeight').css('min-height', sidebarHeight - 30);
    $('.innerTabContent2').css('min-height', bodyHeight - 160);
});

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


$(document).ready(function ($) {
    $('#nav').dcAccordion({
        eventType: 'click',
        autoClose: true,
        autoExpand: false,
        saveState: true,
        disableLink: true,
        speed: 'fast',
        classActive: 'active',
        showCount: false
    });

});

function hideGridfooter() {
    if (getQueryStringByName("mode") == "s") {
        $('.ui-icon-excel').show();
        $('.ui-icon-pdf').show();
        $('.ui-icon-pencil').hide();
        $('.ui-icon-trash').hide();
        $('.ui-icon-plus').hide();
    }
    else {
        $('.ui-icon-pencil').show();
        $('.ui-icon-trash').show();
        $('.ui-icon-excel').hide();
        $('.ui-icon-pdf').hide();
        $('.ui-icon-plus').hide();
    }
}

function DropDown(el) {
    this.dd = el;
    this.initEvents();
}

DropDown.prototype = {
    initEvents: function () {
        var obj = this;

        obj.dd.on('click', function (event) {
            $(this).toggleClass('active');
            event.stopPropagation();
        });
    }
}

var myLanguage = {
    errorTitle: 'Form submission failed!',
    requiredFields: 'You have not answered all required fields',
    badTime: 'You have not given a correct time',
    badEmail: '',//'incorrect e-mail address',
    badTelephone: '',//'incorrect phone number',
    badSecurityAnswer: 'You have not given a correct answer to the security question',
    badDate: 'You have not given a correct date',
    lengthBadStart: 'You must give an answer between ',
    lengthBadEnd: ' characters',
    lengthTooLongStart: 'You have given an answer longer than ',
    lengthTooShortStart: 'You have given an answer shorter than ',
    notConfirmed: 'Values could not be confirmed',
    badDomain: 'Incorrect domain value',
    badUrl: '',//'The answer you gave was not a correct URL',
    badCustomVal: 'You gave an incorrect answer',
    badInt: '', //The answer you gave was not a correct number',
    badSecurityNumber: 'Your social security number was incorrect',
    badUKVatAnswer: 'Incorrect UK VAT Number',
    badStrength: 'The password isn\'t strong enough',
    badNumberOfSelectedOptionsStart: 'You have to choose at least ',
    badNumberOfSelectedOptionsEnd: ' answers',
    badAlphaNumeric: '',
    badAlphaNumericExtra: '',
    wrongFileSize: 'The file you are trying to upload is too large',
    wrongFileType: 'The file you are trying to upload is of wrong type',
    groupCheckedRangeStart: 'Please choose between ',
    groupCheckedTooFewStart: 'Please choose at least ',
    groupCheckedTooManyStart: 'Please choose a maximum of ',
    groupCheckedEnd: ' item(s)'
};


function getQueryStringByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
    results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}