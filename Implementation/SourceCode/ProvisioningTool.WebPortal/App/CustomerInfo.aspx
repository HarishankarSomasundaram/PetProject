<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CustomerInfo.aspx.cs" Inherits="CustomerInfo" %>
<%--<%@ Import Namespace= "System.Web.Optimization" %>  --%>

<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Details</title>
    <ProvisioningTool:Includes ID="Includes" runat="server" />
    <ProvisioningTool:Header ID="Header" runat="server" />
    <ProvisioningTool:Footer ID="Footer" runat="server" />
    <script lang="javascript" type="text/javascript">

        <%if (currentUser.Role.RoleID == (int)ProvisioningTool.Entity.UserRole.SystemEngineer)
          {%>
        $('#headerMaster').hide();
        $('#headerSysEng').hide();
        <%}  %>
        $.cookie('isIframe', null, { expires: 1 });
        
        function GetQueryStringParams(sParam) {
            var sPageURL = window.location.search.substring(1);
            var sURLVariables = sPageURL.split('&');
            for (var i = 0; i < sURLVariables.length; i++) {
                var sParameterName = sURLVariables[i].split('=');
                if (sParameterName[0] == sParam) {
                    return sParameterName[1];
                }
            }
        }

        $.urlParam = function (name, url) {
            if (!url) {
                url = window.location.href;
            }
            var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(url);
            if (!results) {
                return 0;
            }
            return results[1] || 0;
        }


        function ButtonEvent(msg, selectedId) {
            //alert(msg, selectedId);
            $('#main').get(0).reset();
            $("#lblHeader").text(msg);
            $('#hidTabname').val(msg);
            $.cookie('lefttab', selectedId);
            $("[id*='btnCustomerSubmit']").click();
            return false;
        }


        function ProvisioningButtonEvent(msg) {
            $("#lblHeaderPro").text(msg);
            $('#hidTabname').val(msg);
            $("[id*='btnCustomerProvisioning']").click();
            return false;
        }
        var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
        var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
        var getWebServiceURL = baseServiceURL + masterServiceName + "GetSiteDetailsBySite/";


        function getSiteDetails(siteID) {
            //$.removeCookie('siteID'); // => true
            $.cookie('siteID', siteID);
            //alert(siteID + 'siteID');
            $.ajax({
                type: "GET", //GET or POST or PUT or DELETE verb
                url: getWebServiceURL + siteID, // Location of the service
                data: "", //Data sent to server
                contentType: "application/json; charset=utf-8", // content type sent to server
                dataType: "json", //Expected data format from server
                success: function (data) {
                    console.log(data);
                    $("#siteCode").text(data.Site.SiteCode);
                    $("#siteAddress1").text(data.Site.Address1);
                    $("#siteAddress2").text(data.Site.Address2);
                    $("#siteCity").text(data.Site.CityName);
                    $("#siteState").text(data.Site.StateName);
                    $("#siteCountry").text(data.Site.CountryName);
                    $("#siteZipCode").text(data.Site.ZipCode);
                    $("#siteAccountRep").text(data.Site.AccountRepName);
                    $("#sitePrimaryEngineer").text(data.Site.PrimaryEngineerName);
                    $("#sitePhone").text(data.Site.PhoneNumber);
                    $("#siteWebAddress").text(data.Site.Website);
                    $("#siteWebAddress").attr("href", data.Site.Website);
                    $("#sitePrimaryContact").text(data.Site.PrimaryContactName);
                    $("#sitePrimaryTitle").text(data.Site.PrimaryContactTitleName);
                    $("#sitePrimaryPhone").text(data.Site.PrimaryContactPhone);
                    $("#sitePrimaryWebAddress").text(data.Site.PrimaryContactEmail);
                    $("#sitePrimaryWebAddress").attr("href", "mailto:" + data.Site.PrimaryContactEmail);
                    //$("#siteName").text(data.Site.SiteName);
                    //$("#hidSiteName").text(data.Site.SiteName);
                }
            });
        }


        $(document).ready(function () {
            var _href = $("#eSiteIcon").attr("href");
            
            if ($.cookie("siteID") != null) {
                $("#ddlCustomerSites").select2("val", $.cookie("siteID"));
                getSiteDetails($.cookie("siteID"));
                $("#eSiteIcon").attr("href", _href + '&id=' + $.cookie("siteID"));
            }


            $('.customerSite').on("change", function () {
                var sessionSiteId = $('.customerSite option:selected').val();
                getSiteDetails(sessionSiteId);
                $("#siteInfoTab").html($('.customerSite option:selected').text());
                $("#eSiteIcon").attr("href", _href + '&id=' + sessionSiteId);
                ButtonEvent('Users', 0);
            });

            if ($.cookie("siteID") == "" || $.cookie("siteID") == null) {
                var sessionSiteId = $('.customerSite option:selected').val();
                $("#ddlCustomerSites").select2("val", sessionSiteId);
                $("#eSiteIcon").attr("href", _href + '&id=' + sessionSiteId);
                getSiteDetails(sessionSiteId);
            }

            $("#siteInfoTab").html($('.customerSite option:selected').text());

            $("#btnSearch").live("click", function () {
                $.cookie('SearchUser', null, { expires: 1 });
                var SearchUserText = $("#txtSearch").val();
                $.cookie('SearchUser', SearchUserText);
            });

            $("#txtSearch").val($.cookie("SearchUser"));

            var selectedTabIndex = $("#tabs").tabs('option', 'active');

            $("#ui-id-1").click(function () {
                $.cookie("lefttab", 0);
                if (selectedTabIndex == 1)
                    ButtonEvent("Users", 0);

            });
            $("#ui-id-2").click(function () {
                if (selectedTabIndex == 0)
                    ProvisioningButtonEvent('Provisioning Check List');
            });

            var CustomerNameVal = $("#CustomerNameVal").html();
            $("#customerNamelbl").html(CustomerNameVal);

            var CustomerCode = $("#hidCustomerCode").val();
            CustomerCode = " (" + CustomerCode + ")";
            $("#customerNamelbl").html(CustomerNameVal);

            var CustomerPhoneValue = $("#customerPhone").html();
            $("#customerPhonelbl").html(CustomerPhoneValue);

            $(".closeSiteIcon").click(function () {
                var delsiteId = $('#ddlCustomerSites option:selected').val();
                var delhref__ = "<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerSites.aspx?nav=SiteInfo&iframe=1&iframedo=v&siteDelete=yes&isColorBox=yes&delsiteID=" + delsiteId;
                $("#delSiteIcon").attr("href", delhref__);
            });

            $('#Header_headderlinks').hide();
        });
        //Set the Headder name on Add or Update clicked on Grid
        var iframe = GetParameterValues('iframe');
        var iframedo = GetParameterValues('iframedo');
        //alert(headerName);
        if (iframe != '' && iframe != undefined) {
            $('.sideBar').hide();
            $('.nav').hide();
            $('.profileAction').hide();
            $('#hidIsIframe').val("1");
            $('#hidIsIframedo').val(iframedo);
            ButtonEvent(iframe, 0);
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
    </script>
</head>
<body id="PageBody" runat="server">
    <form id="main" runat="server">
        <div id="pdfContainer" style="display: block;"></div>
        <div id="dialog-message" title="Warning">
            <div>Please, select row</div>
        </div>
        <div>
            <section id="contentWrap">
                <div class="container doubleColumn customerInfo">

                    <div class="greyBG"></div>
                    <div class="hideContent toggle" id="hideContent" runat="server">
                        <asp:HiddenField ID="hidSiteName" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hidCustomerName" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hidCustomerCode" ClientIDMode="Static" runat="server" />
                        <div id="widgetContentWrap" runat="server">
                            <article class="widget">
                                <div class="headerSection">
                                    <span class="toggleIcon"></span>
                                    <h1 id="custInformation"><span id="customerNamelbl"></span>
                                        <span class="yellowIcon phoneIcon"></span><span id="customerPhonelbl"></span>
                                    </h1>
                                </div>
                                <div class="widgetContentWrap popupInfo">
                                    <div class="widgetContent">
                                        <div class="firstColumn">
                                            <span class="boldText" id="CustomerNameVal" clientidmode="Static" runat="server"></span>
                                            <br />
                                            <address>
                                                <span class="boldText" id="customerAddress1" runat="server"></span>
                                                <br />
                                                <span class="boldText" id="customerAddress2" runat="server"></span>
                                                <br />
                                                <span id="customerCity" runat="server"></span>, <span id="customerState" runat="server"></span>, <span runat="server" id="customerCountry"></span>, <span id="customerZipCode" runat="server"></span>

                                            </address>
                                        </div>

                                        <div class="secondColumn">
                                            <ul>
                                                <li>
                                                    <label>Account Rep:</label><span id="customerAccountRep" runat="server"></span></li>
                                                <li>
                                                    <label>Primary Engineer:</label><span id="customerPrimaryEngineer" runat="server"></span></li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="widgetContactDetail">
                                        <div class="firstColumn">
                                            <h4><span class="yellowIcon phoneIcon"></span><span id="customerPhone" runat="server"></span></h4>
                                        </div>
                                        <div class="secondColumn">
                                            <h4><span class="blueIcon faxIcon"></span><span id="customerFax" runat="server"></span></h4>
                                        </div>
                                        <div class="thirdColumn">
                                            <h4><span class="pinkIcon webIcon"></span><a href="javascript:void(0)" id="customerEmail" runat="server"><span id="customerWeb" runat="server"></span></a></h4>
                                        </div>
                                        <div class="clear"></div>
                                    </div>
                                </div>
                            </article>

                            <article class="widget">
                                <div class="headerSection">
                                    <span class="toggleIcon"></span>
                                    <div class="selectSiteWrap">
                                        <div class="selectSite">
                                            <asp:DropDownList ID="ddlCustomerSites" runat="server" class="selector customerSite" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                        <%if (currentUser.ApplicationUserID == (int)ProvisioningTool.Entity.UserRole.Administrator)
                                          {%>
                                        <div class="actionPanel">

                                            <span class="addSiteIcon"><a href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerSites.aspx?nav=SiteInfo&iframe=1&do=a&iframedo=a&isColorBox=yes" class="iframe Site"></a></span>
                                            <span class="editSiteIcon"><a id="eSiteIcon" href="<%=ConfigurationManager.AppSettings["IFrameBaseURL"].ToString() %>CustomerSites.aspx?nav=SiteInfo&do=e&iframe=1&iframedo=e&isColorBox=yes" class="iframe Site"></a></span>
                                            <span class="closeSiteIcon"><a href="#" id="delSiteIcon" class="iframe Site"></a></span>
                                        </div>

                                        <%} %>
                                        <div class="siteCode">
                                            <label>Site Code :</label><span id="siteCode"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="widgetContentWrap popupInfo">
                                    <div class="widgetContent">

                                        <div class="clear"></div>
                                        <div class="firstColumn">
                                            <address>
                                                <span class="boldText" id="siteAddress1"></span>
                                                <br />
                                                <span class="boldText" id="siteAddress2"></span>
                                                <br />
                                                <span id="siteCity"></span>, <span id="siteState"></span>, <span id="siteCountry"></span>, <span id="siteZipCode"></span>

                                            </address>
                                        </div>

                                        <div class="secondColumn">
                                            <ul>
                                                <li>
                                                    <label>Account Rep:</label><span id="siteAccountRep"></span></li>
                                                <li>
                                                    <label>Primary Engineer:</label><span id="sitePrimaryEngineer"></span></li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="widgetContactDetail">
                                        <div class="firstColumn">
                                            <h4><span class="yellowIcon phoneIcon"></span><span id="sitePhone"></span></h4>
                                        </div>
                                        <div class="thirdColumn">
                                            <h4><span class="pinkIcon webIcon"></span><a href="javascript:void(0)" id="siteWebAddress" target="_blank"></a></h4>
                                        </div>
                                        <div class="clear"></div>
                                        <div class="contactPersonDetail">
                                            <ul>
                                                <li>
                                                    <label>Primary Contact:</label><span id="sitePrimaryContact"></span></li>
                                                <li>
                                                    <label>Title:</label><span id="sitePrimaryTitle"></span></li>
                                            </ul>
                                            <div class="firstColumn">
                                                <h4><span class="yellowIcon phoneIcon"></span><span id="sitePrimaryPhone"></span></h4>
                                            </div>
                                            <div class="secondColumn">
                                                <h4><span class="greenIcon mailIcon"></span><a href="javascript:void(0)" id="sitePrimaryWebAddress"></a></h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                    <div class="clear"></div>

                    <div id="tabs" class="tabSectionWrap">
                        <div class="searchInput rightInput no-print" id="searchgrid" runat="server">
                            <form>
                                <input type="search" id="txtSearch" class="watermark" placeholder="Search User">
                                <input type="submit" id="btnSearch" value="">
                            </form>
                        </div>
                        <ul class="widgetHTab no-print" id="siteandprovisioning" runat="server">
                            <li><a href="#hTab-1"><span id="siteInfoTab"></span></a></li>
                            <li><a href="#hTab-2"><span id="provisioningInfoTab">Provisioning</span></a></li>
                            <asp:HiddenField ID="hidSiteId" runat="server" />
                        </ul>
                        <div id="hTab-1" class="tabPanelWrap">
                            <div id="innertabs">
                                <div class="tabHeaderWrap no-print">
                                    <div>
                                        <h2>
                                            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h2>
                                        <!--div class="actionPanel">
                                            <span class="tabActionInfo"><a href="javascript:void(0)"></a></span>
                                            <span class="tabActionAdd"><a href="javascript:void(0)"></a></span>
                                            <span class="tabActionEdit"><a href="javascript:void(0)"></a></span>
                                            <span class="tabActionClose"><a href="javascript:void(0)"></a></span>
                                            <input type="hidden" name="ispost" value="0" id="ispost" />
                                        </div-->
                                    </div>
                                </div>
                                <ul class="innerUl innerDetailWrapUl no-print" runat="server">
                                    <asp:Button ID="btnCustomerSubmit" runat="server" Text="" Style="display: none" OnClick="btnCustomerSubmit_Click"></asp:Button>
                                    <asp:HiddenField ID="hidTabname" runat="server" />
                                    <asp:HiddenField ID="hidIsIframe" runat="server" />
                                    <asp:HiddenField ID="hidIsIframedo" runat="server" />
                                    <li><a href="#innerTab" id="auserInfo" runat="server" onclick="ButtonEvent('Users', 0)"><span class="userInfoIcon"></span>Users</a></li>
                                    <li><a href="#innerTab" id="aworkStation" runat="server" onclick="ButtonEvent('Workstations', 1)"><span class="workStationIcon"></span>Workstations</a></li>
                                    <li><a href="#innerTab" id="alapTop" runat="server" onclick="ButtonEvent('Laptops', 2)"><span class="lapTopIcon"></span>Laptops</a></li>
                                    <li><a href="#innerTab" id="arouters" runat="server" onclick="ButtonEvent('Routers', 3)"><span class="routersIcon"></span>Routers</a></li>
                                    <li><a href="#innerTab" id="afirewalls" runat="server" onclick="ButtonEvent('Firewalls', 4)"><span class="firewallsIcon"></span>Firewalls</a></li>
                                    <li><a href="#innerTab" id="aswitches" runat="server" onclick="ButtonEvent('Network Switches', 5)"><span class="switchesIcon"></span>Network Switches</a></li>
                                    <li><a href="#innerTab" id="aprinters" runat="server" onclick="ButtonEvent('Printers', 6)"><span class="printersIcon"></span>Printers</a></li>
                                    <li><a href="#innerTab" id="aservers" runat="server" onclick="ButtonEvent('Servers', 7)"><span class="serversIcon"></span>Servers</a></li>
                                    <li><a href="#innerTab" id="amobileDev" runat="server" onclick="ButtonEvent('Mobile Devices', 8)"><span class="mobileDevIcon"></span>Mobile Devices</a></li>
                                    <li><a href="#innerTab" id="aphoneSys" runat="server" onclick="ButtonEvent('Phone System', 9)"><span class="phoneSysIcon"></span>Phone System</a></li>
                                    <li><a href="#innerTab" id="anetworkShares" runat="server" onclick="ButtonEvent('Network Shares', 10)"><span class="networkSharesIcon"></span>Network Shares</a></li>
                                    <li><a href="#innerTab" id="awireless" runat="server" onclick="ButtonEvent('Wireless', 11)"><span class="wirelessIcon"></span>Wireless</a></li>
                                    <li><a href="#innerTab" id="ainternet" runat="server" onclick="ButtonEvent('Internet/Web', 13)"><span class="internetIcon"></span>Internet/Web</a></li>
                                    
                                </ul>
                                <div id="innerTab" runat="server">
                                    <asp:PlaceHolder ID="ControlContainer" runat="server"></asp:PlaceHolder>
                                </div>

                            </div>
                        </div>

                        <div id="hTab-2" class="tabPanelWrap">
                            <div class="innerTabContent">
                                <div class="tabHeaderWrap no-print">
                                    <div>
                                        <h2>
                                            <asp:Label ID="lblHeaderPro" runat="server" Text=""></asp:Label></h2>
                                        <input type="hidden" name="ispost" value="0" id="ispost1" />
                                        <!--span class="tabActionInfo"><a href="#innerTab2" id="apro" runat="server" onclick="ProvisioningButtonEvent('Provisioning Check List')"></a></!--span-->
                                        <asp:Button ID="btnCustomerProvisioning" runat="server" Text="" Style="display: none" OnClick="btnCustomerProvisioning_Click"></asp:Button>
                                    </div>
                                </div>
                                <div id="innerTab2">
                                    <asp:PlaceHolder ID="ControlContainer2" runat="server"></asp:PlaceHolder>
                                </div>

                            </div>
                        </div>
                    </div>

                    <script>
                        $("#tabs").tabs();

                        var selectedTab = $.cookie("lefttab");

                        if ($.cookie("lefttab") == null) {
                            $("#innertabs").tabs({ active: 0 }).addClass("ui-tabs-vertical ui-helper-clearfix ui-tabs-active");
                        }
                        else {
                            $("#innertabs").tabs({ active: selectedTab }).addClass("ui-tabs-vertical ui-helper-clearfix ui-tabs-active");
                        }
                        $("#innertabs").removeClass("ui-corner-top").addClass("ui-corner-left");
                    </script>
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


