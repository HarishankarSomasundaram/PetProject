using Library;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class includes_UserControls_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser = Session["UserDetails"] as ApplicationUser;
            lblUserName.Text = ConvertHelper.ConvertToString(applicationUser.ApplicationUsername, "");

            if (applicationUser != null && applicationUser.Role != null)
            {
                if (applicationUser.Role.RoleName == ConvertHelper.ConvertToString(UserRole.Administrator))
                {
                    //aheaderSearch.InnerText = "Customer";
                    //headerSearch.Visible = true;
                    logoLink.HRef = "../../../App/Main.aspx";
                    if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("search") ||
                        HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("managesystemengineer.aspx") ||
                        HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("customerinfo.aspx") ||
                        HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("settings.aspx") ||
                        HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("users.aspx")
                        )
                    {
                        //headerHome.Visible = true;
                        //if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("search"))
                        //{
                        //    headerSearch.Visible = false;
                        //}
                        //else
                        //{
                        //    //aheaderSearch.InnerText = "Customer";
                        //    headerSearch.Visible = true;
                        ////} 
                        //headerSysEng.Visible = false;
                        headerMaster.Visible = true;
                    }
                    //else if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("masters/customerinfo.aspx") || 
                    //    HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("masters/globalmaster.aspx") ||
                    //    HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("masters/grouppolicysetup.aspx") ||
                    //    HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("masters/harddisk.aspx") ||
                    //    HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("masters/headingmaster.aspx") ||
                    //    HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("masters/CustomerSites.aspx"))
                    //{
                    //    //headerMaster.Visible = false;
                    //    //headerSysEng.Visible = true;
                    //}

                    //headerSearch.Visible = true;
                    //headerSysEng.Visible = true;
                    //headerSettings.Visible = true;


                    //if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("search"))
                    //{
                    //    headerHome.Visible = true;
                    //    headerHome.InnerText = "Users";
                    //    headerHome.HRef = "../../../App/Users.aspx";
                    //}

                    if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("search"))
                    {
                        SiteMapPath1.Visible = false;
                    }
                    
                }
                else
                {
                    headerMaster.Visible = false;

                    //if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("search"))
                    //{
                    //    headerSearch.Visible = false;
                    //}
                    //else
                    //{
                    //    headerSearch.Visible = true;
                    //}

                    //aheaderCust.InnerText = "Change Customer";
                    //headerSysEng.Visible = false;
                    //aheaderCust.HRef = "../../../App/search.aspx";
                    SiteMapPath1.Visible = false; 
                    logoLink.HRef = "../../../App/search.aspx";
                }
            }
            else
            {
                Response.Redirect("../App/logout.aspx");
            }
        }
        else
        {
            Response.Redirect("../App/logout.aspx");
        }

        #region [Inetillize the cookie for iframe operations]

        HttpCookie isIframeCookie = new HttpCookie("isIframe");
        // Set the cookie expiration date.
        isIframeCookie.Expires = DateTime.Now.AddHours(1);
        isIframeCookie = Request.Cookies["isIframe"];

        //check the iframe operation for Add
        if (Request.QueryString["isColorBox"] != null)
        {
            if (isIframeCookie != null)
                isIframeCookie.Value = "1";

            //headderlinks.Style.Add("display", "none");
            headderlogout.Style.Add("display", "none");
            containerMain.Style.Add("display", "none");
            //headderMain.Style.Add("display", "none");
        }
        else
        {
            //headderlinks.Style.Add("display", "block");
            headderlogout.Style.Add("display", "block");
            containerMain.Style.Add("display", "block");
            //headderMain.Style.Add("display", "block");
        }
        #endregion
    }
    
}