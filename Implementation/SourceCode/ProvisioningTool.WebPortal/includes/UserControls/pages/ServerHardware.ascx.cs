using Library;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class UserControlServerHardware : System.Web.UI.UserControl
{
    #region [ Variable Declarations ]
    CompanyBLL companyBLL;
    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;
    string baseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    string masterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    #endregion [ Variable Declarations ]

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}