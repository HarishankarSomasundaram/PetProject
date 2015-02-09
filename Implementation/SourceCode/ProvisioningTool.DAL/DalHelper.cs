
namespace ProvisioningTool.DAL
{
    public sealed class DalHelper
    {
        #region [ Variable Declaration ]
        public static string columnNameRowsAffected { get { return "RowsAffected"; } }
        public static string columnNameIsDuplicate { get { return "IsDuplicate"; } }
        public static string columnNameIsUpdated { get { return "IsUpdated"; } }
        public static string columnNameIsDeleted { get { return "IsDeleted"; } }
        public static string columnNameId { get { return "ID"; } }
        public static string columnSqlMessage { get { return "SqlMessage"; } }
        #endregion [ Variable Declaration ]

        #region [ ApplicationUsers ]
        public static string SPApplicationUserAdd { get { return "ApplicationUserAdd"; } }
        public static string SPApplicationUserUpdate { get { return "ApplicationUserUpdate"; } }
        public static string SPGetAllApplicationUsers { get { return "GetAllApplicationUsers"; } }
        public static string SPDeleteApplicationUserByApplicationUserID { get { return "DeleteApplicationUserByApplicationUserID"; } }
        public static string SPGetApplicationUserByUserName { get { return "GetApplicationUserByUserName"; } }
        public static string SPSearchApplicationUserByKey { get { return "SearchApplicationUserByKey"; } }
        public static string SPGetApplicationUserByUserNameAndEmail { get { return "GetApplicationUserByUserNameAndEmail"; } }
        public static string SPGetApplicationUserByApplicationUserID { get { return "GetApplicationUserByApplicationUserID"; } }
        #endregion [ ApplicationUsers ]

        #region [ Global Masters ]
        public static string SPGlobalMasterDetailsByMasterName_List { get { return "GlobalMasterDetailsByMasterName_List"; } }
        public static string SPGlobalMasterDetailsByDetailID_List { get { return "GlobalMasterDetailsByDetailID_List"; } }
        public static string SPGlobalMasterDetailsByMasterNameAndSiteID_List { get { return "GlobalMasterDetailsByMasterNameAndSiteID_List"; } }
        public static string SPGlobalMasterDetailAdd { get { return "GlobalMasterDetailAdd"; } }
        public static string SPGlobalMasterDetailUpdateByMasterDetailID { get { return "GlobalMasterDetailUpdateByMasterDetailID"; } }
        public static string SPGlobalMasterDetailDeleteByMasterDetailID { get { return "GlobalMasterDetailDeleteByMasterDetailID"; } }
        #endregion [ Global Masters ]

        #region [ Company ]
        public static string SPCompanies_List { get { return "Companies_List"; } }
        #endregion [ Company ]

        #region [ User ]
        //public static string SPUser_List { get { return "User_List"; } }
        public static string SPUser_List { get { return "UsersInfo_List"; } }
        public static string SPUserWithOutSiteID_List { get { return "UsersInfoWithOutSiteID_List"; } }
        public static string SPUserAdd { get { return "UserAdd"; } }
        public static string SPUserUpdate { get { return "UserUpdate"; } }
        public static string SPDeleteUserByUserID { get { return "UserDeleteByUserID"; } }
        public static string SPUserAndUserDetailsByUserID { get { return "UserByUserID_List"; } }

        #endregion [ User ]

        #region [ Customer ]
        public static string SPCustomerAdd { get { return "CustomerAdd"; } }
        public static string SPCustomerUpdate { get { return "CustomerUpdate"; } }
        public static string SPCustomerDeleteByCustomerID { get { return "CustomerDeleteByCustomerID"; } }
        public static string SPCustomers_List { get { return "Customers_List"; } }
        public static string SPSitesToCustomer_List { get { return "SitesToCustomer_List"; } }
        public static string SPSearchCustomersBySearchKey_List { get { return "SearchCustomersBySearchKey_List"; } }
        public static string SPCustomerByCustomerID_List { get { return "CustomerByCustomerID_List"; } }
        #endregion [ Customer ]

