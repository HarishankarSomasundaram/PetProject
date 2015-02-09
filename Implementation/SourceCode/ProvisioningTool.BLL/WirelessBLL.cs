using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.BLL
{
    public class WirelessBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public WirelessBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllWirelesss ]
        public PTResponse GetAllWirelesss(int siteID)
        {
            try
            {
                response.WirelessList = dataAdapter.GetAllWirelesss(siteID);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region [ Get Wireless By Wireless ID ]
        public PTResponse GetWirelessAndWirelessDetailsByWirelessID(int wirelessID)
        {
            try
            {
                response.Wireless = dataAdapter.GetWirelessAndWirelessDetailsByWirelessID(wirelessID);
                if (response.Wireless == null)
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

        #region [ Save Wireless ]
        public PTResponse SaveWireless(PTRequest request)
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
                                dataAdapter.AddWirelesss(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Wireless has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Wireless already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Wireless.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyWireless(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Wireless has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Wireless already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Wireless.";
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
        #endregion [ Save Wireless ]

        #region [Delete Wireless]
        public bool DeleteWirelessByWirelessID(int wirelessID)
        {

            try
            {
                return dataAdapter.DeleteWirelessByWirelessID(wirelessID);
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
