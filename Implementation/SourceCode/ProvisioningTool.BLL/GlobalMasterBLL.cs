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
    public class GlobalMasterBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        bool isUpdated;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public GlobalMasterBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            isUpdated = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [Get Global Master Details By Master Name ]
        public PTResponse GetGlobalMasterAndDetailsByMasterName(string masterName, string searchFilter)
        {
            try
            {
                response.GlobalMaster = dataAdapter.GetGlobalMasterAndDetailsByMasterName(masterName, searchFilter);
                if (response.GlobalMasterList == null)
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
        #endregion [Get Global Master Details By Master Name And SiteID]

        #region [Get Global Master Details By Master Name ]
        public PTResponse GetGlobalMasterAndDetailsByMasterNameAndSiteID(string masterName, int siteID)
        {
            try
            {
                response.GlobalMaster = dataAdapter.GetGlobalMasterAndDetailsByMasterNameAndSiteID(masterName, siteID);
                if (response.GlobalMasterList == null)
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
        #endregion [Get Global Master Details By Master Name And SiteID]


        #region [Get Global Master Details By Master Detail ID ]
        public PTResponse GetGlobalMasterAndDetailsByDetailID(string masterName, int masterDetailID)
        {
            try
            {
                response.GlobalMaster = dataAdapter.GetGlobalMasterAndDetailsByDetailID(masterName, masterDetailID);
                if (response.GlobalMasterList == null)
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
        #endregion [Get Global Master Details By Master Name And SiteID]


        #region [GlobalMasterDetailAdd]

        public PTResponse GlobalMasterDetailAdd(GlobalMasterDetail globalMasterDetail, string masterName)
        {
            try
            {
                isUpdated = dataAdapter.GlobalMasterDetailAdd(globalMasterDetail, masterName);
                if (isUpdated)
                {
                    response.isSuccess = true;
                }
                else
                {
                    response.isSuccess = false;
                    response.Message = "Record already exists";
                }

                return response;
            }
            catch (Exception e)
            {
                //throw;
                response.Message = e.Message;
                response.isSuccess = false;
                return response;
            }
        }
        #endregion [GlobalMasterDetailAdd]

        #region [GlobalMasterDetailUpdateByMasterDetailID ]

        public PTResponse GlobalMasterDetailUpdateByMasterDetailID(GlobalMasterDetail globalMasterDetail, string masterName)
        {
            try
            {
                isUpdated = dataAdapter.GlobalMasterDetailUpdateByMasterDetailID(globalMasterDetail, masterName);
                if (isUpdated)
                {
                    response.isSuccess = true;
                }
                else
                {
                    response.isSuccess = false;
                    response.Message = "Record already exists";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion [GlobalMasterDetailUpdateByMasterDetailID];

        #region [GlobalMasterDetailDeleteByMasterDetailID ]

        public PTResponse GlobalMasterDetailDeleteByMasterDetailID(GlobalMasterDetail globalMasterDetail)
        {
            try
            {
                isUpdated = dataAdapter.GlobalMasterDetailDeleteByMasterDetailID(globalMasterDetail);
                if (isUpdated)
                {
                    response.isSuccess = true;
                }
                else
                {
                    response.isSuccess = false;
                    response.Message = "Related records found !";
                }
                return response;
            }
            catch (Exception e)
            {
                //throw;
                response.Message = e.Message;
                response.isSuccess = false;
                return response;
            }
        }
        #endregion [GlobalMasterDetailDeleteByMasterDetailID];
    }
}
