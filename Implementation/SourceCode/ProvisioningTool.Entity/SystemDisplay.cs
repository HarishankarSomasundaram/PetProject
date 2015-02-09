using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemDisplay : Audit
    {
        public SystemDisplay()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemDisplayID { get; set; }
        
        public int SystemID { get; set;}
        public  GlobalMasterDetail Display { get; set; }
    }
}