        #region [ Sites ]
        public static string SPSiteAdd { get { return "SiteAdd"; } }
        public static string SPSiteUpdateBySiteID { get { return "SiteUpdateBySiteID"; } }
        public static string SPDeleteSiteBySiteID { get { return "SiteDeleteBySiteID"; } }
        public static string SPSite_List { get { return "Site_List"; } }
        public static string SPSiteByCustomerID_Lists { get { return "SiteByCustomerID_List"; } }        
        public static string SPSitesBySiteID_List { get { return "SitesBySiteID_List"; } }
        public static string SPSitesBySearchKey_List { get { return "SPSitesBySearchKey_List"; } }
        
        #endregion [ Sites]

        #region [ ServerHardware ]
        public static string SPServerHardwareAdd { get { return "ServerHardwareAdd"; } }
        public static string SPServerHardwareUpdateByServerHardwareID { get { return "ServerHardwareUpdateByServerHardwareID"; } }
        public static string SPServerHardwareDeleteByServerHardwareID { get { return "ServerHardwareDeleteByServerHardwareID"; } }
        public static string SPServerHardware_List { get { return "GetALLServerHardwares"; } }
        public static string SPServerHardwareByServerHardwareID_List { get { return "ServerHardwareByServerHardwareID_List"; } }
        public static string SPGetAllHardDiskDetails { get { return "GetHardDiskDetails"; } }
        #endregion [ ServerHardware]

        #region [ WorkStationHardware ]
        public static string SPWorkStationHardwareAdd { get { return "WorkStationHardwareAdd"; } }
        public static string SPWorkStationHardwareUpdateByWorkStationHardwareID { get { return "WorkStationHardwareUpdateByWorkStationHardwareID"; } }
        public static string SPWorkStationHardwareDeleteByWorkStationHardwareID { get { return "WorkStationHardwareDeleteByWorkStationHardwareID"; } }
        public static string SPWorkStationHardware_List { get { return "GetALLWorkStationHardwares"; } }
        public static string SPWorkStationHardwareByWorkStationHardwareID_List { get { return "WorkStationHardwareByWorkStationHardwareID_List"; } }
        #endregion [ WorkStationHardware]

        #region [ LaptopHardware ]
        public static string SPLaptopHardwareAdd { get { return "LaptopHardwareAdd"; } }
        public static string SPLaptopHardwareUpdateByLaptopHardwareID { get { return "LaptopHardwareUpdateByLaptopHardwareID"; } }
        public static string SPLaptopHardwareDeleteByLaptopHardwareID { get { return "LaptopHardwareDeleteByLaptopHardwareID"; } }
        public static string SPLaptopHardware_List { get { return "GetALLLaptopHardwares"; } }
        public static string SPLaptopHardwareByLaptopHardwareID_List { get { return "LaptopHardwareByLaptopHardwareID_List"; } }
        #endregion [ LaptopHardware]

        #region [ Routers ]
        public static string SPRouterAdd { get { return "RouterAndDetailsAdd"; } }
        public static string SPRouterUpdateByRouterID { get { return "RouterUpdateByRouterID"; } }
        public static string SPRouterDeleteByRouterID { get { return "RouterDeleteByRouterID"; } }
        public static string SPRouter_List { get { return "Router_List"; } }
        public static string SPRouterByRouterID_List { get { return "RouterByRouterID_List"; } }
        #endregion [ Routers]

        #region [ PhoneSystems ]
        public static string SPPhoneSystemAdd { get { return "PhoneSystemAndDetailsAdd"; } }
        public static string SPPhoneSystemUpdateByPhoneSystemID { get { return "PhoneSystemUpdateByPhoneSystemID"; } }
        public static string SPPhoneSystemDeleteByPhoneSystemID { get { return "PhoneSystemDeleteByPhoneSystemID"; } }
        public static string SPPhoneSystem_List { get { return "PhoneSystem_List"; } }
        public static string SPPhoneSystemByPhoneSystemID_List { get { return "PhoneSystemByPhoneSystemID_List"; } }
        #endregion [ PhoneSystems]

