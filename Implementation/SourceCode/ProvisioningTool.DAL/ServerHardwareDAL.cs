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
    internal class ServerHardwareDAL
    {
        #region [ Declarations ]
        private List<ServerHardware> serverHardwareList;
        private ServerHardware serverHardware;
        #region [Colunm Attributes]
        private readonly string columnServerHardwareID = "ServerHardwareID";
        private readonly string columnHostName = "HostName";
        private readonly string columnServerHardwareModelID = "Model";
        private readonly string columnSerialNumber = "SerialNumber";
        private readonly string columnCPU = "CPU";
        private readonly string columnCPUID = "CPUID";
        private readonly string columnMemory = "Memory";
        private readonly string columnMemoryIDs = "MemoryIDs";
        private readonly string columnMemoryID = "MemoryID";

        private readonly string columnMotherBoard = "MotherBoard";
        private readonly string columnMotherBoardID = "MotherBoardID";
        private readonly string columnHardDrive = "Hard Drive";
        private readonly string columnHardDriveIDs = "HardDriveIDs";
        private readonly string columnHardDriveID = "HardDriveID";

        private readonly string columnChipset = "Chipset";
        private readonly string columnChipsetIDs = "ChipsetIDs";
        private readonly string columnChipsetID = "ChipsetID";
        private readonly string columnVideoCard = "Video Card";
        private readonly string columnVideoCardIDs = "VideoCardIDs";
        private readonly string columnVideoCardID = "VideoCardID";

        private readonly string columnDisplay = "Display";
        private readonly string columnDisplayIDs = "DisplayIDs";
        private readonly string columnDisplayID = "DisplayID";
        private readonly string columnMultimedia = "Multimedia";
        private readonly string columnMultimediaID = "MultimediaID";
        private readonly string columnMultimediaIDs = "MultimediaIDs";
        private readonly string columnPort = "Port";
        private readonly string columnPortID = "PortID";
        private readonly string columnPortIDs = "PortIDs";

        private readonly string columnSlot = "Slot";
        private readonly string columnSlotID = "SlotID";
        private readonly string columnSlotIDs = "SlotIDs";
        private readonly string columnPower = "Power";
        private readonly string columnPowerID = "PowerID";
        private readonly string columnPowerIDs = "PowerIDs";
        private readonly string columnChassis = "Chassis";
        private readonly string columnChassisID = "ChassisID";
        private readonly string columnChassisIDs = "ChassisIDs";
        private readonly string columnManufacturer = "Manufacturer";

        private readonly string columnNotes = "Notes";
        private readonly string columnStatusID = "Status";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedOn = "ModifiedOn";

        private readonly string columnCore = "Core";

        private readonly string columnMasterDetailID = "MasterDetailID";
        private readonly string columnMasterValue = "MasterValue";
        private readonly string columnNotesMasterID = "NotesMasterID";
        private readonly string columnNotesDetailID = "NotesDetailID";
        private readonly string columnNotesClientID = "NotesClientID";

        private readonly string columnSystemHardDiskDriveID = "SystemHardDiskDriveID";
        private readonly string columnSystemHardDiskID = "SystemHardDiskID";
        private readonly string columnHardDriveName = "HardDriveName";
        private readonly string columnSize = "Size";
        private readonly string columnSizeUnit = "SizeUnit";
        private readonly string columnDriveCharacter = "DriveCharacter";
        private readonly string columnQuantity = "Quantity";

        #endregion

        #endregion [ Declarations ]

        #region [ Constructor ]
        internal ServerHardwareDAL()
        {
        }
        #endregion [ Constructor ]

        #region [ Add ServerHardware ]
        public ServerHardware AddServerHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[22];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.HostName);

                parameters[1] = new SqlParameter("@Model", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.ModelName);

                parameters[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.SerialNumber);

                parameters[3] = new SqlParameter("@CPUID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.CPUID);

                parameters[4] = new SqlParameter("@MemoryID", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.MemoryIDs);

                parameters[5] = new SqlParameter("@MotherBoardID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.MotherBoardID);

                parameters[6] = new SqlParameter("@HardDriveID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.HardDriveIDs);

                parameters[7] = new SqlParameter("@ChipsetID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.ChipsetID);

                parameters[8] = new SqlParameter("@VideoCardID", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.VideoCardIDs);

                parameters[9] = new SqlParameter("@Display", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.DisplayIDs);

                parameters[10] = new SqlParameter("@MultimediaID", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.MultimediaIDs);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.CreatedBy);

                parameters[12] = new SqlParameter("@PortID", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.PortIDs);

                parameters[13] = new SqlParameter("@SlotID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.SlotIDs);

                parameters[15] = new SqlParameter("@ChassisID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.ChassisID);

                parameters[16] = new SqlParameter("@PowerID", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.PowerIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.StatusID);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.CreatedBy);

                parameters[19] = new SqlParameter("@Core", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.Core);

                parameters[20] = new SqlParameter("@Manufacturer", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.Manufacturer);

                parameters[21] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[21].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.SiteID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerHardwareAdd, parameters);
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
                return request.ServerHardware;

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
        #endregion [ Add ServerHardware ]

        #region [ Update ServerHardware ]
        public ServerHardware ModifyServerHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[22];


                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.HostName);

                parameters[1] = new SqlParameter("@Model", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.ModelName);

                parameters[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.SerialNumber);

                parameters[3] = new SqlParameter("@CPUID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.CPUID);

                parameters[4] = new SqlParameter("@MemoryID", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.MemoryIDs);

                parameters[5] = new SqlParameter("@MotherBoardID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.MotherBoardID);

                parameters[6] = new SqlParameter("@HardDriveID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.HardDriveIDs);

                parameters[7] = new SqlParameter("@ChipsetID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.ChipsetID);

                parameters[8] = new SqlParameter("@VideoCardID", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.VideoCardIDs);

                parameters[9] = new SqlParameter("@Display", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.DisplayIDs);

                parameters[10] = new SqlParameter("@MultimediaID", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.MultimediaIDs);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.CreatedBy);

                parameters[12] = new SqlParameter("@PortID", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.PortIDs);

                parameters[13] = new SqlParameter("@SlotID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.SlotIDs);

                parameters[15] = new SqlParameter("@ChassisID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.ChassisID);

                parameters[16] = new SqlParameter("@PowerID", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.PowerIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.StatusID);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.CreatedBy);

                parameters[19] = new SqlParameter("@Core", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.Core);

                parameters[20] = new SqlParameter("@ServerHardwareID", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(request.ServerHardware.ServerHardwareID);

                parameters[21] = new SqlParameter("@Manufacturer", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.ServerHardware.Manufacturer);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerHardwareUpdateByServerHardwareID, parameters);
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
                return request.ServerHardware;

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
        #endregion [ Update ServerHardware ]

        #region [ Delete ServerHardware ]
        internal bool DeleteServerHardwareByServerHardwareID(int ServerHardwareID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@ServerHardwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(ServerHardwareID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerHardwareDeleteByServerHardwareID, parameters);
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
        #endregion [ Delete ServerHardware ]

        #region [Get All ServerHardware]
        public List<ServerHardware> GetAllServerHardware(int SiteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);
            SqlDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(SiteID);

                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerHardware_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }


                //reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerHardware_List,parameters);
                //if (reader != null)
                //{
                //    return ProcessDataReader(reader);
                //}
                return serverHardwareList;
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

        #region [ ProcessDataSet]
        //Parses the data reader and converts to object
        private List<ServerHardware> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        private List<ServerHardware> ConvertToObject(DataSet ds)
        {
            serverHardwareList = new List<ServerHardware>();
            DataTable serverHardwareDT = new DataTable();
            DataTable serverHardwaresMemoryDT = new DataTable();
            DataTable serverHardwareDisplayDT = new DataTable();
            DataTable serverHardwareMultimediaDT = new DataTable();
            DataTable serverHardwarePortDT = new DataTable();
            DataTable serverHardwarePowerDT = new DataTable();
            DataTable serverHardwareSlotDT = new DataTable();
            DataTable serverHardwareVideoCardDT = new DataTable();
            DataTable globalMasterDetailsDT = new DataTable();
            DataTable systemHardDiskDT = new DataTable();
            DataTable systemHardDiskDriveDT = new DataTable();
            DataTable notesDetailsDT = new DataTable();
            DataTable serverHardwareHardDriveDT = new DataTable();

            
            

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    serverHardwareDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    serverHardwaresMemoryDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    serverHardwareDisplayDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    serverHardwareMultimediaDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    serverHardwarePortDT = ds.Tables[4];
                if (ds.Tables[5] != null)
                    serverHardwarePowerDT = ds.Tables[5];
                if (ds.Tables[6] != null)
                    serverHardwareSlotDT = ds.Tables[6];
                if (ds.Tables[7] != null)
                    serverHardwareVideoCardDT = ds.Tables[7];
                if (ds.Tables[8] != null)
                    globalMasterDetailsDT = ds.Tables[8];
                if (ds.Tables[9] != null)
                    systemHardDiskDT = ds.Tables[9];
                if (ds.Tables[10] != null)
                    systemHardDiskDriveDT = ds.Tables[10];
                if (ds.Tables[11] != null)
                    notesDetailsDT = ds.Tables[11];
                if (ds.Tables[12] != null)
                    serverHardwareHardDriveDT = ds.Tables[12];


                if (serverHardwareDT.Rows.Count > 0)
                {
                    serverHardwareList = (from DataRow serverHardware in serverHardwareDT.Rows

                                      select new ServerHardware
                                      {
                                            ServerHardwareID = ConvertHelper.ConvertToInteger(serverHardware[columnServerHardwareID]),
                                            HostName = ConvertHelper.ConvertToString(serverHardware[columnHostName]),
                                            SerialNumber = ConvertHelper.ConvertToString(serverHardware[columnSerialNumber]),

                                            ModelName = ConvertHelper.ConvertToString(serverHardware[columnServerHardwareModelID]),
                                            CPUID = ConvertHelper.ConvertToInteger(serverHardware[columnCPUID]),
                                            MemoryIDs = ConvertHelper.ConvertToString(serverHardware[columnMemoryIDs]),
                                            MotherBoardID = ConvertHelper.ConvertToInteger(serverHardware[columnMotherBoardID]),
                                            HardDriveIDs = ConvertHelper.ConvertToString(serverHardware[columnHardDriveIDs]),
                                            ChipsetID = ConvertHelper.ConvertToInteger(serverHardware[columnChipsetID]),
                                            VideoCardIDs = ConvertHelper.ConvertToString(serverHardware[columnVideoCardIDs]),
                                            DisplayIDs = ConvertHelper.ConvertToString(serverHardware[columnDisplayIDs]),
                                            MultimediaIDs = ConvertHelper.ConvertToString(serverHardware[columnMultimediaIDs]),
                                            PortIDs = ConvertHelper.ConvertToString(serverHardware[columnPortIDs]),
                                            SlotIDs = ConvertHelper.ConvertToString(serverHardware[columnSlotIDs]),
                                            ChassisID = ConvertHelper.ConvertToInteger(serverHardware[columnChassisID]),
                                            PowerIDs = ConvertHelper.ConvertToString(serverHardware[columnPowerIDs]),
                                            CPUName = DataRowHelper.ConvertToString(serverHardware[columnCPU]),
                                            MemoryName = DataRowHelper.ConvertToString(serverHardware[ columnMemory]),
                                            MotherBoardName = DataRowHelper.ConvertToString(serverHardware[ columnMotherBoard]),
                                            HardDriveName = DataRowHelper.ConvertToString(serverHardware[ columnHardDrive]),
                                            ChipsetName = DataRowHelper.ConvertToString(serverHardware[ columnChipsetID]),
                                            VideoCardName = DataRowHelper.ConvertToString(serverHardware[ columnVideoCard]),
                                            DisplayName = DataRowHelper.ConvertToString(serverHardware[ columnDisplay]),
                                            MultimediaIDName = DataRowHelper.ConvertToString(serverHardware[ columnMultimedia]),
                                            PortName = DataRowHelper.ConvertToString(serverHardware[ columnPort]),
                                            SlotName = DataRowHelper.ConvertToString(serverHardware[ columnSlot]),
                                            ChassisName = DataRowHelper.ConvertToString(serverHardware[ columnChassis]),
                                            PowerName = DataRowHelper.ConvertToString(serverHardware[ columnPower]),
                                            Manufacturer = ConvertHelper.ConvertToString(serverHardware[columnManufacturer]),
                                            Core = ConvertHelper.ConvertToInteger(serverHardware[columnCore]),
                                            FullNotes = ConvertHelper.ConvertToString(serverHardware[columnNotes]),
                                            StatusID = ConvertHelper.ConvertToInteger(serverHardware[columnStatusID]),
                                            CreatedBy = ConvertHelper.ConvertToInteger(serverHardware[columnCreatedBy]),
                                            CreatedOn = ConvertHelper.ConvertToDateTime(serverHardware[columnCreatedOn]),
                                            ModifiedBy = ConvertHelper.ConvertToInteger(serverHardware[columnModifiedBy]),
                                            ModifiedOn = ConvertHelper.ConvertToDateTime(serverHardware[columnModifiedOn]),


                                            CPU = (from DataRow cpu in globalMasterDetailsDT.Rows
                                                   where cpu.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(serverHardware[columnCPUID])
                                                               select (new GlobalMasterDetail
                                                               {
                                                                   MasterDetailID = DataRowHelper.ConvertToInteger(cpu[columnMasterDetailID]),
                                                                   MasterValue = DataRowHelper.ConvertToString(cpu[columnMasterValue])
                                                               })).FirstOrDefault(),

                                            MotherBoard = (from DataRow motherBoard in globalMasterDetailsDT.Rows
                                                           where motherBoard.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(serverHardware[columnMotherBoardID])
                                                   select (new GlobalMasterDetail
                                                   {
                                                       MasterDetailID = DataRowHelper.ConvertToInteger(motherBoard[columnMasterDetailID]),
                                                       MasterValue = DataRowHelper.ConvertToString(motherBoard[columnMasterValue])
                                                   })).FirstOrDefault(),

                                            Chipset = (from DataRow chipset in globalMasterDetailsDT.Rows
                                                       where chipset.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(serverHardware[columnChipsetID])
                                                           select (new GlobalMasterDetail
                                                           {
                                                               MasterDetailID = DataRowHelper.ConvertToInteger(chipset[columnMasterDetailID]),
                                                               MasterValue = DataRowHelper.ConvertToString(chipset[columnMasterValue])
                                                           })).FirstOrDefault(),

                                            Chassis = (from DataRow chassis in globalMasterDetailsDT.Rows
                                                       where chassis.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(serverHardware[columnChassisID])
                                                           select (new GlobalMasterDetail
                                                           {
                                                               MasterDetailID = DataRowHelper.ConvertToInteger(chassis[columnMasterDetailID]),
                                                               MasterValue = DataRowHelper.ConvertToString(chassis[columnMasterValue])
                                                           })).FirstOrDefault(),



                                            Memorys = (from DataRow memorys in serverHardwaresMemoryDT.Rows
                                                       where memorys.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                       select (new SystemMemory
                                                            {
                                                                SystemID = DataRowHelper.ConvertToInteger(memorys[columnMemoryID]),
                                                                Quantity = DataRowHelper.ConvertToInteger(memorys[columnQuantity]),
                                                                Memory = (from DataRow memory in globalMasterDetailsDT.Rows
                                                                          where memory.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(memorys[columnMemoryID])
                                                                                  select (new GlobalMasterDetail
                                                                                  {
                                                                                      MasterDetailID = DataRowHelper.ConvertToInteger(memory[columnMasterDetailID]),
                                                                                      MasterValue = DataRowHelper.ConvertToString(memory[columnMasterValue])
                                                                                  })).FirstOrDefault(),
                                                            })).ToList(),

                                            VideoCards = (from DataRow videoCards in serverHardwareVideoCardDT.Rows
                                                         where videoCards.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                       select (new SystemVideoCard
                                                       {
                                                           SystemID = DataRowHelper.ConvertToInteger(videoCards[columnVideoCardID]),
                                                           VideoCard = (from DataRow videoCard in globalMasterDetailsDT.Rows
                                                                        where videoCard.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(videoCards[columnVideoCardID])
                                                                     select (new GlobalMasterDetail
                                                                     {
                                                                         MasterDetailID = DataRowHelper.ConvertToInteger(videoCard[columnMasterDetailID]),
                                                                         MasterValue = DataRowHelper.ConvertToString(videoCard[columnMasterValue])
                                                                     })).FirstOrDefault(),
                                                       })).ToList(),

                                            Displays = (from DataRow displays in serverHardwareDisplayDT.Rows
                                                       where displays.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                         select (new SystemDisplay
                                                         {
                                                             SystemID = DataRowHelper.ConvertToInteger(displays[columnDisplayID]),
                                                             Display = (from DataRow display in globalMasterDetailsDT.Rows
                                                                        where display.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(displays[columnDisplayID])
                                                                          select (new GlobalMasterDetail
                                                                          {
                                                                              MasterDetailID = DataRowHelper.ConvertToInteger(display[columnMasterDetailID]),
                                                                              MasterValue = DataRowHelper.ConvertToString(display[columnMasterValue])
                                                                          })).FirstOrDefault(),
                                                         })).ToList(),

                                            Multimedias = (from DataRow multimedias in serverHardwareMultimediaDT.Rows
                                                          where multimedias.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                       select (new SystemMultimedia
                                                       {
                                                           SystemID = DataRowHelper.ConvertToInteger(multimedias[columnMultimediaID]),
                                                           Multimedia = (from DataRow multimedia in globalMasterDetailsDT.Rows
                                                                         where multimedia.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(multimedias[columnMultimediaID])
                                                                      select (new GlobalMasterDetail
                                                                      {
                                                                          MasterDetailID = DataRowHelper.ConvertToInteger(multimedia[columnMasterDetailID]),
                                                                          MasterValue = DataRowHelper.ConvertToString(multimedia[columnMasterValue])
                                                                      })).FirstOrDefault(),
                                                       })).ToList(),


                                            Ports = (from DataRow ports in serverHardwarePortDT.Rows
                                                    where ports.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                          select (new SystemPort
                                                          {
                                                              SystemID = DataRowHelper.ConvertToInteger(ports[columnPortID]),
                                                              Port = (from DataRow port in globalMasterDetailsDT.Rows
                                                                      where port.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(ports[columnPortID])
                                                                            select (new GlobalMasterDetail
                                                                            {
                                                                                MasterDetailID = DataRowHelper.ConvertToInteger(port[columnMasterDetailID]),
                                                                                MasterValue = DataRowHelper.ConvertToString(port[columnMasterValue])
                                                                            })).FirstOrDefault(),
                                                          })).ToList(),

                                            Slots = (from DataRow slots in serverHardwareSlotDT.Rows
                                                     where slots.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                    select (new SystemSlot
                                                    {
                                                        SystemID = DataRowHelper.ConvertToInteger(slots[columnSlotID]),
                                                        Slot = (from DataRow slot in globalMasterDetailsDT.Rows
                                                                where slot.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(slots[columnSlotID])
                                                                select (new GlobalMasterDetail
                                                                {
                                                                    MasterDetailID = DataRowHelper.ConvertToInteger(slot[columnMasterDetailID]),
                                                                    MasterValue = DataRowHelper.ConvertToString(slot[columnMasterValue])
                                                                })).FirstOrDefault(),
                                                    })).ToList(),

                                            Notes = (from DataRow notes in notesDetailsDT.Rows
                                                   where notes.Field<int>(columnNotesMasterID) == 5
                                                   select (new NotesMaster
                                                   {
                                                       NotesMasterID = DataRowHelper.ConvertToInteger(notes[columnNotesMasterID]),
                                                       NotesDetailList = (from DataRow notesDetail in notesDetailsDT.Rows
                                                                          where notesDetail.Field<int>(columnNotesMasterID) == DataRowHelper.ConvertToInteger(notes[columnNotesMasterID]) &&
                                                                          notesDetail.Field<int>(columnNotesClientID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                                          select (new NotesDetail
                                                                          {
                                                                              NotesDetailID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesDetailID]),
                                                                              NotesMasterID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesMasterID]),
                                                                              NotesClientID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesClientID]),
                                                                              Notes = DataRowHelper.ConvertToString(notesDetail[columnNotes])
                                                                          })).ToList(),
                                                   })).FirstOrDefault(),

                                            Powers = (from DataRow powers in serverHardwarePowerDT.Rows
                                                      where powers.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                    select (new SystemPower
                                                    {
                                                        SystemID = DataRowHelper.ConvertToInteger(powers[columnPowerID]),
                                                        Power = (from DataRow power in globalMasterDetailsDT.Rows
                                                                 where power.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(powers[columnPowerID])
                                                                select (new GlobalMasterDetail
                                                                {
                                                                    MasterDetailID = DataRowHelper.ConvertToInteger(power[columnMasterDetailID]),
                                                                    MasterValue = DataRowHelper.ConvertToString(power[columnMasterValue])
                                                                })).FirstOrDefault(),
                                                    })).ToList(),


                                            HardDrives = (from DataRow hardDrives in serverHardwareHardDriveDT.Rows
                                                          where hardDrives.Field<int>(columnServerHardwareID) == DataRowHelper.ConvertToInteger(serverHardware[columnServerHardwareID])
                                                           select (new SystemHardDrive
                                                            {
                                                                Quantity  = DataRowHelper.ConvertToInteger(hardDrives[columnQuantity]),
                                                                DiskDetails = (from DataRow diskDetails in systemHardDiskDT.Rows
                                                                               where diskDetails.Field<int>(columnSystemHardDiskID) == DataRowHelper.ConvertToInteger(hardDrives[columnHardDriveID])
                                                                       select (new HardDisk
                                                                       {
                                                                           SystemHardDiskID = DataRowHelper.ConvertToInteger(diskDetails[columnSystemHardDiskID]),
                                                                           HardDiskName = DataRowHelper.ConvertToString(diskDetails[columnHardDriveName]),
                                                                           Size = DataRowHelper.ConvertToInteger(diskDetails[columnSize]),
                                                                           SizeUnit = DataRowHelper.ConvertToString(diskDetails[columnSizeUnit]),
                                                                           HardDiskDrive = (from DataRow hardDiskDrive in systemHardDiskDriveDT.Rows
                                                                                            where hardDiskDrive.Field<int>(columnSystemHardDiskID) == DataRowHelper.ConvertToInteger(diskDetails[columnSystemHardDiskID])
                                                                                    select (new HardDiskDrive
                                                                                    {
                                                                                        DriveCharacter = DataRowHelper.ConvertToString(hardDiskDrive[columnDriveCharacter]),
                                                                                        Size = DataRowHelper.ConvertToInteger(hardDiskDrive[columnSize]),
                                                                                        SizeUnit = DataRowHelper.ConvertToString(hardDiskDrive[columnSizeUnit])
                                                                                    })).ToList(),
                                                                       })).ToList(),
                                                            })).ToList(),
                                              }).ToList();

                                           
                }
            }
            return serverHardwareList;
        }

        #region [Get User And User Attribute Details By UserID]

        public ServerHardware GetServerHardwarAndUserDetailsByServerHardwarID(int ServerHardwareID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ServerHardwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(ServerHardwareID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPServerHardwareByServerHardwareID_List, parameters);
                if (ds != null)
                {
                    //serverHardware = ConvertAllUserAttributesToObject(ds);
                    serverHardwareList = ProcessDataSet(ds);
                }
                return serverHardwareList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion

        #region [ private methods ]
        //Parses the data reader and converts to object
        private List<ServerHardware> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                serverHardwareList = new List<ServerHardware>();
                while (reader.Read())
                    serverHardwareList.Add(ConvertToObject(reader));
                return serverHardwareList;
            }
            return null;
        }

        //Converts each data record into object
        private ServerHardware ConvertToObject(IDataRecord dataRecord)
        {
            serverHardware = new ServerHardware();

            serverHardware.Port = new GlobalMasterDetail();
            serverHardware.CPU = new GlobalMasterDetail();
            serverHardware.Memory = new GlobalMasterDetail();
            serverHardware.MotherBoard = new GlobalMasterDetail();
            serverHardware.HardDrive = new GlobalMasterDetail();
            serverHardware.Chipset = new GlobalMasterDetail();
            serverHardware.VideoCard = new GlobalMasterDetail();
            serverHardware.Display = new GlobalMasterDetail();
            serverHardware.Multimedia = new GlobalMasterDetail();
            serverHardware.Port = new GlobalMasterDetail();
            serverHardware.Slot = new GlobalMasterDetail();
            serverHardware.Chassis = new GlobalMasterDetail();
            serverHardware.Power = new GlobalMasterDetail();

            serverHardware.ServerHardwareID = DataRowHelper.ConvertToInteger(dataRecord, columnServerHardwareID);
            serverHardware.HostName = DataRowHelper.ConvertToString(dataRecord, columnHostName);
            serverHardware.SerialNumber = DataRowHelper.ConvertToString(dataRecord, columnSerialNumber);

            serverHardware.ModelName = DataRowHelper.ConvertToString(dataRecord, columnServerHardwareModelID);
            serverHardware.CPUName = DataRowHelper.ConvertToString(dataRecord, columnCPU);
            serverHardware.MemoryName = DataRowHelper.ConvertToString(dataRecord, columnMemory);
            serverHardware.MotherBoardName = DataRowHelper.ConvertToString(dataRecord, columnMotherBoard);
            serverHardware.HardDriveName = DataRowHelper.ConvertToString(dataRecord, columnHardDrive);
            serverHardware.ChipsetName = DataRowHelper.ConvertToString(dataRecord, columnChipsetID);
            serverHardware.VideoCardName = DataRowHelper.ConvertToString(dataRecord, columnVideoCard);
            serverHardware.DisplayName = DataRowHelper.ConvertToString(dataRecord, columnDisplay);
            serverHardware.MultimediaIDName = DataRowHelper.ConvertToString(dataRecord, columnMultimedia);
            serverHardware.PortName = DataRowHelper.ConvertToString(dataRecord, columnPort);
            serverHardware.SlotName = DataRowHelper.ConvertToString(dataRecord, columnSlot);
            serverHardware.ChassisName = DataRowHelper.ConvertToString(dataRecord, columnChassis);
            serverHardware.PowerName = DataRowHelper.ConvertToString(dataRecord, columnPower);
            serverHardware.Manufacturer = DataRowHelper.ConvertToString(dataRecord, columnManufacturer);

            serverHardware.View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Servers&opp=SH&id=" + ConvertHelper.ConvertToString(serverHardware.ServerHardwareID) + " style='color: blue;text-decoration: underline;'>More</a>");
            //serverHardware.FullNotes = ConvertHelper.ConvertToString(dataRecord, columnNotes);

            serverHardware.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
            serverHardware.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
            serverHardware.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
            serverHardware.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
            serverHardware.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);

            return serverHardware;
        }

        //Converts each data set into object
        private ServerHardware ConvertAllUserAttributesToObject(DataSet ds)
        {
            serverHardware = new ServerHardware();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    serverHardware.ServerHardwareID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ServerHardwareID"]);
                    serverHardware.HostName = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["HostName"]);
                    serverHardware.SerialNumber = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["SerialNumber"]);

                    serverHardware.ModelName = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Model"]);
                    serverHardware.CPUID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["CPUID"]);
                    serverHardware.MemoryIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["MemoryIDs"]);
                    serverHardware.MotherBoardID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["MotherBoardID"]);
                    serverHardware.HardDriveIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["HardDriveIDs"]);
                    serverHardware.ChipsetID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ChipsetID"]);
                    serverHardware.VideoCardIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["VideoCardIDs"]);
                    serverHardware.DisplayIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["DisplayIDs"]);
                    serverHardware.MultimediaIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["MultimediaIDs"]);
                    serverHardware.PortIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["PortIDs"]);
                    serverHardware.SlotIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["SlotIDs"]);
                    serverHardware.ChassisID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ChassisID"]);
                    serverHardware.PowerIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["PowerIDs"]);
                    serverHardware.Manufacturer = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Manufacturer"]);
                    serverHardware.Core = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["Core"]);

                    serverHardware.FullNotes = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Notes"]);

                    serverHardware.StatusID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["StatusID"]);
                    serverHardware.CreatedBy = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["CreatedBy"]);
                    serverHardware.CreatedOn = ConvertHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                    serverHardware.ModifiedBy = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ModifiedBy"]);
                    serverHardware.ModifiedOn = ConvertHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["ModifiedOn"]);
                }
            }

            return serverHardware;
        }
        #endregion
    }
}
