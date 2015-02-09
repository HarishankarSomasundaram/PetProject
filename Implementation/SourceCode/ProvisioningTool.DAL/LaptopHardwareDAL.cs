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
    internal class LaptopHardwareDAL
    {
        #region [ Declarations ]
        private List<LaptopHardware> laptopHardwareList;
        private LaptopHardware laptopHardware;
        #region [Colunm Attributes]
        private readonly string columnLaptopHardwareID = "LaptopHardwareID";
        private readonly string columnHostName = "HostName";
        private readonly string columnLaptopHardwareModelID = "Model";
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
        internal LaptopHardwareDAL()
        {
        }
        #endregion [ Constructor ]

        #region [ Add LaptopHardware ]
        public LaptopHardware AddLaptopHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[22];

                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.HostName);

                parameters[1] = new SqlParameter("@Model", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.ModelName);

                parameters[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.SerialNumber);

                parameters[3] = new SqlParameter("@CPUID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.CPUID);

                parameters[4] = new SqlParameter("@MemoryID", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.MemoryIDs);

                parameters[5] = new SqlParameter("@MotherBoardID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.MotherBoardID);

                parameters[6] = new SqlParameter("@HardDriveID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.HardDriveIDs);

                parameters[7] = new SqlParameter("@ChipsetID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.ChipsetID);

                parameters[8] = new SqlParameter("@VideoCardID", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.VideoCardIDs);

                parameters[9] = new SqlParameter("@Display", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.DisplayIDs);

                parameters[10] = new SqlParameter("@MultimediaID", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.MultimediaIDs);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.CreatedBy);

                parameters[12] = new SqlParameter("@PortID", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.PortIDs);

                parameters[13] = new SqlParameter("@SlotID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.SlotIDs);

                parameters[15] = new SqlParameter("@ChassisID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.ChassisID);

                parameters[16] = new SqlParameter("@PowerID", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.PowerIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.StatusID);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.CreatedBy);

                parameters[19] = new SqlParameter("@Core", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.Core);

                parameters[20] = new SqlParameter("@Manufacturer", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.Manufacturer);

                parameters[21] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[21].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.SiteID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopHardwareAdd, parameters);
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
                return request.LaptopHardware;

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
        #endregion [ Add LaptopHardware ]

        #region [ Update LaptopHardware ]
        public LaptopHardware ModifyLaptopHardware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[22];


                parameters[0] = new SqlParameter("@HostName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.HostName);

                parameters[1] = new SqlParameter("@Model", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.ModelName);

                parameters[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.SerialNumber);

                parameters[3] = new SqlParameter("@CPUID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.CPUID);

                parameters[4] = new SqlParameter("@MemoryID", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.MemoryIDs);

                parameters[5] = new SqlParameter("@MotherBoardID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.MotherBoardID);

                parameters[6] = new SqlParameter("@HardDriveID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.HardDriveIDs);

                parameters[7] = new SqlParameter("@ChipsetID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.ChipsetID);

                parameters[8] = new SqlParameter("@VideoCardID", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.VideoCardIDs);

                parameters[9] = new SqlParameter("@Display", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.DisplayIDs);

                parameters[10] = new SqlParameter("@MultimediaID", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.MultimediaIDs);

                parameters[11] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.CreatedBy);

                parameters[12] = new SqlParameter("@PortID", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.PortIDs);

                parameters[13] = new SqlParameter("@SlotID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.SlotIDs);

                parameters[15] = new SqlParameter("@ChassisID", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.ChassisID);

                parameters[16] = new SqlParameter("@PowerID", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.PowerIDs);

                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.FullNotes);

                parameters[18] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.StatusID);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.CreatedBy);

                parameters[19] = new SqlParameter("@Core", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.Core);

                parameters[20] = new SqlParameter("@LaptopHardwareID", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(request.LaptopHardware.LaptopHardwareID);

                parameters[21] = new SqlParameter("@Manufacturer", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(request.LaptopHardware.Manufacturer);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopHardwareUpdateByLaptopHardwareID, parameters);
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
                return request.LaptopHardware;

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
        #endregion [ Update LaptopHardware ]

        #region [ Delete LaptopHardware ]
        internal bool DeleteLaptopHardwareByLaptopHardwareID(int LaptopHardwareID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@LaptopHardwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(LaptopHardwareID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopHardwareDeleteByLaptopHardwareID, parameters);
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
        #endregion [ Delete LaptopHardware ]

        #region [Get All LaptopHardware]
        public List<LaptopHardware> GetAllLaptopHardware(int SiteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);
            SqlDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(SiteID);

                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopHardware_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                return laptopHardwareList;
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
        private List<LaptopHardware> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        private List<LaptopHardware> ConvertToObject(DataSet ds)
        {
            laptopHardwareList = new List<LaptopHardware>();
            DataTable laptopHardwareDT = new DataTable();
            DataTable laptopHardwaresMemoryDT = new DataTable();
            DataTable laptopHardwareDisplayDT = new DataTable();
            DataTable laptopHardwareMultimediaDT = new DataTable();
            DataTable laptopHardwarePortDT = new DataTable();
            DataTable laptopHardwarePowerDT = new DataTable();
            DataTable laptopHardwareSlotDT = new DataTable();
            DataTable laptopHardwareVideoCardDT = new DataTable();
            DataTable globalMasterDetailsDT = new DataTable();
            DataTable systemHardDiskDT = new DataTable();
            DataTable systemHardDiskDriveDT = new DataTable();
            DataTable notesDetailsDT = new DataTable();
            DataTable laptopHardwareHardDriveDT = new DataTable();

            
            

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    laptopHardwareDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    laptopHardwaresMemoryDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    laptopHardwareDisplayDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    laptopHardwareMultimediaDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    laptopHardwarePortDT = ds.Tables[4];
                if (ds.Tables[5] != null)
                    laptopHardwarePowerDT = ds.Tables[5];
                if (ds.Tables[6] != null)
                    laptopHardwareSlotDT = ds.Tables[6];
                if (ds.Tables[7] != null)
                    laptopHardwareVideoCardDT = ds.Tables[7];
                if (ds.Tables[8] != null)
                    globalMasterDetailsDT = ds.Tables[8];
                if (ds.Tables[9] != null)
                    systemHardDiskDT = ds.Tables[9];
                if (ds.Tables[10] != null)
                    systemHardDiskDriveDT = ds.Tables[10];
                if (ds.Tables[11] != null)
                    notesDetailsDT = ds.Tables[11];
                if (ds.Tables[12] != null)
                    laptopHardwareHardDriveDT = ds.Tables[12];


                if (laptopHardwareDT.Rows.Count > 0)
                {
                    laptopHardwareList = (from DataRow laptopHardware in laptopHardwareDT.Rows

                                      select new LaptopHardware
                                      {
                                            LaptopHardwareID = ConvertHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID]),
                                            HostName = ConvertHelper.ConvertToString(laptopHardware[columnHostName]),
                                            SerialNumber = ConvertHelper.ConvertToString(laptopHardware[columnSerialNumber]),

                                            ModelName = ConvertHelper.ConvertToString(laptopHardware[columnLaptopHardwareModelID]),
                                            CPUID = ConvertHelper.ConvertToInteger(laptopHardware[columnCPUID]),
                                            MemoryIDs = ConvertHelper.ConvertToString(laptopHardware[columnMemoryIDs]),
                                            MotherBoardID = ConvertHelper.ConvertToInteger(laptopHardware[columnMotherBoardID]),
                                            HardDriveIDs = ConvertHelper.ConvertToString(laptopHardware[columnHardDriveIDs]),
                                            ChipsetID = ConvertHelper.ConvertToInteger(laptopHardware[columnChipsetID]),
                                            VideoCardIDs = ConvertHelper.ConvertToString(laptopHardware[columnVideoCardIDs]),
                                            DisplayIDs = ConvertHelper.ConvertToString(laptopHardware[columnDisplayIDs]),
                                            MultimediaIDs = ConvertHelper.ConvertToString(laptopHardware[columnMultimediaIDs]),
                                            PortIDs = ConvertHelper.ConvertToString(laptopHardware[columnPortIDs]),
                                            SlotIDs = ConvertHelper.ConvertToString(laptopHardware[columnSlotIDs]),
                                            ChassisID = ConvertHelper.ConvertToInteger(laptopHardware[columnChassisID]),
                                            PowerIDs = ConvertHelper.ConvertToString(laptopHardware[columnPowerIDs]),
                                            CPUName = DataRowHelper.ConvertToString(laptopHardware[columnCPU]),
                                            MemoryName = DataRowHelper.ConvertToString(laptopHardware[ columnMemory]),
                                            MotherBoardName = DataRowHelper.ConvertToString(laptopHardware[ columnMotherBoard]),
                                            HardDriveName = DataRowHelper.ConvertToString(laptopHardware[ columnHardDrive]),
                                            ChipsetName = DataRowHelper.ConvertToString(laptopHardware[ columnChipsetID]),
                                            VideoCardName = DataRowHelper.ConvertToString(laptopHardware[ columnVideoCard]),
                                            DisplayName = DataRowHelper.ConvertToString(laptopHardware[ columnDisplay]),
                                            MultimediaIDName = DataRowHelper.ConvertToString(laptopHardware[ columnMultimedia]),
                                            PortName = DataRowHelper.ConvertToString(laptopHardware[ columnPort]),
                                            SlotName = DataRowHelper.ConvertToString(laptopHardware[ columnSlot]),
                                            ChassisName = DataRowHelper.ConvertToString(laptopHardware[ columnChassis]),
                                            PowerName = DataRowHelper.ConvertToString(laptopHardware[ columnPower]),
                                            Manufacturer = ConvertHelper.ConvertToString(laptopHardware[columnManufacturer]),
                                            Core = ConvertHelper.ConvertToInteger(laptopHardware[columnCore]),
                                            FullNotes = ConvertHelper.ConvertToString(laptopHardware[columnNotes]),
                                            StatusID = ConvertHelper.ConvertToInteger(laptopHardware[columnStatusID]),
                                            CreatedBy = ConvertHelper.ConvertToInteger(laptopHardware[columnCreatedBy]),
                                            CreatedOn = ConvertHelper.ConvertToDateTime(laptopHardware[columnCreatedOn]),
                                            ModifiedBy = ConvertHelper.ConvertToInteger(laptopHardware[columnModifiedBy]),
                                            ModifiedOn = ConvertHelper.ConvertToDateTime(laptopHardware[columnModifiedOn]),


                                            CPU = (from DataRow cpu in globalMasterDetailsDT.Rows
                                                   where cpu.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptopHardware[columnCPUID])
                                                               select (new GlobalMasterDetail
                                                               {
                                                                   MasterDetailID = DataRowHelper.ConvertToInteger(cpu[columnMasterDetailID]),
                                                                   MasterValue = DataRowHelper.ConvertToString(cpu[columnMasterValue])
                                                               })).FirstOrDefault(),

                                            MotherBoard = (from DataRow motherBoard in globalMasterDetailsDT.Rows
                                                           where motherBoard.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptopHardware[columnMotherBoardID])
                                                   select (new GlobalMasterDetail
                                                   {
                                                       MasterDetailID = DataRowHelper.ConvertToInteger(motherBoard[columnMasterDetailID]),
                                                       MasterValue = DataRowHelper.ConvertToString(motherBoard[columnMasterValue])
                                                   })).FirstOrDefault(),

                                            Chipset = (from DataRow chipset in globalMasterDetailsDT.Rows
                                                       where chipset.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptopHardware[columnChipsetID])
                                                           select (new GlobalMasterDetail
                                                           {
                                                               MasterDetailID = DataRowHelper.ConvertToInteger(chipset[columnMasterDetailID]),
                                                               MasterValue = DataRowHelper.ConvertToString(chipset[columnMasterValue])
                                                           })).FirstOrDefault(),

                                            Chassis = (from DataRow chassis in globalMasterDetailsDT.Rows
                                                       where chassis.Field<int>(columnMasterDetailID) == DataRowHelper.ConvertToInteger(laptopHardware[columnChassisID])
                                                           select (new GlobalMasterDetail
                                                           {
                                                               MasterDetailID = DataRowHelper.ConvertToInteger(chassis[columnMasterDetailID]),
                                                               MasterValue = DataRowHelper.ConvertToString(chassis[columnMasterValue])
                                                           })).FirstOrDefault(),



                                            Memorys = (from DataRow memorys in laptopHardwaresMemoryDT.Rows
                                                       where memorys.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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

                                            VideoCards = (from DataRow videoCards in laptopHardwareVideoCardDT.Rows
                                                         where videoCards.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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

                                            Displays = (from DataRow displays in laptopHardwareDisplayDT.Rows
                                                       where displays.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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

                                            Multimedias = (from DataRow multimedias in laptopHardwareMultimediaDT.Rows
                                                          where multimedias.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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


                                            Ports = (from DataRow ports in laptopHardwarePortDT.Rows
                                                    where ports.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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

                                            Slots = (from DataRow slots in laptopHardwareSlotDT.Rows
                                                     where slots.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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
                                                                          notesDetail.Field<int>(columnNotesClientID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
                                                                          select (new NotesDetail
                                                                          {
                                                                              NotesDetailID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesDetailID]),
                                                                              NotesMasterID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesMasterID]),
                                                                              NotesClientID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesClientID]),
                                                                              Notes = DataRowHelper.ConvertToString(notesDetail[columnNotes])
                                                                          })).ToList(),
                                                   })).FirstOrDefault(),

                                            Powers = (from DataRow powers in laptopHardwarePowerDT.Rows
                                                      where powers.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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


                                            HardDrives = (from DataRow hardDrives in laptopHardwareHardDriveDT.Rows
                                                          where hardDrives.Field<int>(columnLaptopHardwareID) == DataRowHelper.ConvertToInteger(laptopHardware[columnLaptopHardwareID])
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
            return laptopHardwareList;
        }

        #region [Get User And User Attribute Details By UserID]

        public LaptopHardware GetLaptopHardwarAndUserDetailsByLaptopHardwarID(int LaptopHardwareID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@LaptopHardwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(LaptopHardwareID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPLaptopHardwareByLaptopHardwareID_List, parameters);
                if (ds != null)
                {
                    //laptopHardware = ConvertAllUserAttributesToObject(ds);
                    laptopHardwareList = ProcessDataSet(ds);
                }
                return laptopHardwareList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion

        #region [ private methods ]
        ////Parses the data reader and converts to object
        //private List<LaptopHardware> ProcessDataReader(SqlDataReader reader)
        //{
        //    if (!reader.IsClosed && reader.HasRows)
        //    {
        //        laptopHardwareList = new List<LaptopHardware>();
        //        while (reader.Read())
        //            laptopHardwareList.Add(ConvertToObject(reader));
        //        return laptopHardwareList;
        //    }
        //    return null;
        //}

        ////Converts each data record into object
        //private LaptopHardware ConvertToObject(IDataRecord dataRecord)
        //{
        //    laptopHardware = new LaptopHardware();

        //    laptopHardware.Port = new GlobalMasterDetail();
        //    laptopHardware.CPU = new GlobalMasterDetail();
        //    laptopHardware.Memory = new GlobalMasterDetail();
        //    laptopHardware.MotherBoard = new GlobalMasterDetail();
        //    laptopHardware.HardDrive = new GlobalMasterDetail();
        //    laptopHardware.Chipset = new GlobalMasterDetail();
        //    laptopHardware.VideoCard = new GlobalMasterDetail();
        //    laptopHardware.Display = new GlobalMasterDetail();
        //    laptopHardware.Multimedia = new GlobalMasterDetail();
        //    laptopHardware.Port = new GlobalMasterDetail();
        //    laptopHardware.Slot = new GlobalMasterDetail();
        //    laptopHardware.Chassis = new GlobalMasterDetail();
        //    laptopHardware.Power = new GlobalMasterDetail();

        //    laptopHardware.LaptopHardwareID = DataRowHelper.ConvertToInteger(dataRecord, columnLaptopHardwareID);
        //    laptopHardware.HostName = DataRowHelper.ConvertToString(dataRecord, columnHostName);
        //    laptopHardware.SerialNumber = DataRowHelper.ConvertToString(dataRecord, columnSerialNumber);

        //    laptopHardware.ModelName = DataRowHelper.ConvertToString(dataRecord, columnLaptopHardwareModelID);
        //    laptopHardware.CPUName = DataRowHelper.ConvertToString(dataRecord, columnCPUID);
        //    laptopHardware.MemoryName = DataRowHelper.ConvertToString(dataRecord, columnMemoryID);
        //    laptopHardware.MotherBoardName = DataRowHelper.ConvertToString(dataRecord, columnMotherBoardID);
        //    laptopHardware.HardDriveName = DataRowHelper.ConvertToString(dataRecord, columnHardDriveID);
        //    laptopHardware.ChipsetName = DataRowHelper.ConvertToString(dataRecord, columnChipsetID);
        //    laptopHardware.VideoCardName = DataRowHelper.ConvertToString(dataRecord, columnVideoCardID);
        //    laptopHardware.DisplayName = DataRowHelper.ConvertToString(dataRecord, columnDisplay);
        //    laptopHardware.MultimediaIDName = DataRowHelper.ConvertToString(dataRecord, columnMultimediaID);
        //    laptopHardware.PortName = DataRowHelper.ConvertToString(dataRecord, columnPortID);
        //    laptopHardware.SlotName = DataRowHelper.ConvertToString(dataRecord, columnSlotID);
        //    laptopHardware.ChassisName = DataRowHelper.ConvertToString(dataRecord, columnChassisD);
        //    laptopHardware.PowerName = DataRowHelper.ConvertToString(dataRecord, columnPowerID);
        //    laptopHardware.Manufacturer = DataRowHelper.ConvertToString(dataRecord, columnManufacturer);

        //    laptopHardware.View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Laptops&opp=SH&id=" + ConvertHelper.ConvertToString(laptopHardware.LaptopHardwareID) + " style='color: blue;text-decoration: underline;'>More</a>");
        //    //laptopHardware.FullNotes = ConvertHelper.ConvertToString(dataRecord, columnNotes);

        //    laptopHardware.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
        //    laptopHardware.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
        //    laptopHardware.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
        //    laptopHardware.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
        //    laptopHardware.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);

        //    return laptopHardware;
        //}

        //Converts each data set into object
        private LaptopHardware ConvertAllUserAttributesToObject(DataSet ds)
        {
            laptopHardware = new LaptopHardware();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    laptopHardware.LaptopHardwareID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["LaptopHardwareID"]);
                    laptopHardware.HostName = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["HostName"]);
                    laptopHardware.SerialNumber = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["SerialNumber"]);

                    laptopHardware.ModelName = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Model"]);
                    laptopHardware.CPUID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["CPUID"]);
                    laptopHardware.MemoryIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["MemoryIDs"]);
                    laptopHardware.MotherBoardID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["MotherBoardID"]);
                    laptopHardware.HardDriveIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["HardDriveIDs"]);
                    laptopHardware.ChipsetID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ChipsetID"]);
                    laptopHardware.VideoCardIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["VideoCardIDs"]);
                    laptopHardware.DisplayIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["DisplayIDs"]);
                    laptopHardware.MultimediaIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["MultimediaIDs"]);
                    laptopHardware.PortIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["PortIDs"]);
                    laptopHardware.SlotIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["SlotIDs"]);
                    laptopHardware.ChassisID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ChassisID"]);
                    laptopHardware.PowerIDs = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["PowerIDs"]);
                    laptopHardware.Manufacturer = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Manufacturer"]);
                    laptopHardware.Core = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["Core"]);

                    laptopHardware.FullNotes = ConvertHelper.ConvertToString(ds.Tables[0].Rows[0]["Notes"]);

                    laptopHardware.StatusID = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["StatusID"]);
                    laptopHardware.CreatedBy = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["CreatedBy"]);
                    laptopHardware.CreatedOn = ConvertHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]);
                    laptopHardware.ModifiedBy = ConvertHelper.ConvertToInteger(ds.Tables[0].Rows[0]["ModifiedBy"]);
                    laptopHardware.ModifiedOn = ConvertHelper.ConvertToDateTime(ds.Tables[0].Rows[0]["ModifiedOn"]);
                }
            }

            return laptopHardware;
        }
        #endregion
    }
}
