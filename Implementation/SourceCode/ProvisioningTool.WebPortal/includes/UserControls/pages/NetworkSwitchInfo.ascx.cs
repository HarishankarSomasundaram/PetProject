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

public partial class UserControlsNetworkSwitchInfo : UCController
{

    #region [ Variable Declarations ]

    PTResponse response;
    PTRequest request;
    NetworkSwitch networkSwitch;
    WebServiceHelper webServiceHelper;

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
            InitializeIframe(CrudNetworkSwitch, divGrdNetworkSwitchInfo);
            PopulateControls();
            provClose.Visible = false;
            if (CurrentAction == ActionType.Add)
            {
                CrudNetworkSwitch.Visible = true;
                divGrdNetworkSwitchInfo.Visible = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                ModifyNetworkSwitch(base.Id);
                //txtHostName.Enabled = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.View)
            {
                CrudNetworkSwitch.Visible = false;
                divGrdNetworkSwitchInfo.Visible = true;
            }
            else if (CurrentAction == ActionType.MoreView)
            {
                ModifyNetworkSwitch(base.Id);
                DisableControls(divNetworkSwitchDetail);
                divNetworkSwitchDetail.Attributes.Add("class", divNetworkSwitchDetail.Attributes["class"] + " viewPage");
                inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
                inlineInterface.Attributes.Add("class", inlineInterface.Attributes["class"] + " columnAlign");
                txtInterfaces.Attributes.Remove("class");
                txtNotes.Attributes.Remove("class");
                btnSubmit.Visible = false;
                btnBack.Visible = true;
                btnBack.Enabled = true;
            }
            else
            {
                CrudNetworkSwitch.Visible = false;
                divGrdNetworkSwitchInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            showMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get NetworkSwitch Info and Bind the Controls For Edit And View]
    private void ModifyNetworkSwitch(string networkSwitchid)
    {
        try
        {
            CrudNetworkSwitch.Visible = true;
            divGrdNetworkSwitchInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(networkSwitchid) != null)
            {
                serviceURL = PostServiceURL + "GETNETWORKSWITCHANDNETWORKSWITCHDETAILSBYNETWORKSWITCHID";
                request.NetworkSwitch = new NetworkSwitch();
                request.NetworkSwitch.NetworkSwitchID = ConvertHelper.ConvertToInteger(networkSwitchid);
                hidEditID.Value = ConvertHelper.ConvertToString(networkSwitchid);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.NetworkSwitch != null)
            {

                txtHostName.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.Hostname);
                txtHostName.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.Hostname);
                ddlModel.SelectedValue = ConvertHelper.ConvertToString(response.NetworkSwitch.NetworkSwitchModel.MasterDetailID);
                ddlModel.ToolTip = ddlModel.SelectedItem.Text;

                txtSerialNo.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.SerialNumber);
                txtSerialNo.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.SerialNumber);

                //txtInstalledDate.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.InstalledOn == null ? string.Empty : response.NetworkSwitch.InstalledOn.ToString());
                //txtWarrantyExpires.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.WarrantyExpiresOn == null ? string.Empty : response.NetworkSwitch.WarrantyExpiresOn.ToString());
                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.NetworkSwitch.InstalledOn).ToString("MM-dd-yyyy");
                txtWarrantyExpires.Text = ConvertHelper.ConvertToDateTime(response.NetworkSwitch.WarrantyExpiresOn).ToString("MM-dd-yyyy");
                txtSpeed.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.Speed);

                if (response.NetworkSwitch.POE)
                {
                    rbtPOE.SelectedIndex = 0;
                }
                else
                {
                    rbtPOE.SelectedIndex = 1;
                }

                txtPower.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.Power);
                txtPower.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.Power);

                if (response.NetworkSwitch.IPAddress == "DHCP")
                {
                    chkDHCP.Checked = true;
                    txtIPAddress.Attributes.Add("disabled", "disabled");
                }
                else
                    txtIPAddress.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.IPAddress);

                txtSubnet.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.Subnet);
                txtGateway.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.Gateway);
                txtAdminUsername.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.AdminUserName);
                txtPassword.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.AdminPassword);
                txtPassword.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.AdminPassword);

                ddlOSVersion.SelectedValue = ConvertHelper.ConvertToString(response.NetworkSwitch.OSVersion.MasterDetailID);
                ddlOSVersion.ToolTip = ddlOSVersion.SelectedItem.Text;

                txtFirmware.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.Firmware);
                txtFirmware.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.Firmware);

                txtVLAN.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.VLAN);
                txtVLAN.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.VLAN);

                txtSFPType.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.SFPType);
                txtSFPType.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.SFPType);

                txtNotes.Text = response.NetworkSwitch.Notes != null ? ConvertHelper.ConvertToString(response.NetworkSwitch.Notes.Replace("|", ";")) : "";
                txtNotes.ToolTip = response.NetworkSwitch.Notes != null ? ConvertHelper.ConvertToString(response.NetworkSwitch.Notes.Replace("|", ";")) : "";

                MultipleItemsSelectByValuesForDropdown(ddlModules, response.NetworkSwitch.NetworkSwitchModules, ',');
                hidModuleID.Value = ConvertHelper.ConvertToString(response.NetworkSwitch.NetworkSwitchModules, "");
                txtInterfaces.Text = ConvertHelper.ConvertToString(response.NetworkSwitch.NetworkSwitchInterfaces.Replace(",", ";"));
                txtInterfaces.ToolTip = ConvertHelper.ConvertToString(response.NetworkSwitch.NetworkSwitchInterfaces.Replace(",", ";"));
            }

        }
        catch (Exception ex)
        {
            showMessage(ex.Message, false);
        }
    }
    #endregion

    #region [Add NetworkSwitch]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string serviceName = string.Empty;
            networkSwitch = new NetworkSwitch();
            networkSwitch.NetworkSwitchModel = new GlobalMasterDetail();
            networkSwitch.OSVersion = new GlobalMasterDetail();

            networkSwitch.Hostname = ConvertHelper.ConvertToString(txtHostName.Text, "");
            networkSwitch.NetworkSwitchModel.MasterDetailID = ConvertHelper.ConvertToInteger(ddlModel.SelectedValue);
            networkSwitch.SerialNumber = ConvertHelper.ConvertToString(txtSerialNo.Text, "");
            if (ConvertHelper.ConvertToString(txtInstalledDate.Text) != null && ConvertHelper.ConvertToString(txtInstalledDate.Text, "") != "" && ConvertHelper.ConvertToString(txtWarrantyExpires.Text) != null && ConvertHelper.ConvertToString(txtWarrantyExpires.Text, "") != "")
            {
               // DateTime InstalledOnDateTime = DateTime.Parse(txtInstalledDate.Text);
                networkSwitch.InstalledOn = txtInstalledDate.Text; //InstalledOnDateTime.Add(new TimeSpan(5, 30, 0));
                networkSwitch.Speed = ConvertHelper.ConvertToString(txtSpeed.Text);
                networkSwitch.POE = ConvertHelper.ConvertToBoolean(rbtPOE.SelectedValue);
                networkSwitch.Power = ConvertHelper.ConvertToString(txtPower.Text);
                //DateTime WarrantyExpiresOnDateTime = DateTime.Parse(txtWarrantyExpires.Text);
                networkSwitch.WarrantyExpiresOn = txtWarrantyExpires.Text;// WarrantyExpiresOnDateTime.Add(new TimeSpan(5, 30, 0));
                if (chkDHCP.Checked)
                    networkSwitch.IPAddress = "DHCP";
                else
                    networkSwitch.IPAddress = ConvertHelper.ConvertToString(txtIPAddress.Text, "");
                networkSwitch.Subnet = ConvertHelper.ConvertToString(txtSubnet.Text, "");
                networkSwitch.Gateway = ConvertHelper.ConvertToString(txtGateway.Text, "");
                networkSwitch.AdminUserName = ConvertHelper.ConvertToString(txtAdminUsername.Text, "");
                networkSwitch.AdminPassword = ConvertHelper.ConvertToString(txtPassword.Text, "");
                networkSwitch.OSVersion.MasterDetailID = ConvertHelper.ConvertToInteger(ddlOSVersion.SelectedValue);
                networkSwitch.Firmware = ConvertHelper.ConvertToString(txtFirmware.Text, "");
                networkSwitch.VLAN = ConvertHelper.ConvertToString(txtVLAN.Text, "");
                networkSwitch.SFPType = ConvertHelper.ConvertToString(txtSFPType.Text, "");
                networkSwitch.NetworkSwitchModules = ConvertHelper.ConvertToString(hidModuleID.Value, "");
                networkSwitch.NetworkSwitchInterfaces = ConvertHelper.ConvertToString(txtInterfaces.Text, "");
                networkSwitch.Notes = ConvertHelper.ConvertToString(txtNotes.Text, "");
                networkSwitch.CreatedBy = currentUser.ApplicationUserID;
                networkSwitch.ModifiedBy = currentUser.ApplicationUserID;

                if (base.Id != null)
                    networkSwitch.NetworkSwitchID = ConvertHelper.ConvertToInteger(base.Id);

                networkSwitch.StatusID = 1;

                serviceName = "SAVENETWORKSWITCH";
                request = new PTRequest();
                request.NetworkSwitch = networkSwitch;
                request.NetworkSwitch.Site = new Site();
                request.NetworkSwitch.Site.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                request.CurrentAction = CurrentAction;

                request.URL = string.Format(PostServiceURL + "{0}", serviceName);
                response = new PTResponse();
                webServiceHelper = new WebServiceHelper();
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    showMessage(response.Message, true);
                    if (HiddenColorBox.Value == "0")
                    {
                        CrudNetworkSwitch.Visible = false;
                        divGrdNetworkSwitchInfo.Visible = true;
                    }
                    else {
                        CrudNetworkSwitch.Visible = false;
                        divGrdNetworkSwitchInfo.Visible = false;
                        provClose.Visible = true;
                    }
                }
                else
                {
                    showMessage(response.Message, false);
                    CrudNetworkSwitch.Visible = true;
                    divGrdNetworkSwitchInfo.Visible = false;
                    hidModuleID.Value = ConvertHelper.ConvertToString(request.NetworkSwitch.NetworkSwitchModules == null ? string.Empty : request.NetworkSwitch.NetworkSwitchModules);
                    MultipleItemsSelectByValuesForDropdown(ddlModules, request.NetworkSwitch.NetworkSwitchModules == null ? string.Empty : request.NetworkSwitch.NetworkSwitchModules.Replace(",", ";"), ';');
                }
            }
            else
            {
                CrudNetworkSwitch.Visible = true;
                divGrdNetworkSwitchInfo.Visible = false;
                hidModuleID.Value = ConvertHelper.ConvertToString(request.NetworkSwitch.NetworkSwitchModules == null ? string.Empty : request.NetworkSwitch.NetworkSwitchModules);
                MultipleItemsSelectByValuesForDropdown(ddlModules, request.NetworkSwitch.NetworkSwitchModules == null ? string.Empty : request.NetworkSwitch.NetworkSwitchModules.Replace(",", ";"), ';');
                showMessage("Please enter date values", false);

            }
        }
        catch (Exception ex)
        {
            showMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Back to Grid View Mode of Corresponding Fier Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            CrudNetworkSwitch.Visible = false;
            divGrdNetworkSwitchInfo.Visible = true;
        }
        catch (Exception ex)
        {
            showMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Show Message For Success Or Failure Operation]
    public void showMessage(string msg, bool IsSuccess)
    {
        if (IsSuccess == true)
        {
            lblUserControlMessage.Visible = true;
            lblUserControlMessage.Text = msg;
            lblUserControlMessage.CssClass = "sccmsg lblMsgClass";
            lblUserControlMessage.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblUserControlMessage.Visible = true;
            lblUserControlMessage.Text = msg;
            lblUserControlMessage.CssClass = "errmsg lblMsgClass";
            lblUserControlMessage.ForeColor = System.Drawing.Color.Red;
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
            request.GlobalMaster.MasterName = GlobalNSModel;
            PopulateGlobalMasterDropdown(request, ddlModel,false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalNSOS;
            PopulateGlobalMasterDropdown(request, ddlOSVersion, false);

            request.GlobalMaster = new GlobalMaster();
            request.GlobalMaster.MasterName = GlobalNSModule;
            PopulateGlobalMasterDropdown(request, ddlModules, false);
            ddlModules.Items.Insert(0, new ListItem(""));

            //Get All Networkswitch and Populate for Provisioning check list page
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string userResultString = string.Empty;
            serviceURL = GetServiceURL + "GETALLNETWORKSWITCHES/NetworkSwitch/0/dummytext";
            request.URL = serviceURL;
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            userResultString = webServiceHelper.GetRequest(serviceURL);
            response = webServiceHelper.ConvertToObject<PTResponse>(userResultString);
            if (response != null && response.NetworkSwitchList != null && response.NetworkSwitchList.Count > 0)
            {
                PopulateNetworkSwitchDropDownList(ddldeviceList, response.NetworkSwitchList, true);
            }

        }
        catch (Exception ex)
        { showMessage(ex.Message, false); }
    }
    #endregion


    #region [Network Switch Selection for Provisioning Check List]
    protected void btnFill_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedDeviceId = ConvertHelper.ConvertToString(ddldeviceList.SelectedValue);
            ModifyNetworkSwitch(selectedDeviceId);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion
}