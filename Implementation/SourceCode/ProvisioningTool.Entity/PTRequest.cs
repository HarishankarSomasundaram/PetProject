using System;

namespace ProvisioningTool.Entity
{
    public class PTRequest
    {
        public PTRequest()
        {
        }

        public PTRequest(string url)
        {
            if (url == null)
                throw new ArgumentNullException("Service URL is null");
            URL = url;
        }
        public int id { get; set; }
        public string URL { get; set; }
        public ActionType CurrentAction { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public GlobalMaster GlobalMaster { get; set; }
        public GlobalMasterDetail GlobalMasterDetail { get; set; }
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public Site Site { get; set; }
        public User User { get; set; }
        public SystemHardDrive SystemHardDrive { get; set; }
        public ServerHardware ServerHardware { get; set; }
        public ServerInfo ServerInfo { get; set; }
        public WorkStationHardware WorkStationHardware { get; set; }
        public WorkStationInfo WorkStationInfo { get; set; }
        public LaptopHardware LaptopHardware { get; set; }
        public LaptopInfo LaptopInfo { get; set; }
        public NetworkShare NetworkShare { get; set; }
        public NetworkShareDetail NetworkShareDetail { get; set; }
        public Router Router { get; set; }
        public Firewall Firewall { get; set; }
        public Printer Printer { get; set; }
        public PhoneSystem PhoneSystem { get; set; }
        public GroupPolicy GroupPolicy { get; set; }
        public GroupPolicySetup GroupPolicySetup { get; set; }
        public HardDisk HardDisk { get; set; }
        public HardDiskDrive HardDiskDrive { get; set; }
        public HeadingMaster HeadingMaster { get; set; }
             
        public UserSecurityGroup UserSecurityGroup { get; set; }
        public UserRemoteAccess UserRemoteAccess { get; set; }
        public UserComputer UserComputer { get; set; }
        public UserLaptops UserLaptop { get; set; }
        public UserMobilePhone UserMobilePhone { get; set; }
        public UserTablet UserTablet { get; set; }
        public UserApp UserApps { get; set; }
        public UserNetworkShare UserNetworkShare { get; set; }
        public UserServer UserServer { get; set; }
        public UserPrinter UserPrinter { get; set; }
        public MobileDevice MobileDevice { get; set; }
        public NetworkSwitch NetworkSwitch { get; set; }
        public Wireless Wireless { get; set; }
        public Software Software { get; set; }
        public Checklist Checklist { get; set; }

        public InternetDomain InternetDomain { get; set; }
        public InternetProvider InternetProvider { get; set; }
        public InternetEmailHost InternetEmailHost { get; set; }
        public InternetWebHost InternetWebHost { get; set; }
        public Role Role { get; set; }
        public int sessionSiteID { get; set; }
        public string searchFilter { get; set; }

        public HistoryTracker HistoryTracker { get; set; }
        public NotesMaster NotesMaster { get; set; }
        public Mail Mail { get; set; }
    }
}
