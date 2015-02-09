
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
namespace ProvisioningTool.DAL
{
    public class DalAdapter
    {
        #region [ Declarations ]
        ApplicationUserDAL applicationUserDAL;
        GlobalMasterDAL globalMasterDAL;
        CompanyDAL companyDAL;
        CustomersDAL customerDAL;
        UserDAL userDAL;
        SiteDAL siteDAL;
        ServerHardwareDAL serverHardwareDAL;
        ServerInfoDAL serverInfoDAL;
        WorkStationHardwareDAL workStationHardwareDAL;
        WorkStationInfoDAL workStationInfoDAL;
        LaptopHardwareDAL laptopHardwareDAL;
        RouterDAL routerDAL;
        FirewallDAL firewallDAL;
        PrinterDAL printerDAL;
        LaptopInfoDAL laptopInfoDAL;
        MobileDeviceDAL mobileDeviceDAL;
        NetworkSwitchDAL networkSwitchDAL;
        PhoneSystemDAL phoneSystemDAL;
        WirelessDAL wirelessDAL;
        NetworkShareDAL networkShareDAL;
        SoftwareDAL softwareDAL;
        ChecklistDAL checklistDAL;
        GroupPolicyDAL groupPolicyDAL;
        InternetDomainDAL internetDomainDAL;
        InternetProviderDAL internetProviderDAL;
        InternetEmailHostDAL internetEmailHostDAL;
        InternetWebHostDAL internetWebHostDAL;
        HeadingMasterDAL headingMasterDAL;
        HardDiskDAL hardDiskDAL;
        HistoryTrackerDAL historyTrackerDAL;
        NotesDAL notesDAL;
        #endregion [ Declarations ]

        #region [ Constructor ]
        public DalAdapter()
        {
            applicationUserDAL = new ApplicationUserDAL();
            globalMasterDAL = new GlobalMasterDAL();
            companyDAL = new CompanyDAL();
            customerDAL = new CustomersDAL();
            userDAL = new UserDAL();
            siteDAL = new SiteDAL();
            serverHardwareDAL = new ServerHardwareDAL();
            workStationHardwareDAL = new WorkStationHardwareDAL();
            laptopHardwareDAL = new LaptopHardwareDAL();
            routerDAL = new RouterDAL();
            firewallDAL = new FirewallDAL();
            serverInfoDAL = new ServerInfoDAL();
            workStationInfoDAL = new WorkStationInfoDAL();
            laptopInfoDAL = new LaptopInfoDAL();
            mobileDeviceDAL = new MobileDeviceDAL();
            networkSwitchDAL = new NetworkSwitchDAL();
            phoneSystemDAL = new PhoneSystemDAL();
            wirelessDAL = new WirelessDAL();
            networkShareDAL = new NetworkShareDAL();
            softwareDAL = new SoftwareDAL();
            printerDAL = new PrinterDAL();
            checklistDAL = new ChecklistDAL();
            groupPolicyDAL = new GroupPolicyDAL();
            headingMasterDAL = new HeadingMasterDAL();
            hardDiskDAL = new HardDiskDAL();
            internetDomainDAL = new InternetDomainDAL();
            internetProviderDAL = new InternetProviderDAL();
            internetEmailHostDAL = new InternetEmailHostDAL();
            historyTrackerDAL = new HistoryTrackerDAL();
            internetWebHostDAL = new InternetWebHostDAL();
            notesDAL = new NotesDAL();
        }
        #endregion [ Constructor ]

        public List<ApplicationUser> GetAllApplicationUsers()
        {
            return applicationUserDAL.GetAllApplicationUsers();
        }

        #region [ ApplicationUser ]
        public List<ApplicationUser> GetApplicationUserByUserName(string username, out string SqlMessage)
        {
            List<ApplicationUser> applicationUserList = applicationUserDAL.GetApplicationUserByUserName(username, out  SqlMessage);
            return applicationUserList;
        }

        public List<ApplicationUser> SearchApplicationUserByKey(string Key, out string SqlMessage)
        {
            List<ApplicationUser> applicationUserList = applicationUserDAL.SearchApplicationUserByKey(Key, out  SqlMessage);
            return applicationUserList;
        }

        public List<ApplicationUser> GetApplicationUserByUserNameAndEmail(string username, string email, out string SqlMessage)
        {
            List<ApplicationUser> applicationUserList = applicationUserDAL.GetApplicationUserByUserNameAndEmail(username, email, out  SqlMessage);
            return applicationUserList;
        }

