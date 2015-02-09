using Library;
using Newtonsoft.Json;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.Services;
using System.Globalization;

public partial class UserControlsInternetWeb : UCController
{
    #region [ Variable Declarations ]

    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;
    string baseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    string masterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    string serviceURL = string.Empty;
    string url = string.Empty;
    string serviceName = string.Empty;
    string serviceResponseString = string.Empty;
    #endregion [ Variable Declarations ]

    protected void Page_Load(object sender, EventArgs e)
    {
        divMessage.Attributes["style"] = "display:block";
        DetermineAction();
        if (!Page.IsPostBack && CurrentAction != ActionType.MoreView) { Page.Validate(); }
    }

    #region [Determine Action]
    private void DetermineAction()
    {
        try
        {
            if (CurrentAction == ActionType.Add)
            {
                ModifyInternetWeb();
                CrudInternetWeb.Visible = true;
                //divGrdInternetWebInfo.Visible = false;
                hideAllGrid(false);
                btnSubmitDomain.Visible = true;
                btnSubmitEmail.Visible = true;
                btnSubmitProvider.Visible = true;
                btnSubmitWebHost.Visible = true;

            }
            else if (CurrentAction == ActionType.Edit)
            {
                ModifyInternetWeb();
            }
            else if (CurrentAction == ActionType.MoreView)//To view the page without edit
            //else if (CurrentAction == ActionType.View)
            {

                ModifyInternetWeb();
                btnSubmitDomain.Visible = false;
                btnSubmitEmail.Visible = false;
                btnSubmitProvider.Visible = false;
                btnSubmitWebHost.Visible = false;

                DisableControls(divProviderDetail);
                DisableControls(divDomainDetail);
                DisableControls(divEmailDetail);
                DisableControls(divWebHostDetail);

                divProviderDetail.Attributes.Add("class", divProviderDetail.Attributes["class"] + " viewPage");
                divDomainDetail.Attributes.Add("class", divDomainDetail.Attributes["class"] + " viewPage");
                divEmailDetail.Attributes.Add("class", divEmailDetail.Attributes["class"] + " viewPage");
                divWebHostDetail.Attributes.Add("class", divWebHostDetail.Attributes["class"] + " viewPage");

            }
            else
            {
                CrudInternetWeb.Visible = false;
                //divGrdInternetWebInfo.Visible = true;
                hideAllGrid(true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get InternetWeb Info and Bind the Controls For Edit And View]
    private void ModifyInternetWeb()
    {
        try
        {
            string operatingGrid = string.Empty;
            CrudInternetWeb.Visible = true;
            CrudProvider.Visible = false;
            CrudDomain.Visible = false;
            CrudEmail.Visible = false;
            CrudWebHost.Visible = false;

            //divGrdInternetWebInfo.Visible = false;
            hideAllGrid(false);
            string[] selgrid = ConvertHelper.ConvertToString(base.nav, "").Split('-');
            if (selgrid.Count() > 0)
            {
                operatingGrid = ConvertHelper.ConvertToString(selgrid[1], "").ToUpper();
                //check to which the grid operation belong to
                if (operatingGrid == "PROVIDER")
                {
                    if (CurrentAction == ActionType.Edit || CurrentAction == ActionType.MoreView)
                        bindProvider();
                    btnBackProvider.Visible = true;
                    btnBackProvider.Enabled = true;
                    CrudProvider.Visible = true;
                }
                else if (operatingGrid == "DOMAIN")
                {
                    if (CurrentAction == ActionType.Edit || CurrentAction == ActionType.MoreView)
                        bindDomain();
                    btnBackDomain.Visible = true;
                    btnBackDomain.Enabled = true;
                    CrudDomain.Visible = true;
                }
                else if (operatingGrid == "WEBHOST")
                {
                    if (CurrentAction == ActionType.Edit || CurrentAction == ActionType.MoreView)
                        bindWebHost();
                    btnBackWebHost.Visible = true;
                    btnBackWebHost.Enabled = true;
                    CrudWebHost.Visible = true;
                }
                else if (operatingGrid == "EMAIL")
                {
                    if (CurrentAction == ActionType.Edit || CurrentAction == ActionType.MoreView)
                        bindEmail();
                    btnBackEmail.Visible = true;
                    btnBackEmail.Enabled = true;
                    CrudEmail.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            //ShowMessage(ex.Message, false);
        }
    }
    #endregion

    #region [Bind Provider]
    public void bindProvider()
    {
        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        serviceURL = string.Empty;
        url = string.Empty;
        serviceName = string.Empty;
        serviceResponseString = string.Empty;

        if (ConvertHelper.ConvertToString(base.Id) != null)
        {
            serviceURL = PostServiceURL + "GETINTERNETPROVIDERBYINTERNETPROVIDERID";
            request.InternetProvider = new InternetProvider();
            request.InternetProvider.ProviderID = ConvertHelper.ConvertToInteger(base.Id);
            request.URL = serviceURL;
        }

        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.InternetProvider != null)
        {

            txtProvider.Text = ConvertHelper.ConvertToString(response.InternetProvider.Provider);
            txtCircutID.Text = ConvertHelper.ConvertToString(response.InternetProvider.CircutID);
            txtAccountNumber.Text = ConvertHelper.ConvertToString(response.InternetProvider.AccountNumber);
            txtProviderType.Text = ConvertHelper.ConvertToString(response.InternetProvider.ProviderType);
            txtBrandWidth.Text = ConvertHelper.ConvertToString(response.InternetProvider.BrandWidth);
            txtNetworkID.Text = ConvertHelper.ConvertToString(response.InternetProvider.NetworkID);
            txtStaticIPAddress.Text = ConvertHelper.ConvertToString(response.InternetProvider.StaticIPAddress);
            txtSubnet.Text = ConvertHelper.ConvertToString(response.InternetProvider.Subnet);
            txtGateway.Text = ConvertHelper.ConvertToString(response.InternetProvider.Gateway);
            txtPhone.Text = ConvertHelper.ConvertToString(response.InternetProvider.Phone);


        }
        else
        {
            ShowMessage("Invalid record could not be found ", false);
        }
    }
    #endregion

    #region [Bind Email]
    public void bindEmail()
    {
        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        serviceURL = string.Empty;
        url = string.Empty;
        serviceName = string.Empty;
        serviceResponseString = string.Empty;

        if (ConvertHelper.ConvertToString(base.Id) != null)
        {
            serviceURL = PostServiceURL + "GETINTERNETEMAILHOSTBYINTERNETEMAILHOSTID";
            request.InternetEmailHost = new InternetEmailHost();
            request.InternetEmailHost.EmailHostID = ConvertHelper.ConvertToInteger(base.Id);
            request.URL = serviceURL;
        }

        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.InternetEmailHost != null)
        {

            txtEmailHosting.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.EmailHosting);
            txtEmailProvider.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.Provider);
            txtAccountLogin.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.AccountLogin);
            txtEmailPassword.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.EmailPassword);
            txtIPAddress.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.IPAddress);
            txtEmailAdminPanel.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.AdminPanel);
            txtEmailDNSManaged.SelectedIndex = ConvertHelper.ConvertToBoolean(response.InternetEmailHost.DNSManaged) ? 0 : 1;
            txtNameServers.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.NameServers);
            txtEmailPhone.Text = ConvertHelper.ConvertToString(response.InternetEmailHost.Phone);


        }
        else
        {
            ShowMessage("Invalid record could not be found ", false);
        }
    }
    #endregion

    #region [Bind WebHost]
    public void bindWebHost()
    {
        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        serviceURL = string.Empty;
        url = string.Empty;
        serviceName = string.Empty;
        serviceResponseString = string.Empty;

        if (ConvertHelper.ConvertToString(base.Id) != null)
        {
            serviceURL = PostServiceURL + "GETINTERNETWEBHOSTBYINTERNETWEBHOSTID";
            request.InternetWebHost = new InternetWebHost();
            request.InternetWebHost.WebHostID = ConvertHelper.ConvertToInteger(base.Id);
            request.URL = serviceURL;
        }

        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.InternetWebHost != null)
        {

            txtWebHost.Text = ConvertHelper.ConvertToString(response.InternetWebHost.WebHost);
            txtWebHostProvider.Text = ConvertHelper.ConvertToString(response.InternetWebHost.Provider);
            txtWebHostAccountID.Text = ConvertHelper.ConvertToString(response.InternetWebHost.AccountID);
            txtWebHostPassword.Text = ConvertHelper.ConvertToString(response.InternetWebHost.WebHostPassword);
            txtWebHostIPAddress.Text = ConvertHelper.ConvertToString(response.InternetWebHost.IPAddress);
            txtWebhostAdminPanel.Text = ConvertHelper.ConvertToString(response.InternetWebHost.AdminPanel);
            txtWebHostDNSManaged.SelectedIndex = ConvertHelper.ConvertToBoolean(response.InternetWebHost.DNSManaged) ? 0 : 1;
            txtWebHostNameServer.Text = ConvertHelper.ConvertToString(response.InternetWebHost.NameServer);
            txtWebHostPhone.Text = ConvertHelper.ConvertToString(response.InternetWebHost.Phone);


        }
        else
        {
            ShowMessage("Invalid record could not be found ", false);
        }
    }
    #endregion

    #region [Bind Domain]
    public void bindDomain()
    {
        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        serviceURL = string.Empty;
        url = string.Empty;
        serviceName = string.Empty;
        serviceResponseString = string.Empty;

        if (ConvertHelper.ConvertToString(base.Id) != null)
        {
            serviceURL = PostServiceURL + "GETINTERNETDOMAINBYINTERNETDOMAINID";
            request.InternetDomain = new InternetDomain();
            request.InternetDomain.DomainID = ConvertHelper.ConvertToInteger(base.Id);
            request.URL = serviceURL;
        }

        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.InternetDomain != null)
        {

            txtDomain.Text = ConvertHelper.ConvertToString(response.InternetDomain.Domain);
            txtRegistrar.Text = ConvertHelper.ConvertToString(response.InternetDomain.Registrar);
            txtAccountID.Text = ConvertHelper.ConvertToString(response.InternetDomain.AccountID);
            txtDomainPassword.Text = ConvertHelper.ConvertToString(response.InternetDomain.DomainPassword);
            //txtExpiration.Text = ConvertHelper.ConvertToString(response.InternetDomain.Expiration == null ? string.Empty : response.InternetDomain.Expiration.ToString("MM-dd-yyyy"));
            txtExpiration.Text = ConvertHelper.ConvertToDateTime(response.InternetDomain.Expiration).ToString("MM-dd-yyyy");
            txtAdminPanel.Text = ConvertHelper.ConvertToString(response.InternetDomain.AdminPanel);
            txtDNSManaged.SelectedIndex = (response.InternetDomain.DNSManaged) ? 0 : 1;
            txtServer.Text = ConvertHelper.ConvertToString(response.InternetDomain.Server);
            txtDomainPhone.Text = ConvertHelper.ConvertToString(response.InternetDomain.Phone);


        }
        else
        {
            ShowMessage("Invalid record could not be found ", false);
        }
    }
    #endregion

    #region [Add Email]
    protected void btnSubmitEmail_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceURL = PostServiceURL;

            request.InternetEmailHost = new InternetEmailHost();
            if (ConvertHelper.ConvertToString(txtEmailHosting.Text, "") != ""
                || ConvertHelper.ConvertToString(txtEmailProvider.Text, "") != ""
                || ConvertHelper.ConvertToString(txtAccountLogin.Text, "") != ""
                || ConvertHelper.ConvertToString(txtEmailPassword.Text, "") != ""
                || ConvertHelper.ConvertToString(txtIPAddress.Text, "") != ""
                || ConvertHelper.ConvertToString(txtEmailAdminPanel.Text, "") != ""
                || ConvertHelper.ConvertToBoolean(txtEmailDNSManaged.SelectedValue, false)
                || ConvertHelper.ConvertToString(txtNameServers.Text, "") != ""
                || ConvertHelper.ConvertToString(txtEmailPhone.Text, "") != "")
            {
                request.InternetEmailHost.EmailHosting = ConvertHelper.ConvertToString(txtEmailHosting.Text, "");
                request.InternetEmailHost.Provider = ConvertHelper.ConvertToString(txtEmailProvider.Text, "");
                request.InternetEmailHost.AccountLogin = ConvertHelper.ConvertToString(txtAccountLogin.Text, "");
                request.InternetEmailHost.EmailPassword = ConvertHelper.ConvertToString(txtEmailPassword.Text, "");
                request.InternetEmailHost.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text, "");
                request.InternetEmailHost.AdminPanel = ConvertHelper.ConvertToString(txtEmailAdminPanel.Text, "");
                request.InternetEmailHost.DNSManaged = ConvertHelper.ConvertToBoolean(txtEmailDNSManaged.SelectedValue, false);
                request.InternetEmailHost.NameServers = ConvertHelper.ConvertToString(txtNameServers.Text, "");
                request.InternetEmailHost.Phone = ConvertHelper.ConvertToString(txtEmailPhone.Text, "");

                request.InternetEmailHost.CreatedBy = currentUser.ApplicationUserID;
                request.InternetEmailHost.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.InternetEmailHost.EmailHostID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVEINTERNETEMAILHOST";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.InternetEmailHost.StatusID = 1;
                    serviceName = "SAVEINTERNETEMAILHOST";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                //            response = webServiceHelper.PostRequest(request);
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    //ShowMessage(response.Message, true);
                    ShowMessage(response.Message, true);
                    CrudEmail.Visible = false;
                    //divGrdInternetWebInfo.Visible = true;
                    hideAllGrid(true);
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudEmail.Visible = true;
                    //divGrdInternetWebInfo.Visible = false;
                    hideAllGrid(false);
                }
            }
            else
            {
                ShowMessage("Empty row cannot be inserted", false);
                CrudEmail.Visible = true;
                //divGrdInternetWebInfo.Visible = false;
                hideAllGrid(false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Add Web Host]

    protected void btnSubmitWebHost_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceURL = PostServiceURL;
            if (ConvertHelper.ConvertToString(txtWebHost.Text, "") != ""
                || ConvertHelper.ConvertToString(txtWebHostProvider.Text, "") != ""
                || ConvertHelper.ConvertToString(txtAccountLogin.Text, "") != ""
                || ConvertHelper.ConvertToString(txtWebHostAccountID.Text, "") != ""
                || ConvertHelper.ConvertToString(txtWebHostPassword.Text, "") != ""
                || ConvertHelper.ConvertToString(txtWebHostIPAddress.Text, "") != ""
                || ConvertHelper.ConvertToBoolean(txtWebHostDNSManaged.SelectedValue, false)
                || ConvertHelper.ConvertToString(txtWebhostAdminPanel.Text, "") != ""
                || ConvertHelper.ConvertToString(txtWebHostNameServer.Text, "") != ""
                || ConvertHelper.ConvertToString(txtWebHostPhone.Text, "") != "")
            {
                request.InternetWebHost = new InternetWebHost();
                request.InternetWebHost.WebHost = ConvertHelper.ConvertToString(txtWebHost.Text, "");
                request.InternetWebHost.Provider = ConvertHelper.ConvertToString(txtWebHostProvider.Text, "");
                request.InternetWebHost.AccountID = ConvertHelper.ConvertToString(txtWebHostAccountID.Text, "");
                request.InternetWebHost.WebHostPassword = ConvertHelper.ConvertToString(txtWebHostPassword.Text, "");
                request.InternetWebHost.IPAddress = ConvertHelper.ConvertToString(txtWebHostIPAddress.Text, "");
                request.InternetWebHost.AdminPanel = ConvertHelper.ConvertToString(txtWebhostAdminPanel.Text, "");
                request.InternetWebHost.DNSManaged = ConvertHelper.ConvertToBoolean(txtWebHostDNSManaged.SelectedValue, false);
                request.InternetWebHost.NameServer = ConvertHelper.ConvertToString(txtWebHostNameServer.Text, "");
                request.InternetWebHost.Phone = ConvertHelper.ConvertToString(txtWebHostPhone.Text, "");

                request.InternetWebHost.CreatedBy = currentUser.ApplicationUserID;
                request.InternetWebHost.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.InternetWebHost.WebHostID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVEINTERNETWEBHOST";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.InternetWebHost.StatusID = 1;
                    serviceName = "SAVEINTERNETWEBHOST";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                //            response = webServiceHelper.PostRequest(request);
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    //ShowMessage(response.Message, true);
                    ShowMessage(response.Message, true);
                    CrudWebHost.Visible = false;
                    //divGrdInternetWebInfo.Visible = true;
                    hideAllGrid(true);
                    //Response.Redirect("CustomerInfo.aspx?nav=InternetWebHosts");
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudWebHost.Visible = true;
                    //divGrdInternetWebInfo.Visible = false;
                    hideAllGrid(false);
                }
            }
            else
            {
                ShowMessage("Empty row cannot be inserted", false);
                CrudWebHost.Visible = true;
                //divGrdInternetWebInfo.Visible = false;
                hideAllGrid(false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Add Domain]

    protected void btnSubmitDomain_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceURL = PostServiceURL;

            request.InternetDomain = new InternetDomain();
            if (ConvertHelper.ConvertToString(txtDomain.Text, "") != ""
              || ConvertHelper.ConvertToString(txtRegistrar.Text, "") != ""
              || ConvertHelper.ConvertToString(txtAccountID.Text, "") != ""
              || ConvertHelper.ConvertToString(txtDomainPassword.Text, "") != ""
              || ConvertHelper.ConvertToString(txtExpiration.Text, "") != ""
              || ConvertHelper.ConvertToString(txtAdminPanel.Text, "") != ""
              || ConvertHelper.ConvertToBoolean(txtDNSManaged.SelectedValue, false)
              || ConvertHelper.ConvertToString(txtServer.Text, "") != ""
              || ConvertHelper.ConvertToString(txtDomainPhone.Text, "") != "")
            {
                request.InternetDomain.Domain = ConvertHelper.ConvertToString(txtDomain.Text, "");
                request.InternetDomain.Registrar = ConvertHelper.ConvertToString(txtRegistrar.Text, "");
                request.InternetDomain.AccountID = ConvertHelper.ConvertToString(txtAccountID.Text, "");
                request.InternetDomain.DomainPassword = ConvertHelper.ConvertToString(txtDomainPassword.Text, "");

                //DateTime expirationDateTime = ConvertHelper.ConvertToDateTime(txtExpiration.Text, DateTime.MinValue);
                //request.InternetDomain.Expiration = expirationDateTime.Add(new TimeSpan(5, 30, 0));

                request.InternetDomain.Expiration = ConvertHelper.ConvertToString(txtExpiration.Text, "");

                request.InternetDomain.AdminPanel = ConvertHelper.ConvertToString(txtAdminPanel.Text, "");
                request.InternetDomain.DNSManaged = ConvertHelper.ConvertToBoolean(txtDNSManaged.SelectedValue, false);
                request.InternetDomain.Server = ConvertHelper.ConvertToString(txtServer.Text, "");
                request.InternetDomain.Phone = ConvertHelper.ConvertToString(txtDomainPhone.Text, "");


                request.InternetDomain.CreatedBy = currentUser.ApplicationUserID;
                request.InternetDomain.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.InternetDomain.DomainID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVEINTERNETDOMAIN";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.InternetDomain.StatusID = 1;
                    serviceName = "SAVEINTERNETDOMAIN";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                //            response = webServiceHelper.PostRequest(request);
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    //ShowMessage(response.Message, true);
                    ShowMessage(response.Message, true);
                    CrudDomain.Visible = false;
                    //divGrdInternetWebInfo.Visible = true;
                    hideAllGrid(true);
                    //Response.Redirect("CustomerInfo.aspx?nav=InternetDomains");
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudDomain.Visible = true;
                    //divGrdInternetWebInfo.Visible = false;
                    hideAllGrid(false);
                }
            }
            else
            {
                ShowMessage("Empty row cannot be inserted", false);
                CrudDomain.Visible = true;
                //divGrdInternetWebInfo.Visible = false;
                hideAllGrid(false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Add Provider]

    protected void btnSubmitProvider_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceURL = PostServiceURL;

            request.InternetProvider = new InternetProvider();
            if (ConvertHelper.ConvertToString(txtProvider.Text, "") != ""
            || ConvertHelper.ConvertToString(txtCircutID.Text, "") != ""
            || ConvertHelper.ConvertToString(txtAccountNumber.Text, "") != ""
            || ConvertHelper.ConvertToString(txtProviderType.Text, "") != ""
            || ConvertHelper.ConvertToString(txtBrandWidth.Text, "") != ""
            || ConvertHelper.ConvertToString(txtNetworkID.Text, "") != ""
            || ConvertHelper.ConvertToString(txtSubnet.Text, "") != ""
            || ConvertHelper.ConvertToString(txtGateway.Text, "") != ""
            || ConvertHelper.ConvertToString(txtPhone.Text, "") != "")
            {
                request.InternetProvider.Provider = ConvertHelper.ConvertToString(txtProvider.Text, "");
                request.InternetProvider.CircutID = ConvertHelper.ConvertToString(txtCircutID.Text, "");
                request.InternetProvider.AccountNumber = ConvertHelper.ConvertToString(txtAccountNumber.Text, "");
                request.InternetProvider.ProviderType = ConvertHelper.ConvertToString(txtProviderType.Text, "");
                request.InternetProvider.BrandWidth = ConvertHelper.ConvertToString(txtBrandWidth.Text, "");
                request.InternetProvider.NetworkID = ConvertHelper.ConvertToString(txtNetworkID.Text, "");
                request.InternetProvider.StaticIPAddress = ConvertHelper.ConvertToString(txtStaticIPAddress.Text, "");
                request.InternetProvider.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text, "");
                request.InternetProvider.Gateway = ConvertHelper.ConvertToString(txtGateway.Text, "");
                request.InternetProvider.Phone = ConvertHelper.ConvertToString(txtPhone.Text, "");

                request.InternetProvider.CreatedBy = currentUser.ApplicationUserID;
                request.InternetProvider.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.InternetProvider.ProviderID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVEINTERNETPROVIDER";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.InternetProvider.StatusID = 1;
                    serviceName = "SAVEINTERNETPROVIDER";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                //            response = webServiceHelper.PostRequest(request);
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    //ShowMessage(response.Message, true);
                    ShowMessage(response.Message, true);
                    CrudProvider.Visible = false;
                    //divGrdInternetWebInfo.Visible = true;
                    hideAllGrid(true);
                    //Response.Redirect("CustomerInfo.aspx?nav=InternetProviders");
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudProvider.Visible = true;
                    //divGrdInternetWebInfo.Visible = false;
                    hideAllGrid(false);
                }
            }
            else
            {
                ShowMessage("Empty row cannot be inserted", false);
                CrudProvider.Visible = true;
                //divGrdInternetWebInfo.Visible = false;
                hideAllGrid(false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Back button click]

    protected void btnBackProvider_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudProvider.Visible = false;
            //divGrdInternetWebInfo.Visible = true;
            hideAllGrid(true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnBackDomain_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudDomain.Visible = false;
            //divGrdInternetWebInfo.Visible = true;
            hideAllGrid(true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnBackEmail_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudEmail.Visible = false;
            //divGrdInternetWebInfo.Visible = true;
            hideAllGrid(true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnBackWebHost_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudWebHost.Visible = false;
            //divGrdInternetWebInfo.Visible = true;
            hideAllGrid(true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion

    public void hideAllGrid(bool flag)
    {
        divGrdInternetProviderInfo.Visible = flag;
        divGrdInternetDomainInfo.Visible = flag;
        divGrdInternetWebHostInfo.Visible = flag;
        divGrdInternetEmailHostInfo.Visible = flag;

    }
}