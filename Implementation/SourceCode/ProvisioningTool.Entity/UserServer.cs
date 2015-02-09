using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserServer : Audit
    {
        public UserServer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserServerID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail Server { get; set; }
        public int ServerID { get; set; }
        public string ServerName { get; set; }
        public string SelServerItems { get; set; }
        public string SelectedServerIDs { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
