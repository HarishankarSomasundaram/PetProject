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
    internal class WorkStationInfoDAL
    {
        #region [ Declarations ]
        private List<WorkStationInfo> workStationInfoList;
        private WorkStationInfo workStationInfo;
        private List<SystemHardDrive> systemHardDriveList;
        private SystemHardDrive systemHardDrive;
        #region [Colunm Attributes]
        private readonly string columnWorkStationInfoID = "WorkStationID";
        private readonly string columnManuf = "Manufacture";
        private readonly string columnHostName = "HostName";
        private readonly string columnWorkStationInfoModelID = "WorkStationInfoModel";
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

        private readonly string columnWorkStationModelID = "WorkStationModelID";
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
        private readonly string columnWorkStationHardwareID = "WorkStationHardwareID";
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
        internal WorkStationInfoDAL()
        {
        }
        #endregion [ Constructor ]

        #region [ Add WorkStationInfo ]
        public WorkStationInfo AddWorkStationInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.HostName);

                parameters[1] = new SqlParameter("@InstalledDate", SqlDbType.DateTime);
                parameters[1].Value = DBValueHelper.ConvertToDBDate(request.WorkStationInfo.InstalledDate);

                parameters[2] = new SqlParameter("@WorkStationModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.WorkStationModelID);

                parameters[3] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.SerialNumber);

                parameters[4] = new SqlParameter("@WarrantyExpires", SqlDbType.DateTime);
                parameters[4].Value = DBValueHelper.ConvertToDBDate(request.WorkStationInfo.WarrantyExpires);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.IPAddress);

                parameters[6] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Subnet);

                parameters[7] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Gateway);

                parameters[8] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.AdminUserName);

                parameters[9] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Password);

                parameters[10] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Domain);

                parameters[12] = new SqlParameter("@OperatingSystemID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.OperatingSystemID);

                parameters[13] = new SqlParameter("@OperatingSystemLicenseKey", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.OperatingSystemLicenseKey);

                parameters[15] = new SqlParameter("@AntiVirusID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.AntiVirusID);

                parameters[16] = new SqlParameter("@AntiVirusLicenseKey", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.AntiVirusLicenseKey);

                parameters[19] = new SqlParameter("@BackSoftwareID", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationBackupIDs);

                parameters[20] = new SqlParameter("@ApplicationSoftware", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationApplicationIDs);

                parameters[21] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationAssignedUserIDs);

                parameters[22] = new SqlParameter("@WorkStationRole", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationRoleIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.CreatedBy);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.CreatedBy);

                parameters[23] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[23].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.SiteID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationInfoAdd, parameters);
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
                return request.WorkStationInfo;

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
        #endregion [ Add WorkStationInfo ]

        #region [ Update WorkStationInfo ]
        public WorkStationInfo ModifyWorkStationInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.HostName);

                parameters[1] = new SqlParameter("@InstalledDate", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.InstalledDate);

                parameters[2] = new SqlParameter("@WorkStationModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.WorkStationModelID);

                parameters[3] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.SerialNumber);

                parameters[4] = new SqlParameter("@WarrantyExpires", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WarrantyExpires);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.IPAddress);

                parameters[6] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Subnet);

                parameters[7] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Gateway);

                parameters[8] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.AdminUserName);

                parameters[9] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Password);

                parameters[10] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.Domain);

                parameters[12] = new SqlParameter("@OperatingSystemID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.OperatingSystemID);

                parameters[13] = new SqlParameter("@OperatingSystemLicenseKey", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.OperatingSystemLicenseKey);

                parameters[15] = new SqlParameter("@AntiVirusID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.AntiVirusID);

                parameters[16] = new SqlParameter("@AntiVirusLicenseKey", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.AntiVirusLicenseKey);

                parameters[19] = new SqlParameter("@BackSoftwareID", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationBackupIDs);

                parameters[20] = new SqlParameter("@ApplicationSoftware", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationApplicationIDs);

                parameters[21] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationAssignedUserIDs);

                parameters[22] = new SqlParameter("@WorkStationRole", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.WorkStationRoleIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.WorkStationInfo.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.CreatedBy);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.CreatedBy);

                parameters[23] = new SqlParameter("@WorkStationID", SqlDbType.Int);
                parameters[23].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationInfo.WorkStationID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationInfoUpdateByWorkStationInfoID, parameters);
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
                return request.WorkStationInfo;

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
        #endregion [ Update WorkStationInfo ]

        #region [ Delete WorkStationInfo ]
        internal bool DeleteWorkStationInfoByWorkStationInfoID(int WorkStationInfoID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@WorkStationInfoID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(WorkStationInfoID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationInfoDeleteByWorkStationInfoID, parameters);
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
        #endregion [ Delete WorkStationInfo ]

        #region [Get All WorkStationInfo]
        public List<WorkStationInfo> GetAllWorkStationInfo(int SiteID, string searchFilter)
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
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationInfo_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                //reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationInfo_List,parameters);
                //if (reader != null)
                //{
                //    return ProcessDataReader(reader);
                //}
                return workStationInfoList;
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

        public WorkStationInfo GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(int WorkStationInfoID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@WorkStationID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(WorkStationInfoID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationInfoByWorkStationInfoID_List, parameters);
                if (ds != null)
                {
                    //workStationInfo = ConvertAllUserAttributesToObject(ds);
                    workStationInfoList = ProcessDataSet(ds);
                }
                return workStationInfoList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion

        #region [ ProcessDataSet]
        //Parses the data reader and converts to object
        private List<WorkStationInfo> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        #region [ private methods ]
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

        private List<WorkStationInfo> ConvertToObject(DataSet ds)
        {
            workStationInfoList = new List<WorkStationInfo>();

            DataTable workStationDT = new DataTable();
            DataTable notesDetailsDT = new DataTable();
            DataTable systemBackupSoftwaresDT = new DataTable();
            DataTable systemApplicationsDT = new DataTable();
            DataTable systemRoleDT = new DataTable();
            DataTable systemAssignedUsersDT = new DataTable();
            DataTable globalMasterDetailDT = new DataTable();
            DataTable workStationHardwaresDT = new DataTable();
            DataTable userDT = new DataTable();
            DataTable systemMasterDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    workStationDT = ds.Tables[0];
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
                    workStationHardwaresDT = ds.Tables[7];
                if (ds.Tables[8] != null)
                    userDT = ds.Tables[8];
                if (ds.Tables[9] != null)
                    systemMasterDT = ds.Tables[9];


                if (workStationDT.Rows.Count > 0)
                {
                    workStationInfoList = (from DataRow workStation in workStationDT.Rows

                                      select new WorkStationInfo
                                      {
                                          WorkStationID = DataRowHelper.ConvertToInteger(workStation[columnWorkStationInfoID]),
                                          HostName = DataRowHelper.ConvertToString(workStation[columnHostName]),
                                          SerialNumber = DataRowHelper.ConvertToString(workStation[columnSerialNumber]),
                                          WorkStationModelName = DataRowHelper.ConvertToString(workStation[columnModel]),
                                          OperationSystemName = DataRowHelper.ConvertToString(workStation[columnOS]),
                                          Manufacturer = DataRowHelper.ConvertToString(workStation[columnManuf]),
                                          Core = DataRowHelper.ConvertToString(workStation[columnCore]),
                                          IPAddress = DataRowHelper.ConvertToString(workStation[columnIPAddress]),
                                          ProcessorName = DataRowHelper.ConvertToString(workStation[columnProcessor]),
                                          InstalledDate = DataRowHelper.ConvertToString(workStation[columnInstalledDate]),
                                          WorkStationBackupIDs = DataRowHelper.ConvertToString(workStation[columnBackupSoftware]),
                                          WorkStationAssignedUserIDs = DataRowHelper.ConvertToString(workStation[columnAssignedUser]),
                                          WorkStationApplicationIDs = DataRowHelper.ConvertToString(workStation[columnApplicationSoftware]),
                                          WorkStationRoleIDs = DataRowHelper.ConvertToString(workStation[columnSystemRole]),
                                          FullNotes = DataRowHelper.ConvertToString(workStation[columnNotes]),
                                          View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=WorkStations&opp=S&id=" + ConvertHelper.ConvertToString(workStation[columnWorkStationInfoID]) + " style='color: blue;text-decoration: underline;'>More</a>"),
                                          StatusID = DataRowHelper.ConvertToInteger(workStation[columnStatusID]),
                                          CreatedBy = DataRowHelper.ConvertToInteger(workStation[columnCreatedBy]),
                                          CreatedOn = DataRowHelper.ConvertToDateTime(workStation[columnCreatedOn]),
                                          ModifiedBy = DataRowHelper.ConvertToInteger(workStation[columnModifiedBy]),
                                          ModifiedOn = DataRowHelper.ConvertToDateTime(workStation[columnModifiedOn]),
                                          WarrantyExpires = DataRowHelper.ConvertToString(workStation[columnWarrantyExpires]),

                                          WorkStationModelID = DataRowHelper.ConvertToInteger(workStation[columnWorkStationModelID]),

                                          OperatingSystemID = DataRowHelper.ConvertToInteger(workStation[columnOperatingSystemID]),
                                          OperatingSystemLicenseKey = DataRowHelper.ConvertToString(workStation[columnOperatingSystemLicenseKey]),

                                          Subnet = DataRowHelper.ConvertToString(workStation[columnSubnet]),
                                          Gateway = DataRowHelper.ConvertToString(workStation[columnGateway]),
                                          AdminUserName = DataRowHelper.ConvertToString(workStation[columnAdminUserName]),
                                          Password = DataRowHelper.ConvertToString(workStation[columnPassword]),
                                          Domain = DataRowHelper.ConvertToString(workStation[columnDomain]),

                                          AntiVirusID = DataRowHelper.ConvertToInteger(workStation[columnAntiVirusID]),
                                          AntiVirusLicenseKey = DataRowHelper.ConvertToString(workStation[columnAntiVirusLicenseKey]),



                                          AntiVirus = (from DataRow antiVirus in globalMasterDetailDT.Rows
                                                       where antiVirus.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStation[columnAntiVirusID])
                                                       select (new GlobalMasterDetail
                                                       {
                                                           MasterDetailID = DataRowHelper.ConvertToInteger(antiVirus[columnMasterDetailID]),
                                                           MasterValue = DataRowHelper.ConvertToString(antiVirus[columnMasterValue])
                                                       })).FirstOrDefault(),

                                          OperationSystem = (from DataRow operationSystem in globalMasterDetailDT.Rows
                                                             where operationSystem.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStation[columnOperatingSystemID])
                                                             select (new GlobalMasterDetail
                                                             {
                                                                 MasterDetailID = DataRowHelper.ConvertToInteger(operationSystem[columnMasterDetailID]),
                                                                 MasterValue = DataRowHelper.ConvertToString(operationSystem[columnMasterValue])
                                                             })).FirstOrDefault(),

                                          WorkStationBackup = (from DataRow workStationBackup in systemBackupSoftwaresDT.Rows
                                                          where  workStationBackup.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(workStation[columnWorkStationInfoID])
                                                          select (new SystemBackup
                                                          {
                                                              System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                        where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(workStationBackup[columnSystemID])
                                                                        select (new SystemMaster
                                                                        {
                                                                            SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                            SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                        })).FirstOrDefault(),
                                                              ClientID = DataRowHelper.ConvertToInteger(workStationBackup[columnClientID]),
                                                              LicenseKey = DataRowHelper.ConvertToString(workStationBackup[columnLicenseKey]),
                                                              BackupSoftware = (from DataRow backupSoftware in globalMasterDetailDT.Rows
                                                                                where backupSoftware.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStationBackup[columnBackupSoftwareID])
                                                                                select (new GlobalMasterDetail
                                                                                {
                                                                                    MasterDetailID = DataRowHelper.ConvertToInteger(backupSoftware[columnMasterDetailID]),
                                                                                    MasterValue = DataRowHelper.ConvertToString(backupSoftware[columnMasterValue])
                                                                                })).FirstOrDefault(),
                                                          })).ToList(),

                                          WorkStationApplication = (from DataRow workStationApplication in systemApplicationsDT.Rows
                                                               where  workStationApplication.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(workStation[columnWorkStationInfoID])
                                                               select (new SystemApplication
                                                               {
                                                                   System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                             where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(workStationApplication[columnSystemID])
                                                                             select (new SystemMaster
                                                                             {
                                                                                 SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                 SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                             })).FirstOrDefault(),
                                                                   ClientID = DataRowHelper.ConvertToInteger(workStationApplication[columnClientID]),
                                                                   LicenseKey = DataRowHelper.ConvertToString(workStationApplication[columnLicenseKey]),
                                                                   Application = (from DataRow application in globalMasterDetailDT.Rows
                                                                                  where application.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStationApplication[columnApplicationID])
                                                                                  select (new GlobalMasterDetail
                                                                                  {
                                                                                      MasterDetailID = DataRowHelper.ConvertToInteger(application[columnMasterDetailID]),
                                                                                      MasterValue = DataRowHelper.ConvertToString(application[columnMasterValue])
                                                                                  })).FirstOrDefault(),
                                                               })).ToList(),


                                          WorkStationRole = (from DataRow workStationRole in systemRoleDT.Rows
                                                        where workStationRole.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(workStation[columnWorkStationInfoID])
                                                        select (new SystemRole
                                                        {
                                                            System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                      where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(workStationRole[columnSystemID])
                                                                      select (new SystemMaster
                                                                      {
                                                                          SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                          SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                      })).FirstOrDefault(),
                                                            ClientID = DataRowHelper.ConvertToInteger(workStationRole[columnClientID]),
                                                            Role = (from DataRow role in globalMasterDetailDT.Rows
                                                                    where role.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStationRole[columnRoleID])
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
                                                                          notesDetail.Field<int>(columnNotesClientID) == DataRowHelper.ConvertToInteger(workStation[columnWorkStationInfoID])
                                                                          select (new NotesDetail
                                                                          {
                                                                              NotesDetailID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesDetailID]),
                                                                              NotesMasterID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesMasterID]),
                                                                              NotesClientID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesClientID]),
                                                                              Notes = DataRowHelper.ConvertToString(notesDetail[columnNotes])
                                                                          })).ToList(),
                                                   })).FirstOrDefault(),



                                          WorkStationAssignedUser = (from DataRow workStationAssignedUser in systemAssignedUsersDT.Rows
                                                                     where workStationAssignedUser.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(workStation[columnWorkStationInfoID])
                                                                     select (new AssignedUser
                                                                     {
                                                                         ClientID = DataRowHelper.ConvertToInteger(workStationAssignedUser[columnSystemID]),
                                                                         User = (from DataRow user in userDT.Rows
                                                                                 where user.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(workStationAssignedUser[columnUserID])
                                                                                 select (new User
                                                                                 {
                                                                                     UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                                                                     FirstName = DataRowHelper.ConvertToString(user[columnFirstName]),
                                                                                LastName = DataRowHelper.ConvertToString(user[columnLastName]),
                                                                                UserName = DataRowHelper.ConvertToString(user[columnUserName])
                                                                                 })).FirstOrDefault(),
                                                                         System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                                   where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(workStationAssignedUser[columnSystemID])
                                                                                   select (new SystemMaster
                                                                                   {
                                                                                       SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                       SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                                   })).FirstOrDefault(),

                                                                     })).ToList(),
                                          WorkStationModel = (from DataRow workStationModel in workStationHardwaresDT.Rows
                                                         where workStationModel.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStation[columnWorkStationModelID])
                                                         select (new WorkStationHardware
                                                         {
                                                             WorkStationHardwareID = DataRowHelper.ConvertToInteger(workStationModel[columnWorkStationHardwareID]),
                                                             ModelName = DataRowHelper.ConvertToString(workStationModel[columnModel]),

                                                             HostName = DataRowHelper.ConvertToString(workStationModel[columnHostName]),

                                                             SerialNumber = DataRowHelper.ConvertToString(workStationModel[columnSerialNumber]),
                                                             Core = DataRowHelper.ConvertToInteger(workStationModel[columnCore]),
                                                             Manufacturer = DataRowHelper.ConvertToString(workStationModel[columnManufacturer])

                                                         })).FirstOrDefault(),


                                      }).ToList();
                }
            }

            return workStationInfoList;
        }


        #endregion
    }
}
