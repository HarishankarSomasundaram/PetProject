using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserComputer : Audit
    {
        public UserComputer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserComputerID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail Computer { get; set; }
        public int ComputerID { get; set; }
        public string ComputerName { get; set; }
        public string SelectedComputerIDs { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
