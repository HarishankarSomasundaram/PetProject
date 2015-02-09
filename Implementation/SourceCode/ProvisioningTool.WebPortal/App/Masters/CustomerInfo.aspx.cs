using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Web.Services;

public partial class MastersCustomerInfo : FormController
{
    #region [ Variable Declarations ]
    CompanyBLL companyBLL;
    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;

    #endregion [ Variable Declarations ]

    #region [ Page Load Events]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DetermineAction();
            if (Request.QueryString["isColorBox"] != null)
            {
                btnBack.Style.Add("display", "none");
                PageBody.Attributes.Add("class", "colorbox-parent");
                lnkAutoTask.Visible = false;
                lnkConnectWise.Visible = false;
            }
            if (CurrentAction != ActionType.MoreView)
            {
                Page.Validate("Req");
            }
        }
        
    }

    #endregion [ Page Load Events]

    #region [ Control Events ]

    protected void btnPopupSubmit_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "key", "DisplayDialog();", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        try
        {
            divMessage.Style.Add("display", "block");
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string url = string.Empty;
            request.Customer = new Customer();
            request.Customer.City = new GlobalMasterDetail();
            request.Customer.State = new GlobalMasterDetail();
            request.Customer.Country = new GlobalMasterDetail();
            request.Customer.AccountRep = new GlobalMasterDetail();
            request.Customer.PrimaryEngineer = new GlobalMasterDetail();
            request.Customer.Company = new Company();
            //Framing the UR
            url = string.Format(PostServiceURL + "{0}", "SAVECUSTOMER");
            request.URL = url;
            request.Customer.Company.CompanyID = ConvertHelper.ConvertToInteger(hidCompanyID.Value);
            request.Customer.CustomerCode = ConvertHelper.ConvertToString(txtCustomerCode.Text);
            request.Customer.CustomerName = ConvertHelper.ConvertToString(txtCustomerName.Text);
            request.Customer.Address = ConvertHelper.ConvertToString(txtAddress.Text);
            request.Customer.City.MasterDetailID = ConvertHelper.ConvertToInteger(ddlCity.SelectedItem.Value);
            request.Customer.State.MasterDetailID = ConvertHelper.ConvertToInteger(ddlState.SelectedItem.Value);
            request.Customer.Country.MasterDetailID = ConvertHelper.ConvertToInteger(ddlCountry.SelectedItem.Value);
            request.Customer.ZipCode = ConvertHelper.ConvertToString(txtZipCode.Text);
            request.Customer.PhoneNumber = ConvertHelper.ConvertToString(txtPhoneNumber.Text);
            request.Customer.AlternatePhoneNo = ConvertHelper.ConvertToString(txtAltPhoneNumber.Text);
            request.Customer.Fax = ConvertHelper.ConvertToString(txtFax.Text);
            request.Customer.EmailAddress = ConvertHelper.ConvertToString(txtEmailAddress.Text);
            request.Customer.AccountRep.MasterDetailID = ConvertHelper.ConvertToInteger(ddlAccountRep.SelectedItem.Value);
            request.Customer.PrimaryEngineer.MasterDetailID = ConvertHelper.ConvertToInteger(ddlPrimaryEngineer.SelectedItem.Value);
            request.Customer.Notes = ConvertHelper.ConvertToString(txtNotes.Text);
            request.Customer.StatusID = 1;
            request.Customer.CreatedBy = currentUser.ApplicationUserID;
            request.Customer.ModifiedBy = currentUser.ApplicationUserID;
            request.Customer.CreatedOn = DateTime.Now;
            request.Customer.ModifiedOn = DateTime.Now;
            request.Customer.MappingID = ConvertHelper.ConvertToString(hidAutoTaskID.Value, "0");

            if (hidIsAutoTask.Value == "1")
                request.Customer.IsAutoTask = true;
            else
                request.Customer.IsAutoTask = false;

            if (CheckCustomer(txtCustomerCode.Text, ConvertHelper.ConvertToBoolean(hidIsAutoTaskCheckRequried.Value)))
            {
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.Customer.CustomerID = ConvertHelper.ConvertToInteger(base.Id);
                }
                response = new PTResponse();
                response = webServiceHelper.PostRequest<PTResponse>(request);

                if (response != null && response.isSuccess == true)
                {
                    ShowMessage(response.Message, true);
                    if (response.isSuccess)
                    {
                        //POPUP Dialog Box
                        if (hidIsAutoTask.Value == "1")
                        {
                            if (CurrentAction == ActionType.Add)
                            {
                                string message = "Customer created successfully. Do you want to create Site and User from Auto Task?";
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            }
                            else
                            {
                                CrudCustomer.Visible = false;
                                //FormPage.Visible = false;
                                grdCustomer.Visible = true;
                                hidIsAutoTask.Value = string.Empty;
                            }
                        }
                        else
                        {
                            CrudCustomer.Visible = false;
                            //FormPage.Visible = false;
                            grdCustomer.Visible = true;
                            hidIsAutoTask.Value = string.Empty;
                        }
                    }
                }
            }
            else
            {
                string message = "Customer Already exists.";
                message = message + "<br/>" + CompareCustomer() + "<br/> Do you want to Update? ";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2('" + message + "');", true);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        grdCustomer.Visible = true;
        FormPage.Visible = false;
        CrudCustomer.Visible = false;
        hidIsAutoTask.Value = string.Empty;
    }

    #endregion [ Control Events ]

    #region [ Private Funciton ]
    //Populate Drop down values
    private void PopulateControls()
    {
        companyBLL = new CompanyBLL();
        response = new PTResponse();
        response.CompanyList = companyBLL.GetAllCompanies();
        if (response.CompanyList != null && response.CompanyList.Count > 0)
        {
            //PopulateCompanyDropDownList(ddlCompany, response.CompanyList, true);
            //txtCompany.InnerText = ConvertHelper.ConvertToString(response.CompanyList[0].CompanyName);
            hidCompanyID.Value = ConvertHelper.ConvertToString(response.CompanyList[0].CompanyID);
            //txtCompany.ReadOnly = true;
        }

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
    }

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
        if (response != null && response.CustomerList != null)
        {
            if (sFieldName == "id")
            {
                bool IsCountry, IsCity, IsState;
                string sCountry, sCity, sState;
                IsCountry = IsCity = IsState = false;

                hidAutoTaskID.Value = response.CustomerList[0].CustomerCode;
                hidIsAutoTask.Value = "1";
                txtCustomerName.Text = response.CustomerList[0].CustomerName; txtCustomerName.ReadOnly = true;
                txtCustomerCode.Text = response.CustomerList[0].CustomerCode;
                txtPhoneNumber.Text = response.CustomerList[0].PhoneNumber; txtPhoneNumber.ReadOnly = true;
                txtAltPhoneNumber.Text = response.CustomerList[0].AlternatePhoneNo; txtAltPhoneNumber.ReadOnly = true;
                txtFax.Text = response.CustomerList[0].Fax; txtFax.ReadOnly = true;
                txtAddress.Text = response.CustomerList[0].Address; txtAddress.ReadOnly = true;
                txtZipCode.Text = response.CustomerList[0].ZipCode; txtZipCode.ReadOnly = true;

                sCountry = ConvertHelper.ConvertToString(response.CustomerList[0].CountryName, "");
                sCity = ConvertHelper.ConvertToString(response.CustomerList[0].CityName, "");
                sState = ConvertHelper.ConvertToString(response.CustomerList[0].StateName, "");

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

    private void DetermineAction()
    {
        if (CurrentAction == ActionType.Add)
        {
            btnSubmit.Visible = true;
            PopulateControls();
            AddCustomerMaster();
            if (Request.QueryString["AutoTaskCustomerID"] != null)
                PopulateAutoTaskCustomer(ConvertHelper.ConvertToString(Request.QueryString["AutoTaskCustomerID"]), "id");
        }
        else if (CurrentAction == ActionType.Edit)
        {
            btnSubmit.Visible = true;
            PopulateControls();
            ModifyCustomerMaster();
            lnkAutoTask.Visible = false;
            lnkConnectWise.Visible = false;
        }
        else if (CurrentAction == ActionType.View)
        {
            btnSubmit.Visible = false;
            CrudCustomer.Visible = false;
            FormPage.Visible = false;
            grdCustomer.Visible = true;
            lnkAutoTask.Visible = false;
            lnkConnectWise.Visible = false;
        }
        else if (CurrentAction == ActionType.MoreView)
        {
            PopulateControls();
            ModifyCustomerMaster();
            DisableControls(cusomerDetail);
            cusomerDetail.Attributes.Add("class", cusomerDetail.Attributes["class"] + " viewPage");
            btnSubmit.Visible = false;
            FormPage.Visible = true;
            CrudCustomer.Visible = true;
            grdCustomer.Visible = false;
            btnBack.Visible = true;
            btnBack.Enabled = true;

            btnEdit.Visible = true;
            btnEdit.Enabled = true;

            lnkAutoTask.Visible = false;
            lnkConnectWise.Visible = false;
        }

    }

    private void ModifyCustomerMaster()
    {
        try
        {
            CrudCustomer.Visible = true;
            grdCustomer.Visible = false;
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETALLCUSTOMERBYCUSTOMERID";
                request.Customer = new Customer();
                request.Customer.CustomerID = ConvertHelper.ConvertToInteger(base.Id);
                request.URL = serviceURL;
            }
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.Customer != null)
            {
                //ddlCompany.SelectedValue = ConvertHelper.ConvertToString(response.Customer.CompanyID);
                //txtCompany.InnerText = ConvertHelper.ConvertToString(response.Customer.CompanyName);
                hidCompanyID.Value = ConvertHelper.ConvertToString(response.Customer.CompanyID);
                //txtCompany.ReadOnly = true;
                ddlCountry.SelectedValue = ConvertHelper.ConvertToString(response.Customer.CountryID);
                ddlState.SelectedValue = ConvertHelper.ConvertToString(response.Customer.StateID);
                ddlCity.SelectedValue = ConvertHelper.ConvertToString(response.Customer.CityID);
                ddlAccountRep.SelectedValue = ConvertHelper.ConvertToString(response.Customer.AccountRepID);
                ddlPrimaryEngineer.SelectedValue = ConvertHelper.ConvertToString(response.Customer.PrimaryEngineerID);
                txtCustomerCode.Text = ConvertHelper.ConvertToString(response.Customer.CustomerCode); ;
                txtCustomerName.Text = ConvertHelper.ConvertToString(response.Customer.CustomerName);
                txtAddress.Text = ConvertHelper.ConvertToString(response.Customer.Address);
                txtZipCode.Text = ConvertHelper.ConvertToString(response.Customer.ZipCode);
                txtPhoneNumber.Text = ConvertHelper.ConvertToString(response.Customer.PhoneNumber);
                txtAltPhoneNumber.Text = ConvertHelper.ConvertToString(response.Customer.AlternatePhoneNo);
                txtFax.Text = ConvertHelper.ConvertToString(response.Customer.Fax);
                txtEmailAddress.Text = ConvertHelper.ConvertToString(response.Customer.EmailAddress);
                txtNotes.Text = ConvertHelper.ConvertToString(response.Customer.Notes);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void AddCustomerMaster()
    {
        CrudCustomer.Visible = true;
        grdCustomer.Visible = false;
    }

    #endregion [ Private Funciton ]

    #region [ Public Function ]

    public bool CheckCustomer(string sCustomerCode, bool IsRequried)
    {
        try
        {
            if (!IsRequried)
            {
                CurrentAction = ActionType.Edit;
                base.Id = ConvertHelper.ConvertToString(Session["customerID"]);
                return true;
            }

            if (CurrentAction == ActionType.Add)
            {
                PTResponse checkresponse = new PTResponse();
                webServiceHelper = new WebServiceHelper();
                string serviceURL = string.Empty;
                List<Customer> customerList = new List<Customer>();

                serviceURL = GetServiceURL + "GETALLCUSTOMERS/Customers/0/id";
                string userResultString = string.Empty;

                webServiceHelper = new WebServiceHelper();
                userResultString = webServiceHelper.GetRequest(serviceURL);
                checkresponse = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
                if (checkresponse != null && checkresponse.CustomerList != null)
                {
                    Customer customer = new Customer();
                    customerList = checkresponse.CustomerList;
                    customer = customerList.Find(cust => cust.CustomerCode == sCustomerCode && cust.StatusID == 1);
                    if (customer != null)
                    {
                        customerList.Clear();
                        base.Id = ConvertHelper.ConvertToString(customer.CustomerID);
                        Session["customerID"] = customer.CustomerID;
                        Session["customer"] = customer;
                        customerList.Add(customer);
                    }
                    else
                    {
                        customerList.Clear();
                    }
                }
                if (customerList != null)
                {
                    if (customerList.Count > 0)
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
            divMessage.Style.Add("display", "block");
            ShowMessage(ex.Message, false);
            return false;
        }
    }

    public string InsertSites(string sAutoTaskID, string sCustomerCode)
    {
        try
        {
            string sMessage;
            sMessage = string.Empty;
            bool IsCountry, IsCity, IsState;
            string sCountry, sCity, sState;
            IsCountry = IsCity = IsState = false;
            int iSiteCount = 0;
            CheckCustomer(sCustomerCode, true);
            int iCustomer = ConvertHelper.ConvertToInteger(Session["customerID"], 0);
            List<Site> siteList = new List<ProvisioningTool.Entity.Site>();
            PopulateAutoTaskCustomer(sAutoTaskID, "ParentAccountID");
            if (Session["SiteList"] != null)
                siteList = (List<Site>)(Session["SiteList"]);
            if (siteList != null)
            {
                if (siteList.Count > 0)
                {
                    foreach (Site site in siteList)
                    {

                        PTRequest siterequest = new PTRequest();
                        PTResponse siteresponse = new PTResponse();
                        webServiceHelper = new WebServiceHelper();
                        string url = string.Empty;
                        siterequest.Site = new Site();
                        siterequest.Site.Customer = new Customer();
                        siterequest.Site.City = new GlobalMasterDetail();
                        siterequest.Site.State = new GlobalMasterDetail();
                        siterequest.Site.Country = new GlobalMasterDetail();
                        siterequest.Site.AccountRep = new GlobalMasterDetail();
                        siterequest.Site.PrimaryEngineer = new GlobalMasterDetail();
                        siterequest.Site.PrimaryContact = new ProvisioningTool.Entity.User();

                        //Framing the URL
                        url = string.Format(PostServiceURL + "{0}", "SAVESITE");
                        siterequest.URL = url;
                        siterequest.Site.Customer.CustomerID = ConvertHelper.ConvertToInteger(iCustomer);
                        siterequest.Site.SiteName = ConvertHelper.ConvertToString(site.SiteName, string.Empty);
                        siterequest.Site.SiteCode = ConvertHelper.ConvertToString(site.SiteCode, string.Empty);
                        siterequest.Site.Address1 = ConvertHelper.ConvertToString(site.Address1, string.Empty);
                        siterequest.Site.Address2 = ConvertHelper.ConvertToString(site.Address2, string.Empty);

                        sCountry = ConvertHelper.ConvertToString(site.CountryName, "");
                        sCity = ConvertHelper.ConvertToString(site.CityName, "");
                        sState = ConvertHelper.ConvertToString(site.StateName, "");

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

                        siterequest.Site.City.MasterDetailID = ConvertHelper.ConvertToInteger(ddlCity.SelectedItem.Value);
                        siterequest.Site.State.MasterDetailID = ConvertHelper.ConvertToInteger(ddlState.SelectedItem.Value);
                        siterequest.Site.Country.MasterDetailID = ConvertHelper.ConvertToInteger(ddlCountry.SelectedItem.Value);
                        siterequest.Site.ZipCode = ConvertHelper.ConvertToString(site.ZipCode);
                        siterequest.Site.PhoneNumber = ConvertHelper.ConvertToString(site.PhoneNumber);
                        siterequest.Site.Website = ConvertHelper.ConvertToString(site.Website);
                        siterequest.Site.PrimaryContact.UserID = 0;
                        siterequest.Site.PrimaryEngineer.MasterDetailID = 0;
                        siterequest.Site.AccountRep.MasterDetailID = 0;
                        siterequest.Site.StatusID = 1;
                        siterequest.Site.CreatedBy = currentUser.ApplicationUserID;
                        siterequest.Site.ModifiedBy = currentUser.ApplicationUserID;
                        siterequest.Site.IsAutoTask = true;
                        siterequest.Site.MappingID = ConvertHelper.ConvertToString(site.SiteCode, string.Empty);
                        siterequest.CurrentAction = ActionType.Add;
                        siteresponse = webServiceHelper.PostRequest<PTResponse>(siterequest);
                        if (siteresponse != null && siteresponse.isSuccess == true)
                        {
                            iSiteCount = iSiteCount + 1;
                        }

                    }
                }
                sMessage = iSiteCount.ToString() + " Sities created";
                Session["SiteList"] = null;
                GetAllSites(iCustomer);
            }
            else
                sMessage = "Site not available for this Customer";
            return sMessage;
        }
        catch (Exception ex)
        {
            divMessage.Style.Add("display", "block");
            ShowMessage(ex.Message, false);
            return "";
        }
    }

    public string InsertUsers()
    {
        try
        {
            string sMessage = string.Empty;
            int iUserCount = 0;
            if (Session["SiteList"] != null)
            {
                List<Site> siteFullList = (List<Site>)(Session["SiteList"]);
                if (siteFullList != null)
                {
                    if (siteFullList.Count > 0)
                    {
                        foreach (Site site in siteFullList)
                        {
                            GetAutoTaskUser(site.SiteCode, "AccountID");
                            if (Session["UserList"] != null)
                            {
                                List<User> UserFullList = (List<User>)(Session["UserList"]);
                                #region [ User List ]
                                if (UserFullList != null)
                                {
                                    if (UserFullList.Count > 0)
                                    {
                                        foreach (User user in UserFullList)
                                        {
                                            #region [ User Insert ]
                                            PTRequest userrequest = new PTRequest();
                                            PTResponse userresponse = new PTResponse();
                                            webServiceHelper = new WebServiceHelper();
                                            string serviceURL = string.Empty;
                                            string url = string.Empty;
                                            string serviceName = string.Empty;
                                            string statusMessage = string.Empty;
                                            serviceURL = PostServiceURL;

                                            userrequest.User = new User();
                                            userrequest.User.Title = new GlobalMasterDetail();
                                            userrequest.User.Department = new GlobalMasterDetail();
                                            userrequest.UserApps = new UserApp();
                                            userrequest.UserComputer = new UserComputer();
                                            userrequest.UserLaptop = new UserLaptops();
                                            userrequest.UserMobilePhone = new UserMobilePhone();
                                            userrequest.UserNetworkShare = new UserNetworkShare();
                                            userrequest.UserPrinter = new UserPrinter();
                                            userrequest.UserRemoteAccess = new UserRemoteAccess();
                                            userrequest.UserSecurityGroup = new UserSecurityGroup();
                                            userrequest.UserServer = new UserServer();
                                            userrequest.UserTablet = new UserTablet();

                                            ////Framing the URL
                                            //url = string.Format(serviceURL + "{0}", serviceName);

                                            userrequest.User.FirstName = ConvertHelper.ConvertToString(user.FirstName, string.Empty);
                                            userrequest.User.LastName = ConvertHelper.ConvertToString(user.LastName, string.Empty);
                                            userrequest.User.UserName = ConvertHelper.ConvertToString(user.UserName, string.Empty);
                                            userrequest.User.Password = ConvertHelper.ConvertToString(user.Password, string.Empty);
                                            userrequest.User.TitleID = 0;
                                            userrequest.User.DepartmentID = 0;

                                            userrequest.UserApps.SelectedAppIDs = string.Empty;
                                            userrequest.UserComputer.SelectedComputerIDs = string.Empty;
                                            userrequest.UserLaptop.SelLaptopItems = string.Empty;
                                            userrequest.UserMobilePhone.SelectedMobilePhoneIDs = string.Empty;
                                            userrequest.UserNetworkShare.SelNetworkShareItems = string.Empty;
                                            userrequest.UserPrinter.SelectedPrinterIDs = string.Empty;
                                            userrequest.UserRemoteAccess.SelRemoteAccessItems = string.Empty;
                                            userrequest.UserSecurityGroup.SelectedSecurityGroupIDs = string.Empty;
                                            userrequest.UserServer.SelServerItems = string.Empty;
                                            userrequest.UserTablet.SelectedTabletIDs = string.Empty;

                                            userrequest.User.Email = ConvertHelper.ConvertToString(user.Email, string.Empty);
                                            userrequest.User.Phone1 = ConvertHelper.ConvertToString(user.Phone1, string.Empty);
                                            userrequest.User.Phone2 = ConvertHelper.ConvertToString(user.Phone2, string.Empty);
                                            userrequest.User.Notes = ConvertHelper.ConvertToString(user.Notes, string.Empty);

                                            userrequest.User.CreatedBy = currentUser.ApplicationUserID;
                                            userrequest.User.ModifiedBy = currentUser.ApplicationUserID;
                                            userrequest.CurrentAction = ActionType.Add;
                                            userrequest.sessionSiteID = ConvertHelper.ConvertToInteger(site.SiteID);
                                            userrequest.User.StatusID = 1;
                                            userrequest.User.IsAutoTask = true;
                                            userrequest.User.MappingID = ConvertHelper.ConvertToString(site.SiteCode, string.Empty);
                                            serviceName = "SAVEUSER";

                                            //Framing the URL
                                            url = string.Format(serviceURL + "{0}", serviceName);
                                            userrequest.URL = url;
                                            userresponse = webServiceHelper.PostRequest<PTResponse>(userrequest);
                                            if (userresponse != null && userresponse.isSuccess == true)
                                            {
                                                iUserCount = iUserCount + 1;
                                            }
                                            #endregion [ User Insert ]
                                        }
                                    }
                                }
                                #endregion [ User List ]
                            }
                        }
                        sMessage = iUserCount.ToString() + " Users created";
                    }
                    sMessage = "No User found";
                }
                sMessage = "No User found";
            }
            return sMessage;
        }
        catch (Exception ex)
        {
            divMessage.Style.Add("display", "block");
            ShowMessage(ex.Message, false);
            return string.Empty;
        }
    }

    public void GetAllSites(int iCustomerID)
    {
        try
        {
            PTResponse getAllSiteresponse = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            List<Site> siteFullList = new List<Site>();
            List<Site> siteList = new List<Site>();

            serviceURL = GetServiceURL + "GETALLSITES/Sites/0/0";
            string userResultString = string.Empty;

            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            getAllSiteresponse = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (getAllSiteresponse != null && getAllSiteresponse.SiteList != null)
            {
                siteFullList = getAllSiteresponse.SiteList;
                if (siteFullList != null)
                {
                    if (siteFullList.Count > 0)
                    {
                        foreach (Site site in siteFullList)
                        {
                            if (site.CustomerID == iCustomerID)
                                siteList.Add(site);

                        }
                        Session["SiteList"] = siteList;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    public string CompareCustomer()
    {
        try
        {
            string sMessage = string.Empty;
            Customer cust = (Customer)Session["customer"];
            //Customer Code
            if (cust.CustomerCode != txtCustomerCode.Text)
                sMessage = sMessage + "Customer Code:" + cust.CustomerCode + "<br/>";

            //Customer Name
            if (cust.CustomerName != txtCustomerName.Text)
                sMessage = "Customer Name:" + cust.CustomerName + "<br/>";

            //Address
            if (cust.Address != txtAddress.Text)
                sMessage = sMessage + "Customer Address:" + cust.Address + "<br/>";

            //Country
            if (cust.CountryName != ddlCountry.SelectedItem.Text)
                sMessage = sMessage + "Customer Country:" + cust.CountryName + "<br/>";

            //State 
            if (cust.StateName != ddlState.SelectedItem.Text)
                sMessage = sMessage + "Customer State:" + cust.StateName + "<br/>";

            //City
            if (cust.CityName != ddlCity.SelectedItem.Text)
                sMessage = sMessage + "Customer City:" + cust.CityName + "<br/>";

            //Zipcode
            if (cust.ZipCode != txtZipCode.Text)
                sMessage = sMessage + "Customer Zipcode:" + cust.ZipCode + "<br/>";

            //Phone Number
            if (cust.PhoneNumber != txtPhoneNumber.Text)
                sMessage = sMessage + "Customer Phone Number:" + cust.PhoneNumber + "<br/>";

            //Alternate Number
            if (cust.AlternatePhoneNo != txtAltPhoneNumber.Text)
                sMessage = sMessage + "Customer Alternate Number:" + cust.AlternatePhoneNo + "<br/>";

            if (sMessage == string.Empty)
                sMessage = "No change found in customer details";

            return sMessage;
        }
        catch (Exception ex)
        {
            divMessage.Style.Add("display", "block");
            ShowMessage(ex.Message, false);
            return string.Empty;
        }
    }
    #endregion [ Public Function ]

    #region [ Web Method ]
    [WebMethod]
    public static string InsertSitesandUsers(string AutoTaskID, string CustomerCode)
    {
        try
        {

            string sMessage = "";
            MastersCustomerInfo customerInfo = new MastersCustomerInfo();
            sMessage = customerInfo.InsertSites(AutoTaskID, CustomerCode);
            sMessage = sMessage + " " + customerInfo.InsertUsers();
            customerInfo.ShowMessage(sMessage, true);

            return sMessage;

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion [ Web Method ]
}

