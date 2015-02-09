using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserRemoteAccess : Audit
    {
        public UserRemoteAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserRemoteAccessID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail RemoteAccess { get; set; }
        public int RemoteAccessID { get; set; }
        public string RemoteAccessName { get; set; }
        public string SelRemoteAccessItems { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
