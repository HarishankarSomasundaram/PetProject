using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class GlobalMasterDetail
    {
        public GlobalMasterDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int MasterDetailID { get; set; }
        public int MasterID { get; set; }
        public string MasterValue { get; set; }
        public int SubManufacturerID { get; set; }
        public string Manufacturers { get; set; }
        public int SubTypeID { get; set; }
        public string Types { get; set; }
        public Site Site { get; set; }
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string CreatedUser { get; set; }
        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string oper { get; set; }
        public int id { get; set; }
        public List<Site> SiteList { get; set; }
    }
}
