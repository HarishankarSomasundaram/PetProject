
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class Software : Audit
    {
        public Software()
        {
            //
            // TODO: Software Add constructor logic here
            //
        }
        public int SoftwareID { get; set; }
        public string Application { get; set; }
        public string SoftwareDescription { get; set; }
        public string LicenseKey { get; set; }
        public string Server { get; set; }
        public string PathID { get; set; }
        public int ApplicationInstalledOn { get; set; }
        public string InstalledOn { get; set; }
        public string Version { get; set; }
        public List<AssignedUser> AssignedUser { get; set; }
        public string SelectedAssignedUsers { get; set; }
        public NotesMaster NotesMaster { get; set; }
        public string Notes { get; set; }
        public string View { get; set; }
    }
}
