using System;
using System.Web;

public partial class Logout : BaseController
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Clear();
        Session.Clear();
        HttpContext.Current.Session.Abandon();
        Session.Abandon();
        //Library.CookieHelper.CreateCookie(Page, "nav-item", false);
        Response.Cookies.Remove("IsAuthenticated");
        RedirectLoginPage();
    }
}