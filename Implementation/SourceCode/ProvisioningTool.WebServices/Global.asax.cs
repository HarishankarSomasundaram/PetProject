using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

/// <summary>
/// Summary description for Global
/// </summary>

public class Global : System.Web.HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        //RouteTable.Routes.Add(new ServiceRoute("ProvisioningToolServices", new WebServiceHostFactory(), typeof(ProvisioningToolServices.ProvisioningToolServices)));
        RegisterRoutes(RouteTable.Routes);
    }

    private void RegisterRoutes(RouteCollection routes)
    {
        routes.Add(new ServiceRoute("ProvisioningToolServices",
                   new WebServiceHostFactory(), typeof(ProvisioningToolServices.ProvisioningToolServices)));
    }

    protected void Session_Start(object sender, EventArgs e)
    {

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        EnableCrossDomainAjaxCall();
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {

    }

    protected void Application_Error(object sender, EventArgs e)
    {

    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {

    }

    private void EnableCrossDomainAjaxCall()
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization, Accept");
            HttpContext.Current.Response.End();
        }
    }

}