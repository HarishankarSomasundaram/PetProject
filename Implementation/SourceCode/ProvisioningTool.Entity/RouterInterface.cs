using System;
namespace ProvisioningTool.Entity
{
    public class RouterInterface : Audit
    {
        public RouterInterface()
        {
            //
            // TODO: RouterInterface Add constructor logic here
            //
        }
        public int RouterInterfaceID { get; set; }
        public int RouterID { get; set; }
        public string InterfaceValue { get; set; }
    }
}
