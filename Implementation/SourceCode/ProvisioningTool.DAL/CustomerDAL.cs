using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library;
using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Common;
using ProvisioningTool.Entity;


namespace ProvisioningTool.DAL
{
    internal class CustomersDAL
    {
        #region [ Declarations ]
        private List<Customer> CustomersList;

        private readonly string columnCustomerID = "CustomerID";
        private readonly string columnCompanyID = "CompanyID";
        private readonly string columnCompanyName = "CompanyName";
        private readonly string columnCustomerCode = "CustomerCode";
        private readonly string columnCustomerName = "CustomerName";
        private readonly string columnAddress = "Address";
        private readonly string columnCityID = "CityID";
        private readonly string columnCityName = "CityName";
        private readonly string columnStateID = "StateID";
        private readonly string columnStateName = "StateName";
        private readonly string columnCountryID = "CountryID";
        private readonly string columnCountryName = "CountryName";
        private readonly string columnZipCode = "ZipCode";
        private readonly string columnPhoneNumber = "PhoneNumber";
        private readonly string columnAlternatePhoneNo = "AlternatePhoneNo";
        private readonly string columnFax = "Fax";
        private readonly string columnWebAddress = "WebAddress";
        private readonly string columnAccountRep = "AccountRepID";
        private readonly string columnAccountRepName = "AccountRepName";
        private readonly string columnPrimaryEngineer = "PrimaryEngineerID";
        private readonly string columnPrimaryEngineerName = "PrimaryEngineerName";
        private readonly string columnNotes = "Notes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnCreatedByName = "CreatedByName";
        private readonly string columnModifiedByName = "ModifiedByName";
        #endregion [ Declarations ]

        internal CustomersDAL()
        {
        }