        #region [ Firewalls ]
        public static string SPFierwallAdd { get { return "FirewallAdd"; } }
        public static string SPFirewall_List { get { return "Firewall_List"; } }
        public static string SPFirewallByFirewallID_List { get { return "FirewallByFirewallID_List"; } }
        public static string SPFirewallUpdate { get { return "FirewallUpdate"; } }
        public static string SPDeleteFirewallByFirewallID { get { return "FirewallDeleteByFirewallID"; } }
        #endregion [ Firewalls ]

        #region [ ServerInfo ]
        public static string SPServerInfoAdd { get { return "ServerInfoAdd"; } }
        public static string SPServerInfoUpdateByServerInfoID { get { return "ServerInfoUpdateByServerInfoID"; } }
        public static string SPServerInfoDeleteByServerInfoID { get { return "ServerInfoDeleteByServerInfoID"; } }
        public static string SPServerInfo_List { get { return "GetALLServerInfos"; } }
        public static string SPServerInfoByServerInfoID_List { get { return "ServerInfoByServerInfoID_List"; } }
        #endregion [ ServerInfo]

        #region [ WorkStationInfo ]
        public static string SPWorkStationInfoAdd { get { return "WorkStationInfoAdd"; } }
        public static string SPWorkStationInfoUpdateByWorkStationInfoID { get { return "WorkStationInfoUpdateByWorkStationInfoID"; } }
        public static string SPWorkStationInfoDeleteByWorkStationInfoID { get { return "WorkStationInfoDeleteByWorkStationInfoID"; } }
        public static string SPWorkStationInfo_List { get { return "GetALLWorkStationInfos"; } }
        public static string SPWorkStationInfoByWorkStationInfoID_List { get { return "WorkStationInfoByWorkStationInfoID_List"; } }
        #endregion [ WorkStationInfo]

        #region [ LaptopInfo ]
        public static string SPLaptopInfoAdd { get { return "LaptopInfoAdd"; } }
        public static string SPLaptopInfoUpdateByLaptopInfoID { get { return "LaptopInfoUpdateByLaptopInfoID"; } }
        public static string SPLaptopInfoDeleteByLaptopInfoID { get { return "LaptopInfoDeleteByLaptopInfoID"; } }
        public static string SPLaptopInfo_List { get { return "GetALLLaptopInfos"; } }
        public static string SPLaptopInfoByLaptopInfoID_List { get { return "LaptopInfoByLaptopInfoID_List"; } }
        #endregion [ LaptopInfo]

        #region [ MobileDevice ]
        //public static string SPMobileDevice_List { get { return "MobileDevice_List"; } }
        public static string SPMobileDevice_List { get { return "MobileDevicesInfo_List"; } }
        public static string SPMobileDeviceAdd { get { return "MobileDeviceAdd"; } }
        public static string SPMobileDeviceUpdate { get { return "MobileDeviceUpdate"; } }
        public static string SPDeleteMobileDeviceByMobileDeviceID { get { return "MobileDeviceDeleteByMobileDeviceID"; } }
        public static string SPMobileDeviceByMobileDeviceID { get { return "MobileDeviceByMobileDeviceID_List"; } }

        #endregion [ MobileDevice ]

        #region [ NetworkSwitch ]
        //public static string SPNetworkSwitch_List { get { return "NetworkSwitch_List"; } }
        public static string SPNetworkSwitch_List { get { return "NetworkSwitch_List"; } }
        public static string SPNetworkSwitchAdd { get { return "NetworkSwitchAdd"; } }
        public static string SPNetworkSwitchUpdate { get { return "NetworkSwitchUpdate"; } }
        public static string SPDeleteNetworkSwitchByNetworkSwitchID { get { return "NetworkSwitchDeleteByNetworkSwitchID"; } }
        public static string SPNetworkSwitchAndNetworkSwitchDetailsByNetworkSwitchID { get { return "NetworkSwitchByNetworkSwitchID_List"; } }

        #endregion [ NetworkSwitch ]

        #region [ NetworkShare ]

