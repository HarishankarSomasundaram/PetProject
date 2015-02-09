using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProvisioningTool.BLL
{
    public class ApplicationUserBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public ApplicationUserBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ Save ApplicationUser ]
        public PTResponse SaveApplicationUser(PTRequest request)
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
                                dataAdapter.AddApplicationUser(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "User has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "User already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving User.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyApplicationUser(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "User has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "User already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating User.";
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
        #endregion [ Save ApplicationUser ]

        #region [ Get ApplicationUser by Username ]
        public PTResponse GetApplicationUserByUserName(string username)
        {
            try
            {
                string SqlMessage = string.Empty;
                response.ApplicationUserList = dataAdapter.GetApplicationUserByUserName(username, out  SqlMessage);
                if (response.ApplicationUserList == null)
                {
                    response.isSuccess = false;
                    response.Message = SqlMessage;
                }
                else
                {
                    response.isSuccess = true;
                    response.Message = SqlMessage;
                }
                return response;
            }
            catch (SqlException sqlEx)
            {
                response.isSuccess = false;
                response.Message = sqlEx.Message;
                return response;
            }
        }
        #endregion

        #region [ Get ApplicationUser by Username ]
        public PTResponse GetApplicationUserByUserNameAndEmail(string username, string email)
        {
            try
            {
                string SqlMessage = string.Empty;
                response.ApplicationUserList = dataAdapter.GetApplicationUserByUserNameAndEmail(username, email, out  SqlMessage);
                if (response.ApplicationUserList == null)
                {
                    response.isSuccess = false;
                    response.Message = SqlMessage;
                }
                else
                {
                    response.isSuccess = true;
                    response.Message = SqlMessage;
                }
                return response;
            }
            catch (SqlException sqlEx)
            {
                response.isSuccess = false;
                response.Message = sqlEx.Message;
                return response;
            }
        }
        #endregion

        #region [ Search ApplicationUser by Key ]
        public PTResponse SearchApplicationUserByKey(string Key)
        {
            try
            {
                string SqlMessage = string.Empty;
                response.ApplicationUserList = dataAdapter.SearchApplicationUserByKey(Key, out  SqlMessage);
                if (response.ApplicationUserList == null)
                {
                    response.isSuccess = false;
                    response.Message = SqlMessage;
                }
                else
                {
                    response.isSuccess = true;
                    response.Message = SqlMessage;
                }
                return response;
            }
            catch (SqlException sqlEx)
            {
                response.isSuccess = false;
                response.Message = sqlEx.Message;
                return response;
            }
        }
        #endregion

        #region [ Get ApplicationUser By ApplicationUser ID ]
        public PTResponse GetApplicationUserByApplicationUserID(int applicationUserID)
        {
            try
            {
                response.ApplicationUser = dataAdapter.GetApplicationUserByApplicationUserID(applicationUserID);
                if (response.ApplicationUser == null)
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
        #endregion [ Get ApplicationUser By ApplicationUser ID ]

        #region [ GetAllApplicationUsers ]
        public PTResponse GetAllApplicationUsers()
        {
            try
            {
                response.ApplicationUserList = dataAdapter.GetAllApplicationUsers();
                if (response.SoftwareList == null)
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

        #region [Delete ApplicationUser]
        public bool DeleteApplicationUserByApplicationUserID(int applicationUserID)
        {

            try
            {
                return dataAdapter.DeleteApplicationUserByApplicationUserID(applicationUserID);
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