        public bool DeleteApplicationUserByApplicationUserID(int applicationUserID)
        {
            return applicationUserDAL.DeleteApplicationUserByApplicationUserID(applicationUserID);
        }

        public ApplicationUser GetApplicationUserByApplicationUserID(int applicationUserID)
        {
            ApplicationUser applicationUser = applicationUserDAL.GetApplicationUserByApplicationUserID(applicationUserID);
            return applicationUser;
        }

        public ApplicationUser AddApplicationUser(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return applicationUserDAL.AddApplicationUser(request, out isDuplicate, out rowsAffected);
        }

        public ApplicationUser ModifyApplicationUser(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return applicationUserDAL.ModifyApplicationUser(request, out isDuplicate, out rowsAffected);
        }

        #endregion [ ApplicationUser ]

        #region [ Company ]
        public List<Company> GetAllCompanies()
        {
            return companyDAL.GetAllCompanies();
        }
        #endregion [ Company ]

        #region [ Global Master]
        public GlobalMaster GetGlobalMasterAndDetailsByMasterName(string masterName, string searchFilter)
        {
            List<GlobalMaster> globalMasterList = globalMasterDAL.GetGlobalMasterAndDetailsByMasterName(masterName, searchFilter);
            if (globalMasterList != null && globalMasterList.Count > 0)
                return globalMasterList[0];
            else
                return null;
        }

        public GlobalMaster GetGlobalMasterAndDetailsByMasterNameAndSiteID(string masterName, int siteID)
        {
            List<GlobalMaster> globalMasterList = globalMasterDAL.GetGlobalMasterAndDetailsByMasterName(masterName, siteID);
            if (globalMasterList != null && globalMasterList.Count > 0)
                return globalMasterList[0];
            else
                return null;
        }

        public GlobalMaster GetGlobalMasterAndDetailsByDetailID(string masterName, int masterDetailID)
        {
            List<GlobalMaster> globalMasterList = globalMasterDAL.GetGlobalMasterAndDetailsByMasterDetailID(masterName, masterDetailID);
            if (globalMasterList != null && globalMasterList.Count > 0)
                return globalMasterList[0];
            else
                return null;
        }


        public bool GlobalMasterDetailAdd(GlobalMasterDetail globalMasterDetail, string masterName)
        {
            return globalMasterDAL.GlobalMasterDetailAdd(globalMasterDetail, masterName);
        }
        public bool GlobalMasterDetailUpdateByMasterDetailID(GlobalMasterDetail globalMasterDetail, string masterName)
        {
            return globalMasterDAL.GlobalMasterDetailUpdateByMasterDetailID(globalMasterDetail, masterName);
        }
        public bool GlobalMasterDetailDeleteByMasterDetailID(GlobalMasterDetail globalMasterDetail)
        {
            return globalMasterDAL.GlobalMasterDetailDeleteByMasterDetailID(globalMasterDetail);
        }


        #endregion [ Global Master]

        #region [ Customer ]

        public Customer AddCustomer(Customer customer, out bool isDuplicate, out int rowsAffected)
        {
            return customerDAL.AddCustomer(customer, out isDuplicate, out rowsAffected);
        }
        public Customer ModifyCustomer(Customer customer, out bool isDuplicate, out int rowsAffected)
        {
            return customerDAL.ModifyCustomer(customer, out isDuplicate, out rowsAffected);
        }

        public bool DeleteCustomerByCustomerID(int customerID)
        {
            return customerDAL.DeleteCustomerByCustomerID(customerID);
        }


        public List<Customer> GetAllCustomers()
        {
            return customerDAL.GetAllCustomers();
        }

        public List<Customer> GetAllSitesToCustomer()
        {
            return customerDAL.GetAllSitesToCustomer();
        }

        public Customer GetAllCustomerByCustomerID(int customerID)
        {
            return customerDAL.GetAllCustomerByCustomerID(customerID)[0];
        }

        public List<Customer> GetCustomerBySearchKey(Customer customer)
        {
            return customerDAL.GetCustomerBySearchKey(customer);
        }
        #endregion[ Customer ]

        #region [ Site ]

        public Site AddSite(Site site, out bool isDuplicate, out int rowsAffected)
        {
            return siteDAL.AddSite(site, out isDuplicate, out rowsAffected);
        }

