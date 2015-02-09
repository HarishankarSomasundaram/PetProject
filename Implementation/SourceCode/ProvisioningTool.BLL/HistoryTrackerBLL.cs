using ProvisioningTool.DAL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.BLL
{
    public class HistoryTrackerBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public HistoryTrackerBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ Public Methods ]


        #region [ GetHistoryTrackerByCustomerID ]
        public PTResponse GetHistoryTrackerDetails(PTRequest request)
        {
            try
            {
                response.HistoryTracker = dataAdapter.GetHistoryTrackerDetails(request);
                if (response.HistoryTracker == null)
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
        #endregion [GetHistoryTrackerByCustomerID]


        #endregion[ Public Methods ]


    }
}
