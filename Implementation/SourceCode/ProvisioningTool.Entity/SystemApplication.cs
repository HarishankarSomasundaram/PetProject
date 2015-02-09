using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemApplication : Audit
    {
        public SystemApplication()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int SystemApplicationID { get; set; }
        public SystemMaster System { get; set; }
        public int ClientID { get; set; }
        public GlobalMasterDetail Application { get; set; }
        public string LicenseKey { get; set; }
    }
}
