using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class PhoneSystemAssignedUser : Audit
    {
        public PhoneSystemAssignedUser()
        {
            //
            // TODO: PhoneSystem Add constructor logic here
            //
        }
        public int PhoneSystemAssignedUserID { get; set; }
        public int PhoneSystemID { get; set; }
        public User User { get; set; }
    }
}
