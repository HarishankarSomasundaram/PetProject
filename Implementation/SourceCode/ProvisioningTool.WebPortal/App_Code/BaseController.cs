using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using Library;
using ProvisioningTool.Common;
using ProvisioningTool.Entity;
/// <summary>
/// Summary description for BaseController
/// </summary>
public class BaseController : System.Web.UI.Page
{
    public BaseController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected void Page_Init(object Sender, EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected void OnLoadComplete(object Sender, EventArgs e)
    {
        // call the base method so the page life-cycle will continue
        base.OnLoadComplete(e);
    }
    /// <summary>
    /// It Converts dd/MM/yyyy to MM/dd/yyyy format
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public DateTime ConvertDateFormat(string date)
    {
        DateTime dt = DateTime.Parse(date, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));
        return new DateTime(dt.Year, dt.Month, dt.Day);
    }

    /// <summary>
    /// It Converts dd/MM/yyyy to MM/dd/yyyy format
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public DateTime? ConvertDateFormatForReports(string date)
    {
        try
        {
            DateTime ndate = DateTime.Parse(date, System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            return new DateTime(ndate.Year, ndate.Month, ndate.Day);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// It Converts yyyy/MM/dd to MM/dd/yyyy format
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public string ConvertDateFormat(DateTime date)
    {
        return date.ToString("dd/MM/yyyy"); ;
    }

    public DateTime ConvertDate(string date)
    {

        string[] splitDate;
        DateTime myDate;
        try
        {
            splitDate = date.Split('/');
            string dateFormat = string.Concat(splitDate[0], "/", splitDate[1], "/", splitDate[2]);

            myDate = ConvertDateFormat(dateFormat);
        }
        catch (Exception ex)
        {
            myDate = DateTime.Now;
        }
        return myDate;

    }

    public DateTime ConvertDateWithTime(string date)
    {
        //string[] splitDate;
        DateTime myDate;
        try
        {
            //splitDate = date.Split('/');
            //string dateFormat = string.Concat(splitDate[0], "/", splitDate[1], "/", splitDate[2]);

            DateTime dt = DateTime.Parse(date, System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            myDate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            return myDate;
        }
        catch (Exception ex)
        {
            myDate = DateTime.Now;
        }
        return myDate;

    }

    public DateTime? ConvertDateForReports(string date)
    {

        string[] splitDate;
        DateTime myDate;
        try
        {
            splitDate = date.Split('/');
            string dateFormat = string.Concat(splitDate[0], "/", splitDate[1], "/", splitDate[2]);

            myDate = ConvertDateFormat(dateFormat);
        }
        catch (Exception ex)
        {
            myDate = DateTime.Now;
        }
        return myDate;

    }

    public DateTime ConvertDateForReports2(string date)
    {

        string[] splitDate;
        DateTime myDate;
        try
        {
            splitDate = date.Split('/');
            string dateFormat = string.Concat(splitDate[0], "/", splitDate[1], "/", splitDate[2]);

            myDate = ConvertDateFormat(dateFormat);
        }
        catch (Exception ex)
        {
            myDate = DateTime.Now;
        }
        return myDate;

    }

    public void SetPageNoCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetNoStore();
    }

    #region [ Culture ]

    /// <summary>
    /// The name of the culture selection dropdown list in the common header.
    /// </summary>
    //public string UICulture = ConfigurationManager.AppSettings["UICulture"];

    /// <summary>
    /// The name of the PostBack event target field in a posted form.  You can use this to see which
    /// control triggered a PostBack:  Request.Form[PostBackEventTarget] .
    /// </summary>
    public const string PostBackEventTarget = "__EVENTTARGET";

    /// <SUMMARY>
    /// Overriding the InitializeCulture method to set the user selected
    /// option in the current thread. Note that this method is called much
    /// earlier in the Page lifecycle and we don't have access to any controls
    /// in this stage, so have to use Form collection.
    /// </SUMMARY>
    //protected override void InitializeCulture()
    //{
    /*///<remarks><REMARKS>
    ///Check if PostBack occured. Cannot use IsPostBack in this method
    ///as this property is not set yet.
    ///</remarks>
    //if (Request[PostBackEventTarget] != null)
    {
        switch (UICulture)
        {
            case "hi-IN": SetCulture("hi-IN", "hi-IN");
                break;
            case "ta-IN": SetCulture("ta-IN", "ta-IN");
                break;
            case "en-US": SetCulture("en-US", "en-US");
                break;
            case "en-GB": SetCulture("en-GB", "en-GB");
                break;
            case "fr-FR": SetCulture("fr-FR", "fr-FR");
                break;
            default: break;
        }
    }
    ///<remarks>
    ///Get the culture from the session if the control is tranferred to a
    ///new page in the same application.
    ///</remarks>
    if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
    {
        Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
        Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
    }*/
    // base.InitializeCulture();
    //}


    /// <Summary>
    /// Sets the current UICulture and CurrentCulture based on
    /// the arguments
    /// </Summary>
    /// <PARAM name="name"></PARAM>
    /// <PARAM name="locale"></PARAM>
    protected void SetCulture(string name, string locale)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
        ///<remarks>
        ///Saving the current thread's culture set by the User in the Session
        ///so that it can be used across the pages in the current application.
        ///</remarks>
        Session["MyUICulture"] = Thread.CurrentThread.CurrentUICulture;
        Session["MyCulture"] = Thread.CurrentThread.CurrentCulture;
    }

    #endregion [ Culture ]

    public void RedirectHomePage()
    {
        //Response.Redirect("~/workspace1.aspx");
    }

    public void RedirectLoginPage()
    {
        Response.Redirect("~/App/Login.aspx");
    }
    public void RedirectLogoutPage()
    {
        Response.Redirect("~/App/Logout.aspx");
    }

    public void RedirectDashboard()
    {
        //Response.Redirect("~/App/CustomerInfo.aspx", false);
        ApplicationUser applicationUser;
        applicationUser = Session["UserDetails"] as ApplicationUser;
        if (applicationUser.Role.RoleID == (int)UserRole.Administrator)
        {
            Response.Redirect("~/App/Main.aspx", false);
        }
        else if (applicationUser.Role.RoleID == (int)UserRole.SystemEngineer)
        {
            Response.Redirect("~/App/Search.aspx", false);
        }
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
        Label lblErrorMessage = (Label)Page.FindControl("lblErrorMessage");
        Panel pnlError = (Panel)Page.FindControl("pnlError");
        HtmlGenericControl divMessage = new HtmlGenericControl();
        if (pnlError != null && lblErrorMessage != null)
            ShowMessage(Message, IsSuccess, lblErrorMessage, pnlError);
        else
        {
            //for pages has lblMessage control to display error messages
            Label lblMessage = (Label)Page.FindControl("lblMessage");
            if (lblMessage == null)//For Master pages
            {
                try
                {
                    ContentPlaceHolder cph = Page.Master.FindControl("MainContent") as ContentPlaceHolder;
                    lblMessage = (Label)cph.FindControl("lblMessage");
                    divMessage = (HtmlGenericControl)cph.FindControl("divMessage");
                    if (divMessage != null)
                        divMessage.Attributes["style"] = "display:block";
                }
                catch (Exception eee) { }
            }
            else
            {
                divMessage = (HtmlGenericControl)Page.FindControl("divMessage");
                if (divMessage != null)
                    divMessage.Attributes["style"] = "display:block";
            }
            if (lblMessage != null)
            {
                lblMessage.Text = Message;
                if (IsSuccess == true)
                {

                    lblMessage.CssClass = "sccmsg lblMsgClass";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.CssClass = "errmsg lblMsgClass";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    public void ShowMessage(string Message, bool IsSuccess, Label lblErrorMessage, Panel pnlError)
    {
        lblErrorMessage.Visible = true;
        if (string.IsNullOrEmpty(Message))
        {
            lblErrorMessage.Text = "";
            pnlError.Visible = false;
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
                pnlError.Visible = true;
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

    protected virtual void DisableControls(Control ctrl)
    {
        if (ctrl.HasControls())
        {
            foreach (Control control in ctrl.Controls)
            {
                if (control is TextBox)
                    ((TextBox)control).ReadOnly = true;
                if (control is DropDownList)
                    ((DropDownList)control).Enabled = false;
                if (control is CheckBoxList)
                    ((CheckBoxList)control).Enabled = false;
                if (control is CheckBox)
                    ((CheckBox)control).Enabled = false;
                if (control is RadioButton)
                    ((RadioButton)control).Enabled = false;
                if (control is RadioButtonList)
                    ((RadioButtonList)control).Enabled = false;
                if (control is LinkButton)
                    ((LinkButton)control).Enabled = false;
                if (control is Button)
                    ((Button)control).Enabled = false;
                if (control is Panel)
                {
                    //foreach (Control control1 in control.Controls)
                    DisableControls((Control)control);
                }
                if (control is HtmlGenericControl)
                    DisableControls((HtmlGenericControl)control);

            }
        }
    }

    public void ShowOrHide(object obj, bool isShow)
    {
        switch (obj.GetType().Name)
        {
            case "Label":
                ((Label)(obj)).Visible = isShow;
                break;
            case "TextBox":
                ((TextBox)(obj)).Visible = isShow;
                break;
            case "DropDownList":
                ((DropDownList)(obj)).Visible = isShow;
                break;
            case "HyperLink":
            case "a":
            case "HtmlAnchor":
                ((HtmlAnchor)(obj)).Visible = isShow;
                break;
            case "HtmlGenericControl":
                ((HtmlGenericControl)(obj)).Visible = isShow;
                break;
            case "LinkButton":
                ((LinkButton)(obj)).Visible = isShow;
                break;
            case "HtmlTableRow":
                ((HtmlTableRow)(obj)).Visible = isShow;
                break;
            default:
                ((TextBox)(obj)).Visible = isShow;
                break;
        }
    }
    #endregion [ Message ]

    public int ComputeDataTableIntegerTotal(DataTable dt, string Column)
    {
        int total = (from DataRow dr in dt.AsEnumerable()
                     where dr.RowState != DataRowState.Deleted
                     select Convert.ToInt32(dr[Column])).Sum();
        return total;
    }
    public decimal ComputeDataTableDecimalTotal(DataTable dt, string Column)
    {
        decimal total = (from DataRow dr in dt.AsEnumerable()
                         where dr.RowState != DataRowState.Deleted
                         select Convert.ToDecimal(dr[Column])).Sum();
        return total;
    }

    #region [ Populate Dropdown ]

    #region [ PopulateCityDropDownList ]
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ddlBrand"></param>
    /// <param name="brands"></param>
    /// <param name="includeSelect"></param>
    /// <returns></returns>
    public DropDownList PopulateCompanyDropDownList(DropDownList ddlCompany, List<Company> companies, bool includeSelect)
    {
        if (companies != null && companies.Count > 0)
        {
            ListItem li;
            ddlCompany.Items.Clear();
            if (includeSelect)
                ddlCompany.Items.Add(new ListItem("Select", "0"));
            companies.ForEach(delegate(Company c) { li = new ListItem(c.CompanyName, c.CompanyID.ToString()); ddlCompany.Items.Add(li); });
        }
        else
            return null;
        return ddlCompany;
    }
    #endregion [ PopulateCityDropDownList ]

    #region [ PopulateUsersDropDownList ]
    public DropDownList PopulateUserDropDownList(DropDownList ddlUser, List<User> Users, bool includeSelect)
    {
        if (Users != null && Users.Count > 0)
        {
            ListItem li;
            ddlUser.Items.Clear();
            if (includeSelect)
                ddlUser.Items.Add(new ListItem("Select", "0"));
            Users.ForEach(delegate(User u) { li = new ListItem(u.UserName, u.UserID.ToString()); ddlUser.Items.Add(li); });
        }
        else
            return null;
        return ddlUser;
    }
    #endregion [ PopulateUsersDropDownList ]

    #region [ PopulateRolesDropDownList ]
    public DropDownList PopulateRoleDropDownList(DropDownList ddlRole, List<Role> Roles, bool includeSelect)
    {
        if (Roles != null && Roles.Count > 0)
        {
            ListItem li;
            ddlRole.Items.Clear();
            if (includeSelect)
                ddlRole.Items.Add(new ListItem("Select", "0"));
            Roles.ForEach(delegate(Role u) { li = new ListItem(u.RoleName, u.RoleID.ToString()); ddlRole.Items.Add(li); });
        }
        else
            return null;
        return ddlRole;
    }
    #endregion [ PopulateRolesDropDownList ]

    #region [ PopulateCustomersDropDownList ]
    public DropDownList PopulateCustomerDropDownList(DropDownList ddlCustomer, List<Customer> Customers, bool includeSelect)
    {
        if (Customers != null && Customers.Count > 0)
        {
            ListItem li;
            ddlCustomer.Items.Clear();
            if (includeSelect)
                ddlCustomer.Items.Add(new ListItem("Select", "0"));
            Customers.ForEach(delegate(Customer u) { li = new ListItem(u.CustomerCode, u.CustomerID.ToString()); ddlCustomer.Items.Add(li); });
        }
        else
            return null;
        return ddlCustomer;
    }
    #endregion [ PopulateCustomersDropDownList ]

    public DropDownList PopulateMasterDetailDropDownList(DropDownList ddlMasterDetail, List<GlobalMasterDetail> companies, bool includeSelect)
    {
        if (companies != null && companies.Count > 0)
        {
            ListItem li;
            ddlMasterDetail.Items.Clear();
            if (includeSelect)
                ddlMasterDetail.Items.Add(new ListItem("Select", "0"));
            companies.ForEach(delegate(GlobalMasterDetail g) { li = new ListItem(g.MasterValue, g.MasterDetailID.ToString()); ddlMasterDetail.Items.Add(li); });
        }
        else
            return null;
        return ddlMasterDetail;
    }

    #endregion [ Populate Dropdown ]

    #region [ PopulateUsersDropDownList ]

    public void PopulateGlobalMasterDropdown(PTRequest request, DropDownList control)
    {
        string serviceResponseString = string.Empty;
        PTResponse response = new PTResponse();
        WebServiceHelper webServiceHelper = new WebServiceHelper();
        response = webServiceHelper.PostRequest<PTResponse>(request);

        if (response != null && response.GlobalMaster != null && response.GlobalMaster.GlobalMasterDetailList != null && response.GlobalMaster.GlobalMasterDetailList.Count > 0)
            PopulateMasterDetailDropDownList(control, response.GlobalMaster.GlobalMasterDetailList, true);
    }

    #endregion [ PopulateUsersDropDownList ]


    public DropDownList PopulateSiteDropDownList(DropDownList ddlCustomerSite, List<Site> sites, bool includeSelect)
    {
        if (sites != null && sites.Count > 0)
        {
            ListItem li;
            ddlCustomerSite.Items.Clear();
            if (includeSelect)
                ddlCustomerSite.Items.Add(new ListItem("Select Site", "0"));
            sites.ForEach(delegate(Site g) { li = new ListItem(g.SiteName, g.SiteID.ToString()); ddlCustomerSite.Items.Add(li); });
        }
        else
            return null;
        return ddlCustomerSite;
    }


    #region [ DropDownListColorBoxProcess ]
    public void DropDownListColorBoxProcess(DropDownList ddlName, int selectedIndex, bool isEnabled, string isColorBox)
    {
        if (isColorBox.ToLower().Trim() == "yes")
        {
            string a = ConvertHelper.ConvertToString(Request.QueryString["isColorBox"], "no");
            ddlName.Enabled = isEnabled;
            ddlName.SelectedValue = ConvertHelper.ConvertToString(selectedIndex);
        }
    }
    #endregion [ DropDownListColorBoxProcess ]

}