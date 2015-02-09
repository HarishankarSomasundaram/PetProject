using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.BLL
{
    public class SiteBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public SiteBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ Public Methods ]

        #region [ Save Site ]
        public PTResponse SaveSite(PTRequest request)
        {
            if (request == null) throw new ArgumentNullException("Invalid Request Recieved or Request is null");
            try
            {
                if (CheckAttributes(request))
                {
                    if (request.CurrentAction == ActionType.Add)
                    {
                        dataAdapter.AddSite(request.Site, out isDuplicate, out rowsAffected);
                    }
                    else if (request.CurrentAction == ActionType.Edit)
                    {
                        dataAdapter.ModifySite(request.Site, out isDuplicate, out rowsAffected);
                    }
                    if (!isDuplicate && rowsAffected == 1)
                    {
                        response.isSuccess = true;
                        response.isDuplicate = false;
                        response.Message = "Site details has been saved successfully.";
                    }
                    else if (isDuplicate && rowsAffected == 0)
                    {
                        response.isSuccess = false;
                        response.isDuplicate = true;
                        response.Message = "Site name already exist(s).";
                    }
                    else if (!isDuplicate && rowsAffected == 0)
                    {
                        response.isSuccess = false;
                        response.isDuplicate = false;
                        response.Message = "Error while saving site details.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
        #endregion [ Save Site ]

        #region [ GetAllSites ]
        public PTResponse GetAllSites(int CustomerID, int searchFilter)
        {
            try
            {
                response.SiteList = dataAdapter.GetAllSites(CustomerID, searchFilter);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion [GetAllSites]

        #region [ GetSiteBySiteID ]
        public PTResponse GetSiteBySiteID(int siteID)
        {
            try
            {
                response.Site = dataAdapter.GetSiteBySiteID(siteID);
                if (response.Site == null)
                    response.isSuccess = false;
                else
                    response.isSuccess = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion [GetSiteBySiteID]

        #region [ SearchSiteByKey ]
        public PTResponse SearchSiteByKey(string Key)
        {
            try
            {
                response.SiteList = dataAdapter.SearchSiteByKey(Key);
                if (response.SiteList == null)
                    response.isSuccess = false;
                else
                    response.isSuccess = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion [SearchSiteByKey]

        #region [ GetSiteByCustomerID ]
        public PTResponse GetSiteByCustomerID(int siteID)
        {
            try
            {
                response.SiteList = dataAdapter.GetSitesByCustomerID(siteID);
                if (response.SiteList == null)
                    response.isSuccess = false;
                else
                    response.isSuccess = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion [GetSiteByCustomerID]

        public bool DeleteSiteBySiteID(int customerID)
        {

            try
            {
                return dataAdapter.DeleteSiteBySiteID(customerID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion[ Public Methods ]

        private bool CheckAttributes(PTRequest request)
        {
            return true;
        }
    }
}
