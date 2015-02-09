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
    internal class UserDAL
    {
        #region [ Declarations ]
        private List<User> UserList;
        private User user;
        #region [Colunm Attributes]

        private readonly string columnUserID = "UserID";
        private readonly string columnFirstName = "FirstName";
        private readonly string columnLastName = "LastName";
        private readonly string columnUserName = "UserName";
        private readonly string columnPassword = "Password";

        private readonly string columnTitleID = "TitleID";
        private readonly string columnTitleName = "TitleName";

        private readonly string columnDepartmentID = "DepartmentID";
        private readonly string columnDepartmentName = "DepartmentName";

        private readonly string columnUserInfoID = "UserInfoID";
        private readonly string columnMasterID = "MasterID";
        private readonly string columnMasterName = "MasterName";

        private readonly string columnEmail = "Email";
        private readonly string columnPhone1 = "Phone1";
        private readonly string columnPhone2 = "Phone2";
        private readonly string columnNotes = "Notes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedOn = "ModifiedOn";

        //Columns to get Multiple selected attribute values of User 
        private readonly string columnSelectedectedAppIDs = "SelectedAppIDs";
        private readonly string columnSelectedectedComputerIDs = "SelectedComputerIDs";
        private readonly string columnSelectedLaptopItems = "SelLaptopItems";
        private readonly string columnSelectedectedMobilePhoneIDs = "SelectedMobilePhoneIDs";
        private readonly string columnSelectedNetworkShareItems = "SelNetworkShareItems";
        private readonly string columnSelectedectedPrinterIDs = "SelectedPrinterIDs";
        private readonly string columnSelectedRemoteAccessItems = "SelRemoteAccessItems";
        private readonly string columnSelectedectedSecurityGroupIDs = "SelectedSecurityGroupIDs";
        private readonly string columnSelectedServerItems = "SelServerItems";
        private readonly string columnSelectedectedTabletIDs = "SelectedTabletIDs";

        private readonly string columnMasterDetailID = "MasterDetailID";
        private readonly string columnMasterValue = "MasterValue";
        private readonly string columnTemplate = "Template";


        private readonly string columnID = "ID";
        private readonly string columnName = "Name";
        private readonly string columnDummyDate = "DummyDate";
        private readonly string columnMappingID = "MappingID";
        //private readonly string columnSelectedectedTabletIDs = "SelectedTabletIDs";



        #endregion

        #endregion [ Declarations ]

        #region [ Add Users ]
        internal User AddUsers(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@FirstName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.User.FirstName);

                parameters[1] = new SqlParameter("@LastName", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.User.LastName);

                parameters[2] = new SqlParameter("@UserName", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.User.UserName);

                parameters[3] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.User.Password);

                parameters[4] = new SqlParameter("@TitleID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.User.TitleID);

                parameters[5] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.User.DepartmentID);

                parameters[6] = new SqlParameter("@Email", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.User.Email);

                parameters[7] = new SqlParameter("@Phone1", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.User.Phone1);

                parameters[8] = new SqlParameter("@Phone2", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.User.Phone2);

                parameters[9] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.User.Notes);

                parameters[10] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[10].Value = DBValueHelper.ConvertToDBInteger(request.User.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.User.CreatedBy);

                //CR Start
                //parameters[12] = new SqlParameter("@UserApps", SqlDbType.VarChar);
                //parameters[12].Value = request.UserApps.SelectedAppIDs;
                //CR Ends

                parameters[12] = new SqlParameter("@UserComputers", SqlDbType.VarChar);
                parameters[12].Value = request.UserComputer.SelectedComputerIDs;

                parameters[13] = new SqlParameter("@UserLaptops", SqlDbType.VarChar);
                parameters[13].Value = request.UserLaptop.SelLaptopItems;

                parameters[14] = new SqlParameter("@UserMobilePhones", SqlDbType.VarChar);
                parameters[14].Value = request.UserMobilePhone.SelectedMobilePhoneIDs;

                parameters[15] = new SqlParameter("@UserNetworkShares", SqlDbType.VarChar);
                parameters[15].Value = request.UserNetworkShare.SelNetworkShareItems;

                parameters[16] = new SqlParameter("@UserPrinters", SqlDbType.VarChar);
                parameters[16].Value = request.UserPrinter.SelectedPrinterIDs;

                parameters[17] = new SqlParameter("@UserRemoteAccess", SqlDbType.VarChar);
                parameters[17].Value = request.UserRemoteAccess.SelRemoteAccessItems;

                parameters[18] = new SqlParameter("@UserSecurityGroup", SqlDbType.VarChar);
                parameters[18].Value = request.UserSecurityGroup.SelectedSecurityGroupIDs;

                parameters[19] = new SqlParameter("@UserServers", SqlDbType.VarChar);
                parameters[19].Value = request.UserServer.SelServerItems;

                parameters[20] = new SqlParameter("@UserTablets", SqlDbType.VarChar);
                parameters[20].Value = request.UserTablet.SelectedTabletIDs;

                parameters[21] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[21].Value = request.sessionSiteID;

                parameters[22] = new SqlParameter("@IsAutoTask", SqlDbType.Bit);
                parameters[22].Value = DBValueHelper.ConvertToDBBoolean(request.User.IsAutoTask);

                parameters[23] = new SqlParameter("@MappingID", SqlDbType.VarChar);
                parameters[23].Value = DBValueHelper.ConvertToDBString(request.User.MappingID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPUserAdd, parameters);
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
                return request.User;

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
        #endregion [ Add Users ]

        #region [ Update User ]
        internal User ModifyUser(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[21];

                parameters[0] = new SqlParameter("@UserID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.User.UserID);

                parameters[1] = new SqlParameter("@FirstName", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.User.FirstName);

                parameters[2] = new SqlParameter("@LastName", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.User.LastName);

                parameters[3] = new SqlParameter("@UserName", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.User.UserName);

                parameters[4] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.User.Password);

                parameters[5] = new SqlParameter("@TitleID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.User.TitleID);

                parameters[6] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(request.User.DepartmentID);

                parameters[7] = new SqlParameter("@Email", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.User.Email);

                parameters[8] = new SqlParameter("@Phone1", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.User.Phone1);

                parameters[9] = new SqlParameter("@Phone2", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.User.Phone2);

                parameters[10] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.User.Notes);

                parameters[11] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.User.ModifiedBy);

                //CR Start
                //parameters[12] = new SqlParameter("@UserApps", SqlDbType.VarChar);
                //parameters[12].Value = request.UserApps.SelectedAppIDs;
                //CR Ends

                parameters[12] = new SqlParameter("@UserComputers", SqlDbType.VarChar);
                parameters[12].Value = request.UserComputer.SelectedComputerIDs;

                parameters[13] = new SqlParameter("@UserLaptops", SqlDbType.VarChar);
                parameters[13].Value = request.UserLaptop.SelLaptopItems;

                parameters[14] = new SqlParameter("@UserMobilePhones", SqlDbType.VarChar);
                parameters[14].Value = request.UserMobilePhone.SelectedMobilePhoneIDs;

                parameters[15] = new SqlParameter("@UserNetworkShares", SqlDbType.VarChar);
                parameters[15].Value = request.UserNetworkShare.SelNetworkShareItems;

                parameters[16] = new SqlParameter("@UserPrinters", SqlDbType.VarChar);
                parameters[16].Value = request.UserPrinter.SelectedPrinterIDs;

                parameters[17] = new SqlParameter("@UserRemoteAccess", SqlDbType.VarChar);
                parameters[17].Value = request.UserRemoteAccess.SelRemoteAccessItems;

                parameters[18] = new SqlParameter("@UserSecurityGroup", SqlDbType.VarChar);
                parameters[18].Value = request.UserSecurityGroup.SelectedSecurityGroupIDs;

                parameters[19] = new SqlParameter("@UserServers", SqlDbType.VarChar);
                parameters[19].Value = request.UserServer.SelServerItems;

                parameters[20] = new SqlParameter("@UserTablets", SqlDbType.VarChar);
                parameters[20].Value = request.UserTablet.SelectedTabletIDs;



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPUserUpdate, parameters);
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
                return request.User;

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
        #endregion [ Update Users ]

        #region [ Delete User ]
        internal bool DeleteUserByUserID(int userID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@UserID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(userID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteUserByUserID, parameters);
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
        #endregion [ Delete Users ]

        #region [Get All Users]
        public List<User> GetAllUsers(int siteID, string searchFilter)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);

            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPUser_List, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return UserList;
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
        #endregion

        #region [Get All Users]
        public List<User> GetAllUsersWithoutSiteID()
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);

            SqlDataReader reader = null;
            try
            {

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPUserWithOutSiteID_List);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return UserList;
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
        #endregion

        #region [Get User And User Attribute Details By UserID]

        public User GetUserAndUserDetailsByUserID(int userID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@UserID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(userID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPUserAndUserDetailsByUserID, parameters);
                if (ds != null)
                {
                    user = ConvertAllUserAttributesToObject(ds);
                }
                return user;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        #region [ ConvertAllUserAttributesToObject]
        //All the User Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ UserApps,UserComputers,UserLaptops,UserMobilePhones... to List]
        public User ConvertAllUserAttributesToObject(DataSet ds)
        {
            UserList = new List<User>();

            DataTable userdt = new DataTable();
            DataTable userAppsdt = new DataTable();
            DataTable userComputerdt = new DataTable();
            DataTable userMobilePhonedt = new DataTable();
            DataTable userPrinterdt = new DataTable();
            DataTable userSecurityGroupdt = new DataTable();
            DataTable userTabletdt = new DataTable();
            DataTable userLaptopdt = new DataTable();
            DataTable userNetworkSharesdt = new DataTable();
            DataTable userRemoteAccessdt = new DataTable();
            DataTable userServersdt = new DataTable();
            DataTable userNetworkSharesDetaildt = new DataTable();
            DataTable globalMasterDetaildt = new DataTable();


            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    userdt = ds.Tables[0];
                if (ds.Tables[1] != null)
                    globalMasterDetaildt = ds.Tables[1];
                if (ds.Tables[2] != null)
                    userAppsdt = ds.Tables[2];
                if (ds.Tables[3] != null)
                    userComputerdt = ds.Tables[3];
                if (ds.Tables[4] != null)
                    userMobilePhonedt = ds.Tables[4];
                if (ds.Tables[5] != null)
                    userPrinterdt = ds.Tables[5];
                if (ds.Tables[6] != null)
                    userSecurityGroupdt = ds.Tables[6];
                if (ds.Tables[7] != null)
                    userTabletdt = ds.Tables[7];
                if (ds.Tables[8] != null)
                    userLaptopdt = ds.Tables[8];
                if (ds.Tables[9] != null)
                    userNetworkSharesdt = ds.Tables[9];
                if (ds.Tables[10] != null)
                    userRemoteAccessdt = ds.Tables[10];
                if (ds.Tables[11] != null)
                    userServersdt = ds.Tables[11];
                if (ds.Tables[12] != null)
                    userNetworkSharesDetaildt = ds.Tables[12];


                //Convert user Data table to its Corresponding List
                if (userdt.Rows.Count > 0)
                {
                    UserList = (from DataRow user in userdt.Rows
                                select new User
                                {
                                    UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                    FirstName = DataRowHelper.ConvertToString(user[columnFirstName], ""),
                                    LastName = DataRowHelper.ConvertToString(user[columnLastName], ""),
                                    UserName = DataRowHelper.ConvertToString(user[columnUserName], ""),
                                    Password = DataRowHelper.ConvertToString(user[columnPassword], ""),
                                    MappingID = DataRowHelper.ConvertToString(user[columnMappingID], ""),
                                    //Title.MasterDetailID = DataRowHelper.ConvertToInteger(user[columnTitleID],""),
                                    TitleID = DataRowHelper.ConvertToInteger(user[columnTitleID]),
                                    TitleName = DataRowHelper.ConvertToString(user[columnTitleName], ""),

                                    //Department.MasterDetailID = DataRowHelper.ConvertToInteger(user[columnDepartmentID],""),
                                    DepartmentID = DataRowHelper.ConvertToInteger(user[columnDepartmentID]),
                                    DepartmentName = DataRowHelper.ConvertToString(user[columnDepartmentName], ""),

                                    //User App List
                                    SelectedApps = DataRowHelper.ConvertToString(user[columnSelectedectedAppIDs], ""),
                                    UserAppsList = (from DataRow userApps in userAppsdt.Rows
                                                    where userApps.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                    select (new UserApp
                                                  {
                                                      UserID = DataRowHelper.ConvertToInteger(userApps[columnUserID]),
                                                      App = (from DataRow gmd in globalMasterDetaildt.Rows
                                                             where gmd.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(userApps[columnMasterDetailID])
                                                             select (new GlobalMasterDetail
                                                             {
                                                                 MasterDetailID = DataRowHelper.ConvertToInteger(gmd[columnMasterDetailID]),
                                                                 MasterValue = DataRowHelper.ConvertToString(gmd[columnMasterValue], "")
                                                             })).FirstOrDefault()
                                                  })).ToList(),

                                    //User Computer  List
                                    SelectedComputer = DataRowHelper.ConvertToString(user[columnSelectedectedComputerIDs], ""),
                                    UserComputerList = (from DataRow userComputer in userComputerdt.Rows
                                                        //where userComputer.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                        select (new WorkStationInfo
                                                        {
                                                            WorkStationID = DataRowHelper.ConvertToInteger(userComputer[columnID]),
                                                            HostName = DataRowHelper.ConvertToString(userComputer[columnName]),
                                                            InstalledDate = DataRowHelper.ConvertToString(DateTime.Now),
                                                            WarrantyExpires = DataRowHelper.ConvertToString(DateTime.Now),
                                                            CreatedOn = DataRowHelper.ConvertToDateTime(DateTime.Now),
                                                            ModifiedOn = DataRowHelper.ConvertToDateTime(DateTime.Now)

                                                        })).ToList(),

                                    //User MobilePhone List
                                    SelectedMobilePhone = DataRowHelper.ConvertToString(user[columnSelectedectedMobilePhoneIDs], ""),
                                    UserMobilePhoneList = (from DataRow userMobilePhone in userMobilePhonedt.Rows
                                                           //where userMobilePhone.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                           select (new MobileDevice
                                                        {
                                                            MobileDeviceID = DataRowHelper.ConvertToInteger(userMobilePhone[columnID]),
                                                            Hostname = DataRowHelper.ConvertToString(userMobilePhone[columnName]),
                                                            InstalledOn = DataRowHelper.ConvertToString(DateTime.Now),
                                                            CreatedOn = DataRowHelper.ConvertToDateTime(DateTime.Now),
                                                            ModifiedOn = DataRowHelper.ConvertToDateTime(DateTime.Now)
                                                        })).ToList(),

                                    //User Printer List
                                    SelectedPrinter = DataRowHelper.ConvertToString(user[columnSelectedectedPrinterIDs], ""),
                                    UserPrinterList = (from DataRow userPrinter in userPrinterdt.Rows
                                                       // where userPrinter.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                       select (new Printer
                                                       {
                                                           PrinterID = DataRowHelper.ConvertToInteger(userPrinter[columnID]),
                                                           Hostname = DataRowHelper.ConvertToString(userPrinter[columnName]),
                                                           InstalledOn = DataRowHelper.ConvertToString(DateTime.Now),
                                                           WarrantyExpiresOn = DataRowHelper.ConvertToString(DateTime.Now),
                                                           CreatedOn = DataRowHelper.ConvertToDateTime(DateTime.Now),
                                                           ModifiedOn = DataRowHelper.ConvertToDateTime(DateTime.Now)
                                                       })).ToList(),

                                    //User SecurityGroup List
                                    SelectedSecurityGroup = DataRowHelper.ConvertToString(user[columnSelectedectedSecurityGroupIDs], ""),
                                    UserSecurityGroupList = (from DataRow userSecurityGroup in userSecurityGroupdt.Rows
                                                             where userSecurityGroup.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                             select (new UserSecurityGroup
                                                             {
                                                                 UserID = DataRowHelper.ConvertToInteger(userSecurityGroup[columnUserID]),
                                                                 SecurityGroup = (from DataRow gmd in globalMasterDetaildt.Rows
                                                                                  where gmd.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(userSecurityGroup[columnMasterDetailID])
                                                                                  select (new GlobalMasterDetail
                                                                                  {
                                                                                      MasterDetailID = DataRowHelper.ConvertToInteger(gmd[columnMasterDetailID]),
                                                                                      MasterValue = DataRowHelper.ConvertToString(gmd[columnMasterValue], "")
                                                                                  })).FirstOrDefault()
                                                             })).ToList(),

                                    //User Tablet  List
                                    SelectedTablet = DataRowHelper.ConvertToString(user[columnSelectedectedTabletIDs], ""),
                                    UserTabletList = (from DataRow userTablet in userTabletdt.Rows
                                                      where userTablet.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                      select (new UserTablet
                                                      {
                                                          UserID = DataRowHelper.ConvertToInteger(userTablet[columnUserID]),
                                                          Tablet = (from DataRow gmd in globalMasterDetaildt.Rows
                                                                    where gmd.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(userTablet[columnMasterDetailID])
                                                                    select (new GlobalMasterDetail
                                                                    {
                                                                        MasterDetailID = DataRowHelper.ConvertToInteger(gmd[columnMasterDetailID]),
                                                                        MasterValue = DataRowHelper.ConvertToString(gmd[columnMasterValue], "")
                                                                    })).FirstOrDefault()
                                                      })).ToList(),

                                    //User Laptop List
                                    SelectedLaptop = DataRowHelper.ConvertToString(user[columnSelectedLaptopItems], ""),
                                    UserLaptopList = (from DataRow userLaptop in userLaptopdt.Rows
                                                      // where userLaptop.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                      select (new LaptopInfo
                                                      {
                                                          LaptopID = DataRowHelper.ConvertToInteger(userLaptop[columnID]),
                                                          HostName = DataRowHelper.ConvertToString(userLaptop[columnName]),
                                                          InstalledDate = DataRowHelper.ConvertToString(DateTime.Now),
                                                          WarrantyExpires = DataRowHelper.ConvertToString(DateTime.Now),
                                                          CreatedOn = DataRowHelper.ConvertToDateTime(DateTime.Now),
                                                          ModifiedOn = DataRowHelper.ConvertToDateTime(DateTime.Now)
                                                      })).ToList(),

                                    //User NetworkShares List
                                    SelectedNetworkShares = DataRowHelper.ConvertToString(user[columnSelectedNetworkShareItems], ""),
                                    UserNetworkSharesList = (from DataRow userNetworkShare in userNetworkSharesdt.Rows
                                                             // where userNetworkShare.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                             select (new NetworkShare
                                                             {
                                                                 NetworkShareID = DataRowHelper.ConvertToInteger(userNetworkShare[columnID]),
                                                                 NetworkShareName = DataRowHelper.ConvertToString(userNetworkShare[columnName]),
                                                                 CreatedOn = DataRowHelper.ConvertToDateTime(DateTime.Now),
                                                                 ModifiedOn = DataRowHelper.ConvertToDateTime(DateTime.Now)
                                                             })).ToList(),

                                    //User RemoteAccess List
                                    SelectedRemoteAccess = DataRowHelper.ConvertToString(user[columnSelectedRemoteAccessItems], ""),
                                    UserRemoteAccessList = (from DataRow userRemoteAccess in userRemoteAccessdt.Rows
                                                            where userRemoteAccess.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                            select (new UserRemoteAccess
                                                            {
                                                                UserID = DataRowHelper.ConvertToInteger(userRemoteAccess[columnUserID]),
                                                                RemoteAccess = (from DataRow gmd in globalMasterDetaildt.Rows
                                                                                where gmd.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(userRemoteAccess[columnMasterDetailID])
                                                                                select (new GlobalMasterDetail
                                                                                {
                                                                                    MasterDetailID = DataRowHelper.ConvertToInteger(gmd[columnMasterDetailID]),
                                                                                    MasterValue = DataRowHelper.ConvertToString(gmd[columnMasterValue], "")
                                                                                })).FirstOrDefault()
                                                            })).ToList(),

                                    //User Servers List
                                    SelectedServers = DataRowHelper.ConvertToString(user[columnSelectedServerItems], ""),
                                    UserServersList = (from DataRow userServer in userServersdt.Rows
                                                       //  where userServer.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(user[columnUserID])
                                                       select (new ServerInfo
                                                       {
                                                           ServerID = DataRowHelper.ConvertToInteger(userServer[columnID]),
                                                           HostName = DataRowHelper.ConvertToString(userServer[columnName]),
                                                           InstalledDate = DataRowHelper.ConvertToString(DateTime.Now),
                                                           WarrantyExpires = DataRowHelper.ConvertToString(DateTime.Now),
                                                           CreatedOn = DataRowHelper.ConvertToDateTime(DateTime.Now),
                                                           ModifiedOn = DataRowHelper.ConvertToDateTime(DateTime.Now)

                                                       })).ToList(),

                                    //Convert each Object List to sub Lists

                                    Email = DataRowHelper.ConvertToString(user[columnEmail], ""),
                                    Phone1 = DataRowHelper.ConvertToString(user[columnPhone1], ""),
                                    Phone2 = DataRowHelper.ConvertToString(user[columnPhone2], ""),
                                    Notes = DataRowHelper.ConvertToString(user[columnNotes], ""),
                                    StatusID = DataRowHelper.ConvertToInteger(user[columnStatusID]),
                                    CreatedBy = DataRowHelper.ConvertToInteger(user[columnCreatedBy]),
                                    CreatedOn = DataRowHelper.ConvertToDateTime(user[columnCreatedOn]),
                                    ModifiedBy = DataRowHelper.ConvertToInteger(user[columnModifiedBy]),
                                    ModifiedOn = DataRowHelper.ConvertToDateTime(user[columnModifiedOn]),
                                }).ToList();
                }
                return UserList[0];
            }
            return null;
        }
        #endregion

        #endregion

        #region [Private Methods]
        private List<User> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                UserList = new List<User>();
                while (reader.Read())
                    UserList.Add(ConvertToObject(reader));
                return UserList;
            }
            return null;
        }

        private User ConvertToObject(IDataRecord dataRecord)
        {
            User user = new User();
            user.Title = new GlobalMasterDetail();
            user.Department = new GlobalMasterDetail();

            user.UserID = DataRowHelper.ConvertToInteger(dataRecord, columnUserID);
            user.FirstName = DataRowHelper.ConvertToString(dataRecord, columnFirstName);
            user.LastName = DataRowHelper.ConvertToString(dataRecord, columnLastName);
            user.UserName = DataRowHelper.ConvertToString(dataRecord, columnUserName);
            user.Password = DataRowHelper.ConvertToString(dataRecord, columnPassword);
            user.MappingID = DataRowHelper.ConvertToString(dataRecord, columnMappingID);

            user.Title.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnTitleID);
            user.TitleID = DataRowHelper.ConvertToInteger(dataRecord, columnTitleID);
            user.TitleName = DataRowHelper.ConvertToString(dataRecord, columnTitleName);

            user.Department.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnDepartmentID);
            user.DepartmentID = DataRowHelper.ConvertToInteger(dataRecord, columnDepartmentID);
            user.DepartmentName = DataRowHelper.ConvertToString(dataRecord, columnDepartmentName);

            //This Static binding is for Showing the View Col in Jq Grid
            user.View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=User%20Info&id=" + ConvertHelper.ConvertToString(user.UserID) + " style='color: blue;text-decoration: underline;'>More</a>");

            //Convert each Object List to sub Lists

            user.Email = DataRowHelper.ConvertToString(dataRecord, columnEmail);
            user.Phone1 = DataRowHelper.ConvertToString(dataRecord, columnPhone1);
            user.Phone2 = DataRowHelper.ConvertToString(dataRecord, columnPhone2);
            user.Notes = DataRowHelper.ConvertToString(dataRecord, columnNotes);
            //user.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
            //user.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
            //user.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
            //user.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
            //user.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);

            return user;
        }
        #endregion
    }
}
