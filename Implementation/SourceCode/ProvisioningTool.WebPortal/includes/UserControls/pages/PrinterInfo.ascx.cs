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

public partial class UserControlsPrinterInfo : UCController
{

    #region [ Variable Declarations ]

    PTResponse response;
    PTRequest request;
    Printer printer;
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

            InitializeIframe(CrudPrinter, divGrdPrinterInfo);
            provClose.Visible = false;

            PopulateControls();
            if (CurrentAction == ActionType.Add)
            {
                CrudPrinter.Visible = true;
                divGrdPrinterInfo.Visible = false;
                btnPrinterSubmit.Visible = true;
                btnPrinterBack.Visible = true;
                DwnldLink.Visible = false;
            }
            else if (CurrentAction == ActionType.Edit)
            {

                ModifyPrinter(base.Id);
                btnPrinterSubmit.Visible = true;
                btnPrinterBack.Visible = true;
            }
            else if (CurrentAction == ActionType.View)
            {
                CrudPrinter.Visible = false;
                divGrdPrinterInfo.Visible = true;

            }
            else if (CurrentAction == ActionType.MoreView)
            {
                ModifyPrinter(base.Id);
                DisableControls(divPrinterDetail);
                divPrinterDetail.Attributes.Add("class", divPrinterDetail.Attributes["class"] + " viewPage");
                inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
                inlineInterface.Attributes.Add("class", inlineInterface.Attributes["class"] + " columnAlign");
                txtInterfaces.Attributes.Remove("class");
                txtNotes.Attributes.Remove("class");
                btnPrinterSubmit.Visible = false;
                btnPrinterBack.Visible = true;
                btnPrinterBack.Enabled = true;
                fileUpload.Visible = false;
            }
            else
            {
                CrudPrinter.Visible = false;
                divGrdPrinterInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get Printer Info and Bind the Controls For Edit And View]
    private void ModifyPrinter(string printerid)
    {
        try
        {
            CrudPrinter.Visible = true;
            divGrdPrinterInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(printerid) != null)
            {
                serviceURL = PostServiceURL + "GETPRINTERANDPRINTERDETAILSBYPRINTERID";
                request.Printer = new Printer();
                request.Printer.PrinterID = ConvertHelper.ConvertToInteger(printerid);
                hidEditID.Value = ConvertHelper.ConvertToString(printerid);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.Printer != null)
            {

                txtHostName.Text = ConvertHelper.ConvertToString(response.Printer.Hostname);
                txtHostName.ToolTip = ConvertHelper.ConvertToString(response.Printer.Hostname);

                txtManufacture.Text = ConvertHelper.ConvertToString(response.Printer.Manufacture);
                txtManufacture.ToolTip = ConvertHelper.ConvertToString(response.Printer.Manufacture);

                ddlModel.SelectedValue = ConvertHelper.ConvertToString(response.Printer.PrinterModel.MasterDetailID);
                ddlModel.ToolTip = ddlModel.SelectedItem.Text;

                txtSerialNo.Text = ConvertHelper.ConvertToString(response.Printer.SerialNumber);
                txtSerialNo.ToolTip = ConvertHelper.ConvertToString(response.Printer.SerialNumber);
                //txtInstalledDate.Text = ConvertHelper.ConvertToString(response.Printer.InstalledOn == null ? string.Empty : response.Printer.InstalledOn.ToString());
                //txtWarrantyExpires.Text = ConvertHelper.ConvertToString(response.Printer.WarrantyExpiresOn == null ? string.Empty : response.Printer.WarrantyExpiresOn.ToString());
                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.Printer.InstalledOn).ToString("MM-dd-yyyy");
                txtWarrantyExpires.Text = ConvertHelper.ConvertToDateTime(response.Printer.WarrantyExpiresOn).ToString("MM-dd-yyyy");
                txtIPAddress.Text = ConvertHelper.ConvertToString(response.Printer.IPAddress);
                txtSubnet.Text = ConvertHelper.ConvertToString(response.Printer.Subnet);
                txtGateway.Text = ConvertHelper.ConvertToString(response.Printer.Gateway);
                txtAdminUsername.Text = ConvertHelper.ConvertToString(response.Printer.AdminUserName);
                txtPassword.Text = ConvertHelper.ConvertToString(response.Printer.AdminPassword);
                txtPassword.ToolTip = ConvertHelper.ConvertToString(response.Printer.AdminPassword);

                ddlOSVersion.SelectedValue = ConvertHelper.ConvertToString(response.Printer.OSVersion.MasterDetailID);
                ddlOSVersion.ToolTip = ddlOSVersion.SelectedItem.Text;

                txtFirmware.Text = ConvertHelper.ConvertToString(response.Printer.Firmware);
                txtFirmware.ToolTip = ConvertHelper.ConvertToString(response.Printer.Firmware);

                hidModuleID.Value = ConvertHelper.ConvertToString(response.Printer.PrinterModules);
                MultipleItemsSelectByValuesForDropdown(ddlModules, response.Printer.PrinterModules, ',');
                hidAssignedUsers.Value = ConvertHelper.ConvertToString(response.Printer.AssignedUsers);
                MultipleItemsSelectByValuesForDropdown(ddlAssignedUsers, response.Printer.AssignedUsers.Replace("|", ";"), ';');

                txtInterfaces.Text = ConvertHelper.ConvertToString(response.Printer.PrinterInterfaces.Replace(",", ";"));
                txtInterfaces.ToolTip = ConvertHelper.ConvertToString(response.Printer.PrinterInterfaces.Replace(",", ";"));

                txtNotes.Text = ConvertHelper.ConvertToString(response.Printer.PrinterNotes.Replace("|", ";"));
                txtNotes.ToolTip = ConvertHelper.ConvertToString(response.Printer.PrinterNotes.Replace("|", ";"));

                if (response.Printer.ViewDocumentPath != null)
                {
                    downloadpath = ConvertHelper.ConvertToString(response.Printer.ViewDocumentPath);

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

    #region [Add Printer]

    protected void btnPrinterSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string serviceName = string.Empty;
            string currenttime = string.Empty;
            string fileName = string.Empty; string fileType = string.Empty;
            string SaveLocation = string.Empty;
            printer = new Printer();
            printer.PrinterModel = new GlobalMasterDetail();
            printer.OSVersion = new GlobalMasterDetail();

            printer.Hostname = ConvertHelper.ConvertToString(txtHostName.Text, "");
            printer.Manufacture = ConvertHelper.ConvertToString(txtManufacture.Text, "");
            printer.PrinterModel.MasterDetailID = ConvertHelper.ConvertToInteger(ddlModel.SelectedValue);

            printer.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text, "");
            printer.InstalledOn = ConvertHelper.ConvertToString(txtInstalledDate.Text);
            printer.WarrantyExpiresOn = ConvertHelper.ConvertToString(txtWarrantyExpires.Text);
            printer.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text, "");
            printer.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text, "");
            printer.Gateway = ConvertHelper.ConvertToString(txtGateway.Text, "");
            printer.AdminUserName = ConvertHelper.ConvertToString(txtAdminUsername.Text, "");
            printer.AdminPassword = ConvertHelper.ConvertToString(txtPassword.Text, "");
            printer.OSVersion.MasterDetailID = ConvertHelper.ConvertToInteger(ddlOSVersion.SelectedValue);
            printer.Firmware = ConvertHelper.ConvertToString(txtFirmware.Text, "");

            printer.PrinterModules = ConvertHelper.ConvertToString(hidModuleID.Value, "");
            printer.PrinterInterfaces = ConvertHelper.ConvertToString(txtInterfaces.Text, "");
            printer.AssignedUsers = ConvertHelper.ConvertToString(hidAssignedUsers.Value, "");
            //printer.PrinterSiteToSites = ConvertHelper.ConvertToString(txtsitePass.Text, "");
            //printer.PrinterSiteToSites = ConvertHelper.ConvertToString(txtSitetoSiteCollection.Text, ""); 
            printer.PrinterNotes = ConvertHelper.ConvertToString(txtNotes.Text, "");
            printer.CreatedBy = currentUser.ApplicationUserID;
            printer.ModifiedBy = currentUser.ApplicationUserID;

            //Upload functions

            if (fileUpload.HasFile)
            {
                fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                fileType = Path.GetFileName(fileUpload.PostedFile.ContentType); 
                currenttime = DateTime.Now.ToString("yyyyMMddhhss");
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                SaveLocation = Server.MapPath("~/App_Data/Documents/Printer/") + fileNameWithoutExtension + "_" + currenttime + extension;
                fileUpload.PostedFile.SaveAs(SaveLocation);
            }

            printer.Documents = new Documents();
            printer.Documents.Type = "PRINTER";
            printer.Documents.DocumentPath = SaveLocation;
            printer.Documents.DocumentName = fileName;
            printer.Documents.DocumentType = fileType;


            if (base.Id != null)
                printer.PrinterID = ConvertHelper.ConvertToInteger(base.Id);

            printer.StatusID = 1;

            serviceName = "SAVEPRINTER";
            request = new PTRequest();
            request.Printer = printer;
            request.Printer.Site = new Site();
            request.Printer.Site.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
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
                    CrudPrinter.Visible = false;
                    divGrdPrinterInfo.Visible = true;
                }
                else
                {
                    CrudPrinter.Visible = false;
                    divGrdPrinterInfo.Visible = false;
                    provClose.Visible = true;
                }
            }
            else
            {
                ShowMessage(response.Message, false);
                CrudPrinter.Visible = true;
                divGrdPrinterInfo.Visible = false;

                hidModuleID.Value = ConvertHelper.ConvertToString(request.Printer.PrinterModules == null ? string.Empty : request.Printer.PrinterModules);
                MultipleItemsSelectByValuesForDropdown(ddlModules, request.Printer.PrinterModules == null ? string.Empty : request.Printer.PrinterModules.Replace(",", ";"), ';');
                hidAssignedUsers.Value = ConvertHelper.ConvertToString(request.Printer.AssignedUsers == null ? string.Empty : request.Printer.AssignedUsers);
                MultipleItemsSelectByValuesForDropdown(ddlAssignedUsers, request.Printer.AssignedUsers == null ? string.Empty : request.Printer.AssignedUsers.Replace(",", ";"), ';');
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Back to Grid View Mode of Corresponding Fier Grid]
    protected void btnPrinterBack_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudPrinter.Visible = false;
            divGrdPrinterInfo.Visible = true;
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
            request.GlobalMaster.MasterName = GlobalPrinterModel;
            PopulateGlobalMasterDropdown(request, ddlModel, false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalPrinterOS;
            PopulateGlobalMasterDropdown(request, ddlOSVersion, false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalPrinterModule;
            PopulateGlobalMasterDropdown(request, ddlModules, false);
            ddlModules.Items.Insert(0, new ListItem(""));


            // Get All usres and populate
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
                PopulateUserDropDownList(ddlAssignedUsers, response.UserList, true);
            }

            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLPRINTERS/Mastername/0/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.PrinterList != null && response.PrinterList.Count > 0)
            {
                PopulatePrinterDropDownList(ddldeviceList, response.PrinterList, true);
            }

        }
        catch (Exception ex)
        { ShowMessage(ex.Message, false); }
    }


    #endregion

    #region Download
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
            ModifyPrinter(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion
}