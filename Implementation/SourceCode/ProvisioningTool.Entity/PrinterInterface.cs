using System;
namespace ProvisioningTool.Entity
{
    public class PrinterInterface : Audit
    {
        public PrinterInterface()
        {
            //
            // TODO: PrinterInterface Add constructor logic here
            //
        }
        public int PrinterInterfaceID { get; set; }
        public int PrinterID { get; set; }
        public string InterfaceValue { get; set; }
    }
}
