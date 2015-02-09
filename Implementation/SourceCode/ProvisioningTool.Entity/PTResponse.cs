using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class PTResponse
    {
        public PTResponse()
        {
        }
        public PTResponse(bool isposted)
        {
            isPosted = isposted;
        }

        public bool isPosted { get; set; }
        public bool isSuccess { get; set; }
        public string Message { get; set; }

        public bool isDuplicate { get; set; }
        public int rowsAffected { get; set; }
        public int ID { get; set; }


        public int Total { get; set; }
        public int Page { get; set; }
        public int Records { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public GlobalMaster GlobalMaster { get; set; }
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public Site Site { get; set; }
        public SystemHardDrive SystemHardDrive { get; set; }
        public User User { get; set; }
        public ServerHardware ServerHardware { get; set; }
        public ServerInfo ServerInfo { get; set; }
        public WorkStationHardware WorkStationHardware { get; set; }
        public WorkStationInfo WorkStationInfo { get; set; }
        public LaptopHardware LaptopHardware { get; set; }
        public LaptopInfo LaptopInfo { get; set; }
        public Router Router { get; set; }
        public Firewall Firewall { get; set; }
        public MobileDevice MobileDevice { get; set; }
        public PhoneSystem PhoneSystem { get; set; }
        public NetworkSwitch NetworkSwitch { get; set; }
        public Wireless Wireless { get; set; }
        public Printer Printer { get; set; }
        public Software Software { get; set; }
        public NetworkShare NetworkShare { get; set; }
        public NetworkShareDetail NetworkShareDetail { get; set; }
        public Checklist Checklist { get; set; }
        public GroupPolicy GroupPolicy { get; set; }
        public GroupPolicySetup GroupPolicySetup { get; set; }
        public HardDisk HardDisk { get; set; }
        public HardDiskDrive HardDiskDrive { get; set; }
        public HeadingMaster HeadingMaster { get; set; }
        public InternetDomain InternetDomain { get; set; }
        public InternetProvider InternetProvider { get; set; }
        public InternetEmailHost InternetEmailHost { get; set; }
        public InternetWebHost InternetWebHost { get; set; }
        public Role Role { get; set; }
        public HistoryTracker HistoryTracker { get; set; }
        public Mail Mail { get; set; }

        public List<ApplicationUser> ApplicationUserList { get; set; }
        public List<GlobalMaster> GlobalMasterList { get; set; }
        public List<GlobalMasterDetail> GlobalMasterDetailList { get; set; }
        public List<Company> CompanyList { get; set; }
        public List<Customer> CustomerList { get; set; }
        public List<Site> SiteList { get; set; }
        public List<Firewall> FirewallList { get; set; }
        public List<User> UserList { get; set; }
        public List<ServerHardware> ServerHardwarList { get; set; }
        public List<ServerInfo> ServerInfoList { get; set; }
        public List<WorkStationHardware> WorkStationHardwareList { get; set; }
        public List<WorkStationInfo> WorkStationInfoList { get; set; }
        public List<LaptopInfo> LaptopInfoList { get; set; }
        public List<LaptopHardware> LaptopHardwareList { get; set; }
        public List<Router> RouterList { get; set; }
        public List<MobileDevice> MobileDeviceList { get; set; }
        public List<PhoneSystem> PhoneSystemList { get; set; }
        public List<SystemHardDrive> SystemHardDriveList { get; set; }
        public List<NetworkSwitch> NetworkSwitchList { get; set; }
        public List<Wireless> WirelessList { get; set; }
        public List<Software> SoftwareList { get; set; }
        public List<NetworkShareDetail> NetworkShareDetailList { get; set; }
        public List<Printer> PrinterList { get; set; }
        public List<Checklist> ChecklistItems { get; set; }
        public List<GroupPolicySetup> GroupPolicySetupList { get; set; }
        public List<GroupPolicy> GroupPolicyList { get; set; }
        public List<FieldTypeMaster> FieldTypeMasterList { get; set; }
        public List<HeadingMaster> HeadingMasterList { get; set; }
        public List<HardDisk> HardDiskList { get; set; }

        public List<InternetDomain> InternetDomainList { get; set; }
        public List<InternetProvider> InternetProviderList { get; set; }
        public List<InternetEmailHost> InternetEmailHostList { get; set; }
        public List<InternetWebHost> InternetWebHostList { get; set; }
        public List<HistoryTracker> HistoryTrackerList { get; set; }
        public List<Role> RoleList { get; set; }

        public List<NotesMaster> NotesMasterList { get; set; }

    }

}
