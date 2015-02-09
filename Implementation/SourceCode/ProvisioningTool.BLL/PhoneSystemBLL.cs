using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;
using ProvisioningTool.Entity;

namespace ProvisioningTool.BLL
{
    public class PhoneSystemBLL
    {
        #region [ Variable Declarations ]
        PTResponse response;
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public PhoneSystemBLL()
        {
            dataAdapter = new DalAdapter();
            response = new PTResponse();
            this.rowsAffected = 0;
            this.isDuplicate = false;
        }
        #endregion [ Constructor ]

        #region [GetAllPhoneSystems]
        public PTResponse GetAllPhoneSystems(int siteID, string searchFilter)
        {
            try
            {
                response.PhoneSystemList = dataAdapter.GetAllPhoneSystems(siteID, searchFilter);
                if (response.RouterList == null)
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
        #endregion[GetAllPhoneSystems]

        #region [ Save PhoneSystem ]
        public PTResponse SavePhoneSystem(PTRequest request)
        {
            try
            {
                if (request.CurrentAction == ActionType.Add)
                {
                    dataAdapter.AddPhoneSystem(request.PhoneSystem, out isDuplicate, out rowsAffected);
                }
                else if (request.CurrentAction == ActionType.Edit)
                {
                    dataAdapter.ModifyPhoneSystem(request.PhoneSystem, out isDuplicate, out rowsAffected);
                }
                if (!isDuplicate && rowsAffected == 1)
                {
                    response.isSuccess = true;
                    response.Message = "Phone System has been saved successfully.";
                }
                else if (!isDuplicate && rowsAffected == 0)
                {
                    response.isSuccess = false;
                    response.Message = "Phone System already exist(s).";
                }
                else if (isDuplicate && rowsAffected == 0)
                {
                    response.isSuccess = false;
                    response.Message = "Error while saving Phone System.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        #endregion [ Save PhoneSystem]

        #region [ Delete PhoneSystem By phoneSystemID]
        public bool DeletePhoneSystemByPhoneSystemID(int phoneSystemID)
        {
            return dataAdapter.DeletePhoneSystemByPhoneSystemID(phoneSystemID);
        }
        #endregion [ Delete PhoneSystem By PhoneSystemID]

        #region [ Get PhoneSystem By PhoneSystemID ]
        public PTResponse GetPhoneSystemByPhoneSystemID(int phoneSystemID)
        {
            response = new PTResponse();
            response.PhoneSystem = dataAdapter.GetPhoneSystemByPhoneSystemID(phoneSystemID);
            return response;
        }
        #endregion [ Get PhoneSystem By PhoneSystemID ]
    }
}
