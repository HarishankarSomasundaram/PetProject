using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemMemory : Audit
    {
        public SystemMemory()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemMemoryID { get; set; }
        
        public int SystemID { get; set;}
        public  GlobalMasterDetail Memory { get; set; }
        public int Quantity { get; set; }
    
    }
}
