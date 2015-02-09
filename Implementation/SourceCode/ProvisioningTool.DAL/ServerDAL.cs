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
    internal class ServerInfoDAL
    {
        #region [ Declarations ]
        private List<ServerInfo> serverInfoList;
        //private ServerInfo serverInfo;
        private List<SystemHardDrive> systemHardDriveList;
        private SystemHardDrive systemHardDrive;
        #region [Colunm Attributes]
        private readonly string columnServerInfoID = "ServerID";
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

        private readonly string columnServerModelID = "ServerModelID";
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
        private readonly string columnServerHardwareID = "ServerHardwareID";
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
        internal ServerInfoDAL()
        {
        }
        #endregion [ Constructor ]

        #region [ Add ServerInfo ]
        public ServerInfo AddServerInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.HostName);

                parameters[1] = new SqlParameter("@InstalledDate", SqlDbType.DateTime);
                parameters[1].Value = DBValueHelper.ConvertToDBDate(request.ServerInfo.InstalledDate);

                parameters[2] = new SqlParameter("@ServerModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.ServerModelID);

                parameters[3] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.SerialNumber);

                parameters[4] = new SqlParameter("@WarrantyExpires", SqlDbType.DateTime);
                parameters[4].Value = DBValueHelper.ConvertToDBDate(request.ServerInfo.WarrantyExpires);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.IPAddress);

                parameters[6] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Subnet);

                parameters[7] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Gateway);

                parameters[8] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.AdminUserName);

                parameters[9] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Password);

                parameters[10] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Domain);

                parameters[12] = new SqlParameter("@OperatingSystemID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.OperatingSystemID);

                parameters[13] = new SqlParameter("@OperatingSystemLicenseKey", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.OperatingSystemLicenseKey);

                parameters[15] = new SqlParameter("@AntiVirusID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.AntiVirusID);

                parameters[16] = new SqlParameter("@AntiVirusLicenseKey", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.AntiVirusLicenseKey);

                parameters[19] = new SqlParameter("@BackSoftwareID", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerBackupIDs);

                parameters[20] = new SqlParameter("@ApplicationSoftware", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerApplicationIDs);

                parameters[21] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerAssignedUserIDs);

                parameters[22] = new SqlParameter("@ServerRole", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerRoleIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.CreatedBy);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.CreatedBy);

                parameters[23] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[23].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.SiteID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerInfoAdd, parameters);
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
                return request.ServerInfo;

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
        #endregion [ Add ServerInfo ]

        #region [ Update ServerInfo ]
        public ServerInfo ModifyServerInfo(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.HostName);

                parameters[1] = new SqlParameter("@InstalledDate", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.InstalledDate);

                parameters[2] = new SqlParameter("@ServerModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.ServerModelID);

                parameters[3] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.SerialNumber);

                parameters[4] = new SqlParameter("@WarrantyExpires", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.WarrantyExpires);

                parameters[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.IPAddress);

                parameters[6] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Subnet);

                parameters[7] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Gateway);

                parameters[8] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.AdminUserName);

                parameters[9] = new SqlParameter("@Password", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Password);

                parameters[10] = new SqlParameter("@Domain", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.Domain);

                parameters[12] = new SqlParameter("@OperatingSystemID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.OperatingSystemID);

                parameters[13] = new SqlParameter("@OperatingSystemLicenseKey", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.OperatingSystemLicenseKey);

                parameters[15] = new SqlParameter("@AntiVirusID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.AntiVirusID);

                parameters[16] = new SqlParameter("@AntiVirusLicenseKey", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.AntiVirusLicenseKey);

                parameters[19] = new SqlParameter("@BackSoftwareID", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerBackupIDs);

                parameters[20] = new SqlParameter("@ApplicationSoftware", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerApplicationIDs);

                parameters[21] = new SqlParameter("@AssignedUser", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerAssignedUserIDs);

                parameters[22] = new SqlParameter("@ServerRole", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.ServerRoleIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.ServerInfo.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.StatusID);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.CreatedBy);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.CreatedBy);

                parameters[23] = new SqlParameter("@ServerID", SqlDbType.Int);
                parameters[23].Value = DBValueHelper.ConvertToDBInteger(request.ServerInfo.ServerID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerInfoUpdateByServerInfoID, parameters);
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
                return request.ServerInfo;

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
        #endregion [ Update ServerInfo ]

        #region [ Delete ServerInfo ]
        internal bool DeleteServerInfoByServerInfoID(int ServerInfoID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@ServerInfoID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(ServerInfoID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerInfoDeleteByServerInfoID, parameters);
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
        #endregion [ Delete ServerInfo ]

        #region [Get All ServerInfo]
        public List<ServerInfo> GetAllServerInfo(int SiteID, string searchFilter)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);
            SqlDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(SiteID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);

                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerInfo_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                //if (reader != null)
                //{
                //    return ProcessDataReader(reader);
                //}
                return serverInfoList;
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

        public ServerInfo GetServerHardwarAndUserDetailsByServerHardwarID(int ServerInfoID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ServerID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(ServerInfoID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerInfoByServerInfoID_List, parameters);
                if (ds != null)
                {
                    //serverInfo = ConvertAllUserAttributesToObject(ds);
                    serverInfoList = ProcessDataSet(ds);
                }
                return serverInfoList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion

        #region [ ProcessDataSet]
        //Parses the data reader and converts to object
        private List<ServerInfo> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        #region [ private methods ]
        //Parses the data reader and converts to object
        //private List<ServerInfo> ProcessDataReader(SqlDataReader reader)
        //{
        //    if (!reader.IsClosed && reader.HasRows)
        //    {
        //        serverInfoList = new List<ServerInfo>();
        //        while (reader.Read())
        //            serverInfoList.Add(ConvertToObject(reader));
        //        return serverInfoList;
        //    }
        //    return null;
        //}

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
        //Converts each data record into object
        //private ServerInfo ConvertToObject(IDataRecord dataRecord)
        //{
        //    serverInfo = new ServerInfo();

        //    serverInfo.ServerModel = new ServerHardware();
        //    serverInfo.OperationSystem = new GlobalMaster();

        //    serverInfo.ServerID = DataRowHelper.ConvertToInteger(dataRecord, columnServerInfoID);
        //    serverInfo.HostName = DataRowHelper.ConvertToString(dataRecord, columnHostName);
        //    serverInfo.SerialNumber = DataRowHelper.ConvertToString(dataRecord, columnSerialNumber);

        //    serverInfo.ServerModelName = DataRowHelper.ConvertToString(dataRecord, columnModel);
        //    serverInfo.OperationSystemName = DataRowHelper.ConvertToString(dataRecord, columnOS);
        //    serverInfo.Manufacturer = DataRowHelper.ConvertToString(dataRecord, columnManuf);
        //    serverInfo.Core = DataRowHelper.ConvertToString(dataRecord, columnCore);
        //    serverInfo.IPAddress = DataRowHelper.ConvertToString(dataRecord, columnIPAddress);
        //    serverInfo.ProcessorName = DataRowHelper.ConvertToString(dataRecord, columnProcessor);
        //    serverInfo.InstalledDate = DataRowHelper.ConvertToDateTime(dataRecord, columnInstalledDate);
        //    //serverInfo.DisplayName = DataRowHelper.ConvertToString(dataRecord, columnDisplay);
        //    //serverInfo.MultimediaIDName = DataRowHelper.ConvertToString(dataRecord, columnMultimediaID);
        //    //serverInfo.PortName = DataRowHelper.ConvertToString(dataRecord, columnPortID);
        //    //serverInfo.SlotName = DataRowHelper.ConvertToString(dataRecord, columnSlotID);
        //    //serverInfo.ChassisName = DataRowHelper.ConvertToString(dataRecord, columnChassisD);
        //    //serverInfo.PowerName = DataRowHelper.ConvertToString(dataRecord, columnPowerID);

        //    serverInfo.FullNotes = DataRowHelper.ConvertToString(dataRecord, columnNotes);
        //    serverInfo.View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Servers&opp=S&id=" + ConvertHelper.ConvertToString(serverInfo.ServerID) + " style='color: blue;text-decoration: underline;'>More</a>");

        //    serverInfo.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
        //    serverInfo.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
        //    serverInfo.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
        //    serverInfo.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
        //    serverInfo.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);

        //    return serverInfo;
        //}

        ////Converts each data set into object
        //private ServerInfo ConvertAllUserAttributesToObject(DataSet ds)
        //{
        //    serverInfo = new ServerInfo();

        //    DataTable ServersDT = new DataTable();
        //    DataTable serverAPPDT = new DataTable();
        //    DataTable serverBackAppDT = new DataTable();
        //    DataTable serverDT = new DataTable();
        //    DataTable routerModulesDT = new DataTable();

        //    if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            serverInfo = new ServerInfo();

        //            serverInfo.ServerModel = new ServerHardware();
        //            //serverInfo.OperationSystem = new GlobalMasterde();

        //            serverInfo.ServerID = DataRowHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ServerID"]);
        //            serverInfo.HostName = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["HostName"]);
        //            serverInfo.SerialNumber = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["SerialNumber"]);

        //            serverInfo.ServerModelID = DataRowHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ServerModelID"]);
        //            serverInfo.OperatingSystemID = DataRowHelper.ConvertToInteger(ds.Tables[0].Rows[0]["OperatingSystemID"]);
        //            serverInfo.AntiVirusID = DataRowHelper.ConvertToInteger(ds.Tables[0].Rows[0]["AntiVirusID"]);
        //            serverInfo.InstalledDate = DataRowHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["InstalledDate"]);
        //            serverInfo.WarrantyExpires = DataRowHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["WarrantyExpires"]);
        //            serverInfo.IPAddress = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["IPAddress"]);
        //            serverInfo.Subnet = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["Subnet"]);
        //            serverInfo.Gateway = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["Gateway"]);
        //            serverInfo.AdminUserName = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["AdminUserName"]);
        //            serverInfo.Password = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["Password"]);
        //            serverInfo.Domain = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["Domain"]);
        //            serverInfo.OperatingSystemLicenseKey = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["OperatingSystemLicenseKey"]);
        //            serverInfo.AntiVirusLicenseKey = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["AntiVirusLicenseKey"]);
        //            serverInfo.ServerBackupIDs = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["BackupSoftware"]);
        //            serverInfo.ServerAssignedUserIDs = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["AssignedUser"]);
        //            serverInfo.ServerApplicationIDs = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["ApplicationSoftware"]);
        //            serverInfo.ServerRoleIDs = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0]["SystemRole"]);

        //            serverInfo.FullNotes = DataRowHelper.ConvertToString(ds.Tables[0].Rows[0][columnNotes]);

        //            serverInfo.StatusID = DataRowHelper.ConvertToInteger(ds.Tables[0].Rows[0]["StatusID"]);
        //            serverInfo.CreatedBy = DataRowHelper.ConvertToInteger(ds.Tables[0].Rows[0]["CreatedBy"]);
        //            serverInfo.CreatedOn = DataRowHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
        //            serverInfo.ModifiedBy = DataRowHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ModifiedBy"]);
        //            serverInfo.ModifiedOn = DataRowHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["ModifiedOn"]);
        //        }
        //    }

        //    return serverInfo;
        //}

        private List<ServerInfo> ConvertToObject(DataSet ds)
        {
            serverInfoList = new List<ServerInfo>();

            DataTable serverDT = new DataTable();
            DataTable notesDetailsDT = new DataTable();
            DataTable systemBackupSoftwaresDT = new DataTable();
            DataTable systemApplicationsDT = new DataTable();
            DataTable systemRoleDT = new DataTable();
            DataTable systemAssignedUsersDT = new DataTable();
            DataTable globalMasterDetailDT = new DataTable();
            DataTable serverHardwaresDT = new DataTable();
            DataTable userDT = new DataTable();
            DataTable systemMasterDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    serverDT = ds.Tables[0];
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
                    serverHardwaresDT = ds.Tables[7];
                if (ds.Tables[8] != null)
                    userDT = ds.Tables[8];
                 if (ds.Tables[9] != null)
                     systemMasterDT = ds.Tables[9];
                

                if (serverDT.Rows.Count > 0)
                {
                    serverInfoList = (from DataRow server in serverDT.Rows

                                      select new ServerInfo
                                      {
                                          ServerID = DataRowHelper.ConvertToInteger(server[columnServerInfoID]),
                                          HostName = DataRowHelper.ConvertToString(server[columnHostName]),
                                          SerialNumber = DataRowHelper.ConvertToString(server[columnSerialNumber]),
                                          ServerModelName = DataRowHelper.ConvertToString(server[columnModel]),
                                          OperationSystemName = DataRowHelper.ConvertToString(server[columnOS]),
                                          Manufacturer = DataRowHelper.ConvertToString(server[columnManuf]),
                                          Core = DataRowHelper.ConvertToString(server[columnCore]),
                                          IPAddress = DataRowHelper.ConvertToString(server[columnIPAddress]),
                                          ProcessorName = DataRowHelper.ConvertToString(server[columnProcessor]),
                                          InstalledDate = DataRowHelper.ConvertToString(server[columnInstalledDate]),
                                          ServerBackupIDs = DataRowHelper.ConvertToString(server[columnBackupSoftware]),
                                          ServerAssignedUserIDs = DataRowHelper.ConvertToString(server[columnAssignedUser]),
                                          ServerApplicationIDs = DataRowHelper.ConvertToString(server[columnApplicationSoftware]),
                                          ServerRoleIDs = DataRowHelper.ConvertToString(server[columnSystemRole]),
                                          FullNotes = DataRowHelper.ConvertToString(server[columnNotes]),
                                          View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Servers&opp=S&id=" + ConvertHelper.ConvertToString(server[columnServerInfoID]) + " style='color: blue;text-decoration: underline;'>More</a>"),
                                          StatusID = DataRowHelper.ConvertToInteger(server[columnStatusID]),
                                          CreatedBy = DataRowHelper.ConvertToInteger(server[columnCreatedBy]),
                                          CreatedOn = DataRowHelper.ConvertToDateTime(server[columnCreatedOn]),
                                          ModifiedBy = DataRowHelper.ConvertToInteger(server[columnModifiedBy]),
                                          ModifiedOn = DataRowHelper.ConvertToDateTime(server[columnModifiedOn]),
                                          WarrantyExpires = DataRowHelper.ConvertToString(server[columnWarrantyExpires]),

                                          ServerModelID = DataRowHelper.ConvertToInteger(server[columnServerModelID]),

                                          OperatingSystemID = DataRowHelper.ConvertToInteger(server[columnOperatingSystemID]),
                                          OperatingSystemLicenseKey = DataRowHelper.ConvertToString(server[columnOperatingSystemLicenseKey]),

                                          Subnet = DataRowHelper.ConvertToString(server[columnSubnet]),
                                          Gateway = DataRowHelper.ConvertToString(server[columnGateway]),
                                          AdminUserName = DataRowHelper.ConvertToString(server[columnAdminUserName]),
                                          Password = DataRowHelper.ConvertToString(server[columnPassword]),
                                          Domain = DataRowHelper.ConvertToString(server[columnDomain]),

                                          AntiVirusID = DataRowHelper.ConvertToInteger(server[columnAntiVirusID]),
                                          AntiVirusLicenseKey = DataRowHelper.ConvertToString(server[columnAntiVirusLicenseKey]),



                                          AntiVirus = (from DataRow antiVirus in globalMasterDetailDT.Rows
                                                       where antiVirus.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(server[columnAntiVirusID])
                                                       select (new GlobalMasterDetail
                                                       {
                                                           MasterDetailID = DataRowHelper.ConvertToInteger(antiVirus[columnMasterDetailID]),
                                                           MasterValue = DataRowHelper.ConvertToString(antiVirus[columnMasterValue])
                                                       })).FirstOrDefault(),

                                          OperationSystem = (from DataRow operationSystem in globalMasterDetailDT.Rows
                                                             where operationSystem.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(server[columnOperatingSystemID])
                                                             select (new GlobalMasterDetail
                                                             {
                                                                 MasterDetailID = DataRowHelper.ConvertToInteger(operationSystem[columnMasterDetailID]),
                                                                 MasterValue = DataRowHelper.ConvertToString(operationSystem[columnMasterValue])
                                                             })).FirstOrDefault(),

                                          ServerBackup = (from DataRow serverBackup in systemBackupSoftwaresDT.Rows
                                                          where  serverBackup.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(server[columnServerInfoID])
                                                          select (new SystemBackup
                                                          {
                                                              System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                        where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(serverBackup[columnSystemID])
                                                                        select (new SystemMaster
                                                                        {
                                                                            SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                            SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                        })).FirstOrDefault(),
                                                              ClientID = DataRowHelper.ConvertToInteger(serverBackup[columnClientID]),
                                                              LicenseKey = DataRowHelper.ConvertToString(serverBackup[columnLicenseKey]),
                                                              BackupSoftware = (from DataRow backupSoftware in globalMasterDetailDT.Rows
                                                                                where backupSoftware.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(serverBackup[columnBackupSoftwareID])
                                                                                select (new GlobalMasterDetail
                                                                                {
                                                                                    MasterDetailID = DataRowHelper.ConvertToInteger(backupSoftware[columnMasterDetailID]),
                                                                                    MasterValue = DataRowHelper.ConvertToString(backupSoftware[columnMasterValue])
                                                                                })).FirstOrDefault(),
                                                          })).ToList(),

                                          ServerApplication = (from DataRow serverApplication in systemApplicationsDT.Rows
                                                               where  serverApplication.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(server[columnServerInfoID])
                                                               select (new SystemApplication
                                                               {
                                                                   System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                             where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(serverApplication[columnSystemID])
                                                                             select (new SystemMaster
                                                                             {
                                                                                 SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                 SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                             })).FirstOrDefault(),
                                                                   ClientID = DataRowHelper.ConvertToInteger(serverApplication[columnClientID]),
                                                                   LicenseKey = DataRowHelper.ConvertToString(serverApplication[columnLicenseKey]),
                                                                   Application = (from DataRow application in globalMasterDetailDT.Rows
                                                                                  where application.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(serverApplication[columnApplicationID])
                                                                                  select (new GlobalMasterDetail
                                                                                  {
                                                                                      MasterDetailID = DataRowHelper.ConvertToInteger(application[columnMasterDetailID]),
                                                                                      MasterValue = DataRowHelper.ConvertToString(application[columnMasterValue])
                                                                                  })).FirstOrDefault(),
                                                               })).ToList(),


                                          ServerRole = (from DataRow serverRole in systemRoleDT.Rows
                                                        where serverRole.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(server[columnServerInfoID])
                                                        select (new SystemRole
                                                        {
                                                            System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                      where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(serverRole[columnSystemID])
                                                                      select (new SystemMaster
                                                                      {
                                                                          SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                          SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                      })).FirstOrDefault(),
                                                            ClientID = DataRowHelper.ConvertToInteger(serverRole[columnClientID]),
                                                            Role = (from DataRow role in globalMasterDetailDT.Rows
                                                                    where role.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(serverRole[columnRoleID])
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
                                                                          notesDetail.Field<int>(columnNotesClientID) == DataRowHelper.ConvertToInteger(server[columnServerInfoID])
                                                                          select (new NotesDetail
                                                                          {
                                                                              NotesDetailID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesDetailID]),
                                                                              NotesMasterID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesMasterID]),
                                                                              NotesClientID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesClientID]),
                                                                              Notes = DataRowHelper.ConvertToString(notesDetail[columnNotes])
                                                                          })).ToList(),
                                                   })).FirstOrDefault(),



                                          ServerAssignedUser = (from DataRow serverAssignedUser in systemAssignedUsersDT.Rows
                                                                where serverAssignedUser.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(server[columnServerInfoID])
                                                                select (new AssignedUser
                                                                {
                                                                    ClientID = DataRowHelper.ConvertToInteger(serverAssignedUser[columnSystemID]),
                                                                    User = (from DataRow user in userDT.Rows
                                                                            where user.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(serverAssignedUser[columnUserID])
                                                                            select (new User
                                                                            {
                                                                                UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                                                                FirstName = DataRowHelper.ConvertToString(user[columnFirstName]),
                                                                                LastName = DataRowHelper.ConvertToString(user[columnLastName]),
                                                                                UserName = DataRowHelper.ConvertToString(user[columnUserName])
                                                                            })).FirstOrDefault(),
                                                                    System = (from DataRow  systemMaster in systemMasterDT.Rows
                                                                              where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(serverAssignedUser[columnSystemID])
                                                                              select (new SystemMaster
                                                                            {
                                                                                SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                            })).FirstOrDefault(),     
                                                                   
                                                                })).ToList(),
                                          ServerModel = (from DataRow serverModel in serverHardwaresDT.Rows
                                                         where serverModel.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(server[columnServerModelID])
                                                         select (new ServerHardware
                                                         {
                                                             ServerHardwareID = DataRowHelper.ConvertToInteger(serverModel[columnServerHardwareID]),
                                                             ModelName = DataRowHelper.ConvertToString(serverModel[columnModel]),

                                                             HostName = DataRowHelper.ConvertToString(serverModel[columnHostName]),

                                                             SerialNumber = DataRowHelper.ConvertToString(serverModel[columnSerialNumber]),
                                                             Core = DataRowHelper.ConvertToInteger(serverModel[columnCore]),
                                                             Manufacturer = DataRowHelper.ConvertToString(serverModel[columnManufacturer])

                                                         })).FirstOrDefault(),


                                      }).ToList();
                }
            }

            return serverInfoList;
        }


        #endregion
    }
}
