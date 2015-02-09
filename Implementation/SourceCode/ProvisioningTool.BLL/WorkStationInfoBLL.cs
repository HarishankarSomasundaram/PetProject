using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.BLL
{
    public class WorkStationInfoBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public WorkStationInfoBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllWorkStationInfos ]
        public PTResponse GetAllWorkStationInfo(int siteID, string searchFilter)
        {
            try
            {
                response.WorkStationInfoList = dataAdapter.GetAllWorkStationInfo(siteID, searchFilter);
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

        #region [ Get WorkStationInfo By WorkStationInfo ID ]
        public PTResponse GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(int WorkStationInfoID)
        {
            try
            {
                response.WorkStationInfo = dataAdapter.GetWorkStationInfoAndUserDetailsByWorkStationInfoID(WorkStationInfoID);
                if (response.WorkStationInfo == null)
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

        #region [ Save WorkStationInfo ]
        public PTResponse SaveWorkStationInfo(PTRequest request)
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
                                dataAdapter.AddWorkStationInfo(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Workstation details has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Workstation already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Workstation.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyWorkStationInfo(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Workstation details has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Workstation details already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Workstation.";
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
        #endregion [ Save WorkStationInfo ]

        #region [Delete WorkStationInfo]
        public bool DeleteWorkStationInfoByWorkStationInfoID(int WorkStationInfoID)
        {

            try
            {
                return dataAdapter.DeleteWorkStationInfoByWorkStationInfoID(WorkStationInfoID);
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