        public static string SPNetworkShare_List { get { return "NetworkShareDetails_List"; } }
        public static string SPNetworkShareAdd { get { return "NetworkShareAdd"; } }
        public static string SPNetworkShareUpdate { get { return "NetworkShareDetailsUpdate"; } }
        public static string SPDeleteNetworkShareByNetworkShareID { get { return "NetworkShareDetailsDeleteByNetworkShareDetailsID"; } }
        public static string SPNetworkShareAndNetworkShareDetailsByNetworkShareID { get { return "NetworkShareDetailsByNetworkShareDetailsID_List"; } }
        public static string SPNetworkShareDetailAdd { get { return "NetworkShareDetailsAdd"; } }

        #endregion [ NetworkShare ]

        #region [ Wireless ]
        //public static string SPWireless_List { get { return "Wireless_List"; } }
        public static string SPWireless_List { get { return "WirelessInfo_List"; } }
        public static string SPWirelessAdd { get { return "WirelessAdd"; } }
        public static string SPWirelessUpdate { get { return "WirelessUpdate"; } }
        public static string SPDeleteWirelessByWirelessID { get { return "WirelessDeleteByWirelessID"; } }
        public static string SPWirelessByWirelessID { get { return "WirelessByWirelessID_List"; } }

        #endregion [ Wireless ]

        #region [ Software ]
        //public static string SPSoftware_List { get { return "Software_List"; } }
        public static string SPSoftware_List { get { return "SoftwareInfo_List"; } }
        public static string SPSoftwareAdd { get { return "SoftwareAdd"; } }
        public static string SPSoftwareUpdate { get { return "SoftwareUpdate"; } }
        public static string SPDeleteSoftwareBySoftwareID { get { return "SoftwareDeleteBySoftwareID"; } }
        public static string SPSoftwareBySoftwareID { get { return "SoftwareBySoftwareID_List"; } }

        #endregion [ Software ]

        #region [ Printers ]
        public static string SPPrinterAdd { get { return "PrinterAdd"; } }
        public static string SPPrinter_List { get { return "Printer_List"; } }
        public static string SPPrinterByPrinterID_List { get { return "PrinterByPrinterID_List"; } }
        public static string SPPrinterUpdate { get { return "PrinterUpdate"; } }
        public static string SPDeletePrinterByPrinterID { get { return "PrinterDeleteByPrinterID"; } }
        #endregion [ Printers ]

        #region [ Provisioning CheckListItems ]
        public static string SPCheckListItemsAdd { get { return "ChecklistAdd"; } }
        public static string SPCheckListItems_List { get { return "CheckListItems_List"; } }
        public static string SPCheckListItemsByUserID_List { get { return "CheckListItemsByUserID_List"; } }
        public static string SPCheckListItemsUpdate { get { return "ChecklistUpdate"; } }
        public static string SPDeleteCheckListItemsByCheckListID { get { return "CheckListItemsDeleteByCheckListID"; } }
        #endregion [ Provisioning CheckListItems ]

        #region [ Group Policy Info ]
        public static string SPGroupPolicySetupDelete { get { return "GroupPolicySetupDelete"; } }
        public static string SPGroupPolicySetupEdit { get { return "GroupPolicySetupEdit"; } }
        public static string SPGroupPolicySetupAdd { get { return "GroupPolicySetupAdd"; } }
        public static string SPGroupPolicySetupbyGroupPolicySetupID { get { return "GroupPolicySetupbyGroupPolicySetupID"; } }
        public static string SPGroupPolicySetupFull { get { return "GroupPolicySetupFull"; } }
        public static string SPGroupPolicyAdd { get { return "GroupPolicyAdd"; } }
        public static string SPGroupPolicyList { get { return "GroupPolicyList"; } }
        public static string SPFieldTypeMasterList { get { return "FieldTypeMasterList"; } }
        public static string SPHeadingMasterList { get { return "HeadingMasterList"; } }
        public static string SPGroupPolicyDelete { get { return "GroupPolicyDelete"; } }
        #endregion [ Group Policy Info ]

        #region [ InternetDomain ]
        //public static string SPInternetDomain_List { get { return "InternetDomain_List"; } }
        public static string SPInternetDomain_List { get { return "InternetDomainInfo_List"; } }
        public static string SPInternetDomainAdd { get { return "InternetDomainAdd"; } }
        public static string SPInternetDomainUpdate { get { return "InternetDomainUpdate"; } }
        public static string SPDeleteInternetDomainByInternetDomainID { get { return "InternetDomainDeleteByInternetDomainID"; } }
        public static string SPInternetDomainByInternetDomainID { get { return "InternetDomainByInternetDomainID_List"; } }

