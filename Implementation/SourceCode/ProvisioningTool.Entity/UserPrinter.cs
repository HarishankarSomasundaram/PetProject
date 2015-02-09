using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProvisioningTool.Entity;

namespace ProvisioningTool.Entity
{

    public class UserPrinter : Audit
    {
        public UserPrinter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UserPrinterID { get; set; }
        public int UserID { get; set; }

        public GlobalMasterDetail Printer { get; set; }
        public int PrinterID { get; set; }
        public string PrinterName { get; set; }
        public string SelectedPrinterIDs { get; set; }

        //public string oper { get; set; }
        //public int id { get; set; }

    }
}
