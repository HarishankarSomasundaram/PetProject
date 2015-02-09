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

public partial class UserControlsWirelessInfo : UCController
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
            InitializeIframe(CrudWireless, divGrdWirelessInfo);
            provClose.Visible = false;

            if (CurrentAction == ActionType.Add)
            {
                PopulateControls();
                CrudWireless.Visible = true;
                divGrdWirelessInfo.Visible = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                PopulateControls();
                ModifyWireless(base.Id);
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.MoreView)//To view the page without edit
            //else if (CurrentAction == ActionType.View)
            {
                PopulateControls();
                ModifyWireless(base.Id);
                btnSubmit.Visible = false;
                DisableControls(divWirelessDetail);
                divWirelessDetail.Attributes.Add("class", divWirelessDetail.Attributes["class"] + " viewPage");
                inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");

                txtNotes.Attributes.Remove("class");
                btnBack.Visible = true;
                btnBack.Enabled = true;
            }
            else
            {
                CrudWireless.Visible = false;
                divGrdWirelessInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get Wireless Info and Bind the Controls For Edit And View]
    private void ModifyWireless(string wirelessId)
    {
        try
        {
            CrudWireless.Visible = true;
            divGrdWirelessInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(wirelessId) != null)
            {
                serviceURL = PostServiceURL + "GETWIRELESSBYWIRELESSID";
                request.Wireless = new Wireless();
                request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                request.Wireless.WirelessID = ConvertHelper.ConvertToInteger(wirelessId);
                hidEditID.Value = ConvertHelper.ConvertToString(wirelessId);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.Wireless != null && response.Wireless.WirelessModel != null && response.Wireless.WirelessManufacture != null)
            {
                txtHostName.Text = ConvertHelper.ConvertToString(response.Wireless.Hostname);

                //ddlType.SelectedValue = ConvertHelper.ConvertToString(response.Wireless.WirelessType.MasterDetailID);
                txtType.Text = ConvertHelper.ConvertToString(response.Wireless.WirelessTypeValue);

                ddlModel.SelectedValue = ConvertHelper.ConvertToString(response.Wireless.WirelessModel.MasterDetailID);
                ddlModel.ToolTip = ddlModel.SelectedItem.Text;
                ddlManufacture.SelectedValue = ConvertHelper.ConvertToString(response.Wireless.WirelessManufacture.MasterDetailID);

                txtSerialNo.Text = ConvertHelper.ConvertToString(response.Wireless.SerialNumber);

                if (response.Wireless.IPAddress == "DHCP")
                {
                    chkDHCP.Checked = true;
                    txtIPAddress.Attributes.Add("disabled", "disabled");
                }
                else
                    txtIPAddress.Text = ConvertHelper.ConvertToString(response.Wireless.IPAddress, "");

                txtSubnet.Text = ConvertHelper.ConvertToString(response.Wireless.Subnet);
                txtGateway.Text = ConvertHelper.ConvertToString(response.Wireless.Gateway);
                txtAdminUsername.Text = ConvertHelper.ConvertToString(response.Wireless.AdminUserName);

                txtPassword.Text = ConvertHelper.ConvertToString(response.Wireless.AdminPassword);

                txtSSID.Text = ConvertHelper.ConvertToString(response.Wireless.SSID);
                txtAuthentication.Text = ConvertHelper.ConvertToString(response.Wireless.Authentication);

                txtEncryption.Text = ConvertHelper.ConvertToString(response.Wireless.WirelessEncryption);

                txtNotes.Text = ConvertHelper.ConvertToString(response.Wireless.Notes.Replace("|", ";"));

                //txtWarrantyExpires.Text = ConvertHelper.ConvertToString(response.Wireless.WarrantyExpiresOn == null ? string.Empty : response.Wireless.WarrantyExpiresOn.ToString());
                //txtInstalledDate.Text = ConvertHelper.ConvertToString(response.Wireless.InstalledOn == null ? string.Empty : response.Wireless.InstalledOn.ToString());
                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.Wireless.InstalledOn).ToString("MM-dd-yyyy");
                txtWarrantyExpires.Text = ConvertHelper.ConvertToDateTime(response.Wireless.WarrantyExpiresOn).ToString("MM-dd-yyyy");
            }
            else
            {

                ShowMessage("Invalid record could not be found ", false);

            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            //ShowMessage(ex.Message, false);
        }
    }
    #endregion

    #region [Add Wireless]
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

            if (ConvertHelper.ConvertToString(txtInstalledDate.Text) != null && ConvertHelper.ConvertToString(txtWarrantyExpires.Text) != null && ConvertHelper.ConvertToString(txtWarrantyExpires.Text, "") != "" && ConvertHelper.ConvertToString(txtInstalledDate.Text, "") != "")
            {
                request.Wireless = new Wireless();
                request.Wireless.WirelessType = new GlobalMasterDetail();
                request.Wireless.WirelessModel = new GlobalMasterDetail();
                request.Wireless.WirelessManufacture = new GlobalMasterDetail();

                request.Wireless.Hostname = ConvertHelper.ConvertToString(txtHostName.Text);
                //request.Wireless.WirelessType.MasterDetailID = ConvertHelper.ConvertToInteger(ddlType.SelectedValue);
                request.Wireless.WirelessTypeValue = ConvertHelper.ConvertToString(txtType.Text, "");
                request.Wireless.WirelessModel.MasterDetailID = ConvertHelper.ConvertToInteger(ddlModel.SelectedValue);
                request.Wireless.WirelessManufacture.MasterDetailID = ConvertHelper.ConvertToInteger(ddlManufacture.SelectedValue);

                CultureInfo provider = CultureInfo.InvariantCulture;
                //DateTime installedDateTime = DateTime.Parse(txtInstalledDate.Text);
                request.Wireless.InstalledOn = ConvertHelper.ConvertToString(txtInstalledDate.Text); //installedDateTime.Add(new TimeSpan(5, 30, 0));

                //DateTime warrantyExpiresDateTime = DateTime.Parse(txtWarrantyExpires.Text);
                request.Wireless.WarrantyExpiresOn = ConvertHelper.ConvertToString(txtWarrantyExpires.Text); //warrantyExpiresDateTime.Add(new TimeSpan(5, 30, 0));

                request.Wireless.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text);

                if (chkDHCP.Checked)
                    request.Wireless.IPAddress = "DHCP";
                else
                    request.Wireless.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text, "");

                request.Wireless.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text);
                request.Wireless.Gateway = ConvertHelper.ConvertToString(txtGateway.Text);
                request.Wireless.AdminUserName = ConvertHelper.ConvertToString(txtAdminUsername.Text);
                request.Wireless.AdminPassword = ConvertHelper.ConvertToString(txtPassword.Text);
                request.Wireless.SSID = ConvertHelper.ConvertToInteger(txtSSID.Text, 0);
                request.Wireless.Authentication = ConvertHelper.ConvertToString(txtAuthentication.Text, "");
                request.Wireless.WirelessEncryption = ConvertHelper.ConvertToString(txtEncryption.Text, "");
                request.Wireless.Notes = ConvertHelper.ConvertToString(txtNotes.Text, "").Replace(";", ";");

                request.Wireless.CreatedBy = currentUser.ApplicationUserID;
                request.Wireless.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.Wireless.WirelessID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVEWIRELESS";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.Wireless.StatusID = 1;
                    serviceName = "SAVEWIRELESS";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    ShowMessage(response.Message, true);
                    if (HiddenColorBox.Value == "0")
                    {
                        CrudWireless.Visible = false;
                        divGrdWirelessInfo.Visible = true;
                    }
                    else
                    {
                        CrudWireless.Visible = false;
                        divGrdWirelessInfo.Visible = false;
                        provClose.Visible = true;
                    }
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudWireless.Visible = true;
                    divGrdWirelessInfo.Visible = false;
                }
            }
            else
            {
                CrudWireless.Visible = true;
                divGrdWirelessInfo.Visible = false;
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



    #region [Back to Grid View Mode of Corresponding Wireless Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudWireless.Visible = false;
            divGrdWirelessInfo.Visible = true;
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

            //request.GlobalMaster = new GlobalMaster();
            //request.GlobalMaster.MasterName = "Global Master - Wireless Type";
            //PopulateGlobalMasterDropdown(request, ddlType);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalWirelessManfacture;
            PopulateGlobalMasterDropdown(request, ddlManufacture, false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalWirelessModel;
            PopulateGlobalMasterDropdown(request, ddlModel, false);


            //Get All Firewall and Populate for Provisioning check list page
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLWIRELESSES/WIRELESSES/0/dummytext";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.WirelessList != null && response.WirelessList.Count > 0)
            {
                PopulateWirelessDropDownList(ddldeviceList, response.WirelessList, true);
            }

        }
        catch (Exception ex)
        { ShowMessage(ex.Message, false); }
    }
    #endregion

    #region [Wireless Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyWireless(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion
}
