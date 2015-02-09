using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class GroupPolicySetup : Audit
    {
        public GroupPolicySetup()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int GroupPolicySetupID { get; set; }
        public int HeadingID { get; set; }
        public int HeadingCount { get; set; }
        public string HeadingName { get; set; }
        public HeadingMaster HeadingMaster { get; set; }
        public int FieldCount { get; set; }
        public string FieldName { get; set; }
        public int FieldType { get; set; }
        public string FieldTypeName { get; set; }
        public FieldTypeMaster FieldTypeMaster { get; set; }
        public bool IsRequired { get; set; }

        public int SiteID { get; set; }
        public int CustomerID { get; set; }
        
        public string View { get; set; }

    }
}
