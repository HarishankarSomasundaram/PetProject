using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserNetworkShare : Audit
    {
        public UserNetworkShare()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserNetworkShareID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail NetworkShare { get; set; }
        public int NetworkShareID { get; set; }
        public string NetworkShareName { get; set; }
        public string SelNetworkShareItems { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
