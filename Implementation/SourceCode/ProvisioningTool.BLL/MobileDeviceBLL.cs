using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.BLL
{
    public class MobileDeviceBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public MobileDeviceBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllMobileDevices ]
        public PTResponse GetAllMobileDevices(int siteID, string searchFilter)
        {
            try
            {
                response.MobileDeviceList = dataAdapter.GetAllMobileDevices(siteID, searchFilter);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region [ Get MobileDevice By MobileDevice ID ]
        public PTResponse GetMobileDeviceAndMobileDeviceDetailsByMobileDeviceID(int mobileDeviceID)
        {
            try
            {
                response.MobileDevice = dataAdapter.GetMobileDeviceAndMobileDeviceDetailsByMobileDeviceID(mobileDeviceID);
                if (response.MobileDevice == null)
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

        #region [ Save MobileDevice ]
        public PTResponse SaveMobileDevice(PTRequest request)
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
                                dataAdapter.AddMobileDevices(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Mobile Device has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Mobile Device already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Mobile Device.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyMobileDevice(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Mobile Device has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Mobile Device already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Mobile Device.";
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
        #endregion [ Save MobileDevice ]

        #region [Delete MobileDevice]
        public bool DeleteMobileDeviceByMobileDeviceID(int mobileDeviceID)
        {
            bool isdeleted = false;
            try
            {
                isdeleted =dataAdapter.DeleteMobileDeviceByMobileDeviceID(mobileDeviceID);
                return isdeleted;
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
