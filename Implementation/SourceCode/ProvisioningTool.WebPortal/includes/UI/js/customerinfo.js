$(document).ready(function ($) {

    //Check the Url Operation and based on which if the operation is More View state then block the user to operate on action functionalites over controls
    //(ie)-- + add, ! History List, x Delete and E-Edit the fiels on the go is blocked
    var url = $(location).attr('href');
    var operation = GetParameterValues("do", url);
    var recordId = GetParameterValues("id", url);

    if (operation == 'm') {
        $('.actionPanel').css('display', 'none');
    }

    if (operation == 'a') {
        $('.infoSiteIcon').css('display', 'none');
    }

    //Masking for IP Adress
    $(".ipaddress").mask('099.099.099.099');

    $("input[type=tel]").mask('(000) 000-0000');

    $(".selector").select2();

    $(".Security").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDDlSecurityGroup").val($(this).val());

    });

    $(".Remote").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlRemoteAccess").val($(this).val());
    });

    $(".Computer").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlComputer").val($(this).val());
    });

    $(".Laptop").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlLaptop").val($(this).val());

    });
    $(".Mobile").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlMobilePhone").val($(this).val());

    });

    $(".Tablets").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlTablet").val($(this).val());

    });

    $(".Apps").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlApps").val($(this).val());

    });

    $(".Network").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlNetworkShares").val($(this).val());

    });

    $(".Servers").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlServers").val($(this).val());

    });

    $(".Printers").change(function () {
        $("#includes_usercontrols_pages_userinfo_ascx_hidmulDdlPrinters").val($(this).val());

    });


    function IsEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }

    //Integer Validator
    $(".IntegerValidation").keydown(function (event) {
        // Allow only backspace and delete
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
               || event.keyCode == 27 || event.keyCode == 13
               || (event.keyCode == 65 && event.ctrlKey === true)
               || (event.keyCode >= 35 && event.keyCode <= 39)) {
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
            if (event.shiftKey) {
                event.preventDefault();
            }
        }
    });

    $(".IntegerValidation, .AlphaWihSpace, .AlphaNumWihSpace, .Username").bind("paste", function (e) {
        event.preventDefault();
    });
    $(".IntegerValidation .AlphaWihSpace, .AlphaNumWihSpace, .Username").bind("drop", function (e) {
        event.preventDefault();
    });

    $(".AlphaWihSpace").keypress(function (e) {
        var arr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz -_";
        var code;
        // Allow only backspace and delete
        if (e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 9
               || e.keyCode == 27 || e.keyCode == 13
               || (e.keyCode == 65 && e.ctrlKey === true)
               || (e.keyCode >= 35 && e.keyCode <= 39)) {
            return;
        }
        if (window.event)
            code = e.keyCode;
        else
            code = e.which;

        var char = keychar = String.fromCharCode(code);

        if (arr.indexOf(char) == -1) {
            return false;
        }
    });
    $(".AlphaNumWihSpace").keypress(function (e) {
        var arr = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
        var code;
        // Allow only backspace and delete
        if (e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 9
               || e.keyCode == 27 || e.keyCode == 13
               || (e.keyCode == 65 && e.ctrlKey === true)
               || (e.keyCode >= 35 && e.keyCode <= 39)) {
            return;
        }
        if (window.event)
            code = e.keyCode;
        else
            code = e.which;
        var char = keychar = String.fromCharCode(code);
        if (arr.indexOf(char) == -1)
            return false;
    });
    $(".Username").keypress(function (e) {
        var arr = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_";
        var code;
        // Allow only backspace and delete
        if (e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 9
               || e.keyCode == 27 || e.keyCode == 13
               || (e.keyCode == 65 && e.ctrlKey === true)
               || (e.keyCode >= 35 && e.keyCode <= 39)) {
            return;
        }
        if (window.event)
            code = e.keyCode;
        else
            code = e.which;
        var char = keychar = String.fromCharCode(code);
        if (arr.indexOf(char) == -1)
            return false;
    });

    function isIpAddress(s) {
        var ip = /^(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}$/;
        return s.match(ip);
    }

    var iframeClick, sHostName, sNavigateURl;
    $(".Printer").click(function () {
        iframeClick = "Printer";

    });

    $(".Servers").click(function () {
        iframeClick = "Servers";
    });

    $(".Apps").click(function () {
        iframeClick = "Apps";
    });

    $(".NetworkShares").click(function () {
        iframeClick = "NetworkShares";
    });
    $(".Tablet").click(function () {
        iframeClick = "Tablet";
    });
    $(".MobileDevices").click(function () {
        iframeClick = "MobileDevices";
    });
    $(".Laptops").click(function () {
        iframeClick = "Laptops";
    });
    $(".Workstations").click(function () {
        iframeClick = "Workstations";
    });
    $(".UserTitle").click(function () {
        iframeClick = "UserTitle";
    });
    $(".Department").click(function () {
        iframeClick = "Department";
    });
    $(".SecurityGroup").click(function () {
        iframeClick = "SecurityGroup";
    });
    $(".RemoteAccess").click(function () {
        iframeClick = "RemoteAccess";
    });
    $(".WirelessManufacture").click(function () {
        iframeClick = "WirelessManufacture";
    });
    $(".WirelessModel").click(function () {
        iframeClick = "WirelessModel";
    });
    $(".PhoneSystemModelMaster").click(function () {
        iframeClick = "PhoneSystemModelMaster";
    });
    $(".PhoneSystemOSversion").click(function () {
        iframeClick = "PhoneSystemOSversion";
    });
    $(".PhoneSystemModules").click(function () {
        iframeClick = "PhoneSystemModules";
    });
    $(".UserInfo").click(function () {
        iframeClick = "UserInfo";
    });
    $(".SoftwareUserInfo").click(function () {
        iframeClick = "SoftwareUserInfo";
    });
    $(".Chasis").click(function () {
        iframeClick = "Chasis";
    });
    $(".Power").click(function () {
        iframeClick = "Power";
    });
    $(".Slots").click(function () {
        iframeClick = "Slots";
    });
    $(".Display").click(function () {
        iframeClick = "Display";
    });
    $(".VCard").click(function () {
        iframeClick = "VCard";
    });
    $(".Ports").click(function () {
        iframeClick = "Ports";
    });
    $(".Multimedia").click(function () {
        iframeClick = "Multimedia";
    });
    $(".Chipset").click(function () {
        iframeClick = "Chipset";
    });
    $(".Motherboard").click(function () {
        iframeClick = "Motherboard";
    });
    $(".CPU").click(function () {
        iframeClick = "CPU";
    });
    $(".SysRoles").click(function () {
        iframeClick = "SysRoles";
    });
    $(".Application").click(function () {
        iframeClick = "Application";
    });
    $(".BKApplication").click(function () {
        iframeClick = "BKApplication";
    });
    $(".AntiVirus").click(function () {
        iframeClick = "AntiVirus";
    });
    $(".OppSystem").click(function () {
        iframeClick = "OppSystem";
    });
    $(".LaptopHardware").click(function () {
        iframeClick = "LaptopHardware";
    });
    $(".ServerHardware").click(function () {
        iframeClick = "ServerHardware";
    });
    $(".WorkStationHardware").click(function () {
        iframeClick = "WorkStationHardware";
    });
    $(".FirewallModel").click(function () {
        iframeClick = "FirewallModel";
    });
    $(".FirewallOSVersion").click(function () {
        iframeClick = "FirewallOSVersion";
    });
    $(".FirewallModules").click(function () {
        iframeClick = "FirewallModules";
    });
    $(".MDType").click(function () {
        iframeClick = "MDType";
    });
    $(".MDManuf").click(function () {
        iframeClick = "MDManuf";
    });
    $(".MDModel").click(function () {
        iframeClick = "MDModel";
    });
    $(".MDIAssignedUser").click(function () {
        iframeClick = "MDIAssignedUser";
    });
    $(".Site").click(function () {
        iframeClick = "Site";
    });

    $(".RIModel").click(function () {
        iframeClick = "RouterModel";
    });

    $(".RIOSVersion").click(function () {
        iframeClick = "RouterOSVersion";
    });

    $(".RIModules").click(function () {
        iframeClick = "RouterModule";
    });

    $(".AddSHardware, .AddLHardware, .AddWHardware, .ViewWHardware, .ViewLHardware, .ViewSHardware").click(function () {
        sHostName = $("#txtHostName").val();
        Page = $(this).text();


        if (Page == "Add Server Hardware") {
            iframeClick = "ServerHardware";
            sNavigateURl = "CustomerInfo.aspx?do=a&HostName=" + sHostName + "&nav=Servers&isColorBox=yes&opp=SH"
        }

        if (Page == "Add Laptop Hardware") {
            iframeClick = "LaptopHardware";
            sNavigateURl = "CustomerInfo.aspx?do=a&HostName=" + sHostName + "&nav=Laptops&isColorBox=yes&opp=SH"
        }

        if (Page == "Add Workstation Hardware") {
            iframeClick = "WorkStationHardware";
            sNavigateURl = "CustomerInfo.aspx?do=a&HostName=" + sHostName + "&nav=Workstations&isColorBox=yes&opp=SH"
        }

        /*View Model details of the Server, Workstation, Laptop*/
        if (Page == "View Server Hardware") {
            iframeClick = "ServerHardware";
            recordId = $('#ddlSModel option:selected').val();
            sNavigateURl = "CustomerInfo.aspx?do=m&HostName=" + sHostName + "&nav=Servers&isColorBox=yes&opp=SH&id=" + recordId
        }

        if (Page == "View Laptop Hardware") {
            iframeClick = "LaptopHardware";
            recordId = $('#ddlLModel option:selected').val();
            sNavigateURl = "CustomerInfo.aspx?do=m&HostName=" + sHostName + "&nav=Laptops&isColorBox=yes&opp=SH&id=" + recordId
        }

        if (Page == "View Workstation Hardware") {
            iframeClick = "WorkStationHardware";
            recordId = $('#ddlWModel option:selected').val();
            sNavigateURl = "CustomerInfo.aspx?do=m&HostName=" + sHostName + "&nav=Workstations&isColorBox=yes&opp=SH&id=" + recordId
        }


        $(".AddHardware, .ViewHardware").colorbox({
            iframe: true,
            height: "85%",
            innerHeight: "900px",
            initialHeight: "200px",
            maxHeight: "800px",
            transition: 'none',
            href: sNavigateURl,

            onOpen: function () {
                $(".toolbar a").tooltip("disable");
                $('body').css({ overflow: 'hidden' });
                $.cookie("isIframe", '1');
            },
            onClosed: function () {
                $(".toolbar a").tooltip("enable");
                $('body').css({ overflow: 'auto' });
                $.cookie('isIframe', null, { expires: -1 });
                $.cookie('isIframeCookieOperation', null, { expires: -1 });
                if (iframeClick == "ServerHardware")
                    BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlSModel", "GETALLSERVERHARDWARES");
                else if (iframeClick == "LaptopHardware")
                    BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlLModel", "GETALLLAPTOPHARDWARES");
                else if (iframeClick == "WorkStationHardware")
                    BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlWModel", "GETALLWORKSTATIONHARDWARES");
            }
        });
    });


    //Script to Load ColorBox Frame on the UI
    $(".iframe").colorbox({
        iframe: true,
        height: "85%",
        innerHeight: "900px",
        initialHeight: "200px",
        maxHeight: "800px",
        transition: 'none',

        onOpen: function () {
            $(".toolbar a").tooltip("disable");
            $('body').css({ overflow: 'hidden' });
            $.cookie("isIframe", '1');
        },
        onClosed: function () {
            $(".toolbar a").tooltip("enable");
            $('body').css({ overflow: 'auto' });
            $.cookie('isIframe', null, { expires: -1 });
            $.cookie('isIframeCookieOperation', null, { expires: -1 });

            if (iframeClick == "Printer")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlPrinters", "GETALLPRINTERS");
            else if (iframeClick == "Servers")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlServers", "GETALLSERVERINFO");
            else if (iframeClick == "Apps")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlApps", "Applications");
            else if (iframeClick == "Tablet")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlTablet", "Tablets");
            else if (iframeClick == "NetworkShares")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlNetworkShares", "GETALLNETWORKSHARE");
            else if (iframeClick == "MobileDevices")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlMobilePhone", "GETALLMOBILEDEVICES");
            else if (iframeClick == "Laptops")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlLaptop", "GETALLLAPTOPINFO");
            else if (iframeClick == "RouterModel")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_routerinfo_ascx_ddlModel", "Router Models");
            else if (iframeClick == "RouterOSVersion")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_routerinfo_ascx_ddlOSVersion", "Router OS versions");
            else if (iframeClick == "RouterModule")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_routerinfo_ascx_ddlModules", "Router Modules");
            else if (iframeClick == "Workstations")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlComputer", "GETALLWORKSTATIONINFO");
            else if (iframeClick == "UserTitle")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlTitle", "Titles");
            else if (iframeClick == "Department")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlDepartment", "Departments");
            else if (iframeClick == "SecurityGroup")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_userinfo_ascx_mulDDlSecurityGroup", "Security Groups");
            else if (iframeClick == "RemoteAccess")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_userinfo_ascx_mulDdlRemoteAccess", "Remote Access");
            else if (iframeClick == "WirelessManufacture")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlManufacture", "Wireless Manufacturers");
            else if (iframeClick == "WirelessModel")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlModel", "Wireless Models");
            else if (iframeClick == "PhoneSystemModelMaster")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlModel", "Phone System Models");
            else if (iframeClick == "PhoneSystemOSversion")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlOSVersion", "Phone System OS versions");
            else if (iframeClick == "PhoneSystemModules")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#includes_usercontrols_pages_phonesysteminfo_ascx_ddlModules", "Phone System Modules");
            else if (iframeClick == "UserInfo")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlAUsers", "GETALLUSERS");
            else if (iframeClick == "SoftwareUserInfo")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlAssignedUsers", "GETALLUSERS");
            else if (iframeClick == "MDIAssignedUser")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlAssignedUser", "GETALLUSERS");
            else if (iframeClick == "Chasis")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlChasis", "Chassis");
            else if (iframeClick == "Power")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlPower", "Powers");
            else if (iframeClick == "Slots")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlSlots", "Slots");
            else if (iframeClick == "Display")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlDisplay1", "Displays");
            else if (iframeClick == "VCard")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlVideoCard", "Video Cards");
            else if (iframeClick == "Ports")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlPorts", "Ports");
            else if (iframeClick == "Multimedia")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlMultimedia", "Multimedia");
            else if (iframeClick == "Chipset")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlChipset", "Chipsets");
            else if (iframeClick == "Motherboard")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlMotherboard", "MotherBoards");
            else if (iframeClick == "CPU")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlCPU", "CPUs");
            else if (iframeClick == "Memory")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlMemory", "Memory");
            else if (iframeClick == "SysRoles")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlSRoles", "Roles");
            else if (iframeClick == "Application")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlApp", "Application Softwares");
            else if (iframeClick == "BKApplication")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlBA", "Backup Softwares");
            else if (iframeClick == "AntiVirus")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlAV", "Antivirus");
            else if (iframeClick == "OppSystem")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlOS", "Operating Systems");
            else if (iframeClick == "LaptopHardware")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlLModel", "GETALLLAPTOPHARDWARES");
            else if (iframeClick == "ServerHardware")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlSModel", "GETALLSERVERHARDWARES");
            else if (iframeClick == "WorkStationHardware")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlWModel", "GETALLWORKSTATIONHARDWARES");
            else if (iframeClick == "FirewallModel")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlModel", "Firewall Models");
            else if (iframeClick == "FirewallOSVersion")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlOSVersion", "Firewall OS Versions");
            else if (iframeClick == "FirewallModules")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlModules", "Firewall Modules");
            else if (iframeClick == "MDType")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlType", "Mobile Device Types");
            else if (iframeClick == "MDManuf")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlManufacture", "Mobile Device Manufacturers");
            else if (iframeClick == "MDModel")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForGlobalMaster", json, "#ddlModel", "Mobile Device Models");
            else if (iframeClick == "Site")
                BindDropDownListsByDictionarycls("../includes/UserControls/common/WebMethods.aspx/PopulateForMultSelect", json, "#ddlCustomerSites", "GETSITESBYCUSTOMERID");
        },
        onComplete: function () {
            try {
                setTimeout(function () {
                    var body = $('#cboxLoadedContent iframe').contents().find('body');
                    //$(this).colorbox.resize({ innerHeight: body.height() });
                    // body.addClass('iframeBodyClass');
                }, 1000);
            }
            catch (err) {
                console.log('iframe_onComplete_' + err);
            }
        }
    });

    //RE-Populate Choosen Dropdown on By JSon
    var obj = {};
    obj["A"] = "0";
    var json = JSON.stringify(obj);
    function BindDropDownListsByDictionarycls(MethodURL, json, DropDownID, ddlname) {
        var siteID = $.cookie("siteID");
        if (siteID == "" || siteID == null) { siteID = 0 }

        var sessionCustomerId = $.cookie("sessionCustomerId");
        if (sessionCustomerId == "" || sessionCustomerId == null) { sessionCustomerId = 0 }

        $.ajax({
            type: 'POST',
            url: MethodURL,
            data: '{"dropdownServiceUrl":"' + ddlname + '","sessionSiteId":"' + siteID + '","sessionCustomerId":"' + sessionCustomerId + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = $(DropDownID);
                select.children().remove();
                if (ddlname != "GETSITESBYCUSTOMERID") {
                    select.append($("<option>").val(0).text('–Select–'));
                }
                else {
                    select.val('');
                    select.empty();
                }
                if (data.d) {
                    $(data.d).each(function (index, item) {
                        select.append($("<option>").val(item.Id).text(item.value));
                    });
                    //alert(DropDownID);
                    $(DropDownID).trigger("chosen:updated");

                }
                if (ddlname == "GETSITESBYCUSTOMERID") {
                    $("#ddlCustomerSites option:first-child").attr("selected", "selected");
                }
            }

        });
    }

    //Jquery dialog for Jq grid Alert
    $(".dialog").dialog({
        autoOpen: false,
        height: 100,
        width: 160

    });

    $('.TrackHistory').click(function (e) {
        e.stopPropagation();
        var next = $(this).parent();

        var $this = $(this);
        var url = $(this).attr('href');
        var HistoryTrackerID = GetParameterValues("HistoryTrackerID", url);
        //alert(HistoryTrackerID);
        var HistoryMasterName = GetParameterValues("HistoryMasterName", url);
        //alert(HistoryMasterName);
        var HistoryFieldName = GetParameterValues("HistoryFieldName", url);
        //alert(HistoryFieldName);
        var ISForward = GetParameterValues("ISForward", url);
        //alert(ISForward);
        var elemrntId = GetParameterValues("elemrntId", url);
        var rowid = $('#hidEditID').val();
        //alert(rowid);
        // GetHistoryTrackerValue("../includes/UserControls/pages/UserInfo.ascx/GetHistoryTrackerDetails", HistoryTrackerID, HistoryMasterName, HistoryFieldName, ISForward, elemrntId, rowid);

        $.ajax({
            type: 'POST',
            url: "../includes/UserControls/common/WebMethods.aspx/GetHistoryTrackerDetails",
            data: '{"HistoryTrackerID":"' + rowid + '","HistoryMasterName":"' + HistoryMasterName + '","HistoryFieldName":"' + HistoryFieldName + '","ISForward":"' + ISForward + '","elemrntId":"' + elemrntId + '","rowid":"' + rowid + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    var TrackerValue = data.d["TrackerValue"];
                    //alert(TrackerValue);
                    var HistoryHtml = data.d["HistoryHtml"];
                    //alert(elemrntId);
                    var prevhref = data.d["prevhref"];
                    // alert(prevhref);
                    var nexthref = data.d["nexthref"];
                    //alert(nexthref);
                    //alert(href);
                    if (TrackerValue != "" && TrackerValue != null)
                        $('#' + elemrntId).val(TrackerValue);
                    //select for dropdown
                    $(this).select2('val', TrackerValue);
                    $(this).attr('href', nexthref);
                    // alert(HistoryHtml);
                    next.next('.tooltip-popup').empty();
                    if (HistoryHtml == null) {
                        next.next('.tooltip-popup').append('<table border = 1><tr><th>No History</th></tr></table>');
                    }
                    else {
                        next.next('.tooltip-popup').append(HistoryHtml);
                    }
                    next.next('.tooltip-popup').fadeToggle('fast');
                }
            }
        });
        return false;
    })

    $(document).click(function () {
        $('.tooltip-popup').fadeOut('fast');
    });

    $('.tooltip-popup').click(function (e) {
        e.stopPropagation();
    });

    //Split the Page Querystring and give the resultant value requred 
    //This is somthing like Request.QueryString["ReqValue"]-->some valin query string
    function GetParameterValues(param, url) {
        var url = url.slice(url.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                return urlparam[1];
            }
        }
    }

    $('.prevTrackHistory').click(function () {
        var $this = $(this);
        var url = $(this).attr('href');
        var HistoryTrackerID = GetParameterValues("HistoryTrackerID", url);
        //alert(HistoryTrackerID);
        var HistoryMasterName = GetParameterValues("HistoryMasterName", url);
        //alert(HistoryMasterName);
        var HistoryFieldName = GetParameterValues("HistoryFieldName", url);
        //alert(HistoryFieldName);
        var ISForward = GetParameterValues("ISForward", url);
        //alert(ISForward);
        var elemrntId = GetParameterValues("elemrntId", url);
        var rowid = GetParameterValues("rowid", url);

        GetHistoryTrackerValue("../includes/UserControls/common/WebMethods.aspx/GetHistoryTrackerDetails", HistoryTrackerID, HistoryMasterName, HistoryFieldName, ISForward, elemrntId, rowid);

        return false;

    })

    $('.nextTrackHistory').click(function () {
        var url = $(this).attr('href');
        var $this = $(this);
        var currentObject = $(this).attr('class');
        var HistoryTrackerID = GetParameterValues("HistoryTrackerID", url);
        //alert(HistoryTrackerID);
        var HistoryMasterName = GetParameterValues("HistoryMasterName", url);
        //alert(HistoryMasterName);
        var HistoryFieldName = GetParameterValues("HistoryFieldName", url);
        //alert(HistoryFieldName);
        var ISForward = GetParameterValues("ISForward", url);
        //alert(ISForward);
        var elemrntId = GetParameterValues("elemrntId", url);
        var rowid = GetParameterValues("rowid", url);

        GetHistoryTrackerValue("../includes/UserControls/common/WebMethods.aspx/GetHistoryTrackerDetails", HistoryTrackerID, HistoryMasterName, HistoryFieldName, ISForward, elemrntId, rowid);

        return false;
    })


    function GetHistoryTrackerValue(MethodURL, HistoryTrackerID, HistoryMasterName, HistoryFieldName, ISForward, elemrntId, rowid) {
        $.ajax({
            type: 'POST',
            url: "../includes/UserControls/common/WebMethods.aspx/GetHistoryTrackerDetails",
            data: '{"HistoryTrackerID":"' + HistoryTrackerID + '","HistoryMasterName":"' + HistoryMasterName + '","HistoryFieldName":"' + HistoryFieldName + '","ISForward":"' + ISForward + '","elemrntId":"' + elemrntId + '","rowid":"' + rowid + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    var TrackerValue = data.d["TrackerValue"];
                    //alert(TrackerValue);
                    //alert(elemrntId);
                    var prevhref = data.d["prevhref"];
                    // alert(prevhref);
                    var nexthref = data.d["nexthref"];
                    //alert(nexthref);
                    //alert(href);
                    if (TrackerValue != "" && TrackerValue != null)
                        $('#' + elemrntId).val(TrackerValue);
                    //select for dropdown
                    $('#' + elemrntId).select2('val', TrackerValue);
                    $('#prev' + elemrntId).attr('href', prevhref);
                    $('#next' + elemrntId).attr('href', nexthref);
                }
            }
        });

    }

    //SOFTWARE INFO 

    //dropdown change function to maintain
    $(".AssignedUsers").change(function () {
        $("#hidAssignedUsers").val($(this).val());
    });

    //PRINTERS INFO

    $(".Modules").change(function () {
        $("#hidModuleID").val($(this).val());
    });

    //FIREWALL INFO

    $('#txtsitePass').keypress(function (e) {
        e.preventDefault();
    });

    $('#txtsitePass').on("cut copy paste", function (e) {
        e.preventDefault();
    });

    $(".Users").change(function () {
        $("#hidAssignedUserID").val($(this).val());
    });

    //USERS INFO 

    ////Fade out the message after 10 sec
    //$('#lblErrorMessage').fadeIn().delay(2000).fadeOut(function () {
    //    //This is for URL rewrite with out post back 
    //    var stateObj = {};
    //    var pagePathName = window.location.pathname;
    //    //History.pushState(stateObj, "", pagePathName.substring(pagePathName.lastIndexOf("/") + 1));
    //    //$('#divMessage').css("display", "none");
        
    //    return false;
    //});

    //Tags input common method invoke
    $('.multiText').tagsInput({
        tabindex: true
    });

    //SERVER, LAPTOP AND WORKSTATION JQuery
    $('#chkDHCP').click(function () {
        if ($('#chkDHCP').is(":checked") == true) {
            $('#txtIPAddress').val('');
            $("#txtIPAddress").attr("disabled", "disabled");
        }
        else {
            $("#txtIPAddress").removeAttr("disabled");
        }
    });

    $("#btnAdd").on("click", function () {
        var BA = $("[id*='ddlBA'] :selected").text();
        var key = $("#txtLKBA").val();
        if (BA != 'Select') {
            $("#txtBAWLK").addTag(BA + ":" + key);

            $('#txtBAWLK').focus();

            $("#ddlBA").val(0);
            $("#txtLKBA").val("");
            $("#txtLKBA").removeClass("valid");
        }

        return false;
    });

    $("#btnAddAPP").on("click", function () {
        var BA = $("[id*='ddlApp'] :selected").text();
        var key = $("#txtLKApp").val();
        if (BA != 'Select') {
            $("#txtLKWA").addTag(BA + ":" + key);

            $('#txtLKWA').focus();

            $("#ddlApp").val(0);
            $("#txtLKApp").val("");
            $("#txtLKApp").removeClass("valid");
        }

        return false;
    });

    $("#btnMemoryAdd").on("click", function () {
        var Value = $("[id*='ddlMemory'] :selected").text();
        var CurrentValue = $('#txtTotalMemoryQuantity').val();
        if (CurrentValue.indexOf(Value) < 0) {
            var Quantiy = $("#txtMemoryQuanity").val();
            if (Value != 'Select') {
                $("#txtTotalMemoryQuantity").addTag(Value + ":" + Quantiy);

                $('#txtTotalMemoryQuantity').focus();

                $("#ddlMemory").val(0);
                $("#txtMemoryQuanity").val("");
                $("#txtMemoryQuanity").removeClass("valid");
            }
        }
        else {
            alert('Memory already exists');
        }

        return false;
    });

    $('#txtLKWA_tag').keypress(function (e) {
        e.preventDefault();
    });

    $('#txtLKWA_tag').on("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#txtBAWLK_tag').keypress(function (e) {
        e.preventDefault();
    });

    $('#txtBAWLK_tag').on("cut copy paste", function (e) {
        e.preventDefault();
    });

    var selectedItem = "";
    var memorySelected = "";

    $(".Memory").change(function () {
        $('#ddlMemory_chosen .chosen-choices .search-choice').each(function () {
            selectedItem = $('#ddlMemory option:selected').val();

            $('#ddlMemory_chosen .chosen-choices .search-choice').each(function () {
                memorySelected = $('#ddlMemory_chosen .chosen-choices .search-choice').text();
                var memId = getId(memorySelected);
            });
        });
    });

    function getId(value) {
        $('#ddlMemory').each(function () {
            if ($(this).text() == value) {
                return $(this).val();
            }
            else {
                return 0;
            }
        });
    }
    $(".Video").change(function () {
        $("#hidmulddlVideo").val($(this).val());
    });

    $(".HardDrive").change(function () {
        $("#hidmulddlHardDrive").val($(this).val());
    });

    $(".Port").change(function () {
        $("#hidmulddlPort").val($(this).val());
    });

    $(".Display").change(function () {
        $("#hidmulddlDisplay").val($(this).val());
    });

    $(".Slot").change(function () {
        $("#hidmulddlSlot").val($(this).val());
    });

    $(".Multimedia").change(function () {
        $("#hidmulddlMultimedia").val($(this).val());
    });

    $(".Power").change(function () {
        $("#hidmulddlPower").val($(this).val());
    });

    $(".ServerRole").change(function () {
        $("#hidmulddlSRoles").val($(this).val());
    });

    $(".AUser").change(function () {
        $("#hidmulddlAUsers").val($(this).val());
    });

    $(".BApp").change(function () {
        $("#hidmulddlBA").val($(this).val());
    });

    $(".APP").change(function () {
        $("#hidmulddlApp").val($(this).val());
    });

    $("#txtInstalledDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "mm-dd-yy"
    });

    $("#txtWEDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "mm-dd-yy"
    });

    $("input[type=text], input[type=email], input[type=number], input[type=tel], textarea").hover(function () {
        var textValue = $(this).val();
        $(this).attr('title', textValue);
    });

    $(".chosen-container-multi, .select2-container").hover(function () {
        var textValue = $(this).text();
        $(this).attr('title', textValue);
    });

});
