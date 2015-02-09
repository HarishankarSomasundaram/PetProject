using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.BLL
{
    public class ServerInfoBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public ServerInfoBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllServerInfos ]
        public PTResponse GetAllServerInfo(int siteID, string searchFilter)
        {
            try
            {
                response.ServerInfoList = dataAdapter.GetAllServerInfo(siteID, searchFilter);
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

        #region [ Get ServerInfo By ServerInfo ID ]
        public PTResponse GetServerHardwarAndUserDetailsByServerHardwarID(int ServerInfoID)
        {
            try
            {
                response.ServerInfo = dataAdapter.GetServerInfoAndUserDetailsByServerInfoID(ServerInfoID);
                if (response.ServerInfo == null)
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

        #region [ Save ServerInfo ]
        public PTResponse SaveServerInfo(PTRequest request)
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
                                dataAdapter.AddServerInfo(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Server Info has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Server Info already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving ServerInfo.";
                                }
                            }
                            else if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyServerInfo(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Server Info has been updated successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Server Info already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating ServerInfo.";
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
        #endregion [ Save ServerInfo ]

        #region [Delete ServerInfo]
        public bool DeleteServerInfoByServerInfoID(int ServerInfoID)
        {

            try
            {
                return dataAdapter.DeleteServerInfoByServerInfoID(ServerInfoID);
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
