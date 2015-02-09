using System;
using System.Collections.Generic;

namespace ProvisioningTool.Entity
{
    /// <summary>
    /// Summary description for ApplicationUsers
    /// </summary>
    /// 
    public class ApplicationUser  :Audit
    {
        public ApplicationUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int ApplicationUserID { get; set; }
        public string ApplicationUsername { get; set; }
        public string ApplicationPassword { get; set; }
        public int Site { get; set; }
        public Role Role { get; set; }
        public string EmailID { get; set; }
        public bool IsAuthenticated { get; set; }
        public string View { get; set; }
        public string Status { get; set; }
        public string oper { get; set; }
        public int id { get; set; }
    }
}
