using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class Site : Audit
    {
        public Site()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int SiteID { get; set; }
        public Customer Customer { get; set; }
        public int CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

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
        public string Website { get; set; }

        public GlobalMasterDetail AccountRep { get; set; }
        public int AccountRepID { get; set; }
        public string AccountRepName { get; set; }

        public GlobalMasterDetail PrimaryEngineer { get; set; }
        public int PrimaryEngineerID { get; set; }
        public string PrimaryEngineerName { get; set; }

        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }

        public User PrimaryContact { get; set; }
        public int PrimaryContactID { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public int PrimaryContactTitle { get; set; }
        public string PrimaryContactTitleName { get; set; }
        public string PrimaryContactEmail { get; set; }

        public string oper { get; set; }
        public int id { get; set; }
        public string View { get; set; }

        public bool IsAutoTask { get; set; }
        public string MappingID { get; set; }
    }
}
