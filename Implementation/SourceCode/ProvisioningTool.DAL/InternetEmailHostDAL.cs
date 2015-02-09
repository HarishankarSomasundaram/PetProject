
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

    internal class InternetEmailHostDAL
    {
        #region [ Declarations ]
        private List<InternetEmailHost> InternetEmailHostList;
        DataSet dsInternetEmailHost;
        private InternetEmailHost internetEmailHost;

        private readonly string columnEmailHostID = "EmailHostID";
        private readonly string columnEmailHosting = "EmailHosting";
        private readonly string columnProvider = "Provider";
        private readonly string columnAccountLogin = "AccountLogin";
        private readonly string columnEmailPassword = "EmailPassword";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnAdminPanel = "AdminPanel";
        private readonly string columnDNSManaged = "DNSManaged";
        private readonly string columnNameServers = "NameServers";
        private readonly string columnPhone = "Phone";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnSiteID = "SiteID";

        #endregion [ Declarations ]

        internal InternetEmailHostDAL()
        {
        }
        #region [ Add InternetEmailHost ]
        internal InternetEmailHost AddInternetEmailHost(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@EmailHosting", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.EmailHosting);

                parameters[1] = new SqlParameter("@Provider", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.Provider);

                parameters[2] = new SqlParameter("@AccountLogin", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.AccountLogin);

                parameters[3] = new SqlParameter("@EmailPassword", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.EmailPassword);

                parameters[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.IPAddress);

                parameters[5] = new SqlParameter("@AdminPanel", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.AdminPanel);

                parameters[6] = new SqlParameter("@DNSManaged", SqlDbType.Bit);
                parameters[6].Value = DBValueHelper.ConvertToDBBoolean(request.InternetEmailHost.DNSManaged);

                parameters[7] = new SqlParameter("@NameServers", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.NameServers);

                parameters[8] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.Phone);

                parameters[9] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[9].Value = DBValueHelper.ConvertToDBInteger(request.InternetEmailHost.StatusID);

                parameters[10] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(request.InternetEmailHost.CreatedBy);

                parameters[11] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.sessionSiteID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetEmailHostAdd, parameters);
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
                return request.InternetEmailHost;

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
        #endregion [ Add InternetEmailHost ]



        #region [ Update InternetEmailHost ]
        internal InternetEmailHost ModifyInternetEmailHost(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@EmailHostID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.InternetEmailHost.EmailHostID);

                parameters[1] = new SqlParameter("@EmailHosting", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.EmailHosting);

                parameters[2] = new SqlParameter("@Provider", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.Provider);

                parameters[3] = new SqlParameter("@AccountLogin", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.AccountLogin);

                parameters[4] = new SqlParameter("@EmailPassword", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.EmailPassword);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.IPAddress);

                parameters[6] = new SqlParameter("@AdminPanel", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.AdminPanel);

                parameters[7] = new SqlParameter("@DNSManaged", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(request.InternetEmailHost.DNSManaged);

                parameters[8] = new SqlParameter("@NameServers", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.NameServers);

                parameters[9] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.InternetEmailHost.Phone);

                parameters[10] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(request.InternetEmailHost.ModifiedBy);

                parameters[11] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(1);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetEmailHostUpdate, parameters);
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
                return request.InternetEmailHost;

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
        #endregion [ Update InternetEmailHost ]

        #region[ Delete InternetEmailHost ]
        //Delete/Update Status to 2 the InternetEmailHost from the DB based on the given parameters
        public bool DeleteInternetEmailHostByInternetEmailHostID(int internetEmailHostID)
        {
            SqlDataReader reader = null;
            dsInternetEmailHost = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@EmailHostID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(internetEmailHostID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteInternetEmailHostByInternetEmailHostID, parameters);
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
        #endregion[Delete InternetEmailHost]

        #region [Get All InternetEmailHosts]
        public List<InternetEmailHost> GetAllInternetEmailHosts(int siteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllInternetEmailHost);

            SqlDataReader reader = null;
            dsInternetEmailHost = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                dsInternetEmailHost = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetEmailHost_List, parameters);
                if (dsInternetEmailHost != null)
                {
                    InternetEmailHostList = ConvertAllInternetEmailHostAttributesToObjectList(dsInternetEmailHost);
                    if (InternetEmailHostList != null && InternetEmailHostList.Count > 0)
                    {
                        return InternetEmailHostList;
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
        #endregion [ GET ALL InternetEmailHost ]

        #region [Get InternetEmailHost And InternetEmailHost Attribute Details By InternetEmailHostID]

        public InternetEmailHost GetInternetEmailHostAndInternetEmailHostDetailsByInternetEmailHostID(int internetEmailHostID)
        {

            dsInternetEmailHost = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@InternetEmailHostID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(internetEmailHostID);
                dsInternetEmailHost = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetEmailHostByInternetEmailHostID, parameters);
                if (dsInternetEmailHost != null)
                {
                    InternetEmailHostList = ConvertAllInternetEmailHostAttributesToObjectList(dsInternetEmailHost);
                    if (InternetEmailHostList != null && InternetEmailHostList.Count > 0)
                    {
                        return InternetEmailHostList[0];
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

        //All the InternetEmailHost Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<InternetEmailHost> ConvertAllInternetEmailHostAttributesToObjectList(DataSet ds)
        {
            InternetEmailHostList = new List<InternetEmailHost>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();


            DataTable InternetEmailHostdt = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    InternetEmailHostdt = ds.Tables[0];

                    //Convert InternetEmailHost Data table to its Corresponding List
                    if (InternetEmailHostdt.Rows.Count > 0)
                    {
                        InternetEmailHostList = (from DataRow internetEmailHost in InternetEmailHostdt.Rows
                                                 select new InternetEmailHost
                                                 {

                                                     EmailHostID = DataRowHelper.ConvertToInteger(internetEmailHost[columnEmailHostID]),
                                                     EmailHosting = DataRowHelper.ConvertToString(internetEmailHost[columnEmailHosting]),
                                                     Provider = DataRowHelper.ConvertToString(internetEmailHost[columnProvider]),
                                                     AccountLogin = DataRowHelper.ConvertToString(internetEmailHost[columnAccountLogin]),
                                                     EmailPassword = DataRowHelper.ConvertToString(internetEmailHost[columnEmailPassword]),
                                                     IPAddress = DataRowHelper.ConvertToString(internetEmailHost[columnIPAddress]),
                                                     AdminPanel = DataRowHelper.ConvertToString(internetEmailHost[columnAdminPanel]),
                                                     DNSManaged = DataRowHelper.ConvertToBoolean(internetEmailHost[columnDNSManaged]),
                                                     NameServers = DataRowHelper.ConvertToString(internetEmailHost[columnNameServers]),
                                                     Phone = DataRowHelper.ConvertToString(internetEmailHost[columnPhone]),
                                                     StatusID = DataRowHelper.ConvertToInteger(internetEmailHost[columnStatusID]),
                                                     CreatedBy = DataRowHelper.ConvertToInteger(internetEmailHost[columnCreatedBy]),
                                                     CreatedOn = DataRowHelper.ConvertToDateTime(internetEmailHost[columnCreatedOn]),
                                                     ModifiedBy = DataRowHelper.ConvertToInteger(internetEmailHost[columnModifiedBy]),
                                                     ModifiedOn = DataRowHelper.ConvertToDateTime(internetEmailHost[columnModifiedOn]),
                                                     View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=INTERNET/WEB-EMAIL&id=" + DataRowHelper.ConvertToInteger(internetEmailHost[columnEmailHostID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                                 }).ToList();
                    }
                }

                return InternetEmailHostList;

            }
            return null;
        }

    }
}
