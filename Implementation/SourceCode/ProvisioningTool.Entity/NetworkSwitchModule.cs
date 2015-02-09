using System;

namespace ProvisioningTool.Entity
{

    public class NetworkSwitchModule : Audit
    {
        public NetworkSwitchModule()
        {
            //
            // TODO: NetworkSwitchModule Add constructor logic here
            //
        }
        public int NetworkSwitchModuleID { get; set; }
        public int NetworkSwitchID { get; set; }
        public GlobalMasterDetail Module { get; set; }
    }
}
