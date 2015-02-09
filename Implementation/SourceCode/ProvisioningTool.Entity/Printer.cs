using System;
using System.Collections.Generic;

namespace ProvisioningTool.Entity
{
    public class Printer : Audit
    {
        public Printer()
        {
            //
            // TODO: Printer Add constructor logic here
            //
        }
        public int PrinterID { get; set; }
        public string Hostname { get; set; }
        public string Manufacture { get; set; }
        public GlobalMasterDetail PrinterModel { get; set; }
        
        public string SerialNumber { get; set; }
        public string InstalledOn { get; set; }
        public string WarrantyExpiresOn { get; set; }
        public string IPAddress { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public List<PrinterModule> PrinterModuleList { get; set; }
        public List<AssignedUser> PrinterAssignedUserList { get; set; }
        public GlobalMasterDetail OSVersion { get; set; }
        public string Firmware { get; set; }
        public List<PrinterInterface> PrinterInterfaceList { get; set; }

        public NotesMaster Notes { get; set; }
        public string View { get; set; }
        public Site Site { get; set; }
        public Documents Documents { get; set; }
        public string ViewDocumentPath { get; set; }
        public string PrinterInterfaces { get; set; }
        public string PrinterModules { get; set; }
        public string PrinterNotes { get; set; }
        public string AssignedUsers { get; set; }

      
    }
}
