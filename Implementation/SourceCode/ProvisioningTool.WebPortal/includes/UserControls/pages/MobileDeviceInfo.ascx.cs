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

public partial class UserControlsMobileDeviceInfo : UCController
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
            InitializeIframe(CrudMobileDevice, divGrdMobileDeviceInfo);
            provClose.Visible = false;

            if (CurrentAction == ActionType.Add)
            {
                PopulateControls();
                CrudMobileDevice.Visible = true;
                divGrdMobileDeviceInfo.Visible = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                PopulateControls();
                ModifyMobileDevice(base.Id);
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.MoreView)//To view the page without edit
            //else if (CurrentAction == ActionType.View)
            {
                PopulateControls();
                ModifyMobileDevice(base.Id);
                btnSubmit.Visible = false;
                DisableControls(divMobileDeviceDetail);
                divMobileDeviceDetail.Attributes.Add("class", divMobileDeviceDetail.Attributes["class"] + " viewPage");
                inlineAssignedUser.Attributes.Add("class", inlineAssignedUser.Attributes["class"] + " columnAlign");
                btnBack.Visible = true;
                btnBack.Enabled = true;
            }
            else
            {
                CrudMobileDevice.Visible = false;
                divGrdMobileDeviceInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get MobileDevice Info and Bind the Controls For Edit And View]
    private void ModifyMobileDevice(string mobileDeviceid)
    {
        try
        {
            CrudMobileDevice.Visible = true;
            divGrdMobileDeviceInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(mobileDeviceid) != null)
            {
                serviceURL = PostServiceURL + "GETMOBILEDEVICEBYMOBILEDEVICEID";
                request.MobileDevice = new MobileDevice();
                request.MobileDevice.MobileDeviceID = ConvertHelper.ConvertToInteger(mobileDeviceid);
                hidEditID.Value = ConvertHelper.ConvertToString(mobileDeviceid);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.MobileDevice != null && response.MobileDevice.MobileDeviceType != null && response.MobileDevice.MobileDeviceModel != null && response.MobileDevice.MobileDeviceManufacture != null)
            {
                txtHostName.Text = ConvertHelper.ConvertToString(response.MobileDevice.Hostname);
                txtHostName.ToolTip = ConvertHelper.ConvertToString(response.MobileDevice.Hostname);

                ddlType.SelectedValue = ConvertHelper.ConvertToString(response.MobileDevice.MobileDeviceType.MasterDetailID);
                ddlModel.SelectedValue = ConvertHelper.ConvertToString(response.MobileDevice.MobileDeviceModel.MasterDetailID);
                ddlManufacture.SelectedValue = ConvertHelper.ConvertToString(response.MobileDevice.MobileDeviceManufacture.MasterDetailID);
                ddlAssignedUser.SelectedValue = ConvertHelper.ConvertToString(response.MobileDevice.AssignedUser.UserID);
                ddlType.ToolTip = ddlType.SelectedItem.Text;
                ddlModel.ToolTip = ddlModel.SelectedItem.Text;
                ddlManufacture.ToolTip = ddlManufacture.SelectedItem.Text;
                //txtInstalledOn.Text = ConvertHelper.ConvertToString(response.MobileDevice.InstalledOn == null ? string.Empty : response.MobileDevice.InstalledOn.ToString());
                txtInstalledOn.Text = ConvertHelper.ConvertToDateTime(response.MobileDevice.InstalledOn).ToString("MM-dd-yyyy");
            }
            else
            {
                ShowMessage("Invalid / Incomplete input", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            //ShowMessage(ex.Message, false);
        }
    }
    #endregion

    #region [Add MobileDevice]
    protected void btnSubmit_Click(object sender, EventArgs e)
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

            request.MobileDevice = new MobileDevice();
            request.MobileDevice.MobileDeviceType = new GlobalMasterDetail();
            request.MobileDevice.MobileDeviceModel = new GlobalMasterDetail();
            request.MobileDevice.MobileDeviceManufacture = new GlobalMasterDetail();
            request.MobileDevice.AssignedUser = new User();
            request.MobileDevice.Hostname = ConvertHelper.ConvertToString(txtHostName.Text);
            request.MobileDevice.MobileDeviceType.MasterDetailID = ConvertHelper.ConvertToInteger(ddlType.SelectedValue);
            request.MobileDevice.MobileDeviceModel.MasterDetailID = ConvertHelper.ConvertToInteger(ddlModel.SelectedValue);
            request.MobileDevice.MobileDeviceManufacture.MasterDetailID = ConvertHelper.ConvertToInteger(ddlManufacture.SelectedValue);
            request.MobileDevice.AssignedUser.UserID = ConvertHelper.ConvertToInteger(ddlAssignedUser.SelectedValue);
            CultureInfo provider = CultureInfo.InvariantCulture;
            //string str = DMY_MDY();
            if (ConvertHelper.ConvertToString(txtInstalledOn.Text) != null && ConvertHelper.ConvertToString(txtInstalledOn.Text, "") != "")
            {
                //DateTime yourDateTime = DateTime.Parse(txtInstalledOn.Text);
                request.MobileDevice.InstalledOn = txtInstalledOn.Text;// yourDateTime.Add(new TimeSpan(5, 30, 0));

                request.MobileDevice.CreatedBy = currentUser.ApplicationUserID;
                request.MobileDevice.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.MobileDevice.MobileDeviceID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVEMOBILEDEVICE";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.MobileDevice.StatusID = 1;
                    serviceName = "SAVEMOBILEDEVICE";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                //            response = webServiceHelper.PostRequest(request);
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    ShowMessage(response.Message, true);
                    if (HiddenColorBox.Value == "0")
                    {
                        CrudMobileDevice.Visible = false;
                        divGrdMobileDeviceInfo.Visible = true;
                    }
                    else {
                        CrudMobileDevice.Visible = false;
                        divGrdMobileDeviceInfo.Visible = false;
                        provClose.Visible = true;
                    }
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudMobileDevice.Visible = true;
                    divGrdMobileDeviceInfo.Visible = false;
                }
            }
            else
            {
                CrudMobileDevice.Visible = true;
                divGrdMobileDeviceInfo.Visible = false;
                ShowMessage("Please enter date value", false);

            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    public string DMY_MDY(string d)
    {
        string tm = "";
        if (d.Length > 10)
        {
            tm = d.Substring(10);
            d = d.Substring(0, 10);
        }
        string[] a = d.Split('-');
        if (a.Length > 1)
        {
            return a[0] + a[2] + a[1] + tm;
        }
        return "";
    }


    #region [Back to Grid View Mode of Corresponding MobileDevice Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudMobileDevice.Visible = false;
            divGrdMobileDeviceInfo.Visible = true;
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
            request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = "Mobile Device Types";
            PopulateGlobalMasterDropdown(request, ddlType, true);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = "Mobile Device Manufacturers";
            PopulateGlobalMasterDropdown(request, ddlManufacture, true);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = "Mobile Device Models";
            PopulateGlobalMasterDropdown(request, ddlModel, true);

            #region [GET ALL USERS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLUSERS/Mastername/0/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.UserList != null && response.UserList.Count > 0)
            {
                PopulateUserDropDownList(ddlAssignedUser, response.UserList, true);
            }
            #endregion

            #region [GET ALL MOBILEDEVICES AND POPULATE]

            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = GetServiceURL + "GETALLMOBILEDEVICES/Mastername/0/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);

            if (response != null && response.MobileDeviceList != null && response.MobileDeviceList.Count > 0)
            {
                PopulateMobileDeviceDropDownList(ddldeviceList, response.MobileDeviceList, true);
            }
            #endregion
        }
        catch (Exception ex)
        { ShowMessage(ex.Message, false); }
    }


    #endregion


    #region [Mobile Device Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyMobileDevice(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion
}
