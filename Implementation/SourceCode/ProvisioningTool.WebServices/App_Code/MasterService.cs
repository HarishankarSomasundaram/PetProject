using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using ProvisioningTool.Common;
using System;
using System.ServiceModel.Web;
using System.Collections.Specialized;
using FirstRestFulService;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Security;
using System.Configuration;
using System.Net.Mail;
using ServiceAPIWrapper;

namespace ProvisioningToolServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MasterService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MasterService : IMasterService
    {
        #region[Declaration]
        CompanyBLL companyBLL;
        CustomerBLL customerBLL;
        GlobalMasterBLL globalMasterBLL;
        UserBLL userBLL;
        MobileDeviceBLL mobileDeviceBLL;
        SiteBLL siteBLL;
        RouterBLL routerBLL;
        FirewallBLL firewallBLL;
        PhoneSystemBLL phoneSystemBLL;
        NetworkSwitchBLL networkSwitchBLL;
        ServerHardwareBLL serverHardwareBLL;
        ServerInfoBLL serverInfoBLL;
        WorkStationHardwareBLL workStationHardwareBLL;
        WorkStationInfoBLL workStationInfoBLL;
        LaptopHardwareBLL laptopHardwareBLL;
        LaptopInfoBLL laptopInfoBLL;
        WirelessBLL wirelessBLL;
        SoftwareBLL softwareBLL;
        PrinterBLL printerBLL;
        NetworkShareBLL networkShareBLL;
        ChecklistBLL checklistBLL;
        PTResponse response;
        PTResponse tempResponse;
        GroupPolicyBLL groupPolicyBLL;
        InternetDomainBLL internetDomainBLL;
        InternetProviderBLL internetProviderBLL;
        InternetEmailHostBLL internetEmailHostBLL;
        InternetWebHostBLL internetWebHostBLL;
        HeadingMasterBLL headingMasterBLL;
        HardDiskBLL hardDiskBLL;
        ApplicationUserBLL applicationUserBLL;
        HistoryTrackerBLL historyTrackerBLL;
        NotesBLL notesBLL;

        #region [ Auto Task ]
        public string AutotaskUserID = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["AutotaskUserID"], "");
        public string AutotaskPassword = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["AutotaskPwd"], "");
        Autotask api = new Autotask();
        #endregion [ Auto Task ]

        int pageSize = 1;
        int total = 0;
        #endregion[Declaration]

        #region[User Authentication]
        public string LoginUser()
        {

            HttpContext httpContext = HttpContext.Current;
            NameValueCollection headerList = httpContext.Request.Headers;

            //Getting the Values from URL [values are available as Headers]
            string loginUsername = headerList.Get("Username");
            string loginPassword = headerList.Get("Password");

            response = new PTResponse();
            PTRequest request = new PTRequest();
            applicationUserBLL = new ApplicationUserBLL();
            response = applicationUserBLL.GetApplicationUserByUserName(loginUsername);
            if (response != null && response.ApplicationUserList != null && response.ApplicationUserList.Count > 0)
            {
                //check the User is active or inactive 
                if (response.Message != "User is inactive")
                {
                    ApplicationUser applicationUser = new ApplicationUser();
                    applicationUser = response.ApplicationUserList.Find(delegate(ApplicationUser tempapplicationUser) { return tempapplicationUser.ApplicationUsername.ToUpper() == loginUsername.ToUpper() && tempapplicationUser.ApplicationPassword == loginPassword; });
                    if (applicationUser != null)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Invalid password";
                    }
                }
                else
                {
                    return response.Message;
                }

            }
            else if (response.isSuccess == false && response.Message != "")
            {
                return response.Message;

            }
            else
            {
                return "Invalid username";
            }


        }
        #endregion [User Authentication]

        #region [ Service Methods for Mobile Development ]
        /// <summary>
        /// GetDatawithTwoParam - To GET(with entity as input parameter) and POST
        /// </summary>
        /// <param name="MethodName" value="Based on this name the function will be executed"></param>
        /// <param name="param1" value="Value for First Parameter"></param>
        /// <param name="param1" value="Value for Second Parameter"></param>
        /// <param name="param1" value="Value for Thrid Parameter"></param>
        /// <returns>PTResponse</returns>
        public PTResponse GetMobileService(string methodName, string param1, string param2, string param3)
        {
            response = new PTResponse();
            PTRequest request = new PTRequest();
            switch (methodName.ToUpper())
            {
                case "GETALLCUSTOMERBYCUSTOMERID":
                    request.Customer = new Customer();
                    request.Customer.CustomerID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "SENDPASSWORDBYEMAIL":
                    request.ApplicationUser = new ApplicationUser();
                    //request.ApplicationUser.ApplicationUsername = ConvertHelper.ConvertToString(param1, "");
                    request.ApplicationUser.EmailID = ConvertHelper.ConvertToString(param2, "");
                    response = ProcessData(request, methodName);
                    break;
                case "GETSITEBYSITEID":
                    request.Site = new Site();
                    request.Site.SiteID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;

                case "GETSITESBYCUSTOMERID":
                    request.Site = new Site();
                    request.Site.Customer = new Customer();
                    request.Site.Customer.CustomerID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETUSERANDUSERDETAILSBYUSERID":
                    request.User = new User();
                    request.User.UserID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "POPULATEALLUSERS":
                    request.sessionSiteID = ConvertHelper.ConvertToInteger(param1, 0);
                    request.searchFilter = ConvertHelper.ConvertToString(param3, "0");
                    response = ProcessData(request, methodName);
                    break;
                case "POPULATEALLUSERSWITHOUTSITEID":
                    request.searchFilter = ConvertHelper.ConvertToString(param3, "0");
                    response = ProcessData(request, methodName);
                    break;
                case "GETWORKSTATIONINFOANDUSERDETAILSBYWORKSTATIONINFOID":
                    request.WorkStationInfo = new WorkStationInfo();
                    request.WorkStationInfo.WorkStationID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLWORKSTATIONINFO":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETLAPTOPINFOANDUSERDETAILSBYLAPTOPINFOID":
                    request.LaptopInfo = new LaptopInfo();
                    request.LaptopInfo.LaptopID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLLAPTOPINFO":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETROUTERBYROUTERID":
                    request.Router = new Router();
                    request.Router.RouterID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLROUTERS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETFIREWALLANDFIREWALLDETAILSBYFIREWALLID":
                    request.Firewall = new Firewall();
                    request.Firewall.FirewallID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLFIREWALLS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETNETWORKSWITCHANDNETWORKSWITCHDETAILSBYNETWORKSWITCHID":
                    request.NetworkSwitch = new NetworkSwitch();
                    request.NetworkSwitch.NetworkSwitchID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLNETWORKSWITCHES":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETPRINTERANDPRINTERDETAILSBYPRINTERID":
                    request.Printer = new Printer();
                    request.Printer.PrinterID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLPRINTERS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETSERVERINFOANDUSERDETAILSBYSERVERINFOID":
                    request.ServerInfo = new ServerInfo();
                    request.ServerInfo.ServerID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLSERVERINFO":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETMOBILEDEVICEBYMOBILEDEVICEID":
                    request.MobileDevice = new MobileDevice();
                    request.MobileDevice.MobileDeviceID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLMOBILEDEVICES":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETPHONESYSTEMBYPHONESYSTEMID":
                    request.PhoneSystem = new PhoneSystem();
                    request.PhoneSystem.PhoneSystemID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLPHONESYSTEMS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETNETWORKSHAREDETAILBYNETWORKSHAREDETAILID":
                    request.NetworkShareDetail = new NetworkShareDetail();
                    request.NetworkShareDetail.NetworkShareDetailID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLNETWORKSHARE":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETWIRELESSBYWIRELESSID":
                    request.Wireless = new Wireless();
                    request.Wireless.WirelessID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLWIRELESSES":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETSOFTWAREBYSOFTWAREID":
                    request.Software = new Software();
                    request.Software.SoftwareID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLSOFTWARES":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETINTERNETPROVIDERBYINTERNETPROVIDERID":
                    request.InternetProvider = new InternetProvider();
                    request.InternetProvider.ProviderID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLINTERNETDOMAINS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETINTERNETDOMAINBYINTERNETDOMAINID":
                    request.InternetDomain = new InternetDomain();
                    request.InternetDomain.DomainID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLINTERNETPROVIDERS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETINTERNETEMAILHOSTBYINTERNETEMAILHOSTID":
                    request.InternetEmailHost = new InternetEmailHost();
                    request.InternetEmailHost.EmailHostID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLINTERNETEMAILHOSTS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "GETINTERNETWEBHOSTBYINTERNETWEBHOSTID":
                    request.InternetWebHost = new InternetWebHost();
                    request.InternetWebHost.WebHostID = ConvertHelper.ConvertToInteger(param1, 0);
                    response = ProcessData(request, methodName);
                    break;
                case "GETALLINTERNETWEBHOSTS":
                    response = GetData(methodName, "", ConvertHelper.ConvertToString(param1, "0"), ConvertHelper.ConvertToString(param3, "0"));
                    break;
                case "SENDMAILTOCUSTOMER":
                    request.Mail = new Mail();
                    request.Mail.PageName = ConvertHelper.ConvertToString(param1, "0");
                    request.Mail.PageIdentity = ConvertHelper.ConvertToInteger(param2, 0);
                    request.Mail.To = ConvertHelper.ConvertToString(param3, "0");
                    response = ProcessData(request, methodName);
                    break;
            }
            return response;
        }
        #endregion [ Service Methods for Mobile Development ]

        #region [ Service Methods for Update Mobile Development ]
        /// <summary>
        /// GetDatawithTwoParam - To GET(with entity as input parameter) and POST
        /// </summary>
        /// <param name="MethodName" value="Based on this name the function will be executed"></param>
        /// <param name="param1" value="Value for First Parameter"></param>
        /// <param name="param1" value="Value for Second Parameter"></param>
        /// <param name="param1" value="Value for Thrid Parameter"></param>
        /// <returns>PTResponse</returns>
        public PTResponse UpdateMobileService(string methodName, string param1, string param2, string param3, string param4)
        {
            response = new PTResponse();
            PTRequest request = new PTRequest();

            switch (methodName.ToUpper())
            {
                #region[Update Notes]
                case "UPDATENOTES":
                    request.NotesMaster = new NotesMaster();
                    request.NotesMaster.NotesDetails = new NotesDetail();
                    //NotesDetail notesDetail = new NotesDetail();
                    //request.NotesMaster.NotesDetailList = new List<NotesDetail>();
                    //notesDetail.NotesClientID = ConvertHelper.ConvertToInteger(param2, 0);
                    //notesDetail.Notes = ConvertHelper.ConvertToString(param3, "");
                    //notesDetail.isFromIOS = 1;
                    //request.NotesMaster.NotesDetailList.Add(notesDetail);

                    //notes Master info
                    request.NotesMaster.ModifiedBy = ConvertHelper.ConvertToInteger(param1, 0);
                    request.NotesMaster.NotesMasterName = ConvertHelper.ConvertToString(param2);
                    request.NotesMaster.NotesDetails.NotesClientID = ConvertHelper.ConvertToInteger(param3, 0);
                    request.NotesMaster.NotesDetails.Notes = ConvertHelper.ConvertToString(param4);
                    request.NotesMaster.NotesDetails.isFromIOS = 1;
                    response = ProcessData(request, methodName);
                    break;
                #endregion[Update Notes]
            }
            return response;
        }

        #endregion [ Service Methods for Update Mobile Development ]

        #region [ProcessData]
        /// <summary>
        /// ProcessData - To GET(with entity as input parameter) and POST
        /// </summary>
        /// <param name="Request with the corresponding entity values"></param>
        /// <param name="MethodName- Based on this name the function will be executed"></param>
        /// <returns></returns>
        public PTResponse ProcessData(PTRequest request, string methodName)
        {
            response = new PTResponse();
            switch (methodName.ToUpper())
            {
                #region[CUSTOMER]
                case "GETALLCOMPANIES":
                    companyBLL = new CompanyBLL();
                    response.CompanyList = new List<Company>();
                    response.CompanyList = companyBLL.GetAllCompanies();
                    break;
                case "GETALLCUSTOMERBYCUSTOMERID":
                    customerBLL = new CustomerBLL();
                    response = customerBLL.GetAllCustomerByCustomerID(request.Customer.CustomerID);
                    break;
                case "SAVECUSTOMER":
                    customerBLL = new CustomerBLL();
                    response = customerBLL.SaveCustomer(request);
                    break;
                case "DELETECUSTOMERBYCUSTOMERID":
                    customerBLL = new CustomerBLL();
                    response.isSuccess = customerBLL.DeleteCustomerByCustomerID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[CUSTOMER]

                #region[USER]

                case "GETUSERANDUSERDETAILSBYUSERID":
                    userBLL = new UserBLL();
                    response = userBLL.GetUserAndUserDetailsByUserID(ConvertHelper.ConvertToInteger(request.User.UserID));
                    break;
                case "POPULATEALLUSERS":
                    userBLL = new UserBLL();
                    response = userBLL.GetAllUsers(ConvertHelper.ConvertToInteger(request.sessionSiteID), request.searchFilter);
                    break;
                case "POPULATEALLUSERSWITHOUTSITEID":
                    userBLL = new UserBLL();
                    response = userBLL.GetAllUsersWithoutSiteID();
                    break;
                case "SAVEUSER":
                    userBLL = new UserBLL();
                    response = userBLL.SaveUser(request);
                    break;
                case "DELETEUSERBYUSERID":
                    userBLL = new UserBLL();
                    response.isSuccess = userBLL.DeleteUserByUserID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[USER]

                #region[Router]
                case "GETROUTERBYROUTERID":
                    routerBLL = new RouterBLL();
                    response = routerBLL.GetRouterByRouterID(ConvertHelper.ConvertToInteger(request.Router.RouterID));
                    break;
                case "SAVEROUTER":
                    routerBLL = new RouterBLL();
                    response = routerBLL.SaveRouter(request);
                    break;
                case "DELETEROUTERBYROUTERID":
                    routerBLL = new RouterBLL();
                    response.isSuccess = routerBLL.DeleteRouterByRouterID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[Router]

                #region[PHONESYSTEM]
                case "GETPHONESYSTEMBYPHONESYSTEMID":
                    phoneSystemBLL = new PhoneSystemBLL();
                    response = phoneSystemBLL.GetPhoneSystemByPhoneSystemID(ConvertHelper.ConvertToInteger(request.PhoneSystem.PhoneSystemID));
                    break;
                case "SAVEPHONESYSTEM":
                    phoneSystemBLL = new PhoneSystemBLL();
                    response = phoneSystemBLL.SavePhoneSystem(request);
                    break;
                case "DELETEPHONESYSTEMBYPHONESYSTEMD":
                    phoneSystemBLL = new PhoneSystemBLL();
                    response.isSuccess = phoneSystemBLL.DeletePhoneSystemByPhoneSystemID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[PHONESYSTEM]

                #region[Firewall]

                case "GETFIREWALLANDFIREWALLDETAILSBYFIREWALLID":
                    firewallBLL = new FirewallBLL();
                    response = firewallBLL.GetFirewallAndFirewallDetailsByFirewallID(ConvertHelper.ConvertToInteger(request.Firewall.FirewallID));
                    break;
                case "SAVEFIREWALL":
                    firewallBLL = new FirewallBLL();
                    response = firewallBLL.SaveFirewall(request);
                    break;
                case "DELETEFIREWALLBYFIREWALLID":
                    firewallBLL = new FirewallBLL();
                    response.isSuccess = firewallBLL.DeleteFirewallByFirewallID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[Firewall]

                #region[NetworkSwitch]

                case "GETNETWORKSWITCHANDNETWORKSWITCHDETAILSBYNETWORKSWITCHID":
                    networkSwitchBLL = new NetworkSwitchBLL();
                    response = networkSwitchBLL.GetNetworkSwitchAndNetworkSwitchDetailsByNetworkSwitchID(ConvertHelper.ConvertToInteger(request.NetworkSwitch.NetworkSwitchID));
                    break;
                case "SAVENETWORKSWITCH":
                    networkSwitchBLL = new NetworkSwitchBLL();
                    response = networkSwitchBLL.SaveNetworkSwitch(request);
                    break;
                case "DELETENETWORKSWITCHBYNETWORKSWITCHID":
                    networkSwitchBLL = new NetworkSwitchBLL();
                    response.isSuccess = networkSwitchBLL.DeleteNetworkSwitchByNetworkSwitchID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[NetworkSwitch]

                #region[SITE]

                case "GETSITEBYSITEID":
                    siteBLL = new SiteBLL();
                    response = siteBLL.GetSiteBySiteID(ConvertHelper.ConvertToInteger(request.Site.SiteID));
                    break;

                case "GETSITESBYCUSTOMERID":
                    siteBLL = new SiteBLL();
                    response = siteBLL.GetSiteByCustomerID(ConvertHelper.ConvertToInteger(request.Site.Customer.CustomerID));
                    break;

                case "SAVESITE":
                    siteBLL = new SiteBLL();
                    response = siteBLL.SaveSite(request);
                    break;
                case "DELETESITEBYSITEID":
                    siteBLL = new SiteBLL();
                    response.isSuccess = siteBLL.DeleteSiteBySiteID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[SITE]

                #region[SERVERHARDWARE]
                case "POPULATESERVERHARDWARES":
                    serverHardwareBLL = new ServerHardwareBLL();
                    response = serverHardwareBLL.GetAllServerHardwares(request.ServerHardware.SiteID);
                    break;
                case "GETSERVERHARDWARANDUSERDETAILSBYSERVERHARDWARID":
                    serverHardwareBLL = new ServerHardwareBLL();
                    response = serverHardwareBLL.GetServerHardwarAndUserDetailsByServerHardwarID(request.ServerHardware.ServerHardwareID);
                    break;
                case "SAVESERVERHARDWARE":
                    serverHardwareBLL = new ServerHardwareBLL();
                    response = serverHardwareBLL.SaveServerHardware(request);
                    break;
                case "DELETESERVERHARDWAREBYSERVERHARDWAREID":
                    serverHardwareBLL = new ServerHardwareBLL();
                    response.isSuccess = serverHardwareBLL.DeleteServerHardwareByServerHardwareID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[SERVERHARDWARE]

                #region[WORKSTATIONHARDWARE]
                case "POPULATEWORKSTATIONHARDWARES":
                    workStationHardwareBLL = new WorkStationHardwareBLL();
                    response = workStationHardwareBLL.GetAllWorkStationHardwares(request.WorkStationHardware.SiteID);
                    break;
                case "GETWORKSTATIONHARDWARANDUSERDETAILSBYWORKSTATIONHARDWARID":
                    workStationHardwareBLL = new WorkStationHardwareBLL();
                    response = workStationHardwareBLL.GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(request.WorkStationHardware.WorkStationHardwareID);
                    break;
                case "SAVEWORKSTATIONHARDWARE":
                    workStationHardwareBLL = new WorkStationHardwareBLL();
                    response = workStationHardwareBLL.SaveWorkStationHardware(request);
                    break;
                case "DELETEWORKSTATIONHARDWAREBYWORKSTATIONHARDWAREID":
                    workStationHardwareBLL = new WorkStationHardwareBLL();
                    response.isSuccess = workStationHardwareBLL.DeleteWorkStationHardwareByWorkStationHardwareID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[WORKSTATIONHARDWARE]

                #region[LAPTOPHARDWARE]
                case "POPULATELAPTOPHARDWARES":
                    laptopHardwareBLL = new LaptopHardwareBLL();
                    response = laptopHardwareBLL.GetAllLaptopHardwares(request.LaptopHardware.SiteID);
                    break;
                case "GETLAPTOPHARDWARANDUSERDETAILSBYLAPTOPHARDWARID":
                    laptopHardwareBLL = new LaptopHardwareBLL();
                    response = laptopHardwareBLL.GetLaptopHardwarAndUserDetailsByLaptopHardwarID(request.LaptopHardware.LaptopHardwareID);
                    break;
                case "SAVELAPTOPHARDWARE":
                    laptopHardwareBLL = new LaptopHardwareBLL();
                    response = laptopHardwareBLL.SaveLaptopHardware(request);
                    break;
                case "DELETELAPTOPHARDWAREBYLAPTOPHARDWAREID":
                    laptopHardwareBLL = new LaptopHardwareBLL();
                    response.isSuccess = laptopHardwareBLL.DeleteLaptopHardwareByLaptopHardwareID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[LAPTOPHARDWARE]

                #region[SERVERINFO]
                case "POPULATEHARDDISK":
                    serverInfoBLL = new ServerInfoBLL();
                    response = serverInfoBLL.GetAllHardDisk();
                    break;
                case "GETSERVERINFOANDUSERDETAILSBYSERVERINFOID":
                    serverInfoBLL = new ServerInfoBLL();
                    response = serverInfoBLL.GetServerHardwarAndUserDetailsByServerHardwarID(request.ServerInfo.ServerID);
                    break;
                case "SAVESERVERINFO":
                    serverInfoBLL = new ServerInfoBLL();
                    response = serverInfoBLL.SaveServerInfo(request);
                    break;
                case "DELETESERVERINFOBYSERVERINFOID":
                    serverInfoBLL = new ServerInfoBLL();
                    response.isSuccess = serverInfoBLL.DeleteServerInfoByServerInfoID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[SERVERINFO]

                #region[WORKSTATIONINFO]
                case "GETWORKSTATIONINFOANDUSERDETAILSBYWORKSTATIONINFOID":
                    workStationInfoBLL = new WorkStationInfoBLL();
                    response = workStationInfoBLL.GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(request.WorkStationInfo.WorkStationID);
                    break;
                case "SAVEWORKSTATIONINFO":
                    workStationInfoBLL = new WorkStationInfoBLL();
                    response = workStationInfoBLL.SaveWorkStationInfo(request);
                    break;
                case "DELETEWORKSTATIONINFOBYWORKSTATIONINFOID":
                    workStationInfoBLL = new WorkStationInfoBLL();
                    response.isSuccess = workStationInfoBLL.DeleteWorkStationInfoByWorkStationInfoID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[WORKSTATIONINFO]

                #region[LAPTOPINFO]
                case "GETLAPTOPINFOANDUSERDETAILSBYLAPTOPINFOID":
                    laptopInfoBLL = new LaptopInfoBLL();
                    response = laptopInfoBLL.GetLaptopHardwarAndUserDetailsByLaptopHardwarID(request.LaptopInfo.LaptopID);
                    break;
                case "SAVELAPTOPINFO":
                    laptopInfoBLL = new LaptopInfoBLL();
                    response = laptopInfoBLL.SaveLaptopInfo(request);
                    break;
                case "DELETELAPTOPINFOBYLAPTOPINFOID":
                    laptopInfoBLL = new LaptopInfoBLL();
                    response.isSuccess = laptopInfoBLL.DeleteLaptopInfoByLaptopInfoID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[LAPTOPINFO]

                #region[GlobalMasterAndDetails]
                case "GETGLOBALMASTERANDDETAILSBYMASTERNAME":
                    globalMasterBLL = new GlobalMasterBLL();
                    response = globalMasterBLL.GetGlobalMasterAndDetailsByMasterName(request.GlobalMaster.MasterName, "");
                    break;
                #endregion[GlobalMasterAndDetails]

                #region[MobileDevice]

                case "GETMOBILEDEVICEBYMOBILEDEVICEID":
                    mobileDeviceBLL = new MobileDeviceBLL();
                    response = mobileDeviceBLL.GetMobileDeviceAndMobileDeviceDetailsByMobileDeviceID(ConvertHelper.ConvertToInteger(request.MobileDevice.MobileDeviceID));
                    break;
                case "SAVEMOBILEDEVICE":
                    mobileDeviceBLL = new MobileDeviceBLL();
                    response = mobileDeviceBLL.SaveMobileDevice(request);
                    break;
                case "DELETEMOBILEDEVICEBYMOBILEDEVICEID":
                    mobileDeviceBLL = new MobileDeviceBLL();
                    response.isSuccess = mobileDeviceBLL.DeleteMobileDeviceByMobileDeviceID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[MobileDevice]

                #region[WIRELESS]

                case "GETWIRELESSBYWIRELESSID":
                    wirelessBLL = new WirelessBLL();
                    response = wirelessBLL.GetWirelessAndWirelessDetailsByWirelessID(ConvertHelper.ConvertToInteger(request.Wireless.WirelessID));
                    break;
                case "SAVEWIRELESS":
                    wirelessBLL = new WirelessBLL();
                    response = wirelessBLL.SaveWireless(request);
                    break;
                case "DELETEWIRELESSBYWIRELESSID":
                    wirelessBLL = new WirelessBLL();
                    response.isSuccess = wirelessBLL.DeleteWirelessByWirelessID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[WIRELESS]

                #region[Software]

                case "GETSOFTWAREBYSOFTWAREID":
                    softwareBLL = new SoftwareBLL();
                    response = softwareBLL.GetSoftwareAndSoftwareDetailsBySoftwareID(ConvertHelper.ConvertToInteger(request.Software.SoftwareID));
                    break;
                case "SAVESOFTWARE":
                    softwareBLL = new SoftwareBLL();
                    response = softwareBLL.SaveSoftware(request);
                    break;
                case "DELETESOFTWAREBYSOFTWAREID":
                    softwareBLL = new SoftwareBLL();
                    response.isSuccess = softwareBLL.DeleteSoftwareBySoftwareID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[Software]

                #region[Printer]

                case "GETPRINTERANDPRINTERDETAILSBYPRINTERID":
                    printerBLL = new PrinterBLL();
                    response = printerBLL.GetPrinterAndPrinterDetailsByPrinterID(ConvertHelper.ConvertToInteger(request.Printer.PrinterID));
                    break;
                case "SAVEPRINTER":
                    printerBLL = new PrinterBLL();
                    response = printerBLL.SavePrinter(request);
                    break;
                case "DELETEPRINTERBYPRINTERID":
                    printerBLL = new PrinterBLL();
                    response.isSuccess = printerBLL.DeletePrinterByPrinterID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[Printer]

                #region [ Network Share ]
                case "SAVENETWORKSHARE":
                    networkShareBLL = new NetworkShareBLL();
                    response = networkShareBLL.SaveNetworkShare(request);
                    break;
                case "GETNETWORKSHAREDETAILBYNETWORKSHAREDETAILID":
                    networkShareBLL = new NetworkShareBLL();
                    response = networkShareBLL.GetNetworkShareDetailsByNetworkShareDetailID(request.NetworkShareDetail.NetworkShareDetailID);
                    break;
                case "DELETENETWORKSHAREDETAILIDBYNETWORKSHAREDETAILID":
                    networkShareBLL = new NetworkShareBLL();
                    response.isSuccess = networkShareBLL.DeleteNetworkShareByNetworkShareID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion [ Network Share ]

                #region[CHECKLIST]

                case "GETCHECKLISTANDCHECKLISTDETAILSBYUSERID":
                    checklistBLL = new ChecklistBLL();
                    response = checklistBLL.GetChecklistAndChecklistDetailsByUserID(ConvertHelper.ConvertToInteger(request.User.UserID));
                    break;
                case "SAVECHECKLIST":
                    checklistBLL = new ChecklistBLL();
                    response = checklistBLL.SaveChecklist(request);
                    break;
                case "DELETECHECKLISTBYCHECKLISTID":
                    checklistBLL = new ChecklistBLL();
                    response.isSuccess = checklistBLL.DeleteChecklistByChecklistID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[CHECKLIST]

                #region [ Group Policy ]
                case "GETGROUPPOLICYSETUPLIST":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response = groupPolicyBLL.GetAllGroupPolicySetup(request.GroupPolicySetup.SiteID);
                    break;
                case "GETGROUPPOLICYLIST":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response = groupPolicyBLL.GetAllGroupPolicy();
                    break;
                case "GETFIELDTYPELIST":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response = groupPolicyBLL.GetAllFieldTypeMaster();
                    break;
                case "GETHEADINGLIST":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response = groupPolicyBLL.GetAllHeadingMaster();
                    break;
                case "DELETEGROUPPOLICY":
                    groupPolicyBLL = new GroupPolicyBLL();
                    groupPolicyBLL.DeleteGroupPolicy();
                    break;
                case "SAVEGROUPPOLICY":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response = groupPolicyBLL.SaveGroupPolicy(request);
                    break;
                case "SAVEGROUPPOLICYSETUP":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response = groupPolicyBLL.SaveGroupPolicySetup(request);
                    break;
                case "GETGROUPPOLICYSETUP":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response = groupPolicyBLL.GetGroupPolicySetupByGroupPolicySetupID(request.GroupPolicySetup.GroupPolicySetupID);
                    break;
                case "DELETEGROUPPOLICYSETUPBYGROUPPOLICYSETUPID":
                    groupPolicyBLL = new GroupPolicyBLL();
                    response.isSuccess = groupPolicyBLL.DeleteGroupPolicyByGroupPolicyID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion [ Group Policy]

                #region [ Heading Master ]
                case "GETHEADINGMASTERLIST":
                    headingMasterBLL = new HeadingMasterBLL();
                    response = headingMasterBLL.GetAllHeadingMasters();
                    break;
                case "DELETEHEADINGMASTER":
                    headingMasterBLL = new HeadingMasterBLL();
                    response.isSuccess = headingMasterBLL.DeleteHeadingMasterByHeadingMasterID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                case "SAVEHEADINGMASTER":
                    headingMasterBLL = new HeadingMasterBLL();
                    response = headingMasterBLL.SaveHeadingMaster(request);
                    break;
                case "GETHEADINGMASTER":
                    headingMasterBLL = new HeadingMasterBLL();
                    response = headingMasterBLL.GetHeadingMasterByHeadingMasterID(request.HeadingMaster.HeadingMasterID);
                    break;
                #endregion [ Heading Master ]

                #region [ Hard Drive ]
                case "GETHARDDISKLIST":
                    hardDiskBLL = new HardDiskBLL();
                    response = hardDiskBLL.GetAllHardDisks();
                    break;
                case "DELETEHARDDISK":
                    hardDiskBLL = new HardDiskBLL();
                    response.isSuccess = hardDiskBLL.DeleteHardDiskByHardDiskID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                case "SAVEHARDDISK":
                    hardDiskBLL = new HardDiskBLL();
                    response = hardDiskBLL.SaveHardDisk(request);
                    break;
                case "GETHARDDISK":
                    hardDiskBLL = new HardDiskBLL();
                    response = hardDiskBLL.GetHardDiskByHardDiskID(request.HardDisk.SystemHardDiskID);
                    break;
                #endregion [ Hard Drive ]

                #region[InternetProvider]

                case "GETINTERNETPROVIDERBYINTERNETPROVIDERID":
                    internetProviderBLL = new InternetProviderBLL();
                    response = internetProviderBLL.GetInternetProviderAndInternetProviderDetailsByInternetProviderID(ConvertHelper.ConvertToInteger(request.InternetProvider.ProviderID));
                    break;
                case "SAVEINTERNETPROVIDER":
                    internetProviderBLL = new InternetProviderBLL();
                    response = internetProviderBLL.SaveInternetProvider(request);
                    break;
                case "DELETEINTERNETPROVIDERBYINTERNETPROVIDERID":
                    internetProviderBLL = new InternetProviderBLL();
                    response.isSuccess = internetProviderBLL.DeleteInternetProviderByInternetProviderID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[InternetProvider]

                #region[InternetDomain]

                case "GETINTERNETDOMAINBYINTERNETDOMAINID":
                    internetDomainBLL = new InternetDomainBLL();
                    response = internetDomainBLL.GetInternetDomainAndInternetDomainDetailsByInternetDomainID(ConvertHelper.ConvertToInteger(request.InternetDomain.DomainID));
                    break;
                case "SAVEINTERNETDOMAIN":
                    internetDomainBLL = new InternetDomainBLL();
                    response = internetDomainBLL.SaveInternetDomain(request);
                    break;
                case "DELETEINTERNETDOMAINBYINTERNETDOMAINID":
                    internetDomainBLL = new InternetDomainBLL();
                    response.isSuccess = internetDomainBLL.DeleteInternetDomainByInternetDomainID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[InternetDomain]

                #region[InternetEmailHost]

                case "GETINTERNETEMAILHOSTBYINTERNETEMAILHOSTID":
                    internetEmailHostBLL = new InternetEmailHostBLL();
                    response = internetEmailHostBLL.GetInternetEmailHostAndInternetEmailHostDetailsByInternetEmailHostID(ConvertHelper.ConvertToInteger(request.InternetEmailHost.EmailHostID));
                    break;
                case "SAVEINTERNETEMAILHOST":
                    internetEmailHostBLL = new InternetEmailHostBLL();
                    response = internetEmailHostBLL.SaveInternetEmailHost(request);
                    break;
                case "DELETEINTERNETEMAILHOSTBYINTERNETEMAILHOSTID":
                    internetEmailHostBLL = new InternetEmailHostBLL();
                    response.isSuccess = internetEmailHostBLL.DeleteInternetEmailHostByInternetEmailHostID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[InternetEmailHost]

                #region[InternetWebHost]

                case "GETINTERNETWEBHOSTBYINTERNETWEBHOSTID":
                    internetWebHostBLL = new InternetWebHostBLL();
                    response = internetWebHostBLL.GetInternetWebHostAndInternetWebHostDetailsByInternetWebHostID(ConvertHelper.ConvertToInteger(request.InternetWebHost.WebHostID));
                    break;
                case "SAVEINTERNETWEBHOST":
                    internetWebHostBLL = new InternetWebHostBLL();
                    response = internetWebHostBLL.SaveInternetWebHost(request);
                    break;
                case "DELETEINTERNETWEBHOSTBYINTERNETWEBHOSTID":
                    internetWebHostBLL = new InternetWebHostBLL();
                    response.isSuccess = internetWebHostBLL.DeleteInternetWebHostByInternetWebHostID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                #endregion[InternetWebHost]

                #region[ApplicationUser]

                case "GETAPPLICATIONUSERBYAPPLICATIONUSERID":
                    applicationUserBLL = new ApplicationUserBLL();
                    response = applicationUserBLL.GetApplicationUserByApplicationUserID(ConvertHelper.ConvertToInteger(request.ApplicationUser.ApplicationUserID));
                    break;
                case "SAVEAPPLICATIONUSER":
                    applicationUserBLL = new ApplicationUserBLL();
                    response = applicationUserBLL.SaveApplicationUser(request);
                    break;
                case "GETAPPLICATIONUSERBYAPPLICATIONUSERNAME":
                    applicationUserBLL = new ApplicationUserBLL();
                    response = applicationUserBLL.GetApplicationUserByUserName(request.ApplicationUser.ApplicationUsername);
                    break;
                case "DELETEAPPLICATIONUSERBYAPPLICATIONUSERID":
                    applicationUserBLL = new ApplicationUserBLL();
                    applicationUserBLL.DeleteApplicationUserByApplicationUserID(ConvertHelper.ConvertToInteger(request.id));
                    break;
                case "SENDPASSWORDBYEMAIL":
                    applicationUserBLL = new ApplicationUserBLL();
                    response = applicationUserBLL.GetApplicationUserByUserNameAndEmail(request.ApplicationUser.ApplicationUsername == null ? "" :
                    request.ApplicationUser.ApplicationUsername,
                    request.ApplicationUser.EmailID == null ? "" :
                    request.ApplicationUser.EmailID);
                    if (response != null && response.ApplicationUserList != null && response.ApplicationUserList.Count > 0)
                    {//User exist and can send the email

                        //string message = "Logon username : " + response.ApplicationUserList[0].ApplicationUsername + " <br>Password : " + response.ApplicationUserList[0].ApplicationPassword;
                        //string userPassword = response.ApplicationUserList[0].ApplicationPassword
                        bool emailStatus = EMAIL(response.ApplicationUserList[0].EmailID, response);
                        response.isSuccess = emailStatus;
                    }
                    else
                    {
                        response.isSuccess = false;
                    }
                    break;
                #endregion[ApplicationUser]

                #region [History Tracker]
                case "GETHISTORYTRACKERDETAILS":
                    historyTrackerBLL = new HistoryTrackerBLL();
                    response = historyTrackerBLL.GetHistoryTrackerDetails(request);
                    break;
                #endregion

                #region [Update Mobile Data Notes]
                case "UPDATENOTES":
                    request.CurrentAction = ActionType.Edit;
                    notesBLL = new NotesBLL();
                    response = notesBLL.UpdateNotes(request);
                    break;
                #endregion [Update Mobile Data Notes]

                #region[Send Mail To Customer]

                case "SENDMAILTOCUSTOMER":
                    if (request.Mail.To != null)
                    {
                        bool emailStatus = MAILERTOCUSTOMER(request);
                        response.isSuccess = emailStatus;
                    }
                    else
                    {
                        response.isSuccess = false;
                    }
                    break;
                #endregion[Send Mail To Customer]

                #region [ GlobalMasterCrud ]
                case "GLOBALMASTERCRUD":
                    globalMasterBLL = new GlobalMasterBLL();
                    GlobalMasterDetail objGlobalMaster = new GlobalMasterDetail();
                    objGlobalMaster = request.GlobalMasterDetail;
                    string sMasterName = request.GlobalMasterDetail.SiteName;
                    request.GlobalMasterDetail.SiteName = "0";
                    response = globalMasterBLL.GlobalMasterDetailAdd(objGlobalMaster, sMasterName);
                    break;
                #endregion [ GlobalMasterCrud ]
            }
            return response;
        }
        #endregion [ProcessData]

        #region [GetDataForColorBox]
        /// <summary>
        /// GetDataForColorBox - To GET the result set
        /// </summary>
        /// <param name="methodName">Calling MethodName</param>
        /// <param name="masterName">MasterName - this is only applicable for Global Master other and for others provide some junk data</param>
        /// <returns></returns>
        public PTResponse GetDataForColorBox(string methodName, string masterName, string siteID, string searchText)
        {


            response = new PTResponse();
            switch (methodName.ToUpper())
            {
                #region[Global Master]
                case "GETGLOBALMASTERANDDETAILSBYMASTERNAME":
                    globalMasterBLL = new GlobalMasterBLL();
                    tempResponse = globalMasterBLL.GetGlobalMasterAndDetailsByMasterName(masterName, searchText);
                    if (tempResponse != null && tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null && tempResponse.GlobalMaster.GlobalMasterDetailList.Count > 0)
                    {
                        tempResponse.GlobalMaster.GlobalMasterDetailList = tempResponse.GlobalMaster.GlobalMasterDetailList.FindAll(delegate(GlobalMasterDetail tempGlobalMasterDetail) { return tempGlobalMasterDetail.MasterValue.ToLower() == searchText.ToLower(); });
                    }
                    else
                    {
                        tempResponse = null;
                    }
                    return new PTResponse
                    {
                        Total = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        GlobalMasterDetailList = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList : null // Result set as Entity List
                    };
                #endregion[Global Master]

            }
            return response;
        }
        #endregion [GetData]

        #region [GetData]
        /// <summary>
        /// GetData - To GET the result set
        /// </summary>
        /// <param name="methodName">Calling MethodName</param>
        /// <param name="masterName">MasterName - this is only applicable for Global Master other and for others provide some junk data</param>
        /// <returns></returns>
        public PTResponse GetData(string methodName, string masterName, string siteID, string searchFilter)
        {
            response = new PTResponse();
            switch (methodName.ToUpper())
            {
                #region[CUSTOMER]
                case "GETALLCUSTOMERS":
                    customerBLL = new CustomerBLL();
                    tempResponse = customerBLL.GetAllCustomers();
                    return new PTResponse
                    {
                        Total = tempResponse.CustomerList != null ? tempResponse.CustomerList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        CustomerList = tempResponse.CustomerList // Result set as Entity List
                    };

                case "GETALLSITESTOCUSTOMER":
                    customerBLL = new CustomerBLL();
                    tempResponse = customerBLL.GetAllSitesToCustomer();
                    return new PTResponse
                    {
                        Total = tempResponse.CustomerList != null ? tempResponse.CustomerList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        CustomerList = tempResponse.CustomerList // Result set as Entity List
                    };
                #endregion[CUSTOMER]

                #region[Global Master]
                case "GETGLOBALMASTERANDDETAILSBYMASTERNAME":
                    globalMasterBLL = new GlobalMasterBLL();
                    tempResponse = globalMasterBLL.GetGlobalMasterAndDetailsByMasterName(masterName, searchFilter);
                    return new PTResponse
                    {
                        Total = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        GlobalMasterDetailList = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList : null // Result set as Entity List
                    };
                #endregion[Global Master]

                #region[Global Master]
                case "GETGLOBALMASTERANDDETAILSBYMASTERNAMEANDSITEID":
                    globalMasterBLL = new GlobalMasterBLL();
                    tempResponse = globalMasterBLL.GetGlobalMasterAndDetailsByMasterNameAndSiteID(masterName, ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        GlobalMasterDetailList = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList : null // Result set as Entity List
                    };
                #endregion[Global Master]

                #region[Global Master]
                case "GETGLOBALMASTERANDDETAILSBYMASTERDETAILID":
                    globalMasterBLL = new GlobalMasterBLL();
                    tempResponse = globalMasterBLL.GetGlobalMasterAndDetailsByDetailID(masterName, ConvertHelper.ConvertToInteger(searchFilter));
                    return new PTResponse
                    {
                        Total = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        GlobalMasterDetailList = (tempResponse.GlobalMaster != null && tempResponse.GlobalMaster.GlobalMasterDetailList != null) ? tempResponse.GlobalMaster.GlobalMasterDetailList : null // Result set as Entity List
                    };
                #endregion[Global Master]

                #region[Routers]
                case "GETALLROUTERS":
                    routerBLL = new RouterBLL();
                    tempResponse = routerBLL.GetAllRouters(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.RouterList != null ? tempResponse.RouterList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        RouterList = tempResponse.RouterList // Result set as Entity List
                    };
                #endregion[Routers]

                #region[PhoneSystems]
                case "GETALLPHONESYSTEMS":
                    phoneSystemBLL = new PhoneSystemBLL();
                    tempResponse = phoneSystemBLL.GetAllPhoneSystems(ConvertHelper.ConvertToInteger(siteID), searchFilter);
                    return new PTResponse
                    {
                        Total = tempResponse.PhoneSystemList != null ? tempResponse.PhoneSystemList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        PhoneSystemList = tempResponse.PhoneSystemList // Result set as Entity List
                    };
                #endregion[PhoneSystems]

                #region[SITE]
                case "GETALLSITES":
                    siteBLL = new SiteBLL();
                    tempResponse = siteBLL.GetAllSites(ConvertHelper.ConvertToInteger(siteID), ConvertHelper.ConvertToInteger(searchFilter));
                    return new PTResponse
                    {
                        Total = tempResponse.SiteList != null ? tempResponse.SiteList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        SiteList = tempResponse.SiteList // Result set as Entity List
                    };
                #endregion[SITE]

                #region[Users]
                case "GETALLUSERS":

                    userBLL = new UserBLL();
                    tempResponse = userBLL.GetAllUsers(ConvertHelper.ConvertToInteger(siteID), searchFilter);
                    return new PTResponse
                    {
                        Total = tempResponse.UserList != null ? tempResponse.UserList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        UserList = tempResponse.UserList // Result set as Entity List
                    };
                #endregion[Users]

                #region[Firewalls]
                case "GETALLFIREWALLS":

                    firewallBLL = new FirewallBLL();
                    tempResponse = firewallBLL.GetAllFirewalls(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.FirewallList != null ? tempResponse.FirewallList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        FirewallList = tempResponse.FirewallList // Result set as Entity List
                    };
                #endregion[Firewalls]

                #region[NetworkSwitchs]
                case "GETALLNETWORKSWITCHES":

                    networkSwitchBLL = new NetworkSwitchBLL();
                    tempResponse = networkSwitchBLL.GetAllNetworkSwitchs(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.NetworkSwitchList != null ? tempResponse.NetworkSwitchList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        NetworkSwitchList = tempResponse.NetworkSwitchList // Result set as Entity List
                    };
                #endregion[NetworkSwitchs]

                #region[ServerHardware]
                case "GETALLSERVERHARDWARES":

                    serverHardwareBLL = new ServerHardwareBLL();
                    tempResponse = serverHardwareBLL.GetAllServerHardwares(ConvertHelper.ConvertToInteger(siteID));

                    return new PTResponse
                    {
                        Total = tempResponse.ServerHardwarList != null ? tempResponse.ServerHardwarList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        ServerHardwarList = tempResponse.ServerHardwarList// Result set as Entity List
                    };


                #endregion[ServerHardware]

                #region[WorkStationHardware]
                case "GETALLWORKSTATIONHARDWARES":

                    workStationHardwareBLL = new WorkStationHardwareBLL();
                    tempResponse = workStationHardwareBLL.GetAllWorkStationHardwares(ConvertHelper.ConvertToInteger(siteID));

                    return new PTResponse
                    {
                        Total = tempResponse.WorkStationHardwareList != null ? tempResponse.WorkStationHardwareList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        WorkStationHardwareList = tempResponse.WorkStationHardwareList // Result set as Entity List
                    };


                #endregion[WorkStationHardware]

                #region[LaptopHardware]
                case "GETALLLAPTOPHARDWARES":

                    laptopHardwareBLL = new LaptopHardwareBLL();
                    tempResponse = laptopHardwareBLL.GetAllLaptopHardwares(ConvertHelper.ConvertToInteger(siteID));

                    return new PTResponse
                    {
                        Total = tempResponse.LaptopHardwareList != null ? tempResponse.LaptopHardwareList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        LaptopHardwareList = tempResponse.LaptopHardwareList // Result set as Entity List
                    };


                #endregion[LaptopHardware]

                #region[ServerInfo]
                case "GETALLSERVERINFO":

                    serverInfoBLL = new ServerInfoBLL();
                    tempResponse = serverInfoBLL.GetAllServerInfo(ConvertHelper.ConvertToInteger(siteID), searchFilter);

                    return new PTResponse
                    {
                        Total = tempResponse.ServerInfoList != null ? tempResponse.ServerInfoList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        ServerInfoList = tempResponse.ServerInfoList// Result set as Entity List
                    };


                #endregion[ServerInfo]

                #region[WorkStationInfo]
                case "GETALLWORKSTATIONINFO":

                    workStationInfoBLL = new WorkStationInfoBLL();
                    tempResponse = workStationInfoBLL.GetAllWorkStationInfo(ConvertHelper.ConvertToInteger(siteID), searchFilter);

                    return new PTResponse
                    {
                        Total = tempResponse.WorkStationInfoList != null ? tempResponse.WorkStationInfoList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        WorkStationInfoList = tempResponse.WorkStationInfoList // Result set as Entity List
                    };


                #endregion[WorkStationInfo]

                #region[LaptopInfo]
                case "GETALLLAPTOPINFO":

                    laptopInfoBLL = new LaptopInfoBLL();
                    tempResponse = laptopInfoBLL.GetAllLaptopInfo(ConvertHelper.ConvertToInteger(siteID), searchFilter);
                    return new PTResponse
                    {
                        Total = tempResponse.LaptopInfoList != null ? tempResponse.LaptopInfoList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        LaptopInfoList = tempResponse.LaptopInfoList // Result set as Entity List
                    };


                #endregion[LaptopInfo]

                #region[MobileDevices]
                case "GETALLMOBILEDEVICES":

                    mobileDeviceBLL = new MobileDeviceBLL();
                    tempResponse = mobileDeviceBLL.GetAllMobileDevices(ConvertHelper.ConvertToInteger(siteID), searchFilter);
                    return new PTResponse
                    {
                        Total = tempResponse.MobileDeviceList != null ? tempResponse.MobileDeviceList.Count : 0,   //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        MobileDeviceList = tempResponse.MobileDeviceList // Result set as Entity List
                    };
                #endregion[MobileDevices]

                #region[Wirelesss]
                case "GETALLWIRELESSES":

                    wirelessBLL = new WirelessBLL();
                    tempResponse = wirelessBLL.GetAllWirelesss(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.WirelessList != null ? tempResponse.WirelessList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        WirelessList = tempResponse.WirelessList // Result set as Entity List
                    };
                #endregion[Wirelesss]

                #region[Softwares]
                case "GETALLSOFTWARES":

                    softwareBLL = new SoftwareBLL();
                    tempResponse = softwareBLL.GetAllSoftwares(ConvertHelper.ConvertToInteger(siteID), searchFilter);
                    return new PTResponse
                    {
                        Total = tempResponse.SoftwareList != null ? tempResponse.SoftwareList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        SoftwareList = tempResponse.SoftwareList // Result set as Entity List
                    };
                #endregion[Softwares]

                #region[Printers]
                case "GETALLPRINTERS":

                    printerBLL = new PrinterBLL();
                    tempResponse = printerBLL.GetAllPrinters(ConvertHelper.ConvertToInteger(siteID), searchFilter);
                    return new PTResponse
                    {
                        Total = tempResponse.PrinterList != null ? tempResponse.PrinterList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        PrinterList = tempResponse.PrinterList // Result set as Entity List
                    };
                #endregion[Printers]

                #region[NetworkShare]
                case "GETALLNETWORKSHARE":

                    networkShareBLL = new NetworkShareBLL();
                    tempResponse = networkShareBLL.GetAllNetworkShare(ConvertHelper.ConvertToInteger(siteID), searchFilter);

                    return new PTResponse
                    {
                        Total = tempResponse.NetworkShareDetailList != null ? tempResponse.NetworkShareDetailList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        NetworkShareDetailList = tempResponse.NetworkShareDetailList // Result set as Entity List
                    };


                #endregion[NetworkShare]

                #region[Check Lists]
                case "GETALLCHECKLIST":

                    checklistBLL = new ChecklistBLL();
                    tempResponse = checklistBLL.GetAllChecklists(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.ChecklistItems != null ? tempResponse.ChecklistItems.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        ChecklistItems = tempResponse.ChecklistItems // Result set as Entity List
                    };
                #endregion[Check Lists]]

                #region [GroupPolicySetup]
                case "GETALLGROUPPOLICYSETUP":

                    groupPolicyBLL = new GroupPolicyBLL();
                    tempResponse = groupPolicyBLL.GetAllGroupPolicySetup(ConvertHelper.ConvertToInteger(siteID));

                    return new PTResponse
                    {
                        Total = tempResponse.GroupPolicySetupList != null ? tempResponse.GroupPolicySetupList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        GroupPolicySetupList = tempResponse.GroupPolicySetupList // Result set as Entity List
                    };

                #endregion [GroupPolicySetup]

                #region[InternetDomain]
                case "GETALLINTERNETDOMAINS":

                    internetDomainBLL = new InternetDomainBLL();
                    tempResponse = internetDomainBLL.GetAllInternetDomains(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.InternetDomainList != null ? tempResponse.InternetDomainList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        InternetDomainList = tempResponse.InternetDomainList // Result set as Entity List
                    };
                #endregion[InternetDomain]

                #region[InternetProvider]
                case "GETALLINTERNETPROVIDERS":

                    internetProviderBLL = new InternetProviderBLL();
                    tempResponse = internetProviderBLL.GetAllInternetProviders(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.InternetProviderList != null ? tempResponse.InternetProviderList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        InternetProviderList = tempResponse.InternetProviderList // Result set as Entity List
                    };
                #endregion[InternetProvider]

                #region[InternetEmailHost]
                case "GETALLINTERNETEMAILHOSTS":

                    internetEmailHostBLL = new InternetEmailHostBLL();
                    tempResponse = internetEmailHostBLL.GetAllInternetEmailHosts(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.InternetEmailHostList != null ? tempResponse.InternetEmailHostList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        InternetEmailHostList = tempResponse.InternetEmailHostList // Result set as Entity List
                    };
                #endregion[InternetEmailHost]

                #region[InternetWebHost]
                case "GETALLINTERNETWEBHOSTS":

                    internetWebHostBLL = new InternetWebHostBLL();
                    tempResponse = internetWebHostBLL.GetAllInternetWebHosts(ConvertHelper.ConvertToInteger(siteID));
                    return new PTResponse
                    {
                        Total = tempResponse.InternetWebHostList != null ? tempResponse.InternetWebHostList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        InternetWebHostList = tempResponse.InternetWebHostList // Result set as Entity List
                    };
                #endregion[InternetWebHost]

                #region [Heading Master]
                case "GETALLHEADINGMASTER":

                    headingMasterBLL = new HeadingMasterBLL();
                    tempResponse = headingMasterBLL.GetAllHeadingMasters();
                    return new PTResponse
                    {
                        Total = tempResponse.HeadingMasterList != null ? tempResponse.HeadingMasterList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        HeadingMasterList = tempResponse.HeadingMasterList // Result set as Entity List
                    };

                #endregion [Heading Master]

                #region [Hard Drive]
                case "GETALLHARDDISK":

                    hardDiskBLL = new HardDiskBLL();
                    tempResponse = hardDiskBLL.GetAllHardDisks();
                    return new PTResponse
                    {
                        Total = tempResponse.HardDiskList != null ? tempResponse.HardDiskList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        HardDiskList = tempResponse.HardDiskList // Result set as Entity List
                    };
                #endregion [Hard Drive]

                #region[ApplicationUsers]
                case "GETALLAPPLICATIONUSER":

                    applicationUserBLL = new ApplicationUserBLL();
                    tempResponse = applicationUserBLL.GetAllApplicationUsers();
                    return new PTResponse
                    {
                        Total = tempResponse.ApplicationUserList != null ? tempResponse.ApplicationUserList.Count : 0, //total number of records in our grid/result set
                        Page = 1,// default page to show in the grid
                        Records = pageSize, //How many records to show on each page
                        ApplicationUserList = tempResponse.ApplicationUserList // Result set as Entity List
                    };
                #endregion[ApplicationUsers]

                #region[AutoTask]
                case "GETALLCUSTOMERSAUTOTASK":
                    return AutoTaskCustomer(masterName, searchFilter);
                case "GETALLUSERAUTOTASK":
                    return AutoTaskUser(masterName, searchFilter);
                #endregion[AutoTask]
                    
            }
            return response;
        }
        #endregion [GetData]

        #region [GlobalMasterCrud]
        public PTResponse GlobalMasterCrud(GlobalMasterDetail objGlobalMaster, string masterName, string ApplicationID)
        {

            //if (objGlobalMaster != null)
            //{
            GlobalMasterBLL globalMasterBLL = new GlobalMasterBLL();
            PTResponse response = new PTResponse();
            objGlobalMaster.ModifiedBy = ConvertHelper.ConvertToInteger(ApplicationID);
            objGlobalMaster.CreatedBy = ConvertHelper.ConvertToInteger(ApplicationID);
            objGlobalMaster.MasterDetailID = objGlobalMaster.id;
            if (objGlobalMaster.oper.ToUpper() == "ADD")
            {
                //Add
                response = globalMasterBLL.GlobalMasterDetailAdd(objGlobalMaster, masterName);
            }
            else if (objGlobalMaster.oper.ToUpper() == "EDIT")
            {
                //Edit
                response = globalMasterBLL.GlobalMasterDetailUpdateByMasterDetailID(objGlobalMaster, masterName);
            }
            else if (objGlobalMaster.oper.ToUpper() == "DEL")
            {
                objGlobalMaster.MasterDetailID = objGlobalMaster.id;
                //Delete
                response = globalMasterBLL.GlobalMasterDetailDeleteByMasterDetailID(objGlobalMaster);
            }
            return response;
            //}
            //else
            //{
            //    return response;
            //}
        }
        #endregion [GlobalMasterCrud]

        #region [GetSiteDetialsBySiteID]
        public PTResponse GetSiteDetailsBySite(string siteID)
        {
            siteBLL = new SiteBLL();
            response = siteBLL.GetSiteBySiteID(ConvertHelper.ConvertToInteger(siteID));
            return response;
        }
        #endregion [GetSiteDetialsBySiteID]

        #region[Get Customer By SearchKey]
        public PTResponse GetCustomerBySearchKey(string CustomerCode, string CustomerName, string CompanyName, string viewLink, string masterName)
        {
            customerBLL = new CustomerBLL();
            Customer customer = new Customer();
            customer.Company = new Company();
            //Check if the value is all if so the data is not entered in the UI
            customer.CustomerCode = ConvertHelper.ConvertToString(CustomerCode == "all" ? null : CustomerCode, null);
            //customer.CustomerName = ConvertHelper.ConvertToString(CustomerName == "all" ? "" : CustomerName, "");
            //customer.Company.CompanyName = ConvertHelper.ConvertToString(CompanyName == "all" ? "" : CompanyName, "");
            //customer.PhoneNumber = ConvertHelper.ConvertToString(PhoneNumber == "all" ? "" : PhoneNumber, "");

            customer.CustomerName = ConvertHelper.ConvertToString("all");
            customer.Company.CompanyName = ConvertHelper.ConvertToString("all");
            customer.PhoneNumber = ConvertHelper.ConvertToString("all");
            customer.View = viewLink;
            response = customerBLL.GetCustomerBySearchKey(customer);
            return new PTResponse
            {
                Total = response.CustomerList != null ? response.CustomerList.Count : 0, //total number of records in our grid/result set
                Page = 1,// default page to show in the grid
                Records = pageSize, //How many records to show on each page
                CustomerList = response.CustomerList // Result set as Entity List
            };

        }
        #endregion[Get Customer By SearchKey]

        #region[Get Site By SearchKey]
        public PTResponse SEARCHSITEBYKEY(string masterName, string SiteName)
        {
            //Check if the value is all if so the data is not entered in the UI
            SiteName = ConvertHelper.ConvertToString(SiteName == "all" ? null : SiteName, null);

            siteBLL = new SiteBLL();
            tempResponse = siteBLL.SearchSiteByKey(ConvertHelper.ConvertToString(SiteName));
            return new PTResponse
            {
                Total = tempResponse.SiteList != null ? tempResponse.SiteList.Count : 0, //total number of records in our grid/result set
                Page = 1,// default page to show in the grid
                Records = pageSize, //How many records to show on each page
                SiteList = tempResponse.SiteList // Result set as Entity List
            };


        }
        #endregion[Get Site By SearchKey]

        #region[Get Usres By SearchKey]
        public PTResponse SEARCHUSERSBYKEY(string masterName, string UserName)
        {
            //Check if the value is all if so the data is not entered in the UI
            UserName = ConvertHelper.ConvertToString(UserName == "all" ? null : UserName, null);

            applicationUserBLL = new ApplicationUserBLL();
            tempResponse = applicationUserBLL.SearchApplicationUserByKey(ConvertHelper.ConvertToString(UserName));
            return new PTResponse
            {
                Total = tempResponse.ApplicationUserList != null ? tempResponse.ApplicationUserList.Count : 0, //total number of records in our grid/result set
                Page = 1,// default page to show in the grid
                Records = pageSize, //How many records to show on each page
                ApplicationUserList = tempResponse.ApplicationUserList // Result set as Entity List
            };
        }
        #endregion[Get Usres By SearchKey]


        #region[EMAIL Sending Method]
        //Send Email for Forgot Password
        public bool EMAIL(string toMailAddress, PTResponse response)
        {
            try
            {

                string subjectMail = "intelligIS Reset Your Password";

                string message = "Logon username : " + response.ApplicationUserList[0].ApplicationUsername + " <br>Password : " + response.ApplicationUserList[0].ApplicationPassword;

                string strBodyContent = "Dear " + response.ApplicationUserList[0].ApplicationUsername + ",<BR><BR>";
                strBodyContent = strBodyContent + "A request was made to send you your username and password for intelligIS. Your details are as follows:<br><br>" + message;
                strBodyContent = strBodyContent + "<br><br>Regards, <br>intelligIS Team";
                string BodyMail = strBodyContent;
                string fromMailAddress = System.Configuration.ConfigurationManager.AppSettings["from"];
                string CCMailAddress = string.Empty;
                Mailer.SendEmail(subjectMail, fromMailAddress, toMailAddress, CCMailAddress, BodyMail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion[EMAIL Sending Method]

        #region[MAIL TO CUSTOMER]
        //Send Email for Forgot Password
        public bool MAILERTOCUSTOMER(PTRequest request)
        {
            try
            {
                PTResponse response = new PTResponse();
                PTRequest innerRequest = new PTRequest();
                MailTemplate mailTemplate;
                string BodyMail = string.Empty;
                string subjectMail;// = "intelligIS : Customer details";
                string toMailAddress = request.Mail.To;
                string strBodyContent;// = request.Mail.PageName;
                string fromMailAddress = System.Configuration.ConfigurationManager.AppSettings["from"];
                string CCMailAddress = string.Empty;
                subjectMail = string.Empty;

                strBodyContent = "<table width='100%' bgcolor='#DFDFDF' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                   "<tr><td width='50%' height='0'>&nbsp;</td><td width='550' height='0'>&nbsp;</td><td width='50%' height='0'>&nbsp;</td></tr>" +
                                   "<tr><td width='50%'>&nbsp;</td><td height='0'><table width='700' cellpadding='10' cellspacing='0'>" +
                                  "<tr><td colspan='4'><img alt='' src='http://provisioningtool.techaffinity.com/includes/UI/images/logo.png' style='background: #fff; display: block;'/>" +
                                   "</td></tr><tr><td colspan='4' bgcolor='#333' style='color:#fff; font-size: 18px;'>";


                switch (request.Mail.PageName.ToUpper())
                {
                    #region [ User Info ]
                    case "USERINFO":
                        innerRequest.User = new User();
                        mailTemplate = new MailTemplate();
                        innerRequest.User.UserID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETUSERANDUSERDETAILSBYUSERID");
                        subjectMail = "intelligIS Provisioning Tool - User Details";
                        strBodyContent = strBodyContent + " User Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetUserDetails(response);
                        break;
                    #endregion [ User Info ]

                    #region [ Router Info Details ]
                    case "ROUTERINFO":
                        innerRequest.Router = new Router();
                        mailTemplate = new MailTemplate();
                        innerRequest.Router.RouterID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETROUTERBYROUTERID");
                        subjectMail = "intelligIS Provisioning Tool - Router Details";
                        strBodyContent = strBodyContent + " Router Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetRouterDetails(response);
                        break;
                    #endregion [ Router Info Details ]

                    #region [ Fire wall Info Details ]
                    case "FIREWALLINFO":
                        innerRequest.Firewall = new Firewall();
                        mailTemplate = new MailTemplate();
                        innerRequest.Firewall.FirewallID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETFIREWALLANDFIREWALLDETAILSBYFIREWALLID");
                        subjectMail = "intelligIS Provisioning Tool - Firewall Details";
                        strBodyContent = strBodyContent + " Firewall Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetFirewallDetails(response);
                        break;
                    #endregion [ Fire wall Info Details ]

                    #region [ Network Switch Info Details ]
                    case "NETWORKSWITCHINFO":
                        innerRequest.NetworkSwitch = new NetworkSwitch();
                        mailTemplate = new MailTemplate();
                        innerRequest.NetworkSwitch.NetworkSwitchID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETNETWORKSWITCHANDNETWORKSWITCHDETAILSBYNETWORKSWITCHID");
                        subjectMail = "intelligIS Provisioning Tool - Network Switch Details";
                        strBodyContent = strBodyContent + " Network Switch Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetNetworkSwitchDetails(response);
                        break;
                    #endregion [ Network Switch Info Details ]

                    #region [ Phone System Info Details ]
                    case "PHONESYSTEMINFO":
                        innerRequest.PhoneSystem = new PhoneSystem();
                        mailTemplate = new MailTemplate();
                        innerRequest.PhoneSystem.PhoneSystemID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETPHONESYSTEMBYPHONESYSTEMID");
                        subjectMail = "intelligIS Provisioning Tool - Phone System Details";
                        strBodyContent = strBodyContent + " Phone System Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetPhoneSystemDetails(response);
                        break;
                    #endregion [ Phone System Info Details ]

                    #region [ Server Info Details ]
                    case "SERVERINFO":
                        innerRequest.ServerInfo = new ServerInfo();
                        mailTemplate = new MailTemplate();
                        innerRequest.ServerInfo.ServerID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETSERVERINFOANDUSERDETAILSBYSERVERINFOID");
                        subjectMail = "intelligIS Provisioning Tool - Server Details";
                        strBodyContent = strBodyContent + " Server Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetServerDetails(response);
                        break;
                    #endregion [ Server Info Details ]

                    #region [ Workstation Info Details ]
                    case "WORKSTATIONINFO":
                        innerRequest.WorkStationInfo = new WorkStationInfo();
                        mailTemplate = new MailTemplate();
                        innerRequest.WorkStationInfo.WorkStationID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETWORKSTATIONINFOANDUSERDETAILSBYWORKSTATIONINFOID");
                        subjectMail = "intelligIS Provisioning Tool - Workstation Details";
                        strBodyContent = strBodyContent + " Workstation Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetWorkStationDetails(response);
                        break;
                    #endregion [ Workstation Info Details ]

                    #region [ Laptop Info Details ]
                    case "LAPTOPINFO":
                        innerRequest.LaptopInfo = new LaptopInfo();
                        mailTemplate = new MailTemplate();
                        innerRequest.LaptopInfo.LaptopID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETLAPTOPINFOANDUSERDETAILSBYLAPTOPINFOID");
                        subjectMail = "intelligIS Provisioning Tool - Laptop Details";
                        strBodyContent = strBodyContent + " Laptop Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetLaptopDetails(response);
                        break;
                    #endregion [ Laptop Info Details ]

                    #region [ Wireless Info Details ]
                    case "WIRELESSINFO":
                        innerRequest.Wireless = new Wireless();
                        mailTemplate = new MailTemplate();
                        innerRequest.Wireless.WirelessID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETWIRELESSBYWIRELESSID");
                        subjectMail = "intelligIS Provisioning Tool - Wireless Details";
                        strBodyContent = strBodyContent + " Wireless Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetWirelessInfoDetails(response);
                        break;
                    #endregion [ Wireless Info Details ]

                    #region [ Software Info Details ]
                    case "SOFTWAREINFO":
                        innerRequest.Software = new Software();
                        mailTemplate = new MailTemplate();
                        innerRequest.Software.SoftwareID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETSOFTWAREBYSOFTWAREID");
                        subjectMail = "intelligIS Provisioning Tool - Software Details";
                        strBodyContent = strBodyContent + " Software Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetSoftwareInfoDetails(response);
                        break;
                    #endregion [ Software Info Details ]

                    #region [ Printer Info Details ]
                    case "PRINTERINFO":
                        innerRequest.Printer = new Printer();
                        mailTemplate = new MailTemplate();
                        innerRequest.Printer.PrinterID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETPRINTERANDPRINTERDETAILSBYPRINTERID");
                        subjectMail = "intelligIS Provisioning Tool - Printer Details";
                        strBodyContent = strBodyContent + " Printer Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetPrinterDetails(response);
                        break;
                    #endregion [ Printer Info Details ]

                    #region [ Mobile Devices Details]
                    case "MOBILE DEVICES":
                        innerRequest.MobileDevice = new MobileDevice();
                        mailTemplate = new MailTemplate();
                        innerRequest.MobileDevice.MobileDeviceID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETMOBILEDEVICEBYMOBILEDEVICEID");
                        subjectMail = "intelligIS Provisioning Tool -Mobile Device Details";
                        strBodyContent = strBodyContent + " Mobile Device Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetMobileDevicesDetails(response);
                        break;
                    #endregion [ Mobile Devices Details ]

                    #region [ Network Share Details ]
                    case "NETWORK SHARES":
                        innerRequest.NetworkShareDetail = new NetworkShareDetail();
                        mailTemplate = new MailTemplate();
                        innerRequest.NetworkShareDetail.NetworkShareDetailID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETNETWORKSHAREDETAILBYNETWORKSHAREDETAILID");
                        subjectMail = "intelligIS Provisioning Tool - Network Share Details";
                        strBodyContent = strBodyContent + " Network Share Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetNetworkShareDetails(response);
                        break;
                    #endregion [ Network Share Details ]

                    #region [ Internet/Web - Provider ]
                    case "INTERNETWEBPROVIDER":
                        innerRequest.InternetProvider = new InternetProvider();
                        mailTemplate = new MailTemplate();
                        innerRequest.InternetProvider.ProviderID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETINTERNETPROVIDERBYINTERNETPROVIDERID");
                        subjectMail = "intelligIS Provisioning Tool - Internet/Web-Provider Details";
                        strBodyContent = strBodyContent + " Internet/Web-Provider Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetInterNetWebProviderDetails(response);
                        break;
                    #endregion [ Internet/Web - Provider ]

                    #region [ Internet/Web - Domain ]
                    case "INTERNETWEBDOMAIN":
                        innerRequest.InternetDomain = new InternetDomain();
                        mailTemplate = new MailTemplate();
                        innerRequest.InternetDomain.DomainID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETINTERNETDOMAINBYINTERNETDOMAINID");
                        subjectMail = "intelligIS Provisioning Tool - Internet/Web-Domain Details";
                        strBodyContent = strBodyContent + " Internet/Web-Domain Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetInternetWebDomainlDetails(response);
                        break;
                    #endregion [ Internet/Web - Domain ]

                    #region [ Internet/Web - Web Host]
                    case "INTERNETWEBWEBHOST":
                        innerRequest.InternetWebHost = new InternetWebHost();
                        mailTemplate = new MailTemplate();
                        innerRequest.InternetWebHost.WebHostID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETINTERNETWEBHOSTBYINTERNETWEBHOSTID");
                        subjectMail = "intelligIS Provisioning Tool - Internet/Web-Web Host Details";
                        strBodyContent = strBodyContent + " Internet/Web-Web Host</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetInterDomainWebHosteDetails(response);
                        break;
                    #endregion [ Internet/Web - Web Host ]

                    #region [ Internet/Web - Email Host]
                    case "INTERNETWEBEMAILHOST":
                        innerRequest.InternetEmailHost = new InternetEmailHost();
                        mailTemplate = new MailTemplate();
                        innerRequest.InternetEmailHost.EmailHostID = ConvertHelper.ConvertToInteger(request.Mail.PageIdentity);
                        response = ProcessData(innerRequest, "GETINTERNETEMAILHOSTBYINTERNETEMAILHOSTID");
                        subjectMail = "intelligIS Provisioning Tool - Internet/Web-Email Host Details";
                        strBodyContent = strBodyContent + " Internet/Web-Email Host Details</td></tr>";
                        BodyMail = strBodyContent + mailTemplate.GetInternetWebEmailHostDetails(response);
                        break;
                    #endregion [ Internet/Web - Email Host ]
                }
                //BodyMail = strBodyContent;
                BodyMail = BodyMail + " <tr><td colspan='4' align='center' valign='middle' style='font-size: 12px; color: #888;'>&copy; 2014 - 2015 - intelligIS - All Rights Reserved" +
                            "</td></tr></table></td><td width='50%'>&nbsp;</td></tr></table>";



                Mailer.SendEmail(subjectMail, fromMailAddress, toMailAddress, CCMailAddress, BodyMail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion[MAIL TO CUSTOMER]

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }


        #region [ Connect to Auto Task API ]
        /// <summary>
        /// Connect to Auto Task API
        ///  This function is used to Connect to Auto Task API.
        /// </summary>
        private bool ConnectToWebService()
        {

            try
            {
                if (!api.IsConnected)
                {
                    api.Connect(AutotaskUserID, AutotaskPassword);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion [ Connect to Auto Task API ]

        private PTResponse AutoTaskCustomer(string sAccountName, string sSearchField)
        {

            List<object> list = new List<object>();
            List<Customer> custList = new List<Customer>();
            List<Site> siteList = new List<Site>();
            PTResponse response = new PTResponse();
            if (ConnectToWebService())
            {
                list = api.Query(EntityEnum.Customer, sSearchField, string.IsNullOrEmpty(ConvertHelper.ConvertToString(sAccountName)) ? "" : ConvertHelper.ConvertToString(sAccountName));
                if (list != null && list.Count > 0)
                {
                    foreach (ServiceAPIWrapper.AutotaskWSDL.Account acct in list)
                    {
                        if (ConvertHelper.ConvertToString(acct.ParentAccountID, "") == "")
                        {
                            Customer customer = new Customer();
                            customer.CustomerCode = ConvertHelper.ConvertToString(acct.id);
                            customer.CustomerName = ConvertHelper.ConvertToString(acct.AccountName);
                            customer.Address = ConvertHelper.ConvertToString(acct.Address1);
                            customer.PhoneNumber = ConvertHelper.ConvertToString(acct.Phone);
                            customer.AlternatePhoneNo = ConvertHelper.ConvertToString(acct.AlternatePhone1);
                            customer.ZipCode = ConvertHelper.ConvertToString(acct.PostalCode);
                            customer.CountryName = ConvertHelper.ConvertToString(acct.Country);
                            customer.CityName = ConvertHelper.ConvertToString(acct.City);
                            customer.StateName = ConvertHelper.ConvertToString(acct.State);
                            customer.View = "<a href=CustomerInfo.aspx?do=a&AutoTaskCustomerID=" + ConvertHelper.ConvertToString(acct.id) + " id='customerId' class='addIcon'>Select</a>";
                            custList.Add(customer);
                        }
                        else
                        {
                            Site site = new Site();
                            site.CustomerCode = ConvertHelper.ConvertToString(acct.ParentAccountID);
                            site.SiteCode = ConvertHelper.ConvertToString(acct.id);
                            site.SiteName = ConvertHelper.ConvertToString(acct.AccountName);
                            site.Address1 = ConvertHelper.ConvertToString(acct.Address1);
                            site.Address2 = ConvertHelper.ConvertToString(acct.Address2);
                            site.PhoneNumber = ConvertHelper.ConvertToString(acct.Phone);
                            site.Website = ConvertHelper.ConvertToString(acct.WebAddress);
                            site.ZipCode = ConvertHelper.ConvertToString(acct.PostalCode);
                            site.CountryName = ConvertHelper.ConvertToString(acct.Country);
                            site.CityName = ConvertHelper.ConvertToString(acct.City);
                            site.StateName = ConvertHelper.ConvertToString(acct.State);
                            site.View = "<a href=CustomerSites.aspx?do=a&AutoTaskCustomerID=" + ConvertHelper.ConvertToString(acct.id) + " id='siteid' class='addIcon'>Select</a>";
                            
                            siteList.Add(site);
                        }
                    }
                }
            }
            response.CustomerList = custList;
            response.SiteList = siteList;
            return response;
        }

        private PTResponse AutoTaskUser(string sSearchValue, string sSearchField)
        {

            List<object> list = new List<object>();
            List<User> userList = new List<User>();
            PTResponse response = new PTResponse();
            if (ConnectToWebService())
            {
                list = api.Query(EntityEnum.User, sSearchField, string.IsNullOrEmpty(ConvertHelper.ConvertToString(sSearchValue)) ? "" : ConvertHelper.ConvertToString(sSearchValue));
                if (list != null && list.Count > 0)
                {
                    foreach (ServiceAPIWrapper.AutotaskWSDL.Contact cont in list)
                    {

                        User user = new User();
                        user.UserID = ConvertHelper.ConvertToInteger(cont.id, 0);
                        user.FirstName = ConvertHelper.ConvertToString(cont.FirstName, "");
                        user.LastName = ConvertHelper.ConvertToString(cont.LastName, "");
                        user.UserName = string.Empty;
                        user.Password = string.Empty;
                        user.Email = ConvertHelper.ConvertToString(cont.EMailAddress, "");
                        user.Phone1 = ConvertHelper.ConvertToString(cont.Phone, "");
                        user.Phone2 = ConvertHelper.ConvertToString(cont.AlternatePhone, "");
                        user.View = "<a href=CustomerInfo.aspx?do=a&nav=Users&AutoTaskCustomerID=" + ConvertHelper.ConvertToString(cont.id) + " id='userid' class='addIcon'>Select</a>";
                            
                        userList.Add(user);
                    }
                }
            }
            response.UserList = userList;
            return response;
        }


    }

}

