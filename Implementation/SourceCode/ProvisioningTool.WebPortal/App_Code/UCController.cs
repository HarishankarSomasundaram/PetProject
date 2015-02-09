using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Configuration;
using System.Collections;


/// <summary>
/// Summary description for UCController
/// </summary>
public class UCController : System.Web.UI.UserControl
{
    public ApplicationUser currentUser { get; set; }
    public Site currenSite { get; set; }
    public ProvisioningTool.Entity.ActionType CurrentAction { get; set; }
    public string Id { get; set; }
    public string nav { get; set; }
    public string sessionSiteId { get; set; }
    public string sessionCustomerId { get; set; }
    public string searchFilter { get; set; }


    PTResponse response = new PTResponse();
    ApplicationUserBLL applicationUserBLL = new ApplicationUserBLL();
    public string BaseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    public string MasterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    public string PostService = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["PostService"], "");
    public string GetService = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["GetService"], "");
    public string GetServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["GetService"], "");
    public string PostServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["PostService"], "");


    public const string GlobalMasterRouterModelMaster = "Router Models";
    public const string GlobalMasterRouterOSversion = "Router OS versions";
    public const string GlobalMasterRouterModules = "Router Modules";

    public const string GlobalMasterPhoneSystemModelMaster = "Phone System Models";
    public const string GlobalMasterPhoneSystemOSversion = "Phone System OS versions";
    public const string GlobalMasterPhoneSystemModules = "Phone System Modules";

    public const string GlobalMasterTitles = "Titles";
    public const string GlobalMasterDepartment = "Departments";
    public const string GlobalMasterRemoteAccess = "Remote Access";
    public const string GlobalMasterMobilePhone = "Mobile Phones";
    public const string GlobalMasterTablet = "Tablets";
    public const string GlobalMasterApp = "Applications";
    public const string GlobalSystemRoles = "Roles";
    public const string GlobalSystemOS = "Operating Systems";
    public const string GloablSystemAntivirus = "Antivirus";
    public const string GlobalSystemBackupSoftware = "Backup Softwares";
    public const string GlobalSystemApplicationSoftware = "Application Softwares";
    public const string GlobalSystemHardwareCPU = "CPUs";
    public const string GlobalSystemHardwareMemory = "Memory";
    public const string GlobalSystemHardwareMotherBoard = "MotherBoards";
    public const string GlobalSystemHardwareChipSet = "Chipsets";
    public const string GlobalSystemHardwareVideoCard = "Video Cards";
    public const string GlobalSystemHardwareDisplay = "Displays";
    public const string GlobalSystemHardwareMultimedia = "Multimedia";
    public const string GlobalSystemHardwarePorts = "Ports";
    public const string GlobalSystemHardwareSlots = "Slots";
    public const string GlobalSystemHardwareChasis = "Chassis";
    public const string GlobalSystemHardwarePower = "Powers";
    public const string GlobalRouterModel = "Router Models";
    public const string GlobalRouterOS = "Router OS versions";
    public const string GlobalRouterModule = "Router Modules";
    public const string GlobalPrinterModel = "Printer Models";
    public const string GlobalFirewallModel = "Firewall Models";
    public const string GlobalFirewallOS = "Firewall OS Versions";
    public const string GlobalFirewallModule = "Firewall Modules";
    public const string GlobalMobileType = "Mobile Device Types";
    public const string GlobalMobileManufacture = "Mobile Device Manufacturers";
    public const string GlobalMobileModel = "Mobile Device Models";
    public const string GlobalNSModel = "Network Switch Models";
    public const string GlobalNSOS = "Network Switch OS Versions";
    public const string GlobalNSModule = "Network Switch Modules";
    public const string GlobalPSModel = "Phone System Models";
    public const string GlobalPSOS = "Phone System OS versions";
    public const string GlobalPSModule = "Phone System Modules";
    public const string GlobalWirelessType = "Wireless Types";
    public const string GlobalWirelessManfacture = "Wireless Manufacturers";
    public const string GlobalWirelessModel = "Wireless Models";
    public const string GlobalPrinterOS = "Printer OS Versions";
    public const string GlobalPrinterModule = "Printer Modules";
    public const string GlobalWorkstationManufacturesModule = "Workstation Manufacturers";
    public const string GlobalWorkstationTypesModule = "Workstation Types"; 
    public const string GlobalWorkstationModelsModule = "Workstation Models"; 

    #region [ GlobalMasters Tables MasterName ]
    public string SiteCityName = "Cities";
    public string SiteStateName = "States";
    public string SiteCountryName = "Countries";
    public string SiteAccRep = "Account Rep";
    public string SitePriEng = "Primary Engineers";

    #endregion [ GlobalMasters Tables MasterName ]

    public UCController() { }
    protected void Page_Init(object Sender, EventArgs e)
    {
        currentUser = GetApplicationUserByUserName();

        if (currentUser == null)
        {
            RedirectLoginPage();
        }
        // not in use
        currenSite = GetCurrenSite();

        string sPageType = Request["do"] + "";
        switch (sPageType.ToLower())
        {
            case "v":
                CurrentAction = ProvisioningTool.Entity.ActionType.View;
                break;
            case "a":
                CurrentAction = ProvisioningTool.Entity.ActionType.Add;
                break;
            case "e":
                CurrentAction = ProvisioningTool.Entity.ActionType.Edit;
                break;
            case "p":
                CurrentAction = ProvisioningTool.Entity.ActionType.Print;
                break;
            case "m":
                CurrentAction = ProvisioningTool.Entity.ActionType.MoreView;
                break;
            default:
                CurrentAction = ProvisioningTool.Entity.ActionType.View;
                break;
        }

        if (this.Request.QueryString["search"] != null)
        {
            if (this.Request.QueryString["CID"] != null)
            {
                this.Session["sessionCustomerId"] = this.Request.QueryString["CID"];
                //this.Response.Redirect("CustomerInfo.aspx?adv=1", true);
                sessionCustomerId = this.Session["sessionCustomerId"] + "";
                HttpCookie cookieCustomerId = new HttpCookie("sessionCustomerId");
                // Set the cookie value.
                cookieCustomerId.Value = sessionCustomerId;
                // Set the cookie expiration date.
                cookieCustomerId.Expires = DateTime.Now.AddHours(1);
                // Add the cookie.
                Response.Cookies.Add(cookieCustomerId);
            }
        }
        else
        {
            if (this.Session["sessionCustomerId"] != null)
            {
                sessionCustomerId = this.Session["sessionCustomerId"] + "";
                HttpCookie cookieCustomerId = new HttpCookie("sessionCustomerId");
                // Set the cookie value.
                cookieCustomerId.Value = sessionCustomerId;
                // Set the cookie expiration date.
                cookieCustomerId.Expires = DateTime.Now.AddHours(1);
                // Add the cookie.
                Response.Cookies.Add(cookieCustomerId);
            }
        }

        Id = Request["id"] + "";
        nav = Request["nav"] + "";

        HttpCookie siteIDCookie = new HttpCookie("siteID");
        siteIDCookie.Expires = DateTime.Now.AddHours(1);
        siteIDCookie = Request.Cookies["siteID"];
        if (siteIDCookie != null)
        {
            sessionSiteId = siteIDCookie.Value;
        }


        HttpCookie sessionCustomerIdCookie = new HttpCookie("sessionCustomerId");
        sessionCustomerIdCookie.Expires = DateTime.Now.AddHours(1);
        sessionCustomerIdCookie = Request.Cookies["sessionCustomerId"];
        if (sessionCustomerIdCookie != null && sessionCustomerId == null)
        {
            sessionCustomerId = sessionCustomerIdCookie.Value;
        }

        HttpCookie searchFilterCookie = new HttpCookie("searchFilter");
        searchFilterCookie.Expires = DateTime.Now.AddHours(1);
        searchFilterCookie = Request.Cookies["searchFilter"];
        if (searchFilterCookie != null)
        {
            searchFilter = searchFilterCookie.Value;
        }

        Session["CurrentLoggedInUser"] = currentUser;
        Session["CurrentLoggedInUserName"] = currentUser.ApplicationUsername;
        Session["CurrentLoggedInAppID"] = currentUser.ApplicationUserID;
    }

    private Site GetCurrenSite()
    {
        Site site = new Site();
        if (Session["SiteDetails"] != null)
        {
            site = Session["SiteDetails"] as Site;
        }
        else
            site = null;
        return site;
    }

    private ApplicationUser GetApplicationUserByUserName()
    {
        string username = ConvertHelper.ConvertToString(CookieHelper.GetCookie(System.Web.HttpContext.Current.Request, "UserName"));
        bool IsAuthenticated = ConvertHelper.ConvertToBoolean(CookieHelper.GetCookie(System.Web.HttpContext.Current.Request, "IsAuthenticated"));
        ApplicationUser applicationUser;
        if (IsAuthenticated)
        {
            applicationUser = Session["UserDetails"] as ApplicationUser;
        }
        else
            return null;

        return applicationUser;
    }
    public void RedirectLoginPage()
    {
        Response.Redirect("~/App/Login.aspx");
    }

    #region [Populate Master Details DropDown]

    public DropDownList PopulateMasterDetailDropDownList(DropDownList ddlMasterDetail, List<GlobalMasterDetail> companies, bool includeSelect)
    {
        return PopulateMasterDetailDropDownList(ddlMasterDetail, companies, includeSelect, false);
    }
    public DropDownList PopulateMasterDetailDropDownList(DropDownList ddlMasterDetail, List<GlobalMasterDetail> companies, bool includeSelect, bool activateResult)
    {
        if (companies != null && companies.Count > 0)
        {
            ListItem li;
            ddlMasterDetail.Items.Clear();
            if (includeSelect)
                ddlMasterDetail.Items.Add(new ListItem("", "0"));

            if (activateResult)
                companies.ForEach(delegate(GlobalMasterDetail g) { li = new ListItem(g.MasterValue, g.MasterDetailID.ToString()); li.Attributes.Add("class", "active-result"); ddlMasterDetail.Items.Add(li); });
            else
                companies.ForEach(delegate(GlobalMasterDetail g) { li = new ListItem(g.MasterValue, g.MasterDetailID.ToString()); ddlMasterDetail.Items.Add(li); });
        }
        else
            return null;
        return ddlMasterDetail;
    }
    #region [ PopulateUsersDropDownList ]

    public void PopulateGlobalMasterDropdown(PTRequest request, DropDownList control, bool includeSelect = true)
    {
        PopulateGlobalMasterDropdown(request, control, includeSelect, false);
    }

    public void PopulateGlobalMasterDropdown(PTRequest request, DropDownList control, bool includeSelect = true, bool activateResult = false)
    {
        string serviceResponseString = string.Empty;
        PTResponse response = new PTResponse();
        WebServiceHelper webServiceHelper = new WebServiceHelper();
        response = webServiceHelper.PostRequest<PTResponse>(request);

        if (response != null && response.GlobalMaster != null && response.GlobalMaster.GlobalMasterDetailList != null && response.GlobalMaster.GlobalMasterDetailList.Count > 0)
            PopulateMasterDetailDropDownList(control, response.GlobalMaster.GlobalMasterDetailList, includeSelect, activateResult);
        else
            DropdownAddItem(control);
    }

    #endregion [ PopulateUsersDropDownList ]

    #region [MultipleItemsSelectByValuesForDropdown]
    public void MultipleItemsSelectByValuesForDropdown(DropDownList control, string values, char delimiter)
    {
        try
        {
            if (values != null && values != "" && control != null && control.Items != null && control.Items.Count > 0)
            {
                control.Items.Add(new ListItem("", "0"));
                string[] ddlValues = values.Split(delimiter);
                foreach (string value in ddlValues)
                {
                    control.Items.FindByValue(value).Attributes.Add("selected", "selected");
                }

            }
        }
        catch (Exception)
        {
        }

    }
    #endregion [MultipleItemsSelectByValuesForDropdown]

    #region [ PopulateGlobalmasterDropDownList ]

    public void PopulateGlobalMasterDropdown(string url, DropDownList control)
    {
        string serviceResponseString = string.Empty;
        PTResponse response = new PTResponse();
        WebServiceHelper webServiceHelper = new WebServiceHelper();
        serviceResponseString = webServiceHelper.GetRequest(url);

        if (ConvertHelper.ConvertToString(serviceResponseString) != null)
        {
            response.GlobalMasterDetailList = webServiceHelper.ConvertToObjectList<GlobalMasterDetail>(serviceResponseString);
        }
        if (response != null && response.GlobalMasterDetailList != null && response.GlobalMasterDetailList.Count > 0)
            PopulateMasterDetailDropDownList(control, response.GlobalMasterDetailList, true);
    }

    #endregion [ PopulateGlobal masterDropDownList ]

    public DropDownList PopulateUserDropDownList(DropDownList ddlUser, List<User> users, bool includeSelect)
    {
        if (users != null && users.Count > 0)
        {
            ListItem li;
            ddlUser.Items.Clear();
            if (includeSelect)
                ddlUser.Items.Add(new ListItem("", "0"));
            users.ForEach(delegate(User g) { li = new ListItem(g.UserName, g.UserID.ToString()); ddlUser.Items.Add(li); });
        }
        else
            return null;
        return ddlUser;
    }

    public DropDownList PopulatePrinterDropDownList(DropDownList ddlPrinter, List<Printer> Printers, bool includeSelect)
    {
        if (Printers != null && Printers.Count > 0)
        {
            ListItem li;
            ddlPrinter.Items.Clear();
            if (includeSelect)
                ddlPrinter.Items.Add(new ListItem("", "0"));
            Printers.ForEach(delegate(Printer g) { li = new ListItem(g.Hostname, g.PrinterID.ToString()); ddlPrinter.Items.Add(li); });
        }
        else
            return null;
        return ddlPrinter;
    }

    public DropDownList PopulateWorkStationDropDownList(DropDownList ddlWorkStation, List<WorkStationInfo> WorkStations, bool includeSelect)
    {
        if (WorkStations != null && WorkStations.Count > 0)
        {
            ListItem li;
            ddlWorkStation.Items.Clear();
            if (includeSelect)
                ddlWorkStation.Items.Add(new ListItem("", "0"));

            WorkStations.ForEach(delegate(WorkStationInfo g) { li = new ListItem(g.HostName, g.WorkStationID.ToString()); ddlWorkStation.Items.Add(li); });
        }
        else
            return null;
        return ddlWorkStation;
    }

    public DropDownList PopulateLaptopDropDownList(DropDownList ddlLaptop, List<LaptopInfo> Laptops, bool includeSelect)
    {
        if (Laptops != null && Laptops.Count > 0)
        {
            ListItem li;
            ddlLaptop.Items.Clear();
            if (includeSelect)
                ddlLaptop.Items.Add(new ListItem("", "0"));
            Laptops.ForEach(delegate(LaptopInfo g) { li = new ListItem(g.HostName, g.LaptopID.ToString()); ddlLaptop.Items.Add(li); });
        }
        else
            return null;
        return ddlLaptop;
    }

    public DropDownList PopulateRouterDropDownList(DropDownList ddlRouter, List<Router> routers, bool includeSelect)
    {
        if (routers != null && routers.Count > 0)
        {
            ListItem li;
            ddlRouter.Items.Clear();
            if (includeSelect)
                ddlRouter.Items.Add(new ListItem("", "0"));
            routers.ForEach(delegate(Router g) { li = new ListItem(g.Hostname, g.RouterID.ToString()); ddlRouter.Items.Add(li); });
        }
        else
            return null;
        return ddlRouter;
    }

    public DropDownList PopulateFirewallDropDownList(DropDownList ddlFirewall, List<Firewall> firewalls, bool includeSelect)
    {
        if (firewalls != null && firewalls.Count > 0)
        {
            ListItem li;
            ddlFirewall.Items.Clear();
            if (includeSelect)
                ddlFirewall.Items.Add(new ListItem("", "0"));
            firewalls.ForEach(delegate(Firewall g) { li = new ListItem(g.Hostname, g.FirewallID.ToString()); ddlFirewall.Items.Add(li); });
        }
        else
            return null;
        return ddlFirewall;
    }

    public DropDownList PopulateNetworkSwitchDropDownList(DropDownList ddlNetworkSwitch, List<NetworkSwitch> networkswitchs, bool includeSelect)
    {
        if (networkswitchs != null && networkswitchs.Count > 0)
        {
            ListItem li;
            ddlNetworkSwitch.Items.Clear();
            if (includeSelect)
                ddlNetworkSwitch.Items.Add(new ListItem("", "0"));
            networkswitchs.ForEach(delegate(NetworkSwitch g) { li = new ListItem(g.Hostname, g.NetworkSwitchID.ToString()); ddlNetworkSwitch.Items.Add(li); });
        }
        else
            return null;
        return ddlNetworkSwitch;
    }

    public DropDownList PopulateMobileDeviceDropDownList(DropDownList ddlMobileDevice, List<MobileDevice> MobileDevices, bool includeSelect)
    {
        if (MobileDevices != null && MobileDevices.Count > 0)
        {
            ListItem li;
            ddlMobileDevice.Items.Clear();
            if (includeSelect)
                ddlMobileDevice.Items.Add(new ListItem("", "0"));
            MobileDevices.ForEach(delegate(MobileDevice g) { li = new ListItem(g.Hostname, g.MobileDeviceID.ToString()); ddlMobileDevice.Items.Add(li); });
        }
        else
            return null;
        return ddlMobileDevice;
    }
    #region [POPULATE TABLETS]
    public DropDownList PopulateTabletDropDownList(DropDownList ddlTablet, List<MobileDevice> Tablets, bool includeSelect)
    {
        if (Tablets != null && Tablets.Count > 0)
        {
            ListItem li;
            ddlTablet.Items.Clear();
            if (includeSelect)
                ddlTablet.Items.Add(new ListItem("", "0"));
            Tablets.ForEach(delegate(MobileDevice g) { li = new ListItem(g.Hostname, g.MobileDeviceID.ToString()); ddlTablet.Items.Add(li); });
        }
        else
            return null;
        return ddlTablet;
    }

    #endregion

    public DropDownList PopulatePhoneSystemDropDownList(DropDownList ddlPhoneSystem, List<PhoneSystem> phonesystems, bool includeSelect)
    {
        if (phonesystems != null && phonesystems.Count > 0)
        {
            ListItem li;
            ddlPhoneSystem.Items.Clear();
            if (includeSelect)
                ddlPhoneSystem.Items.Add(new ListItem("", "0"));
            phonesystems.ForEach(delegate(PhoneSystem g) { li = new ListItem(g.Hostname, g.PhoneSystemID.ToString()); ddlPhoneSystem.Items.Add(li); });
        }
        else
            return null;
        return ddlPhoneSystem;
    }

    public DropDownList PopulateWirelessDropDownList(DropDownList ddlWireless, List<Wireless> Wirelessls, bool includeSelect)
    {
        if (Wirelessls != null && Wirelessls.Count > 0)
        {
            ListItem li;
            ddlWireless.Items.Clear();
            if (includeSelect)
                ddlWireless.Items.Add(new ListItem("", "0"));
            Wirelessls.ForEach(delegate(Wireless g) { li = new ListItem(g.Hostname, g.WirelessID.ToString()); ddlWireless.Items.Add(li); });
        }
        else
            return null;
        return ddlWireless;
    }

    public DropDownList PopulateServerDropDownList(DropDownList ddlServer, List<ServerInfo> Servers, bool includeSelect)
    {
        if (Servers != null && Servers.Count > 0)
        {
            ListItem li;
            ddlServer.Items.Clear();
            if (includeSelect)
                ddlServer.Items.Add(new ListItem("", "0"));
            Servers.ForEach(delegate(ServerInfo g) { li = new ListItem(g.HostName, g.ServerID.ToString()); ddlServer.Items.Add(li); });
        }
        else
            return null;
        return ddlServer;
    }

    public DropDownList PopulateNetworkSharesDropDownList(DropDownList ddlNetworkShares, List<NetworkShareDetail> NetworkShareDetailList, bool includeSelect)
    {
        if (NetworkShareDetailList != null && NetworkShareDetailList.Count > 0)
        {
            ListItem li;
            ddlNetworkShares.Items.Clear();
            if (includeSelect)
                ddlNetworkShares.Items.Add(new ListItem("", "0"));
            NetworkShareDetailList.ForEach(delegate(NetworkShareDetail g) { li = new ListItem(g.NetworkShareName, g.NetworkShareID.ToString()); ddlNetworkShares.Items.Add(li); });
        }
        else
            return null;
        return ddlNetworkShares;
    }

    public DropDownList PopulateServerModelDropDownList(DropDownList ddlModel, List<ServerHardware> serverHardware, bool includeSelect)
    {
        if (serverHardware != null && serverHardware.Count > 0)
        {
            ListItem li;
            ddlModel.Items.Clear();
            if (includeSelect)
                ddlModel.Items.Add(new ListItem("", "0"));
            serverHardware.ForEach(delegate(ServerHardware g) { li = new ListItem(g.ModelName, g.ServerHardwareID.ToString()); ddlModel.Items.Add(li); });
        }
        else
            return null;
        return ddlModel;
    }

    public DropDownList PopulateWorkStationModelDropDownList(DropDownList ddlModel, List<WorkStationHardware> workStationHardware, bool includeSelect)
    {
        if (workStationHardware != null && workStationHardware.Count > 0)
        {
            ListItem li;
            ddlModel.Items.Clear();
            if (includeSelect)
                ddlModel.Items.Add(new ListItem("", "0"));
            workStationHardware.ForEach(delegate(WorkStationHardware g) { li = new ListItem(g.ModelName, g.WorkStationHardwareID.ToString()); ddlModel.Items.Add(li); });
        }
        else
            return null;
        return ddlModel;
    }

    public DropDownList PopulateLaptopModelDropDownList(DropDownList ddlModel, List<LaptopHardware> laptopHardware, bool includeSelect)
    {
        if (laptopHardware != null && laptopHardware.Count > 0)
        {
            ListItem li;
            ddlModel.Items.Clear();
            if (includeSelect)
                ddlModel.Items.Add(new ListItem("", "0"));
            laptopHardware.ForEach(delegate(LaptopHardware g) { li = new ListItem(g.ModelName, g.LaptopHardwareID.ToString()); ddlModel.Items.Add(li); });
        }
        else
            return null;
        return ddlModel;
    }

    public DropDownList PopulateHardDiskDropDownList(DropDownList ddlHardDisk, List<SystemHardDrive> systemHardDrive, bool includeSelect)
    {
        if (systemHardDrive != null && systemHardDrive.Count > 0)
        {
            ListItem li;
            ddlHardDisk.Items.Clear();
            if (includeSelect)
                ddlHardDisk.Items.Add(new ListItem("", "0"));
            systemHardDrive.ForEach(
                delegate(SystemHardDrive g)
                {
                    li = new ListItem(g.HardDriveDetails, g.SystemID.ToString());
                    li.Attributes.Add("class", "active-result");
                    ddlHardDisk.Items.Add(li);
                });
        }
        else
            return null;
        return ddlHardDisk;
    }

    #endregion

    #region [ Message ]
    /// <summary>
    /// Show Error Message
    /// </summary>
    /// <param name="Message"></param>
    public void ShowMessage(string Message)
    {
        ShowMessage(Message, false);
    }
    public void HideMessage()
    {
        Label lblErrorMessage = (Label)Page.FindControl("lblErrorMessage");
        Panel pnlError = (Panel)Page.FindControl("pnlError");
        HtmlGenericControl divMessage = new HtmlGenericControl();
        //for pages has lblMessage control to display error messages
        Label lblMessage = (Label)Page.FindControl("lblMessage");
        if (lblMessage == null)//For Master pages
        {
            ContentPlaceHolder cph = Page.Master.FindControl("MainContent") as ContentPlaceHolder;
            lblMessage = (Label)cph.FindControl("lblMessage");
            divMessage = (HtmlGenericControl)cph.FindControl("divMessage");
        }
        else
        {
            divMessage = (HtmlGenericControl)Page.FindControl("divMessage");
        }
        if (divMessage != null)
            divMessage.Attributes["style"] = "display:none";
    }
    public void ShowMessage(string Message, bool IsSuccess)
    {
        Label lblErrorMessage = (Label)FindControl("lblErrorMessage");


        ShowMessage(Message, IsSuccess, lblErrorMessage);
    }
    public void ShowMessage(string Message, bool IsSuccess, Label lblErrorMessage)
    {

        if (string.IsNullOrEmpty(Message))
        {
            lblErrorMessage.Text = "";
        }
        else
        {
            if (lblErrorMessage != null)
            {
                lblErrorMessage.Text = Message;
                if (IsSuccess == true)
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                else
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    public void ShowGlobalMessage(string message)
    {
        /*
        HtmlGenericControl objHTML = (HtmlGenericControl)Page.FindControl("emsBreadCrumb").FindControl("globalsuccess");
        Label lblGlobalMessage = (Label)Page.FindControl("emsBreadCrumb").FindControl("lblGlobalMessage");
        lblGlobalMessage.Text = message;
        objHTML.Visible = true;
         */
    }

    public void ShowGlobalErrorMessage(string message)
    {
        /*
        HtmlGenericControl objHTML = (HtmlGenericControl)Page.FindControl("emsBreadCrumb").FindControl("globalerror");
        Label lblGlobalErrorMessage = (Label)Page.FindControl("emsBreadCrumb").FindControl("lblGlobalErrorMessage");
        lblGlobalErrorMessage.Text = message;
        objHTML.Visible = true;
         */
    }

    protected virtual void DisableControls(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control control in ctrl.Controls)
            {
                if (control is TextBox)
                    ((TextBox)control).ReadOnly = true;
                if (control is HtmlInputButton)
                    ((HtmlInputButton)control).Disabled = true;
                if (control is DropDownList)
                    ((DropDownList)control).Enabled = false;
                if (control is CheckBoxList)
                    ((CheckBoxList)control).Enabled = false;
                if (control is CheckBox)
                    ((CheckBox)control).Enabled = false;
                if (control is RadioButton)
                    ((RadioButton)control).Enabled = false;
                if (control is RadioButtonList)
                    ((RadioButtonList)control).Enabled = false;
                if (control is LinkButton)
                    ((LinkButton)control).Enabled = true;
                if (control is Button)
                    ((Button)control).Enabled = false;
                if (control is Panel)
                {
                    //foreach (Control control1 in control.Controls)
                    DisableControls((Control)control);
                }
                if (control is HtmlGenericControl)
                    DisableControls((HtmlGenericControl)control);

            }
        }
    }

    public void ShowOrHide(object obj, bool isShow)
    {
        switch (obj.GetType().Name)
        {
            case "Label":
                ((Label)(obj)).Visible = isShow;
                break;
            case "TextBox":
                ((TextBox)(obj)).Visible = isShow;
                break;
            case "DropDownList":
                ((DropDownList)(obj)).Visible = isShow;
                break;
            case "HyperLink":
            case "a":
            case "HtmlAnchor":
                ((HtmlAnchor)(obj)).Visible = isShow;
                break;
            case "HtmlGenericControl":
                ((HtmlGenericControl)(obj)).Visible = isShow;
                break;
            case "LinkButton":
                ((LinkButton)(obj)).Visible = isShow;
                break;
            case "HtmlTableRow":
                ((HtmlTableRow)(obj)).Visible = isShow;
                break;
            default:
                ((TextBox)(obj)).Visible = isShow;
                break;
        }
    }

    #region [Bind MultiSelect DropDown With Selected Values]

    public void SetMultiSelectDropDown(DropDownList ddl, string sValue)
    {
        if (sValue != null && sValue != "")
        {
            string sMemory = sValue;
            ArrayList arrMemory = new ArrayList(); ;
            arrMemory.AddRange(sMemory.Split(new char[] { ',' }));
            foreach (string s in arrMemory)
            {
                ddl.Items.FindByValue(s).Attributes.Add("selected", "selected");
            }
        }
    }

    #endregion [Bind MultiSelect DropDown With Selected Values]

    #endregion [ Message ]

    #region [Initilize the Iframe Div based on operation request]
    /// <summary>
    /// Initilize the iframe based on cookie and request 
    /// </summary>
    /// <param name="divCrud"></param>
    /// <param name="divGrid"></param>
    public void InitializeIframe(HtmlGenericControl divCrud, HtmlGenericControl divGrid)
    {

        #region [Inetillize the cookie for iframe operations]

        string isIframe = Request.QueryString["iframe"] != null ? ConvertHelper.ConvertToString(Request.QueryString["iframe"], "") : "";
        string isIframeOperation = Request.QueryString["iframedo"] != null ? ConvertHelper.ConvertToString(Request.QueryString["iframedo"], "") : "";

        HttpCookie isIframeCookie = new HttpCookie("isIframe");
        // Set the cookie expiration date.
        isIframeCookie.Expires = DateTime.Now.AddHours(1);
        isIframeCookie = Request.Cookies["isIframe"];

        HttpCookie isIframeCookieOperation = new HttpCookie("isIframeCookieOperation");
        // Set the cookie expiration date.
        isIframeCookieOperation.Expires = DateTime.Now.AddHours(1);
        isIframeCookieOperation = Request.Cookies["isIframeCookieOperation"];

        //check the iframe operation for Add
        if (isIframe != "" && isIframeOperation != "" && isIframe == "1" && isIframeOperation == "a")
        {
            CurrentAction = ProvisioningTool.Entity.ActionType.Add;
            if (isIframeCookie != null)
                isIframeCookie.Value = "1";
            //else do nothing
        }
        //check the iframe operation for Edit
        else if (isIframe != "" && isIframeOperation != "" && isIframe == "1" && isIframeOperation == "e")
        {
            if (isIframeCookieOperation != null)
            {
                isIframeCookieOperation.Value = "e";
            }
            //else do nothing
            if (isIframeCookie != null)
            {
                isIframeCookie.Value = "1";
                divCrud.Visible = false;
                divGrid.Visible = true;
            }
            //else do nothing
        }
        else
        {
            if (isIframeCookie != null)
                isIframeCookie.Value = "";
            //else do nothing
            if (isIframeCookieOperation != null)
                isIframeCookieOperation.Value = "";
            //else do nothing

        }
        #endregion [Inetillize the cookie for iframe operations]


    }
    #endregion

    #region [ Add Drop down First Item ]
    public void DropdownAddItem(DropDownList ddl)
    {
        ddl.Items.Add(new ListItem("", "0"));
    }
    #endregion [ Add Drop down First Item ]

    #region [ Close Color Box ]
    //Funtion to Close the Color Box 
    public void CloseColorBox()
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseColorBox", "<script>parent.$.colorbox.close();</script>");
    }
    #endregion [ Close Color Box ]

    #region [ Set Drop down list selected Item by Text ]

    //Function for Set Dropdown
    public void SetDropdown(DropDownList ddl, string sValue)
    {
        try
        {
            if (sValue != "")
            {
                ddl.SelectedIndex = -1;
                foreach (ListItem item in ddl.Items)
                {
                    if (item.Text.Equals(sValue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        item.Selected = true;
                        break;
                    }
                }
                // ddl.SelectedValue = ddlCity.Items.FindByText(sValue).Value;
                ddl.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion [ Set Drop down list selected Item by Text ]

    #region [ Add new Global Master Detail's Value ]

    //Fucntion will Check the Global Master Details value available and insert a new value
    public bool GlobalMasterDetailsAdd(string sMasterName, string sMasterValue)
    {
        try
        {
            PTResponse gmdResponse = new PTResponse();
            PTRequest gmdRequest = new PTRequest();
            GlobalMasterDetail globalMasterDetail = new GlobalMasterDetail();
            globalMasterDetail.MasterValue = sMasterValue;
            globalMasterDetail.CreatedBy = currentUser.ApplicationUserID;
            globalMasterDetail.ModifiedBy = currentUser.ApplicationUserID;
            globalMasterDetail.SiteName = sMasterName;
            globalMasterDetail.oper = "ADD";
            gmdRequest.GlobalMasterDetail = new GlobalMasterDetail();
            gmdRequest.GlobalMasterDetail = globalMasterDetail;

            WebServiceHelper webServiceHelper = new WebServiceHelper();
            // string serviceURL = string.Empty;

            // serviceURL = PostServiceURL + "GlobalMasterCrud";
            gmdRequest.URL = string.Format(PostServiceURL + "{0}", "GlobalMasterCrud");
            gmdResponse = new PTResponse();
            gmdResponse = webServiceHelper.PostRequest<PTResponse>(gmdRequest);

            if (gmdResponse != null)
                return gmdResponse.isSuccess;
            else
                return false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }
    #endregion [ Add new Global Master Detail's Value ]



}