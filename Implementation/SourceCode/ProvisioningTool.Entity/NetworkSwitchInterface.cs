using System;
namespace ProvisioningTool.Entity
{
    public class NetworkSwitchInterface : Audit
    {
        public NetworkSwitchInterface()
        {
            //
            // TODO: NetworkSwitchInterface Add constructor logic here
            //
        }
        public int NetworkSwitchInterfaceID { get; set; }
        public int NetworkSwitchID { get; set; }
        public string InterfaceValue { get; set; }
    }
}
