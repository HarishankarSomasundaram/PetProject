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

public partial class UserControlsRouterInfo : UCController
{
    PTRequest request;
    PTResponse response;
    Router router;
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
        InitializeIframe(CrudRouter, divGrdRouterInfo);
        provClose.Visible = false;

        if (CurrentAction == ActionType.Add)
        {
            PopulateControls();
            CrudRouter.Visible = true;
            divGrdRouterInfo.Visible = false;
            DwnldLink.Visible = false;
        }
        else if (CurrentAction == ActionType.Edit)
        {
            PopulateControls();
            ModifyRouter(base.Id);
            CrudRouter.Visible = true;
            divGrdRouterInfo.Visible = false;
        }
        else if (CurrentAction == ActionType.View)
        {
            CrudRouter.Visible = false;
            divGrdRouterInfo.Visible = true;
        }
        else if (CurrentAction == ActionType.MoreView)
        {
            PopulateControls();
            ModifyRouter(base.Id);
            DisableControls(DivRouterDetail);
            DivRouterDetail.Attributes.Add("class", DivRouterDetail.Attributes["class"] + " viewPage");
            inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
            txtInterfaces.Attributes.Remove("class");
            txtNotes.Attributes.Remove("class");
            txtsitePass.Attributes.Remove("class");
            btnSubmit.Visible = false;
            btnBack.Enabled = true;
            CrudRouter.Visible = true;
            divGrdRouterInfo.Visible = false;
            fileUpload.Visible = false;
        }
    }


    #endregion[Determine Action]

    #region[Modify Router]
    private void ModifyRouter(string routerid)
    {
        request = new PTRequest();
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;

        if (ConvertHelper.ConvertToString(routerid) != null)
        {
            serviceURL = PostServiceURL + "GETROUTERBYROUTERID";
            request.Router = new Router();
            request.Router.RouterID = ConvertHelper.ConvertToInteger(routerid);
            hidEditID.Value = ConvertHelper.ConvertToString(routerid);
            request.URL = serviceURL;
        }
        response = webServiceHelper.PostRequest<PTResponse>(request);
        if (response != null && response.Router != null)
        {
            string serviceName = string.Empty;
            router = new Router();
            router = response.Router;
            txtHostName.Text = ConvertHelper.ConvertToString(router.Hostname, "");
            txtHostName.ToolTip = ConvertHelper.ConvertToString(router.Hostname, "");

            txtManufacture.Text = ConvertHelper.ConvertToString(router.Manufacture, "");
            txtManufacture.ToolTip = ConvertHelper.ConvertToString(router.Manufacture, "");

            ddlModel.SelectedValue = ConvertHelper.ConvertToString(router.RouterModel.MasterDetailID, "");
            ddlOSVersion.SelectedValue = ConvertHelper.ConvertToString(router.OSVersion.MasterDetailID, "");
            txtMemory.Text = ConvertHelper.ConvertToString(router.Memory, "");
            txtMemory.ToolTip = ConvertHelper.ConvertToString(router.Memory, "");

            txtSerialNo.Text = ConvertHelper.ConvertToString(router.SerialNumber, "");
            txtSerialNo.ToolTip = ConvertHelper.ConvertToString(router.SerialNumber, "");

            txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(router.InstalledOn).ToString("MM-dd-yyyy");
            txtWarrantyExpires.Text = ConvertHelper.ConvertToDateTime(router.WarrantyExpiresOn).ToString("MM-dd-yyyy");
            txtIPAddress.Text = ConvertHelper.ConvertToString(router.IPAddress, "");
            txtSubnet.Text = ConvertHelper.ConvertToString(router.Subnet, "");
            txtGateway.Text = ConvertHelper.ConvertToString(router.Gateway, "");
            txtAdminUsername.Text = ConvertHelper.ConvertToString(router.AdminUserName, "");
            txtPassword.Text = ConvertHelper.ConvertToString(router.AdminPassword, "");
            txtPassword.ToolTip = ConvertHelper.ConvertToString(router.AdminPassword, "");

            txtFirmware.Text = ConvertHelper.ConvertToString(router.Firmware, "");
            txtFirmware.ToolTip = ConvertHelper.ConvertToString(router.Firmware, "");

            txtInterfaces.Text = ConvertHelper.ConvertToString(router.RouterInterfaces, "").Replace(',', ';');
            txtNotes.Text = ConvertHelper.ConvertToString(router.RouterNotes, "").Replace(',', ';');
            txtNotes.ToolTip = ConvertHelper.ConvertToString(router.RouterNotes, "").Replace(',', ';');
            txtsitePass.Text = ConvertHelper.ConvertToString(router.RouterSiteToSites, "").Replace(',', ';');
            hidModuleID.Value = ConvertHelper.ConvertToString(router.RouterModules, "");
            MultipleItemsSelectByValuesForDropdown(ddlModules, router.RouterModules, ',');

            if (router.ViewDocumentPath != null)
            {
                downloadpath = ConvertHelper.ConvertToString(router.ViewDocumentPath);

            }
            else
            {
                DwnldLink.Visible = false;
                noimg.Visible = true;
                noimg.Text = "No configuration file was uploaded.";
            }
        }
        else
        {
            ShowMessage("Unable to reteive the information", false);
        }

    }
    #endregion[Modify Router]

    #region [Populate Dropdowns]
    private void PopulateControls()
    {
        request = new PTRequest();
        request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

        request.GlobalMaster = new GlobalMaster();
        request.GlobalMaster.MasterName = GlobalRouterModel;
        PopulateGlobalMasterDropdown(request, ddlModel, false);

        request.GlobalMaster.MasterName = GlobalRouterOS;
        PopulateGlobalMasterDropdown(request, ddlOSVersion, false);

        request.GlobalMaster.MasterName = GlobalRouterModule;
        PopulateGlobalMasterDropdown(request, ddlModules, false);
        ddlModules.Items.Insert(0, new ListItem(""));

        //Get All Routers and Populate for Provisioning check list page
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        string serviceURL = string.Empty;
        string userResultString = string.Empty;
        serviceURL = GetServiceURL + "GETALLROUTERS/Router/0/St";
        request.URL = serviceURL;
        response = new PTResponse();
        webServiceHelper = new WebServiceHelper();
        userResultString = webServiceHelper.GetRequest(serviceURL);
        response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
        if (response != null && response.RouterList != null && response.RouterList.Count > 0)
        {
            PopulateRouterDropDownList(ddldeviceList, response.RouterList, true);
        }

    }
    #endregion[Populate Dropdowns]

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string serviceName = string.Empty;
            string currenttime;
            string fileName = string.Empty; string fileType = string.Empty;
            string SaveLocation = string.Empty;
            router = new Router();
            router.Site = new ProvisioningTool.Entity.Site();
            router.Hostname = ConvertHelper.ConvertToString(txtHostName.Text, "");
            router.Manufacture = ConvertHelper.ConvertToString(txtManufacture.Text, "");
            router.RouterModel = new GlobalMasterDetail();
            router.RouterModel.MasterDetailID = ConvertHelper.ConvertToInteger(ddlModel.SelectedValue);
            router.Memory = ConvertHelper.ConvertToString(txtMemory.Text, "");
            router.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text, "");
            router.InstalledOn = ConvertHelper.ConvertToString(txtInstalledDate.Text);
            router.WarrantyExpiresOn = ConvertHelper.ConvertToString(txtWarrantyExpires.Text);
            router.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text, "");
            router.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text, "");
            router.Gateway = ConvertHelper.ConvertToString(txtGateway.Text, "");
            router.AdminUserName = ConvertHelper.ConvertToString(txtAdminUsername.Text, "");
            router.AdminPassword = ConvertHelper.ConvertToString(txtPassword.Text, "");
            router.OSVersion = new GlobalMasterDetail();
            router.OSVersion.MasterDetailID = ConvertHelper.ConvertToInteger(ddlOSVersion.SelectedValue);
            router.Firmware = ConvertHelper.ConvertToString(txtFirmware.Text, "");
            router.CreatedBy = currentUser.ApplicationUserID;
            router.ModifiedBy = currentUser.ApplicationUserID;

            router.RouterModules = ConvertHelper.ConvertToString(hidModuleID.Value, "");
            router.RouterInterfaces = ConvertHelper.ConvertToString(txtInterfaces.Text, "");
            router.RouterSiteToSites = ConvertHelper.ConvertToString(txtsitePass.Text, "");
            router.RouterNotes = ConvertHelper.ConvertToString(txtNotes.Text, "");

            //Upload functions

            if (fileUpload.HasFile)
            {
                fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                fileType = Path.GetFileName(fileUpload.PostedFile.ContentType);
                currenttime = DateTime.Now.ToString("yyyyMMddhhss");
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                SaveLocation = Server.MapPath("~/App_Data/Documents/Router/") + fileNameWithoutExtension + "_" + currenttime + extension;
                fileUpload.PostedFile.SaveAs(SaveLocation);
            }

            router.Documents = new Documents();
            router.Documents.Type = "ROUTER";
            router.Documents.DocumentPath = SaveLocation;
            router.Documents.DocumentName = fileName;
            router.Documents.DocumentType = fileType;

            if (ConvertHelper.ConvertToString(base.Id) != null)
                router.RouterID = ConvertHelper.ConvertToInteger(base.Id);

            serviceName = "SAVEROUTER";

            request = new PTRequest();
            request.Router = router;
            request.CurrentAction = CurrentAction;
            request.Router.Site = new Site();
            request.Router.Site.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);

            request.URL = string.Format(PostServiceURL + "{0}", serviceName);
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
                if (HiddenColorBox.Value == "0")
                {
                    CrudRouter.Visible = false;
                    divGrdRouterInfo.Visible = true;
                }
                else
                {
                    CrudRouter.Visible = false;
                    divGrdRouterInfo.Visible = false;
                    provClose.Visible = true;
                }
            }
            else
            {
                //ShowMessage(response.Message, response.isSuccess);
                CrudRouter.Visible = true;
                divGrdRouterInfo.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerInfo.aspx?do=v&nav=Routers", false);
    }

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
        {
            ShowMessage(ex.Message, false);
        }
    }


    #region [Router Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyRouter(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion
}