using System;

namespace ProvisioningTool.Entity
{

    public class RouterModule : Audit
    {
        public RouterModule()
        {
            //
            // TODO: RouterModule Add constructor logic here
            //
        }
        public int RouterModuleID { get; set; }
        public int RouterID { get; set; }
        public GlobalMasterDetail Module { get; set; }
    }
}