        #endregion [ InternetDomain ]

        #region [ InternetProvider ]
        //public static string SPInternetProvider_List { get { return "InternetProvider_List"; } }
        public static string SPInternetProvider_List { get { return "InternetProviderInfo_List"; } }
        public static string SPInternetProviderAdd { get { return "InternetProviderAdd"; } }
        public static string SPInternetProviderUpdate { get { return "InternetProviderUpdate"; } }
        public static string SPDeleteInternetProviderByInternetProviderID { get { return "InternetProviderDeleteByInternetProviderID"; } }
        public static string SPInternetProviderByInternetProviderID { get { return "InternetProviderByInternetProviderID_List"; } }

        #endregion [ InternetProvider ]

        #region [ InternetWebHost ]
        //public static string SPInternetWebHost_List { get { return "InternetWebHost_List"; } }
        public static string SPInternetWebHost_List { get { return "InternetWebHostInfo_List"; } }
        public static string SPInternetWebHostAdd { get { return "InternetWebHostAdd"; } }
        public static string SPInternetWebHostUpdate { get { return "InternetWebHostUpdate"; } }
        public static string SPDeleteInternetWebHostByInternetWebHostID { get { return "InternetWebHostDeleteByInternetWebHostID"; } }
        public static string SPInternetWebHostByInternetWebHostID { get { return "InternetWebHostByInternetWebHostID_List"; } }

        #endregion [ InternetWebHost ]

        #region [ InternetEmailHost ]
        //public static string SPInternetEmailHost_List { get { return "InternetEmailHost_List"; } }
        public static string SPInternetEmailHost_List { get { return "InternetEmailHostInfo_List"; } }
        public static string SPInternetEmailHostAdd { get { return "InternetEmailHostAdd"; } }
        public static string SPInternetEmailHostUpdate { get { return "InternetEmailHostUpdate"; } }
        public static string SPDeleteInternetEmailHostByInternetEmailHostID { get { return "InternetEmailHostDeleteByInternetEmailHostID"; } }
        public static string SPInternetEmailHostByInternetEmailHostID { get { return "InternetEmailHostByInternetEmailHostID_List"; } }

        #endregion [ InternetEmailHost ]

        #region [ Hard Drive Drive ]
        public static string SPSystemHardDiskList { get { return "SystemHardDiskList"; } }
        public static string SPSystemHardDiskDelete { get { return "SystemHardDiskDelete"; } }
        public static string SPSystemHardDiskbySystemHardDiskID { get { return "SystemHardDiskbySystemHardDiskID"; } }
        public static string SPSystemHardDiskEdit { get { return "SystemHardDiskEdit"; } }
        public static string SPSystemHardDiskAdd { get { return "SystemHardDiskAdd"; } }
        public static string SPSystemHardDiskDriveAdd { get { return "SystemHardDiskDriveAdd"; } }
        public static string SPSystemHardDiskDriveEdit { get { return "SystemHardDiskDriveEdit"; } }
        #endregion [ Hard Drive Drive ]

        #region [ Heading Master ]
        public static string SPHeadingMasterDelete { get { return "HeadingMasterDelete"; } }
        public static string SPHeadingMasterbyHeadingMasterID { get { return "HeadingMasterbyHeadingMasterID"; } }
        public static string SPHeadingMasterAdd { get { return "HeadingMasterAdd"; } }
        public static string SPHeadingMasterEdit { get { return "HeadingMasterEdit"; } }
        #endregion [ Heading Master ]

        #region [History Tracker]
        public static string SPHistoryTrackerByMasterNameFieldNameAndTableID_List { get { return "HistoryTrackerByMasterNameFieldNameAndTableID_List"; } } 
        #endregion

        #region [ Notes Master ]

        public static string SPNotesUpdate { get { return "NotesMasterUpdate"; } }

        #endregion [ Notes Master ]

    }
}
