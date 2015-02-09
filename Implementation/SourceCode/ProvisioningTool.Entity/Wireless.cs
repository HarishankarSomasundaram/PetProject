
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class Wireless : Audit
    {
        public Wireless()
        {
            //
            // TODO: Wireless Add constructor logic here
            //
        }
        public int WirelessID { get; set; }
        public string Hostname { get; set; }
        public GlobalMasterDetail WirelessModel { get; set; }
        public string SerialNumber { get; set; }
        public string InstalledOn { get; set; }
        public string WarrantyExpiresOn { get; set; }
        public string IPAddress { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public GlobalMasterDetail WirelessType { get; set; }
        public GlobalMasterDetail WirelessManufacture { get; set; }
        public int SSID { get; set; }
        public string Authentication { get; set; }
        public string WirelessTypeValue { get; set; }
        public string WirelessEncryption { get; set; }
        public NotesMaster NotesMaster { get; set; }
        public string Notes { get; set; }
        public string View { get; set; }
       

    }
}