        public Site ModifySite(Site site, out bool isDuplicate, out int rowsAffected)
        {
            return siteDAL.ModifySite(site, out isDuplicate, out rowsAffected);
        }

        public bool DeleteSiteBySiteID(int siteID)
        {
            return siteDAL.DeleteSiteBySiteID(siteID);
        }

        public Site GetSiteBySiteID(int siteID)
        {
            return siteDAL.GetAllSiteBySiteID(siteID);
        }

        public List<Site> SearchSiteByKey(string Key)
        {
            return siteDAL.SearchSiteByKey(Key);
        }

        public List<Site> GetSitesByCustomerID(int CustomerID)
        {
            return siteDAL.GetAllSiteByCustomerID(CustomerID);
        }

        public List<Site> GetAllSites(int CustomerID, int searchFilter)
        {
            return siteDAL.GetAllSites(CustomerID, searchFilter);
        }
        #endregion[ Site ]

        #region[ Router ]
        public List<Router> GetAllRouters(int siteID)
        {
            return routerDAL.GetAllRouters(siteID);
        }
        public Router AddRouter(Router router, out bool isDuplicate, out int rowsAffected)
        {
            return routerDAL.AddRouter(router, out isDuplicate, out rowsAffected);
        }
        public Router ModifyRouter(Router router, out bool isDuplicate, out int rowsAffected)
        {
            return routerDAL.ModifyRouter(router, out isDuplicate, out rowsAffected);
        }

        public bool DeleteRouterByRouterID(int routerID)
        {
            return routerDAL.DeleteRouterByRouterID(routerID);
        }

        public Router GetRouterByRouterID(int routerID)
        {
            return routerDAL.GetRouterByRouterID(routerID)[0];
        }
        #endregion[ Router ]

        #region[ PhoneSystem ]
        public List<PhoneSystem> GetAllPhoneSystems(int siteID, string searchFilter)
        {
            return phoneSystemDAL.GetAllPhoneSystems(siteID, searchFilter);
        }
        public PhoneSystem AddPhoneSystem(PhoneSystem phoneSystem, out bool isDuplicate, out int rowsAffected)
        {
            return phoneSystemDAL.AddPhoneSystem(phoneSystem, out isDuplicate, out rowsAffected);
        }
        public PhoneSystem ModifyPhoneSystem(PhoneSystem phoneSystem, out bool isDuplicate, out int rowsAffected)
        {
            return phoneSystemDAL.ModifyPhoneSystem(phoneSystem, out isDuplicate, out rowsAffected);
        }

        public bool DeletePhoneSystemByPhoneSystemID(int phoneSystemID)
        {
            return phoneSystemDAL.DeletePhoneSystemByPhoneSystemID(phoneSystemID);
        }

        public PhoneSystem GetPhoneSystemByPhoneSystemID(int phoneSystemID)
        {
            return phoneSystemDAL.GetPhoneSystemByPhoneSystemID(phoneSystemID);
        }
        #endregion[ PhoneSystem ]

        #region [Users]

        #region [ GetAllUsers ]
        public List<User> GetAllUsers(int siteID, string searchFilter)
        {
            return userDAL.GetAllUsers(siteID, searchFilter);
        }
        #endregion [ GetAllUsers ]

        #region [ GETALLUSERS WITH OUT SITEID]
        public List<User> GetAllUsersWithoutSiteID()
        {
            return userDAL.GetAllUsersWithoutSiteID();
        }
        #endregion[ GETALLUSERS WITH OUT SITEID]

        public User AddUsers(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return userDAL.AddUsers(request, out isDuplicate, out rowsAffected);
        }

