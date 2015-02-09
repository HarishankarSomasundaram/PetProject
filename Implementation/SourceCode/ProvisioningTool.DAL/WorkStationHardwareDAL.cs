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
    internal class WorkStationHardwareDAL
    {
        #region [ Declarations ]
        private List<WorkStationHardware> workStationHardwareList;
        private WorkStationHardware workStationHardware;
        #region [Colunm Attributes]
        private readonly string columnWorkStationHardwareID = "WorkStationHardwareID";
        private readonly string columnHostName = "HostName";
        private readonly string columnWorkStationHardwareModelID = "Model";
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
        internal WorkStationHardwareDAL()
        {
        }
        #endregion [ Constructor ]

        #region [ Add WorkStationHardware ]
        public WorkStationHardware AddWorkStationHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[22];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.HostName);

                parameters[1] = new SqlParameter("@Model", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.ModelName);

                parameters[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.SerialNumber);

                parameters[3] = new SqlParameter("@CPUID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.CPUID);

                parameters[4] = new SqlParameter("@MemoryID", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.MemoryIDs);

                parameters[5] = new SqlParameter("@MotherBoardID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.MotherBoardID);

                parameters[6] = new SqlParameter("@HardDriveID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.HardDriveIDs);

                parameters[7] = new SqlParameter("@ChipsetID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.ChipsetID);

                parameters[8] = new SqlParameter("@VideoCardID", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.VideoCardIDs);

                parameters[9] = new SqlParameter("@Display", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.DisplayIDs);

                parameters[10] = new SqlParameter("@MultimediaID", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.MultimediaIDs);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.CreatedBy);

                parameters[12] = new SqlParameter("@PortID", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.PortIDs);

                parameters[13] = new SqlParameter("@SlotID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.SlotIDs);

                parameters[15] = new SqlParameter("@ChassisID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.ChassisID);

                parameters[16] = new SqlParameter("@PowerID", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.PowerIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.StatusID);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.CreatedBy);

                parameters[19] = new SqlParameter("@Core", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.Core);

                parameters[20] = new SqlParameter("@Manufacturer", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.Manufacturer);

                parameters[21] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[21].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.SiteID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationHardwareAdd, parameters);
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
                return request.WorkStationHardware;

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
        #endregion [ Add WorkStationHardware ]

        #region [ Update WorkStationHardware ]
        public WorkStationHardware ModifyWorkStationHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[22];


                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.HostName);

                parameters[1] = new SqlParameter("@Model", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.ModelName);

                parameters[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.SerialNumber);

                parameters[3] = new SqlParameter("@CPUID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.CPUID);

                parameters[4] = new SqlParameter("@MemoryID", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.MemoryIDs);

                parameters[5] = new SqlParameter("@MotherBoardID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.MotherBoardID);

                parameters[6] = new SqlParameter("@HardDriveID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.HardDriveIDs);

                parameters[7] = new SqlParameter("@ChipsetID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.ChipsetID);

                parameters[8] = new SqlParameter("@VideoCardID", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.VideoCardIDs);

                parameters[9] = new SqlParameter("@Display", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.DisplayIDs);

                parameters[10] = new SqlParameter("@MultimediaID", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.MultimediaIDs);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.CreatedBy);

                parameters[12] = new SqlParameter("@PortID", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.PortIDs);

                parameters[13] = new SqlParameter("@SlotID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.SlotIDs);

                parameters[15] = new SqlParameter("@ChassisID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.ChassisID);

                parameters[16] = new SqlParameter("@PowerID", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.PowerIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.StatusID);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.CreatedBy);

                parameters[19] = new SqlParameter("@Core", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.Core);

                parameters[20] = new SqlParameter("@WorkStationHardwareID", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(request.WorkStationHardware.WorkStationHardwareID);

                parameters[21] = new SqlParameter("@Manufacturer", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.WorkStationHardware.Manufacturer);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationHardwareUpdateByWorkStationHardwareID, parameters);
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
                return request.WorkStationHardware;

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
        #endregion [ Update WorkStationHardware ]

        #region [ Delete WorkStationHardware ]
        internal bool DeleteWorkStationHardwareByWorkStationHardwareID(int WorkStationHardwareID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@WorkStationHardwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(WorkStationHardwareID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationHardwareDeleteByWorkStationHardwareID, parameters);
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
        #endregion [ Delete WorkStationHardware ]

        #region [Get All WorkStationHardware]
        public List<WorkStationHardware> GetAllWorkStationHardware(int SiteID)
        {
            DataSet ds = new DataSet();
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
            parameters[0].Value = DBValueHelper.ConvertToDBInt(SiteID);
            SqlDataReader reader = null;
            try
            {
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationHardware_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }

                //reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationHardware_List,parameters);
                //if (reader != null)
                //{
                //    return ProcessDataReader(reader);
                //}
                return workStationHardwareList;
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
        private List<WorkStationHardware> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        private List<WorkStationHardware> ConvertToObject(DataSet ds)
        {
            workStationHardwareList = new List<WorkStationHardware>();
            DataTable workStationHardwareDT = new DataTable();
            DataTable workStationHardwaresMemoryDT = new DataTable();
            DataTable workStationHardwareDisplayDT = new DataTable();
            DataTable workStationHardwareMultimediaDT = new DataTable();
            DataTable workStationHardwarePortDT = new DataTable();
            DataTable workStationHardwarePowerDT = new DataTable();
            DataTable workStationHardwareSlotDT = new DataTable();
            DataTable workStationHardwareVideoCardDT = new DataTable();
            DataTable globalMasterDetailsDT = new DataTable();
            DataTable systemHardDiskDT = new DataTable();
            DataTable systemHardDiskDriveDT = new DataTable();
            DataTable notesDetailsDT = new DataTable();
            DataTable workStationHardwareHardDriveDT = new DataTable();

            
            

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    workStationHardwareDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    workStationHardwaresMemoryDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    workStationHardwareDisplayDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    workStationHardwareMultimediaDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    workStationHardwarePortDT = ds.Tables[4];
                if (ds.Tables[5] != null)
                    workStationHardwarePowerDT = ds.Tables[5];
                if (ds.Tables[6] != null)
                    workStationHardwareSlotDT = ds.Tables[6];
                if (ds.Tables[7] != null)
                    workStationHardwareVideoCardDT = ds.Tables[7];
                if (ds.Tables[8] != null)
                    globalMasterDetailsDT = ds.Tables[8];
                if (ds.Tables[9] != null)
                    systemHardDiskDT = ds.Tables[9];
                if (ds.Tables[10] != null)
                    systemHardDiskDriveDT = ds.Tables[10];
                if (ds.Tables[11] != null)
                    notesDetailsDT = ds.Tables[11];
                if (ds.Tables[12] != null)
                    workStationHardwareHardDriveDT = ds.Tables[12];


                if (workStationHardwareDT.Rows.Count > 0)
                {
                    workStationHardwareList = (from DataRow workStationHardware in workStationHardwareDT.Rows

                                      select new WorkStationHardware
                                      {
                                            WorkStationHardwareID = ConvertHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID]),
                                            HostName = ConvertHelper.ConvertToString(workStationHardware[columnHostName]),
                                            SerialNumber = ConvertHelper.ConvertToString(workStationHardware[columnSerialNumber]),

                                            ModelName = ConvertHelper.ConvertToString(workStationHardware[columnWorkStationHardwareModelID]),
                                            CPUID = ConvertHelper.ConvertToInteger(workStationHardware[columnCPUID]),
                                            MemoryIDs = ConvertHelper.ConvertToString(workStationHardware[columnMemoryIDs]),
                                            MotherBoardID = ConvertHelper.ConvertToInteger(workStationHardware[columnMotherBoardID]),
                                            HardDriveIDs = ConvertHelper.ConvertToString(workStationHardware[columnHardDriveIDs]),
                                            ChipsetID = ConvertHelper.ConvertToInteger(workStationHardware[columnChipsetID]),
                                            VideoCardIDs = ConvertHelper.ConvertToString(workStationHardware[columnVideoCardIDs]),
                                            DisplayIDs = ConvertHelper.ConvertToString(workStationHardware[columnDisplayIDs]),
                                            MultimediaIDs = ConvertHelper.ConvertToString(workStationHardware[columnMultimediaIDs]),
                                            PortIDs = ConvertHelper.ConvertToString(workStationHardware[columnPortIDs]),
                                            SlotIDs = ConvertHelper.ConvertToString(workStationHardware[columnSlotIDs]),
                                            ChassisID = ConvertHelper.ConvertToInteger(workStationHardware[columnChassisID]),
                                            PowerIDs = ConvertHelper.ConvertToString(workStationHardware[columnPowerIDs]),
                                            CPUName = DataRowHelper.ConvertToString(workStationHardware[columnCPU]),
                                            MemoryName = DataRowHelper.ConvertToString(workStationHardware[ columnMemory]),
                                            MotherBoardName = DataRowHelper.ConvertToString(workStationHardware[ columnMotherBoard]),
                                            HardDriveName = DataRowHelper.ConvertToString(workStationHardware[ columnHardDrive]),
                                            ChipsetName = DataRowHelper.ConvertToString(workStationHardware[ columnChipsetID]),
                                            VideoCardName = DataRowHelper.ConvertToString(workStationHardware[ columnVideoCard]),
                                            DisplayName = DataRowHelper.ConvertToString(workStationHardware[ columnDisplay]),
                                            MultimediaIDName = DataRowHelper.ConvertToString(workStationHardware[ columnMultimedia]),
                                            PortName = DataRowHelper.ConvertToString(workStationHardware[ columnPort]),
                                            SlotName = DataRowHelper.ConvertToString(workStationHardware[ columnSlot]),
                                            ChassisName = DataRowHelper.ConvertToString(workStationHardware[ columnChassis]),
                                            PowerName = DataRowHelper.ConvertToString(workStationHardware[ columnPower]),
                                            Manufacturer = ConvertHelper.ConvertToString(workStationHardware[columnManufacturer]),
                                            Core = ConvertHelper.ConvertToInteger(workStationHardware[columnCore]),
                                            FullNotes = ConvertHelper.ConvertToString(workStationHardware[columnNotes]),
                                            StatusID = ConvertHelper.ConvertToInteger(workStationHardware[columnStatusID]),
                                            CreatedBy = ConvertHelper.ConvertToInteger(workStationHardware[columnCreatedBy]),
                                            CreatedOn = ConvertHelper.ConvertToDateTime(workStationHardware[columnCreatedOn]),
                                            ModifiedBy = ConvertHelper.ConvertToInteger(workStationHardware[columnModifiedBy]),
                                            ModifiedOn = ConvertHelper.ConvertToDateTime(workStationHardware[columnModifiedOn]),


                                            CPU = (from DataRow cpu in globalMasterDetailsDT.Rows
                                                   where cpu.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStationHardware[columnCPUID])
                                                               select (new GlobalMasterDetail
                                                               {
                                                                   MasterDetailID = DataRowHelper.ConvertToInteger(cpu[columnMasterDetailID]),
                                                                   MasterValue = DataRowHelper.ConvertToString(cpu[columnMasterValue])
                                                               })).FirstOrDefault(),

                                            MotherBoard = (from DataRow motherBoard in globalMasterDetailsDT.Rows
                                                           where motherBoard.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStationHardware[columnMotherBoardID])
                                                   select (new GlobalMasterDetail
                                                   {
                                                       MasterDetailID = DataRowHelper.ConvertToInteger(motherBoard[columnMasterDetailID]),
                                                       MasterValue = DataRowHelper.ConvertToString(motherBoard[columnMasterValue])
                                                   })).FirstOrDefault(),

                                            Chipset = (from DataRow chipset in globalMasterDetailsDT.Rows
                                                       where chipset.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStationHardware[columnChipsetID])
                                                           select (new GlobalMasterDetail
                                                           {
                                                               MasterDetailID = DataRowHelper.ConvertToInteger(chipset[columnMasterDetailID]),
                                                               MasterValue = DataRowHelper.ConvertToString(chipset[columnMasterValue])
                                                           })).FirstOrDefault(),

                                            Chassis = (from DataRow chassis in globalMasterDetailsDT.Rows
                                                       where chassis.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(workStationHardware[columnChassisID])
                                                           select (new GlobalMasterDetail
                                                           {
                                                               MasterDetailID = DataRowHelper.ConvertToInteger(chassis[columnMasterDetailID]),
                                                               MasterValue = DataRowHelper.ConvertToString(chassis[columnMasterValue])
                                                           })).FirstOrDefault(),



                                            Memorys = (from DataRow memorys in workStationHardwaresMemoryDT.Rows
                                                       where memorys.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
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

                                            VideoCards = (from DataRow videoCards in workStationHardwareVideoCardDT.Rows
                                                         where videoCards.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
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

                                            Displays = (from DataRow displays in workStationHardwareDisplayDT.Rows
                                                       where displays.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
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

                                            Multimedias = (from DataRow multimedias in workStationHardwareMultimediaDT.Rows
                                                          where multimedias.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
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


                                            Ports = (from DataRow ports in workStationHardwarePortDT.Rows
                                                    where ports.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
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

                                            Slots = (from DataRow slots in workStationHardwareSlotDT.Rows
                                                     where slots.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
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
                                                                          notesDetail.Field<int>(columnNotesClientID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
                                                                          select (new NotesDetail
                                                                          {
                                                                              NotesDetailID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesDetailID]),
                                                                              NotesMasterID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesMasterID]),
                                                                              NotesClientID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesClientID]),
                                                                              Notes = DataRowHelper.ConvertToString(notesDetail[columnNotes])
                                                                          })).ToList(),
                                                   })).FirstOrDefault(),

                                            Powers = (from DataRow powers in workStationHardwarePowerDT.Rows
                                                      where powers.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
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


                                            HardDrives = (from DataRow hardDrives in workStationHardwareHardDriveDT.Rows
                                                          where hardDrives.Field<int>(columnWorkStationHardwareID) == DataRowHelper.ConvertToInteger(workStationHardware[columnWorkStationHardwareID])
                                                           select (new SystemHardDrive
                                                           {
                                                               Quantity = DataRowHelper.ConvertToInteger(hardDrives[columnQuantity]),
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
            return workStationHardwareList;
        }

        #region [Get User And User Attribute Details By UserID]

        public WorkStationHardware GetWorkStationHardwarAndUserDetailsByWorkStationHardwarID(int WorkStationHardwareID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@WorkStationHardwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(WorkStationHardwareID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWorkStationHardwareByWorkStationHardwareID_List, parameters);
                if (ds != null)
                {
                    //workStationHardware = ConvertAllUserAttributesToObject(ds);
                    workStationHardwareList = ProcessDataSet(ds);
                }
                return workStationHardwareList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion

        #region [ private methods ]
        //Parses the data reader and converts to object
        //private List<WorkStationHardware> ProcessDataReader(SqlDataReader reader)
        //{
        //    if (!reader.IsClosed && reader.HasRows)
        //    {
        //        workStationHardwareList = new List<WorkStationHardware>();
        //        while (reader.Read())
        //            workStationHardwareList.Add(ConvertToObject(reader));
        //        return workStationHardwareList;
        //    }
        //    return null;
        //}

        //Converts each data record into object
        //private WorkStationHardware ConvertToObject(IDataRecord dataRecord)
        //{
        //    workStationHardware = new WorkStationHardware();

        //    workStationHardware.Port = new GlobalMasterDetail();
        //    workStationHardware.CPU = new GlobalMasterDetail();
        //    workStationHardware.Memory = new GlobalMasterDetail();
        //    workStationHardware.MotherBoard = new GlobalMasterDetail();
        //    workStationHardware.HardDrive = new GlobalMasterDetail();
        //    workStationHardware.Chipset = new GlobalMasterDetail();
        //    workStationHardware.VideoCard = new GlobalMasterDetail();
        //    workStationHardware.Display = new GlobalMasterDetail();
        //    workStationHardware.Multimedia = new GlobalMasterDetail();
        //    workStationHardware.Port = new GlobalMasterDetail();
        //    workStationHardware.Slot = new GlobalMasterDetail();
        //    workStationHardware.Chassis = new GlobalMasterDetail();
        //    workStationHardware.Power = new GlobalMasterDetail();

        //    workStationHardware.WorkStationHardwareID = DataRowHelper.ConvertToInteger(dataRecord, columnWorkStationHardwareID);
        //    workStationHardware.HostName = DataRowHelper.ConvertToString(dataRecord, columnHostName);
        //    workStationHardware.SerialNumber = DataRowHelper.ConvertToString(dataRecord, columnSerialNumber);

        //    workStationHardware.ModelName = DataRowHelper.ConvertToString(dataRecord, columnWorkStationHardwareModelID);
        //    workStationHardware.CPUName = DataRowHelper.ConvertToString(dataRecord, columnCPUID);
        //    workStationHardware.MemoryName = DataRowHelper.ConvertToString(dataRecord, columnMemoryID);
        //    workStationHardware.MotherBoardName = DataRowHelper.ConvertToString(dataRecord, columnMotherBoardID);
        //    workStationHardware.HardDriveName = DataRowHelper.ConvertToString(dataRecord, columnHardDriveID);
        //    workStationHardware.ChipsetName = DataRowHelper.ConvertToString(dataRecord, columnChipsetID);
        //    workStationHardware.VideoCardName = DataRowHelper.ConvertToString(dataRecord, columnVideoCardID);
        //    workStationHardware.DisplayName = DataRowHelper.ConvertToString(dataRecord, columnDisplay);
        //    workStationHardware.MultimediaIDName = DataRowHelper.ConvertToString(dataRecord, columnMultimediaID);
        //    workStationHardware.PortName = DataRowHelper.ConvertToString(dataRecord, columnPortID);
        //    workStationHardware.SlotName = DataRowHelper.ConvertToString(dataRecord, columnSlotID);
        //    workStationHardware.ChassisName = DataRowHelper.ConvertToString(dataRecord, columnChassisD);
        //    workStationHardware.PowerName = DataRowHelper.ConvertToString(dataRecord, columnPowerID);
        //    workStationHardware.Manufacturer = DataRowHelper.ConvertToString(dataRecord, columnManufacturer);

        //    workStationHardware.View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=WorkStations&opp=SH&id=" + ConvertHelper.ConvertToString(workStationHardware.WorkStationHardwareID) + " style='color: blue;text-decoration: underline;'>More</a>");
        //    //workStationHardware.FullNotes = ConvertHelper.ConvertToString(dataRecord, columnNotes);

        //    workStationHardware.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
        //    workStationHardware.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
        //    workStationHardware.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
        //    workStationHardware.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
        //    workStationHardware.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);

        //    return workStationHardware;
        //}

        //Converts each data set into object
        private WorkStationHardware ConvertAllUserAttributesToObject(DataSet ds)
        {
            workStationHardware = new WorkStationHardware();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    workStationHardware.WorkStationHardwareID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["WorkStationHardwareID"]);
                    workStationHardware.HostName = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["HostName"]);
                    workStationHardware.SerialNumber = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["SerialNumber"]);

                    workStationHardware.ModelName = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Model"]);
                    workStationHardware.CPUID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["CPUID"]);
                    workStationHardware.MemoryIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["MemoryIDs"]);
                    workStationHardware.MotherBoardID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["MotherBoardID"]);
                    workStationHardware.HardDriveIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["HardDriveIDs"]);
                    workStationHardware.ChipsetID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ChipsetID"]);
                    workStationHardware.VideoCardIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["VideoCardIDs"]);
                    workStationHardware.DisplayIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["DisplayIDs"]);
                    workStationHardware.MultimediaIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["MultimediaIDs"]);
                    workStationHardware.PortIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["PortIDs"]);
                    workStationHardware.SlotIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["SlotIDs"]);
                    workStationHardware.ChassisID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ChassisID"]);
                    workStationHardware.PowerIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["PowerIDs"]);
                    workStationHardware.Manufacturer = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Manufacturer"]);
                    workStationHardware.Core = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["Core"]);

                    workStationHardware.FullNotes = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Notes"]);

                    workStationHardware.StatusID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["StatusID"]);
                    workStationHardware.CreatedBy = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["CreatedBy"]);
                    workStationHardware.CreatedOn = ConvertHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                    workStationHardware.ModifiedBy = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ModifiedBy"]);
                    workStationHardware.ModifiedOn = ConvertHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["ModifiedOn"]);
                }
            }

            return workStationHardware;
        }
        #endregion
    }
}
