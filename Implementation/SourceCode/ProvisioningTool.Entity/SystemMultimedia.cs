using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemMultimedia : Audit
    {
        public SystemMultimedia()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemMultimediaID { get; set; }
        
        public int SystemID { get; set;}
        public  GlobalMasterDetail Multimedia { get; set; }
    }
}
