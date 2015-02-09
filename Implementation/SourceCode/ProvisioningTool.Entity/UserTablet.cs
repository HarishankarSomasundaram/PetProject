using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserTablet : Audit
    {
        public UserTablet()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserTabletID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail Tablet { get; set; }
        public int TabletID { get; set; }
        public string TabletName { get; set; }
        public string SelectedTabletIDs { get; set; }
        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
