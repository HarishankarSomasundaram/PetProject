using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class Customer : Audit
    {
        public Customer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int CustomerID { get; set; }
        public Company Company { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public GlobalMasterDetail City { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public GlobalMasterDetail State { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public GlobalMasterDetail Country { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public GlobalMasterDetail AccountRep { get; set; }
        public int AccountRepID { get; set; }
        public string AccountRepName { get; set; }
        public GlobalMasterDetail PrimaryEngineer { get; set; }
        public int PrimaryEngineerID { get; set; }
        public string PrimaryEngineerName { get; set; }
        public string Notes { get; set; }

        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }

        public string View { get; set; }

        public string oper { get; set; }
        public int id { get; set; }

        public bool IsAutoTask { get; set; }
        public string MappingID { get; set; }
    }
}
