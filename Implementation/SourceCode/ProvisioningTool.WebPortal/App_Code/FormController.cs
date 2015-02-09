using System;
using System.Web.Security;
using Library;
using System.Collections.Generic;
using System.Linq;
using ProvisioningTool.Entity;
using ProvisioningTool.BLL;
using System.Configuration;
using System.Web;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
/// <summary>
/// Summary description for FormController
/// </summary>
public class FormController : BaseController
{
    public ApplicationUser currentUser { get; set; }
    public ActionType CurrentAction { get; set; }
    public string Id { get; set; }
    public string sessionSiteId { get; set; }
    public string sessionCustomerId { get; set; }
    public string searchFilter { get; set; }
    PTResponse response = new PTResponse();
    ApplicationUserBLL applicationUserBLL = new ApplicationUserBLL();
    public FormController() { }
    public string BaseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    public string MasterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    public string PostService = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["PostService"], "");
    public string GetService = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["GetService"], "");
    public string GetServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["GetService"], "");
    public string PostServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "") + ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["PostService"], "");

    #region [ GlobalMasters Tables MasterName ]
    public string SiteCityName = "Cities";
    public string SiteStateName = "States";
    public string SiteCountryName = "Countries";
    public string SiteAccRep = "Account Rep";
    public string SitePriEng = "Primary Engineers";

    #endregion [ GlobalMasters Tables MasterName ]

    protected void Page_Init(object Sender, EventArgs e)
    {
         

        currentUser = GetApplicationUserByUserName();

        if (currentUser == null)
        {
            RedirectLoginPage();
        }

        //Implemening Role based Authorization start
        else
        {
          
            if (currentUser.Role.RoleID != (int) UserRole.Administrator)
            {
                List<string> AdminURL = new List<string>();
                AdminURL.Add("/App/Main.aspx");
                AdminURL.Add("/App/Masters/CustomerInfo.aspx");
                AdminURL.Add("/App/Users.aspx");
                AdminURL.Add("/App/Settings.aspx");
                AdminURL.Add("/App/Controls.aspx");
                AdminURL.Add("/App/CustomerSearch.aspx");

                foreach(string objAdminURL in AdminURL)
                {
                    if(objAdminURL.ToLower() ==this.Request.Url.AbsolutePath.ToLower())
                    {
                        RedirectLogoutPage();
                    }
                }
            
            }
            
        }
        //end

        string sPageType = Request["do"] + "";
        switch (sPageType)
        {
            case "v":
                CurrentAction = ActionType.View;
                break;
            case "a":
                CurrentAction = ActionType.Add;
                break;
            case "e":
                CurrentAction = ActionType.Edit;
                break;
            case "p":
                CurrentAction = ActionType.Print;
                break;
            case "m":
                CurrentAction = ActionType.MoreView;
                break;
            default:
                CurrentAction = ActionType.View;
                break;
        }

        if (this.Request.QueryString["search"] != null)
        {
            if (this.Request.QueryString["CID"] != null)
            {
                this.Session["sessionCustomerId"] = this.Request.QueryString["CID"];

                //this.Response.Redirect("CustomerInfo.aspx?adv=1", true);
                sessionCustomerId = this.Session["sessionCustomerId"] + "";

                HttpCookie cookieCustomerId = new HttpCookie("sessionCustomerId");
                // Set the cookie value.
                cookieCustomerId.Value = this.Request.QueryString["CID"];
                // Set the cookie expiration date.
                cookieCustomerId.Expires = DateTime.Now.AddHours(1);
                // Add the cookie.
                Response.Cookies.Add(cookieCustomerId);
            }
        }
        else
        {
            if (this.Session["sessionCustomerId"] != null)
            {
                sessionCustomerId = this.Session["sessionCustomerId"] + "";
                HttpCookie cookieCustomerId = new HttpCookie("sessionCustomerId");
                // Set the cookie value.
                cookieCustomerId.Value = sessionCustomerId;
                // Set the cookie expiration date.
                cookieCustomerId.Expires = DateTime.Now.AddHours(1);
                // Add the cookie.
                Response.Cookies.Add(cookieCustomerId);
            }
        }

        Id = Request["id"] + "";

        HttpCookie siteIDCookie = new HttpCookie("siteID");
        siteIDCookie.Expires = DateTime.Now.AddHours(1);
        siteIDCookie = Request.Cookies["siteID"];
        if (siteIDCookie != null)
        {
            sessionSiteId = siteIDCookie.Value;
        }

        HttpCookie sessionCustomerIdCookie = new HttpCookie("sessionCustomerId");
        sessionCustomerIdCookie.Expires = DateTime.Now.AddHours(1);
        sessionCustomerIdCookie = Request.Cookies["sessionCustomerId"];
        if (sessionCustomerIdCookie != null && sessionCustomerId == null)
        {
            sessionCustomerId = sessionCustomerIdCookie.Value;
        }

        HttpCookie searchFilterCookie = new HttpCookie("searchFilter");
        searchFilterCookie.Expires = DateTime.Now.AddHours(1);
        searchFilterCookie = Request.Cookies["searchFilter"];
        if (searchFilterCookie != null)
        {
            searchFilter = searchFilterCookie.Value;
        }

        Session["CurrentLoggedInUser"] = currentUser;
        Session["CurrentLoggedInUserName"] = currentUser.ApplicationUsername;
        Session["CurrentLoggedInAppID"] = currentUser.ApplicationUserID;
        base.OnPreInit(e);
    }

    private ApplicationUser GetApplicationUserByUserName()
    {
        string username = ConvertHelper.ConvertToString(CookieHelper.GetCookie(System.Web.HttpContext.Current.Request, "UserName"));
        bool IsAuthenticated = ConvertHelper.ConvertToBoolean(CookieHelper.GetCookie(System.Web.HttpContext.Current.Request, "IsAuthenticated"));
        ApplicationUser applicationUser;
        if (IsAuthenticated)
        {
            applicationUser = Session["UserDetails"] as ApplicationUser;
        }
        else
            return null;

        return applicationUser;
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        // call the base method so the page life-cycle will continue
        base.OnLoadComplete(e);
    }

    #region [ Message ]
    /// <summary>
    /// Show Error Message
    /// </summary>
    /// <param name="Message"></param>
    public void ShowMessage(string Message)
    {
        ShowMessage(Message, false);
    }
    public void HideMessage()
    {
        Label lblErrorMessage = (Label)Page.FindControl("lblErrorMessage");
        Panel pnlError = (Panel)Page.FindControl("pnlError");
        HtmlGenericControl divMessage = new HtmlGenericControl();
        //for pages has lblMessage control to display error messages
        Label lblMessage = (Label)Page.FindControl("lblMessage");
        if (lblMessage == null)//For Master pages
        {
            ContentPlaceHolder cph = Page.Master.FindControl("MainContent") as ContentPlaceHolder;
            lblMessage = (Label)cph.FindControl("lblMessage");
            divMessage = (HtmlGenericControl)cph.FindControl("divMessage");
        }
        else
        {
            divMessage = (HtmlGenericControl)Page.FindControl("divMessage");
        }
        if (divMessage != null)
            divMessage.Attributes["style"] = "display:none";
    }
    public void ShowMessage(string Message, bool IsSuccess)
    {
        Label lblErrorMessage = (Label)FindControl("lblErrorMessage");

        ShowMessage(Message, IsSuccess, lblErrorMessage);
    }
    public void ShowMessage(string Message, bool IsSuccess, Label lblErrorMessage)
    {
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
                {
                    //lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                    lblErrorMessage.Attributes.Add("class", "divGreenMessage");

                }
                else
                {
                    //lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessage.Attributes.Add("class", "divRedMessage");
                }
            }
        }
    }
    public void ShowGlobalMessage(string message)
    {
        /*
        HtmlGenericControl objHTML = (HtmlGenericControl)Page.FindControl("emsBreadCrumb").FindControl("globalsuccess");
        Label lblGlobalMessage = (Label)Page.FindControl("emsBreadCrumb").FindControl("lblGlobalMessage");
        lblGlobalMessage.Text = message;
        objHTML.Visible = true;
         */
    }

    public void ShowGlobalErrorMessage(string message)
    {
        /*
        HtmlGenericControl objHTML = (HtmlGenericControl)Page.FindControl("emsBreadCrumb").FindControl("globalerror");
        Label lblGlobalErrorMessage = (Label)Page.FindControl("emsBreadCrumb").FindControl("lblGlobalErrorMessage");
        lblGlobalErrorMessage.Text = message;
        objHTML.Visible = true;
         */
    }



    #endregion [ Message ]

    public void InetilizeIframe(HtmlGenericControl divCrud, HtmlGenericControl divGrid)
    {
        if (currentUser.ApplicationUserID != ConvertHelper.ConvertToInteger(UserRole.Administrator))
        {
            //Do nothing for 
        }
        else
        {
            #region [Inetillize the cookie for iframe operations]

            string isIframe = Request.QueryString["iframe"] != null ? ConvertHelper.ConvertToString(Request.QueryString["iframe"], "") : "";
            string isIframeOperation = Request.QueryString["iframedo"] != null ? ConvertHelper.ConvertToString(Request.QueryString["iframedo"], "") : "";

            HttpCookie isIframeCookie = new HttpCookie("isIframe");
            // Set the cookie expiration date.
            isIframeCookie.Expires = DateTime.Now.AddHours(1);
            isIframeCookie = Request.Cookies["isIframe"];

            HttpCookie isIframeCookieOperation = new HttpCookie("isIframeCookieOperation");
            // Set the cookie expiration date.
            isIframeCookieOperation.Expires = DateTime.Now.AddHours(1);
            isIframeCookieOperation = Request.Cookies["isIframeCookieOperation"];

            //check the iframe operation for Add
            if (isIframe != "" && isIframeOperation != "" && isIframe == "1" && isIframeOperation == "a")
            {
                CurrentAction = ActionType.Add;
                if (isIframeCookie != null)
                    isIframeCookie.Value = "1";
            }
            //check the iframe operation for Edit
            else if (isIframe != "" && isIframeOperation != "" && isIframe == "1" && isIframeOperation == "e")
            {
                if (isIframeCookieOperation != null)
                {
                    isIframeCookieOperation.Value = "e";
                }
                if (isIframeCookie != null)
                {
                    isIframeCookie.Value = "1";
                    if (divCrud != null && divGrid != null)
                    {
                        divCrud.Visible = false;
                        divGrid.Visible = true;
                    }
                }
            }
            else
            {
                if (isIframeCookie != null)
                    isIframeCookie.Value = "";
                if (isIframeCookieOperation != null)
                    isIframeCookieOperation.Value = "";

            }
            #endregion [Inetillize the cookie for iframe operations]

        }
    }

    #region [ Set Drop down list selected Item by Text ]

    //Function for Set Dropdown
    public void SetDropdown(DropDownList ddl, string sValue)
    {
        try
        {
            if (sValue != "")
            {
                ddl.SelectedIndex = -1;
                foreach (ListItem item in ddl.Items)
                {
                    if (item.Text.Equals(sValue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        item.Selected = true;
                        break;
                    }
                }
                // ddl.SelectedValue = ddlCity.Items.FindByText(sValue).Value;
                ddl.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    #endregion [ Set Drop down list selected Item by Text ]

    #region [ Add new Global Master Detail's Value ]

    //Fucntion will Check the Global Master Details value available and insert a new value
    public bool GlobalMasterDetailsAdd(string sMasterName, string sMasterValue)
    {
        try
        {
            PTResponse gmdResponse = new PTResponse();
            PTRequest gmdRequest = new PTRequest();
            GlobalMasterDetail globalMasterDetail = new GlobalMasterDetail();
            globalMasterDetail.MasterValue = sMasterValue;
            globalMasterDetail.CreatedBy = currentUser.ApplicationUserID;
            globalMasterDetail.ModifiedBy = currentUser.ApplicationUserID;
            globalMasterDetail.SiteName = sMasterName;
            globalMasterDetail.oper = "ADD";
            gmdRequest.GlobalMasterDetail = new GlobalMasterDetail();
            gmdRequest.GlobalMasterDetail = globalMasterDetail;

            WebServiceHelper webServiceHelper = new WebServiceHelper();
            // string serviceURL = string.Empty;

            // serviceURL = PostServiceURL + "GlobalMasterCrud";
            gmdRequest.URL = string.Format(PostServiceURL + "{0}", "GlobalMasterCrud");
            gmdResponse = new PTResponse();
            gmdResponse = webServiceHelper.PostRequest<PTResponse>(gmdRequest);

            if (gmdResponse != null)
                return gmdResponse.isSuccess;
            else
                return false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }
    #endregion [ Add new Global Master Detail's Value ]

}
