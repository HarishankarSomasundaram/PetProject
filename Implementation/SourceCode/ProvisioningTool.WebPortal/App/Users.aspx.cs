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

public partial class ManageSystemEngineer : FormController
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
        DetermineAction();
        if (!Page.IsPostBack) { Page.Validate(); }
    }

    #region [Determine Action]
    private void DetermineAction()
    {
        try
        {
            if (!IsPostBack)
            {
                if (CurrentAction == ActionType.Add)
                {

                    CrudManageSystemEngineer.Visible = true;
                    divGrdManageSystemEngineerInfo.Visible = false;
                    btnSubmit.Visible = true;
                    btnBack.Visible = true;
                    customSearch.Visible = false;
                }
                else if (CurrentAction == ActionType.Edit)
                {

                    ModifyManageSystemEngineer();
                    btnSubmit.Visible = true;
                    btnBack.Visible = true;
                    customSearch.Visible = false;
                }
                else if (CurrentAction == ActionType.MoreView)//To view the page without edit
                {
                    ModifyManageSystemEngineer();
                    btnSubmit.Visible = false;
                    DisableControls(divManageSystemEngineerDetail);
                    btnBack.Visible = true;
                    btnBack.Enabled = true;
                }
                else if (CurrentAction == ActionType.View)
                {
                    CrudManageSystemEngineer.Visible = false;
                    divGrdManageSystemEngineerInfo.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Get ManageSystemEngineer Info and Bind the Controls For Edit And View]
    private void ModifyManageSystemEngineer()
    {
        try
        {
            CrudManageSystemEngineer.Visible = true;
            divGrdManageSystemEngineerInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETAPPLICATIONUSERBYAPPLICATIONUSERID";
                request.ApplicationUser = new ApplicationUser();
                request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                request.ApplicationUser.ApplicationUserID = ConvertHelper.ConvertToInteger(base.Id);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.ApplicationUser != null)
            {
                txtApplicationUserName.Text = ConvertHelper.ConvertToString(response.ApplicationUser.ApplicationUsername);
                txtPassword.Attributes.Add("value", ConvertHelper.ConvertToString(response.ApplicationUser.ApplicationPassword));
                txtConfirmPassword.Attributes.Add("value", ConvertHelper.ConvertToString(response.ApplicationUser.ApplicationPassword));
                txtEmail.Text = ConvertHelper.ConvertToString(response.ApplicationUser.EmailID);
                //ddlRole.SelectedValue = response.ApplicationUser.Role != null ? ConvertHelper.ConvertToString(response.ApplicationUser.Role.RoleID) : "2";
                ddlRole.SelectedIndex = response.ApplicationUser.Role != null ? ((ConvertHelper.ConvertToInteger(response.ApplicationUser.Role.RoleID) == 1) ? 1 : 2) : 2;
                if (response.ApplicationUser.StatusID != 2)
                {
                    rbtStatus.SelectedValue = "1";
                }
                else
                {
                    rbtStatus.SelectedValue = "2";
                }
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

    #region [Display Popup]
    protected void btnPopupSubmit_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "key", "DisplayDialog();", true);
    }
    #endregion

    #region [Add ManageSystemEngineer]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            divMessage.Style.Add("display", "block");
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceURL = PostServiceURL;

            request.ApplicationUser = new ApplicationUser();
            request.ApplicationUser.Role = new Role();
            if (ConvertHelper.ConvertToString(txtPassword.Text, "") != "" && ConvertHelper.ConvertToString(txtConfirmPassword.Text, "") != "")
            {
                request.ApplicationUser.ApplicationUsername = ConvertHelper.ConvertToString(txtApplicationUserName.Text);
                request.ApplicationUser.ApplicationPassword = ConvertHelper.ConvertToString(txtPassword.Text);
                request.ApplicationUser.EmailID = ConvertHelper.ConvertToString(txtEmail.Text);
                request.ApplicationUser.Role.RoleID = ConvertHelper.ConvertToInteger(ddlRole.SelectedValue);

                request.ApplicationUser.CreatedBy = currentUser.ApplicationUserID;
                request.ApplicationUser.ModifiedBy = currentUser.ApplicationUserID;
                request.CurrentAction = CurrentAction;
                if (CurrentAction == ActionType.Edit)
                {
                    request.ApplicationUser.ApplicationUserID = ConvertHelper.ConvertToInteger(base.Id);
                    request.ApplicationUser.StatusID = ConvertHelper.ConvertToInteger(rbtStatus.SelectedValue);
                    serviceName = "SAVEAPPLICATIONUSER";
                }
                else
                {
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                    request.ApplicationUser.StatusID = ConvertHelper.ConvertToInteger(rbtStatus.SelectedValue);
                    serviceName = "SAVEAPPLICATIONUSER";

                }
                //Framing the URL
                url = string.Format(serviceURL + "{0}", serviceName);
                request.URL = url;
                response = new PTResponse();
                
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.isSuccess == true)
                {
                    ShowMessage(response.Message, true);
                    CrudManageSystemEngineer.Visible = false;
                    divGrdManageSystemEngineerInfo.Visible = true;
                    customSearch.Visible = true;
                }
                else
                {
                    ShowMessage(response.Message, false);
                    CrudManageSystemEngineer.Visible = true;
                    divGrdManageSystemEngineerInfo.Visible = false;
                }
            }
            else
            {
                ShowMessage("Please enter Password and Confirm Password", false);
                CrudManageSystemEngineer.Visible = true;
                divGrdManageSystemEngineerInfo.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Back to Grid View Mode of Corresponding ManageSystemEngineer Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudManageSystemEngineer.Visible = false;
            divGrdManageSystemEngineerInfo.Visible = true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    #endregion


}