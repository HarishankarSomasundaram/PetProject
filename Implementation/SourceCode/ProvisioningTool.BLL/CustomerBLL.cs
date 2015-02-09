using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;
using ProvisioningTool.Entity;

namespace ProvisioningTool.BLL
{
    public class CustomerBLL
    {
        #region [ Variable Declarations ]
        PTResponse response;
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;

        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public CustomerBLL()
        {
            dataAdapter = new DalAdapter();
            response = new PTResponse();
            this.rowsAffected = 0;
            this.isDuplicate = false;
        }
        #endregion [ Constructor ]


        #region [ Save Customer ]
        public PTResponse SaveCustomer(PTRequest request)
        {
            //try
            //{
            if (request == null) throw new ArgumentNullException("Invalid Request Recieved or Request is null");
            //if (request.CurrentAction == ActionType.Add || request.CurrentAction == ActionType.Edit)
            //{
            try
            {
                if (CheckAttributes(request))
                {
                    if (request.CurrentAction == ActionType.Add)
                    {
                        dataAdapter.AddCustomer(request.Customer, out isDuplicate, out rowsAffected);
                    }
                    else if (request.CurrentAction == ActionType.Edit)
                    {
                        dataAdapter.ModifyCustomer(request.Customer, out isDuplicate, out rowsAffected);
                    }
                    if (!isDuplicate && rowsAffected == 1)
                    {
                        response.isSuccess = true;
                        response.isDuplicate = false;
                        response.Message = "Customer has been saved successfully.";
                    }
                    else if (isDuplicate && rowsAffected == 0)
                    {
                        response.isSuccess = false;
                        response.isDuplicate = true;
                        response.Message = "Customer code already exist(s).";
                    }
                    else if (!isDuplicate && rowsAffected == 0)
                    {
                        response.isSuccess = false;
                        response.isDuplicate = false;
                        response.Message = "Error while saving Customer.";
                    }

                    //}
                    //if (request.CurrentAction == ActionType.Edit)
                    //{
                    //    //dataAdapter.ModifyCustomer(request.Customer, out isDuplicate, out rowsAffected);

                    //    if (!isDuplicate && rowsAffected == 1)
                    //    {
                    //        response.isSuccess = true;
                    //        response.Message = "Customer has been updated successfully.";
                    //    }
                    //    else if (isDuplicate && rowsAffected == 0)
                    //    {
                    //        response.isSuccess = false;
                    //        response.Message = "Customer already exist(s).";
                    //    }
                    //    else if (!isDuplicate && rowsAffected == 0)
                    //    {
                    //        response.isSuccess = false;
                    //        response.Message = "Error while updating Customer...";
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // }
            return response;
            // }
            //finally { }
        }
        #endregion [ Save Customer ]

        public bool DeleteCustomerByCustomerID(int customerID)
        {

            try
            {
                return dataAdapter.DeleteCustomerByCustomerID(customerID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region [ Get All Customers ]
        public PTResponse GetAllCustomers()
        {
            try
            {
                response.CustomerList = dataAdapter.GetAllCustomers();
                if (response.CustomerList == null)
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

        #region [ Get All Sites To Customer ]
        public PTResponse GetAllSitesToCustomer()
        {
            try
            {
                response.CustomerList = dataAdapter.GetAllSitesToCustomer();
                if (response.CustomerList == null)
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

        #region [ GetAllCustomerByCustomerID ]
        public PTResponse GetAllCustomerByCustomerID(int customerID)
        {
            try
            {
                response.Customer = dataAdapter.GetAllCustomerByCustomerID(customerID);
                if (response.Customer == null)
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

        #region[ Get Customer By SearchKey]
        public PTResponse GetCustomerBySearchKey(Customer customer)
        {
            try
            {
                response.CustomerList = dataAdapter.GetCustomerBySearchKey(customer);
                if (response.CustomerList == null)
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
        #endregion[ Get Customer By SearchKey ]



        private bool CheckAttributes(PTRequest request)
        {
            return true;
        }
    }
}
