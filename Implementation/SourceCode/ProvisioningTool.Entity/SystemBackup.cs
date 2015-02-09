using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemBackup: Audit
    {
        public SystemBackup()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemBackupID { get; set; }
        
        public SystemMaster System { get; set;}
        public int ClientID { get; set; }
        public  GlobalMasterDetail BackupSoftware { get; set; }
        public string LicenseKey { get; set; }
    }
}
