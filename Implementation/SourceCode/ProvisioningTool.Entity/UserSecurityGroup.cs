using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserSecurityGroup : Audit
    {
        public UserSecurityGroup()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserSecurityGroupID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail SecurityGroup { get; set; }
        public int SecurityGroupID { get; set; }
        public string SecurityGroupName { get; set; }
        public string SelectedSecurityGroupIDs { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
