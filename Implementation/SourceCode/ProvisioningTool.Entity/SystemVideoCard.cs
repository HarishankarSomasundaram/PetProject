using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemVideoCard : Audit
    {
        public SystemVideoCard()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemVideoCardID { get; set; }
        
        public int SystemID { get; set;}
        public  GlobalMasterDetail VideoCard { get; set; }
    }
}