        public User ModifyUser(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return userDAL.ModifyUser(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteUserByUserID(int userID)
        {
            return userDAL.DeleteUserByUserID(userID);
        }

        public User GetUserAndUserDetailsByUserID(int userID)
        {
            return userDAL.GetUserAndUserDetailsByUserID(userID);
        }
        #endregion

        #region [ServerHardware]

        #region [ GetAllServerHardware ]
        public List<ServerHardware> GetAllServerHardware(int siteID)
        {
            return serverHardwareDAL.GetAllServerHardware(siteID);
        }
        #endregion [ GetAllServerHardware ]

        public ServerHardware AddServerHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return serverHardwareDAL.AddServerHardware(request, out isDuplicate, out rowsAffected);
        }

        public ServerHardware ModifyServerHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return serverHardwareDAL.ModifyServerHardware(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteServerHardwareByServerHardwareID(int serverHardwareID)
        {
            return serverHardwareDAL.DeleteServerHardwareByServerHardwareID(serverHardwareID);
        }

        public ServerHardware GetServerHardwarAndUserDetailsByServerHardwarID(int serverHardwareID)
        {
            return serverHardwareDAL.GetServerHardwarAndUserDetailsByServerHardwarID(serverHardwareID);
        }

        #endregion

        #region [WorkStationHardware]

        #region [ GetAllWorkStationHardware ]
        public List<WorkStationHardware> GetAllWorkStationHardware(int siteID)
        {
            return workStationHardwareDAL.GetAllWorkStationHardware(siteID);
        }
        #endregion [ GetAllWorkStationHardware ]

        public WorkStationHardware AddWorkStationHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return workStationHardwareDAL.AddWorkStationHardware(request, out isDuplicate, out rowsAffected);
        }

        public WorkStationHardware ModifyWorkStationHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return workStationHardwareDAL.ModifyWorkStationHardware(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteWorkStationHardwareByWorkStationHardwareID(int workStationHardwareID)
        {
            return workStationHardwareDAL.DeleteWorkStationHardwareByWorkStationHardwareID(workStationHardwareID);
        }

        public WorkStationHardware GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(int workStationHardwareID)
        {
            return workStationHardwareDAL.GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(workStationHardwareID);
        }
        #endregion

        #region [LaptopHardware]

        #region [ GetAllLaptopHardware ]
        public List<LaptopHardware> GetAllLaptopHardware(int siteID)
        {
            return laptopHardwareDAL.GetAllLaptopHardware(siteID);
        }
        #endregion [ GetAllLaptopHardware ]

        public LaptopHardware AddLaptopHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return laptopHardwareDAL.AddLaptopHardware(request, out isDuplicate, out rowsAffected);
        }

        public LaptopHardware ModifyLaptopHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return laptopHardwareDAL.ModifyLaptopHardware(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteLaptopHardwareByLaptopHardwareID(int laptopHardwareID)
        {
            return laptopHardwareDAL.DeleteLaptopHardwareByLaptopHardwareID(laptopHardwareID);
        }

        public LaptopHardware GetLaptopHardwarAndUserDetailsByLaptopHardwarID(int laptopHardwareID)
        {
            return laptopHardwareDAL.GetLaptopHardwarAndUserDetailsByLaptopHardwarID(laptopHardwareID);
        }
        #endregion

        #region [ServerInfo]

        #region [ GetAllServerInfo ]
        public List<ServerInfo> GetAllServerInfo(int siteID, string searchFilter)
        {
            return serverInfoDAL.GetAllServerInfo(siteID, searchFilter);
        }
        #endregion [ GetAllServerInfo ]

        public ServerInfo AddServerInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return serverInfoDAL.AddServerInfo(request, out isDuplicate, out rowsAffected);
        }

        public ServerInfo ModifyServerInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return serverInfoDAL.ModifyServerInfo(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteServerInfoByServerInfoID(int serverInfoID)
        {
            return serverInfoDAL.DeleteServerInfoByServerInfoID(serverInfoID);
        }

        public ServerInfo GetServerInfoAndUserDetailsByServerInfoID(int serverInfoID)
        {
            return serverInfoDAL.GetServerHardwarAndUserDetailsByServerHardwarID(serverInfoID);
        }

        public List<SystemHardDrive> GetAllHardDisk()
        {
            return serverInfoDAL.GetAllHardDisk();
        }
        #endregion

        #region [WorkStationInfo]

        #region [ GetAllWorkStationInfo ]
        public List<WorkStationInfo> GetAllWorkStationInfo(int siteID, string searchFilter)
        {
            return workStationInfoDAL.GetAllWorkStationInfo(siteID, searchFilter);
        }
        #endregion [ GetAllWorkStationInfo ]

        public WorkStationInfo AddWorkStationInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return workStationInfoDAL.AddWorkStationInfo(request, out isDuplicate, out rowsAffected);
        }

        public WorkStationInfo ModifyWorkStationInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return workStationInfoDAL.ModifyWorkStationInfo(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteWorkStationInfoByWorkStationInfoID(int workStationInfoID)
        {
            return workStationInfoDAL.DeleteWorkStationInfoByWorkStationInfoID(workStationInfoID);
        }

        public WorkStationInfo GetWorkStationInfoAndUserDetailsByWorkStationInfoID(int workStationInfoID)
        {
            return workStationInfoDAL.GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(workStationInfoID);
        }
        #endregion

        #region [LaptopInfo]

        #region [ GetAllLaptopInfo ]
        public List<LaptopInfo> GetAllLaptopInfo(int siteID, string searchFilter)
        {
            return laptopInfoDAL.GetAllLaptopInfo(siteID, searchFilter);
        }
        #endregion [ GetAllLaptopInfo ]

        public LaptopInfo AddLaptopInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return laptopInfoDAL.AddLaptopInfo(request, out isDuplicate, out rowsAffected);
        }

        public LaptopInfo ModifyLaptopInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return laptopInfoDAL.ModifyLaptopInfo(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteLaptopInfoByLaptopInfoID(int laptopInfoID)
        {
            return laptopInfoDAL.DeleteLaptopInfoByLaptopInfoID(laptopInfoID);
        }

        public LaptopInfo GetLaptopInfoAndUserDetailsByLaptopInfoID(int laptopInfoID)
        {
            return laptopInfoDAL.GetLaptopHardwarAndUserDetailsByLaptopHardwarID(laptopInfoID);
        }
        #endregion

        #region [FirewallInfo]

        public List<Firewall> GetAllFirewalls(int siteID)
        {
            return firewallDAL.GetAllFirewalls(siteID);
        }

        public Firewall GetFirewallByFirewallID(int firewallID)
        {
            return firewallDAL.GetFirewallByFirewallID(firewallID);
        }


        public Firewall AddFirewalls(Firewall firewall, out bool isDuplicate, out int rowsAffected)
        {
            return firewallDAL.AddFirewall(firewall, out isDuplicate, out rowsAffected);
        }

        public Firewall ModifyFirewall(Firewall firewall, out bool isDuplicate, out int rowsAffected)
        {
            return firewallDAL.ModifyFirewall(firewall, out isDuplicate, out rowsAffected);
        }

        public bool DeleteFirewallByFirewallID(int FirewallID)
        {
            return firewallDAL.DeleteFirewallByFirewallID(FirewallID);
        }


        #endregion[FirewallInfo]

        #region [MobileDevices]

        #region [ GetAllMobileDevices ]
        public List<MobileDevice> GetAllMobileDevices(int siteID, string searchFilter)
        {
            return mobileDeviceDAL.GetAllMobileDevices(siteID, searchFilter);
        }
        #endregion [ GetAllMobileDevices ]

        public MobileDevice AddMobileDevices(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return mobileDeviceDAL.AddMobileDevice(request, out isDuplicate, out rowsAffected);
        }

        public MobileDevice ModifyMobileDevice(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return mobileDeviceDAL.ModifyMobileDevice(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteMobileDeviceByMobileDeviceID(int MobileDeviceID)
        {
            return mobileDeviceDAL.DeleteMobileDeviceByMobileDeviceID(MobileDeviceID);
        }

        public MobileDevice GetMobileDeviceAndMobileDeviceDetailsByMobileDeviceID(int MobileDeviceID)
        {
            return mobileDeviceDAL.GetMobileDeviceAndMobileDeviceDetailsByMobileDeviceID(MobileDeviceID);
        }
        #endregion

        #region [NetworkSwitchInfo]

        public List<NetworkSwitch> GetAllNetworkSwitchs(int siteID)
        {
            return networkSwitchDAL.GetAllNetworkSwitchs(siteID);
        }

        public NetworkSwitch GetNetworkSwitchByNetworkSwitchID(int networkSwitchID)
        {
            return networkSwitchDAL.GetNetworkSwitchByNetworkSwitchID(networkSwitchID);
        }


        public NetworkSwitch AddNetworkSwitchs(NetworkSwitch networkSwitch, out bool isDuplicate, out int rowsAffected)
        {
            return networkSwitchDAL.AddNetworkSwitch(networkSwitch, out isDuplicate, out rowsAffected);
        }

        public NetworkSwitch ModifyNetworkSwitch(NetworkSwitch networkSwitch, out bool isDuplicate, out int rowsAffected)
        {
            return networkSwitchDAL.ModifyNetworkSwitch(networkSwitch, out isDuplicate, out rowsAffected);
        }

        public bool DeleteNetworkSwitchByNetworkSwitchID(int NetworkSwitchID)
        {
            return networkSwitchDAL.DeleteNetworkSwitchByNetworkSwitchID(NetworkSwitchID);
        }


        #endregion[NetworkSwitchInfo]

        #region [Wirelesss]

        #region [ GetAllWirelesss ]
        public List<Wireless> GetAllWirelesss(int siteID)
        {
            return wirelessDAL.GetAllWirelesss(siteID);
        }
        #endregion [ GetAllWirelesss ]

        public Wireless AddWirelesss(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return wirelessDAL.AddWireless(request, out isDuplicate, out rowsAffected);
        }

        public Wireless ModifyWireless(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return wirelessDAL.ModifyWireless(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteWirelessByWirelessID(int wirelessID)
        {
            return wirelessDAL.DeleteWirelessByWirelessID(wirelessID);
        }

        public Wireless GetWirelessAndWirelessDetailsByWirelessID(int wirelessID)
        {
            return wirelessDAL.GetWirelessAndWirelessDetailsByWirelessID(wirelessID);
        }
        #endregion

        #region [NetworkShare]

        #region [ GetAllNetworkShare ]
        public List<NetworkShareDetail> GetAllNetworkShare(int siteID, string searchFilter)
        {
            return networkShareDAL.GetAllNetworkShare(siteID, searchFilter);
        }
        #endregion [ GetAllNetworkShare ]

        public NetworkShare AddNetworkShare(PTRequest request, out bool isDuplicate, out int rowsAffected, out int iNetWorkShareID)
        {
            return networkShareDAL.AddNetworkShare(request, out isDuplicate, out rowsAffected, out iNetWorkShareID);
        }

        public NetworkShareDetail AddNetworkShare(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return networkShareDAL.AddNetworkShareDetail(request, out isDuplicate, out rowsAffected);
        }

        public NetworkShare ModifyNetworkShare(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return networkShareDAL.ModifyNetworkShare(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteNetworkShareByNetworkShareID(int networkShareID)
        {
            return networkShareDAL.DeleteNetworkShareByNetworkShareID(networkShareID);
        }

        public NetworkShareDetail GetNetworkShareDetailsByNetworkShareDetailID(int networkShareID)
        {
            return networkShareDAL.GetNetworkShareDetailsByNetworkShareDetailID(networkShareID);
        }

        #endregion

        #region [Softwares]

        #region [ GetAllSoftwares ]
        public List<Software> GetAllSoftwares(int siteID, string searchFilter)
        {
            return softwareDAL.GetAllSoftwares(siteID, searchFilter);
        }
        #endregion [ GetAllSoftwares ]

        public Software AddSoftwares(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return softwareDAL.AddSoftware(request, out isDuplicate, out rowsAffected);
        }

        public Software ModifySoftware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return softwareDAL.ModifySoftware(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteSoftwareBySoftwareID(int softwareID)
        {
            return softwareDAL.DeleteSoftwareBySoftwareID(softwareID);
        }

        public Software GetSoftwareAndSoftwareDetailsBySoftwareID(int softwareID)
        {
            return softwareDAL.GetSoftwareAndSoftwareDetailsBySoftwareID(softwareID);
        }
        #endregion

        #region [PrinterInfo]

        public List<Printer> GetAllPrinters(int siteID, string searchFilter)
        {
            return printerDAL.GetAllPrinters(siteID, searchFilter);
        }
        public Printer GetPrinterByPrinterID(int PrinterID)
        {
            return printerDAL.GetPrinterByPrinterID(PrinterID);
        }

        public Printer AddPrinters(Printer Printer, out bool isDuplicate, out int rowsAffected)
        {
            return printerDAL.AddPrinter(Printer, out isDuplicate, out rowsAffected);
        }

        public Printer ModifyPrinter(Printer Printer, out bool isDuplicate, out int rowsAffected)
        {
            return printerDAL.ModifyPrinter(Printer, out isDuplicate, out rowsAffected);
        }

        public bool DeletePrinterByPrinterID(int PrinterID)
        {
            return printerDAL.DeletePrinterByPrinterID(PrinterID);
        }
        #endregion[PrinterInfo]

        #region [CheckListInfo]

        public List<Checklist> GetAllCheckLists(int siteID)
        {
            return checklistDAL.GetAllChecklists(siteID);
        }
        public Checklist GetChecklistAndChecklistDetailsByUserID(int userID)
        {
            return checklistDAL.GetChecklistAndChecklistDetailsByUserID(userID);
        }

        public Checklist AddCheckLists(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return checklistDAL.AddChecklist(request, out isDuplicate, out rowsAffected);
        }

        public Checklist ModifyCheckList(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return checklistDAL.ModifyChecklist(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteCheckListByCheckListID(int CheckListID)
        {
            return checklistDAL.DeleteChecklistByChecklistID(CheckListID);
        }
        #endregion[CheckListInfo]

        #region [ Group Policy ]

        public List<GroupPolicySetup> GetAllGroupPolicySetup(int siteID)
        {
            return groupPolicyDAL.GetAllGroupPolicySetup(siteID);
        }

        public GroupPolicySetup AddGroupPolicySetup(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return groupPolicyDAL.AddGroupPolicySetup(request, out isDuplicate, out rowsAffected);
        }

        public GroupPolicy AddGroupPolicy(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return groupPolicyDAL.AddGroupPolicy(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteGroupPolicyByGroupPolicyID(int groupPolicySetupID)
        {
            return groupPolicyDAL.DeleteGroupPolicyByGroupPolicyID(groupPolicySetupID);
        }

        public GroupPolicySetup ModifyGroupPolicySetup(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return groupPolicyDAL.ModifyGroupPolicySetup(request, out isDuplicate, out rowsAffected);
        }

        public GroupPolicySetup GetGroupPolicySetupByGroupPolicySetupID(int GroupPolicySetupID)
        {
            return groupPolicyDAL.GetGroupPolicySetupByGroupPolicySetupID(GroupPolicySetupID);
        }

        public List<GroupPolicy> GetAllGroupPolicy()
        {
            return groupPolicyDAL.GetAllGroupPolicy();
        }

        public List<FieldTypeMaster> GetAllFieldTypeMaster()
        {
            return groupPolicyDAL.GetAllFieldTypeMaster();
        }

        public List<HeadingMaster> GetAllHeadingMaster()
        {
            return groupPolicyDAL.GetAllHeadingMaster();
        }

        public bool DeleteGroupPolicy()
        {
            return groupPolicyDAL.DeleteGroupPolicy();
        }

        #endregion [ Group Policy ]

        #region [InternetProviders]

        #region [ GetAllInternetProviders ]
        public List<InternetProvider> GetAllInternetProviders(int siteID)
        {
            return internetProviderDAL.GetAllInternetProviders(siteID);
        }
        #endregion [ GetAllInternetProviders ]

        public InternetProvider AddInternetProviders(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetProviderDAL.AddInternetProvider(request, out isDuplicate, out rowsAffected);
        }

        public InternetProvider ModifyInternetProvider(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetProviderDAL.ModifyInternetProvider(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteInternetProviderByInternetProviderID(int internetProviderID)
        {
            return internetProviderDAL.DeleteInternetProviderByInternetProviderID(internetProviderID);
        }

        public InternetProvider GetInternetProviderAndInternetProviderDetailsByInternetProviderID(int internetProviderID)
        {
            return internetProviderDAL.GetInternetProviderAndInternetProviderDetailsByInternetProviderID(internetProviderID);
        }
        #endregion

        #region [InternetDomains]

        #region [ GetAllInternetDomains ]
        public List<InternetDomain> GetAllInternetDomains(int siteID)
        {
            return internetDomainDAL.GetAllInternetDomains(siteID);
        }
        #endregion [ GetAllInternetDomains ]

        public InternetDomain AddInternetDomains(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetDomainDAL.AddInternetDomain(request, out isDuplicate, out rowsAffected);
        }

        public InternetDomain ModifyInternetDomain(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetDomainDAL.ModifyInternetDomain(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteInternetDomainByInternetDomainID(int internetDomainID)
        {
            return internetDomainDAL.DeleteInternetDomainByInternetDomainID(internetDomainID);
        }

        public InternetDomain GetInternetDomainAndInternetDomainDetailsByInternetDomainID(int internetDomainID)
        {
            return internetDomainDAL.GetInternetDomainAndInternetDomainDetailsByInternetDomainID(internetDomainID);
        }
        #endregion

        #region [InternetWebHosts]

        #region [ GetAllInternetWebHosts ]
        public List<InternetWebHost> GetAllInternetWebHosts(int siteID)
        {
            return internetWebHostDAL.GetAllInternetWebHosts(siteID);
        }
        #endregion [ GetAllInternetWebHosts ]

        public InternetWebHost AddInternetWebHosts(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetWebHostDAL.AddInternetWebHost(request, out isDuplicate, out rowsAffected);
        }

        public InternetWebHost ModifyInternetWebHost(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetWebHostDAL.ModifyInternetWebHost(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteInternetWebHostByInternetWebHostID(int internetWebHostID)
        {
            return internetWebHostDAL.DeleteInternetWebHostByInternetWebHostID(internetWebHostID);
        }

        public InternetWebHost GetInternetWebHostAndInternetWebHostDetailsByInternetWebHostID(int internetWebHostID)
        {
            return internetWebHostDAL.GetInternetWebHostAndInternetWebHostDetailsByInternetWebHostID(internetWebHostID);
        }
        #endregion

        #region [InternetEmailHosts]

        #region [ GetAllInternetEmailHosts ]
        public List<InternetEmailHost> GetAllInternetEmailHosts(int siteID)
        {
            return internetEmailHostDAL.GetAllInternetEmailHosts(siteID);
        }
        #endregion [ GetAllInternetEmailHosts ]

        public InternetEmailHost AddInternetEmailHosts(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetEmailHostDAL.AddInternetEmailHost(request, out isDuplicate, out rowsAffected);
        }

        public InternetEmailHost ModifyInternetEmailHost(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return internetEmailHostDAL.ModifyInternetEmailHost(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteInternetEmailHostByInternetEmailHostID(int internetEmailHostID)
        {
            return internetEmailHostDAL.DeleteInternetEmailHostByInternetEmailHostID(internetEmailHostID);
        }

        public InternetEmailHost GetInternetEmailHostAndInternetEmailHostDetailsByInternetEmailHostID(int internetEmailHostID)
        {
            return internetEmailHostDAL.GetInternetEmailHostAndInternetEmailHostDetailsByInternetEmailHostID(internetEmailHostID);
        }
        #endregion

        #region [ Heading Master ]

        public List<HeadingMaster> GetAllHeadingMasters()
        {
            return headingMasterDAL.GetAllHeadingMaster();
        }

        public HeadingMaster AddHeadingMaster(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return headingMasterDAL.AddHeadingMaster(request, out isDuplicate, out rowsAffected);
        }

        public bool DeleteHeadingMasterByHeadingMasterID(int headingMasterID)
        {
            return headingMasterDAL.DeleteHeadingMasterByHeadingMasterID(headingMasterID);
        }

        public HeadingMaster ModifyHeadingMaster(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return headingMasterDAL.ModifyHeadingMaster(request, out isDuplicate, out rowsAffected);
        }

        public HeadingMaster GetHeadingMasterByHeadingMasterID(int headingMasterID)
        {
            return headingMasterDAL.GetHeadingMasterByHeadingMasterID(headingMasterID);
        }

        #endregion [ Heading Master ]

        #region [ Hard Drive ]

        public List<HardDisk> GetAllHardDisks()
        {
            return hardDiskDAL.GetAllHardDisk();
        }

        public bool DeleteHardDiskByHardDiskID(int hardDiskID)
        {
            return hardDiskDAL.DeleteHardDiskByHardDiskID(hardDiskID);
        }

        public HardDisk GetHardDiskByHardDiskID(int hardDiskID)
        {
            return hardDiskDAL.GetHardDiskByHardDiskID(hardDiskID);
        }

        public HardDisk AddHardDisk(PTRequest request, out bool isDuplicate, out int rowsAffected, out int hardDiskID)
        {
            return hardDiskDAL.AddHardDisk(request, out isDuplicate, out rowsAffected, out hardDiskID);
        }

        public HardDiskDrive AddHardDiskDrive(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return hardDiskDAL.AddHardDiskDrive(request, out isDuplicate, out rowsAffected);
        }

        public HardDisk ModifyHardDisk(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return hardDiskDAL.ModifyHardDisk(request, out isDuplicate, out rowsAffected);
        }

        public HardDiskDrive ModifyHardDiskDrive(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            return hardDiskDAL.ModifyHardDiskDrive(request, out isDuplicate, out rowsAffected);
        }
        #endregion [ Hard Drive ]

        public HistoryTracker GetHistoryTrackerDetails(PTRequest request)
        {
            return historyTrackerDAL.GetHistoryTrackerDetails(request);
        }

        public NotesMaster ModifyNotes(NotesMaster notesmaster, out bool isUpdated, out int rowsAffected)
        {
            return notesDAL.UpdateNotes(notesmaster, out isUpdated, out rowsAffected);
        }

        
    }
}
