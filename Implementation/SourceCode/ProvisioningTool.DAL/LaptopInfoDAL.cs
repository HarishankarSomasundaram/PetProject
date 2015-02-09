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
    internal class LaptopInfoDAL
    {
        #region [ Declarations ]
        private List<LaptopInfo> laptopInfoList;
        //private LaptopInfo laptopInfo;
        private List<SystemHardDrive> systemHardDriveList;
        private SystemHardDrive systemHardDrive;
        #region [Colunm Attributes]
        private readonly string columnLaptopInfoID = "LaptopID";
        private readonly string columnManuf = "Manufacture";
        private readonly string columnHostName = "HostName";
        private readonly string columnModel = "Model";
        private readonly string columnInstalledDate = "InstalledDate";
        private readonly string columnSerialNumber = "SerialNumber";
        private readonly string columnProcessor = "Processor";
        private readonly string columnCore = "Core";
        private readonly string columnOS = "OS";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnNotes = "Notes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnMasterDetailID = "MasterDetailID";
        private readonly string columnMasterValue = "MasterValue";

        private readonly string columnLaptopModelID = "LaptopModelID";
        private readonly string columnWarrantyExpires = "WarrantyExpires";
        private readonly string columnSubnet = "Subnet";
        private readonly string columnGateway = "Gateway";
        private readonly string columnAdminUserName = "AdminUserName";
        private readonly string columnPassword = "Password";
        private readonly string columnDomain = "Domain";
        private readonly string columnOperatingSystemID = "OperatingSystemID";
        private readonly string columnOperatingSystemLicenseKey = "OperatingSystemLicenseKey";
        private readonly string columnAntiVirusID = "AntiVirusID";
        private readonly string columnAntiVirusLicenseKey = "AntiVirusLicenseKey";

        private readonly string columnSystemMasterID = "SystemMasterID";
        private readonly string columnClientID = "ClientID";
        private readonly string columnSystemID = "SystemID";
        private readonly string columnApplicationID = "ApplicationID";
        private readonly string columnLicenseKey = "LicenseKey";
        private readonly string columnRoleID = "RoleID";
        private readonly string columnUserID = "UserID";
        private readonly string columnSystemMasterName = "SystemMasterName";
        private readonly string columnBackupSoftwareID = "BackupSoftwareID";
        private readonly string columnNotesDetailID = "NotesDetailID";
        private readonly string columnNotesMasterID = "NotesMasterID";
        private readonly string columnNotesClientID = "NotesClientID";
        private readonly string columnFirstName = "FirstName";
        private readonly string columnLaptopHardwareID = "LaptopHardwareID";
        private readonly string columnManufacturer = "Manufacturer";
        private readonly string columnBackupSoftware = "BackupSoftware";
        private readonly string columnAssignedUser = "AssignedUser";
        private readonly string columnApplicationSoftware = "ApplicationSoftware";
        private readonly string columnSystemRole = "SystemRole";
        private readonly string columnNotesMasterName = "NotesMasterName";
        private readonly string columnLastName = "LastName";
        private readonly string columnUserName = "UserName";

        #endregion

        #endregion [ Declarations ]

        #region [ Constructor ]
        internal LaptopInfoDAL()
        {
        }
        #endregion [ Constructor ]

        #region [ Add LaptopInfo ]
        public LaptopInfo AddLaptopInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.HostName);

                parameters[1] = new SqlParameter("@InstalledDate", SqlDbType.DateTime);
                parameters[1].Value = DBValueHelper.ConvertToDBDate(request.LaptopInfo.InstalledDate);

                parameters[2] = new SqlParameter("@LaptopModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.LaptopModelID);

                parameters[3] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.SerialNumber);

                parameters[4] = new SqlParameter("@WarrantyExpires", SqlDbType.DateTime);
                parameters[4].Value = DBValueHelper.ConvertToDBDate(request.LaptopInfo.WarrantyExpires);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.IPAddress);

                parameters[6] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Subnet);

                parameters[7] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Gateway);

                parameters[8] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.AdminUserName);

                parameters[9] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Password);

                parameters[10] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Domain);

                parameters[12] = new SqlParameter("@OperatingSystemID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.OperatingSystemID);

                parameters[13] = new SqlParameter("@OperatingSystemLicenseKey", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.OperatingSystemLicenseKey);

                parameters[15] = new SqlParameter("@AntiVirusID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.AntiVirusID);

                parameters[16] = new SqlParameter("@AntiVirusLicenseKey", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.AntiVirusLicenseKey);

                parameters[19] = new SqlParameter("@BackSoftwareID", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopBackupIDs);

                parameters[20] = new SqlParameter("@ApplicationSoftware", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopApplicationIDs);

                parameters[21] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopAssignedUserIDs);

                parameters[22] = new SqlParameter("@LaptopRole", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopRoleIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.CreatedBy);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.CreatedBy);

                parameters[23] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[23].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.SiteID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopInfoAdd, parameters);
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
                return request.LaptopInfo;

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
        #endregion [ Add LaptopInfo ]

        #region [ Update LaptopInfo ]
        public LaptopInfo ModifyLaptopInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.HostName);

                parameters[1] = new SqlParameter("@InstalledDate", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.InstalledDate);

                parameters[2] = new SqlParameter("@LaptopModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.LaptopModelID);

                parameters[3] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.SerialNumber);

                parameters[4] = new SqlParameter("@WarrantyExpires", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.WarrantyExpires);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.IPAddress);

                parameters[6] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Subnet);

                parameters[7] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Gateway);

                parameters[8] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.AdminUserName);

                parameters[9] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Password);

                parameters[10] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.Domain);

                parameters[12] = new SqlParameter("@OperatingSystemID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.OperatingSystemID);

                parameters[13] = new SqlParameter("@OperatingSystemLicenseKey", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.OperatingSystemLicenseKey);

                parameters[15] = new SqlParameter("@AntiVirusID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.AntiVirusID);

                parameters[16] = new SqlParameter("@AntiVirusLicenseKey", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.AntiVirusLicenseKey);

                parameters[19] = new SqlParameter("@BackSoftwareID", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopBackupIDs);

                parameters[20] = new SqlParameter("@ApplicationSoftware", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopApplicationIDs);

                parameters[21] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopAssignedUserIDs);

                parameters[22] = new SqlParameter("@LaptopRole", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.LaptopRoleIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.LaptopInfo.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.CreatedBy);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.CreatedBy);

                parameters[23] = new SqlParameter("@LaptopID", SqlDbType.Int);
                parameters[23].Value = DBValueHelper.ConvertToDBInteger(request.LaptopInfo.LaptopID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopInfoUpdateByLaptopInfoID, parameters);
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
                return request.LaptopInfo;

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
        #endregion [ Update LaptopInfo ]

        #region [ Delete LaptopInfo ]
        internal bool DeleteLaptopInfoByLaptopInfoID(int LaptopInfoID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@LaptopInfoID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(LaptopInfoID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopInfoDeleteByLaptopInfoID, parameters);
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
        #endregion [ Delete LaptopInfo ]

        #region [Get All LaptopInfo]
        public List<LaptopInfo> GetAllLaptopInfo(int SiteID, string searchFilter)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);
            DataSet ds = new DataSet();
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
            parameters[0].Value = DBValueHelper.ConvertToDBInt(SiteID);
            parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
            parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);
            
            SqlDataReader reader = null;
            try
            {
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopInfo_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
               //// reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopInfo_List,parameters);
               // if (reader != null)
               // {
               //    // return ProcessDataReader(reader);
               // }
                return laptopInfoList;
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

        #region [Get All HardDisk]
        public List<SystemHardDrive> GetAllHardDisk()
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);

            SqlDataReader reader = null;
            try
            {
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGetAllHardDiskDetails);
                if (reader != null)
                {
                    return ProcessDataReaderDrive(reader);
                }
                return systemHardDriveList;
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

        #region [Get User And User Attribute Details By UserID]

        public LaptopInfo GetLaptopHardwarAndUserDetailsByLaptopHardwarID(int LaptopInfoID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@LaptopID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(LaptopInfoID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopInfoByLaptopInfoID_List, parameters);
                if (ds != null)
                {
                    //laptopInfo = ConvertAllUserAttributesToObject(ds);
                    laptopInfoList = ProcessDataSet(ds);
                }
                return laptopInfoList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion

        #region [ private methods ]

        #region [ ProcessDataSet]
        //Parses the data reader and converts to object
        private List<LaptopInfo> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        ////Parses the data reader and converts to object
        private List<SystemHardDrive> ProcessDataReaderDrive(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                systemHardDriveList = new List<SystemHardDrive>();
                while (reader.Read())
                    systemHardDriveList.Add(ConvertToObjects(reader));
                return systemHardDriveList;
            }
            return null;
        }

        //Converts each data record into object
        private SystemHardDrive ConvertToObjects(IDataRecord dataRecord)
        {
            systemHardDrive = new SystemHardDrive();


            systemHardDrive.SystemID = DataRowHelper.ConvertToInteger(dataRecord, "SystemHardDiskID");
            systemHardDrive.HardDriveDetails = DataRowHelper.ConvertToString(dataRecord, "DriveDetails");


            return systemHardDrive;
        }

        private List<LaptopInfo> ConvertToObject(DataSet ds)
        {
            laptopInfoList = new List<LaptopInfo>();

            DataTable laptopDT = new DataTable();
            DataTable notesDetailsDT = new DataTable();
            DataTable systemBackupSoftwaresDT = new DataTable();
            DataTable systemApplicationsDT = new DataTable();
            DataTable systemRoleDT = new DataTable();
            DataTable systemAssignedUsersDT = new DataTable();
            DataTable globalMasterDetailDT = new DataTable();
            DataTable laptopHardwaresDT = new DataTable();
            DataTable userDT = new DataTable();
            DataTable systemMasterDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    laptopDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    notesDetailsDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    systemBackupSoftwaresDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    systemApplicationsDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    systemRoleDT = ds.Tables[4];
                if (ds.Tables[5] != null)
                    systemAssignedUsersDT = ds.Tables[5];
                if (ds.Tables[6] != null)
                    globalMasterDetailDT = ds.Tables[6];
                if (ds.Tables[7] != null)
                    laptopHardwaresDT = ds.Tables[7];
                if (ds.Tables[8] != null)
                    userDT = ds.Tables[8]; 
                if (ds.Tables[9] != null)
                    systemMasterDT = ds.Tables[9];


                if (laptopDT.Rows.Count > 0)
                {
                    laptopInfoList = (from DataRow laptop in laptopDT.Rows

                                      select new LaptopInfo
                                      {
                                          LaptopID = DataRowHelper.ConvertToInteger(laptop[columnLaptopInfoID]),
                                          HostName = DataRowHelper.ConvertToString(laptop[columnHostName]),
                                          SerialNumber = DataRowHelper.ConvertToString(laptop[columnSerialNumber]),
                                          LaptopModelName = DataRowHelper.ConvertToString(laptop[columnModel]),
                                          OperationSystemName = DataRowHelper.ConvertToString(laptop[columnOS]),
                                          Manufacturer = DataRowHelper.ConvertToString(laptop[columnManuf]),
                                          Core = DataRowHelper.ConvertToString(laptop[columnCore]),
                                          IPAddress = DataRowHelper.ConvertToString(laptop[columnIPAddress]),
                                          ProcessorName = DataRowHelper.ConvertToString(laptop[columnProcessor]),
                                          InstalledDate = DataRowHelper.ConvertToString(laptop[columnInstalledDate]),
                                          LaptopBackupIDs = DataRowHelper.ConvertToString(laptop[columnBackupSoftware]),
                                          LaptopAssignedUserIDs = DataRowHelper.ConvertToString(laptop[columnAssignedUser]),
                                          LaptopApplicationIDs = DataRowHelper.ConvertToString(laptop[columnApplicationSoftware]),
                                          LaptopRoleIDs = DataRowHelper.ConvertToString(laptop[columnSystemRole]),
                                          FullNotes = DataRowHelper.ConvertToString(laptop[columnNotes]),
                                          View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Laptops&opp=S&id=" + ConvertHelper.ConvertToString(laptop[columnLaptopInfoID]) + " style='color: blue;text-decoration: underline;'>More</a>"),
                                          StatusID = DataRowHelper.ConvertToInteger(laptop[columnStatusID]),
                                          CreatedBy = DataRowHelper.ConvertToInteger(laptop[columnCreatedBy]),
                                          CreatedOn = DataRowHelper.ConvertToDateTime(laptop[columnCreatedOn]),
                                          ModifiedBy = DataRowHelper.ConvertToInteger(laptop[columnModifiedBy]),
                                          ModifiedOn = DataRowHelper.ConvertToDateTime(laptop[columnModifiedOn]),
                                          WarrantyExpires = DataRowHelper.ConvertToString(laptop[columnWarrantyExpires]),

                                          LaptopModelID = DataRowHelper.ConvertToInteger(laptop[columnLaptopModelID]),

                                          OperatingSystemID = DataRowHelper.ConvertToInteger(laptop[columnOperatingSystemID]),
                                          OperatingSystemLicenseKey = DataRowHelper.ConvertToString(laptop[columnOperatingSystemLicenseKey]),

                                          Subnet = DataRowHelper.ConvertToString(laptop[columnSubnet]),
                                          Gateway = DataRowHelper.ConvertToString(laptop[columnGateway]),
                                          AdminUserName = DataRowHelper.ConvertToString(laptop[columnAdminUserName]),
                                          Password = DataRowHelper.ConvertToString(laptop[columnPassword]),
                                          Domain = DataRowHelper.ConvertToString(laptop[columnDomain]),

                                          AntiVirusID = DataRowHelper.ConvertToInteger(laptop[columnAntiVirusID]),
                                          AntiVirusLicenseKey = DataRowHelper.ConvertToString(laptop[columnAntiVirusLicenseKey]),




                                          AntiVirus = (from DataRow antiVirus in globalMasterDetailDT.Rows
                                                       where antiVirus.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptop[columnAntiVirusID])
                                                       select (new GlobalMasterDetail
                                                       {
                                                           MasterDetailID = DataRowHelper.ConvertToInteger(antiVirus[columnMasterDetailID]),
                                                           MasterValue = DataRowHelper.ConvertToString(antiVirus[columnMasterValue])
                                                       })).FirstOrDefault(),

                                          OperationSystem = (from DataRow operationSystem in globalMasterDetailDT.Rows
                                                             where operationSystem.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptop[columnOperatingSystemID])
                                                             select (new GlobalMasterDetail
                                                             {
                                                                 MasterDetailID = DataRowHelper.ConvertToInteger(operationSystem[columnMasterDetailID]),
                                                                 MasterValue = DataRowHelper.ConvertToString(operationSystem[columnMasterValue])
                                                             })).FirstOrDefault(),

                                          LaptopBackup = (from DataRow laptopBackup in systemBackupSoftwaresDT.Rows
                                                          where laptopBackup.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(laptop[columnLaptopInfoID])
                                                          select (new SystemBackup
                                                          {
                                                              System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                        where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(laptopBackup[columnSystemID])
                                                                        select (new SystemMaster
                                                                        {
                                                                            SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                            SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                        })).FirstOrDefault(),
                                                              ClientID = DataRowHelper.ConvertToInteger(laptopBackup[columnClientID]),
                                                              LicenseKey = DataRowHelper.ConvertToString(laptopBackup[columnLicenseKey]),
                                                              BackupSoftware = (from DataRow backupSoftware in globalMasterDetailDT.Rows
                                                                                where backupSoftware.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptopBackup[columnBackupSoftwareID])
                                                                                select (new GlobalMasterDetail
                                                                                {
                                                                                    MasterDetailID = DataRowHelper.ConvertToInteger(backupSoftware[columnMasterDetailID]),
                                                                                    MasterValue = DataRowHelper.ConvertToString(backupSoftware[columnMasterValue])
                                                                                })).FirstOrDefault(),
                                                          })).ToList(),

                                          LaptopApplication = (from DataRow laptopApplication in systemApplicationsDT.Rows
                                                               where laptopApplication.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(laptop[columnLaptopInfoID])
                                                               select (new SystemApplication
                                                               {
                                                                   System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                             where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(laptopApplication[columnSystemID])
                                                                             select (new SystemMaster
                                                                             {
                                                                                 SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                 SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                             })).FirstOrDefault(),
                                                                   ClientID = DataRowHelper.ConvertToInteger(laptopApplication[columnClientID]),
                                                                   LicenseKey = DataRowHelper.ConvertToString(laptopApplication[columnLicenseKey]),
                                                                   Application = (from DataRow application in globalMasterDetailDT.Rows
                                                                                  where application.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptopApplication[columnApplicationID])
                                                                                  select (new GlobalMasterDetail
                                                                                  {
                                                                                      MasterDetailID = DataRowHelper.ConvertToInteger(application[columnMasterDetailID]),
                                                                                      MasterValue = DataRowHelper.ConvertToString(application[columnMasterValue])
                                                                                  })).FirstOrDefault(),
                                                               })).ToList(),


                                          LaptopRole = (from DataRow laptopRole in systemRoleDT.Rows
                                                        where laptopRole.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(laptop[columnLaptopInfoID])
                                                        select (new SystemRole
                                                        {
                                                            System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                      where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(laptopRole[columnSystemID])
                                                                      select (new SystemMaster
                                                                      {
                                                                          SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                          SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                      })).FirstOrDefault(),
                                                            ClientID = DataRowHelper.ConvertToInteger(laptopRole[columnClientID]),
                                                            Role = (from DataRow role in globalMasterDetailDT.Rows
                                                                    where role.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptopRole[columnRoleID])
                                                                    select (new GlobalMasterDetail
                                                                    {
                                                                        MasterDetailID = DataRowHelper.ConvertToInteger(role[columnMasterDetailID]),
                                                                        MasterValue = DataRowHelper.ConvertToString(role[columnMasterValue])
                                                                    })).FirstOrDefault(),
                                                        })).ToList(),


                                          Notes = (from DataRow notes in notesDetailsDT.Rows
                                                   select (new NotesMaster
                                                   {
                                                       NotesMasterID = DataRowHelper.ConvertToInteger(notes[columnNotesMasterID]),
                                                       NotesMasterName = DataRowHelper.ConvertToString(notes[columnNotesMasterName]),
                                                       NotesDetailList = (from DataRow notesDetail in notesDetailsDT.Rows
                                                                          where notesDetail.Field<int>(columnNotesMasterID) == DataRowHelper.ConvertToInteger(notes[columnNotesMasterID]) &&
                                                                          notesDetail.Field<int>(columnNotesClientID) == DataRowHelper.ConvertToInteger(laptop[columnLaptopInfoID])
                                                                          select (new NotesDetail
                                                                          {
                                                                              NotesDetailID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesDetailID]),
                                                                              NotesMasterID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesMasterID]),
                                                                              NotesClientID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesClientID]),
                                                                              Notes = DataRowHelper.ConvertToString(notesDetail[columnNotes])
                                                                          })).ToList(),
                                                   })).FirstOrDefault(),



                                          LaptopAssignedUser = (from DataRow laptopAssignedUser in systemAssignedUsersDT.Rows
                                                                where laptopAssignedUser.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(laptop[columnLaptopInfoID])
                                                                select (new AssignedUser
                                                                {
                                                                    ClientID = DataRowHelper.ConvertToInteger(laptopAssignedUser[columnSystemID]),
                                                                    User = (from DataRow user in userDT.Rows
                                                                            where user.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(laptopAssignedUser[columnUserID])
                                                                            select (new User
                                                                            {
                                                                                UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                                                                FirstName = DataRowHelper.ConvertToString(user[columnFirstName]),
                                                                                LastName = DataRowHelper.ConvertToString(user[columnLastName]),
                                                                                UserName = DataRowHelper.ConvertToString(user[columnUserName])
                                                                            })).FirstOrDefault(),
                                                                    System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                              where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(laptopAssignedUser[columnSystemID])
                                                                              select (new SystemMaster
                                                                              {
                                                                                  SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                  SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                              })).FirstOrDefault(),

                                                                })).ToList(),
                                          LaptopModel = (from DataRow laptopModel in laptopHardwaresDT.Rows
                                                         where laptopModel.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptop[columnLaptopModelID])
                                                         select (new LaptopHardware
                                                         {
                                                             LaptopHardwareID = DataRowHelper.ConvertToInteger(laptopModel[columnLaptopHardwareID]),
                                                             ModelName = DataRowHelper.ConvertToString(laptopModel[columnModel]),

                                                             HostName = DataRowHelper.ConvertToString(laptopModel[columnHostName]),

                                                             SerialNumber = DataRowHelper.ConvertToString(laptopModel[columnSerialNumber]),
                                                             Core = DataRowHelper.ConvertToInteger(laptopModel[columnCore]),
                                                             Manufacturer = DataRowHelper.ConvertToString(laptopModel[columnManufacturer])

                                                         })).FirstOrDefault(),


                                      }).ToList();
                }
            }

            return laptopInfoList;
        }


        #endregion
    }
}
