using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemRole
    {
        public SystemRole()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int SystemRoleID { get; set; }

        public SystemMaster System { get; set; }
        public int ClientID { get; set; }
        public GlobalMasterDetail Role { get; set; }
    }
}
