using System;
namespace ProvisioningTool.Entity
{
    public class FirewallSiteToSite : Audit
    {
        public FirewallSiteToSite()
        {
            //
            // TODO: Firewall Site to site Add constructor logic here
            //
        }
        public int FirewallSiteToSiteID { get; set; }
        public int FirewallID { get; set; }
        public string SiteToSiteValue { get; set; }
        public string PasswordKey { get; set; }
        public string SiteToSitePassKey { get; set; }
        
    }
}
