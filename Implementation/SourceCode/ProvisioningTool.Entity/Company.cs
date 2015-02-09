using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class Company:Audit
    {
        public Company()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyAddress3 { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string WebAddress { get; set; }
    }
}
