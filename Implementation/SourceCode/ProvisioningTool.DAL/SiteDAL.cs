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
    internal class SiteDAL
    {
        #region [ Declarations ]
        private List<Site> siteList;

        private readonly string columnSiteID = "SiteID";
        private readonly string columnCustomerID = "CustomerID";
        private readonly string columnCustomerName = "CustomerName";
        private readonly string columnCustomerCode = "CustomerCode";
        private readonly string columnSiteCode = "SiteCode";
        private readonly string columnSiteName = "SiteName";
        private readonly string columnAddress1 = "Address1";
        private readonly string columnAddress2 = "Address2";

        private readonly string columnCityID = "CityID";
        private readonly string columnCityName = "CityName";
        private readonly string columnStateID = "StateID";
        private readonly string columnStateName = "StateName";
        private readonly string columnCountryID = "CountryID";
        private readonly string columnCountryName = "CountryName";
        private readonly string columnZipCode = "ZipCode";
        private readonly string columnPhone = "Phone";

        private readonly string columnWebSite = "WebSite";

        private readonly string columnPrimaryContactID = "PrimaryContactID";
        private readonly string columnPrimaryContactName = "PrimaryContactName";
        private readonly string columnPrimaryContactPhone1 = "PrimaryContactPhone1";
        private readonly string columnPrimaryContactTitleID = "PrimaryContactTitleID";
        private readonly string columnPrimaryContactTitleName = "PrimaryContactTitleName";
        private readonly string columnPrimaryContactEmail = "PrimaryContactEmail";

        private readonly string columnAccountRepID = "AccountRepID";
        private readonly string columnAccountRepName = "AccountRepName";

        private readonly string columnPrimaryEngineerID = "PrimaryEngineerID";
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

        internal SiteDAL()
        {
        }

        #region [ Add Site ]
        internal Site AddSite(Site site, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {
                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[19];

                parameters[0] = new SqlParameter("@CustomerID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(site.Customer.CustomerID);

                parameters[1] = new SqlParameter("@SiteName", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(site.SiteName);

                parameters[2] = new SqlParameter("@Address1", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(site.Address1);

                parameters[3] = new SqlParameter("@Address2", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(site.Address2);

                parameters[4] = new SqlParameter("@CityID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(site.City.MasterDetailID);

                parameters[5] = new SqlParameter("@StateID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(site.State.MasterDetailID);

                parameters[6] = new SqlParameter("@CountryID", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(site.Country.MasterDetailID);

                parameters[7] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(site.ZipCode);

                parameters[8] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(site.PhoneNumber);

                parameters[9] = new SqlParameter("@Website", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(site.Website);

                parameters[10] = new SqlParameter("@AccountRepID", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(site.AccountRep.MasterDetailID);

                parameters[11] = new SqlParameter("@PrimaryEngineerID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(site.PrimaryEngineer.MasterDetailID);

                parameters[12] = new SqlParameter("@PrimaryContactID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(site.PrimaryContact.UserID);

                parameters[13] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(site.StatusID);

                parameters[14] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(site.CreatedBy);

                parameters[15] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(site.ModifiedBy);

                parameters[16] = new SqlParameter("@SiteCode", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(site.SiteCode);

                parameters[18] = new SqlParameter("@IsAutoTask", SqlDbType.Bit);
                parameters[18].Value = DBValueHelper.ConvertToDBBoolean(site.IsAutoTask);

                parameters[17] = new SqlParameter("@MappingID", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(site.MappingID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSiteAdd, parameters);
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
                return site;

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
        #endregion [ Add Site ]

        #region [ Modify Site ]
        internal Site ModifySite(Site site, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {
                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[16];

                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(site.SiteID);

                parameters[1] = new SqlParameter("@CustomerID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(site.Customer.CustomerID);

                parameters[2] = new SqlParameter("@SiteName", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(site.SiteName);

                parameters[3] = new SqlParameter("@Address1", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(site.Address1);

                parameters[4] = new SqlParameter("@Address2", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(site.Address2);

                parameters[5] = new SqlParameter("@CityID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(site.City.MasterDetailID);

                parameters[6] = new SqlParameter("@StateID", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(site.State.MasterDetailID);

                parameters[7] = new SqlParameter("@CountryID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(site.Country.MasterDetailID);

                parameters[8] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(site.ZipCode);

                parameters[9] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(site.PhoneNumber);

                parameters[10] = new SqlParameter("@Website", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(site.Website);

                parameters[11] = new SqlParameter("@AccountRepID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(site.AccountRep.MasterDetailID);

                parameters[12] = new SqlParameter("@PrimaryEngineerID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(site.PrimaryEngineer.MasterDetailID);

                parameters[13] = new SqlParameter("@PrimaryContactID", SqlDbType.Int);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(site.PrimaryContact.UserID);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(site.ModifiedBy);

                parameters[15] = new SqlParameter("@SiteCode", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(site.SiteCode);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSiteUpdateBySiteID, parameters);
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
                return site;

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
        #endregion [ Modify Site ]

        #region[ DeleteSiteBySiteID]
        //Returns the Invoices from the DB based on the given parameters        
        public bool DeleteSiteBySiteID(int siteID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@siteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(siteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteSiteBySiteID, parameters);
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
        #endregion[ DeleteSiteBySiteID ]

        #region[ GetAllSites ]
        //Returns the Invoices from the DB based on the given parameters        
        public List<Site> GetAllSites(int CustomerID, int searchFilter)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@CustomerID", SqlDbType.Int);
                parameters[0].Value =DBValueHelper.ConvertToDBInteger(CustomerID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(searchFilter);

                
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSite_List,parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return siteList;
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
        #endregion[ GetAllSites ]

        #region[ GetAllSiteBySiteID ]
        //Returns the Invoices from the DB based on the given parameters        
        public Site GetAllSiteBySiteID(int siteID)
        {
            SqlDataReader reader = null;

            siteList = new List<Site>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(siteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSitesBySiteID_List, parameters);
                if (reader != null)
                {
                    siteList = ProcessDataReader(reader);
                    if (siteList != null && siteList.Count > 0)
                    {
                        return siteList[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
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
        #endregion[ GetAllSiteBySiteID ]
        
        #region[ SearchSiteByKey ]
        //Returns the Invoices from the DB based on the given parameters        
        public List<Site> SearchSiteByKey(string key)
        {
            SqlDataReader reader = null;

            siteList = new List<Site>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@key", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(key);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSitesBySearchKey_List, parameters);
                if (reader != null)
                {
                    siteList = ProcessDataReader(reader);
                    if (siteList != null && siteList.Count > 0)
                    {
                        return siteList;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
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
        #endregion[ SearchSiteByKey ]

        #region[ GetAll Site By CustomerID ]
        //Returns the Invoices from the DB based on the given parameters        
        public List<Site> GetAllSiteByCustomerID(int CustomerID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@CustomerID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(CustomerID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSiteByCustomerID_Lists, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return siteList;
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
        #endregion[ GetAllSiteBySiteID ]

        #region [ private methods ]
        //Parses the data reader and converts to object
        private List<Site> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                siteList = new List<Site>();
                while (reader.Read())
                    siteList.Add(ConvertToObject(reader));
                return siteList;
            }
            return null;
        }
        //Converts each data record into object
        private Site ConvertToObject(IDataRecord dataRecord)
        {
            Site site = new Site();
            site.Customer = new Customer();
            site.City = new GlobalMasterDetail();
            site.State = new GlobalMasterDetail();
            site.Country = new GlobalMasterDetail();
            site.AccountRep = new GlobalMasterDetail();
            site.PrimaryEngineer = new GlobalMasterDetail();
            site.PrimaryContact = new User();

            site.SiteID = DataRowHelper.ConvertToInteger(dataRecord, columnSiteID);
            site.CustomerID = DataRowHelper.ConvertToInteger(dataRecord, columnCustomerID);
            site.Customer.CustomerID = DataRowHelper.ConvertToInteger(dataRecord, columnCustomerID);
            site.Customer.CustomerName = DataRowHelper.ConvertToString(dataRecord, columnCustomerName);
            site.CustomerCode = DataRowHelper.ConvertToString(dataRecord, columnCustomerCode);
            site.SiteCode = DataRowHelper.ConvertToString(dataRecord, columnSiteCode);
            site.SiteName = DataRowHelper.ConvertToString(dataRecord, columnSiteName);
            site.Address1 = DataRowHelper.ConvertToString(dataRecord, columnAddress1);
            site.Address2 = DataRowHelper.ConvertToString(dataRecord, columnAddress2);

            site.City.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnCityID);
            site.CityID = DataRowHelper.ConvertToInteger(dataRecord, columnCityID);
            site.CityName = DataRowHelper.ConvertToString(dataRecord, columnCityName);

            site.State.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnStateID);
            site.StateID = DataRowHelper.ConvertToInteger(dataRecord, columnStateID);
            site.StateName = DataRowHelper.ConvertToString(dataRecord, columnStateName);

            site.Country.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnCountryID);
            site.CountryID = DataRowHelper.ConvertToInteger(dataRecord, columnCountryID);
            site.CountryName = DataRowHelper.ConvertToString(dataRecord, columnCountryName);

            site.ZipCode = DataRowHelper.ConvertToString(dataRecord, columnZipCode);
            site.PhoneNumber = DataRowHelper.ConvertToString(dataRecord, columnPhone);
            site.Website = DataRowHelper.ConvertToString(dataRecord, columnWebSite);

            site.AccountRep.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnAccountRepID);
            site.AccountRepID = DataRowHelper.ConvertToInteger(dataRecord, columnAccountRepID);
            site.AccountRepName = DataRowHelper.ConvertToString(dataRecord, columnAccountRepName);

            site.PrimaryEngineer.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryEngineerID);
            site.PrimaryEngineerID = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryEngineerID);
            site.PrimaryEngineerName = DataRowHelper.ConvertToString(dataRecord, columnPrimaryEngineerName);

            site.PrimaryContact.UserID = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryContactID);
            site.PrimaryContactID = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryContactID);

            site.PrimaryContact.UserName = DataRowHelper.ConvertToString(dataRecord, columnPrimaryContactName);
            site.PrimaryContactName = DataRowHelper.ConvertToString(dataRecord, columnPrimaryContactName);

            site.PrimaryContact.Phone1 = DataRowHelper.ConvertToString(dataRecord, columnPrimaryContactPhone1);
            site.PrimaryContactPhone = DataRowHelper.ConvertToString(dataRecord, columnPrimaryContactPhone1);
            site.PrimaryContact.TitleID = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryContactTitleID);
            site.PrimaryContactTitle = DataRowHelper.ConvertToInteger(dataRecord, columnPrimaryContactTitleID);
            site.PrimaryContactTitleName = DataRowHelper.ConvertToString(dataRecord, columnPrimaryContactTitleName);
            site.PrimaryContact.Email = DataRowHelper.ConvertToString(dataRecord, columnPrimaryContactEmail);
            site.PrimaryContactEmail = DataRowHelper.ConvertToString(dataRecord, columnPrimaryContactEmail);

            site.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
            site.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
            site.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
            site.CreatedByName = DataRowHelper.ConvertToString(dataRecord, columnCreatedByName);
            site.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
            site.ModifiedByName = DataRowHelper.ConvertToString(dataRecord, columnModifiedByName);
            site.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);
            site.View = ConvertHelper.ConvertToString("<a href=CustomerSites.aspx?do=m&id=" + ConvertHelper.ConvertToString(site.SiteID) + " style='color: blue;text-decoration: underline;'>More</a>");

            return site;
        }
        #endregion [ private methods ]
    }
}
