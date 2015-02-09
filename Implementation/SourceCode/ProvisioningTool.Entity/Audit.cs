using System;

namespace ProvisioningTool.Entity
{
    public class Audit
    {
        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }         
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
