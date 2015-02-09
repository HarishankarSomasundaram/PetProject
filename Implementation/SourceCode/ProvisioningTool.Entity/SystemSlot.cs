using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemSlot : Audit
    {
        public SystemSlot()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemSlotID { get; set; }
        
        public int SystemID { get; set;}
        public  GlobalMasterDetail Slot { get; set; }
    }
}
