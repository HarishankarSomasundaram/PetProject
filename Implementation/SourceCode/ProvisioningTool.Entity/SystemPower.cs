using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemPower : Audit
    {
        public SystemPower()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemPowerID { get; set; }
        
        public int SystemID { get; set;}
        public  GlobalMasterDetail Power { get; set; }
    }
}
