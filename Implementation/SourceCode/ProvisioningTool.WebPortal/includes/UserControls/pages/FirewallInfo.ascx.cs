using Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Newtonsoft.Json;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System.IO;

public partial class UserControlsFirewallInfo : UCController
{

    #region [ Variable Declarations ]

    PTResponse response;
    PTRequest request;
    Firewall firewall;
    WebServiceHelper webServiceHelper;
    string downloadpath = string.Empty;

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
            InitializeIframe(CrudFirewall, divGrdFirewallInfo);
            provClose.Visible = false;
            PopulateControls();
            if (CurrentAction == ActionType.Add)
            {
                CrudFirewall.Visible = true;
                divGrdFirewallInfo.Visible = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                DwnldLink.Visible = false;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                ModifyFirewall(base.Id);
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.View)
            {
                CrudFirewall.Visible = false;
                divGrdFirewallInfo.Visible = true;
            }
            else if (CurrentAction == ActionType.MoreView)
            {
                ModifyFirewall(base.Id);
                DisableControls(divFirewallDetail);
                divFirewallDetail.Attributes.Add("class", divFirewallDetail.Attributes["class"] + " viewPage");
                inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
                inlineInterface.Attributes.Add("class", inlineInterface.Attributes["class"] + " columnAlign");
                inlineSite.Attributes.Add("class", inlineSite.Attributes["class"] + " columnAlign");

                txtInterfaces.Attributes.Remove("class");
                txtNotes.Attributes.Remove("class");
                txtsitePass.Attributes.Remove("class");
                btnSubmit.Visible = false;
                btnBack.Visible = true;
                btnBack.Enabled = true;
                fileUpload.Visible = false;

            }
            else
            {
                CrudFirewall.Visible = false;
                divGrdFirewallInfo.Visible = true;

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Edit Firewall]
    private void ModifyFirewall(string firewallid)
    {
        try
        {
            CrudFirewall.Visible = true;
            divGrdFirewallInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(firewallid) != null)
            {
                serviceURL = PostServiceURL + "GETFIREWALLANDFIREWALLDETAILSBYFIREWALLID";
                request.Firewall = new Firewall();
                request.Firewall.FirewallID = ConvertHelper.ConvertToInteger(firewallid);
                hidEditID.Value = ConvertHelper.ConvertToString(firewallid);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.Firewall != null)
            {

                txtHostName.Text = ConvertHelper.ConvertToString(response.Firewall.Hostname);
                txtManufacture.Text = ConvertHelper.ConvertToString(response.Firewall.Manufacture);

                ddlModel.SelectedValue = ConvertHelper.ConvertToString(response.Firewall.FirewallModel != null ? ConvertHelper.ConvertToString(response.Firewall.FirewallModel.MasterDetailID) : "", "");
                txtMemory.Text = ConvertHelper.ConvertToString(response.Firewall.Memory);

                txtSerialNo.Text = ConvertHelper.ConvertToString(response.Firewall.SerialNumber);
                //txtInstalledDate.Text = ConvertHelper.ConvertToString(response.Firewall.InstalledOn == null ? string.Empty : response.Firewall.InstalledOn.ToString("MM-dd-yyyy"));
                //txtWarrantyExpires.Text = ConvertHelper.ConvertToString(response.Firewall.WarrantyExpiresOn == null ? string.Empty : response.Firewall.WarrantyExpiresOn.ToString("MM-dd-yyyy"));

                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.Firewall.InstalledOn).ToString("MM-dd-yyyy");
                txtWarrantyExpires.Text = ConvertHelper.ConvertToDateTime(response.Firewall.WarrantyExpiresOn).ToString("MM-dd-yyyy");

                txtIPAddress.Text = ConvertHelper.ConvertToString(response.Firewall.IPAddress);
                txtSubnet.Text = ConvertHelper.ConvertToString(response.Firewall.Subnet);
                txtGateway.Text = ConvertHelper.ConvertToString(response.Firewall.Gateway);
                txtAdminUsername.Text = ConvertHelper.ConvertToString(response.Firewall.AdminUserName);
                txtPassword.Text = ConvertHelper.ConvertToString(response.Firewall.AdminPassword);

                ddlOSVersion.SelectedValue = ConvertHelper.ConvertToString(response.Firewall.OSVersion != null ? ConvertHelper.ConvertToString(response.Firewall.OSVersion.MasterDetailID) : "", "");
                txtFirmware.Text = ConvertHelper.ConvertToString(response.Firewall.Firmware);

                if (response.Firewall.FirewallModuleList != null && response.Firewall.FirewallModuleList.Count > 0)
                    hidModuleID.Value = ConvertHelper.ConvertToString(response.Firewall.FirewallModuleList[0].FirewallModuleID);
                else
                    hidModuleID.Value = "";

                MultipleItemsSelectByValuesForDropdown(ddlModules, response.Firewall.FirewallModules, ',');

                txtInterfaces.Text = ConvertHelper.ConvertToString(response.Firewall.FirewallInterfaces.Replace(",", ";"));
                txtsitePass.Text = ConvertHelper.ConvertToString(response.Firewall.FirewallSiteToSites.Replace("|", ";"));
                txtNotes.Text = ConvertHelper.ConvertToString(response.Firewall.FirewallNotes.Replace("|", ";"));


                if (response.Firewall.ViewDocumentPath != null)
                {
                    downloadpath = ConvertHelper.ConvertToString(response.Firewall.ViewDocumentPath);
                    
                }
                else
                {
                    DwnldLink.Visible = false;
                    noimg.Visible = true;
                    noimg.Text = "No configuration file was uploaded.";
                }
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion

    #region [Add Firewall]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string serviceName = string.Empty;
            string currenttime;
            string fileName = string.Empty; string fileType = string.Empty;
            string SaveLocation = string.Empty;
            firewall = new Firewall();
            firewall.FirewallModel = new GlobalMasterDetail();
            firewall.OSVersion = new GlobalMasterDetail();

            firewall.Hostname = ConvertHelper.ConvertToString(txtHostName.Text, "");
            firewall.Manufacture = ConvertHelper.ConvertToString(txtManufacture.Text, "");
            firewall.FirewallModel.MasterDetailID = ConvertHelper.ConvertToInteger(ddlModel.SelectedValue);
            firewall.Memory = ConvertHelper.ConvertToString(txtMemory.Text, "");
            firewall.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text, "");
            firewall.InstalledOn = ConvertHelper.ConvertToString(txtInstalledDate.Text, "");
            firewall.WarrantyExpiresOn = ConvertHelper.ConvertToString(txtWarrantyExpires.Text, "");
            firewall.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text, "");
            firewall.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text, "");
            firewall.Gateway = ConvertHelper.ConvertToString(txtGateway.Text, "");
            firewall.AdminUserName = ConvertHelper.ConvertToString(txtAdminUsername.Text, "");
            firewall.AdminPassword = ConvertHelper.ConvertToString(txtPassword.Text, "");
            firewall.OSVersion.MasterDetailID = ConvertHelper.ConvertToInteger(ddlOSVersion.SelectedValue);
            firewall.Firmware = ConvertHelper.ConvertToString(txtFirmware.Text, "");

            firewall.FirewallModules = ConvertHelper.ConvertToString(hidModuleID.Value, "");
            firewall.FirewallInterfaces = ConvertHelper.ConvertToString(txtInterfaces.Text, "");
            firewall.FirewallSiteToSites = ConvertHelper.ConvertToString(txtsitePass.Text, "");
            //firewall.FirewallSiteToSites = ConvertHelper.ConvertToString(txtSitetoSiteCollection.Text, ""); 
            firewall.FirewallNotes = ConvertHelper.ConvertToString(txtNotes.Text, "");
            firewall.CreatedBy = currentUser.ApplicationUserID;
            firewall.ModifiedBy = currentUser.ApplicationUserID;

            //Upload functions

            if (fileUpload.HasFile)
            {
                fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                fileType = Path.GetFileName(fileUpload.PostedFile.ContentType); 
                currenttime = DateTime.Now.ToString("yyyyMMddhhss");
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                SaveLocation = Server.MapPath("~/App_Data/Documents/Firewall/") + fileNameWithoutExtension + "_" + currenttime + extension;
                fileUpload.PostedFile.SaveAs(SaveLocation);
            }

            firewall.Documents = new Documents();
            firewall.Documents.Type = "FIREWALL";
            firewall.Documents.DocumentPath = SaveLocation;
            firewall.Documents.DocumentName = fileName;
            firewall.Documents.DocumentType = fileType;

            if (base.Id != null)
                firewall.FirewallID = ConvertHelper.ConvertToInteger(base.Id);

            firewall.StatusID = 1;

            serviceName = "SAVEFIREWALL";
            request = new PTRequest();
            request.Firewall = firewall;
            request.Firewall.Site = new Site();
            request.Firewall.Site.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
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
                    CrudFirewall.Visible = false;
                    divGrdFirewallInfo.Visible = true;
                }
                else {
                    CrudFirewall.Visible = false;
                    divGrdFirewallInfo.Visible = false;
                    provClose.Visible = true;
                }
            }
            else
            {
                hidModuleID.Value = ConvertHelper.ConvertToString(request.Firewall.FirewallModules == null ? string.Empty : request.Firewall.FirewallModules);
                MultipleItemsSelectByValuesForDropdown(ddlModules, request.Firewall.FirewallModules == null ? string.Empty : request.Firewall.FirewallModules.Replace(",", ";"), ';');
                ShowMessage(response.Message, false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion

    #region [Back to Grid View Mode of Corresponding Fier Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudFirewall.Visible = false;
            divGrdFirewallInfo.Visible = true;
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
            request.GlobalMaster.MasterName = "Firewall Models";
            PopulateGlobalMasterDropdown(request, ddlModel, false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = "Firewall OS Versions";
            PopulateGlobalMasterDropdown(request, ddlOSVersion, false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = "Firewall Modules";
            PopulateGlobalMasterDropdown(request, ddlModules, false);
            ddlModules.Items.Insert(0, new ListItem(""));

            //Get All Firewall and Populate for Provisioning check list page
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLFIREWALLS/Firewall/0/dummytext";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.FirewallList != null && response.FirewallList.Count > 0)
            {
                PopulateFirewallDropDownList(ddldeviceList, response.FirewallList, true);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    #endregion

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
            else
            {
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion


    #region [Firewall Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyFirewall(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion
}