using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemPort : Audit
    {
        public SystemPort()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemPortID { get; set; }
        
        public int SystemID { get; set;}
        public  GlobalMasterDetail Port { get; set; }
    }
}
