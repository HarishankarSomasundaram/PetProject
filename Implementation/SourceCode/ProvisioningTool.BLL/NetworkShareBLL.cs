using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvisioningTool.BLL
{
    public class NetworkShareBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected, iNetWorkShareID;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public NetworkShareBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = iNetWorkShareID = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllNetworkShares ]
        public PTResponse GetAllNetworkShare(int siteID, string searchFilter)
        {
            try
            {
                response.NetworkShareDetailList = dataAdapter.GetAllNetworkShare(siteID, searchFilter);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion


        #region [ Get NetworkShare By NetworkShare ID ]
        public PTResponse GetNetworkShareDetailsByNetworkShareDetailID(int NetworkShareID)
        {
            try
            {
                response.NetworkShareDetail = dataAdapter.GetNetworkShareDetailsByNetworkShareDetailID(NetworkShareID);
                if (response.NetworkShareDetail == null)
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
        #endregion [GetAllCustomerByCustomerID]

        #region [ Save NetworkShare ]
        public PTResponse SaveNetworkShare(PTRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException("Invalid Request Recieved or Request is null");
                if (request.CurrentAction == ActionType.Add || request.CurrentAction == ActionType.Edit)
                {
                    try
                    {
                        if (CheckAttributes(request))
                        {
                            if (request.CurrentAction == ActionType.Add)
                            {
                                dataAdapter.AddNetworkShare(request, out isDuplicate, out rowsAffected, out iNetWorkShareID);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    List<NetworkShareDetail> networkShareDetailList = request.NetworkShare.NetworkShareDetail;
                                    request.NetworkShareDetail = new NetworkShareDetail();
                                    request.NetworkShareDetail.NetworkShareID = iNetWorkShareID;
                                    if (networkShareDetailList != null)
                                    {
                                        for (int i = 0; i < networkShareDetailList.Count; i++)
                                        {
                                            request.NetworkShareDetail.Mapped = networkShareDetailList[i].Mapped;
                                            request.NetworkShareDetail.Path = networkShareDetailList[i].Path;
                                            request.NetworkShareDetail.NetworkShareDescription = networkShareDetailList[i].NetworkShareDescription;
                                            request.NetworkShareDetail.NetworkShareAssignedUserIDs = networkShareDetailList[i].NetworkShareAssignedUserIDs;
                                            dataAdapter.AddNetworkShare(request, out isDuplicate, out rowsAffected);
                                        }
                                    }
                                    response.isSuccess = true;
                                    response.Message = "Network Share has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Network Share already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Network Share.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyNetworkShare(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Network Share has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Network Share already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Network Share...";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return response;
            }
            finally { }
        }
        #endregion [ Save NetworkShare ]

        #region [Delete NetworkShare]
        public bool DeleteNetworkShareByNetworkShareID(int NetworkShareID)
        {

            try
            {
                return dataAdapter.DeleteNetworkShareByNetworkShareID(NetworkShareID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private bool CheckAttributes(PTRequest request)
        {
            return true;
        }
    }
}
