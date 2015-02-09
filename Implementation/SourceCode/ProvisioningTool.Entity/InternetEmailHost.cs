
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class InternetEmailHost : Audit
    {
        public InternetEmailHost()
        {
            //
            // TODO: InternetEmailHost Add constructor logic here
            //
        }
        public int EmailHostID { get; set; }
        public string EmailHosting { get; set; }
        public string Provider { get; set; }
        public string AccountLogin { get; set; }
        public string EmailPassword { get; set; }
        public string IPAddress { get; set; }
        public string AdminPanel { get; set; }
        public bool DNSManaged { get; set; }
        public string NameServers { get; set; }
        public string Phone { get; set; }
   
        public Site Site { get; set; }
        public string View { get; set; }
        public string oper { get; set; }
        public int id { get; set; }
    }
}
