using System;
namespace ProvisioningTool.Entity
{
    public class PhoneSystemInterface : Audit
    {
        public PhoneSystemInterface()
        {
            //
            // TODO: RouterInterface Add constructor logic here
            //
        }
        public int PhoneSystemInterfaceID { get; set; }
        public int PhoneSystemID { get; set; }
        public string InterfaceValue { get; set; }
    }
}
