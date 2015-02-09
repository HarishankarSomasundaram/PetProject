using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;
using ProvisioningTool.Entity;

namespace ProvisioningTool.BLL
{
    public class NetworkSwitchBLL
    {
        #region [ Variable Declarations ]
        PTResponse response;
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public NetworkSwitchBLL()
        {
            dataAdapter = new DalAdapter();
            response = new PTResponse();
            this.rowsAffected = 0;
            this.isDuplicate = false;
        }
        #endregion [ Constructor ]

        #region [ GetAllNetworkSwitchs ]
        public PTResponse GetAllNetworkSwitchs(int siteID)
        {
            try
            {
                response.NetworkSwitchList = dataAdapter.GetAllNetworkSwitchs(siteID);
                if (response.NetworkSwitchList == null)
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
        #endregion

        #region [ Get NetworkSwitch By NetworkSwitch ID ]
        public PTResponse GetNetworkSwitchAndNetworkSwitchDetailsByNetworkSwitchID(int NetworkSwitchID)
        {
            try
            {
                response.NetworkSwitch = dataAdapter.GetNetworkSwitchByNetworkSwitchID(NetworkSwitchID);
                if (response.NetworkSwitch == null)
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

        #region [ Save NetworkSwitch ]
        public PTResponse SaveNetworkSwitch(PTRequest request)
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
                                dataAdapter.AddNetworkSwitchs(request.NetworkSwitch, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Network Switch has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Network Switch already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Network Switch.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyNetworkSwitch(request.NetworkSwitch, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Network Switch has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Network Switch already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Network Switch.";
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
        #endregion [ Save NetworkSwitch ]

        #region [ Delete NetworkSwitch ]
        public bool DeleteNetworkSwitchByNetworkSwitchID(int NetworkSwitchID)
        {

            try
            {
                return dataAdapter.DeleteNetworkSwitchByNetworkSwitchID(NetworkSwitchID);
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


