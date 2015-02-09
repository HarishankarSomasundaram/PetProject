using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class NetworkShareDetail : Audit
    {
           public NetworkShareDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int NetworkShareDetailID { get; set; }
        public int NetworkShareID { get; set; }
        public string NetworkShareName { get; set; }

        public string Mapped { get; set; }
        public string Path { get; set; }
        public string NetworkShareDescription { get; set; }
        public NetworkShare NetworkShare { get; set; }
        public List<AssignedUser> NetworkShareAssignedUsers { get; set; }

        public string NetworkShareAssignedUserIDs { get; set; }
        public string View { get; set;}
    }
}
