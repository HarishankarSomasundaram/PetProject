
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

    internal class InternetWebHostDAL
    {
        #region [ Declarations ]
        private List<InternetWebHost> InternetWebHostList;
        DataSet dsInternetWebHost;
        private InternetWebHost internetWebHost;

        private readonly string columnWebHostID = "WebHostID";
        private readonly string columnWebHost = "WebHost";
        private readonly string columnProvider = "Provider";
        private readonly string columnAccountID = "AccountID";
        private readonly string columnWebHostPassword = "WebHostPassword";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnAdminPanel = "AdminPanel";
        private readonly string columnDNSManaged = "DNSManaged";
        private readonly string columnNameServer = "NameServer";
        private readonly string columnPhone = "Phone";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnSiteID = "SiteID";

        #endregion [ Declarations ]

        internal InternetWebHostDAL()
        {
        }
        #region [ Add InternetWebHost ]
        internal InternetWebHost AddInternetWebHost(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@WebHost", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.WebHost);

                parameters[1] = new SqlParameter("@Provider", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.Provider);

                parameters[2] = new SqlParameter("@AccountID", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.AccountID);

                parameters[3] = new SqlParameter("@WebHostPassword", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.WebHostPassword);

                parameters[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.IPAddress);

                parameters[5] = new SqlParameter("@AdminPanel", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.AdminPanel);

                parameters[6] = new SqlParameter("@DNSManaged", SqlDbType.Bit);
                parameters[6].Value = DBValueHelper.ConvertToDBBoolean(request.InternetWebHost.DNSManaged);

                parameters[7] = new SqlParameter("@NameServer", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.NameServer);

                parameters[8] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.Phone);

                parameters[9] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[9].Value = DBValueHelper.ConvertToDBInteger(request.InternetWebHost.StatusID);

                parameters[10] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(request.InternetWebHost.CreatedBy);

                parameters[11] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.sessionSiteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetWebHostAdd, parameters);
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
                return request.InternetWebHost;

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
        #endregion [ Add InternetWebHost ]



        #region [ Update InternetWebHost ]
        internal InternetWebHost ModifyInternetWebHost(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[13];

                parameters[0] = new SqlParameter("@WebHostID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.InternetWebHost.WebHostID);

                parameters[1] = new SqlParameter("@WebHost", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.WebHost);

                parameters[2] = new SqlParameter("@Provider", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.Provider);

                parameters[3] = new SqlParameter("@AccountID", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.AccountID);

                parameters[4] = new SqlParameter("@WebHostPassword", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.WebHostPassword);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.IPAddress);

                parameters[6] = new SqlParameter("@AdminPanel", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.AdminPanel);

                parameters[7] = new SqlParameter("@DNSManaged", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(request.InternetWebHost.DNSManaged);

                parameters[8] = new SqlParameter("@NameServer", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.NameServer);

                parameters[9] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.InternetWebHost.Phone);

                parameters[11] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.InternetWebHost.ModifiedBy);

                parameters[12] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(1);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetWebHostUpdate, parameters);
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
                return request.InternetWebHost;

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
        #endregion [ Update InternetWebHost ]

        #region[ Delete InternetWebHost ]
        //Delete/Update Status to 2 the InternetWebHost from the DB based on the given parameters
        public bool DeleteInternetWebHostByInternetWebHostID(int internetWebHostID)
        {
            SqlDataReader reader = null;
            dsInternetWebHost = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@WebHostID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(internetWebHostID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteInternetWebHostByInternetWebHostID, parameters);
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
        #endregion[Delete InternetWebHost]

        #region [Get All InternetWebHosts]
        public List<InternetWebHost> GetAllInternetWebHosts(int siteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllInternetWebHost);

            SqlDataReader reader = null;
            dsInternetWebHost = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                dsInternetWebHost = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetWebHost_List, parameters);
                if (dsInternetWebHost != null)
                {
                    InternetWebHostList = ConvertAllInternetWebHostAttributesToObjectList(dsInternetWebHost);
                    if (InternetWebHostList != null && InternetWebHostList.Count > 0)
                    {
                        return InternetWebHostList;
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
        #endregion [ GET ALL InternetWebHost ]

        #region [Get InternetWebHost And InternetWebHost Attribute Details By InternetWebHostID]

        public InternetWebHost GetInternetWebHostAndInternetWebHostDetailsByInternetWebHostID(int internetWebHostID)
        {

            dsInternetWebHost = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@InternetWebHostID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(internetWebHostID);
                dsInternetWebHost = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetWebHostByInternetWebHostID, parameters);
                if (dsInternetWebHost != null)
                {
                    InternetWebHostList = ConvertAllInternetWebHostAttributesToObjectList(dsInternetWebHost);
                    if (InternetWebHostList != null && InternetWebHostList.Count > 0)
                    {
                        return InternetWebHostList[0];
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

        //All the InternetWebHost Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<InternetWebHost> ConvertAllInternetWebHostAttributesToObjectList(DataSet ds)
        {
            InternetWebHostList = new List<InternetWebHost>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();


            DataTable InternetWebHostdt = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    InternetWebHostdt = ds.Tables[0];

                    //Convert InternetWebHost Data table to its Corresponding List
                    if (InternetWebHostdt.Rows.Count > 0)
                    {
                        InternetWebHostList = (from DataRow internetWebHost in InternetWebHostdt.Rows
                                               select new InternetWebHost
                                               {

                                                   WebHostID = DataRowHelper.ConvertToInteger(internetWebHost[columnWebHostID]),
                                                   WebHost = DataRowHelper.ConvertToString(internetWebHost[columnWebHost]),
                                                   Provider = DataRowHelper.ConvertToString(internetWebHost[columnProvider]),
                                                   AccountID = DataRowHelper.ConvertToString(internetWebHost[columnAccountID]),
                                                   WebHostPassword = DataRowHelper.ConvertToString(internetWebHost[columnWebHostPassword]),
                                                   IPAddress = DataRowHelper.ConvertToString(internetWebHost[columnIPAddress]),
                                                   AdminPanel = DataRowHelper.ConvertToString(internetWebHost[columnAdminPanel]),
                                                   DNSManaged = DataRowHelper.ConvertToBoolean(internetWebHost[columnDNSManaged]),
                                                   NameServer = DataRowHelper.ConvertToString(internetWebHost[columnNameServer]),
                                                   Phone = DataRowHelper.ConvertToString(internetWebHost[columnPhone]),
                                                   StatusID = DataRowHelper.ConvertToInteger(internetWebHost[columnStatusID]),
                                                   CreatedBy = DataRowHelper.ConvertToInteger(internetWebHost[columnCreatedBy]),
                                                   CreatedOn = DataRowHelper.ConvertToDateTime(internetWebHost[columnCreatedOn]),
                                                   ModifiedBy = DataRowHelper.ConvertToInteger(internetWebHost[columnModifiedBy]),
                                                   ModifiedOn = DataRowHelper.ConvertToDateTime(internetWebHost[columnModifiedOn]),
                                                   View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=INTERNET/WEB-WEBHOST&id=" + DataRowHelper.ConvertToInteger(internetWebHost[columnWebHostID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                               }).ToList();
                    }
                }

                return InternetWebHostList;

            }
            return null;
        }

    }
}
