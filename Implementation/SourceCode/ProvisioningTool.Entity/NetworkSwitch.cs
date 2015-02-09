using System;
using System.Collections.Generic;

namespace ProvisioningTool.Entity
{

	public class NetworkSwitch : Audit
	{
         public NetworkSwitch()
        {
            //
            // TODO: NetworkSwitch Add constructor logic here
            //
        }
		public int NetworkSwitchID { get; set; }	
		public string Hostname { get; set; }
        public GlobalMasterDetail NetworkSwitchModel { get; set; }
		public string SerialNumber { get; set; }
        public string InstalledOn { get; set; }
        public string WarrantyExpiresOn { get; set; }	
		public string Speed { get; set; }	
		public bool POE { get; set; }	
		public string Power { get; set; }	
		public string IPAddress { get; set; }	
		public string Subnet { get; set; }	
		public string Gateway { get; set; }	
		public string AdminUserName { get; set; }	
		public string AdminPassword { get; set; }
        public GlobalMasterDetail OSVersion { get; set; }
		public string Firmware { get; set; }
        public List<NetworkSwitchModule> NetworkSwitchModuleList { get; set; }
        public List<NetworkSwitchInterface> NetworkSwitchInterfaceList { get; set; }

        public string NetworkSwitchInterfaces { get; set; }
        public string NetworkSwitchModules { get; set; }

		public string SFPType { get; set; }
        public string VLAN { get; set; }
        public NotesMaster NotesMaster { get; set; }
        public Site Site { get; set; }
        public string Notes { get; set; }
        public string View { get; set; }

	    
	 }
}