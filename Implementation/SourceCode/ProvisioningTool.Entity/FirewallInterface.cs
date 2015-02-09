using System;
namespace ProvisioningTool.Entity
{
    public class FirewallInterface : Audit
    {
        public FirewallInterface()
        {
            //
            // TODO: FirewallInterface Add constructor logic here
            //
        }
        public int FirewallInterfaceID { get; set; }
        public int FirewallID { get; set; }
        public string InterfaceValue { get; set; }
    }
}
