using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProvisioningTool.Entity;

/// <summary>
/// Summary description for MailTemplate
/// </summary>
public class MailTemplate
{
    string sHTML, sTRFormation;
    public MailTemplate()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    #region [ User Details ]
    /// <summary>
    /// GetUserDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetUserDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.User != null)
        {
            string sSecurityGroup, sRemoteAccess, sWorkStation, sServer, sLaptop, sMobilePhone, sTablet, sApps, sNetworkShare, sPrinter;
            sSecurityGroup = sRemoteAccess = sWorkStation = sServer = sLaptop = sMobilePhone = sTablet = sApps = sNetworkShare = sPrinter = string.Empty;

            List<UserSecurityGroup> userSecurityGroupList = response.User.UserSecurityGroupList;
            List<UserRemoteAccess> userRemoteAccess = response.User.UserRemoteAccessList;
            List<WorkStationInfo> userComputerList = response.User.UserComputerList;
            List<LaptopInfo> userLaptopList = response.User.UserLaptopList;
            List<ServerInfo> userServerList = response.User.UserServersList;
            List<MobileDevice> userMobileDeviceList = response.User.UserMobilePhoneList;
            List<UserTablet> userTabletList = response.User.UserTabletList;
            List<UserApp> userAppList = response.User.UserAppsList;
            List<NetworkShare> userNetworkShareDetailList = response.User.UserNetworkSharesList;
            List<Printer> userPrinterList = response.User.UserPrinterList;

            #region [ Security Group List ]

            if (userSecurityGroupList != null)
            {
                for (int i = 0; i < userSecurityGroupList.Count; i++)
                {
                    if (sSecurityGroup == "")
                        sSecurityGroup = userSecurityGroupList[i].SecurityGroup.MasterValue;
                    else
                        sSecurityGroup = sSecurityGroup + "," + userSecurityGroupList[i].SecurityGroup.MasterValue;
                }
            }

            #endregion [ Security Group List ]

            #region [ Remote Access ]
            if (userRemoteAccess != null)
            {
                for (int i = 0; i < userRemoteAccess.Count; i++)
                {
                    if (sRemoteAccess == "")
                        sRemoteAccess = userRemoteAccess[i].RemoteAccess.MasterValue;
                    else
                        sRemoteAccess = sRemoteAccess + "," + userRemoteAccess[i].RemoteAccess.MasterValue;
                }
            }
            #endregion [ Remote Access ]

            #region [ Computer List  ]
            if (userComputerList != null)
            {
                for (int i = 0; i < userComputerList.Count; i++)
                {
                    if (sWorkStation == "")
                        sWorkStation = userComputerList[i].HostName;
                    else
                        sWorkStation = sWorkStation + "," + userComputerList[i].HostName;
                }
            }

            if (userServerList != null)
            {
                for (int i = 0; i < userServerList.Count; i++)
                {
                    if (sServer == "")
                        sServer = userServerList[i].HostName;
                    else
                        sServer = sServer + "," + userServerList[i].HostName;
                }
            }

            if (userLaptopList != null)
            {
                for (int i = 0; i < userLaptopList.Count; i++)
                {
                    if (sLaptop == "")
                        sLaptop = userLaptopList[i].HostName;
                    else
                        sLaptop = sLaptop + "," + userLaptopList[i].HostName;
                }
            }

            #endregion [ Computer List  ]

            #region [ Mobile Devices ]
            if (userMobileDeviceList != null)
            {
                for (int i = 0; i < userMobileDeviceList.Count; i++)
                {
                    if (sMobilePhone == "")
                        sMobilePhone = userMobileDeviceList[i].Hostname;
                    else
                        sMobilePhone = sMobilePhone + "," + userMobileDeviceList[i].Hostname;
                }
            }
            #endregion [ Mobile Devices ]

            #region [ Tablet ]
            if (userTabletList != null)
            {
                for (int i = 0; i < userTabletList.Count; i++)
                {
                    if (sTablet == "")
                        sTablet = userTabletList[i].Tablet.MasterValue;
                    else
                        sTablet = sTablet + "," + userTabletList[i].Tablet.MasterValue;
                }
            }
            #endregion [ Tablet ]

            #region [App ]
            if (userAppList != null)
            {
                for (int i = 0; i < userAppList.Count; i++)
                {
                    if (sApps == "")
                        sApps = userAppList[i].App.MasterValue;
                    else
                        sApps = sApps + "," + userAppList[i].App.MasterValue;
                }
            }
            #endregion [ App ]

            #region [ Network Share ]
            if (userNetworkShareDetailList != null)
            {
                for (int i = 0; i < userNetworkShareDetailList.Count; i++)
                {
                    if (sNetworkShare == "")
                        sNetworkShare = userNetworkShareDetailList[i].NetworkShareName;
                    else
                        sNetworkShare = sNetworkShare + "," + userNetworkShareDetailList[i].NetworkShareName;
                }
            }
            #endregion [ Network Share ]

            #region [ Remote Access ]
            if (userPrinterList != null)
            {
                for (int i = 0; i < userPrinterList.Count; i++)
                {
                    if (sPrinter == "")
                        sPrinter = userPrinterList[i].Hostname;
                    else
                        sPrinter = sPrinter + "," + userPrinterList[i].Hostname;
                }
            }
            #endregion [ Remote Access ]

            sHTML = TRFormation("First Name :", response.User.FirstName, "Last Name :", response.User.LastName);
            sHTML = sHTML + TRFormation("Username :", response.User.UserName, "Password :", response.User.Password);
            sHTML = sHTML + TRFormation("Email :", response.User.Email, "Phone1 :", response.User.Phone1);
            sHTML = sHTML + TRFormation("Phone2 :", response.User.Phone2, "Title :", response.User.TitleName);
            sHTML = sHTML + TRFormation("Department :", response.User.DepartmentName, "Security Group :", sSecurityGroup);
            sHTML = sHTML + TRFormation("Remote Access :", sRemoteAccess, "Computer :", sWorkStation);
            sHTML = sHTML + TRFormation("Laptop :", sLaptop, "Mobile Phone :", sMobilePhone);
            sHTML = sHTML + TRFormation("Tablet :", sTablet, "Apps :", sApps);
            sHTML = sHTML + TRFormation("Network Shares :", sNetworkShare, "Servers :", sServer);
            sHTML = sHTML + TRFormation("Printers :", sPrinter, "Notes :", response.User.Notes);
        }
        return sHTML;
    }
    #endregion [  User Details ]

    #region [ Work station Info Details ]
    /// <summary>
    /// GetWorkStationDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetWorkStationDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.WorkStationInfo != null)
        {
            string sNotes, sAppLKey, sBackupAppLKey, sRoles, sAssignedUser;
            sNotes = sAppLKey = sBackupAppLKey = sRoles = sAssignedUser = string.Empty;
            List<SystemApplication> appList = response.WorkStationInfo.WorkStationApplication;
            List<SystemBackup> backList = response.WorkStationInfo.WorkStationBackup;
            List<SystemRole> roleList = response.WorkStationInfo.WorkStationRole;
            List<AssignedUser> assUserList = response.WorkStationInfo.WorkStationAssignedUser;

            #region [System Application ]
            if (appList != null)
            {
                for (int i = 0; i < appList.Count; i++)
                {
                    if (sAppLKey == "")
                        sAppLKey = appList[i].Application.MasterValue + ":" + appList[i].LicenseKey;
                    else
                        sAppLKey = sAppLKey + "," + appList[i].Application.MasterValue + ":" + appList[i].LicenseKey;
                }
            }
            #endregion [ System Application ]

            #region [System Backup Application ]
            if (backList != null)
            {
                for (int i = 0; i < backList.Count; i++)
                {
                    if (sBackupAppLKey == "")
                        sBackupAppLKey = backList[i].BackupSoftware.MasterValue + ":" + backList[i].LicenseKey;
                    else
                        sBackupAppLKey = sBackupAppLKey + "," + backList[i].BackupSoftware.MasterValue + ":" + backList[i].LicenseKey;
                }
            }
            #endregion [ System Backup Application ]

            #region [System Role ]
            if (roleList != null)
            {
                for (int i = 0; i < roleList.Count; i++)
                {
                    if (sRoles == "")
                        sRoles = roleList[i].Role.MasterValue;
                    else
                        sRoles = sRoles + "," + roleList[i].Role.MasterValue;
                }
            }
            #endregion [ System Role ]

            #region [Assigned User ]
            if (assUserList != null)
            {
                for (int i = 0; i < assUserList.Count; i++)
                {
                    if (sAssignedUser == "")
                        sAssignedUser = assUserList[i].User.UserName;
                    else
                        sAssignedUser = sAssignedUser + "," + assUserList[i].User.UserName;
                }
            }
            #endregion [ Assigned User ]


            if (response.WorkStationInfo.FullNotes != null)
                sNotes = response.WorkStationInfo.FullNotes.Replace('|', ',');

            sHTML = TRFormation("Host Name :", response.WorkStationInfo.HostName, "Model :", response.WorkStationInfo.WorkStationModelName);
            sHTML = sHTML + TRFormation("Serial No :", response.WorkStationInfo.SerialNumber, "Installed Date :", Convert.ToDateTime(response.WorkStationInfo.InstalledDate).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("IP Address :", response.WorkStationInfo.IPAddress, "Subnet :", response.WorkStationInfo.Subnet);
            sHTML = sHTML + TRFormation("Gateway :", response.WorkStationInfo.Gateway, "Warranty Expires Date :", Convert.ToDateTime(response.WorkStationInfo.WarrantyExpires).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Admin Username :", response.WorkStationInfo.AdminUserName, "Password :", response.WorkStationInfo.Password);
            sHTML = sHTML + TRFormation("Domain or Work group :", response.WorkStationInfo.Domain, "", "");
            sHTML = sHTML + TRFormation("Operating System :", response.WorkStationInfo.OperationSystem.MasterValue, "License Key :", response.WorkStationInfo.OperatingSystemLicenseKey);
            sHTML = sHTML + TRFormation("Anti Virus :", response.WorkStationInfo.AntiVirus.MasterValue, "License Key :", response.WorkStationInfo.AntiVirusLicenseKey);
            sHTML = sHTML + TRFormation("Application with License Key :", sAppLKey, "Backup Application with License Key :", sBackupAppLKey);
            sHTML = sHTML + TRFormation("Workstation Roles :", sRoles, "Assigned User :", sAssignedUser);
            sHTML = sHTML + TRFormation("Notes :", sNotes, "", "");
        }
        return sHTML;
    }
    #endregion [ Work station Info Details ]

    #region [ Laptop Info Details ]
    /// <summary>
    /// GetWorkStationDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetLaptopDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.LaptopInfo != null)
        {
            string sNotes, sAppLKey, sBackupAppLKey, sRoles, sAssignedUser;
            sNotes = sAppLKey = sBackupAppLKey = sRoles = sAssignedUser = string.Empty;
            List<SystemApplication> appList = response.LaptopInfo.LaptopApplication;
            List<SystemBackup> backList = response.LaptopInfo.LaptopBackup;
            List<SystemRole> roleList = response.LaptopInfo.LaptopRole;
            List<AssignedUser> assUserList = response.LaptopInfo.LaptopAssignedUser;

            #region [System Application ]
            if (appList != null)
            {
                for (int i = 0; i < appList.Count; i++)
                {
                    if (sAppLKey == "")
                        sAppLKey = appList[i].Application.MasterValue + ":" + appList[i].LicenseKey;
                    else
                        sAppLKey = sAppLKey + "," + appList[i].Application.MasterValue + ":" + appList[i].LicenseKey;
                }
            }
            #endregion [ System Application ]

            #region [System Backup Application ]
            if (backList != null)
            {
                for (int i = 0; i < backList.Count; i++)
                {
                    if (sBackupAppLKey == "")
                        sBackupAppLKey = backList[i].BackupSoftware.MasterValue + ":" + backList[i].LicenseKey;
                    else
                        sBackupAppLKey = sBackupAppLKey + "," + backList[i].BackupSoftware.MasterValue + ":" + backList[i].LicenseKey;
                }
            }
            #endregion [ System Backup Application ]

            #region [System Role ]
            if (roleList != null)
            {
                for (int i = 0; i < roleList.Count; i++)
                {
                    if (sRoles == "")
                        sRoles = roleList[i].Role.MasterValue;
                    else
                        sRoles = sRoles + "," + roleList[i].Role.MasterValue;
                }
            }
            #endregion [ System Role ]

            #region [Assigned User ]
            if (assUserList != null)
            {
                for (int i = 0; i < assUserList.Count; i++)
                {
                    if (sAssignedUser == "")
                        sAssignedUser = assUserList[i].User.UserName;
                    else
                        sAssignedUser = sAssignedUser + "," + assUserList[i].User.UserName;
                }
            }
            #endregion [ Assigned User ]


            if (response.LaptopInfo.FullNotes != null)
                sNotes = response.LaptopInfo.FullNotes.Replace('|', ',');

            sHTML = TRFormation("Host Name :", response.LaptopInfo.HostName, "Model :", response.LaptopInfo.LaptopModelName);
            sHTML = sHTML + TRFormation("Serial No :", response.LaptopInfo.SerialNumber, "Installed Date :", Convert.ToDateTime(response.LaptopInfo.InstalledDate).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("IP Address :", response.LaptopInfo.IPAddress, "Subnet :", response.LaptopInfo.Subnet);
            sHTML = sHTML + TRFormation("Gateway :", response.LaptopInfo.Gateway, "Warranty Expires Date :", Convert.ToDateTime(response.LaptopInfo.WarrantyExpires).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Admin Username :", response.LaptopInfo.AdminUserName, "Password :", response.LaptopInfo.Password);
            sHTML = sHTML + TRFormation("Domain or Work group :", response.LaptopInfo.Domain, "", "");
            sHTML = sHTML + TRFormation("Operating System :", response.LaptopInfo.OperationSystem.MasterValue, "License Key :", response.LaptopInfo.OperatingSystemLicenseKey);
            sHTML = sHTML + TRFormation("Anti Virus :", response.LaptopInfo.AntiVirus.MasterValue, "License Key :", response.LaptopInfo.AntiVirusLicenseKey);
            sHTML = sHTML + TRFormation("Application with License Key :", sAppLKey, "Backup Application with License Key :", sBackupAppLKey);
            sHTML = sHTML + TRFormation("Workstation Roles :", sRoles, "Assigned User :", sAssignedUser);
            sHTML = sHTML + TRFormation("Notes :", sNotes, "", "");
        }
        return sHTML;
    }
    #endregion [ Laptop Info Details ]

    #region [ Router Details ]
    /// <summary>
    /// GetRouterDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetRouterDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.Router != null)
        {
            string sNotes, sSite2Site, sModule;
            sNotes = sSite2Site = sModule = string.Empty;
            List<RouterModule> routerModuleList = response.Router.RouterModuleList;

            #region [Router Module ]
            if (routerModuleList != null)
            {
                for (int i = 0; i < routerModuleList.Count; i++)
                {
                    if (sModule == "")
                        sModule = routerModuleList[i].Module.MasterValue;
                    else
                        sModule = sModule + "," + routerModuleList[i].Module.MasterValue;
                }
            }
            #endregion [ Router Module ]
            if (response.Router.RouterNotes != null)
                sNotes = response.Router.RouterNotes.Replace('|', ',');
            if (response.Router.RouterSiteToSites != null)
                sSite2Site = response.Router.RouterSiteToSites.Replace('|', ',');

            sHTML = TRFormation("Host Name :", response.Router.Hostname, "Manufacture :", response.Router.Manufacture);
            sHTML = sHTML + TRFormation("Model :", response.Router.RouterModel.MasterValue, "Memory :", response.Router.Memory);
            sHTML = sHTML + TRFormation("Serial No :", response.Router.SerialNumber, "Installed On :", Convert.ToDateTime(response.Router.InstalledOn).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Warranty Expires On :", Convert.ToDateTime(response.Router.WarrantyExpiresOn).ToString("MM-dd-yyyy"), "IPAddress :", response.Router.IPAddress);
            sHTML = sHTML + TRFormation("Subnet :", response.Router.Subnet, "Gateway :", response.Router.Gateway);
            sHTML = sHTML + TRFormation("Admin Username :", response.Router.AdminUserName, "Password :", response.Router.AdminPassword);
            sHTML = sHTML + TRFormation("OS Version :", response.Router.OSVersion.MasterValue, "Firmware :", response.Router.Firmware);
            sHTML = sHTML + TRFormation("Modules :", sModule, "Interfaces :", response.Router.RouterInterfaces);
            sHTML = sHTML + TRFormation("Site to Site & Password/Key :", sSite2Site, "Notes :", sNotes);
        }
        return sHTML;
    }
    #endregion [ Router Details ]

    #region [ Firewall Details ]
    /// <summary>
    /// GetFirewallDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetFirewallDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.Firewall != null)
        {
            string sNotes, sSite2Site, sModule;
            sNotes = sSite2Site = sModule = string.Empty;
            List<FirewallModule> firewallModuleList = response.Firewall.FirewallModuleList;

            #region [Firewall Module ]
            if (firewallModuleList != null)
            {
                for (int i = 0; i < firewallModuleList.Count; i++)
                {
                    if (sModule == "")
                        sModule = firewallModuleList[i].Module.MasterValue;
                    else
                        sModule = sModule + "," + firewallModuleList[i].Module.MasterValue;
                }
            }
            #endregion [ Firewall Module ]

            if (response.Firewall.FirewallNotes != null)
                sNotes = response.Firewall.FirewallNotes.Replace('|', ',');
            if (response.Firewall.FirewallSiteToSites != null)
                sSite2Site = response.Firewall.FirewallSiteToSites.Replace('|', ',');
            sHTML = TRFormation("Host Name :", response.Firewall.Hostname, "Manufacture :", response.Firewall.Manufacture);
            sHTML = sHTML + TRFormation("Model :", response.Firewall.FirewallModel.MasterValue, "Memory :", response.Firewall.Memory);
            sHTML = sHTML + TRFormation("Serial No :", response.Firewall.SerialNumber, "Installed On :", Convert.ToDateTime(response.Firewall.InstalledOn).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Warranty Expires On :", Convert.ToDateTime(response.Firewall.WarrantyExpiresOn).ToString("MM-dd-yyyy"), "IPAddress :", response.Firewall.IPAddress);
            sHTML = sHTML + TRFormation("Subnet :", response.Firewall.Subnet, "Gateway :", response.Firewall.Gateway);
            sHTML = sHTML + TRFormation("Admin Username :", response.Firewall.AdminUserName, "Password :", response.Firewall.AdminPassword);
            sHTML = sHTML + TRFormation("OS Version :", response.Firewall.OSVersion.MasterValue, "Firmware :", response.Firewall.Firmware);
            sHTML = sHTML + TRFormation("Modules :", sModule, "Interfaces :", response.Firewall.FirewallInterfaces);
            sHTML = sHTML + TRFormation("Site to Site & Password/Key :", sSite2Site, "Notes :", sNotes);
        }
        return sHTML;
    }
    #endregion [ Firewall Details ]

    #region [ Network Switch Details ]
    /// <summary>
    /// GetNetworkSwitchDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetNetworkSwitchDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.NetworkSwitch != null)
        {
            string sNotes,  sModule;
            sNotes =  sModule = string.Empty;
            List<NetworkSwitchModule> firewallModuleList = response.NetworkSwitch.NetworkSwitchModuleList;

            #region [NetworkSwitch Module ]
            if (firewallModuleList != null)
            {
                for (int i = 0; i < firewallModuleList.Count; i++)
                {
                    if (sModule == "")
                        sModule = firewallModuleList[i].Module.MasterValue;
                    else
                        sModule = sModule + "," + firewallModuleList[i].Module.MasterValue;
                }
            }
            #endregion [ NetworkSwitch Module ]
            if (response.NetworkSwitch.Notes != null)
                sNotes = response.NetworkSwitch.Notes.Replace('|', ',');
            sHTML = TRFormation("Host Name :", response.NetworkSwitch.Hostname, "Model :", response.NetworkSwitch.NetworkSwitchModel.MasterValue);
            sHTML = sHTML + TRFormation("Serial No :", response.NetworkSwitch.SerialNumber, "Installed On :", Convert.ToDateTime(response.NetworkSwitch.InstalledOn).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Warranty Expires On :", Convert.ToDateTime(response.NetworkSwitch.WarrantyExpiresOn).ToString("MM-dd-yyyy"), "POE :", response.NetworkSwitch.POE.ToString());
            sHTML = sHTML + TRFormation("Power :", response.NetworkSwitch.Power, "IPAddress :", response.NetworkSwitch.IPAddress);
            sHTML = sHTML + TRFormation("Subnet :", response.NetworkSwitch.Subnet, "Gateway :", response.NetworkSwitch.Gateway);
            sHTML = sHTML + TRFormation("Admin Username :", response.NetworkSwitch.AdminUserName, "Password :", response.NetworkSwitch.AdminPassword);
            sHTML = sHTML + TRFormation("OS Version :", response.NetworkSwitch.OSVersion.MasterValue,"Speed",response.NetworkSwitch.Speed );
            sHTML = sHTML + TRFormation("Firmware :", response.NetworkSwitch.Firmware,"Modules :", sModule );
            sHTML = sHTML + TRFormation("VLAN",response.NetworkSwitch.VLAN,"SFP Type :", response.NetworkSwitch.SFPType );
            sHTML = sHTML + TRFormation("Interfaces :", response.NetworkSwitch.NetworkSwitchInterfaces,"Notes :", sNotes);
        }
        return sHTML;
    }
    #endregion [ Network Switch Details ]
    
    #region [ Printer Details ]
    /// <summary>
    /// GetPrinterDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetPrinterDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.Printer != null)
        {
            string sNotes, sModule, sAssignedUser;
            sNotes = sModule= sAssignedUser= string.Empty;
            List<PrinterModule> printerModuleList = response.Printer.PrinterModuleList;
            List<AssignedUser> printerAssigneUserList = response.Printer.PrinterAssignedUserList;

            #region [Printer Module ]
            if (printerModuleList != null)
            {
                for (int i = 0; i < printerModuleList.Count; i++)
                {
                    if (sModule == "")
                        sModule = printerModuleList[i].Module.MasterValue;
                    else
                        sModule = sModule + "," + printerModuleList[i].Module.MasterValue;
                }
            }
            #endregion [ Printer Module ]


            #region [Assigned User ]
            if (printerAssigneUserList != null)
            {
                for (int i = 0; i < printerAssigneUserList.Count; i++)
                {
                    if (sAssignedUser == "")
                        sAssignedUser = printerAssigneUserList[i].User.UserName;
                    else
                        sAssignedUser = sAssignedUser + "," + printerAssigneUserList[i].User.UserName;
                }
            }
            #endregion [ Assigned User ]


            if (response.Printer.PrinterNotes != null)
                sNotes = response.Printer.PrinterNotes.Replace('|', ',');
            sHTML = TRFormation("Host Name :", response.Printer.Hostname, "Model :", response.Printer.PrinterModel.MasterValue);
            sHTML = sHTML + TRFormation("Serial No :", response.Printer.SerialNumber, "Installed On :", Convert.ToDateTime(response.Printer.InstalledOn).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Warranty Expires On :", Convert.ToDateTime(response.Printer.WarrantyExpiresOn).ToString("MM-dd-yyyy"), "Manufacture :", response.Printer.Manufacture);
            sHTML = sHTML + TRFormation("OS Version :", response.Printer.OSVersion.MasterValue, "IPAddress :", response.Printer.IPAddress);
            sHTML = sHTML + TRFormation("Subnet :", response.Printer.Subnet, "Gateway :", response.Printer.Gateway);
            sHTML = sHTML + TRFormation("Admin Username :", response.Printer.AdminUserName, "Password :", response.Printer.AdminPassword);
            sHTML = sHTML + TRFormation("Firmware :", response.Printer.Firmware, "Modules :", sModule);
            sHTML = sHTML + TRFormation("Interfaces :", response.Printer.PrinterInterfaces, "Notes :", sNotes);
            sHTML = sHTML + TRFormation("Assigned User", sAssignedUser, "", "");
        }
        return sHTML;
    }
    #endregion [ Printer Details ]

    #region [ Server Info Details ]
    /// <summary>
    /// GetServerDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetServerDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.ServerInfo != null)
        {
            string sNotes, sAppLKey, sBackupAppLKey, sRoles, sAssignedUser;
            sNotes = sAppLKey = sBackupAppLKey = sRoles = sAssignedUser = string.Empty;
            List<SystemApplication> appList = response.ServerInfo.ServerApplication;
            List<SystemBackup> backList = response.ServerInfo.ServerBackup;
            List<SystemRole> roleList = response.ServerInfo.ServerRole;
            List<AssignedUser> assUserList = response.ServerInfo.ServerAssignedUser;

            #region [System Application ]
            if (appList != null)
            {
                for (int i = 0; i < appList.Count; i++)
                {
                    if (sAppLKey == "")
                        sAppLKey = appList[i].Application.MasterValue + ":" + appList[i].LicenseKey;
                    else
                        sAppLKey = sAppLKey + "," + appList[i].Application.MasterValue + ":" + appList[i].LicenseKey;
                }
            }
            #endregion [ System Application ]

            #region [System Backup Application ]
            if (backList != null)
            {
                for (int i = 0; i < backList.Count; i++)
                {
                    if (sBackupAppLKey == "")
                        sBackupAppLKey = backList[i].BackupSoftware.MasterValue + ":" + backList[i].LicenseKey;
                    else
                        sBackupAppLKey = sBackupAppLKey + "," + backList[i].BackupSoftware.MasterValue + ":" + backList[i].LicenseKey;
                }
            }
            #endregion [ System Backup Application ]

            #region [System Role ]
            if (roleList != null)
            {
                for (int i = 0; i < roleList.Count; i++)
                {
                    if (sRoles == "")
                        sRoles = roleList[i].Role.MasterValue;
                    else
                        sRoles = sRoles + "," + roleList[i].Role.MasterValue;
                }
            }
            #endregion [ System Role ]

            #region [Assigned User ]
            if (assUserList != null)
            {
                for (int i = 0; i < assUserList.Count; i++)
                {
                    if (sAssignedUser == "")
                        sAssignedUser = assUserList[i].User.UserName;
                    else
                        sAssignedUser = sAssignedUser + "," + assUserList[i].User.UserName;
                }
            }
            #endregion [ Assigned User ]


            if (response.ServerInfo.FullNotes != null)
                sNotes = response.ServerInfo.FullNotes.Replace('|', ',');

            sHTML = TRFormation("Host Name :", response.ServerInfo.HostName, "Model :", response.ServerInfo.ServerModelName);
            sHTML = sHTML + TRFormation("Serial No :", response.ServerInfo.SerialNumber, "Installed Date :", Convert.ToDateTime(response.ServerInfo.InstalledDate).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("IP Address :", response.ServerInfo.IPAddress, "Subnet :", response.ServerInfo.Subnet);
            sHTML = sHTML + TRFormation("Gateway :", response.ServerInfo.Gateway, "Warranty Expires Date :", Convert.ToDateTime(response.ServerInfo.WarrantyExpires).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Admin Username :", response.ServerInfo.AdminUserName, "Password :", response.ServerInfo.Password);
            sHTML = sHTML + TRFormation("Domain or Work group :", response.ServerInfo.Domain, "", "");
            sHTML = sHTML + TRFormation("Operating System :", response.ServerInfo.OperationSystem.MasterValue, "License Key :", response.ServerInfo.OperatingSystemLicenseKey);
            sHTML = sHTML + TRFormation("Anti Virus :", response.ServerInfo.AntiVirus.MasterValue, "License Key :", response.ServerInfo.AntiVirusLicenseKey);
            sHTML = sHTML + TRFormation("Application with License Key :", sAppLKey, "Backup Application with License Key :", sBackupAppLKey);
            sHTML = sHTML + TRFormation("Workstation Roles :", sRoles, "Assigned User :", sAssignedUser);
            sHTML = sHTML + TRFormation("Notes :", sNotes, "", "");
        }
        return sHTML;
    }
    #endregion [ Server Info Details ]

    #region [ Software Info Details ]
    /// <summary>
    /// GetSoftwareInfoDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetSoftwareInfoDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.Software != null)
        {
            string sNotes, sAssignedUser;
            sNotes = sAssignedUser = string.Empty;
            List<AssignedUser> softwareAssigneUserList = response.Software.AssignedUser;

            #region [Assigned User ]
            if (softwareAssigneUserList != null)
            {
                for (int i = 0; i < softwareAssigneUserList.Count; i++)
                {
                    if (sAssignedUser == "")
                        sAssignedUser = softwareAssigneUserList[i].User.UserName;
                    else
                        sAssignedUser = sAssignedUser + "," + softwareAssigneUserList[i].User.UserName;
                }
            }
            #endregion [ Assigned User ]

            if (response.Software.Notes != null)
                sNotes = response.Software.Notes.Replace('|', ',');
            sHTML = TRFormation("Application :", response.Software.Application, "Description :", response.Software.SoftwareDescription);
            sHTML = sHTML + TRFormation("License Key :", response.Software.LicenseKey, "Server :", response.Software.Server);
            sHTML = sHTML + TRFormation("Path :", response.Software.PathID, "Version :", response.Software.Version);
            sHTML = sHTML + TRFormation("Assigned User :", sAssignedUser, "Installed On :", Convert.ToDateTime(response.Software.InstalledOn).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Notes :", sNotes, "", "");
        }
        return sHTML;
    }
    #endregion [ Software Info Details ]

    #region [ Mobiel Device Info Details ]
    /// <summary>
    /// GetMobileDevicesDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetMobileDevicesDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.MobileDevice != null)
        {
            sHTML = TRFormation("Host Name :", response.MobileDevice.Hostname, "Type :", response.MobileDevice.MobileDeviceType.MasterValue);
            sHTML = sHTML + TRFormation("Manufacture :", response.MobileDevice.MobileDeviceManufacture.MasterValue, "Model :", response.MobileDevice.MobileDeviceModel.MasterValue);
            sHTML = sHTML + TRFormation("Assigned User :", response.MobileDevice.AssignedUser.UserName, "Installed Date :", Convert.ToDateTime(response.MobileDevice.InstalledOn).ToString("MM-dd-yyyy"));
        }
        return sHTML;
    }
    #endregion [ Mobiel Device Info Details ]

    #region [ Wireless Info Details ]
    /// <summary>
    /// GetWirelessInfoDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetWirelessInfoDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.Wireless != null)
        {
            string sNotes;
            sNotes = string.Empty;

            if (response.Wireless.Notes != null)
                sNotes = response.Wireless.Notes.Replace('|', ',');

            sHTML = TRFormation("Host Name :", response.Wireless.Hostname, "Model :", response.Wireless.WirelessModel.MasterValue);
            sHTML = sHTML + TRFormation("Serial No :", response.Wireless.SerialNumber, "Installed On :", Convert.ToDateTime(response.Wireless.InstalledOn).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Warranty Expires On :", Convert.ToDateTime(response.Wireless.WarrantyExpiresOn).ToString("MM-dd-yyyy"), "Manufacture :", response.Wireless.WirelessManufacture.MasterValue);
            sHTML = sHTML + TRFormation("Type :", response.Wireless.WirelessTypeValue, "IPAddress :", response.Wireless.IPAddress);
            sHTML = sHTML + TRFormation("Subnet :", response.Wireless.Subnet, "Gateway :", response.Wireless.Gateway);
            sHTML = sHTML + TRFormation("Admin Username :", response.Wireless.AdminUserName, "Password :", response.Wireless.AdminPassword);
            sHTML = sHTML + TRFormation("SSID :", response.Wireless.SSID.ToString(), "Authentication :", response.Wireless.Authentication);
            sHTML = sHTML + TRFormation("Encryption :", response.Wireless.WirelessEncryption, "Notes :", sNotes);
        }
        return sHTML;
    }
    #endregion [ Wireless Info Details ]

    #region [ Phone System Info Details ]
    /// <summary>
    /// GetPhoneSystemDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetPhoneSystemDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.PhoneSystem != null)
        {
            string sNotes, sModule, sAssignedUser;
            sNotes = sModule = sAssignedUser = string.Empty;
            List<PhoneSystemModule> printerModuleList = response.PhoneSystem.PhoneSystemModuleList;
            List<AssignedUser> printerAssigneUserList = response.PhoneSystem.PhoneSystemAssignedUserList;

            #region [PhoneSystem Module ]
            if (printerModuleList != null)
            {
                for (int i = 0; i < printerModuleList.Count; i++)
                {
                    if (sModule == "")
                        sModule = printerModuleList[i].Module.MasterValue;
                    else
                        sModule = sModule + "," + printerModuleList[i].Module.MasterValue;
                }
            }
            #endregion [ PhoneSystem Module ]


            #region [Assigned User ]
            if (printerAssigneUserList != null)
            {
                for (int i = 0; i < printerAssigneUserList.Count; i++)
                {
                    if (sAssignedUser == "")
                        sAssignedUser = printerAssigneUserList[i].User.UserName;
                    else
                        sAssignedUser = sAssignedUser + "," + printerAssigneUserList[i].User.UserName;
                }
            }
            #endregion [ Assigned User ]


            if (response.PhoneSystem.PhoneSystemNotes != null)
                sNotes = response.PhoneSystem.PhoneSystemNotes.Replace('|', ',');

            sHTML = TRFormation("Type:", response.PhoneSystem.PhoneType, "Host Name :", response.PhoneSystem.Hostname);
            sHTML = sHTML + TRFormation("Manufacture :", response.PhoneSystem.Manufacture, "Model :", response.PhoneSystem.PhoneSystemModel.MasterValue);
            sHTML = sHTML + TRFormation("Serial No :", response.PhoneSystem.SerialNumber, "Installed On :", Convert.ToDateTime(response.PhoneSystem.InstalledOn).ToString("MM-dd-yyyy"));
            sHTML = sHTML + TRFormation("Warranty Expires On :", Convert.ToDateTime(response.PhoneSystem.WarrantyExpiresOn).ToString("MM-dd-yyyy"), "Memory : " ,response.PhoneSystem.Memory);
            sHTML = sHTML + TRFormation("OS Version :", response.PhoneSystem.OSVersion.MasterValue, "IPAddress :", response.PhoneSystem.IPAddress);
            sHTML = sHTML + TRFormation("Subnet :", response.PhoneSystem.Subnet, "Gateway :", response.PhoneSystem.Gateway);
            sHTML = sHTML + TRFormation("Admin Username :", response.PhoneSystem.AdminUserName, "Password :", response.PhoneSystem.AdminPassword);
            sHTML = sHTML + TRFormation("Firmware :", response.PhoneSystem.Firmware, "Modules :", sModule);
            sHTML = sHTML + TRFormation("Interfaces :", response.PhoneSystem.PhoneSystemInterfaces, "Notes :", sNotes);
            sHTML = sHTML + TRFormation("Assigned User", sAssignedUser, "", "");
        }
        return sHTML;
    }
    #endregion [ Phone System Info Details ]

    #region [ Network Share Details ]
    /// <summary>
    /// GetNetworkShareDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetNetworkShareDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.NetworkShareDetail != null)
        {
            string   sAssignedUser;
             sAssignedUser = string.Empty;
             List<AssignedUser> networkShareAssigneUserList = response.NetworkShareDetail.NetworkShareAssignedUsers;

             #region [Assigned User ]
             if (networkShareAssigneUserList != null)
             {
                 for (int i = 0; i < networkShareAssigneUserList.Count; i++)
                 {
                     if (sAssignedUser == "")
                         sAssignedUser = networkShareAssigneUserList[i].User.UserName;
                     else
                         sAssignedUser = sAssignedUser + "," + networkShareAssigneUserList[i].User.UserName;
                 }
             }
             #endregion [ Assigned User ]

             sHTML = TRFormation("Network Share Name :", response.NetworkShareDetail.NetworkShareName, "","");
             sHTML = sHTML + TRFormation("Mapped :", response.NetworkShareDetail.Mapped, "Path :", response.NetworkShareDetail.Path);
             sHTML = sHTML + TRFormation("Description :", response.NetworkShareDetail.NetworkShareDescription, "Assigned User :", sAssignedUser);
        }
        return sHTML;
    }    
    #endregion [ Network Share Details ]

    #region [ Internet Web Provider Details ]
    /// <summary>
    /// GetInterNetWebProviderDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetInterNetWebProviderDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.InternetProvider != null)
        {
            sHTML = TRFormation("Provider :", response.InternetProvider.Provider, "Circut ID :", response.InternetProvider.CircutID);
            sHTML = sHTML + TRFormation("Account Number :", response.InternetProvider.AccountNumber, "Provider Type :", response.InternetProvider.ProviderType);
            sHTML = sHTML + TRFormation("Bandwidth :", response.InternetProvider.BrandWidth, "Network ID :", response.InternetProvider.NetworkID);
            sHTML = sHTML + TRFormation("Static IP Address :", response.InternetProvider.StaticIPAddress, "Subnet : ", response.InternetProvider.Subnet);
            sHTML = sHTML + TRFormation("Gateway :", response.InternetProvider.Gateway, "Phone :", response.InternetProvider.Phone);
        }
        return sHTML;
    }
    #endregion [ Internet Web Provider Details ]

    #region [ Internet Web Domain Details ]
    /// <summary>
    /// GetInternetWebDomainlDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetInternetWebDomainlDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.InternetDomain != null)
        {
            sHTML = TRFormation("Domain :", response.InternetDomain.Domain, "Registrar :", response.InternetDomain.Registrar);
            sHTML = sHTML + TRFormation("Account ID :", response.InternetDomain.AccountID, "Domain Password :", response.InternetDomain.DomainPassword);
            sHTML = sHTML + TRFormation("Expiration :", response.InternetDomain.Expiration, "Admin Panel :", response.InternetDomain.AdminPanel);
            sHTML = sHTML + TRFormation("DNS Managed :", response.InternetDomain.DNSManaged.ToString(), "Server : ", response.InternetDomain.Server);
            sHTML = sHTML + TRFormation( "Phone :", response.InternetDomain.Phone,"","");
        }
        return sHTML;
    }
    #endregion [ Internet Web Domain Details ]

    #region [ Internet Domain Web Host Details ]
    /// <summary>
    /// GetInternetWebDomainlDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetInterDomainWebHosteDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.InternetWebHost != null)
        {
            sHTML = TRFormation("Web Host :", response.InternetWebHost.WebHost, "Provider :", response.InternetWebHost.Provider);
            sHTML = sHTML + TRFormation("Account ID :", response.InternetWebHost.AccountID, "Web Host Password :", response.InternetWebHost.WebHostPassword);
            sHTML = sHTML + TRFormation("IP Address :", response.InternetWebHost.IPAddress, "Admin Panel :", response.InternetWebHost.AdminPanel);
            sHTML = sHTML + TRFormation("DNS Managed :", response.InternetWebHost.DNSManaged.ToString(), "Server : ", response.InternetWebHost.NameServer);
            sHTML = sHTML + TRFormation("Phone :", response.InternetWebHost.Phone, "", "");
        }
        return sHTML;
    }
    #endregion [ Internet Domain Web Host Details ]

    #region [ Internet Email Web Host Details ]
    /// <summary>
    /// GetInternetWebDomainlDetails - To GET the result as string which is in the tr td format
    /// </summary>
    /// <param name="response">PTResponse</param>
    /// <returns>string</returns>
    public string GetInternetWebEmailHostDetails(PTResponse response)
    {
        sHTML = string.Empty;
        if (response.InternetEmailHost != null)
        {
            sHTML = TRFormation("Email Host :", response.InternetEmailHost.EmailHosting, "Provider :", response.InternetEmailHost.Provider);
            sHTML = sHTML + TRFormation("Account Login :", response.InternetEmailHost.AccountLogin, "Email Password :", response.InternetEmailHost.EmailPassword);
            sHTML = sHTML + TRFormation("IP Address :", response.InternetEmailHost.IPAddress, "Admin Panel :", response.InternetEmailHost.AdminPanel);
            sHTML = sHTML + TRFormation("DNS Managed :", response.InternetEmailHost.DNSManaged.ToString(), "Server : ", response.InternetEmailHost.NameServers);
            sHTML = sHTML + TRFormation("Phone :", response.InternetEmailHost.Phone, "", "");
        }
        return sHTML;
    }  
    #endregion [ Internet Email Web Host Details ]

    public string TRFormation(string sFieldName, string sValue, string sFieldName2, string sValue2)
    {
        sTRFormation = string.Empty;
        sTRFormation = "<tr bgcolor='#fff' style='font-size: 14px;'><td valign='top'><label><strong>" + sFieldName + "</strong></label></td>";
        sTRFormation = sTRFormation + "<td valign='top'><span>" + sValue + "</span></td>";
        sTRFormation = sTRFormation + "<td valign='top'><label><strong>" + sFieldName2 + "</strong></label></td>";
        sTRFormation = sTRFormation + "<td valign='top'><span>" + sValue2 + "</span></td></tr>";
        return sTRFormation;
    }
}