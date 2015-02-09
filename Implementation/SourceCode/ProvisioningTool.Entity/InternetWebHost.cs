
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class InternetWebHost : Audit
    {
        public InternetWebHost()
        {
            //
            // TODO: InternetWebHost Add constructor logic here
            //
        }
        public int WebHostID { get; set; }
        public string WebHost { get; set; }
        public string Provider { get; set; }
        public string AccountID { get; set; }
        public string WebHostPassword { get; set; }
        public string IPAddress { get; set; }
        public string AdminPanel { get; set; }
        public bool DNSManaged { get; set; }
        public string NameServer { get; set; }
        public string Phone { get; set; }

        public Site Site { get; set; }
        public string View { get; set; }
        public string oper { get; set; }
        public int id { get; set; }
    }
}
