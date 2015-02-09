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
using System.IO;

public partial class UserControlsPhoneSystemInfo : UCController
{
    PTRequest request;
    PTResponse response;
    PhoneSystem phoneSystem;
    WebServiceHelper webServiceHelper;
    string downloadpath = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divMessage.Attributes["style"] = "display:block";
        DetermineAction();
        if (!Page.IsPostBack && CurrentAction != ActionType.MoreView) { Page.Validate(); }
    }

    #region [Determine Action]
    private void DetermineAction()
    {
        InitializeIframe(CrudPhone, divGrdPhoneInfo);
        provClose.Visible = false;

        if (CurrentAction == ActionType.Add)
        {
            PopulateControls();
            CrudPhone.Visible = true;
            divGrdPhoneInfo.Visible = false;
            DwnldLink.Visible = false;
            
        }
        else if (CurrentAction == ActionType.Edit)
        {
            PopulateControls();
            ModifyPhoneSystem(base.Id);
            CrudPhone.Visible = true;
            divGrdPhoneInfo.Visible = false;
        }
        else if (CurrentAction == ActionType.View)
        {
            CrudPhone.Visible = false;
            divGrdPhoneInfo.Visible = true;
        }
        else if (CurrentAction == ActionType.MoreView)
        {
            PopulateControls();
            ModifyPhoneSystem(base.Id);
            DisableControls(divPhoneSystemDetails);
            divPhoneSystemDetails.Attributes.Add("class", divPhoneSystemDetails.Attributes["class"] + " viewPage");
            inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
            inlineInterface.Attributes.Add("class", inlineInterface.Attributes["class"] + " columnAlign");

            txtInterfaces.Attributes.Remove("class");
            txtNotes.Attributes.Remove("class");
            CrudPhone.Visible = true;
            divGrdPhoneInfo.Visible = false;
            btnSubmit.Visible = false;
            btnBack.Visible = true;
            btnBack.Enabled = true;
            fileUpload.Visible = false;
            noimg.Visible = true;
        }
    }
    #endregion[Determine Action]

    #region [Populate Dropdowns]
    private void PopulateControls()
    {
        request = new PTRequest();
        request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

        request.GlobalMaster = new GlobalMaster();
        request.GlobalMaster.MasterName = GlobalMasterPhoneSystemModelMaster;
        request.Site = new Site();
        request.Site.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
        PopulateGlobalMasterDropdown(request, ddlModel, false);

        request.GlobalMaster.MasterName = GlobalMasterPhoneSystemOSversion;
        PopulateGlobalMasterDropdown(request, ddlOSVersion, false);

        request.GlobalMaster.MasterName = GlobalMasterPhoneSystemModules;
        PopulateGlobalMasterDropdown(request, ddlModules, false);
        ddlModules.Items.Insert(0, new ListItem(""));

        #region [GE ALL USERS AND POPULATE]
        response = new PTResponse();
        request = new PTRequest();
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
            PopulateUserDropDownList(ddlAUsers, response.UserList, true);
        }
        #endregion


        //Get All Phone system and Populate for Provisioning check list page
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        serviceURL = string.Empty;
        userResultString = string.Empty;
        serviceURL = GetServiceURL + "GETALLPHONESYSTEMS/PHONESYSTEM/0/0";
        request.URL = serviceURL;
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        userResultString = webServiceHelper.GetRequest(serviceURL);
        response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
        if (response != null && response.PhoneSystemList != null && response.PhoneSystemList.Count > 0)
        {
            PopulatePhoneSystemDropDownList(ddldeviceList, response.PhoneSystemList, true);
        }
    }
    #endregion[Populate Dropdowns]

    #region[Modify Router]
    private void ModifyPhoneSystem(string phoneSystemid)
    {
        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;

        if (ConvertHelper.ConvertToString(phoneSystemid) != null)
        {
            serviceURL = PostServiceURL + "GETPHONESYSTEMBYPHONESYSTEMID";
            request.PhoneSystem = new PhoneSystem();
            request.PhoneSystem.PhoneSystemID = ConvertHelper.ConvertToInteger(phoneSystemid);
            hidEditID.Value = ConvertHelper.ConvertToString(phoneSystemid);
            request.URL = serviceURL;
        }
        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.PhoneSystem != null)
        {
            string serviceName = string.Empty;
            phoneSystem = new PhoneSystem();
            phoneSystem = response.PhoneSystem;
            txtHostName.Text = ConvertHelper.ConvertToString(phoneSystem.Hostname, "");
            txtHostName.ToolTip = ConvertHelper.ConvertToString(phoneSystem.Hostname, "");

            txtManufacture.Text = ConvertHelper.ConvertToString(phoneSystem.Manufacture, "");
            txtManufacture.ToolTip = ConvertHelper.ConvertToString(phoneSystem.Manufacture, "");

            ddlModel.SelectedValue = ConvertHelper.ConvertToString(phoneSystem.PhoneSystemModel.MasterDetailID);
            ddlModel.ToolTip = ddlModel.SelectedItem.Text;

            ddlOSVersion.SelectedValue = ConvertHelper.ConvertToString(phoneSystem.OSVersion.MasterDetailID);
            ddlOSVersion.ToolTip = ddlOSVersion.SelectedItem.Text;

            txtMemory.Text = ConvertHelper.ConvertToString(phoneSystem.Memory, "");
            txtMemory.ToolTip = ConvertHelper.ConvertToString(phoneSystem.Memory, "");

            txtSerialNo.Text = ConvertHelper.ConvertToString(phoneSystem.SerialNumber, "");
            txtSerialNo.ToolTip = ConvertHelper.ConvertToString(phoneSystem.SerialNumber, "");

            txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(phoneSystem.InstalledOn).ToString("MM-dd-yyyy");
            txtWarrantyExpires.Text = ConvertHelper.ConvertToDateTime(phoneSystem.WarrantyExpiresOn).ToString("MM-dd-yyyy");
            txtIPAddress.Text = ConvertHelper.ConvertToString(phoneSystem.IPAddress, "");
            txtSubnet.Text = ConvertHelper.ConvertToString(phoneSystem.Subnet, "");
            txtGateway.Text = ConvertHelper.ConvertToString(phoneSystem.Gateway, "");
            txtAdminUsername.Text = ConvertHelper.ConvertToString(phoneSystem.AdminUserName, "");
            txtPassword.Text = ConvertHelper.ConvertToString(phoneSystem.AdminPassword, "");
            txtPassword.ToolTip = ConvertHelper.ConvertToString(phoneSystem.AdminPassword, "");

            ddlOSVersion.SelectedValue = ConvertHelper.ConvertToString(phoneSystem.OSVersion.MasterDetailID);
            ddlOSVersion.ToolTip = ddlOSVersion.SelectedItem.Text;

            ddlType.SelectedValue = ConvertHelper.ConvertToString(phoneSystem.PhoneType);
            txtFirmware.Text = ConvertHelper.ConvertToString(phoneSystem.Firmware, "");
            txtFirmware.ToolTip = ConvertHelper.ConvertToString(phoneSystem.Firmware, "");

            txtInterfaces.Text = ConvertHelper.ConvertToString(phoneSystem.PhoneSystemInterfaces, "").Replace(',', ';');
            txtInterfaces.ToolTip = ConvertHelper.ConvertToString(phoneSystem.PhoneSystemInterfaces, "").Replace(',', ';');

            txtNotes.Text = ConvertHelper.ConvertToString(phoneSystem.PhoneSystemNotes, "").Replace(',', ';');
            txtNotes.ToolTip = ConvertHelper.ConvertToString(phoneSystem.PhoneSystemNotes, "").Replace(',', ';');

            hidModuleID.Value = ConvertHelper.ConvertToString(phoneSystem.PhoneSystemModules, "");
            MultipleItemsSelectByValuesForDropdown(ddlModules, phoneSystem.PhoneSystemModules, ',');
            MultipleItemsSelectByValuesForDropdown(ddlAUsers, phoneSystem.PhoneSystemAssignedUsers, '|');

            if (phoneSystem.ViewDocumentPath != null)
            {
                downloadpath = ConvertHelper.ConvertToString(phoneSystem.ViewDocumentPath);
                HiddenFieldDwnldLink.Value = downloadpath;

            }
            else
            {
                DwnldLink.Visible = false;
                noimg.Text = "No configuration file was uploaded.";
            }
        }
        else
        {
            ShowMessage("Unable to reteive the information", false);
        }

    }
    #endregion[Modify Phone System]

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string serviceName = string.Empty;
        string currenttime;
        string fileName = string.Empty; string fileType = string.Empty;
        string SaveLocation = string.Empty;
        phoneSystem = new PhoneSystem();
        phoneSystem.PhoneType = ConvertHelper.ConvertToString(ddlType.SelectedValue, "");
        phoneSystem.Hostname = ConvertHelper.ConvertToString(txtHostName.Text, "");
        phoneSystem.Manufacture = ConvertHelper.ConvertToString(txtManufacture.Text, "");
        phoneSystem.PhoneSystemModel = new GlobalMasterDetail();
        phoneSystem.PhoneSystemModel.MasterDetailID = ConvertHelper.ConvertToInteger(ddlModel.SelectedValue);
        phoneSystem.Memory = ConvertHelper.ConvertToString(txtMemory.Text, "");
        phoneSystem.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text, "");
        phoneSystem.InstalledOn = ConvertHelper.ConvertToString(txtInstalledDate.Text);
        phoneSystem.WarrantyExpiresOn = ConvertHelper.ConvertToString(txtWarrantyExpires.Text);
        phoneSystem.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text, "");
        phoneSystem.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text, "");
        phoneSystem.Gateway = ConvertHelper.ConvertToString(txtGateway.Text, "");
        phoneSystem.AdminUserName = ConvertHelper.ConvertToString(txtAdminUsername.Text, "");
        phoneSystem.AdminPassword = ConvertHelper.ConvertToString(txtPassword.Text, "");
        phoneSystem.OSVersion = new GlobalMasterDetail();
        phoneSystem.OSVersion.MasterDetailID = ConvertHelper.ConvertToInteger(ddlOSVersion.SelectedValue);
        phoneSystem.Firmware = ConvertHelper.ConvertToString(txtFirmware.Text, "");
        phoneSystem.CreatedBy = currentUser.ApplicationUserID;
        phoneSystem.ModifiedBy = currentUser.ApplicationUserID;

        phoneSystem.PhoneSystemModules = ConvertHelper.ConvertToString(hidModuleID.Value == "" ? "" : hidModuleID.Value.Replace(',', ';'), "");
        phoneSystem.PhoneSystemInterfaces = ConvertHelper.ConvertToString(txtInterfaces.Text == "" ? "" : txtInterfaces.Text, "");
        phoneSystem.PhoneSystemAssignedUsers = ConvertHelper.ConvertToString(hidAssignedUserID.Value == "" ? "" : hidAssignedUserID.Value.Replace(',', ','), "");
        phoneSystem.PhoneSystemNotes = ConvertHelper.ConvertToString(txtNotes.Text == "" ? "" : txtNotes.Text.Replace(',', ';'), "");


        //Upload functions

        if (fileUpload.HasFile)
        {
            fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
            fileType = Path.GetFileName(fileUpload.PostedFile.ContentType); 
            currenttime = DateTime.Now.ToString("yyyyMMddhhss");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            SaveLocation = Server.MapPath("~/App_Data/Documents/PhoneSystem/") + fileNameWithoutExtension + "_" + currenttime + extension;
            fileUpload.PostedFile.SaveAs(SaveLocation);
        }
        phoneSystem.Documents = new Documents();
        phoneSystem.Documents.Type = "PHONESYSTEM";
        phoneSystem.Documents.DocumentPath = SaveLocation;
        phoneSystem.Documents.DocumentName = fileName;
        phoneSystem.Documents.DocumentType = fileType;

        if (ConvertHelper.ConvertToString(base.Id) != null)
            phoneSystem.PhoneSystemID = ConvertHelper.ConvertToInteger(base.Id);
        serviceName = "SAVEPHONESYSTEM";

        request = new PTRequest();

        request.PhoneSystem = phoneSystem;
        request.PhoneSystem.Site = new Site();
        request.PhoneSystem.Site.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
        request.CurrentAction = CurrentAction;
        request.URL = string.Format(PostServiceURL + "{0}", serviceName);

        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.isSuccess == true)
        {
            ShowMessage(response.Message, true);
            if (HiddenColorBox.Value == "0")
            {
                CrudPhone.Visible = false;
                divGrdPhoneInfo.Visible = true;
            }
            else
            {
                CrudPhone.Visible = false;
                divGrdPhoneInfo.Visible = false;
                provClose.Visible = true;
            }
        }
        else
        {
            ShowMessage(response.Message, false);
            CrudPhone.Visible = true;
            divGrdPhoneInfo.Visible = false;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerInfo.aspx?do=v&nav=Phone%20System", false);
    }

    #region [Download Uploaded Configuration]
    protected void DwnldLink_Click(object sender, EventArgs e)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(downloadpath);
            if (fileInfo.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion


    #region [Phone System Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyPhoneSystem(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion
}