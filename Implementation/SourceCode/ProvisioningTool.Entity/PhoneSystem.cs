using System;
using System.Collections.Generic;
namespace ProvisioningTool.Entity
{
    public class PhoneSystem : Audit
    {
        public PhoneSystem()
        {
            //
            // TODO: PhoneSystem Add constructor logic here
            //
        }
        
        public int PhoneSystemID { get; set; }
        public string Hostname { get; set; }
        public string PhoneType{ get; set; }
        public string Manufacture { get; set; }
        public GlobalMasterDetail PhoneSystemModel { get; set; }
        public string Memory { get; set; }
        public string SerialNumber { get; set; }
        public string InstalledOn { get; set; }
        public string WarrantyExpiresOn { get; set; }
        public string IPAddress { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public GlobalMasterDetail OSVersion { get; set; }
        public string Firmware { get; set; }
        public List<PhoneSystemModule> PhoneSystemModuleList { get; set; }
        public List<PhoneSystemInterface> PhoneSystemInterfaceList { get; set; }
        //public List<PhoneSystemAssignedUser> AssignedUserList { get; set; }
        public List<AssignedUser> PhoneSystemAssignedUserList { get; set; }
        public NotesMaster Notes { get; set; }
        public string View { get; set; }

        public string PhoneSystemInterfaces { get; set; }
        public string PhoneSystemModules { get; set; }
        public string PhoneSystemAssignedUsers{ get; set; }
        public string PhoneSystemNotes { get; set; }
        public string ViewDocumentPath { get; set; }
        public Site Site { get; set; }
        public Documents Documents { get; set; }
        public string oper { get; set; }
        public int id { get; set; }
    }
}
