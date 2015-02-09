using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;

public partial class Settings : FormController
{
    #region [ Variable Declarations ]
    string ucpath = string.Empty;
    string tabname = string.Empty;
    string menuType = string.Empty;

    #endregion [ Variable Declarations ]


    #region [ Page Load Events]

    protected void Page_Load(object sender, EventArgs e)
    {
        menuType = Request.QueryString["hidglobal"];
        ucpath = Request.QueryString["ucPath"];
        tabname = Request.QueryString["tabName"];

        if (Request.QueryString["navPage"] != null)
        {
           GetUserContaolPath(Request.QueryString["navPage"]);
        }

        if (Request.QueryString["trans"] != null)
        {
            if (menuType == "1")
            {
                GetUserContaolPath("GLOBAL");
            }
            else
            {
                if (tabname != null)
                    GetUserContaolPath(tabname);
            }
        }
    }

    #endregion [ Page Load Events]

    protected void btnCustomerSubmit_Click(object sender, EventArgs e)
    {
        menuType = hidglobal.Value != null ? hidglobal.Value : "";

        tabname = hidTabname.Value != null ? hidTabname.Value : "";

        if (menuType == "1")
        {
            GetUserContaolPath("GLOBAL");
        }
        else
        {
            GetUserContaolPath(tabname);
        }
    }

    private void GetUserContaolPath(string UCPath)
    {

        //if (Request.QueryString["id"] == null && Request.QueryString["do"] == null)
        //{

        //    RemoveQueryStringParams("navPage");
        //    RemoveQueryStringParams("do");
        //    RemoveQueryStringParams("id");
        //    RemoveQueryStringParams("nav");
        //    RemoveQueryStringParams("opp");
        //}

        switch (UCPath.ToUpper())
        {
            case "GLOBAL":
                SavedControlVirtualPath = "~/includes/UserControls/pages/GlobalMaster.ascx";
                ReloadControl(UCPath);
                break;
            case "HARD DRIVE":
                SavedControlVirtualPath = "~/includes/UserControls/pages/HardDisk.ascx";
                ReloadControl(UCPath);
                break;
            case "USERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/UserInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "ROUTERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/RouterInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "FIREWALLS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/FirewallInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "NETWORK SWITCHES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/NetworkSwitchInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "SERVERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/ServerInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "MOBILE DEVICES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/MobileDeviceInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "PHONE SYSTEM":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PhoneSystemInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "WORKSTATIONS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/WorkStation.ascx";
                ReloadControl(UCPath);
                break;
            case "LAPTOPS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/Laptop.ascx";
                ReloadControl(UCPath);
                break;
            case "PRINTERS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PrinterInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "NETWORK SHARES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/NetworkShare.ascx";
                ReloadControl(UCPath);
                break;
            case "WIRELESS":
                SavedControlVirtualPath = "~/includes/UserControls/pages/WirelessInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "PAGEUNDERCONSTRUCTION":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PageUnderConstruction.ascx";
                ReloadControl(UCPath);
                break;
            case "SOFTWARES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/SoftwareInfo.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-DOMAIN":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-PROVIDER":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-EMAIL":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "INTERNET/WEB-WEBHOST":
                SavedControlVirtualPath = "~/includes/UserControls/pages/InternetWeb.ascx";
                ReloadControl(UCPath);
                break;
            case "GROUP POLICIES":
                SavedControlVirtualPath = "~/includes/UserControls/pages/GroupPolicy.ascx";
                ReloadControl(UCPath);
                break;
            case "SYSTEM AUDIT":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PageUnderConstruction.ascx";
                ReloadControl(UCPath);
                break;
            case "SUPPORT TEAM":
                SavedControlVirtualPath = "~/includes/UserControls/pages/PageUnderConstruction.ascx";
                ReloadControl(UCPath);
                break;
            case "PROVISIONING CHECK LIST":
                SavedControlVirtualPath = "~/includes/UserControls/pages/ProvisioningCheckList.ascx";
                ReloadControl(UCPath);
                break;

        }
    }


    private string SavedControlVirtualPath
    {
        get
        {
            if (ViewState["saved"] == null ||
                (string)ViewState["saved"] == string.Empty)
                return null;
            return (string)ViewState["saved"];
        }
        set
        {
            ViewState["saved"] = value;
        }
    }


    private void ReloadControl(string UCPath)
    {

        ControlContainerSetting.Controls.Clear();
        if (SavedControlVirtualPath != null)
        {
            Control control = this.LoadControl(SavedControlVirtualPath);
            if (control != null)
            {
                //Gives the control a unique ID. It is important to ensure
                //the page working properly. Here we use control.GetType().Name
                //as the ID.
                control.ID = control.GetType().Name;
                ControlContainerSetting.Controls.Add(control);
            }
        }
    }

    protected void RemoveQueryStringParams(string rname)
    {
        // reflect to readonly property
        PropertyInfo isReadOnly =
        typeof(System.Collections.Specialized.NameValueCollection)
        .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        // make collection editable
        isReadOnly.SetValue(this.Request.QueryString, false, null);
        // remove
        this.Request.QueryString.Remove(rname);
        // make collection readonly again
        isReadOnly.SetValue(this.Request.QueryString, true, null);
    }
}

