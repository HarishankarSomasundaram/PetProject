using System;

namespace ProvisioningTool.Entity
{

    public class PrinterModule : Audit
    {
        public PrinterModule()
        {
            //
            // TODO: PrinterModule Add constructor logic here
            //
        }
        public int PrinterModuleID { get; set; }
        public int PrinterID { get; set; }
        public GlobalMasterDetail Module { get; set; }
    }
}
