
using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;


namespace ProvisioningTool.DAL
{

    internal class InternetDomainDAL
    {
        #region [ Declarations ]
        private List<InternetDomain> InternetDomainList;
        DataSet dsInternetDomain;
        private InternetDomain internetDomain;

        private readonly string columnDomainID = "DomainID";
        private readonly string columnDomain = "Domain";
        private readonly string columnRegistrar = "Registrar";
        private readonly string columnAccountID = "AccountID";
        private readonly string columnDomainPassword = "DomainPassword";
        private readonly string columnExpiration = "Expiration";
        private readonly string columnAdminPanel = "AdminPanel";
        private readonly string columnDNSManaged = "DNSManaged";
        private readonly string columnServer = "Server";
        private readonly string columnPhone = "Phone";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnSiteID = "SiteID";

        #endregion [ Declarations ]

        internal InternetDomainDAL()
        {
        }
        #region [ Add InternetDomain ]
        internal InternetDomain AddInternetDomain(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            DateTime checkDate = new DateTime();
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Domain);

                parameters[1] = new SqlParameter("@Registrar", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Registrar);

                parameters[2] = new SqlParameter("@AccountID", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.AccountID);

                parameters[3] = new SqlParameter("@DomainPassword", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.DomainPassword);

                parameters[4] = new SqlParameter("@Expiration", SqlDbType.Date);

                //if (request.InternetDomain.Expiration.CompareTo(checkDate) != 0)
                //    parameters[4].Value = DBValueHelper.ConvertToDBDate(request.InternetDomain.Expiration);
                //else
                //    parameters[4].Value = DBNull.Value;

                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Expiration);

                parameters[5] = new SqlParameter("@AdminPanel", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.AdminPanel);

                parameters[6] = new SqlParameter("@DNSManaged", SqlDbType.Bit);
                parameters[6].Value = DBValueHelper.ConvertToDBBoolean(request.InternetDomain.DNSManaged);

                parameters[7] = new SqlParameter("@Server", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Server);

                parameters[8] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Phone);

                parameters[9] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[9].Value = DBValueHelper.ConvertToDBInteger(request.InternetDomain.StatusID);

                parameters[10] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(request.InternetDomain.CreatedBy);

                parameters[11] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.sessionSiteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetDomainAdd, parameters);
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
                return request.InternetDomain;

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
        #endregion [ Add InternetDomain ]



        #region [ Update InternetDomain ]
        internal InternetDomain ModifyInternetDomain(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@DomainID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.InternetDomain.DomainID);

                parameters[1] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Domain);

                parameters[2] = new SqlParameter("@Registrar", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Registrar);

                parameters[3] = new SqlParameter("@AccountID", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.AccountID);

                parameters[4] = new SqlParameter("@DomainPassword", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.DomainPassword);

                parameters[5] = new SqlParameter("@Expiration", SqlDbType.Date);
                parameters[5].Value = DBValueHelper.ConvertToDBDate(request.InternetDomain.Expiration);

                parameters[6] = new SqlParameter("@AdminPanel", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.AdminPanel);

                parameters[7] = new SqlParameter("@DNSManaged", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(request.InternetDomain.DNSManaged);

                parameters[8] = new SqlParameter("@Server", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Server);

                parameters[9] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.InternetDomain.Phone);

                parameters[10] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(request.InternetDomain.ModifiedBy);

                parameters[11] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(1);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetDomainUpdate, parameters);
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
                return request.InternetDomain;

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
        #endregion [ Update InternetDomain ]

        #region[ Delete InternetDomain ]
        //Delete/Update Status to 2 the InternetDomain from the DB based on the given parameters
        public bool DeleteInternetDomainByInternetDomainID(int internetDomainID)
        {
            SqlDataReader reader = null;
            dsInternetDomain = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@DomainID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(internetDomainID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteInternetDomainByInternetDomainID, parameters);
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
        #endregion[Delete InternetDomain]

        #region [Get All InternetDomains]
        public List<InternetDomain> GetAllInternetDomains(int siteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllInternetDomain);

            SqlDataReader reader = null;
            dsInternetDomain = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                dsInternetDomain = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetDomain_List, parameters);
                if (dsInternetDomain != null)
                {
                    InternetDomainList = ConvertAllInternetDomainAttributesToObjectList(dsInternetDomain);
                    if (InternetDomainList != null && InternetDomainList.Count > 0)
                    {
                        return InternetDomainList;
                    }
                    else
                        return null;
                }
                else
                    return null;
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
        #endregion [ GET ALL InternetDomain ]

        #region [Get InternetDomain And InternetDomain Attribute Details By InternetDomainID]

        public InternetDomain GetInternetDomainAndInternetDomainDetailsByInternetDomainID(int internetDomainID)
        {

            dsInternetDomain = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@InternetDomainID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(internetDomainID);
                dsInternetDomain = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetDomainByInternetDomainID, parameters);
                if (dsInternetDomain != null)
                {
                    InternetDomainList = ConvertAllInternetDomainAttributesToObjectList(dsInternetDomain);
                    if (InternetDomainList != null && InternetDomainList.Count > 0)
                    {
                        return InternetDomainList[0];
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        #endregion

        //All the InternetDomain Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<InternetDomain> ConvertAllInternetDomainAttributesToObjectList(DataSet ds)
        {
            InternetDomainList = new List<InternetDomain>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();


            DataTable InternetDomaindt = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    InternetDomaindt = ds.Tables[0];

                    //Convert InternetDomain Data table to its Corresponding List
                    if (InternetDomaindt.Rows.Count > 0)
                    {
                        InternetDomainList = (from DataRow internetDomain in InternetDomaindt.Rows
                                              select new InternetDomain
                                              {

                                                  DomainID = DataRowHelper.ConvertToInteger(internetDomain[columnDomainID]),
                                                  Domain = DataRowHelper.ConvertToString(internetDomain[columnDomain]),
                                                  Registrar = DataRowHelper.ConvertToString(internetDomain[columnRegistrar]),
                                                  AccountID = DataRowHelper.ConvertToString(internetDomain[columnAccountID]),
                                                  DomainPassword = DataRowHelper.ConvertToString(internetDomain[columnDomainPassword]),
                                                  Expiration = DataRowHelper.ConvertToString(internetDomain[columnExpiration],""),
                                                  AdminPanel = DataRowHelper.ConvertToString(internetDomain[columnAdminPanel]),
                                                  DNSManaged = DataRowHelper.ConvertToBoolean(internetDomain[columnDNSManaged]),
                                                  Server = DataRowHelper.ConvertToString(internetDomain[columnServer]),
                                                  Phone = DataRowHelper.ConvertToString(internetDomain[columnPhone]),
                                                  StatusID = DataRowHelper.ConvertToInteger(internetDomain[columnStatusID]),
                                                  CreatedBy = DataRowHelper.ConvertToInteger(internetDomain[columnCreatedBy]),
                                                  CreatedOn = DataRowHelper.ConvertToDateTime(internetDomain[columnCreatedOn]),
                                                  ModifiedBy = DataRowHelper.ConvertToInteger(internetDomain[columnModifiedBy]),
                                                  ModifiedOn = DataRowHelper.ConvertToDateTime(internetDomain[columnModifiedOn]),
                                                  View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=INTERNET/WEB-DOMAIN&id=" + DataRowHelper.ConvertToInteger(internetDomain[columnDomainID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                              }).ToList();
                    }
                }

                return InternetDomainList;

            }
            return null;
        }

    }
}
