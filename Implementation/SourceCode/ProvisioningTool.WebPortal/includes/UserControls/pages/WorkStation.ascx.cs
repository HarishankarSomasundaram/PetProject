using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;

public partial class UserControlsWorkStationInfo : UCController
{
    #region [ Variable Declarations ]
    WorkStationHardwareBLL serverHardwareBLL;
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
            if (!Page.IsPostBack && CurrentAction != ActionType.MoreView) { Page.Validate(); }

            if (Request.QueryString["isColorBox"] != null)
            {
                btnBack.Style.Add("display", "none");
                btnSBack.Style.Add("display", "none");

            }

            if (Request.QueryString["HostName"] != null)
            {
                txtWorkStationHost.Text = ConvertHelper.ConvertToString(Request.QueryString["HostName"], "");
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
            InitializeIframe(CrudWorkStation, divGrdWorkStationInfo);
            provClose.Visible = false;

            if (CurrentAction == ActionType.Add)
            {
                PopulateControls();
                CheckWorkStationHardWare();
                txtHostName.Enabled = true;
                txtWorkStationHost.Enabled = true;
                divGrdWorkStationInfo.Visible = false;
                divGrdWorkStationHardwareInfo.Visible = false;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                PopulateControls();
                CheckWorkStationHardWare();
                divGrdWorkStationInfo.Visible = false;
                divGrdWorkStationHardwareInfo.Visible = false;
            }
            else if (CurrentAction == ActionType.MoreView)
            {
                if (Request.QueryString["opp"] == "S")
                {
                    PopulateControls();
                    ModifyWorkStation(base.Id);
                    //DisableWorkStationControls();
                    DisableControls(CrudWorkStation);
                    //lnkWorkStationHardwware.Visible = false;
                    //lnkViewWorkStationHardware.Visible = false;
                    txtBAWLK.Attributes.Remove("class");
                    txtLKWA.Attributes.Remove("class");
                    txtSNotes.Attributes.Remove("class");
                    divGrdWorkStationInfo.Visible = false;
                    divGrdWorkStationHardwareInfo.Visible = false;
                    CrudWorkStation.Visible = true;
                    CrudWorkStationHardware.Visible = false;
                    btnSSubmit.Visible = false;
                    btnSBack.Visible = true;
                    btnSBack.Enabled = true;
                    CrudWorkStation.Attributes.Add("class", CrudWorkStation.Attributes["class"] + " viewPage");
                    //Add for 100% width in inlineProperty to show full text
                    backupApp.Attributes.Add("class", backupApp.Attributes["class"] + " columnAlign");
                    AppLicense.Attributes.Add("class", AppLicense.Attributes["class"] + " columnAlign");
                    inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");

                }
                else if (Request.QueryString["opp"] == "SH")
                {
                    PopulateControls();
                    ModifyWorkStationHardware();
                    //DisableWorkStationHardwareControls();
                    DisableControls(CrudWorkStationHardware);
                    txtNotes.Attributes.Remove("class");
                    divGrdWorkStationInfo.Visible = false;
                    divGrdWorkStationHardwareInfo.Visible = false;
                    CrudWorkStation.Visible = false;
                    CrudWorkStationHardware.Visible = true;
                    btnSubmit.Visible = false;
                    btnBack.Visible = true;
                    btnBack.Enabled = true;
                    CrudWorkStationHardware.Attributes.Add("class", CrudWorkStationHardware.Attributes["class"] + " viewPage");
                }
            }
            else
            {
                if (Request.QueryString["opp"] == null)
                    CommonBack();
                else
                {
                    divGrdWorkStationInfo.Visible = false;
                    divGrdWorkStationHardwareInfo.Visible = true;
                    CrudWorkStation.Visible = false;
                    CrudWorkStationHardware.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    #endregion [ Determine Action ]

    #region [Get WorkStation Hardware Info and Bind the Controls For Edit And View]

    private void ModifyWorkStationHardware()
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
                serviceURL = PostServiceURL + "GETWORKSTATIONHARDWARANDUSERDETAILSBYWORKSTATIONHARDWARID";
                request.WorkStationHardware = new WorkStationHardware();
                request.WorkStationHardware.WorkStationHardwareID = ConvertHelper.ConvertToInteger(base.Id);
                hidEditID.Value = ConvertHelper.ConvertToString(base.Id);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.WorkStationHardware != null)
            {
                txtWorkStationHost.Text = ConvertHelper.ConvertToString(response.WorkStationHardware.HostName);
                txtSHSer.Text = ConvertHelper.ConvertToString(response.WorkStationHardware.SerialNumber);

                if (response.WorkStationHardware.FullNotes != null)
                    txtNotes.Text = ConvertHelper.ConvertToString(response.WorkStationHardware.FullNotes.Replace('|', ';'));

                txtModel.Text = ConvertHelper.ConvertToString(response.WorkStationHardware.ModelName);
                ddlCPU.SelectedValue = ConvertHelper.ConvertToString(response.WorkStationHardware.CPUID);
                ddlMotherboard.SelectedValue = ConvertHelper.ConvertToString(response.WorkStationHardware.MotherBoardID);
                ddlChipset.SelectedValue = ConvertHelper.ConvertToString(response.WorkStationHardware.ChipsetID);
                ddlChasis.SelectedValue = ConvertHelper.ConvertToString(response.WorkStationHardware.ChassisID);
                txtCore.Text = ConvertHelper.ConvertToString(response.WorkStationHardware.Core);
                txtManufacturer.Text = ConvertHelper.ConvertToString(response.WorkStationHardware.Manufacturer);


                // BindMultiSelectDropDownWithSelectedValues(ddlMemory, hidmulddlMemory, response.WorkStationHardware.MemoryIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlHardDrive, hidmulddlHardDrive, response.WorkStationHardware.HardDriveIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlVideoCard, hidmulddlVideo, response.WorkStationHardware.VideoCardIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlDisplay1, hidmulddlDisplay, response.WorkStationHardware.DisplayIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlMultimedia, hidmulddlMultimedia, response.WorkStationHardware.MultimediaIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlPorts, hidmulddlPort, response.WorkStationHardware.PortIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlSlots, hidmulddlSlot, response.WorkStationHardware.SlotIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlPower, hidmulddlPower, response.WorkStationHardware.PowerIDs);

                List<SystemMemory> memoryList = new List<SystemMemory>();
                memoryList = response.WorkStationHardware.Memorys;
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

    private void ModifyWorkStation(string workstationId)
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

            if (ConvertHelper.ConvertToString(workstationId) != null)
            {
                serviceURL = PostServiceURL + "GETWORKSTATIONINFOANDUSERDETAILSBYWORKSTATIONINFOID";
                request.WorkStationInfo = new WorkStationInfo();
                request.WorkStationInfo.WorkStationID = ConvertHelper.ConvertToInteger(workstationId);
                hidEditID.Value = ConvertHelper.ConvertToString(workstationId);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.WorkStationInfo != null)
            {
                txtHostName.Text = response.WorkStationInfo.HostName;
                txtHostName.ToolTip = response.WorkStationInfo.HostName;

                txtSerialNo.Text = response.WorkStationInfo.SerialNumber;
                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.WorkStationInfo.InstalledDate).ToString("MM-dd-yyyy");
                txtWEDate.Text = ConvertHelper.ConvertToDateTime(response.WorkStationInfo.WarrantyExpires).ToString("MM-dd-yyyy");
                if (response.WorkStationInfo.IPAddress == "DHCP")
                {
                    chkDHCP.Checked = true;
                    txtIPAddress.Attributes.Add("disabled", "disabled");
                }
                else
                    txtIPAddress.Text = response.WorkStationInfo.IPAddress;

                txtSubnet.Text = response.WorkStationInfo.Subnet;
                txtGateway.Text = response.WorkStationInfo.Gateway;
                txtAUName.Text = response.WorkStationInfo.AdminUserName;
                txtAUName.ToolTip = response.WorkStationInfo.AdminUserName;

                txtPassword.Text = response.WorkStationInfo.Password;
                txtLKOS.Text = response.WorkStationInfo.OperatingSystemLicenseKey;
                txtLKAV.Text = response.WorkStationInfo.AntiVirusLicenseKey;
                txtDomain.Text = response.WorkStationInfo.Domain;

                ddlOS.SelectedValue = ConvertHelper.ConvertToString(response.WorkStationInfo.OperatingSystemID);
                ddlOS.ToolTip = ddlOS.SelectedItem.Text;

                ddlAV.SelectedValue = ConvertHelper.ConvertToString(response.WorkStationInfo.AntiVirusID);
                ddlAV.ToolTip = ddlAV.SelectedItem.Text;
                ddlWModel.SelectedValue = ConvertHelper.ConvertToString(response.WorkStationInfo.WorkStationModelID);

                if (response.WorkStationInfo.FullNotes != null)
                {
                    txtSNotes.Text = response.WorkStationInfo.FullNotes.Replace('|', ';');
                    txtSNotes.ToolTip = response.WorkStationInfo.FullNotes.Replace('|', ';');
                }

                if (response.WorkStationInfo.WorkStationRoleIDs != null)
                    BindMultiSelectDropDownWithSelectedValues(ddlSRoles, hidmulddlSRoles, response.WorkStationInfo.WorkStationRoleIDs.Replace('|', ','));
                if (response.WorkStationInfo.WorkStationAssignedUserIDs != null)
                    BindMultiSelectDropDownWithSelectedValues(ddlAUsers, hidmulddlAUsers, response.WorkStationInfo.WorkStationAssignedUserIDs.Replace('|', ','));

                txtLKWA.Text = ConvertStringtoList(response.WorkStationInfo.WorkStationApplicationIDs, ddlApp);
                txtLKWA.ToolTip = ConvertStringtoList(response.WorkStationInfo.WorkStationApplicationIDs, ddlApp);
                txtBAWLK.Text = ConvertStringtoList(response.WorkStationInfo.WorkStationBackupIDs, ddlBA);
                txtBAWLK.ToolTip = ConvertStringtoList(response.WorkStationInfo.WorkStationBackupIDs, ddlBA);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [Get WorkStation Hardware Info and Bind the Controls For Edit And View]

    #region [Populate Dropdowns]

    private void PopulateControls()
    {
        try
        {
            request = new PTRequest();
            request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

            // Dropdown for WorkStation Hardware ddlWManufacturer
            PopulateDropdown(ddlWManufacturer, GlobalWorkstationManufacturesModule, true, false, false);
            PopulateDropdown(ddlWType, GlobalWorkstationTypesModule, true, false, false);
            PopulateDropdown(ddlWModel, GlobalWorkstationModelsModule, true, false, false);
            
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

            //Dropdown for WorkStation Info
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

    #region [ Button Events ]

    #region [ Link Button Events ]

    protected void lnkWorkStationHardwware_Click(object sender, EventArgs e)
    {
        try
        {
            //CrudWorkStation.Visible = false;
            //CrudWorkStationHardware.Visible = true;
            //divGrdWorkStationInfo.Visible = false;
            //divGrdWorkStationHardwareInfo.Visible = false;
            //txtWorkStationHost.Text = txtHostName.Text;
            //txtWorkStationHost.ReadOnly = true;
            //ShowMessage("", false);
            //CurrentAction = ActionType.Add;
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
            CrudWorkStation.Visible = true;
            CrudWorkStationHardware.Visible = false;
            divGrdWorkStationInfo.Visible = false;
            divGrdWorkStationHardwareInfo.Visible = false;
            EnableWorkStationHardwareControls();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void lnkViewWorkStationHardware_Click(object sender, EventArgs e)
    {
        try
        {
            CrudWorkStation.Visible = false;
            CrudWorkStationHardware.Visible = false;
            divGrdWorkStationInfo.Visible = false;
            divGrdWorkStationHardwareInfo.Visible = true;
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
            serviceName = "SAVEWORKSTATIONHARDWARE";

            request.WorkStationHardware = new WorkStationHardware();

            request.WorkStationHardware.Port = new GlobalMasterDetail();
            request.WorkStationHardware.CPU = new GlobalMasterDetail();
            request.WorkStationHardware.Memory = new GlobalMasterDetail();
            request.WorkStationHardware.MotherBoard = new GlobalMasterDetail();
            request.WorkStationHardware.HardDrive = new GlobalMasterDetail();
            request.WorkStationHardware.Chipset = new GlobalMasterDetail();
            request.WorkStationHardware.VideoCard = new GlobalMasterDetail();
            request.WorkStationHardware.Display = new GlobalMasterDetail();
            request.WorkStationHardware.Multimedia = new GlobalMasterDetail();
            request.WorkStationHardware.Port = new GlobalMasterDetail();
            request.WorkStationHardware.Slot = new GlobalMasterDetail();
            request.WorkStationHardware.Chassis = new GlobalMasterDetail();
            request.WorkStationHardware.Power = new GlobalMasterDetail();

            //Framing the URL
            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;
            request.WorkStationHardware.HostName = ConvertHelper.ConvertToString(txtWorkStationHost.Text);
            request.WorkStationHardware.SerialNumber = ConvertHelper.ConvertToString(txtSHSer.Text);

            request.WorkStationHardware.ModelName = ConvertHelper.ConvertToString(txtModel.Text);
            request.WorkStationHardware.Core = ConvertHelper.ConvertToInteger(txtCore.Text);
            request.WorkStationHardware.CPUID = ConvertHelper.ConvertToInteger(ddlCPU.SelectedItem.Value);
            request.WorkStationHardware.MemoryIDs = ConvertHelper.ConvertToString(txtTotalMemoryQuantity.Text);
            request.WorkStationHardware.MotherBoardID = ConvertHelper.ConvertToInteger(ddlMotherboard.SelectedItem.Value);
            request.WorkStationHardware.HardDriveIDs = ConvertHelper.ConvertToString(hidmulddlHardDrive.Value);
            request.WorkStationHardware.ChipsetID = ConvertHelper.ConvertToInteger(ddlChipset.SelectedItem.Value);
            request.WorkStationHardware.VideoCardIDs = ConvertHelper.ConvertToString(hidmulddlVideo.Value);
            request.WorkStationHardware.DisplayIDs = ConvertHelper.ConvertToString(hidmulddlDisplay.Value);
            request.WorkStationHardware.MultimediaIDs = ConvertHelper.ConvertToString(hidmulddlMultimedia.Value);
            request.WorkStationHardware.PortIDs = ConvertHelper.ConvertToString(hidmulddlPort.Value);
            request.WorkStationHardware.SlotIDs = ConvertHelper.ConvertToString(hidmulddlSlot.Value);
            request.WorkStationHardware.ChassisID = ConvertHelper.ConvertToInteger(ddlChasis.SelectedItem.Value);
            request.WorkStationHardware.PowerIDs = ConvertHelper.ConvertToString(hidmulddlPower.Value);


            request.WorkStationHardware.Manufacturer = ConvertHelper.ConvertToString(txtManufacturer.Text);

            if (txtNotes.Text != "" && txtNotes.Text != null)
                request.WorkStationHardware.FullNotes = txtNotes.Text.Replace(',', '|');

            request.WorkStationHardware.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.WorkStationHardware.StatusID = 1;
            request.WorkStationHardware.CreatedBy = currentUser.ApplicationUserID;
            request.WorkStationHardware.ModifiedBy = currentUser.ApplicationUserID;
            request.WorkStationHardware.CreatedOn = DateTime.Now;
            request.WorkStationHardware.ModifiedOn = DateTime.Now;

            request.CurrentAction = CurrentAction;
            //if (CurrentAction == ActionType.Edit)
            //{
            request.WorkStationHardware.WorkStationHardwareID = ConvertHelper.ConvertToInteger(base.Id);
            //}
            response = new PTResponse();
            //response = webServiceHelper.PostRequest(request);
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
                if (response.isSuccess)
                {
                    //CrudWorkStation.Visible = false;
                    //CrudWorkStationHardware.Visible = false;
                    //divGrdWorkStationInfo.Visible = false;
                    //divGrdWorkStationHardwareInfo.Visible = true;
                    PopulateControls();
                    ClearAll();
                    CloseColorBox();
                }
            }
            else
            {
                ShowMessage("Error While Adding the WorkStation Hardware", false);
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
            serviceName = "SAVEWORKSTATIONINFO";

            request.WorkStationInfo = new WorkStationInfo();


            //Framing the URL
            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;
            request.WorkStationInfo.HostName = ConvertHelper.ConvertToString(txtHostName.Text);
            request.WorkStationInfo.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text);

            if (chkDHCP.Checked)
                request.WorkStationInfo.IPAddress = "DHCP";
            else
                request.WorkStationInfo.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text);
            request.WorkStationInfo.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text);
            request.WorkStationInfo.Gateway = ConvertHelper.ConvertToString(txtGateway.Text);
            request.WorkStationInfo.AdminUserName = ConvertHelper.ConvertToString(txtAUName.Text);
            request.WorkStationInfo.Password = ConvertHelper.ConvertToString(txtPassword.Text);
            request.WorkStationInfo.Domain = ConvertHelper.ConvertToString(txtDomain.Text);
            request.WorkStationInfo.OperatingSystemLicenseKey = ConvertHelper.ConvertToString(txtLKOS.Text);
            request.WorkStationInfo.AntiVirusLicenseKey = ConvertHelper.ConvertToString(txtLKAV.Text);

            request.WorkStationInfo.InstalledDate = ConvertHelper.ConvertToString(txtInstalledDate.Text);
            request.WorkStationInfo.WarrantyExpires = ConvertHelper.ConvertToString(txtWEDate.Text);

            request.WorkStationInfo.WorkStationModelID = ConvertHelper.ConvertToInteger(ddlWModel.SelectedItem.Value);

            request.WorkStationInfo.WorkStationRoleIDs = ConvertHelper.ConvertToString(hidmulddlSRoles.Value);
            request.WorkStationInfo.OperatingSystemID = ConvertHelper.ConvertToInteger(ddlOS.SelectedItem.Value);
            request.WorkStationInfo.AntiVirusID = ConvertHelper.ConvertToInteger(ddlAV.SelectedItem.Value);

            request.WorkStationInfo.WorkStationAssignedUserIDs = ConvertHelper.ConvertToString(hidmulddlAUsers.Value);

            if (txtBAWLK.Text != "" || txtBAWLK.Text != null)
                request.WorkStationInfo.WorkStationBackupIDs = ConvertListType(ddlBA, txtBAWLK.Text.Replace(';', ','));

            if (txtLKWA.Text != "" || txtLKWA.Text != null)
                request.WorkStationInfo.WorkStationApplicationIDs = ConvertListType(ddlApp, txtLKWA.Text.Replace(';', ','));

            if (txtSNotes.Text != "" || txtSNotes.Text != null)
                request.WorkStationInfo.FullNotes = txtSNotes.Text.Replace(';', '|');

            request.WorkStationInfo.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.WorkStationInfo.StatusID = 1;
            request.WorkStationInfo.CreatedBy = currentUser.ApplicationUserID;
            request.WorkStationInfo.ModifiedBy = currentUser.ApplicationUserID;
            request.WorkStationInfo.CreatedOn = DateTime.Now;
            request.WorkStationInfo.ModifiedOn = DateTime.Now;

            request.CurrentAction = CurrentAction;
            if (CurrentAction == ActionType.Edit)
            {
                request.WorkStationInfo.WorkStationID = ConvertHelper.ConvertToInteger(base.Id);
            }
            //response = new PTResponse();
            //response = webServiceHelper.PostRequest(request);
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
                if (response.isSuccess)
                {
                    if (HiddenColorBox.Value == "0")
                    {
                        CrudWorkStation.Visible = false;
                        CrudWorkStationHardware.Visible = false;
                        divGrdWorkStationInfo.Visible = true;
                        divGrdWorkStationHardwareInfo.Visible = false;
                        ClearAll();
                        //if (Request.QueryString["isColorBox"] != null)
                        //    CloseColorBox();
                    }
                    else
                    {
                        CrudWorkStation.Visible = false;
                        CrudWorkStationHardware.Visible = false;
                        divGrdWorkStationInfo.Visible = false;
                        divGrdWorkStationHardwareInfo.Visible = false;
                        provClose.Visible = true;
                    }
                }
                else
                {
                    CrudWorkStation.Visible = true;
                    CrudWorkStationHardware.Visible = false;
                    divGrdWorkStationInfo.Visible = false;
                    divGrdWorkStationHardwareInfo.Visible = false;
                }
            }
            else
            {
                ShowMessage("Error While Adding the WorkStation", false);
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
            CrudWorkStationHardware.Visible = false;
            CrudWorkStation.Visible = false;
            divGrdWorkStationInfo.Visible = true;
            divGrdWorkStationHardwareInfo.Visible = false;
            btnSSubmit.Visible = true;
            btnSBack.Visible = false;
            EnableWorkStationControls();
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

            CrudWorkStationHardware.Visible = false;
            CrudWorkStation.Visible = false;
            divGrdWorkStationInfo.Visible = true;
            divGrdWorkStationHardwareInfo.Visible = false;
            btnSSubmit.Visible = true;
            btnSBack.Visible = false;
            EnableWorkStationControls();
            ShowMessage("", false);

            //CrudWorkStationHardware.Visible = false;
            //CrudWorkStation.Visible = false;
            //divGrdWorkStationInfo.Visible = false;
            //divGrdWorkStationHardwareInfo.Visible = false;
            //btnSubmit.Visible = true;
            //btnBack.Visible = false;
            ClearAll();
            //EnableWorkStationHardwareControls();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Button Events ]

    #region [Privte Function]

    private void CommonBack()
    {
        try
        {
            CrudWorkStation.Visible = false;
            CrudWorkStationHardware.Visible = false;
            divGrdWorkStationInfo.Visible = true;
            divGrdWorkStationHardwareInfo.Visible = false;

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    private void CheckWorkStationHardWare()
    {
        try
        {
            if (Request.QueryString["opp"] != null)
            {
                if (Request.QueryString["opp"] == "S")
                {
                    CrudWorkStation.Visible = true;
                    CrudWorkStationHardware.Visible = false;
                    if (Request.QueryString["id"] != null && txtHostName.Text == "")
                    {
                        ModifyWorkStation(base.Id);
                        //txtHostName.Enabled = false;
                    }
                }
                else
                {
                    CrudWorkStation.Visible = false;
                    CrudWorkStationHardware.Visible = true;
                    if (Request.QueryString["id"] != null && txtSHSer.Text == "")
                    {
                        ModifyWorkStationHardware();
                        // txtWorkStationHost.Enabled = false;
                    }
                }
            }
            else
            {
                CrudWorkStation.Visible = false;
                CrudWorkStationHardware.Visible = false;

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
        string[] sSplit = sValue.Split('|');
        BindMultiSelectDropDownWithSelectedValues(ddl, hid, sSplit[0]);
        if (sSplit[1] != null)
            txt.Text = sSplit[1].Replace(',', ';');
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

            serviceURL = PostServiceURL + "POPULATEWORKSTATIONHARDWARES";
            request.WorkStationHardware = new WorkStationHardware();
            request.WorkStationHardware.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.WorkStationHardwareList != null && response.WorkStationHardwareList.Count > 0)
            {
               // PopulateWorkStationModelDropDownList(ddlWModel, response.WorkStationHardwareList, true);

               // PopulateWorkStationModelDropDownList(ddlWManufacturer, response.WorkStationHardwareList, true);
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


            #region [GE ALL COMPUTERS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLWORKSTATIONINFO/Mastername/0/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.WorkStationInfoList != null && response.WorkStationInfoList.Count > 0)
            {
                PopulateWorkStationDropDownList(ddlWorkstations, response.WorkStationInfoList, true);
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
                    ddlMulSelectAttribute.Items.FindByValue(s).Attributes.Add("selected", "selected"); //hari
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
                        sList = ConvertHelper.ConvertToString(ddl.Items.FindByValue(sLists[i]).Text) + ":" + sLists[i + 1]; //hari
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

    #region [Workstation Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedWorkstationId = ConvertHelper.ConvertToString(ddlWorkstations.SelectedValue);
            ModifyWorkStation(selectedWorkstationId);
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
            txtWorkStationHost.Text = String.Empty;
            txtSHSer.Text = String.Empty;
            txtSNotes.Text = String.Empty;
            txtSubnet.Text = String.Empty;
            txtWEDate.Text = String.Empty;
            txtWorkStationHost.ReadOnly = false;
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
            ddlWModel.SelectedIndex = -1;
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
    //Disable Work Station Controls
    private void DisableWorkStationControls()
    {
        try
        {
            txtHostName.ReadOnly = true;
            ddlWModel.Enabled = false;
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

    //Enable Work Station Controls
    private void EnableWorkStationControls()
    {
        try
        {
            txtHostName.ReadOnly = false;
            ddlWModel.Enabled = true;
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

    //Disable Work Station Hardware Controls
    private void DisableWorkStationHardwareControls()
    {
        try
        {
            txtWorkStationHost.ReadOnly = true;
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

    //Enable Work Station Hardware Controls
    private void EnableWorkStationHardwareControls()
    {
        try
        {
            txtWorkStationHost.ReadOnly = false;
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