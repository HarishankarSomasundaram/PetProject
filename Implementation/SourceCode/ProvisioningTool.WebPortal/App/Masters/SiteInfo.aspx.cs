using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class MastersSiteInfo : FormController
{
    #region [ Variable Declarations ]
    PTResponse response;
    PTRequest request;
    UserBLL userBLL;
    WebServiceHelper webServiceHelper;
    string baseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    string masterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    #endregion [ Variable Declarations ]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DetermineAction();

            if (CurrentAction != ActionType.MoreView)
                Page.Validate();
        }
        DropDownListColorBoxProcess(ddlCustomer, ConvertHelper.ConvertToInteger(base.sessionCustomerId), false, ConvertHelper.ConvertToString(Request.QueryString["isColorBox"], "no"));

        HttpCookie isIframeCookie = new HttpCookie("isIframe");
        isIframeCookie.Expires = DateTime.Now.AddHours(1);
        isIframeCookie = Request.Cookies["isIframe"];

        if (Request.QueryString["isColorBox"] != null)
        {
            PageBody.Attributes.Add("class", "colorbox-parent");
            btnBack.Style.Add("display", "none");
        }

    }

    private void DetermineAction()
    {
        //InetilizeIframe(Sitediv, grdSite);

        if (CurrentAction == ActionType.Add)
        {
            btnSubmit.Visible = true;
            PopulateControls();
            AddSiteMaster();
            if (Request.QueryString["AutoTaskCustomerID"] != null)
                PopulateAutoTaskCustomer(ConvertHelper.ConvertToString(Request.QueryString["AutoTaskCustomerID"]), "id");
        }
        else if (CurrentAction == ActionType.Edit)
        {
            btnSubmit.Visible = true;
            PopulateControls();
            ModifySiteMaster();
            lnkAutoTask.Visible = false;

        }
        else if (CurrentAction == ActionType.MoreView)
        {
            btnSubmit.Visible = false;
            PopulateControls();
            ModifySiteMaster();
            DisableControls(Sitediv);
            Sitediv.Attributes.Add("class", Sitediv.Attributes["class"] + " viewPage");
            lnkAutoTask.Visible = false;
        }
        else
        {
            btnSubmit.Visible = false;
            CrudSite.Visible = false;
            grdSite.Visible = true;
            lnkAutoTask.Visible = false;
        }



    }

    public void InetilizeIframe(HtmlGenericControl divCrud, HtmlGenericControl divGrid)
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
            CurrentAction = ActionType.Add;
            form1.Attributes.CssStyle.Add("backcolor", "#fff");
            if (isIframeCookie != null)
                isIframeCookie.Value = "1";
        }
        //check the iframe operation for Edit
        else if (isIframe != "" && isIframeOperation != "" && isIframe == "1" && isIframeOperation == "e")
        {
            form1.Attributes.CssStyle.Add("backcolor", "#fff");
            if (isIframeCookieOperation != null)
            {
                isIframeCookieOperation.Value = "e";
            }
            if (isIframeCookie != null)
            {
                isIframeCookie.Value = "1";
                if (divCrud != null && divGrid != null)
                {
                    divCrud.Visible = false;
                    divGrid.Visible = true;
                }
            }
        }
        else
        {
            if (isIframeCookie != null)
                isIframeCookie.Value = "";
            if (isIframeCookieOperation != null)
                isIframeCookieOperation.Value = "";

        }
        #endregion [Inetillize the cookie for iframe operations]
    }
    private void ModifySiteMaster()
    {
        try
        {
            CrudSite.Visible = true;
            grdSite.Visible = false;
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETSITEBYSITEID";
                request.Site = new Site();
                request.Site.SiteID = ConvertHelper.ConvertToInteger(base.Id);
                request.URL = serviceURL;
            }

            webServiceHelper = new WebServiceHelper();
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.Site != null)
            {
                ddlCountry.SelectedValue = ConvertHelper.ConvertToString(response.Site.CountryID);
                ddlCustomer.SelectedValue = ConvertHelper.ConvertToString(response.Site.CustomerID);
                ddlState.SelectedValue = ConvertHelper.ConvertToString(response.Site.StateID);
                ddlCity.SelectedValue = ConvertHelper.ConvertToString(response.Site.CityID);
                ddlAccountRep.SelectedValue = ConvertHelper.ConvertToString(response.Site.AccountRepID);
                ddlPrimaryEngineer.SelectedValue = ConvertHelper.ConvertToString(response.Site.PrimaryEngineerID);
                ddlPrimaryContact.SelectedValue = ConvertHelper.ConvertToString(response.Site.PrimaryContactID);
                txtZipCode.Text = ConvertHelper.ConvertToString(response.Site.ZipCode);
                txtPhoneNumber.Text = ConvertHelper.ConvertToString(response.Site.PhoneNumber);
                txtSiteName.Text = ConvertHelper.ConvertToString(response.Site.SiteName);
                txtSiteCode.Text = ConvertHelper.ConvertToString(response.Site.SiteCode);
                txtAddress1.Text = ConvertHelper.ConvertToString(response.Site.Address1);
                txtAddress2.Text = ConvertHelper.ConvertToString(response.Site.Address2);
                txtwebsite.Text = ConvertHelper.ConvertToString(response.Site.Website);
                txtPrimaryContactTitle.Text = ConvertHelper.ConvertToString(response.Site.PrimaryContactTitleName);
                txtPrimaryContactPhone.Text = ConvertHelper.ConvertToString(response.Site.PrimaryContact.Phone1);
                txtEmail.Text = ConvertHelper.ConvertToString(response.Site.PrimaryContact.Email);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void AddSiteMaster()
    {
        CrudSite.Visible = true;
        grdSite.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string url = string.Empty;
            request.Site = new Site();
            request.Site.Customer = new Customer();
            request.Site.City = new GlobalMasterDetail();
            request.Site.State = new GlobalMasterDetail();
            request.Site.Country = new GlobalMasterDetail();
            request.Site.AccountRep = new GlobalMasterDetail();
            request.Site.PrimaryEngineer = new GlobalMasterDetail();
            request.Site.PrimaryContact = new ProvisioningTool.Entity.User();

            //Framing the URL
            url = string.Format(PostServiceURL + "{0}", "SAVESITE");
            request.URL = url;
            request.Site.Customer.CustomerID = ConvertHelper.ConvertToInteger(ddlCustomer.SelectedValue);
            request.Site.SiteName = ConvertHelper.ConvertToString(txtSiteName.Text);
            request.Site.SiteCode = ConvertHelper.ConvertToString(txtSiteCode.Text);
            request.Site.Address1 = ConvertHelper.ConvertToString(txtAddress1.Text);
            request.Site.Address2 = ConvertHelper.ConvertToString(txtAddress2.Text);
            request.Site.City.MasterDetailID = ConvertHelper.ConvertToInteger(ddlCity.SelectedItem.Value);
            request.Site.State.MasterDetailID = ConvertHelper.ConvertToInteger(ddlState.SelectedItem.Value);
            request.Site.Country.MasterDetailID = ConvertHelper.ConvertToInteger(ddlCountry.SelectedItem.Value);
            request.Site.ZipCode = ConvertHelper.ConvertToString(txtZipCode.Text);
            request.Site.PhoneNumber = ConvertHelper.ConvertToString(txtPhoneNumber.Text);
            request.Site.Website = ConvertHelper.ConvertToString(txtwebsite.Text);
            request.Site.PrimaryContact.UserID = ConvertHelper.ConvertToInteger(ddlPrimaryContact.SelectedItem.Value);
            request.Site.PrimaryEngineer.MasterDetailID = ConvertHelper.ConvertToInteger(ddlPrimaryEngineer.SelectedItem.Value);
            request.Site.AccountRep.MasterDetailID = ConvertHelper.ConvertToInteger(ddlAccountRep.SelectedItem.Value);
            request.Site.StatusID = 1;
            request.Site.CreatedBy = currentUser.ApplicationUserID;
            request.Site.ModifiedBy = currentUser.ApplicationUserID;
            if (CheckSite(txtSiteCode.Text, ConvertHelper.ConvertToBoolean(hidIsAutoTaskCheckRequried.Value)))
            {
                request.Site.MappingID = ConvertHelper.ConvertToString(hidAutoTaskID.Value);
                if (hidIsAutoTask.Value == "1")
                    request.Site.IsAutoTask = true;
                else
                    request.Site.IsAutoTask = false;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.Site.SiteID = ConvertHelper.ConvertToInteger(base.Id);
                }
                response = new PTResponse();
                //response = webServiceHelper.PostRequest(request);
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    ShowMessage(response.Message, true);
                    if (response.isSuccess)
                    {
                        CrudSite.Visible = false;
                        grdSite.Visible = true;
                    }

                }
                else
                {
                    ShowMessage(response.Message, false);
                }
            }
            else
            {
                string message = "Site detail already exists.";
                message = message + "<br/> Do you want to Update? ";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    private void PopulateControls()
    {

        #region [GE ALL USERS AND POPULATE]
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;
        string url = string.Empty;
        string serviceName = string.Empty;
        string serviceResponseString = string.Empty;
        request = new PTRequest();
        serviceURL = PostServiceURL + "POPULATEALLUSERSWITHOUTSITEID";
        request.URL = serviceURL;
        request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
        response = webServiceHelper.PostRequest<PTResponse>(request);

        if (response != null && response.UserList != null && response.UserList.Count > 0)
        {
            PopulateUserDropDownList(ddlPrimaryContact, response.UserList, true);
        }
        #endregion

        request = new PTRequest();
        request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

        request.GlobalMaster = new GlobalMaster();
        request.GlobalMaster.MasterName = SiteStateName;
        PopulateGlobalMasterDropdown(request, ddlState);

        request.GlobalMaster = new GlobalMaster();
        request.GlobalMaster.MasterName = SiteCityName;
        PopulateGlobalMasterDropdown(request, ddlCity);

        request.GlobalMaster = new GlobalMaster();
        request.GlobalMaster.MasterName = SiteCountryName;
        PopulateGlobalMasterDropdown(request, ddlCountry);

        request.GlobalMaster = new GlobalMaster();
        request.GlobalMaster.MasterName = SiteAccRep;
        PopulateGlobalMasterDropdown(request, ddlAccountRep);

        request.GlobalMaster = new GlobalMaster();
        request.GlobalMaster.MasterName = SitePriEng;
        PopulateGlobalMasterDropdown(request, ddlPrimaryEngineer);

        #region [GE ALL USERS AND POPULATE]
        url = GetServiceURL + "GETALLCUSTOMERS/Mastername/0/searchtext";
        string userResultString = string.Empty;

        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        userResultString = webServiceHelper.GetRequest(url);
        response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);

        if (response != null && response.CustomerList != null && response.CustomerList.Count > 0)
        {
            PopulateCustomerDropDownList(ddlCustomer, response.CustomerList, true);
        }
        #endregion
    }

    //private void PopulateMasterDropdown(string masterName, DropDownList control)
    //{
    //    string url = string.Empty;
    //    string serviceName = string.Empty;
    //    string serviceResponseString = string.Empty;
    //    response = new PTResponse();

    //    url = string.Format(baseServiceURL + masterServiceName + "GlobalMasterDetail/{0}", masterName);
    //    webServiceHelper = new WebServiceHelper();
    //    serviceResponseString = webServiceHelper.GetRequest(url);

    //    if (ConvertHelper.ConvertToString(serviceResponseString) != null)
    //    {
    //        response.GlobalMasterDetailList = webServiceHelper.ConvertToObjectList<GlobalMasterDetail>(serviceResponseString);
    //    }
    //    if (response != null && response.GlobalMasterDetailList != null && response.GlobalMasterDetailList.Count > 0)
    //        PopulateMasterDetailDropDownList(control, response.GlobalMasterDetailList, true);
    //}
    protected void ddlPrimaryContact_SelectedIndexChanged(object sender, EventArgs e)
    {

        User user = new User();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;
        string url = string.Empty;
        string serviceName = string.Empty;
        string serviceResponseString = string.Empty;
        request = new PTRequest();
        serviceURL = PostServiceURL + "GETUSERANDUSERDETAILSBYUSERID";
        request.URL = serviceURL;
        request.User = new User();
        request.User.UserID = ConvertHelper.ConvertToInteger(ddlPrimaryContact.SelectedItem.Value);
        request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.User != null)
        {

            if (user != null)
            {
                txtPrimaryContactPhone.Text = ConvertHelper.ConvertToString(response.User.Phone1, "");
                txtPrimaryContactTitle.Text = ConvertHelper.ConvertToString(response.User.TitleName, "");
                txtEmail.Text = ConvertHelper.ConvertToString(response.User.Email, "");

            }
            else
            {
                txtPrimaryContactTitle.Text = "";
                txtPrimaryContactPhone.Text = "";
                txtEmail.Text = "";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        CrudSite.Visible = false;
        grdSite.Visible = true;

    }

    #region [ Private Function]
    #region [ Populate Auto Task Customer ]
    private void PopulateAutoTaskCustomer(string sCustomerID, string sFieldName)
    {

        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;

        serviceURL = GetServiceURL + "GETALLCUSTOMERSAUTOTASK/" + sCustomerID + "/0/" + sFieldName;
        string userResultString = string.Empty;

        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        userResultString = webServiceHelper.GetRequest(serviceURL);
        response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
        if (response != null && response.SiteList != null)
        {
            if (sFieldName == "id")
            {
                bool IsCountry, IsCity, IsState;
                string sCountry, sCity, sState;
                IsCountry = IsCity = IsState = false;

                hidAutoTaskID.Value = response.SiteList[0].SiteCode;
                hidIsAutoTask.Value = "1";

                FilTextBox(txtSiteName, ConvertHelper.ConvertToString(response.SiteList[0].SiteName, ""));
                txtSiteCode.Text = response.SiteList[0].SiteCode;
                FilTextBox(txtPhoneNumber, ConvertHelper.ConvertToString(response.SiteList[0].PhoneNumber, ""));
                FilTextBox(txtAddress1, ConvertHelper.ConvertToString(response.SiteList[0].Address1, ""));
                FilTextBox(txtAddress2, ConvertHelper.ConvertToString(response.SiteList[0].Address2, ""));
                FilTextBox(txtwebsite, ConvertHelper.ConvertToString(response.SiteList[0].Website, ""));
                FilTextBox(txtZipCode, ConvertHelper.ConvertToString(response.SiteList[0].ZipCode, ""));

                sCountry = ConvertHelper.ConvertToString(response.SiteList[0].CountryName, "");
                sCity = ConvertHelper.ConvertToString(response.SiteList[0].CityName, "");
                sState = ConvertHelper.ConvertToString(response.SiteList[0].StateName, "");

                if (sCountry != "")
                    IsCountry = GlobalMasterDetailsAdd(SiteCountryName, sCountry);
                if (sCity != "")
                    IsCity = GlobalMasterDetailsAdd(SiteCityName, sCity);
                if (sState != "")
                    IsState = GlobalMasterDetailsAdd(SiteStateName, sState);

                if (IsCountry || IsCity || IsState)
                    PopulateControls();

                SetDropdown(ddlCountry, sCountry);
                SetDropdown(ddlState, sState);
                SetDropdown(ddlCity, sCity);
            }
            else if (response.SiteList != null)
            {
                Session["SiteList"] = response.SiteList;
            }
        }
    }

    private void GetAutoTaskUser(string sAccountID, string sFieldName)
    {
        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;

        serviceURL = GetServiceURL + "GETALLUSERAUTOTASK/" + sAccountID + "/0/" + sFieldName;
        string userResultString = string.Empty;

        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        userResultString = webServiceHelper.GetRequest(serviceURL);
        response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
        if (response != null && response.UserList != null)
        {
            Session["UserList"] = response.UserList;
        }
    }
    #endregion [ Populate Auto Task Customer ]

    private bool CheckSite(string sSiteCode, bool IsRequried)
    {
        try
        {
            if (!IsRequried)
            {
                CurrentAction = ActionType.Edit;
                base.Id = ConvertHelper.ConvertToString(Session["siteID"]);
                return true;
            }

            if (CurrentAction == ActionType.Add)
            {
                PTResponse checkresponse = new PTResponse();
                webServiceHelper = new WebServiceHelper();
                string serviceURL = string.Empty;
                List<Site> siteList = new List<Site>();

                serviceURL = GetServiceURL + "GETALLSITES/Sites/0/0";
                string userResultString = string.Empty;

                webServiceHelper = new WebServiceHelper();
                userResultString = webServiceHelper.GetRequest(serviceURL);
                checkresponse = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
                if (checkresponse != null && checkresponse.SiteList != null)
                {
                    Site site = new Site();
                    siteList = checkresponse.SiteList;
                    site = siteList.Find(ste => ste.SiteCode == sSiteCode && ste.StatusID == 1);
                    if (site != null)
                    {
                        siteList.Clear();
                        base.Id = ConvertHelper.ConvertToString(site.SiteID);
                        Session["siteID"] = site.SiteID;
                        Session["site"] = site;
                        siteList.Add(site);
                    }
                    else
                    {
                        siteList.Clear();
                    }
                }
                if (siteList != null)
                {
                    if (siteList.Count > 0)
                        return false;
                    else
                        return true;
                }
                else
                    return true;
            }
            else
                return true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }

    private void FilTextBox(TextBox txt, string sValue)
    {
        if (sValue != "")
        {
            txt.Text = sValue;
            txt.ReadOnly = true;
        }
    }

    #endregion [ Private Function ]

}