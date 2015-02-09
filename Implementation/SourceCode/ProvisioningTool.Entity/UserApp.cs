using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserApp : Audit
    {
        public UserApp()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserAppID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail App { get; set; }
        public int AppID { get; set; }
        public string AppName { get; set; }
        public string SelectedAppIDs { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
