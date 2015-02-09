using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.BLL
{
    public class LaptopInfoBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public LaptopInfoBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllLaptopInfos ]
        public PTResponse GetAllLaptopInfo(int siteID, string searchFilter)
        {
            try
            {
                response.LaptopInfoList = dataAdapter.GetAllLaptopInfo(siteID, searchFilter);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region [ GetAllHardDisk ]
        public PTResponse GetAllHardDisk()
        {
            try
            {
                response.SystemHardDriveList = dataAdapter.GetAllHardDisk();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region [ Get LaptopInfo By LaptopInfo ID ]
        public PTResponse GetLaptopHardwarAndUserDetailsByLaptopHardwarID(int LaptopInfoID)
        {
            try
            {
                response.LaptopInfo = dataAdapter.GetLaptopInfoAndUserDetailsByLaptopInfoID(LaptopInfoID);
                if (response.LaptopInfo == null)
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

        #region [ Save LaptopInfo ]
        public PTResponse SaveLaptopInfo(PTRequest request)
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
                                dataAdapter.AddLaptopInfo(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Laptop Info has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Laptop Info already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving LaptopInfo.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyLaptopInfo(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Laptop Info has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Laptop Info already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating LaptopInfo.";
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
        #endregion [ Save LaptopInfo ]

        #region [Delete LaptopInfo]
        public bool DeleteLaptopInfoByLaptopInfoID(int LaptopInfoID)
        {

            try
            {
                return dataAdapter.DeleteLaptopInfoByLaptopInfoID(LaptopInfoID);
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
