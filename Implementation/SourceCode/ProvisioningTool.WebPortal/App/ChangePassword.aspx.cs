using System;
using System.Configuration;
using ProvisioningTool.Entity;
using Library;
using System.Web.UI.WebControls;

public partial class App_ChangePassword : FormController
{
    #region [ Declaration ]

    PTResponse response;
    ApplicationUser applicationUser;
    PTRequest request;
    WebServiceHelper webServiceHelper;
    public string PostServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["PostService"], "");
    string serviceURL;
    string url;
    string serviceName;
    string serviceResponseString;
    #endregion [ Declaration ]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) { Page.Validate(); }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            response = new PTResponse();
            request = new PTRequest();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceResponseString = string.Empty;

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                ShowMessage("Password mismatch", false, lblMessage); ;
                return;
            }
            else
            {
                serviceURL = PostServiceURL + "GETAPPLICATIONUSERBYAPPLICATIONUSERNAME";
                request.ApplicationUser = new ApplicationUser();
                applicationUser = (ApplicationUser)Session["UserDetails"];
                request.ApplicationUser.ApplicationUsername = ConvertHelper.ConvertToString(applicationUser.ApplicationUsername);
                request.URL = serviceURL;
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.ApplicationUserList != null && response.ApplicationUserList.Count > 0)
                {
                    if (txtOldPassword.Text != response.ApplicationUserList[0].ApplicationPassword)
                    {
                        ShowMessage("Invalid old password",false,lblMessage);;
                        return;
                    }
                    else
                    {
                        serviceURL = PostServiceURL + "SAVEAPPLICATIONUSER";
                        request.CurrentAction = ActionType.Edit;
                        request.ApplicationUser = new ApplicationUser();
                        request.ApplicationUser.Role = new Role();
                        request.ApplicationUser.ApplicationUserID = ConvertHelper.ConvertToInteger(response.ApplicationUserList[0].ApplicationUserID);
                        request.ApplicationUser.ApplicationUsername = ConvertHelper.ConvertToString(response.ApplicationUserList[0].ApplicationUsername);
                        request.ApplicationUser.ApplicationPassword = ConvertHelper.ConvertToString(txtNewPassword.Text);
                        request.ApplicationUser.ModifiedBy = currentUser.ApplicationUserID;
                        request.ApplicationUser.Role.RoleID = ConvertHelper.ConvertToInteger(response.ApplicationUserList[0].Role.RoleID);
                        request.ApplicationUser.StatusID = ConvertHelper.ConvertToInteger(response.ApplicationUserList[0].StatusID);
                        request.ApplicationUser.EmailID = ConvertHelper.ConvertToString(response.ApplicationUserList[0].EmailID);

                        webServiceHelper = new WebServiceHelper();
                        request.URL = serviceURL;
                        response = webServiceHelper.PostRequest<PTResponse>(request);
                        if (response != null && response.isSuccess == true)
                        {
                            ShowMessage("Password has been changed successfully",true,lblMessage);;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error occured while changing password " + ex.Message,false,lblMessage);
        }
    }

    public void ShowMessage(string Message, bool IsSuccess, Label lblErrorMessage)
    {
        lblErrorMessage.Visible = true;
        if (string.IsNullOrEmpty(Message))
        {
            lblErrorMessage.Text = "";
        }
        else
        {
            if (lblErrorMessage != null)
            {
                lblErrorMessage.Text = Message;
                if (IsSuccess == true)
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                else
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        applicationUser = (ApplicationUser)Session["UserDetails"];
        if (applicationUser.Role.RoleName == ConvertHelper.ConvertToString(UserRole.Administrator)){
            Response.Redirect("~/App/Main.aspx", false);
        }
        else {
            Response.Redirect("~/App/Search.aspx", false);
        }
    }
}