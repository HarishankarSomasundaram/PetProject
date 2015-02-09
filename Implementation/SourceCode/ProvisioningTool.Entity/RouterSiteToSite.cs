using System;
namespace ProvisioningTool.Entity
{
    public class RouterSiteToSite : Audit
    {
        public RouterSiteToSite()
        {
            //
            // TODO: RouterInterface Add constructor logic here
            //
        }
        public int RouterSiteToSiteID { get; set; }
        public int RouterID { get; set; }
        public string SiteToSiteValue { get; set; }
        public string PassKey { get; set; }
    }
}
