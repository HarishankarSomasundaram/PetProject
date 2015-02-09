using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Configuration;
using System.Web.Services;

public partial class Login : BaseController
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

    }
    protected void btnLogin_Click(object sender, EventArgs e)
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
            if (ConvertHelper.ConvertToString(txtUsername.Text, "") != "" && ConvertHelper.ConvertToString(txtPassword.Text, "") != "")
            {
                serviceURL = PostServiceURL + "GETAPPLICATIONUSERBYAPPLICATIONUSERNAME";
                request.ApplicationUser = new ApplicationUser();
                request.ApplicationUser.ApplicationUsername = ConvertHelper.ConvertToString(txtUsername.Text);
                request.URL = serviceURL;
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && response.ApplicationUserList != null && response.ApplicationUserList.Count > 0)
                {
                    //check the User is active or inactive 
                    if (response.Message != "User is inactive")
                    {
                        applicationUser = new ApplicationUser();
                        applicationUser = response.ApplicationUserList.Find(delegate(ApplicationUser tempapplicationUser) { return tempapplicationUser.ApplicationUsername.ToUpper() == txtUsername.Text.ToUpper() && tempapplicationUser.ApplicationPassword == txtPassword.Text; });
                        if (applicationUser != null)
                        {
                            Session["UserDetails"] = applicationUser;
                            Session["ApplicationUserId"] = applicationUser.ApplicationUserID;
                            Library.CookieHelper.CreateCookie(Page, "IsAuthenticated", true);
                            
                            RedirectDashboard();
                        }
                        else
                        {
                            Library.CookieHelper.CreateCookie(Page, "IsAuthenticated", false);
                            lblMessage.Text = "Invalid password";
                        }
                    }
                    else
                    {
                        lblMessage.Text = response.Message;
                    }

                }
                else if (response != null && response.isSuccess == false && response.Message != "")
                {
                    lblMessage.Text = response.Message;

                }
                else
                {
                    lblMessage.Text = "Invalid username";
                }
            }
            else
            {
                lblMessage.Text = "Please enter username and password";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ConvertHelper.ConvertToString(ex.Message, "");
        }

    }

    #region [Send Email On forgot Password]

    [WebMethod]
    public static string EmailForForgotPassword(string forgotusername, string email)
    {
        try
        {
            PTResponse response = new PTResponse();
            PTRequest request = new PTRequest();
            WebServiceHelper webServiceHelper = new WebServiceHelper();
            string PostServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["PostService"], "");
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;
            if (ConvertHelper.ConvertToString(email, "") != "")
            {
                serviceURL = PostServiceURL + "SENDPASSWORDBYEMAIL";
                request.ApplicationUser = new ApplicationUser();
                //request.ApplicationUser.ApplicationUsername = ConvertHelper.ConvertToString(forgotusername == null ? "" : forgotusername);
                request.ApplicationUser.EmailID = ConvertHelper.ConvertToString(email == null ? "" : email);
                request.URL = serviceURL;
                response = webServiceHelper.PostRequest<PTResponse>(request);
                if (response != null && (response.isSuccess))
                {
                    return "true";
                }
                else
                    return "false";
            }
            return "false";
        }
        catch (Exception ex)
        {
            return "false";
        }
    }
    #endregion
}