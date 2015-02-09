
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class InternetProvider : Audit
    {
        public InternetProvider()
        {
            //
            // TODO: InternetProvider Add constructor logic here
            //
        }
        public int ProviderID { get; set; }
        public string Provider { get; set; }
        public string CircutID { get; set; }
        public string AccountNumber { get; set; }
        public string ProviderType { get; set; }
        public string BrandWidth { get; set; }
        public string NetworkID { get; set; }
        public string StaticIPAddress { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string Phone { get; set; }
        public string View { get; set; }
        public Site Site { get; set; }
        public string oper { get; set; }
        public int id { get; set; }

    }
}
