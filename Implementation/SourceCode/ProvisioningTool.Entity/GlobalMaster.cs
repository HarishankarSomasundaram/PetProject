using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class GlobalMaster 
    {
        public GlobalMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int MasterID { get; set; }
        public string MasterName { get; set; }
        public string MasterDescription { get; set; }
        public bool IsGlobalMaster { get; set; }
        public List<GlobalMasterDetail> GlobalMasterDetailList { get; set; }
        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
