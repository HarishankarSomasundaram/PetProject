using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProvisioningTool.DAL
{
    internal class PhoneSystemDAL
    {
        #region [ Declarations ]
        private List<PhoneSystem> phoneSystemList;
        private readonly string columnPhoneSystemID = "PhoneSystemID";
        private readonly string columnPhoneType = "PhoneType";
        private readonly string columnHostname = "Hostname";
        private readonly string columnManufacture = "Manufacture";
        private readonly string columnModelID = "ModelID";
        private readonly string columnMemory = "Memory";
        private readonly string columnSerialNumber = "SerialNumber";
        private readonly string columnInstalledOn = "InstalledOn";
        private readonly string columnWarrantyExpiresOn = "WarrantyExpiresOn";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnSubnet = "Subnet";
        private readonly string columnGateway = "Gateway";
        private readonly string columnAdminUserName = "AdminUserName";
        private readonly string columnAdminPassword = "AdminPassword";
        private readonly string columnOSVersionID = "OSVersionID";
        private readonly string columnFirmware = "Firmware";
        private readonly string columnPhoneSystemModules = "PhoneSystemModuleIDS";
        private readonly string columnPhoneSystemInterfaces = "PhoneSystemInterfaces";
        private readonly string columnAssignedUsers = "AssignedUsers";
        private readonly string columnNotes = "Notes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";

        private readonly string columnModelName = "ModelName";
        private readonly string columnModuleName = "ModuleName";
        private readonly string columnPhoneSystemInterfaceID = "PhoneSystemInterfaceID";
        private readonly string columnInterfaceValue = "InterfaceValue";
        private readonly string columnModuleID = "ModuleID";
        private readonly string columnOSVersionName = "OSVersionName";
        private readonly string columnPhoneSystemModuleID = "PhoneSystemModuleID";
        private readonly string columnNotesMasterName = "NotesMasterName";
        private readonly string columnPhoneSystemInfo = "PhoneSystemInfo";

        private readonly string columnNotesMasterID = "NotesMasterID";
        private readonly string columnNotesDetailID = "NotesDetailID";
        private readonly string columnNotesClientID = "NotesClientID";
        private readonly string columnUserID = "UserID";
        private readonly string columnFirstName = "FirstName";
        private readonly string columnLastName = "LastName";
        private readonly string columnUserName = "UserName";
        private readonly string columnPassword = "Password";
        private readonly string columnTitleID = "TitleID";
        private readonly string columnDepartmentID = "DepartmentID";
        private readonly string columnEmail = "Email";
        private readonly string columnPhone1 = "Phone1";
        private readonly string columnPhone2 = "Phone2";
        private readonly string columnPhoneSystemAssignedUserID = "PhoneSystemAssignedUserID";
        private readonly string columnPhoneSystemAssignedUsers = "PhoneSystemAssignedUsers";
        private readonly string columnPhoneSystemNotes = "PhoneSystemNotes";

        private readonly string columnSystemMasterID = "SystemMasterID";
        private readonly string columnSystemMasterName = "SystemMasterName";
        private readonly string columnDocumentPath = "DocumentPath";

        #endregion [ Declarations ]

        internal PhoneSystemDAL()
        {
        }

        #region [ Add PhoneSystem ]
        internal PhoneSystem AddPhoneSystem(PhoneSystem phoneSystem, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[25];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(phoneSystem.Hostname);

                parameters[1] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(phoneSystem.Manufacture);

                parameters[2] = new SqlParameter("@Memory", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(phoneSystem.Memory);

                parameters[3] = new SqlParameter("@ModelID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.PhoneSystemModel.MasterDetailID);

                parameters[4] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(phoneSystem.SerialNumber);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.Date);
                parameters[5].Value = DBValueHelper.ConvertToDBDate(phoneSystem.InstalledOn);

                parameters[6] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.Date);
                parameters[6].Value = DBValueHelper.ConvertToDBDate(phoneSystem.WarrantyExpiresOn);

                parameters[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(phoneSystem.IPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(phoneSystem.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(phoneSystem.Gateway);

                parameters[10] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(phoneSystem.AdminUserName);

                parameters[11] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(phoneSystem.AdminPassword);

                parameters[12] = new SqlParameter("@OSVersionID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.OSVersion.MasterDetailID);

                parameters[13] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(phoneSystem.Firmware);

                parameters[14] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.CreatedBy);

                parameters[15] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemModules);

                parameters[16] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemInterfaces);

                parameters[17] = new SqlParameter("@AssignedUsers", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemAssignedUsers);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemNotes);

                parameters[19] = new SqlParameter("@PhoneType", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneType);

                parameters[20] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.Site.SiteID);

                //DOCUMENTS
                parameters[21] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.Type);

                parameters[22] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.DocumentType);

                parameters[23] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[23].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.DocumentName);

                parameters[24] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[24].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.DocumentPath);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPhoneSystemAdd, parameters);
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
                return phoneSystem;

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
        #endregion [ Add Router ]

        #region [ Modify PhoneSystem By PhoneSystemID ]
        internal PhoneSystem ModifyPhoneSystem(PhoneSystem phoneSystem, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[25];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(phoneSystem.Hostname);

                parameters[1] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(phoneSystem.Manufacture);

                parameters[2] = new SqlParameter("@Memory", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(phoneSystem.Memory);

                parameters[3] = new SqlParameter("@ModelID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.PhoneSystemModel.MasterDetailID);

                parameters[4] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(phoneSystem.SerialNumber);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(phoneSystem.InstalledOn);

                parameters[6] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(phoneSystem.WarrantyExpiresOn);

                parameters[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(phoneSystem.IPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(phoneSystem.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(phoneSystem.Gateway);

                parameters[10] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(phoneSystem.AdminUserName);

                parameters[11] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(phoneSystem.AdminPassword);

                parameters[12] = new SqlParameter("@OSVersionID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.OSVersion.MasterDetailID);

                parameters[13] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(phoneSystem.Firmware);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.ModifiedBy);

                parameters[15] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemModules);

                parameters[16] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemInterfaces);

                parameters[17] = new SqlParameter("@AssignedUsers", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemAssignedUsers);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneSystemNotes);

                parameters[19] = new SqlParameter("@PhoneType", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(phoneSystem.PhoneType);

                parameters[20] = new SqlParameter("@PhoneSystemID", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(phoneSystem.PhoneSystemID);

                //DOCUMENTS
                parameters[21] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.Type);

                parameters[22] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.DocumentType);

                parameters[23] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[23].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.DocumentName);

                parameters[24] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[24].Value = DBValueHelper.ConvertToDBString(phoneSystem.Documents.DocumentPath);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPhoneSystemUpdateByPhoneSystemID, parameters);
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
                return phoneSystem;

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
        #endregion [ Modify PhoneSystem By PhoneSystemID ]

        #region[ DeleteDeletePhoneSystemByPhoneSystemID ]
        public bool DeletePhoneSystemByPhoneSystemID(int phoneSystemID)
        {
            SqlDataReader reader = null;
            bool isDeleted = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@PhoneSystemID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(phoneSystemID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPhoneSystemDeleteByPhoneSystemID, parameters);
                if (reader != null)
                {
                    reader.Read();
                    isDeleted = DataRowHelper.ConvertToBoolean(reader, DalHelper.columnNameIsDeleted);
                }
                return isDeleted;
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
        #endregion[ DeletePhoneSystemByPhoneSystemID ]

        #region [ Get PhoneSystem By PhoneSystemID ]

        public PhoneSystem GetPhoneSystemByPhoneSystemID(int phoneSystemID)
        {
            DataSet ds = new DataSet();
            phoneSystemList = new List<PhoneSystem>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@PhoneSystemID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(phoneSystemID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPhoneSystemByPhoneSystemID_List, parameters);
                if (ds != null)
                {
                    phoneSystemList = ProcessDataSet(ds);
                    if (phoneSystemList.Count > 0)
                    {
                        return phoneSystemList[0];
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
        }
        #endregion [ Get PhoneSystem By PhoneSystemID ]

        #region [Get All PhoneSystems]
        public List<PhoneSystem> GetAllPhoneSystems(int siteID, string searchFilter)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);

                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPhoneSystem_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                return phoneSystemList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ GET ALL PhoneSystem ]

        #region [ ProcessDataSet]
        private List<PhoneSystem> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        #region [ ConvertToObject]
        private List<PhoneSystem> ConvertToObject(DataSet ds)
        {
            phoneSystemList = new List<PhoneSystem>();


            DataTable phoneSystemDT = new DataTable();
            DataTable GlobalMasterDetailDT = new DataTable();
            DataTable phoneSystemInterfaceDT = new DataTable();
            DataTable phoneSystemModulesDT = new DataTable();
            DataTable phoneSystemNotesDT = new DataTable();
            DataTable phoneSystemNotesDetailDT = new DataTable();
            DataTable UserDetailDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    phoneSystemDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    GlobalMasterDetailDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    phoneSystemInterfaceDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    phoneSystemModulesDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    phoneSystemNotesDT = ds.Tables[4];
                if (ds.Tables[5] != null)
                    phoneSystemNotesDetailDT = ds.Tables[5];
                if (ds.Tables[6] != null)
                    UserDetailDT = ds.Tables[6];

                if (phoneSystemDT.Rows.Count > 0)
                {
                    phoneSystemList = (from DataRow phoneSystem in phoneSystemDT.Rows

                                       select new PhoneSystem
                                       {
                                           PhoneSystemID = DataRowHelper.ConvertToInteger(phoneSystem[columnPhoneSystemID]),
                                           Hostname = DataRowHelper.ConvertToString(phoneSystem[columnHostname]),
                                           PhoneType = DataRowHelper.ConvertToString(phoneSystem[columnPhoneType]),
                                           Manufacture = DataRowHelper.ConvertToString(phoneSystem[columnManufacture]),
                                           PhoneSystemModel = (from DataRow phoneModel in GlobalMasterDetailDT.Rows
                                                               where phoneModel.Field<int>(columnModelID) == DataRowHelper.ConvertToInteger(phoneSystem[columnModelID])
                                                               select (new GlobalMasterDetail
                                                               {
                                                                   MasterDetailID = DataRowHelper.ConvertToInteger(phoneModel[columnModelID]),
                                                                   MasterValue = DataRowHelper.ConvertToString(phoneModel[columnModelName],"")
                                                               })).FirstOrDefault(),

                                           Memory = DataRowHelper.ConvertToString(phoneSystem[columnMemory]),
                                           SerialNumber = DataRowHelper.ConvertToString(phoneSystem[columnSerialNumber]),
                                           InstalledOn = DataRowHelper.ConvertToString(phoneSystem[columnInstalledOn]),
                                           WarrantyExpiresOn = DataRowHelper.ConvertToString(phoneSystem[columnWarrantyExpiresOn]),
                                           IPAddress = DataRowHelper.ConvertToString(phoneSystem[columnIPAddress]),
                                           Subnet = DataRowHelper.ConvertToString(phoneSystem[columnSubnet]),
                                           Gateway = DataRowHelper.ConvertToString(phoneSystem[columnGateway]),
                                           AdminUserName = DataRowHelper.ConvertToString(phoneSystem[columnAdminUserName]),
                                           AdminPassword = DataRowHelper.ConvertToString(phoneSystem[columnAdminPassword]),
                                           OSVersion = (from DataRow osVersion in GlobalMasterDetailDT.Rows
                                                        where osVersion.Field<int>(columnOSVersionID) == DataRowHelper.ConvertToInteger(phoneSystem[columnOSVersionID])
                                                        select (new GlobalMasterDetail
                                                        {
                                                            MasterDetailID = DataRowHelper.ConvertToInteger(osVersion[columnOSVersionID]),
                                                            MasterValue = DataRowHelper.ConvertToString(osVersion[columnOSVersionName])
                                                        })).FirstOrDefault(),
                                           Firmware = DataRowHelper.ConvertToString(phoneSystem[columnFirmware]),

                                           PhoneSystemInterfaces = DataRowHelper.ConvertToString(phoneSystem[columnPhoneSystemInterfaces]),
                                           PhoneSystemModules = DataRowHelper.ConvertToString(phoneSystem[columnPhoneSystemModules]),
                                           PhoneSystemModuleList = (from DataRow phoneSystemModule in phoneSystemModulesDT.Rows
                                                                    where phoneSystemModule.Field<int>(columnPhoneSystemID) == DataRowHelper.ConvertToInteger(phoneSystem[columnPhoneSystemID])
                                                                    select (new PhoneSystemModule
                                                                {
                                                                    PhoneSystemID = DataRowHelper.ConvertToInteger(phoneSystemModule[columnPhoneSystemID]),
                                                                    PhoneSystemModuleID = DataRowHelper.ConvertToInteger(phoneSystemModule[columnModuleID]),
                                                                    Module = (from DataRow module in GlobalMasterDetailDT.Rows
                                                                              where module.Field<int>(columnModuleID) == DataRowHelper.ConvertToInteger(phoneSystemModule[columnModuleID])
                                                                              select (new GlobalMasterDetail
                                                                              {
                                                                                  MasterDetailID = DataRowHelper.ConvertToInteger(module[columnModuleID]),
                                                                                  MasterValue = DataRowHelper.ConvertToString(module[columnModuleName], "")
                                                                              })).FirstOrDefault()
                                                                })).ToList(),
                                           PhoneSystemAssignedUsers = DataRowHelper.ConvertToString(phoneSystem[columnPhoneSystemAssignedUsers]),
                                           PhoneSystemAssignedUserList = (from DataRow phoneSystemAssignedUser in UserDetailDT.Rows
                                                                          where phoneSystemAssignedUser.Field<int>(columnPhoneSystemID) == DataRowHelper.ConvertToInteger(phoneSystem[columnPhoneSystemID])
                                                                          select (new AssignedUser
                                                                          {
                                                                              ClientID = DataRowHelper.ConvertToInteger(phoneSystemAssignedUser[columnPhoneSystemID]),
                                                                              User = (from DataRow user in UserDetailDT.Rows
                                                                                      //where user.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(phoneSystemAssignedUser[columnPhoneSystemID])
                                                                                      select (new User
                                                                                      {
                                                                                          UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                                                                          FirstName = DataRowHelper.ConvertToString(user[columnFirstName]),
                                                                                          UserName = DataRowHelper.ConvertToString(user[columnUserName])
                                                                                      })).FirstOrDefault(),
                                                                              System = (from DataRow systemMaster in UserDetailDT.Rows
                                                                                        //where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(phoneSystemAssignedUser[columnSystemMasterID])
                                                                                        select (new SystemMaster
                                                                                        {
                                                                                            SystemMasterID = DataRowHelper.ConvertToInteger(phoneSystemAssignedUser[columnSystemMasterID]),
                                                                                            SystemMasterName = DataRowHelper.ConvertToString(phoneSystemAssignedUser[columnSystemMasterName])
                                                                                        })).FirstOrDefault(),

                                                                          })).ToList(),

                                           PhoneSystemNotes = DataRowHelper.ConvertToString(phoneSystem[columnPhoneSystemNotes]),
                                           ViewDocumentPath = DataRowHelper.ConvertToString(phoneSystem[columnDocumentPath]),
                                           //This Static binding is for Showing the View Col in Jq Grid
                                           View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Phone%20System&id=" + DataRowHelper.ConvertToString(phoneSystem[columnPhoneSystemID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                       }).ToList();
                }
            }
            return phoneSystemList;
        }
        #endregion [ ConvertToObject ]
    }
}
