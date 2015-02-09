using System;
using System.Collections.Generic;

namespace ProvisioningTool.Entity
{
    public class Router : Audit
    {
        public Router()
        {
            //
            // TODO: Routers Add constructor logic here
            //
        }
        public int RouterID { get; set; }
        public string Hostname { get; set; }
        public string Manufacture { get; set; }
        public string Memory { get; set; }
        public GlobalMasterDetail RouterModel { get; set; }
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
        public List<RouterModule> RouterModuleList { get; set; }
        public List<RouterInterface> RouterInterfaceList { get; set; }
        public List<RouterSiteToSite> RouterSiteToSiteList { get; set; }

        public NotesMaster Notes { get; set; }
        public string View { get; set; }

        public string RouterInterfaces { get; set; }
        public string RouterModules { get; set; }
        public string RouterSiteToSites { get; set; }
        public string RouterNotes { get; set; }
        public Documents Documents { get; set; }
        public string ViewDocumentPath { get; set; }
        public Site Site { get; set; }
        public string oper { get; set; }
        public int id { get; set; }
    }
}
