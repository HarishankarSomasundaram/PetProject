
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class Checklist : Audit
    {
        public Checklist()
        {
            //
            // TODO: Checklist Add constructor logic here
            //
        }
        public int CheckListID { get; set; }
        public User User { get; set; }
        public bool UserAccountCreation { get; set; }
        public bool AddUserToDepartment { get; set; }
        public bool AddUserToSecurityGroup { get; set; }
        public bool AddLoginScript { get; set; }
        public bool CreateEmailAccount { get; set; }
        public bool EmailAddress { get; set; }
        public bool AddUserToEmailDistributions { get; set; }
        public bool HostedAntispam { get; set; }
        public bool AssignedPrinters { get; set; }
        public bool PerformTest { get; set; }
        public bool CustomerLANdiagram { get; set; }
        public bool ThanktheCustomer { get; set; }
        public bool AllTaskCompleted { get; set; }
        public string Notes { get; set; }
        public string IsChecklistDone { get; set; }
        public string View { get; set; }
 
    
    }
}
