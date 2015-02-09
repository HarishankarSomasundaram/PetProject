using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;

public partial class CustomerInfo : FormController
{
    string ucpath = string.Empty;
    string tabname = string.Empty;
    PTRequest request;
    PTResponse response;
    SiteBLL siteBLL;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //check the iframe cookie to hide the contents
        if (ConvertHelper.ConvertToString(Request.QueryString["nav"]) != null)
        {
            #region [Inetillize the cookie for iframe operations]

            HttpCookie isIframeCookie = new HttpCookie("isIframe");
            isIframeCookie.Expires = DateTime.Now.AddHours(1);
            isIframeCookie = Request.Cookies["isIframe"];

            if (Request.QueryString["isColorBox"] != null)
            {
                    searchgrid.Style.Add("display", "none");
                    siteandprovisioning.Style.Add("display", "none");
                    hideContent.Style.Add("display", "none");
                    innerTab.Style["margin-left"] = "0px !important";
                    PageBody.Attributes.Add("class", "colorbox-parent");
            }
            #endregion [Inetillize the cookie for iframe operations]

            ucpath = ConvertHelper.ConvertToString(Request.QueryString["nav"], "");

            GetUserContaolPath(ucpath);
        }
        PopulateControls();
    }

    #region[To Invoke UserControl]

    /// <summary>
    /// Gets or sets the control's saved virtual path for control
    /// recreation. The saved virtual path is stored in the view state.
    /// </summary>
    private string SavedControlVirtualPath
    {
        get
        {
            if (ViewState["saved"] == null ||
                (string)ViewState["saved"] == string.Empty)
                return null;
            return (string)ViewState["saved"];
        }
        set
        {
            ViewState["saved"] = value;
        }
    }

    /// <summary>
    /// Recreates and reloads the control according to the saved virtual
    /// path. REMEMBER to give a different ID to each type of web user
    /// control that we need to create.
    /// </summary>
    private void ReloadControl(string UCPath)
    {

        if (UCPath.ToUpper() == "PROVISIONING CHECK LIST")
        {
            ControlContainer2.Controls.Clear();
            if (SavedControlVirtualPath != null)
            {
                Control control = this.LoadControl(SavedControlVirtualPath);
                if (control != null)
                {
                    // Gives the control a unique ID. It is important to ensure
                    // the page working properly. Here we use control.GetType().Name
                    // as the ID.
                    control.ID = control.GetType().Name;
                    ControlContainer2.Controls.Add(control);
                }
            }
        }
        else
        {
            ControlContainer.Controls.Clear();
            if (SavedControlVirtualPath != null)
            {
                Control control = this.LoadControl(SavedControlVirtualPath);
                if (control != null)
                {
                    // Gives the control a unique ID. It is important to ensure
                    // the page working properly. Here we use control.GetType().Name
                    // as the ID.
                    control.ID = control.GetType().Name;
                    ControlContainer.Controls.Add(control);
                }
            }
        }
    }
    #endregion[To Invoke UserControl]

    protected void btnCustomerSubmit_Click(object sender, EventArgs e)
    {
        tabname = hidTabname.Value != null ? hidTabname.Value : "";
        lblHeader.Text = tabname;
        GetUserContaolPath(tabname);
        if (ConvertHelper.ConvertToString(hidIsIframe.Value, "") == "1")
            Response.Redirect("CustomerInfo.aspx?do=v&nav=" + tabname + "&iframe=1&do=" + ConvertHelper.ConvertToString(hidIsIframedo.Value, "") == "a" ? "a" : "e", false);
        else
            Response.Redirect("CustomerInfo.aspx?do=v&nav=" + tabname, false);
    }


    private void GetUserContaolPath(string UCPath)
    {
        if (UCPath.ToUpper() == "PROVISIONING CHECK LIST")
            lblHeaderPro.Text = "Provisioning Check List";
        else
            lblHeader.Text = UCPath;

        switch (UCPath.ToUpper())
        {
            case "USERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/UserInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "ROUTERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/RouterInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "FIREWALLS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/FirewallInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "NETWORK SWITCHES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/NetworkSwitchInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "SERVERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/ServerInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "MOBILE DEVICES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/MobileDeviceInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "PHONE SYSTEM":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PhoneSystemInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "WORKSTATIONS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/WorkStation.ascx";
                ReloadControl(UCPath);
                break;
            case "LAPTOPS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/Laptop.ascx";
                ReloadControl(UCPath);
                break;

            case "PRINTERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PrinterInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "NETWORK SHARES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/NetworkShare.ascx";
                ReloadControl(UCPath);
                break;
            case "WIRELESS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/WirelessInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "SITE TO SITE":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PageUnderConstruction.ascx";
                ReloadControl(UCPath);
                break;
            case "SOFTWARES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/SoftwareInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-DOMAIN":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-PROVIDER":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-EMAIL":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-WEBHOST":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "GROUP POLICIES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/GroupPolicy.ascx";
                ReloadControl(UCPath);
                break;
            case "SYSTEM AUDIT":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PageUnderConstruction.ascx";
                ReloadControl(UCPath);
                break;
            case "SUPPORT TEAM":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PageUnderConstruction.ascx";
                ReloadControl(UCPath);
                break;
            case "PROVISIONING CHECK LIST":
                SavedControlVirtualPath = "~/includes/UserControls/pages/ProvisioningCheckList.ascx";
                ReloadControl(UCPath);
                break;

        }
    }


    #region [Populate Dropdowns]
    private void PopulateControls()
    {
        try
        {

            #region [GE ALL PrinterS AND POPULATE]
            WebServiceHelper webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            PTRequest request = new PTRequest();
            if (ConvertHelper.ConvertToString(base.sessionCustomerId) != null)
            {
                serviceURL = PostServiceURL + "GETSITESBYCUSTOMERID";
                request.Site = new Site();
                request.Site.Customer = new Customer();
                request.Site.Customer.CustomerID = ConvertHelper.ConvertToInteger(base.sessionCustomerId);
                request.URL = serviceURL;


                webServiceHelper = new WebServiceHelper();
                response = webServiceHelper.PostRequest<PTResponse>(request);


                if (response != null && response.SiteList != null && response.SiteList.Count > 0)
                {
                    PopulateSiteDropDownList(ddlCustomerSites, response.SiteList, false);
                }
            }
            #endregion

            #region [Bind Customer]
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            request = new PTRequest();
            if (ConvertHelper.ConvertToString(base.sessionCustomerId) != null)
            {
                serviceURL = PostServiceURL + "GETALLCUSTOMERBYCUSTOMERID";
                request.Customer = new Customer();
                request.Customer.CustomerID = ConvertHelper.ConvertToInteger(base.sessionCustomerId);
                request.URL = serviceURL;

                webServiceHelper = new WebServiceHelper();
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.Customer != null)
                {
                    customerAddress1.InnerHtml = ConvertHelper.ConvertToString(response.Customer.Address, "");
                    customerCity.InnerHtml = ConvertHelper.ConvertToString(response.Customer.CityName, "");
                    customerState.InnerHtml = ConvertHelper.ConvertToString(response.Customer.StateName, "");
                    customerCountry.InnerHtml = ConvertHelper.ConvertToString(response.Customer.CountryName, "");
                    customerZipCode.InnerHtml = ConvertHelper.ConvertToString(response.Customer.ZipCode, "");
                    customerAccountRep.InnerHtml = ConvertHelper.ConvertToString(response.Customer.AccountRepName, "");
                    customerPrimaryEngineer.InnerHtml = ConvertHelper.ConvertToString(response.Customer.PrimaryEngineerName, "");
                    customerPhone.InnerHtml = ConvertHelper.ConvertToString(response.Customer.PhoneNumber, "");
                    customerFax.InnerHtml = ConvertHelper.ConvertToString(response.Customer.Fax, "");
                    customerWeb.InnerHtml = ConvertHelper.ConvertToString(response.Customer.EmailAddress, "");
                    customerEmail.HRef = ConvertHelper.ConvertToString("mailto:"+response.Customer.EmailAddress, "");
                    CustomerNameVal.InnerHtml = ConvertHelper.ConvertToString(response.Customer.CustomerName + " (" + response.Customer.CustomerCode + ")", "");
                    hidCustomerCode.Value = ConvertHelper.ConvertToString(response.Customer.CustomerCode, "");
                }

            }
            #endregion

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion

    protected void btnCustomerProvisioning_Click(object sender, EventArgs e)
    {
        tabname = hidTabname.Value != null ? hidTabname.Value : "";
        lblHeaderPro.Text = tabname;
        GetUserContaolPath(tabname);
        Response.Redirect("CustomerInfo.aspx?do=v&nav=" + tabname + "&#hTab-2", false);
    }

}