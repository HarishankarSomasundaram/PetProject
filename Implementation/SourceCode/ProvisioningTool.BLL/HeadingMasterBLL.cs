using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;


namespace ProvisioningTool.BLL
{
   public class HeadingMasterBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public HeadingMasterBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllHeadingMasters ]
        public PTResponse GetAllHeadingMasters()
        {
            try
            {
                response.HeadingMasterList = dataAdapter.GetAllHeadingMasters();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion [ GetAllHeadingMasters ]

        #region [ Heading Master by Heading Master ID ]
        public PTResponse GetHeadingMasterByHeadingMasterID(int HeadingMasterID)
        {
            try
            {
                response.HeadingMaster = dataAdapter.GetHeadingMasterByHeadingMasterID(HeadingMasterID);
                if (response.HeadingMaster == null)
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
        #endregion [ Heading Master by Heading Master ID ]

        #region [ Save Heading Master ]
        public PTResponse SaveHeadingMaster(PTRequest request)
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
                                dataAdapter.AddHeadingMaster(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Heading Master has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Heading Master already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Heading Master...";
                                }
                            }
                            else
                            {
                                dataAdapter.ModifyHeadingMaster(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Heading Master has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Heading Master already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Heading Master...";
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
        #endregion [ Save Heading Master ]

        #region [Delete  Heading Master  ]
        public bool DeleteHeadingMasterByHeadingMasterID(int HeadingMasterID)
        {

            try
            {
                return dataAdapter.DeleteHeadingMasterByHeadingMasterID(HeadingMasterID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion  [Delete Heading Master ]

        #region [ Private Function ]
        private bool CheckAttributes(PTRequest request)
        {
            return true;
        }
        #endregion [ Private Function ]
    }
}
