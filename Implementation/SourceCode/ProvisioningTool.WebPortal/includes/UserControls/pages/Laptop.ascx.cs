using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;

public partial class UserControlsLaptopInfo : UCController
{
    #region [ Variable Declarations ]
    LaptopHardwareBLL serverHardwareBLL;
    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;
    //string baseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    // string masterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    #endregion [ Variable Declarations ]

    #region [ Page Load Event ]

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            divMessage.Attributes["style"] = "display:block";
            DetermineAction();

            if (!Page.IsPostBack && CurrentAction != ActionType.MoreView)
            {
                Page.Validate();
            }
            if (Request.QueryString["isColorBox"] != null)
            {
                btnBack.Style.Add("display", "none");
                btnSBack.Style.Add("display", "none");
            }
            if (Request.QueryString["HostName"] != null)
            {
                txtLaptopHost.Text = ConvertHelper.ConvertToString(Request.QueryString["HostName"], "");
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Page Load Event ]

    #region [ Determine Action ]

    private void DetermineAction()
    {
        try
        {
            InitializeIframe(CrudLaptop, divGrdLaptopInfo);
            provClose.Visible = false;
            if (CurrentAction == ActionType.Add)
            {
                PopulateControls();
                CheckLaptopHardWare();
                txtHostName.Enabled = true;
                txtLaptopHost.Enabled = true;
                divGrdLaptopInfo.Visible = false;
                divGrdLaptopHardwareInfo.Visible = false;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                PopulateControls();
                CheckLaptopHardWare();
                divGrdLaptopInfo.Visible = false;
                divGrdLaptopHardwareInfo.Visible = false;
            }
            else if (CurrentAction == ActionType.MoreView)
            {
                if (Request.QueryString["opp"] == "S")
                {
                    PopulateControls();
                    ModifyLaptop(base.Id);
                    txtBAWLK.Attributes.Remove("class");
                    txtLKWA.Attributes.Remove("class");
                    txtSNotes.Attributes.Remove("class");
                    lnkViewLaptopHardware.Visible = false;
                    lnkLaptopHardwware.Visible = false;
                    //DisableLaptopControls();
                    DisableControls(CrudLaptop);
                    CrudLaptop.Attributes.Add("class", CrudLaptop.Attributes["class"] + " viewPage");
                    //Add for 100% width in inlineProperty to show full text
                    backupApp.Attributes.Add("class", backupApp.Attributes["class"] + " columnAlign");
                    AppLicense.Attributes.Add("class", AppLicense.Attributes["class"] + " columnAlign");
                    inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
                    divGrdLaptopInfo.Visible = false;
                    divGrdLaptopHardwareInfo.Visible = false;
                    CrudLaptop.Visible = true;
                    CrudLaptopHardware.Visible = false;
                    btnSSubmit.Visible = false;
                    btnSBack.Visible = true;
                    btnSBack.Enabled = true;
                }
                else if (Request.QueryString["opp"] == "SH")
                {
                    PopulateControls();
                    ModifyLaptopHardware();
                    DisableControls(CrudLaptopHardware);
                    CrudLaptopHardware.Attributes.Add("class", CrudLaptopHardware.Attributes["class"] + " viewPage");
                    //DisableLaptopHardwareControls();
                    txtNotes.Attributes.Remove("class");
                    divGrdLaptopInfo.Visible = false;
                    divGrdLaptopHardwareInfo.Visible = false;
                    CrudLaptop.Visible = false;
                    CrudLaptopHardware.Visible = true;
                    btnSubmit.Visible = false;
                    btnBack.Visible = true;
                    btnBack.Enabled = true;
                }
            }
            else
            {
                if (Request.QueryString["opp"] == null)
                    CommonBack();
                else
                {
                    divGrdLaptopHardwareInfo.Visible = true;
                    divGrdLaptopInfo.Visible = false;
                    CrudLaptop.Visible = false;
                    CrudLaptopHardware.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    #endregion [ Determine Action ]

    #region [Get Laptop Hardware Info and Bind the Controls For Edit And View]

    private void ModifyLaptopHardware()
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETLAPTOPHARDWARANDUSERDETAILSBYLAPTOPHARDWARID";
                request.LaptopHardware = new LaptopHardware();
                request.LaptopHardware.LaptopHardwareID = ConvertHelper.ConvertToInteger(base.Id);
                hidEditID.Value = ConvertHelper.ConvertToString(base.Id);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.LaptopHardware != null)
            {
                txtLaptopHost.Text = ConvertHelper.ConvertToString(response.LaptopHardware.HostName);
                txtSHSer.Text = ConvertHelper.ConvertToString(response.LaptopHardware.SerialNumber);

                if (response.LaptopHardware.FullNotes != null)
                    txtNotes.Text = ConvertHelper.ConvertToString(response.LaptopHardware.FullNotes.Replace('|', ';'));

                txtModel.Text = ConvertHelper.ConvertToString(response.LaptopHardware.ModelName);
                ddlCPU.SelectedValue = ConvertHelper.ConvertToString(response.LaptopHardware.CPUID);
                ddlMotherboard.SelectedValue = ConvertHelper.ConvertToString(response.LaptopHardware.MotherBoardID);
                ddlChipset.SelectedValue = ConvertHelper.ConvertToString(response.LaptopHardware.ChipsetID);
                ddlChasis.SelectedValue = ConvertHelper.ConvertToString(response.LaptopHardware.ChassisID);
                txtCore.Text = ConvertHelper.ConvertToString(response.LaptopHardware.Core);
                txtManufacturer.Text = ConvertHelper.ConvertToString(response.LaptopHardware.Manufacturer);


                // BindMultiSelectDropDownWithSelectedValues(ddlMemory, hidmulddlMemory, response.LaptopHardware.MemoryIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlHardDrive, hidmulddlHardDrive, response.LaptopHardware.HardDriveIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlVideoCard, hidmulddlVideo, response.LaptopHardware.VideoCardIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlDisplay1, hidmulddlDisplay, response.LaptopHardware.DisplayIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlMultimedia, hidmulddlMultimedia, response.LaptopHardware.MultimediaIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlPorts, hidmulddlPort, response.LaptopHardware.PortIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlSlots, hidmulddlSlot, response.LaptopHardware.SlotIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlPower, hidmulddlPower, response.LaptopHardware.PowerIDs);

                List<SystemMemory> memoryList = new List<SystemMemory>();
                memoryList = response.LaptopHardware.Memorys;
                if (memoryList != null)
                {
                    string sMemoryList = "";
                    foreach (SystemMemory memory in memoryList)
                    {
                        if (sMemoryList == "")
                            sMemoryList = memory.Memory.MasterValue + ":" + ConvertHelper.ConvertToString(memory.Quantity, "") + ";";
                        else
                            sMemoryList = sMemoryList + memory.Memory.MasterValue + ":" + ConvertHelper.ConvertToString(memory.Quantity, "") + ";";
                    }
                    txtTotalMemoryQuantity.Text = sMemoryList;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    private void ModifyLaptop(string laptopid)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(laptopid) != null)
            {
                serviceURL = PostServiceURL + "GETLAPTOPINFOANDUSERDETAILSBYLAPTOPINFOID";
                request.LaptopInfo = new LaptopInfo();
                request.LaptopInfo.LaptopID = ConvertHelper.ConvertToInteger(laptopid);
                hidEditID.Value = ConvertHelper.ConvertToString(laptopid);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.LaptopInfo != null)
            {
                txtHostName.Text = response.LaptopInfo.HostName;
                txtHostName.ToolTip = response.LaptopInfo.HostName;

                txtSerialNo.Text = response.LaptopInfo.SerialNumber;
                txtSerialNo.ToolTip = response.LaptopInfo.SerialNumber;
                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.LaptopInfo.InstalledDate).ToString("MM-dd-yyyy");
                txtWEDate.Text = ConvertHelper.ConvertToDateTime(response.LaptopInfo.WarrantyExpires).ToString("MM-dd-yyyy");
                if (response.LaptopInfo.IPAddress == "DHCP")
                {
                    chkDHCP.Checked = true;
                    txtIPAddress.Attributes.Add("disabled", "disabled");
                }
                else
                    txtIPAddress.Text = response.LaptopInfo.IPAddress;

                txtSubnet.Text = response.LaptopInfo.Subnet;
                txtGateway.Text = response.LaptopInfo.Gateway;
                txtAUName.Text = response.LaptopInfo.AdminUserName;
                txtPassword.Text = response.LaptopInfo.Password;
                txtLKOS.Text = response.LaptopInfo.OperatingSystemLicenseKey;
                txtLKOS.ToolTip = response.LaptopInfo.OperatingSystemLicenseKey;
                txtLKAV.Text = response.LaptopInfo.AntiVirusLicenseKey;
                txtLKAV.ToolTip = response.LaptopInfo.AntiVirusLicenseKey;
                txtDomain.Text = response.LaptopInfo.Domain;

                ddlOS.SelectedValue = ConvertHelper.ConvertToString(response.LaptopInfo.OperatingSystemID);
                ddlAV.SelectedValue = ConvertHelper.ConvertToString(response.LaptopInfo.AntiVirusID);
                ddlLModel.SelectedValue = ConvertHelper.ConvertToString(response.LaptopInfo.LaptopModelID);

                if (response.LaptopInfo.FullNotes != null)
                {
                    txtSNotes.Text = response.LaptopInfo.FullNotes.Replace('|', ';');
                    txtSNotes.ToolTip = response.LaptopInfo.FullNotes.Replace('|', ';');
                }
                txtLKWA.Text = ConvertStringtoList(response.LaptopInfo.LaptopApplicationIDs, ddlApp);
                txtBAWLK.Text = ConvertStringtoList(response.LaptopInfo.LaptopBackupIDs, ddlBA);
                if (response.LaptopInfo.LaptopRoleIDs != null)
                    BindMultiSelectDropDownWithSelectedValues(ddlSRoles, hidmulddlSRoles, response.LaptopInfo.LaptopRoleIDs.Replace('|', ','));
                if (response.LaptopInfo.LaptopAssignedUserIDs != null)
                    BindMultiSelectDropDownWithSelectedValues(ddlAUsers, hidmulddlAUsers, response.LaptopInfo.LaptopAssignedUserIDs.Replace('|', ','));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [Get Laptop Hardware Info and Bind the Controls For Edit And View]

    #region [Populate Dropdowns]

    private void PopulateControls()
    {
        try
        {
            request = new PTRequest();
            request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

            // Dropdown for Laptop Hardware

            PopulateDropdown(ddlCPU, GlobalSystemHardwareCPU, true, false, false);
            PopulateDropdown(ddlMemory, GlobalSystemHardwareMemory, false, true, true);
            PopulateDropdown(ddlMotherboard, GlobalSystemHardwareMotherBoard, true, false, false);
            PopulateDropdown(ddlChipset, GlobalSystemHardwareChipSet, true, false, false);
            PopulateDropdown(ddlVideoCard, GlobalSystemHardwareVideoCard, false, true, false);
            PopulateDropdown(ddlDisplay1, GlobalSystemHardwareDisplay, false, true, false);
            PopulateDropdown(ddlMultimedia, GlobalSystemHardwareMultimedia, false, true, false);
            PopulateDropdown(ddlPorts, GlobalSystemHardwarePorts, false, true, false);
            PopulateDropdown(ddlSlots, GlobalSystemHardwareSlots, false, true, false);
            PopulateDropdown(ddlChasis, GlobalSystemHardwareChasis, true, false, false);
            PopulateDropdown(ddlPower, GlobalSystemHardwarePower, false, true, false);

            //Dropdown for Laptop Info
            PopulateDropdown(ddlSRoles, GlobalSystemRoles, false, true, false);
            PopulateDropdown(ddlApp, GlobalSystemApplicationSoftware, true, false, false);
            PopulateDropdown(ddlOS, GlobalSystemOS, true, false, false);
            PopulateDropdown(ddlBA, GlobalSystemBackupSoftware, true, false, false);
            PopulateDropdown(ddlAV, GloablSystemAntivirus, true, false, false);
            BindModelandUserDropdown();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    #endregion [Populate Dropdowns]

    #region [ Button Event ]

    #region [ Link Button Events ]

    protected void lnkLaptopHardwware_Click(object sender, EventArgs e)
    {
        try
        {
            CrudLaptop.Visible = false;
            CrudLaptopHardware.Visible = true;
            divGrdLaptopInfo.Visible = false;
            divGrdLaptopHardwareInfo.Visible = false;
            txtLaptopHost.Text = txtHostName.Text;
            txtLaptopHost.ReadOnly = true;
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        try
        {
            CrudLaptop.Visible = true;
            CrudLaptopHardware.Visible = false;
            divGrdLaptopInfo.Visible = false;
            divGrdLaptopHardwareInfo.Visible = false;
            EnableLaptopHardwareControls();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void lnkViewLaptopHardware_Click(object sender, EventArgs e)
    {
        try
        {
            CrudLaptop.Visible = false;
            CrudLaptopHardware.Visible = false;
            divGrdLaptopInfo.Visible = false;
            divGrdLaptopHardwareInfo.Visible = true;
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Link Button Events ]

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
            //serviceURL = BaseServiceURL + MasterServiceName;
            serviceURL = PostServiceURL;
            serviceName = "SAVELAPTOPHARDWARE";

            request.LaptopHardware = new LaptopHardware();

            request.LaptopHardware.Port = new GlobalMasterDetail();
            request.LaptopHardware.CPU = new GlobalMasterDetail();
            request.LaptopHardware.Memory = new GlobalMasterDetail();
            request.LaptopHardware.MotherBoard = new GlobalMasterDetail();
            request.LaptopHardware.HardDrive = new GlobalMasterDetail();
            request.LaptopHardware.Chipset = new GlobalMasterDetail();
            request.LaptopHardware.VideoCard = new GlobalMasterDetail();
            request.LaptopHardware.Display = new GlobalMasterDetail();
            request.LaptopHardware.Multimedia = new GlobalMasterDetail();
            request.LaptopHardware.Port = new GlobalMasterDetail();
            request.LaptopHardware.Slot = new GlobalMasterDetail();
            request.LaptopHardware.Chassis = new GlobalMasterDetail();
            request.LaptopHardware.Power = new GlobalMasterDetail();

            //Framing the URL
            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;
            request.LaptopHardware.HostName = ConvertHelper.ConvertToString(txtLaptopHost.Text);
            request.LaptopHardware.SerialNumber = ConvertHelper.ConvertToString(txtSHSer.Text);

            request.LaptopHardware.ModelName = ConvertHelper.ConvertToString(txtModel.Text);
            request.LaptopHardware.Core = ConvertHelper.ConvertToInteger(txtCore.Text);
            request.LaptopHardware.CPUID = ConvertHelper.ConvertToInteger(ddlCPU.SelectedItem.Value);
            request.LaptopHardware.MemoryIDs = ConvertHelper.ConvertToString(txtTotalMemoryQuantity.Text);
            request.LaptopHardware.MotherBoardID = ConvertHelper.ConvertToInteger(ddlMotherboard.SelectedItem.Value);
            request.LaptopHardware.HardDriveIDs = ConvertHelper.ConvertToString(hidmulddlHardDrive.Value);
            request.LaptopHardware.ChipsetID = ConvertHelper.ConvertToInteger(ddlChipset.SelectedItem.Value);
            request.LaptopHardware.VideoCardIDs = ConvertHelper.ConvertToString(hidmulddlVideo.Value);
            request.LaptopHardware.DisplayIDs = ConvertHelper.ConvertToString(hidmulddlDisplay.Value);
            request.LaptopHardware.MultimediaIDs = ConvertHelper.ConvertToString(hidmulddlMultimedia.Value);
            request.LaptopHardware.PortIDs = ConvertHelper.ConvertToString(hidmulddlPort.Value);
            request.LaptopHardware.SlotIDs = ConvertHelper.ConvertToString(hidmulddlSlot.Value);
            request.LaptopHardware.ChassisID = ConvertHelper.ConvertToInteger(ddlChasis.SelectedItem.Value);
            request.LaptopHardware.PowerIDs = ConvertHelper.ConvertToString(hidmulddlPower.Value);

            request.LaptopHardware.Manufacturer = ConvertHelper.ConvertToString(txtManufacturer.Text);

            if (txtNotes.Text != "" && txtNotes.Text != null)
                request.LaptopHardware.FullNotes = txtNotes.Text.Replace(',', '|');

            request.LaptopHardware.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.LaptopHardware.StatusID = 1;
            request.LaptopHardware.CreatedBy = currentUser.ApplicationUserID;
            request.LaptopHardware.ModifiedBy = currentUser.ApplicationUserID;
            request.LaptopHardware.CreatedOn = DateTime.Now;
            request.LaptopHardware.ModifiedOn = DateTime.Now;

            request.CurrentAction = CurrentAction;
            if (CurrentAction == ActionType.Edit)
            {
                request.LaptopHardware.LaptopHardwareID = ConvertHelper.ConvertToInteger(base.Id);
            }
            response = new PTResponse();
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
                if (response.isSuccess)
                {
                    //CrudLaptop.Visible = false;
                    //CrudLaptopHardware.Visible = false;
                    //divGrdLaptopInfo.Visible = false;
                    //divGrdLaptopHardwareInfo.Visible = true;
                    PopulateControls();
                    ClearAll();
                    CloseColorBox();
                }

            }
            else
            {
                ShowMessage("Error While Adding the Laptop Hardware", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    protected void btnSSubmit_Click(object sender, EventArgs e)
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
            //serviceURL = BaseServiceURL + MasterServiceName;
            serviceURL = PostServiceURL;
            serviceName = "SAVELAPTOPINFO";

            request.LaptopInfo = new LaptopInfo();


            //Framing the URL
            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;
            request.LaptopInfo.HostName = ConvertHelper.ConvertToString(txtHostName.Text);
            request.LaptopInfo.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text);
            if (chkDHCP.Checked)
                request.LaptopInfo.IPAddress = "DHCP";
            else
                request.LaptopInfo.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text);
            request.LaptopInfo.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text);
            request.LaptopInfo.Gateway = ConvertHelper.ConvertToString(txtGateway.Text);
            request.LaptopInfo.AdminUserName = ConvertHelper.ConvertToString(txtAUName.Text);
            request.LaptopInfo.Password = ConvertHelper.ConvertToString(txtPassword.Text);
            request.LaptopInfo.Domain = ConvertHelper.ConvertToString(txtDomain.Text);
            request.LaptopInfo.OperatingSystemLicenseKey = ConvertHelper.ConvertToString(txtLKOS.Text);
            request.LaptopInfo.AntiVirusLicenseKey = ConvertHelper.ConvertToString(txtLKAV.Text);

            request.LaptopInfo.InstalledDate = ConvertHelper.ConvertToString(txtInstalledDate.Text);
            request.LaptopInfo.WarrantyExpires = ConvertHelper.ConvertToString(txtWEDate.Text);

            request.LaptopInfo.LaptopModelID = ConvertHelper.ConvertToInteger(ddlLModel.SelectedItem.Value);

            request.LaptopInfo.LaptopRoleIDs = ConvertHelper.ConvertToString(hidmulddlSRoles.Value);
            request.LaptopInfo.OperatingSystemID = ConvertHelper.ConvertToInteger(ddlOS.SelectedItem.Value);
            request.LaptopInfo.AntiVirusID = ConvertHelper.ConvertToInteger(ddlAV.SelectedItem.Value);

            request.LaptopInfo.LaptopAssignedUserIDs = ConvertHelper.ConvertToString(hidmulddlAUsers.Value);

            if (txtBAWLK.Text != "" || txtBAWLK.Text != null)
                request.LaptopInfo.LaptopBackupIDs = ConvertListType(ddlBA, txtBAWLK.Text.Replace(';', ','));

            if (txtLKWA.Text != "" || txtLKWA.Text != null)
                request.LaptopInfo.LaptopApplicationIDs = ConvertListType(ddlApp, txtLKWA.Text.Replace(';', ','));

            if (txtSNotes.Text != "" || txtSNotes.Text != null)
                request.LaptopInfo.FullNotes = txtSNotes.Text.Replace(';', '|');

            request.LaptopInfo.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.LaptopInfo.StatusID = 1;
            request.LaptopInfo.CreatedBy = currentUser.ApplicationUserID;
            request.LaptopInfo.ModifiedBy = currentUser.ApplicationUserID;
            request.LaptopInfo.CreatedOn = DateTime.Now;
            request.LaptopInfo.ModifiedOn = DateTime.Now;

            request.CurrentAction = CurrentAction;
            if (CurrentAction == ActionType.Edit)
            {
                request.LaptopInfo.LaptopID = ConvertHelper.ConvertToInteger(base.Id);
            }
            response = new PTResponse();
            //response = webServiceHelper.PostRequest(request);
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
                if (response.isSuccess)
                {
                    if (HiddenColorBox.Value == "0")
                    {
                        CrudLaptop.Visible = false;
                        CrudLaptopHardware.Visible = false;
                        divGrdLaptopInfo.Visible = true;
                        divGrdLaptopHardwareInfo.Visible = false;
                        ClearAll();
                        //if (Request.QueryString["isColorBox"] != null)
                        //    CloseColorBox();
                    }
                    else
                    {
                        CrudLaptop.Visible = false;
                        CrudLaptopHardware.Visible = false;
                        divGrdLaptopInfo.Visible = false;
                        divGrdLaptopHardwareInfo.Visible = false;
                        provClose.Visible = true;
                    }
                }
                else
                {
                    CrudLaptop.Visible = true;
                    CrudLaptopHardware.Visible = false;
                    divGrdLaptopInfo.Visible = false;
                    divGrdLaptopHardwareInfo.Visible = false;
                }
            }
            else
            {
                ShowMessage("Error While Adding the Laptop Info", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnSBack_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            CrudLaptopHardware.Visible = false;
            CrudLaptop.Visible = false;
            divGrdLaptopInfo.Visible = true;
            divGrdLaptopHardwareInfo.Visible = false;
            btnSSubmit.Visible = true;
            btnSBack.Visible = false;
            EnableLaptopControls();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            CrudLaptopHardware.Visible = false;
            CrudLaptop.Visible = false;
            divGrdLaptopInfo.Visible = false;
            divGrdLaptopHardwareInfo.Visible = true;
            btnSubmit.Visible = true;
            btnBack.Visible = false;
            EnableLaptopHardwareControls();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Button Event ]

    #region [Privte Function]

    private void CommonBack()
    {
        try
        {
            CrudLaptop.Visible = false;
            CrudLaptopHardware.Visible = false;
            divGrdLaptopInfo.Visible = true;
            divGrdLaptopHardwareInfo.Visible = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void CheckLaptopHardWare()
    {
        try
        {
            if (Request.QueryString["opp"] != null)
            {
                if (Request.QueryString["opp"] == "S")
                {
                    CrudLaptop.Visible = true;
                    CrudLaptopHardware.Visible = false;
                    if (Request.QueryString["id"] != null && txtHostName.Text == "")
                    {
                        ModifyLaptop(base.Id);
                        //txtHostName.Enabled = false;
                    }
                }
                else
                {
                    CrudLaptop.Visible = false;
                    CrudLaptopHardware.Visible = true;
                    if (Request.QueryString["id"] != null && txtSHSer.Text == "")
                    {
                        ModifyLaptopHardware();
                        //txtLaptopHost.Enabled = false;
                    }
                }
            }
            else
            {
                CrudLaptop.Visible = false;
                CrudLaptopHardware.Visible = false;

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void PopulateDropdown(DropDownList dropDownList, string sMasterName, bool includeSelect, bool IsMultiSelect, bool activateResult)
    {
        try
        {
            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = sMasterName;
            PopulateGlobalMasterDropdown(request, dropDownList, includeSelect, activateResult);
            if (IsMultiSelect)
                dropDownList.Items.Insert(0, new ListItem(""));
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void BindMultiSelectDropdownWithTextBox(DropDownList ddl, HiddenField hid, TextBox txt, string sValue)
    {
        try
        {
            if (sValue != string.Empty)
            {
                string[] sSplit = sValue.Split('|');
                BindMultiSelectDropDownWithSelectedValues(ddl, hid, sSplit[0]);
                if (sSplit[1] != null)
                    txt.Text = sSplit[1].Replace(',', ';');
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #region [Bind Model and User ]

    private void BindModelandUserDropdown()
    {
        try
        {
            #region [GE ALL USERS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
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

            #region [GE ALL HARDWARE AND POPULATE]

            serviceURL = PostServiceURL + "POPULATELAPTOPHARDWARES";
            request.LaptopHardware = new LaptopHardware();
            request.LaptopHardware.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.LaptopHardwareList != null && response.LaptopHardwareList.Count > 0)
            {
                PopulateLaptopModelDropDownList(ddlLModel, response.LaptopHardwareList, true);
            }
            #endregion [GE ALL HARDWARE AND POPULATE]

            #region [GE ALL HARDWARE Disk Detail]

            serviceURL = PostServiceURL + "POPULATEHARDDISK";
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.SystemHardDriveList != null && response.SystemHardDriveList.Count > 0)
            {
                PopulateHardDiskDropDownList(ddlHardDrive, response.SystemHardDriveList, true);
            }
            #endregion [GE ALL HARDWARE Disk Detail]

            #region [GE ALL Laptops AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLLAPTOPINFO/Mastername/0/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.LaptopInfoList != null && response.LaptopInfoList.Count > 0)
            {
                PopulateLaptopDropDownList(ddldeviceList, response.LaptopInfoList, true);
            }
            #endregion
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }


    }

    #endregion [Bind Model and User ]

    #region [Bind MultiSelect DropDown With Selected Values]

    public void BindMultiSelectDropDownWithSelectedValues(DropDownList ddlMulSelectAttribute, HiddenField hidMulSelectDdl, string multipleVal)
    {
        try
        {
            if (multipleVal != null && multipleVal != "")
            {
                string sMemory = multipleVal;
                ArrayList arrMemory = new ArrayList(); ;
                arrMemory.AddRange(sMemory.Split(new char[] { ',' }));
                foreach (string s in arrMemory)
                {
                    ddlMulSelectAttribute.Items.FindByValue(s).Attributes.Add("selected", "selected");
                    if (hidMulSelectDdl.Value == "")
                        hidMulSelectDdl.Value = ConvertHelper.ConvertToString(s);
                    else
                        hidMulSelectDdl.Value = ConvertHelper.ConvertToString(hidMulSelectDdl.Value) + "," + ConvertHelper.ConvertToString(s);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [Bind MultiSelect DropDown With Selected Values]

    private string ConvertListType(DropDownList ddl, string sKeyList)
    {

        string sFullstring;
        sFullstring = string.Empty;
        try
        {
            if (sKeyList != null && sKeyList != "")
            {
                string[] sTempKey;
                string[] sKeyLists = sKeyList.Split(',');

                for (int i = 0; i < sKeyLists.Length; i++)
                {
                    sTempKey = sKeyLists[i].Split(':');
                    if (sFullstring == string.Empty)
                        sFullstring = ConvertHelper.ConvertToString(ddl.Items.FindByText(sTempKey[0]).Value) + "|" + ConvertHelper.ConvertToString(sTempKey[1]);
                    else
                        sFullstring = sFullstring + "," + ConvertHelper.ConvertToString(ddl.Items.FindByText(sTempKey[0]).Value) + "|" + ConvertHelper.ConvertToString(sTempKey[1]);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        return sFullstring;
    }

    private string ConvertStringtoList(string sListKey, DropDownList ddl)
    {
        string sFullstring, sList, sKeyList;
        sFullstring = sList = sKeyList = string.Empty;
        try
        {
            if (sListKey != null && sListKey != "")
            {
                string[] sLists = sListKey.Split('|');
                for (int i = 0; i < sLists.Length; i++)
                {
                    if (sList == string.Empty)
                    {
                        sList = ConvertHelper.ConvertToString(ddl.Items.FindByValue(sLists[i]).Text) + ":" + sLists[i + 1];
                        // sKeyList = ;
                    }
                    else
                    {
                        sList = sList + ";" + ConvertHelper.ConvertToString(ddl.Items.FindByValue(sLists[i]).Text) + ":" + sLists[i + 1]; ;
                        //sKeyList = sKeyList + "," + sLists[i + 1];
                    }
                    i++;
                }
                sFullstring = sList;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        return sFullstring;

    }

    #region [Laptop Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyLaptop(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion


    #region [ Clear, Enable and Disabling the Controls ]

    //Clear All controls Values
    private void ClearAll()
    {
        try
        {
            txtAUName.Text = String.Empty;
            txtCore.Text = String.Empty;
            txtDomain.Text = String.Empty;
            txtGateway.Text = String.Empty;
            txtHostName.Text = String.Empty;
            txtInstalledDate.Text = String.Empty;
            txtIPAddress.Text = String.Empty;
            txtLKApp.Text = String.Empty;
            txtLKAV.Text = String.Empty;
            txtLKBA.Text = String.Empty;
            txtLKOS.Text = String.Empty;
            txtManufacturer.Text = String.Empty;
            txtModel.Text = String.Empty;
            txtNotes.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtSerialNo.Text = String.Empty;
            txtLaptopHost.Text = String.Empty;
            txtSHSer.Text = String.Empty;
            txtSNotes.Text = String.Empty;
            txtSubnet.Text = String.Empty;
            txtWEDate.Text = String.Empty;
            txtLaptopHost.ReadOnly = false;
            ddlApp.SelectedIndex = -1;
            ddlAUsers.SelectedIndex = -1;
            ddlAV.SelectedIndex = -1;
            ddlBA.SelectedIndex = -1;
            ddlChasis.SelectedIndex = -1;
            ddlChipset.SelectedIndex = -1;
            ddlCPU.SelectedIndex = -1;
            ddlDisplay1.SelectedIndex = -1;
            ddlHardDrive.SelectedIndex = -1;
            ddlMemory.SelectedIndex = -1;
            ddlLModel.SelectedIndex = -1;
            ddlMotherboard.SelectedIndex = -1;
            ddlMultimedia.SelectedIndex = -1;
            ddlOS.SelectedIndex = -1;
            ddlPorts.SelectedIndex = -1;
            ddlPower.SelectedIndex = -1;
            ddlSlots.SelectedIndex = -1;
            ddlSRoles.SelectedIndex = -1;
            ddlVideoCard.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //Disable Laptop Controls
    private void DisableLaptopControls()
    {
        try
        {
            txtHostName.ReadOnly = true;
            ddlLModel.Enabled = false;
            txtSerialNo.ReadOnly = true;
            txtIPAddress.ReadOnly = true;
            txtSubnet.ReadOnly = true;
            txtGateway.ReadOnly = true;
            txtInstalledDate.ReadOnly = true;
            txtWEDate.ReadOnly = true;
            txtAUName.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtDomain.ReadOnly = true;
            ddlOS.Enabled = false;
            txtLKOS.ReadOnly = true;
            ddlAV.Enabled = false;
            txtLKAV.ReadOnly = true;
            ddlBA.Enabled = false;
            txtLKBA.ReadOnly = true;
            btnAdd.Enabled = false;
            ddlApp.Enabled = false;
            txtLKApp.ReadOnly = true;
            btnAddAPP.Enabled = false;
            txtLKWA.ReadOnly = true;
            txtBAWLK.ReadOnly = true;
            ddlSRoles.Enabled = false;
            ddlAUsers.Enabled = false;
            txtSNotes.ReadOnly = true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //Enable Laptop Controls
    private void EnableLaptopControls()
    {
        try
        {
            txtHostName.ReadOnly = false;
            ddlLModel.Enabled = true;
            txtSerialNo.ReadOnly = false;
            txtIPAddress.ReadOnly = false;
            txtSubnet.ReadOnly = false;
            txtGateway.ReadOnly = false;
            txtInstalledDate.ReadOnly = false;
            txtWEDate.ReadOnly = false;
            txtAUName.ReadOnly = false;
            txtPassword.ReadOnly = false;
            txtDomain.ReadOnly = false;
            ddlOS.Enabled = true;
            txtLKOS.ReadOnly = false;
            ddlAV.Enabled = true;
            txtLKAV.ReadOnly = false;
            ddlBA.Enabled = true;
            txtLKBA.ReadOnly = false;
            btnAdd.Enabled = true;
            ddlApp.Enabled = true;
            txtLKApp.ReadOnly = false;
            btnAddAPP.Enabled = true;
            txtLKWA.ReadOnly = false;
            txtBAWLK.ReadOnly = false;
            ddlSRoles.Enabled = true;
            ddlAUsers.Enabled = true;
            txtSNotes.ReadOnly = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //Disable Laptop Hardware Controls
    private void DisableLaptopHardwareControls()
    {
        try
        {
            txtLaptopHost.ReadOnly = true;
            txtModel.ReadOnly = true;
            txtSHSer.ReadOnly = true;
            txtCore.ReadOnly = true;
            ddlMemory.Enabled = false;
            ddlCPU.Enabled = false;
            ddlHardDrive.Enabled = false;
            ddlMotherboard.Enabled = false;
            ddlChipset.Enabled = false;
            ddlMultimedia.Enabled = false;
            ddlPorts.Enabled = false;
            ddlVideoCard.Enabled = false;
            ddlDisplay1.Enabled = false;
            ddlSlots.Enabled = false;
            ddlPower.Enabled = false;
            ddlChasis.Enabled = false;
            txtManufacturer.ReadOnly = true;
            txtNotes.ReadOnly = true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //Enable Laptop Hardware Controls
    private void EnableLaptopHardwareControls()
    {
        try
        {
            txtLaptopHost.ReadOnly = false;
            txtModel.ReadOnly = false;
            txtSHSer.ReadOnly = false;
            txtCore.ReadOnly = false;
            ddlMemory.Enabled = true;
            ddlCPU.Enabled = true;
            ddlHardDrive.Enabled = true;
            ddlMotherboard.Enabled = true;
            ddlChipset.Enabled = true;
            ddlMultimedia.Enabled = true;
            ddlPorts.Enabled = true;
            ddlVideoCard.Enabled = true;
            ddlDisplay1.Enabled = true;
            ddlSlots.Enabled = true;
            ddlPower.Enabled = true;
            ddlChasis.Enabled = true;
            txtManufacturer.ReadOnly = false;
            txtNotes.ReadOnly = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Clear, Enable and Disabling the Controls ]

    #endregion  [Privte Function]


}