        #region [ Add Customers ]
        internal Customer AddCustomer(Customer customer, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {
                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[20];

                parameters[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(customer.Company.CompanyID);

                parameters[1] = new SqlParameter("@CustomerCode", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(customer.CustomerCode);

                parameters[2] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(customer.CustomerName);

                parameters[3] = new SqlParameter("@Address", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(customer.Address);

                parameters[4] = new SqlParameter("@CityID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(customer.City.MasterDetailID);

                parameters[5] = new SqlParameter("@StateID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(customer.State.MasterDetailID);

                parameters[6] = new SqlParameter("@CountryID", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(customer.Country.MasterDetailID);

                parameters[7] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(customer.ZipCode);

                parameters[8] = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(customer.PhoneNumber);

                parameters[9] = new SqlParameter("@AlternatePhoneNo", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(customer.AlternatePhoneNo);

                parameters[10] = new SqlParameter("@Fax", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(customer.Fax);

                parameters[11] = new SqlParameter("@WebAddress", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(customer.EmailAddress);

                parameters[12] = new SqlParameter("@AccountRep", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(customer.AccountRep.MasterDetailID);

                parameters[13] = new SqlParameter("@PrimaryEngineer", SqlDbType.Int);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(customer.PrimaryEngineer.MasterDetailID);

                parameters[14] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(customer.Notes);

                parameters[15] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(customer.StatusID);

                parameters[16] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[16].Value = DBValueHelper.ConvertToDBInteger(customer.CreatedBy);

                parameters[17] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[17].Value = DBValueHelper.ConvertToDBInteger(customer.ModifiedBy);

                parameters[18] = new SqlParameter("@IsAutoTask", SqlDbType.Bit);
                parameters[18].Value = DBValueHelper.ConvertToDBBoolean(customer.IsAutoTask);

                parameters[19] = new SqlParameter("@MappingID", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(customer.MappingID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCustomerAdd, parameters);
                if (reader != null)
                {
                    reader.Read();
                    rowsAffected = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameRowsAffected);
                    isDuplicate = DataRowHelper.ConvertToBoolean(reader, DalHelper.columnNameIsDuplicate);
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                return customer;

            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion [ Add Customers ]

        #region [ Modify Customer]
        internal Customer ModifyCustomer(Customer customer, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {
                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[17];

                parameters[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(customer.Company.CompanyID);

                parameters[1] = new SqlParameter("@CustomerCode", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(customer.CustomerCode);

                parameters[2] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(customer.CustomerName);

                parameters[3] = new SqlParameter("@Address", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(customer.Address);

                parameters[4] = new SqlParameter("@CityID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(customer.City.MasterDetailID);

                parameters[5] = new SqlParameter("@StateID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(customer.State.MasterDetailID);

                parameters[6] = new SqlParameter("@CountryID", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(customer.Country.MasterDetailID);

                parameters[7] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(customer.ZipCode);

                parameters[8] = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(customer.PhoneNumber);

                parameters[9] = new SqlParameter("@AlternatePhoneNo", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(customer.AlternatePhoneNo);

                parameters[10] = new SqlParameter("@Fax", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(customer.Fax);

                parameters[11] = new SqlParameter("@WebAddress", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(customer.EmailAddress);

                parameters[12] = new SqlParameter("@AccountRep", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(customer.AccountRep.MasterDetailID);

                parameters[13] = new SqlParameter("@PrimaryEngineer", SqlDbType.Int);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(customer.PrimaryEngineer.MasterDetailID);

                parameters[14] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(customer.Notes);

                parameters[15] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(customer.ModifiedBy);

                parameters[16] = new SqlParameter("@CustomerID", SqlDbType.Int);
                parameters[16].Value = DBValueHelper.ConvertToDBInteger(customer.CustomerID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCustomerUpdate, parameters);
                if (reader != null)
                {
                    reader.Read();
                    rowsAffected = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameRowsAffected);
                    isDuplicate = DataRowHelper.ConvertToBoolean(reader, DalHelper.columnNameIsDuplicate);
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                return customer;

            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion [ Modify Customer ]

        #region[ DeleteCustomerByCustomerID]
        //Returns the Invoices from the DB based on the given parameters        
        public bool DeleteCustomerByCustomerID(int customerID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@CustomerID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(customerID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCustomerDeleteByCustomerID, parameters);
                if (reader != null)
                {
                    reader.Read();
                    return DataRowHelper.ConvertToBoolean(reader, DalHelper.columnNameIsDeleted);
                }
                return false;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion[ DeleteCustomerByCustomerID ]

        #region[ Get All Customers ]
        //Returns the Invoices from the DB based on the given parameters        
        public List<Customer> GetAllCustomers()
        {
            SqlDataReader reader = null;
            try
            {
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCustomers_List);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return CustomersList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion[ Get All Customers ]

        #region[ Get All Sites To Customers ]
        //Returns the Invoices from the DB based on the given parameters        
        public List<Customer> GetAllSitesToCustomer()
        {
            SqlDataReader reader = null;
            try
            {
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSitesToCustomer_List);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return CustomersList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion[ Get All Sites To Customers ]

        #region[ GetAllCustomerByCustomerID]
        //Returns the Invoices from the DB based on the given parameters        
        public List<Customer> GetAllCustomerByCustomerID(int customerID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@CustomerID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(customerID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCustomerByCustomerID_List, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return CustomersList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion[ GetAllCustomerByCustomerID ]

        #region[ Get Customer By SearchKey]
        //Returns the Customers from the DB based on the given search Key        
        public List<Customer> GetCustomerBySearchKey(Customer customer)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@CustomerCode", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(customer.CustomerCode);

                parameters[1] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(customer.CustomerName);

                parameters[2] = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(customer.Company.CompanyName);

                parameters[3] = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(customer.PhoneNumber);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSearchCustomersBySearchKey_List, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader, customer.View);
                }
                return CustomersList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion[ Get Customer By SearchKey ]

        #region [ private methods ]

        //Parses the data reader and converts to object
        private List<Customer> ProcessDataReader(SqlDataReader reader)
        {
            return ProcessDataReader(reader, "");
        }
        //Parses the data reader and converts to object
        private List<Customer> ProcessDataReader(SqlDataReader reader, string viewLink)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                CustomersList = new List<Customer>();
                while (reader.Read())
                    CustomersList.Add(ConvertToObject(reader, viewLink));
                return CustomersList;
            }
            return null;
        }
        //Converts each data record into object
        private Customer ConvertToObject(IDataRecord dataRecord)
        {
            return ConvertToObject(dataRecord, "");
        }
        //Converts each data record into object
        private Customer ConvertToObject(IDataRecord dataRecord, string viewLink)
        {
            Customer customer = new Customer();

            customer.Company = new Company();
            customer.City = new GlobalMasterDetail();
            customer.State = new GlobalMasterDetail();
            customer.Country = new GlobalMasterDetail();
            customer.AccountRep = new GlobalMasterDetail();
            customer.PrimaryEngineer = new GlobalMasterDetail();

            customer.CustomerID = DataRowHelper.ConvertToInteger(dataRecord, columnCustomerID);

            customer.Company.CompanyID = DataRowHelper.ConvertToInteger(dataRecord, columnCompanyID);
            customer.CompanyName = DataRowHelper.ConvertToString(dataRecord, columnCompanyName);
            customer.CompanyID = DataRowHelper.ConvertToInteger(dataRecord, columnCompanyID);


            customer.CustomerCode = DataRowHelper.ConvertToString(dataRecord, columnCustomerCode);
            customer.CustomerName = DataRowHelper.ConvertToString(dataRecord, columnCustomerName);
            customer.Address = DataRowHelper.ConvertToString(dataRecord, columnAddress);

            customer.City.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnCityID);
            customer.CityID = DataRowHelper.ConvertToInteger(dataRecord, columnCityID);
            customer.CityName = DataRowHelper.ConvertToString(dataRecord, columnCityName);

            customer.State.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnStateID);
            customer.StateID = DataRowHelper.ConvertToInteger(dataRecord, columnStateID);
            customer.StateName = DataRowHelper.ConvertToString(dataRecord, columnStateName);

            customer.Country.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnCountryID);
            customer.CountryID = DataRowHelper.ConvertToInteger(dataRecord, columnCountryID);
            customer.CountryName = DataRowHelper.ConvertToString(dataRecord, columnCountryName);

            customer.ZipCode = DataRowHelper.ConvertToString(dataRecord, columnZipCode);
            customer.PhoneNumber = DataRowHelper.ConvertToString(dataRecord, columnPhoneNumber);
            customer.AlternatePhoneNo = DataRowHelper.ConvertToString(dataRecord, columnAlternatePhoneNo);
            customer.Fax = DataRowHelper.ConvertToString(dataRecord, columnFax);
            customer.EmailAddress = DataRowHelper.ConvertToString(dataRecord, columnWebAddress);

            customer.AccountRep.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnAccountRep);
            customer.AccountRepID = DataRowHelper.ConvertToInteger(dataRecord, columnAccountRep);
            customer.AccountRepName = DataRowHelper.ConvertToString(dataRecord, columnAccountRepName);

            customer.PrimaryEngineer.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryEngineer);
            customer.PrimaryEngineerID = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryEngineer);
            customer.PrimaryEngineerName = DataRowHelper.ConvertToString(dataRecord, columnPrimaryEngineerName);

            customer.Notes = DataRowHelper.ConvertToString(dataRecord, columnNotes);
            customer.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
            customer.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
            customer.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
            customer.CreatedByName = DataRowHelper.ConvertToString(dataRecord, columnCreatedByName);
            customer.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
            customer.ModifiedByName = DataRowHelper.ConvertToString(dataRecord, columnModifiedByName);
            customer.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);
            if (viewLink.ToUpper() == "ADMIN")
                customer.View = ConvertHelper.ConvertToString("<a href='CustomerSites.aspx?CID=" + ConvertHelper.ConvertToString(customer.CustomerID) + "' id='customerId' class='view'>Select</a>");
            else
                customer.View = ConvertHelper.ConvertToString("<a href='CustomerInfo.aspx?do=v&nav=Users&search=1&CID=" + ConvertHelper.ConvertToString(customer.CustomerID) + "' id='customerId' class='view'>View</a>");

            return customer;
        }
        #endregion [ private methods ]

    }
}
