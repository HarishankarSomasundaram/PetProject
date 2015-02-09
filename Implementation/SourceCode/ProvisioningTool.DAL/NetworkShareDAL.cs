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
using System.Reflection;

namespace ProvisioningTool.DAL
{
    internal class NetworkShareDAL
    {
        #region [ Declarations ]
        private List<NetworkShareDetail> networkShareDetailList;
        private NetworkShareDetail networkShareDetail;

        #region [Colunm Attributes]

        private readonly string columnNetworkShareID = "NetworkShareID";
        private readonly string columnNetworkShareName = "NetworkShareName";
        private readonly string columnNetworkShareDetailID = "NetworkShareDetailID";
        private readonly string columnServerMapped = "Mapped";
        private readonly string columnPath = "Path";
        private readonly string columnNetworkShareDescription = "NetworkShareDescription";
        private readonly string columnSystemAssignedUserID = "AssignedUser";
        private readonly string columnSystemID = "SystemID";
        private readonly string columnClientID = "ClientID";
        private readonly string columnUserID = "UserID";
        private readonly string columnSystemMasterID = "SystemMasterID";
        private readonly string columnSystemMasterName = "SystemMasterName";
        private readonly string columnFirstName = "FirstName";
        private readonly string columnLastName = "LastName";
        private readonly string columnUserName = "UserName";

        #endregion  [Colunm Attributes]

        #endregion [ Declarations ]

        #region [ Constructor ]

        internal NetworkShareDAL()
        {
        }

        #endregion [ Constructor ]

        #region [ Add NetworkShare ]
        public NetworkShare AddNetworkShare(PTRequest request, out bool isDuplicate, out int rowsAffected, out int iNetworkShareID)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = iNetworkShareID = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[5];


                parameters[0] = new SqlParameter("@NetworkShareName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.NetworkShare.NetworkShareName);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.CreatedBy);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.CreatedBy);

                parameters[4] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.SiteID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkShareAdd, parameters);
                if (reader != null)
                {
                    reader.Read();
                    rowsAffected = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameRowsAffected);
                    isDuplicate = DataRowHelper.ConvertToBoolean(reader, DalHelper.columnNameIsDuplicate);
                    iNetworkShareID = DataRowHelper.ConvertToInteger(reader, columnNetworkShareID);
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                return request.NetworkShare;

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
        #endregion [ Add NetworkShare ]

        #region [ Add Network Share  Details ]
        public NetworkShareDetail AddNetworkShareDetail(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[8];


                parameters[0] = new SqlParameter("@NetworkShareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShareDetail.NetworkShareID);

                parameters[1] = new SqlParameter("@Mapped", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.Mapped);

                parameters[5] = new SqlParameter("@Path", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.Path);

                parameters[6] = new SqlParameter("@NetworkShareDescription", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.NetworkShareDescription);

                parameters[7] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.NetworkShareAssignedUserIDs);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.CreatedBy);

