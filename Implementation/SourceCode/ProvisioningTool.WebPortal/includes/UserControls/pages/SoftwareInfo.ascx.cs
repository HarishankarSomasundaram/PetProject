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

public partial class UserControlsSoftwareInfo : UCController
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
            if (CurrentAction == ActionType.Add)
            {
                PopulateControls();
                CrudSoftware.Visible = true;
                divGrdSoftwareInfo.Visible = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                PopulateControls();
                ModifySoftware();
                btnSubmit.Visible = true;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.MoreView)//To view the page without edit
            //else if (CurrentAction == ActionType.View)
            {
                PopulateControls();
                ModifySoftware();
                btnSubmit.Visible = false;
                DisableControls(divSoftwareDetail);
                divSoftwareDetail.Attributes.Add("class", divSoftwareDetail.Attributes["class"] + " viewPage");
                inlineNotes.Attributes.Add("class", inlineNotes.Attributes["class"] + " columnAlign");
                txtNotes.Attributes.Remove("class");
                btnBack.Visible = true;
                btnBack.Enabled = true;
            }
            else
            {
                CrudSoftware.Visible = false;
                divGrdSoftwareInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get Software Info and Bind the Controls For Edit And View]
    private void ModifySoftware()
    {
        try
        {
            CrudSoftware.Visible = true;
            divGrdSoftwareInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETSOFTWAREBYSOFTWAREID";
                request.Software = new Software();
                request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                request.Software.SoftwareID = ConvertHelper.ConvertToInteger(base.Id);
                hidEditID.Value = ConvertHelper.ConvertToString(base.Id);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.Software != null)
            {

                txtApplication.Text = ConvertHelper.ConvertToString(response.Software.Application);
                txtApplication.ToolTip = ConvertHelper.ConvertToString(response.Software.Application);

                txtDescription.Text = ConvertHelper.ConvertToString(response.Software.SoftwareDescription);
                txtDescription.ToolTip = ConvertHelper.ConvertToString(response.Software.SoftwareDescription);

                txtLicense.Text = ConvertHelper.ConvertToString(response.Software.LicenseKey);
                txtLicense.ToolTip = ConvertHelper.ConvertToString(response.Software.LicenseKey);

                txtServer.Text = ConvertHelper.ConvertToString(response.Software.Server);
                txtServer.ToolTip = ConvertHelper.ConvertToString(response.Software.Server);

                txtPath.Text = ConvertHelper.ConvertToString(response.Software.PathID);
                txtPath.ToolTip = ConvertHelper.ConvertToString(response.Software.PathID);

               // txtInstalledDate.Text = ConvertHelper.ConvertToString(response..InstalledOn == null ? string.Empty : response.Software.InstalledOn);
                txtInstalledDate.Text = ConvertHelper.ConvertToDateTime(response.Software.InstalledOn).ToString("MM-dd-yyyy");
                txtVersion.Text = ConvertHelper.ConvertToString(response.Software.Version);
                hidAssignedUsers.Value = ConvertHelper.ConvertToString(response.Software.SelectedAssignedUsers == null ? "" : response.Software.SelectedAssignedUsers);
                MultipleItemsSelectByValuesForDropdown(ddlAssignedUsers, response.Software.SelectedAssignedUsers == null ? "" : response.Software.SelectedAssignedUsers.Replace("|", ";"), ';');
                txtNotes.Text = (response.Software.Notes != null && response.Software.Notes != "") ? ConvertHelper.ConvertToString(response.Software.Notes.Replace("|", ";")) : "";
                txtNotes.ToolTip = txtNotes.Text;
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

    #region [Add Software]
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

            request.Software = new Software();
            //request.Software.AssignedUser = new ();


            CultureInfo provider = CultureInfo.InvariantCulture;
            //string str = DMY_MDY();
            if (ConvertHelper.ConvertToString(txtInstalledDate.Text) != null)
            {
                request.Software.Application = ConvertHelper.ConvertToString(txtApplication.Text);
                request.Software.SoftwareDescription = ConvertHelper.ConvertToString(txtDescription.Text);
                request.Software.LicenseKey = ConvertHelper.ConvertToString(txtLicense.Text);
                request.Software.Server = ConvertHelper.ConvertToString(txtServer.Text);
                request.Software.PathID = ConvertHelper.ConvertToString(txtPath.Text);
                request.Software.Version = ConvertHelper.ConvertToString(txtVersion.Text);
                //DateTime installedDateTime = DateTime.Parse(txtInstalledDate.Text);
                request.Software.InstalledOn = ConvertHelper.ConvertToString(txtInstalledDate.Text);   //installedDateTime.Add(new TimeSpan(5, 30, 0));
                request.Software.SelectedAssignedUsers = ConvertHelper.ConvertToString(hidAssignedUsers.Value, "").Replace("|", ",");
                request.Software.Notes = ConvertHelper.ConvertToString(txtNotes.Text,"").Replace(";", ";");
                request.Software.CreatedBy = currentUser.ApplicationUserID;
                request.Software.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.Software.SoftwareID = ConvertHelper.ConvertToInteger(base.Id);
                    serviceName = "SAVESOFTWARE";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.Software.StatusID = 1;
                    serviceName = "SAVESOFTWARE";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                //            response = webServiceHelper.PostRequest(request);
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    //ShowMessage(response.Message, true);
                    ShowMessage(response.Message, true);
                    CrudSoftware.Visible = false;
                    divGrdSoftwareInfo.Visible = true;
                    //Response.Redirect("CustomerInfo.aspx?nav=Softwares");
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudSoftware.Visible = true;
                    divGrdSoftwareInfo.Visible = false;
                    hidAssignedUsers.Value = ConvertHelper.ConvertToString(request.Software.SelectedAssignedUsers == null ? "" : request.Software.SelectedAssignedUsers);
                    MultipleItemsSelectByValuesForDropdown(ddlAssignedUsers, request.Software.SelectedAssignedUsers == null ? "" : request.Software.SelectedAssignedUsers.Replace(",", ";"), ';');

                }
            }
            else
            {
                CrudSoftware.Visible = true;
                divGrdSoftwareInfo.Visible = false;
                hidAssignedUsers.Value = ConvertHelper.ConvertToString(request.Software.SelectedAssignedUsers == null ? "" : request.Software.SelectedAssignedUsers);
                MultipleItemsSelectByValuesForDropdown(ddlAssignedUsers, request.Software.SelectedAssignedUsers == null ? "" : request.Software.SelectedAssignedUsers.Replace(",", ";"), ';');
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


    #region [Back to Grid View Mode of Corresponding Software Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudSoftware.Visible = false;
            divGrdSoftwareInfo.Visible = true;
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
                PopulateUserDropDownList(ddlAssignedUsers, response.UserList, true);
            }
            #endregion


        }
        catch (Exception ex)
        { ShowMessage(ex.Message, false); }
    }


    #endregion


}
