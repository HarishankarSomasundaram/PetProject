using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class GroupPolicy : Audit
    {
        public GroupPolicy()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int GroupPolicyID { get; set; }
        public GroupPolicySetup GroupPolicySetup { get; set; }
        public int GroupPolicySetupID { get; set; }
        public string GroupPolicyFieldValue { get; set; }

        public int SiteID { get; set; }
    }
}
