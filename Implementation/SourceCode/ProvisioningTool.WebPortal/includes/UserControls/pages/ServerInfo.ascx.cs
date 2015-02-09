using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;

public partial class UserControlsServerInfo : UCController
{
    #region [ Variable Declarations ]

    private ServerHardwareBLL serverHardwareBLL;

    private PTResponse response;
    private PTRequest request;
    private WebServiceHelper webServiceHelper;
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
                txtServerHost.Text = ConvertHelper.ConvertToString(Request.QueryString["HostName"], "");
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
            InitializeIframe(CrudServer, divGrdServerHardwareInfo);
            provClose.Visible = false;

            if (CurrentAction == ActionType.Add)
            {
                PopulateControls();
                CheckServerHardWare();
                txtHostName.Enabled = true;
                txtServerHost.Enabled = true;
                divGrdServerInfo.Visible = false;
                divGrdServerHardwareInfo.Visible = false;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                PopulateControls();
                CheckServerHardWare();
                divGrdServerInfo.Visible = false;
                divGrdServerHardwareInfo.Visible = false;
            }
            else if (CurrentAction == ActionType.MoreView)
            {
                if (Request.QueryString["opp"] == "S")
                {
                    PopulateControls();
                    ModifyServer(base.Id);
                    //DisableServerControls();
                    DisableControls(CrudServer);
                    CrudServer.Attributes.Add("class", CrudServer.Attributes["class"] + " viewPage");
                    inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
                    //inlineInterface.Attributes.Add("class", inlineInterface.Attributes["class"] + " columnAlign");
                    lnkViewServerHardware.Visible = false;
                    lnkServerHardwware.Visible = false;
                    txtBAWLK.Attributes.Remove("class");
                    txtLKWA.Attributes.Remove("class");
                    txtSNotes.Attributes.Remove("class");
                    divGrdServerInfo.Visible = false;
                    divGrdServerHardwareInfo.Visible = false;
                    CrudServer.Visible = true;
                    CrudServerHardware.Visible = false;
                    btnSSubmit.Visible = false;
                    btnSBack.Visible = true;
                    btnSBack.Enabled = true;
                }
                else if (Request.QueryString["opp"] == "SH")
                {
                    PopulateControls();
                    ModifyServerHardware();
                    //DisableServerHardwareControls();
                    DisableControls(CrudServerHardware);
                    CrudServerHardware.Attributes.Add("class", CrudServerHardware.Attributes["class"] + " viewPage");
                    txtNotes.Attributes.Remove("class");
                    divGrdServerInfo.Visible = false;
                    divGrdServerHardwareInfo.Visible = false;
                    CrudServer.Visible = false;
                    CrudServerHardware.Visible = true;
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
                    divGrdServerInfo.Visible = false;
                    divGrdServerHardwareInfo.Visible = true;
                    CrudServer.Visible = false;
                    CrudServerHardware.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Determine Action ]

    #region [Get Server Hardware Info and Bind the Controls For Edit And View]

    private void ModifyServerHardware()
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
                serviceURL = PostServiceURL + "GETSERVERHARDWARANDUSERDETAILSBYSERVERHARDWARID";
                request.ServerHardware = new ServerHardware();
                request.ServerHardware.ServerHardwareID = ConvertHelper.ConvertToInteger(base.Id);
                hidEditID.Value = ConvertHelper.ConvertToString(base.Id);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.ServerHardware != null)
            {
                txtServerHost.Text = ConvertHelper.ConvertToString(response.ServerHardware.HostName);
                txtSHSer.Text = ConvertHelper.ConvertToString(response.ServerHardware.SerialNumber);

                if (response.ServerHardware.FullNotes != null)
                    txtNotes.Text = ConvertHelper.ConvertToString(response.ServerHardware.FullNotes.Replace('|', ';'));

                txtModel.Text = ConvertHelper.ConvertToString(response.ServerHardware.ModelName);
                ddlCPU.SelectedValue = ConvertHelper.ConvertToString(response.ServerHardware.CPUID);
                ddlMotherboard.SelectedValue = ConvertHelper.ConvertToString(response.ServerHardware.MotherBoardID);
                ddlChipset.SelectedValue = ConvertHelper.ConvertToString(response.ServerHardware.ChipsetID);
                ddlChasis.SelectedValue = ConvertHelper.ConvertToString(response.ServerHardware.ChassisID);
                txtCore.Text = ConvertHelper.ConvertToString(response.ServerHardware.Core);
                txtManufacturer.Text = ConvertHelper.ConvertToString(response.ServerHardware.Manufacturer);

                //BindMultiSelectDropDownWithSelectedValues(ddlMemory, hidmulddlMemory, response.ServerHardware.MemoryIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlHardDrive, hidmulddlHardDrive, response.ServerHardware.HardDriveIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlVideoCard, hidmulddlVideo, response.ServerHardware.VideoCardIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlDisplay1, hidmulddlDisplay, response.ServerHardware.DisplayIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlMultimedia, hidmulddlMultimedia, response.ServerHardware.MultimediaIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlPorts, hidmulddlPort, response.ServerHardware.PortIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlSlots, hidmulddlSlot, response.ServerHardware.SlotIDs);
                BindMultiSelectDropDownWithSelectedValues(ddlPower, hidmulddlPower, response.ServerHardware.PowerIDs);

                List<SystemMemory> memoryList = new List<SystemMemory>();
                memoryList = response.ServerHardware.Memorys;
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

    private void ModifyServer(string serverid)
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

            if (ConvertHelper.ConvertToString(serverid) != null)
            {
                serviceURL = PostServiceURL + "GETSERVERINFOANDUSERDETAILSBYSERVERINFOID";
                request.ServerInfo = new ServerInfo();
                request.ServerInfo.ServerID = ConvertHelper.ConvertToInteger(serverid);
                hidEditID.Value = ConvertHelper.ConvertToString(serverid);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.ServerInfo != null)
            {
                txtHostName.Text = response.ServerInfo.HostName;
                txtHostName.ToolTip = response.ServerInfo.HostName;

                txtSerialNo.Text = response.ServerInfo.SerialNumber;
                txtSerialNo.ToolTip = response.ServerInfo.SerialNumber;

                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.ServerInfo.InstalledDate).ToString("MM-dd-yyyy");
                txtWEDate.Text = ConvertHelper.ConvertToDateTime(response.ServerInfo.WarrantyExpires).ToString("MM-dd-yyyy");
                if (response.ServerInfo.IPAddress == "DHCP")
                {
                    chkDHCP.Checked = true;
                    txtIPAddress.Attributes.Add("disabled", "disabled");
                }
                else
                    txtIPAddress.Text = response.ServerInfo.IPAddress;

                txtSubnet.Text = response.ServerInfo.Subnet;
                txtGateway.Text = response.ServerInfo.Gateway;
                txtAUName.Text = response.ServerInfo.AdminUserName;
                txtAUName.ToolTip = response.ServerInfo.AdminUserName;

                txtPassword.Text = response.ServerInfo.Password;
                txtPassword.ToolTip = response.ServerInfo.Password;

                txtLKOS.Text = response.ServerInfo.OperatingSystemLicenseKey;
                txtLKAV.Text = response.ServerInfo.AntiVirusLicenseKey;

                txtDomain.Text = response.ServerInfo.Domain;
                txtDomain.ToolTip = response.ServerInfo.Domain;

                ddlOS.SelectedValue = ConvertHelper.ConvertToString(response.ServerInfo.OperatingSystemID);
                ddlAV.SelectedValue = ConvertHelper.ConvertToString(response.ServerInfo.AntiVirusID);
                ddlSModel.SelectedValue = ConvertHelper.ConvertToString(response.ServerInfo.ServerModelID);

                if (response.ServerInfo.FullNotes != null)
                {
                    txtSNotes.Text = response.ServerInfo.FullNotes.Replace('|', ';');
                    txtSNotes.ToolTip = response.ServerInfo.FullNotes.Replace('|', ';');
                }

                if (response.ServerInfo.ServerRoleIDs != null)
                    BindMultiSelectDropDownWithSelectedValues(ddlSRoles, hidmulddlSRoles, response.ServerInfo.ServerRoleIDs.Replace('|', ','));
                if (response.ServerInfo.ServerAssignedUserIDs != null)
                    BindMultiSelectDropDownWithSelectedValues(ddlAUsers, hidmulddlAUsers, response.ServerInfo.ServerAssignedUserIDs.Replace('|', ','));

                txtLKWA.Text = ConvertStringtoList(response.ServerInfo.ServerApplicationIDs, ddlApp);
                txtLKWA.ToolTip = ConvertStringtoList(response.ServerInfo.ServerApplicationIDs, ddlApp);

                txtBAWLK.Text = ConvertStringtoList(response.ServerInfo.ServerBackupIDs, ddlBA);
                txtBAWLK.ToolTip = ConvertStringtoList(response.ServerInfo.ServerBackupIDs, ddlBA);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [Get Server Hardware Info and Bind the Controls For Edit And View]

    #region [Populate Dropdowns]

    private void PopulateControls()
    {
        try
        {
            request = new PTRequest();
            request.URL = PostServiceURL + "GETGLOBALMASTERANDDETAILSBYMASTERNAME";

            // Dropdown for Server Hardware
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

            //Dropdown for Server Info
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

    #region [Privte Function]

    private void CommonBack()
    {
        try
        {
            CrudServer.Visible = false;
            CrudServerHardware.Visible = false;
            divGrdServerInfo.Visible = true;
            divGrdServerHardwareInfo.Visible = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void CheckServerHardWare()
    {
        try
        {
            if (Request.QueryString["opp"] != null)
            {
                if (Request.QueryString["opp"] == "S")
                {
                    CrudServer.Visible = true;
                    CrudServerHardware.Visible = false;
                    if (Request.QueryString["id"] != null && txtHostName.Text == "")
                    {
                        ModifyServer(base.Id);
                        //txtHostName.Enabled = false;
                    }
                }
                else
                {
                    CrudServer.Visible = false;
                    CrudServerHardware.Visible = true;
                    if (Request.QueryString["id"] != null && txtSHSer.Text == "")
                    {
                        ModifyServerHardware();
                        //txtServerHost.Enabled = false;
                    }
                }
            }
            else
            {
                CrudServer.Visible = false;
                CrudServerHardware.Visible = false;
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

    #region [Bind Model and User ]

    #region [Server list dropdown for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyServer(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion

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

            serviceURL = PostServiceURL + "POPULATESERVERHARDWARES";
            request.ServerHardware = new ServerHardware();
            request.ServerHardware.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.ServerHardwarList != null && response.ServerHardwarList.Count > 0)
            {
                PopulateServerModelDropDownList(ddlSModel, response.ServerHardwarList, true);
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

            #region [GE ALL ServerS AND POPULATE]
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLSERVERINFO/Mastername/0/0";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.ServerInfoList != null && response.ServerInfoList.Count > 0)
            {
                PopulateServerDropDownList(ddldeviceList, response.ServerInfoList, true);
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
            txtServerHost.Text = String.Empty;
            txtSHSer.Text = String.Empty;
            txtSNotes.Text = String.Empty;
            txtSubnet.Text = String.Empty;
            txtWEDate.Text = String.Empty;
            txtServerHost.ReadOnly = false;
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
            ddlSModel.SelectedIndex = -1;
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

    //Disable Server Controls
    private void DisableServerControls()
    {
        try
        {
            txtHostName.ReadOnly = true;
            ddlSModel.Enabled = false;
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

    //Enable Server Controls
    private void EnableServerControls()
    {
        try
        {
            txtHostName.ReadOnly = false;
            ddlSModel.Enabled = true;
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

    //Disable Server Hardware Controls
    private void DisableServerHardwareControls()
    {
        try
        {
            txtServerHost.ReadOnly = true;
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

    //Enable Server Hardware Controls
    private void EnableServerHardwareControls()
    {
        try
        {
            txtServerHost.ReadOnly = false;
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


    #endregion [Privte Function]

    #region [ Button Events ]

    #region [ Link Button Events ]

    protected void lnkServerHardwware_Click(object sender, EventArgs e)
    {
        try
        {
            CrudServer.Visible = false;
            CrudServerHardware.Visible = true;
            divGrdServerInfo.Visible = false;
            divGrdServerHardwareInfo.Visible = false;
            txtServerHost.Text = txtHostName.Text;
            txtServerHost.ReadOnly = true;
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
            CrudServer.Visible = true;
            CrudServerHardware.Visible = false;
            divGrdServerInfo.Visible = false;
            divGrdServerHardwareInfo.Visible = false;
            EnableServerHardwareControls();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void lnkViewServerHardware_Click(object sender, EventArgs e)
    {
        try
        {
            CrudServer.Visible = false;
            CrudServerHardware.Visible = false;
            divGrdServerInfo.Visible = false;
            divGrdServerHardwareInfo.Visible = true;
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
            serviceURL = PostServiceURL;
            serviceName = "SAVESERVERHARDWARE";

            request.ServerHardware = new ServerHardware();

            request.ServerHardware.Port = new GlobalMasterDetail();
            request.ServerHardware.CPU = new GlobalMasterDetail();
            request.ServerHardware.Memory = new GlobalMasterDetail();
            request.ServerHardware.MotherBoard = new GlobalMasterDetail();
            request.ServerHardware.HardDrive = new GlobalMasterDetail();
            request.ServerHardware.Chipset = new GlobalMasterDetail();
            request.ServerHardware.VideoCard = new GlobalMasterDetail();
            request.ServerHardware.Display = new GlobalMasterDetail();
            request.ServerHardware.Multimedia = new GlobalMasterDetail();
            request.ServerHardware.Port = new GlobalMasterDetail();
            request.ServerHardware.Slot = new GlobalMasterDetail();
            request.ServerHardware.Chassis = new GlobalMasterDetail();
            request.ServerHardware.Power = new GlobalMasterDetail();

            //Framing the URL
            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;
            request.ServerHardware.HostName = ConvertHelper.ConvertToString(txtServerHost.Text);
            request.ServerHardware.SerialNumber = ConvertHelper.ConvertToString(txtSHSer.Text);

            request.ServerHardware.ModelName = ConvertHelper.ConvertToString(txtModel.Text);
            request.ServerHardware.Core = ConvertHelper.ConvertToInteger(txtCore.Text);
            request.ServerHardware.CPUID = ConvertHelper.ConvertToInteger(ddlCPU.SelectedItem.Value);
            request.ServerHardware.MemoryIDs = ConvertHelper.ConvertToString(txtTotalMemoryQuantity.Text);
            request.ServerHardware.MotherBoardID = ConvertHelper.ConvertToInteger(ddlMotherboard.SelectedItem.Value);
            request.ServerHardware.HardDriveIDs = ConvertHelper.ConvertToString(hidmulddlHardDrive.Value);
            request.ServerHardware.ChipsetID = ConvertHelper.ConvertToInteger(ddlChipset.SelectedItem.Value);
            request.ServerHardware.VideoCardIDs = ConvertHelper.ConvertToString(hidmulddlVideo.Value);
            request.ServerHardware.DisplayIDs = ConvertHelper.ConvertToString(hidmulddlDisplay.Value);
            request.ServerHardware.MultimediaIDs = ConvertHelper.ConvertToString(hidmulddlMultimedia.Value);
            request.ServerHardware.PortIDs = ConvertHelper.ConvertToString(hidmulddlPort.Value);
            request.ServerHardware.SlotIDs = ConvertHelper.ConvertToString(hidmulddlSlot.Value);
            request.ServerHardware.ChassisID = ConvertHelper.ConvertToInteger(ddlChasis.SelectedItem.Value);
            request.ServerHardware.PowerIDs = ConvertHelper.ConvertToString(hidmulddlPower.Value);
            request.ServerHardware.Manufacturer = ConvertHelper.ConvertToString(txtManufacturer.Text);
            //Site site = Session["SiteDetails"] as Site;
            request.ServerHardware.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);

            if (txtNotes.Text != "" && txtNotes.Text != null)
                request.ServerHardware.FullNotes = txtNotes.Text.Replace(';', '|');

            request.ServerHardware.StatusID = 1;
            request.ServerHardware.CreatedBy = currentUser.ApplicationUserID;
            request.ServerHardware.ModifiedBy = currentUser.ApplicationUserID;
            request.ServerHardware.CreatedOn = DateTime.Now;
            request.ServerHardware.ModifiedOn = DateTime.Now;

            request.CurrentAction = CurrentAction;
            if (CurrentAction == ActionType.Edit)
            {
                request.ServerHardware.ServerHardwareID = ConvertHelper.ConvertToInteger(base.Id);
            }
            response = new PTResponse();
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
                if (response.isSuccess)
                {
                    //CrudServer.Visible = false;
                    //CrudServerHardware.Visible = false;
                    //divGrdServerInfo.Visible = false;
                    //divGrdServerHardwareInfo.Visible = true;
                    PopulateControls();
                    ClearAll();
                    CloseColorBox();
                }
            }
            else
            {
                ShowMessage("Error While Adding the Server Hardware", false);
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
            serviceURL = PostServiceURL;
            serviceName = "SAVESERVERINFO";

            request.ServerInfo = new ServerInfo();

            //Framing the URL
            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;
            request.ServerInfo.HostName = ConvertHelper.ConvertToString(txtHostName.Text);
            request.ServerInfo.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text);
            if (chkDHCP.Checked)
                request.ServerInfo.IPAddress = "DHCP";
            else
                request.ServerInfo.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text);
            request.ServerInfo.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text);
            request.ServerInfo.Gateway = ConvertHelper.ConvertToString(txtGateway.Text);
            request.ServerInfo.AdminUserName = ConvertHelper.ConvertToString(txtAUName.Text);
            request.ServerInfo.Password = ConvertHelper.ConvertToString(txtPassword.Text);
            request.ServerInfo.Domain = ConvertHelper.ConvertToString(txtDomain.Text);
            request.ServerInfo.OperatingSystemLicenseKey = ConvertHelper.ConvertToString(txtLKOS.Text);
            request.ServerInfo.AntiVirusLicenseKey = ConvertHelper.ConvertToString(txtLKAV.Text);

            request.ServerInfo.InstalledDate = ConvertHelper.ConvertToString(txtInstalledDate.Text);
            request.ServerInfo.WarrantyExpires = ConvertHelper.ConvertToString(txtWEDate.Text);

            request.ServerInfo.ServerModelID = ConvertHelper.ConvertToInteger(ddlSModel.SelectedItem.Value);

            request.ServerInfo.ServerRoleIDs = ConvertHelper.ConvertToString(hidmulddlSRoles.Value);
            request.ServerInfo.OperatingSystemID = ConvertHelper.ConvertToInteger(ddlOS.SelectedItem.Value);
            request.ServerInfo.AntiVirusID = ConvertHelper.ConvertToInteger(ddlAV.SelectedItem.Value);

            request.ServerInfo.ServerAssignedUserIDs = ConvertHelper.ConvertToString(hidmulddlAUsers.Value);

            if (txtBAWLK.Text != "" || txtBAWLK.Text != null)
                request.ServerInfo.ServerBackupIDs = ConvertListType(ddlBA, txtBAWLK.Text.Replace(';', ','));

            if (txtLKWA.Text != "" || txtLKWA.Text != null)
                request.ServerInfo.ServerApplicationIDs = ConvertListType(ddlApp, txtLKWA.Text.Replace(';', ','));

            if (txtSNotes.Text != "" || txtSNotes.Text != null)
                request.ServerInfo.FullNotes = txtSNotes.Text.Replace(';', '|');

            request.ServerInfo.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.ServerInfo.StatusID = 1;
            request.ServerInfo.CreatedBy = currentUser.ApplicationUserID;
            request.ServerInfo.ModifiedBy = currentUser.ApplicationUserID;
            request.ServerInfo.CreatedOn = DateTime.Now;
            request.ServerInfo.ModifiedOn = DateTime.Now;

            request.CurrentAction = CurrentAction;
            if (CurrentAction == ActionType.Edit)
            {
                request.ServerInfo.ServerID = ConvertHelper.ConvertToInteger(base.Id);
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

                        CrudServer.Visible = false;
                        CrudServerHardware.Visible = false;
                        divGrdServerInfo.Visible = true;
                        divGrdServerHardwareInfo.Visible = false;
                        ClearAll();
                        //if (Request.QueryString["isColorBox"] != null)
                            //CloseColorBox();
                    }
                    else
                    {
                        CrudServer.Visible = false;
                        CrudServerHardware.Visible = false;
                        divGrdServerInfo.Visible = false;
                        divGrdServerHardwareInfo.Visible = false;
                        provClose.Visible = true;
                    }
                }
                else
                {
                    CrudServer.Visible = true;
                    CrudServerHardware.Visible = false;
                    divGrdServerInfo.Visible = false;
                    divGrdServerHardwareInfo.Visible = false;
                }
            }
            else
            {
                ShowMessage("Error While Adding the Server Info", false);
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
            CrudServerHardware.Visible = false;
            CrudServer.Visible = false;
            divGrdServerInfo.Visible = true;
            divGrdServerHardwareInfo.Visible = false;
            btnSSubmit.Visible = true;
            btnSBack.Visible = false;
            EnableServerControls();
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
            CrudServerHardware.Visible = false;
            CrudServer.Visible = false;
            divGrdServerInfo.Visible = false;
            divGrdServerHardwareInfo.Visible = true;
            btnSubmit.Visible = true;
            btnBack.Visible = false;
            EnableServerHardwareControls();
            ShowMessage("", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Button Events ]

}