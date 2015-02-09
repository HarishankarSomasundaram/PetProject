using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserLaptops : Audit
    {
        public UserLaptops()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserLaptopID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail Laptop { get; set; }
        public int LaptopID { get; set; }
        public string LaptopName { get; set; }
        public string SelLaptopItems { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
