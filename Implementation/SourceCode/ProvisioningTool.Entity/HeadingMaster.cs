using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class HeadingMaster : Audit
    {
        public HeadingMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int HeadingMasterID { get; set; }
        public string HeadingMasterName { get; set; }
        public int Priority { get; set; }

        
    }
}
