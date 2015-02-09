
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace ProvisioningTool.Entity
{

    public class InternetDomain : Audit
    {
        public InternetDomain()
        {
            //
            // TODO: InternetDomain Add constructor logic here
            //
        }
        public int DomainID { get; set; }
        public string Domain { get; set; }
        public string Registrar { get; set; }
        public string AccountID { get; set; }
        public string DomainPassword { get; set; }
        public string Expiration { get; set; }
        public string AdminPanel { get; set; }
        public bool DNSManaged { get; set; }
        public string Server { get; set; }
        public string Phone { get; set; }
        public Site Site { get; set; }
        public string View { get; set; }
        public string oper { get; set; }
        public int id { get; set; }


    }
}
