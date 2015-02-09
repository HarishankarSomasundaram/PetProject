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

public partial class UserControlsUserInfo : UCController
{
    #region [ Variable Declarations ]

    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;
    string baseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    string masterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    #endregion [ Variable Declarations ]

    protected void Page_Load(object sender, EventArgs e)
    {
        divMessage.Attributes["style"] = "display:block";
        DetermineAction();
        if (!Page.IsPostBack && CurrentAction != ActionType.MoreView) { Page.Validate(); }
        if (Request.QueryString["isColorBox"] != null)
        {
            btnBack.Style.Add("display", "none");
            //searchgrid.Style.Add("display", "none");
            //first.Style.Add("display", "none");
            //siteandprovisioning.Style.Add("display", "none");
            //hideContent.Style.Add("display", "none");
            //innerTab.Style["margin-left"] = "0px !important";
            //PageBody.Attributes.Add("class", "colorbox-parent");
        }

    }

    #region [Determine Action]
    private void DetermineAction()
    {
        try
        {
            InitializeIframe(CrudUser, divGrdUserInfo);

            if (CurrentAction == ActionType.Add)
            {
                PopulateControls();
                CrudUser.Visible = true;
                divGrdUserInfo.Visible = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                if (Request.QueryString["AutoTaskCustomerID"] != null)
                    PopulateAutoTaskUser(ConvertHelper.ConvertToString(Request.QueryString["AutoTaskCustomerID"]), "id");
            }
            else if (CurrentAction == ActionType.Edit)
            {
                PopulateControls();
                ModifyUser(base.Id);
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                lnkAutoTask.Visible = false;
            }
            else if (CurrentAction == ActionType.MoreView)//To view the page without edit
            //else if (CurrentAction == ActionType.View)
            {
                PopulateControls();
                ModifyUser(base.Id);
                btnSubmit.Visible = false;
                DisableControls(divUserDetail);
                divUserDetail.Attributes.Add("class", divUserDetail.Attributes["class"] + " viewPage");
                txtNotes.Attributes.Remove("class");//this will make the class non editable 
                btnBack.Visible = true;
                btnBack.Enabled = true;
                lnkAutoTask.Visible = false;
            }
            else
            {
                CrudUser.Visible = false;
                divGrdUserInfo.Visible = true;
                lnkAutoTask.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get Users and Bind the Controls For Edit And View]
    private void ModifyUser(string UserId)
    {
        try
        {
            CrudUser.Visible = true;
            divGrdUserInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(UserId) != null)
            {
                serviceURL = PostServiceURL + "GETUSERANDUSERDETAILSBYUSERID";
                request.User = new User();
                request.User.UserID = ConvertHelper.ConvertToInteger(UserId);
                request.URL = serviceURL;
                hidEditID.Value = ConvertHelper.ConvertToString(UserId);
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.User != null)
            {
                txtFirstName.Text = ConvertHelper.ConvertToString(response.User.FirstName);
                txtFirstName.ToolTip = ConvertHelper.ConvertToString(response.User.FirstName);

                txtLastName.Text = ConvertHelper.ConvertToString(response.User.LastName);
                txtLastName.ToolTip = ConvertHelper.ConvertToString(response.User.LastName);

                txtUserName.Text = ConvertHelper.ConvertToString(response.User.UserName);
                txtUserName.ToolTip = ConvertHelper.ConvertToString(response.User.UserName);

                txtPassword.Text = ConvertHelper.ConvertToString(response.User.Password);
                txtPassword.ToolTip = ConvertHelper.ConvertToString(response.User.Password);

                ddlTitle.SelectedValue = ConvertHelper.ConvertToString(response.User.TitleID);
                ddlTitle.ToolTip = ConvertHelper.ConvertToString(response.User.TitleName);

                ddlDepartment.SelectedValue = ConvertHelper.ConvertToString(response.User.DepartmentID);
                ddlDepartment.ToolTip = ConvertHelper.ConvertToString(response.User.DepartmentName);

                txtEmail.Text = ConvertHelper.ConvertToString(response.User.Email);
                txtEmail.ToolTip = ConvertHelper.ConvertToString(response.User.Email);

                txtPhone1.Text = ConvertHelper.ConvertToString(response.User.Phone1);
                txtPhone2.Text = ConvertHelper.ConvertToString(response.User.Phone2);
                txtNotes.Text = response.User.Notes != null ? ConvertHelper.ConvertToString(response.User.Notes.Replace("|", ";")) : "";
                txtNotes.ToolTip = response.User.Notes != null ? ConvertHelper.ConvertToString(response.User.Notes.Replace("|", ";")) : "";
                #region [Binding the Multiple seletion Dropdown aby setting the selected property]


               // MultipleItemsSelectByValuesForDropdown(mulDdlApps, response.User.SelectedApps, ',');
               // hidmulDdlApps.Value = ConvertHelper.ConvertToString(response.User.SelectedApps, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlComputer, response.User.SelectedComputer, ',');
                hidmulDdlComputer.Value = ConvertHelper.ConvertToString(response.User.SelectedComputer, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlLaptop, response.User.SelectedLaptop, ',');
                hidmulDdlLaptop.Value = ConvertHelper.ConvertToString(response.User.SelectedLaptop, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlMobilePhone, response.User.SelectedMobilePhone, ',');
                hidmulDdlMobilePhone.Value = ConvertHelper.ConvertToString(response.User.SelectedMobilePhone, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlNetworkShares, response.User.SelectedNetworkShares, ',');
                hidmulDdlNetworkShares.Value = ConvertHelper.ConvertToString(response.User.SelectedNetworkShares, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlPrinters, response.User.SelectedPrinter, ',');
                hidmulDdlPrinters.Value = ConvertHelper.ConvertToString(response.User.SelectedPrinter, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlRemoteAccess, response.User.SelectedRemoteAccess, ',');
                hidmulDdlRemoteAccess.Value = ConvertHelper.ConvertToString(response.User.SelectedRemoteAccess, "");
                MultipleItemsSelectByValuesForDropdown(mulDDlSecurityGroup, response.User.SelectedSecurityGroup, ',');
                hidmulDDlSecurityGroup.Value = ConvertHelper.ConvertToString(response.User.SelectedSecurityGroup, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlServers, response.User.SelectedServers, ',');
                hidmulDdlServers.Value = ConvertHelper.ConvertToString(response.User.SelectedServers, "");
                MultipleItemsSelectByValuesForDropdown(mulDdlTablet, response.User.SelectedTablet, ',');
                hidmulDdlTablet.Value = ConvertHelper.ConvertToString(response.User.SelectedTablet, "");
                #endregion
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            //ShowMessage(ex.Message, false);
        }
    }
    #endregion

    #region [Add User]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string statusMessage = string.Empty;
            serviceURL = PostServiceURL;

            request.User = new User();
            request.User.Title = new GlobalMasterDetail();
            request.User.Department = new GlobalMasterDetail();
            request.UserApps = new UserApp();
            request.UserComputer = new UserComputer();
            request.UserLaptop = new UserLaptops();
            request.UserMobilePhone = new UserMobilePhone();
            request.UserNetworkShare = new UserNetworkShare();
            request.UserPrinter = new UserPrinter();
            request.UserRemoteAccess = new UserRemoteAccess();
            request.UserSecurityGroup = new UserSecurityGroup();
            request.UserServer = new UserServer();
            request.UserTablet = new UserTablet();

            ////Framing the URL
            //url = string.Format(serviceURL + "{0}", serviceName);

            request.User.FirstName = ConvertHelper.ConvertToString(txtFirstName.Text);
            request.User.LastName = ConvertHelper.ConvertToString(txtLastName.Text);
            request.User.UserName = ConvertHelper.ConvertToString(txtUserName.Text);
            request.User.Password = ConvertHelper.ConvertToString(txtPassword.Text);
            request.User.TitleID = ConvertHelper.ConvertToInteger(ddlTitle.SelectedValue);
            request.User.DepartmentID = ConvertHelper.ConvertToInteger(ddlDepartment.SelectedValue);

           // request.UserApps.SelectedAppIDs = ConvertHelper.ConvertToString(hidmulDdlApps.Value);
            request.UserComputer.SelectedComputerIDs = ConvertHelper.ConvertToString(hidmulDdlComputer.Value);
            request.UserLaptop.SelLaptopItems = ConvertHelper.ConvertToString(hidmulDdlLaptop.Value);
            request.UserMobilePhone.SelectedMobilePhoneIDs = ConvertHelper.ConvertToString(hidmulDdlMobilePhone.Value);
            request.UserNetworkShare.SelNetworkShareItems = ConvertHelper.ConvertToString(hidmulDdlNetworkShares.Value);
            request.UserPrinter.SelectedPrinterIDs = ConvertHelper.ConvertToString(hidmulDdlPrinters.Value);
            request.UserRemoteAccess.SelRemoteAccessItems = ConvertHelper.ConvertToString(hidmulDdlRemoteAccess.Value);
            request.UserSecurityGroup.SelectedSecurityGroupIDs = ConvertHelper.ConvertToString(hidmulDDlSecurityGroup.Value);
            request.UserServer.SelServerItems = ConvertHelper.ConvertToString(hidmulDdlServers.Value);
            request.UserTablet.SelectedTabletIDs = ConvertHelper.ConvertToString(hidmulDdlTablet.Value);

            request.User.Email = ConvertHelper.ConvertToString(txtEmail.Text);
            request.User.Phone1 = ConvertHelper.ConvertToString(txtPhone1.Text);
            request.User.Phone2 = ConvertHelper.ConvertToString(txtPhone2.Text);
            request.User.Notes = ConvertHelper.ConvertToString(txtNotes.Text);

            request.User.CreatedBy = currentUser.ApplicationUserID;
            request.User.ModifiedBy = currentUser.ApplicationUserID;
            bool IsRequired;
            if (hidIsAutoTaskCheckRequried.Value == "1")
                IsRequired = true;
            else
                IsRequired = false;

            if (CheckUser(hidAutoTaskID.Value, IsRequired))
            {
                request.User.MappingID = ConvertHelper.ConvertToString(hidAutoTaskID.Value,"0");
                if (hidIsAutoTask.Value == "1")
                    request.User.IsAutoTask = true;
                else
                    request.User.IsAutoTask = false;

                request.CurrentAction = CurrentAction;
                request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                if (CurrentAction == ActionType.Edit)
                {
                    request.User.UserID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVEUSER";
                }
                else
                {
                    request.User.StatusID = 1;
                    serviceName = "SAVEUSER";

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
                    CrudUser.Visible = false;
                    divGrdUserInfo.Visible = true;
                    //Response.Redirect("CustomerInfo.aspx?nav=users");
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudUser.Visible = true;
                    divGrdUserInfo.Visible = false;
                    //pre select the selected multi sel dropdown
                   // MultipleItemsSelectByValuesForDropdown(mulDdlApps, request.UserApps.SelectedAppIDs == null ? "" : request.UserApps.SelectedAppIDs.Replace(",", ";"), ';');
                    //hidmulDdlApps.Value = ConvertHelper.ConvertToString(request.UserApps.SelectedAppIDs == null ? "" : request.UserApps.SelectedAppIDs, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlComputer, request.UserComputer.SelectedComputerIDs == null ? "" : request.UserComputer.SelectedComputerIDs.Replace(",", ";"), ';');
                    hidmulDdlComputer.Value = ConvertHelper.ConvertToString(request.UserComputer.SelectedComputerIDs == null ? "" : request.UserComputer.SelectedComputerIDs, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlLaptop, request.UserLaptop.SelLaptopItems == null ? "" : request.UserLaptop.SelLaptopItems.Replace(",", ";"), ';');
                    hidmulDdlLaptop.Value = ConvertHelper.ConvertToString(request.UserLaptop.SelLaptopItems == null ? "" : request.UserLaptop.SelLaptopItems, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlMobilePhone, request.UserMobilePhone.SelectedMobilePhoneIDs == null ? "" : request.UserMobilePhone.SelectedMobilePhoneIDs.Replace(",", ";"), ';');
                    hidmulDdlMobilePhone.Value = ConvertHelper.ConvertToString(request.UserMobilePhone.SelectedMobilePhoneIDs == null ? "" : request.UserMobilePhone.SelectedMobilePhoneIDs, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlNetworkShares, request.UserNetworkShare.SelNetworkShareItems == null ? "" : request.UserNetworkShare.SelNetworkShareItems.Replace(",", ";"), ';');
                    hidmulDdlNetworkShares.Value = ConvertHelper.ConvertToString(request.UserNetworkShare.SelNetworkShareItems == null ? "" : request.UserNetworkShare.SelNetworkShareItems, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlPrinters, request.UserPrinter.SelectedPrinterIDs == null ? "" : request.UserPrinter.SelectedPrinterIDs.Replace(",", ";"), ';');
                    hidmulDdlPrinters.Value = ConvertHelper.ConvertToString(request.UserPrinter.SelectedPrinterIDs == null ? "" : request.UserPrinter.SelectedPrinterIDs, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlRemoteAccess, request.UserRemoteAccess.SelRemoteAccessItems == null ? "" : request.UserRemoteAccess.SelRemoteAccessItems.Replace(",", ";"), ';');
                    hidmulDdlRemoteAccess.Value = ConvertHelper.ConvertToString(request.UserRemoteAccess.SelRemoteAccessItems == null ? "" : request.UserRemoteAccess.SelRemoteAccessItems, "");
                    MultipleItemsSelectByValuesForDropdown(mulDDlSecurityGroup, request.UserSecurityGroup.SelectedSecurityGroupIDs == null ? "" : request.UserSecurityGroup.SelectedSecurityGroupIDs.Replace(",", ";"), ';');
                    hidmulDDlSecurityGroup.Value = ConvertHelper.ConvertToString(request.UserSecurityGroup.SelectedSecurityGroupIDs == null ? "" : request.UserSecurityGroup.SelectedSecurityGroupIDs, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlServers, request.UserServer.SelServerItems == null ? "" : request.UserServer.SelServerItems.Replace(",", ";"), ';');
                    hidmulDdlServers.Value = ConvertHelper.ConvertToString(request.UserServer.SelServerItems == null ? "" : request.UserServer.SelServerItems, "");
                    MultipleItemsSelectByValuesForDropdown(mulDdlTablet, request.UserTablet.SelectedTabletIDs == null ? "" : request.UserTablet.SelectedTabletIDs.Replace(",", ";"), ';');
                    hidmulDdlTablet.Value = ConvertHelper.ConvertToString(request.UserTablet.SelectedTabletIDs == null ? "" : request.UserTablet.SelectedTabletIDs, "");
                }
            }
            else
            {
                string message = "User details already exists.";
                message = message + "<br/> Do you want to Update?";
                hidIsAutoTaskCheckRequried.Value = "0";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
            }

        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Bind MultiSelect DropDown With Selected Values]
    public void BindMultiSelectDropDownWithSelectedValues(DropDownList ddlMulSelectAttribute, HiddenField hidMulSelectDdl, string multipleVal)
    {
        if (multipleVal != null && multipleVal != "")
        {
            string sMemory = multipleVal;
            ArrayList arrMemory = new ArrayList();
            arrMemory.AddRange(sMemory.Split(new char[] { ',' }));
            foreach (string s in arrMemory)
            {
                ddlMulSelectAttribute.Items.FindByValue(s).Attributes.Add("selected", "selected");
                hidMulSelectDdl.Value = ConvertHelper.ConvertToString(hidMulSelectDdl.Value) + "," + ConvertHelper.ConvertToString(s);
            }
        }
    }

    #endregion

    #region [Back to Grid View Mode of Corresponding User Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            //String bindGrid = "<script type='text/javascript'>jqGridGenerator(InitializeGrid('Users'));</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "InvokeGrid", bindGrid);
            //Response.Redirect("CustomerInfo.aspx?nav=users");

            ShowMessage("", true);
            CrudUser.Visible = false;
            divGrdUserInfo.Visible = true;
        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message, false);
        }

    }

    #endregion


    #region [User Selection for Provisioning Check List]
    protected void ddlAUsers_Change(object sender, EventArgs e)
    {
        try
        {
            string selectedUserId = ConvertHelper.ConvertToString(ddlAUsers.SelectedValue);
            ModifyUser(selectedUserId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion


    #region [Populate Dropdowns]
    private void PopulateControls()
    {
        try
        {
            request = new PTRequest();
            request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalMasterApp;
            request.Site = new Site();
            request.Site.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
           // PopulateGlobalMasterDropdown(request, mulDdlApps, true);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalMasterDepartment;
            PopulateGlobalMasterDropdown(request, ddlDepartment, false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalMasterTitles;
            PopulateGlobalMasterDropdown(request, ddlTitle, false);

            #region [GET ALL MOBILEDEVICES AND POPULATE]

            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLMOBILEDEVICES/Mastername/" + ConvertHelper.ConvertToString(base.sessionSiteId) + "/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);

            if (response != null && response.MobileDeviceList != null && response.MobileDeviceList.Count > 0)
            {
                PopulateMobileDeviceDropDownList(mulDdlMobilePhone, response.MobileDeviceList, true);
               
            }
            #endregion

            #region [GET ALL TABLETS AND POPULATE]

            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLMOBILEDEVICES/Mastername/" + ConvertHelper.ConvertToString(base.sessionSiteId) + "/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);

            if (response != null && response.MobileDeviceList != null && response.MobileDeviceList.Count > 0)
            {
                PopulateTabletDropDownList(mulDdlTablet, response.MobileDeviceList, true);
                //PopulateGlobalMasterDropdown(request, mulDdlTablet, true);
            }
            #endregion

            request.GlobalMaster = new GlobalMaster();
            request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";
            request.GlobalMaster.MasterName = GlobalMasterRemoteAccess;
            PopulateGlobalMasterDropdown(request, mulDdlRemoteAccess, true);


            //request.GlobalMaster = new GlobalMaster();
            //request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";
            ////request.GlobalMaster.MasterName = GlobalMasterTablet;
            //request.GlobalMaster.MasterName = GlobalMasterMobilePhone;
            //PopulateGlobalMasterDropdown(request, mulDdlTablet, true);


            #region [GET ALL COMPUTERS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLWORKSTATIONINFO/Mastername/" + ConvertHelper.ConvertToString(base.sessionSiteId) + "/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.WorkStationInfoList != null && response.WorkStationInfoList.Count > 0)
            {
                PopulateWorkStationDropDownList(mulDdlComputer, response.WorkStationInfoList, true);
            }

            #endregion

            request.GlobalMaster = new GlobalMaster();
            request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";
            request.GlobalMaster.MasterName = "Security Groups";
            PopulateGlobalMasterDropdown(request, mulDDlSecurityGroup, true);

            #region [GET ALL ServerS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLSERVERINFO/Mastername/" + ConvertHelper.ConvertToString(base.sessionSiteId) + "/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.ServerInfoList != null && response.ServerInfoList.Count > 0)
            {
                PopulateServerDropDownList(mulDdlServers, response.ServerInfoList, true);
            }
            #endregion

            #region [GET ALL PrinterS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLPRINTERS/Mastername/" + ConvertHelper.ConvertToString(base.sessionSiteId) + "/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.PrinterList != null && response.PrinterList.Count > 0)
            {
                PopulatePrinterDropDownList(mulDdlPrinters, response.PrinterList, true);
            }
            #endregion

            #region [GET ALL NetworkSharesS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLNETWORKSHARE/Mastername/" + ConvertHelper.ConvertToString(base.sessionSiteId) + "/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.NetworkShareDetailList != null && response.NetworkShareDetailList.Count > 0)
            {
                PopulateNetworkSharesDropDownList(mulDdlNetworkShares, response.NetworkShareDetailList, true);
            }
            #endregion


            #region [GET ALL LaptopS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLLAPTOPINFO/Mastername/" + ConvertHelper.ConvertToString(base.sessionSiteId) + "/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.LaptopInfoList != null && response.LaptopInfoList.Count > 0)
            {
                PopulateLaptopDropDownList(mulDdlLaptop, response.LaptopInfoList, true);
            }
            #endregion


            #region [GET ALL USERS AND POPULATE FOR PROVISIOING CHECK LIST PAGE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            string serviceResponseString = string.Empty;

            serviceURL = PostServiceURL + "POPULATEALLUSERS";
            request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.UserList != null && response.UserList.Count > 0)
            {
                PopulateUserDropDownList(ddlAUsers, response.UserList, true);
            }
            #endregion

        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message, false);
        }
    }

    #endregion

    #region [ Populate Auto Task Customer ]
    private void PopulateAutoTaskUser(string sCustomerID, string sFieldName)
    {

        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;

        serviceURL = GetServiceURL + "GETALLUSERAUTOTASK/" + sCustomerID + "/0/" + sFieldName;
        string userResultString = string.Empty;

        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        userResultString = webServiceHelper.GetRequest(serviceURL);
        response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
        if (response != null && response.UserList != null)
        {
            if (sFieldName == "id")
            {

                hidAutoTaskID.Value = ConvertHelper.ConvertToString(response.UserList[0].UserID, "");
                hidIsAutoTask.Value = "1";
                FilTextBox(txtFirstName, ConvertHelper.ConvertToString(response.UserList[0].FirstName, ""));
                FilTextBox(txtUserName, ConvertHelper.ConvertToString(response.UserList[0].UserName, ""));
                FilTextBox(txtLastName, ConvertHelper.ConvertToString(response.UserList[0].LastName, ""));
                FilTextBox(txtEmail, ConvertHelper.ConvertToString(response.UserList[0].Email, ""));
                FilTextBox(txtPhone1, ConvertHelper.ConvertToString(response.UserList[0].Phone1, ""));
                FilTextBox(txtPhone2, ConvertHelper.ConvertToString(response.UserList[0].Phone2, ""));
            }
            else if (response.SiteList != null)
            {
                Session["SiteList"] = response.SiteList;
            }
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


    private bool CheckUser(string sUserCode, bool IsRequried)
    {
        try
        {
            if (!IsRequried)
            {
                CurrentAction = ActionType.Edit;
                base.Id = ConvertHelper.ConvertToString(Session["userID"]);
                return true;
            }

            if (CurrentAction == ActionType.Add)
            {
                PTResponse checkresponse = new PTResponse();
                webServiceHelper = new WebServiceHelper();
                string serviceURL = string.Empty;
                List<User> userList = new List<User>();

                serviceURL = GetServiceURL + "GETALLUSERS/Users/" + sessionSiteId + "/0";
                string userResultString = string.Empty;

                webServiceHelper = new WebServiceHelper();
                userResultString = webServiceHelper.GetRequest(serviceURL);
                checkresponse = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
                if (checkresponse != null && checkresponse.UserList != null)
                {
                    User user = new User();
                    userList = checkresponse.UserList;
                    user = userList.Find(ste => ste.MappingID == sUserCode);
                    if (user != null)
                    {
                        userList.Clear();
                        base.Id = ConvertHelper.ConvertToString(user.UserID);
                        Session["userID"] = user.UserID;
                        Session["user"] = user;
                        userList.Add(user);
                    }
                    else
                    {
                        userList.Clear();
                    }
                }
                if (userList != null)
                {
                    if (userList.Count > 0)
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

    #endregion [ Populate Auto Task Customer ]

}
