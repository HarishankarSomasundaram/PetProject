using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class ServerInfo : Audit
    {
        public ServerInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string oper { get; set; }
        public int id { get; set; }

        public int ServerID { get; set; }
        public string HostName { get; set; }
        public string Manufacturer { get; set; }
        public string InstalledDate { get; set; }

        public int ServerModelID { get; set; }
        public string ServerModelName { get; set; }
        public ServerHardware ServerModel { get; set; }

        public string SerialNumber { get; set; }
        public string WarrantyExpires { get; set; }

        public string IPAddress { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string AdminUserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }

        public List<SystemRole> ServerRole { get; set; }
        public string ServerRoleIDs { get; set; }

        public int OperatingSystemID { get; set; }
        public GlobalMasterDetail OperationSystem { get; set; }
        public string OperationSystemName { get; set; }
        public string OperatingSystemLicenseKey { get; set; }

        public GlobalMasterDetail AntiVirus { get; set; }
        public int AntiVirusID { get; set; }
        public string AntiVirusLicenseKey { get; set; }

        public List<SystemBackup> ServerBackup { get; set; }
        

        public string ServerBackupIDs { get; set; }

        public List<SystemApplication> ServerApplication { get; set; }
        public string ServerApplicationIDs { get; set; }

        public List<AssignedUser> ServerAssignedUser { get; set; }
        public string ServerAssignedUserIDs { get; set; }

        public NotesMaster Notes { get; set; }
        public string FullNotes { get; set; }


        public string ProcessorName { get; set; }
        public string MemoryName { get; set; }
        public string Core { get; set; }
        public string RolesName { get; set; }
        public int SiteID { get; set; }

        public string View { get; set; }
    }
}
