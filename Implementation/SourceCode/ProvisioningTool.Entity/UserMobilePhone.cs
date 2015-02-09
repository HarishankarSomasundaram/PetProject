using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserMobilePhone : Audit
    {
        public UserMobilePhone()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserMobilePhoneID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail MobilePhone { get; set; }
        public int MobilePhoneID { get; set; }
        public string MobilePhoneName { get; set; }
        public string SelectedMobilePhoneIDs { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
