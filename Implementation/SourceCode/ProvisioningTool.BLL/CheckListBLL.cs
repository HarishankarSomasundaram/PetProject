using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;
using ProvisioningTool.Entity;

namespace ProvisioningTool.BLL
{
    public class ChecklistBLL
    {
        #region [ Variable Declarations ]
        PTResponse response;
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public ChecklistBLL()
        {
            dataAdapter = new DalAdapter();
            response = new PTResponse();
            this.rowsAffected = 0;
            this.isDuplicate = false;
        }
        #endregion [ Constructor ]

        #region [ GetAllChecklists ]
        public PTResponse GetAllChecklists(int siteID)
        {
            try
            {
                response.ChecklistItems = dataAdapter.GetAllCheckLists(siteID);
                if (response.ChecklistItems == null)
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

        #region [ Get Checklist By Checklist ID ]
        public PTResponse GetChecklistAndChecklistDetailsByUserID(int userID)
        {
            try
            {
                response.Checklist = dataAdapter.GetChecklistAndChecklistDetailsByUserID(userID);
                if (response.Checklist == null)
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

        #region [ Save Checklist ]
        public PTResponse SaveChecklist(PTRequest request)
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
                                dataAdapter.AddCheckLists(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Check list has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Check list already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Check list.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                //dataAdapter.ModifyCheckList(request, out isDuplicate, out rowsAffected);
                                dataAdapter.AddCheckLists(request, out isDuplicate, out rowsAffected);//For both add and update same function will be called but based on 
                                //Existance of data in checklist entry bor corresponding user the Add/Update 
                                //Operation will be performed

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Check list has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Check list already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Check list.";
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
        #endregion [ Save Checklist ]

        #region [ Delete Checklist ]
        public bool DeleteChecklistByChecklistID(int ChecklistID)
        {

            try
            {
                return dataAdapter.DeleteCheckListByCheckListID(ChecklistID);
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


