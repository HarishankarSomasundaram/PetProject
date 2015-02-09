using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvisioningTool.BLL
{
    public class GroupPolicyBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public GroupPolicyBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllGroupPolicySetup ]
        public PTResponse GetAllGroupPolicySetup(int siteID)
        {
            try
            {
                response.GroupPolicySetupList = dataAdapter.GetAllGroupPolicySetup(siteID);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion [ GetAllGroupPolicySetup ]

        #region [ GetAllGroupPolicy ]
        public PTResponse GetAllGroupPolicy()
        {
            try
            {
                response.GroupPolicyList = dataAdapter.GetAllGroupPolicy();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion [ GetAllGroupPolicy ]

        #region [ GetAllFieldTypeMaster ]
        public PTResponse GetAllFieldTypeMaster()
        {
            try
            {
                response.FieldTypeMasterList = dataAdapter.GetAllFieldTypeMaster();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion [ GetAllFieldTypeMaster ]

        #region [ GetAllHeadingMaster ]
        public PTResponse GetAllHeadingMaster()
        {
            try
            {
                response.HeadingMasterList = dataAdapter.GetAllHeadingMaster();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion [ GetAllHeadingMaster ]

        #region [ Get Group Policy Setup by Group Policy Setup ID ]
        public PTResponse GetGroupPolicySetupByGroupPolicySetupID(int GroupPolicySetupID)
        {
            try
            {
                response.GroupPolicySetup = dataAdapter.GetGroupPolicySetupByGroupPolicySetupID(GroupPolicySetupID);
                if (response.GroupPolicySetup == null)
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
        #endregion [ Get Group Policy Setup by Group Policy Setup ID ]

        #region [ Save Group Policy ]
        public PTResponse SaveGroupPolicy(PTRequest request)
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
                                dataAdapter.AddGroupPolicy(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Group Policy has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Group Policy already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Group Policy...";
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
        #endregion [ Save Group Policy ]

        #region [ Save Group Policy Setup ]
        public PTResponse SaveGroupPolicySetup(PTRequest request)
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
                                dataAdapter.AddGroupPolicySetup(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Group Policy Setup has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Group Policy Setup already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Group Policy Setup.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyGroupPolicySetup(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Group Policy Setup has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Group Policy Setup already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Group Policy Setup.";
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
        #endregion [ Save Group Policy Setup ]

        #region [Delete  Group Policy  ]
        public bool DeleteGroupPolicyByGroupPolicyID(int GroupPolicyID)
        {

            try
            {
                return dataAdapter.DeleteGroupPolicyByGroupPolicyID(GroupPolicyID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion  [Delete Group Policy ]

        #region [Delete Group Policy ]
        public bool DeleteGroupPolicy()
        {

            try
            {
                return dataAdapter.DeleteGroupPolicy();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion  [Delete Group Policy ]

        #region [ Private Function ]
        private bool CheckAttributes(PTRequest request)
        {
            return true;
        }
        #endregion [ Private Function ]
    }
}