                parameters[4] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.CreatedBy);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkShareDetailAdd, parameters);
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
                return request.NetworkShareDetail;

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
        #endregion [ Add Network Share  Details  ]

        #region [ Update NetworkShareDetail ]
        public NetworkShare ModifyNetworkShare(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[9];

                parameters[0] = new SqlParameter("@NetworkShareDetailID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShareDetail.NetworkShareDetailID);

                parameters[1] = new SqlParameter("@Mapped", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.Mapped);

                parameters[5] = new SqlParameter("@Path", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.Path);

                parameters[6] = new SqlParameter("@NetworkShareDescription", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.NetworkShareDescription);

                parameters[7] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.NetworkShareDetail.NetworkShareAssignedUserIDs);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.CreatedBy);

                parameters[4] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.NetworkShare.CreatedBy);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkShareUpdate, parameters);
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
                return request.NetworkShare;

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
        #endregion [ Update NetworkShareDetail ]

        #region [ Delete NetworkShare ]
        internal bool DeleteNetworkShareByNetworkShareID(int NetworkShareID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@NetworkShareDetailID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(NetworkShareID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteNetworkShareByNetworkShareID, parameters);
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
        #endregion [ Delete NetworkShare ]

        #region [Get All NetworkShare]
        public List<NetworkShareDetail> GetAllNetworkShare(int SiteID, string searchFilter)
        {
            SqlDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(SiteID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);

                //reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkShare_List, parameters);
                //if (reader != null)
                //{
                //    return ProcessDataReader(reader);
                //}
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkShare_List, parameters);
                if (ds != null)
                {
                    networkShareDetailList = ProcessDataSet(ds);
                }
                return networkShareDetailList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion

        #region [Get Network Share Details by Network Share ID ]
        public NetworkShareDetail GetNetworkShareDetailsByNetworkShareDetailID(int NetworkShareDetailID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@NetworkShareDetailID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(NetworkShareDetailID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkShareAndNetworkShareDetailsByNetworkShareID, parameters);
                if (ds != null)
                {
                    networkShareDetailList = ProcessDataSet(ds);
                }
                return networkShareDetailList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [Get Network Share Details by Network Share ID ]

        #region [ private methods ]       

        private List<NetworkShareDetail> ProcessDataSet(DataSet ds)
        {
            networkShareDetailList = new List<NetworkShareDetail>();


            DataTable networkShareDetailDT = new DataTable();
            DataTable networkShareDT = new DataTable();
            DataTable userDT = new DataTable();
            DataTable systemMasterDT = new DataTable();
            DataTable systemAssignedUsersDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    networkShareDetailDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    networkShareDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    systemAssignedUsersDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    userDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    systemMasterDT = ds.Tables[4];
            }

            if (networkShareDetailDT.Rows.Count > 0)
            {
                networkShareDetailList = (from DataRow networkShareDetail in networkShareDetailDT.Rows

                                          select new NetworkShareDetail
                                          {
                                              NetworkShareDetailID = DataRowHelper.ConvertToInteger(networkShareDetail[columnNetworkShareDetailID]),
                                              NetworkShareID = DataRowHelper.ConvertToInteger(networkShareDetail[columnNetworkShareID]),
                                              NetworkShareName = DataRowHelper.ConvertToString(networkShareDetail[columnNetworkShareName]),
                                              Path = DataRowHelper.ConvertToString(networkShareDetail[columnPath]),
                                              Mapped = DataRowHelper.ConvertToString(networkShareDetail[columnServerMapped]),
                                              NetworkShareAssignedUserIDs = DataRowHelper.ConvertToString(networkShareDetail[columnSystemAssignedUserID]),
                                              NetworkShareDescription = DataRowHelper.ConvertToString(networkShareDetail[columnNetworkShareDescription]),
                                              View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Network%20Shares&id=" + DataRowHelper.ConvertToString(networkShareDetail[columnNetworkShareDetailID]) + " style='color: blue;text-decoration: underline;'>More</a>"),
                                              NetworkShare = (from DataRow networkShare in networkShareDT.Rows
                                                              where networkShare.Field<int>(columnNetworkShareID) == DataRowHelper.ConvertToInteger(networkShareDetail[columnNetworkShareID])
                                                              select (new NetworkShare
                                                              {
                                                                  NetworkShareID = DataRowHelper.ConvertToInteger(networkShare[columnNetworkShareID]),
                                                                  NetworkShareName = DataRowHelper.ConvertToString(networkShare[columnNetworkShareName]),

                                                              })).FirstOrDefault(),
                                              NetworkShareAssignedUsers = (from DataRow networkShareAssignedUser in systemAssignedUsersDT.Rows
                                                                          where networkShareAssignedUser.Field<int>(columnSystemID) == 5 &&
                                                                          networkShareAssignedUser.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(networkShareDetail[columnNetworkShareDetailID])
                                                                          select (new AssignedUser
                                                                          {
                                                                              ClientID = DataRowHelper.ConvertToInteger(networkShareAssignedUser[columnSystemID]),
                                                                              User = (from DataRow user in userDT.Rows
                                                                                      where user.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(networkShareAssignedUser[columnUserID])
                                                                                      select (new User
                                                                                      {
                                                                                          UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                                                                          FirstName = DataRowHelper.ConvertToString(user[columnFirstName]),
                                                                                          LastName = DataRowHelper.ConvertToString(user[columnLastName]),
                                                                                          UserName = DataRowHelper.ConvertToString(user[columnUserName])
                                                                                      })).FirstOrDefault(),
                                                                              System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                                        where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(networkShareAssignedUser[columnSystemID])
                                                                                        select (new SystemMaster
                                                                                        {
                                                                                            SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                            SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                                        })).FirstOrDefault(),
                                                                          })).ToList(),
                                          }).ToList();
            }

            return networkShareDetailList;
        }
        #endregion [ private methods ]

    }
}
