using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvisioningTool.BLL
{
    public class WorkStationHardwareBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public WorkStationHardwareBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllWorkStationHardwares ]
        public PTResponse GetAllWorkStationHardwares(int siteID)
        {
            try
            {
                response.WorkStationHardwareList = dataAdapter.GetAllWorkStationHardware(siteID);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion



        #region [ Get WorkStationHardware By WorkStationHardware ID ]
        public PTResponse GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(int WorkStationHardwareID)
        {
            try
            {
                response.WorkStationHardware = dataAdapter.GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(WorkStationHardwareID);
                if (response.WorkStationHardware == null)
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

        #region [ Save WorkStationHardware ]
        public PTResponse SaveWorkStationHardware(PTRequest request)
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
                            if (request.CurrentAction == ActionType.Edit || request.CurrentAction == ActionType.Add)
                            {
                                dataAdapter.AddWorkStationHardware(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Workstation Hardware has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Workstation Hardware already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Workstation Hardware.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyWorkStationHardware(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Workstation Hardware has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Workstation Hardware already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Workstation Hardware.";
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
        #endregion [ Save WorkStationHardware ]

        #region [Delete WorkStationHardware]
        public bool DeleteWorkStationHardwareByWorkStationHardwareID(int WorkStationHardwareID)
        {

            try
            {
                return dataAdapter.DeleteWorkStationHardwareByWorkStationHardwareID(WorkStationHardwareID);
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
