using System;

namespace ProvisioningTool.Entity
{

    public class FirewallModule : Audit
    {
        public FirewallModule()
        {
            //
            // TODO: FirewallModule Add constructor logic here
            //
        }
        public int FirewallModuleID { get; set; }
        public int FirewallID { get; set; }
        public GlobalMasterDetail Module { get; set; }
    }
}
