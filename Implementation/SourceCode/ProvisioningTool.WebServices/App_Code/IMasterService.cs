using ProvisioningTool.Entity;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ProvisioningToolServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerServices" in both code and config file together.
    [ServiceContract]
    public interface IMasterService
    {
        #region[GlobalMaster]
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GlobalMasterCrud/{MasterName}/{ApplicationID}")]
        PTResponse GlobalMasterCrud(GlobalMasterDetail objGlobalMaster, string masterName, string ApplicationID);
        #endregion[GlobalMaster]

        #region[ProcessData]
        // This method will be used for C# Only
        [OperationContract(Name = "ProcessData")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ProcessData/{MethodName}")]
        PTResponse ProcessData(PTRequest request, string methodName);
        #endregion[ProcessData]

        #region[GetData]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "GetData")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetData/{MethodName}/{masterName}/{siteID}/{searchFilter}")]
        PTResponse GetData(string methodName, string masterName, string siteID, string searchFilter);
        #endregion[GetData]

        #region[GetDataForColorBox]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "GetDataForColorBox")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDataForColorBox/{MethodName}/{masterName}/{siteID}/{searchText}")]
        PTResponse GetDataForColorBox(string methodName, string masterName, string siteID, string searchText);
        #endregion[GetDataForColorBox]

        #region[GetData]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "GetSiteDetailsBySite")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSiteDetailsBySite/{siteID}")]
        PTResponse GetSiteDetailsBySite(string siteID);
        #endregion[GetData]

        #region[GetData]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "GetCustomerBySearchKey")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerBySearchKey/{CustomerCode}/{CustomerName}/{CompanyName}/{viewLink}/{masterName}")]
        PTResponse GetCustomerBySearchKey(string CustomerCode, string CustomerName, string CompanyName, string viewLink, string masterName);
        #endregion[GetData]

        #region[GetData]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "SEARCHSITEBYKEY")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SEARCHSITEBYKEY/{masterName}/{SiteName}")]
        PTResponse SEARCHSITEBYKEY(string masterName, string SiteName);
        #endregion[GetData]

        #region[GetData]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "SEARCHUSERSBYKEY")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SEARCHUSERSBYKEY/{masterName}/{UserName}")]
        PTResponse SEARCHUSERSBYKEY(string masterName, string UserName);
        #endregion[GetData]
        
        #region[LoginUser]
        [OperationContract]
        [WebGet(UriTemplate = "Login", ResponseFormat = WebMessageFormat.Json)]
        string LoginUser();
        #endregion[LoginUser]

        #region[Get Data for Mobile Application]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "GetMobileService")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMobileService/{methodName}/{param1}/{param2}/{param3}")]
        PTResponse GetMobileService(string methodName, string param1, string param2, string param3);
        #endregion[Get Data for Mobile Application]

        #region[Update Data for Mobile Application]
        // This method will be used for ASPX / JS Call
        [OperationContract(Name = "UpdateMobileService")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateMobileService/{methodName}/{param1}/{param2}/{param3}/{param4}")]
        PTResponse UpdateMobileService(string methodName, string param1, string param2, string param3, string param4);
        #endregion[Update Data for Mobile Application]

    }
}
