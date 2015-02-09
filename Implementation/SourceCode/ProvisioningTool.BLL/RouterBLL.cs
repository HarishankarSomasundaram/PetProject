using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;
using ProvisioningTool.Entity;

namespace ProvisioningTool.BLL
{
    public class RouterBLL
    {
        #region [ Variable Declarations ]
        PTResponse response;
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public RouterBLL()
        {
            dataAdapter = new DalAdapter();
            response = new PTResponse();
            this.rowsAffected = 0;
            this.isDuplicate = false;
        }
        #endregion [ Constructor ]

        #region [GetAllRouters]
        public PTResponse GetAllRouters(int siteID)
        {
            try
            {
                response.RouterList = dataAdapter.GetAllRouters(siteID);
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
        #endregion[GetAllRouters]

        #region [ Save Router ]
        public PTResponse SaveRouter(PTRequest request)
        {
            try
            {
                if (request.CurrentAction == ActionType.Add)
                {
                    dataAdapter.AddRouter(request.Router, out isDuplicate, out rowsAffected);
                }
                else if (request.CurrentAction == ActionType.Edit)
                {
                    dataAdapter.ModifyRouter(request.Router, out isDuplicate, out rowsAffected);
                }
                if (!isDuplicate && rowsAffected == 1)
                {
                    response.isSuccess = true;
                    response.Message = "Router has been saved successfully.";
                }
                else if (!isDuplicate && rowsAffected == 0)
                {
                    response.isSuccess = false;
                    response.Message = "Router already exist(s).";
                }
                else if (isDuplicate && rowsAffected == 0)
                {
                    response.isSuccess = false;
                    response.Message = "Error while saving Router.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        #endregion [ Save Router]

        #region [ Delete Router By RouterID]
        public bool DeleteRouterByRouterID(int routerID)
        {
            return dataAdapter.DeleteRouterByRouterID(routerID);
        }
        #endregion [ Delete Router By RouterID]

        #region [ Get Router By RouterID ]
        public PTResponse GetRouterByRouterID(int routerID)
        {
            response = new PTResponse();
            response.Router = dataAdapter.GetRouterByRouterID(routerID);
            return response;
        }
        #endregion [ Get Router By RouterID ]
    }
}
