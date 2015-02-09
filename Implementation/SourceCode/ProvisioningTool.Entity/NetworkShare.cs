using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class NetworkShare : Audit
    {
        public NetworkShare()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int NetworkShareID { get; set; }
        public string NetworkShareName { get; set; }

        public List<NetworkShareDetail> NetworkShareDetail { get; set; }
        public string NetworkShareDetailsIDs { get; set; }
        //Removing data table due to service and it is not used anywhere- Babu
        //public DataTable NetworkShareDetailsDT { get; set; }
        public string NetworkShareAssignedUserIDs { get; set; }

        public int SiteID { get; set; }
    }
}
