
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class MobileDevice : Audit
    {
        public MobileDevice()
        {
            //
            // TODO: MobileDevice Add constructor logic here
            //
        }
        public int MobileDeviceID { get; set; }
        public string Hostname { get; set; }
        public GlobalMasterDetail MobileDeviceType { get; set; }
        public GlobalMasterDetail MobileDeviceManufacture { get; set; }
        public GlobalMasterDetail MobileDeviceModel { get; set; }
        public User AssignedUser { get; set; }
        public string InstalledOn { get; set; }

    }
}
