using System;
using System.Collections.Generic;

namespace ProvisioningTool.Entity
{
    public class Firewall : Audit
    {
        public Firewall()
        {
            //
            // TODO: Firewall Add constructor logic here
            //
        }
        public int FirewallID { get; set; }
        public string Hostname { get; set; }
        public string Manufacture { get; set; }
        public GlobalMasterDetail FirewallModel { get; set; }
        public string Memory { get; set; }        
        public string SerialNumber { get; set; }
        public string InstalledOn { get; set; }
        public string WarrantyExpiresOn { get; set; }
        public string IPAddress { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public GlobalMasterDetail OSVersion { get; set; }
        public string Firmware { get; set; }
        public List<FirewallModule> FirewallModuleList { get; set; }
        public List<FirewallInterface> FirewallInterfaceList { get; set; }
        public List<FirewallSiteToSite> FirewallSiteToSiteList { get; set; }
        public NotesMaster Notes { get; set; }
        public string View { get; set; }

        public string FirewallInterfaces { get; set; }
        public string FirewallModules { get; set; }
        public string FirewallSiteToSites { get; set; }
        public string FirewallNotes { get; set; }
        public string ViewDocumentPath { get; set; }
        public Site Site { get; set; }
        public Documents Documents { get; set; }
        public string oper { get; set; }
        public int id { get; set; }
    }
}
