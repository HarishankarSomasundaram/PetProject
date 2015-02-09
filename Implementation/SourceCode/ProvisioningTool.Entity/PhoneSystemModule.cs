using System;
namespace ProvisioningTool.Entity
{
    public class PhoneSystemModule : Audit
    {
        public PhoneSystemModule()
        {
            //
            // TODO: RouterModule Add constructor logic here
            //
        }
        public int PhoneSystemModuleID { get; set; }
        public int PhoneSystemID { get; set; }
        public GlobalMasterDetail Module { get; set; }
    }
}
