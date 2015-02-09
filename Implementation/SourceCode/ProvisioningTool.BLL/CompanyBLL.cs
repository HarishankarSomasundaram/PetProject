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
    public class CompanyBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public CompanyBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllCompanies ]
        public List<Company> GetAllCompanies()
        {
            try
            {
                return dataAdapter.GetAllCompanies();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
