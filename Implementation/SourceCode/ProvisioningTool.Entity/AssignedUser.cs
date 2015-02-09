using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class AssignedUser : Audit
    {
        public AssignedUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemAssignedUserID { get; set; }
        
        public SystemMaster System { get; set;}
        public int ClientID { get; set; }
        public User User { get; set; }
    }
}
