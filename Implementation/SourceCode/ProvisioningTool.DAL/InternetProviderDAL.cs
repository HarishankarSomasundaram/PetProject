
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

    internal class InternetProviderDAL
    {
        #region [ Declarations ]
        private List<InternetProvider> InternetProviderList;
        DataSet dsInternetProvider;
        private InternetProvider internetProvider;

        private readonly string columnProviderID = "ProviderID";
        private readonly string columnProvider = "Provider";
        private readonly string columnCircutID = "CircutID";
        private readonly string columnAccountNumber = "AccountNumber";
        private readonly string columnProviderType = "ProviderType";
        private readonly string columnBrandWidth = "BrandWidth";
        private readonly string columnNetworkID = "NetworkID";
        private readonly string columnStaticIPAddress = "StaticIPAddress";
        private readonly string columnSubnet = "Subnet";
        private readonly string columnGateway = "Gateway";
        private readonly string columnPhone = "Phone";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnSiteID = "SiteID";

        #endregion [ Declarations ]

        internal InternetProviderDAL()
        {
        }
        #region [ Add InternetProvider ]
        internal InternetProvider AddInternetProvider(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[13];

                parameters[0] = new SqlParameter("@Provider", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Provider);

                parameters[1] = new SqlParameter("@CircutID", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.CircutID);

                parameters[2] = new SqlParameter("@AccountNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.AccountNumber);

                parameters[3] = new SqlParameter("@ProviderType", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.ProviderType);

                parameters[4] = new SqlParameter("@BrandWidth", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.BrandWidth);

                parameters[5] = new SqlParameter("@NetworkID", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.NetworkID);

                parameters[6] = new SqlParameter("@StaticIPAddress", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.StaticIPAddress);

                parameters[7] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Subnet);

                parameters[8] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Gateway);

                parameters[9] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Phone);

                parameters[10] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(request.InternetProvider.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.InternetProvider.CreatedBy);

                parameters[12] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.sessionSiteID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetProviderAdd, parameters);
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
                return request.InternetProvider;

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
        #endregion [ Add InternetProvider ]



        #region [ Update InternetProvider ]
        internal InternetProvider ModifyInternetProvider(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[13];

                parameters[0] = new SqlParameter("@ProviderID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.InternetProvider.ProviderID);

                parameters[1] = new SqlParameter("@Provider", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Provider);

                parameters[2] = new SqlParameter("@CircutID", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.CircutID);

                parameters[3] = new SqlParameter("@AccountNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.AccountNumber);

                parameters[4] = new SqlParameter("@ProviderType", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.ProviderType);

                parameters[5] = new SqlParameter("@BrandWidth", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.BrandWidth);

                parameters[6] = new SqlParameter("@NetworkID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.NetworkID);

                parameters[7] = new SqlParameter("@StaticIPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.StaticIPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Gateway);

                parameters[10] = new SqlParameter("@Phone", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.InternetProvider.Phone);

                parameters[11] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.InternetProvider.ModifiedBy);

                parameters[12] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(1);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetProviderUpdate, parameters);
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
                return request.InternetProvider;

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
        #endregion [ Update InternetProvider ]

        #region[ Delete InternetProvider ]
        //Delete/Update Status to 2 the InternetProvider from the DB based on the given parameters
        public bool DeleteInternetProviderByInternetProviderID(int internetProviderID)
        {
            SqlDataReader reader = null;
            dsInternetProvider = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ProviderID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(internetProviderID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteInternetProviderByInternetProviderID, parameters);
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
        #endregion[Delete InternetProvider]

        #region [Get All InternetProviders]
        public List<InternetProvider> GetAllInternetProviders(int siteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllInternetProvider);

            SqlDataReader reader = null;
            dsInternetProvider = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                dsInternetProvider = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetProvider_List, parameters);
                if (dsInternetProvider != null)
                {
                    InternetProviderList = ConvertAllInternetProviderAttributesToObjectList(dsInternetProvider);
                    if (InternetProviderList != null && InternetProviderList.Count > 0)
                    {
                        return InternetProviderList;
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
        #endregion [ GET ALL InternetProvider ]

        #region [Get InternetProvider And InternetProvider Attribute Details By InternetProviderID]

        public InternetProvider GetInternetProviderAndInternetProviderDetailsByInternetProviderID(int internetProviderID)
        {

            dsInternetProvider = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ProviderID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(internetProviderID);
                dsInternetProvider = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPInternetProviderByInternetProviderID, parameters);
                if (dsInternetProvider != null)
                {
                    InternetProviderList = ConvertAllInternetProviderAttributesToObjectList(dsInternetProvider);
                    if (InternetProviderList != null && InternetProviderList.Count > 0)
                    {
                        return InternetProviderList[0];
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

        //All the InternetProvider Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<InternetProvider> ConvertAllInternetProviderAttributesToObjectList(DataSet ds)
        {
            InternetProviderList = new List<InternetProvider>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();


            DataTable InternetProviderdt = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    InternetProviderdt = ds.Tables[0];

                    //Convert InternetProvider Data table to its Corresponding List
                    if (InternetProviderdt.Rows.Count > 0)
                    {
                        InternetProviderList = (from DataRow internetProvider in InternetProviderdt.Rows
                                                select new InternetProvider
                                                {

                                                    ProviderID = DataRowHelper.ConvertToInteger(internetProvider[columnProviderID]),
                                                    Provider = DataRowHelper.ConvertToString(internetProvider[columnProvider]),
                                                    CircutID = DataRowHelper.ConvertToString(internetProvider[columnCircutID]),
                                                    AccountNumber = DataRowHelper.ConvertToString(internetProvider[columnAccountNumber]),
                                                    ProviderType = DataRowHelper.ConvertToString(internetProvider[columnProviderType]),
                                                    BrandWidth = DataRowHelper.ConvertToString(internetProvider[columnBrandWidth]),
                                                    NetworkID = DataRowHelper.ConvertToString(internetProvider[columnNetworkID]),
                                                    StaticIPAddress = DataRowHelper.ConvertToString(internetProvider[columnStaticIPAddress]),
                                                    Subnet = DataRowHelper.ConvertToString(internetProvider[columnSubnet]),
                                                    Gateway = DataRowHelper.ConvertToString(internetProvider[columnGateway]),
                                                    Phone = DataRowHelper.ConvertToString(internetProvider[columnPhone]),
                                                    StatusID = DataRowHelper.ConvertToInteger(internetProvider[columnStatusID]),
                                                    CreatedBy = DataRowHelper.ConvertToInteger(internetProvider[columnCreatedBy]),
                                                    CreatedOn = DataRowHelper.ConvertToDateTime(internetProvider[columnCreatedOn]),
                                                    ModifiedBy = DataRowHelper.ConvertToInteger(internetProvider[columnModifiedBy]),
                                                    ModifiedOn = DataRowHelper.ConvertToDateTime(internetProvider[columnModifiedOn]),
                                                    View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Internet/Web-Provider&id=" + DataRowHelper.ConvertToInteger(internetProvider[columnProviderID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                                }).ToList();
                    }
                }

                return InternetProviderList;

            }
            return null;
        }

    }
}
