using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProvisioningTool.DAL
{

    internal class PrinterDAL
    {
        #region [ Declarations ]
        private List<Printer> PrinterList;

        private readonly string columnPrinterID = "PrinterID";
        private readonly string columnManufacture = "Manufacture";
        private readonly string columnHostname = "Hostname";
        private readonly string columnPrinterModel = "PrinterModel";
        private readonly string columnModelID = "ModelID";
        private readonly string columnModelName = "ModelName";

        private readonly string columnSerialNumber = "SerialNumber";
        private readonly string columnInstalledOn = "InstalledOn";
        private readonly string columnWarrantyExpiresOn = "WarrantyExpiresOn";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnSubnet = "Subnet";
        private readonly string columnGateway = "Gateway";
        private readonly string columnAdminUserName = "AdminUserName";
        private readonly string columnAdminPassword = "AdminPassword";
        private readonly string columnOSVersion = "OSVersion";
        private readonly string columnOSVersionID = "OSVersionID";
        private readonly string columnOSVersionName = "OSVersionName";
        private readonly string columnFirmware = "Firmware";
        private readonly string columnModuleID = "ModuleID";
        private readonly string columnPrinterModuleIDS = "PrinterModuleIDS";
        private readonly string columnModuleName = "ModuleName";
        private readonly string columnInterfaceID = "InterfaceID";
        private readonly string columnSiteToSiteID = "PrinterSiteID";
        private readonly string columnSiteToSiteValue = "SiteToSiteValue";
        private readonly string columnAssignedUsers = "AssignedUsers";
        private readonly string columnPrinterKey = "PasswordKey";
        private readonly string columnPrinterInterfaceID = "PrinterInterfaceID";
        private readonly string columnInterfaceValue = "InterfaceValue";
        private readonly string columnPrinterInterfaces = "PrinterInterfaces";
        private readonly string columnPrinterNotes = "PrinterNotes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";

        private readonly string columnSystemAssignedUserID = "SystemAssignedUserID";
        private readonly string columnClientID = "ClientID";
        private readonly string columnSystemMasterID = "SystemMasterID";
        private readonly string columnSystemMasterName = "SystemMasterName";
        private readonly string columnSystemID = "SystemID";
        private readonly string columnUserID = "UserID";
        private readonly string columnFirstName = "UserFirstName";
        private readonly string columnUserName = "UserUserName";
        private readonly string columnUserUserID = "UserUserID";
        private readonly string columnDocumentPath = "DocumentPath";

        #endregion [ Declarations ]

        internal PrinterDAL()
        {
        }

        #region [ Add Printer ]
        internal Printer AddPrinter(Printer Printer, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[26];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(Printer.Hostname);

                parameters[1] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(Printer.Manufacture);

                parameters[2] = new SqlParameter("@ModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(Printer.PrinterModel.MasterDetailID);

                parameters[4] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(Printer.SerialNumber);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.DateTime);
                parameters[5].Value = DBValueHelper.ConvertToDBDate(Printer.InstalledOn);

                parameters[6] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.DateTime);
                parameters[6].Value = DBValueHelper.ConvertToDBDate(Printer.WarrantyExpiresOn);

                parameters[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(Printer.IPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(Printer.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(Printer.Gateway);

                parameters[10] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(Printer.AdminUserName);

                parameters[11] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(Printer.AdminPassword);

                parameters[12] = new SqlParameter("@OSVersionID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(Printer.OSVersion.MasterDetailID);

                parameters[13] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(Printer.Firmware);

                parameters[14] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(Printer.PrinterModules);

                parameters[15] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(Printer.PrinterInterfaces);

                parameters[16] = new SqlParameter("@AssignedUsers", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(Printer.AssignedUsers);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(Printer.PrinterNotes);

                parameters[19] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(Printer.StatusID);

                parameters[20] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(Printer.CreatedBy);

                parameters[21] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[21].Value = DBValueHelper.ConvertToDBInteger(Printer.Site.SiteID);

                //DOCUMENTS
                parameters[22] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(Printer.Documents.Type);

                parameters[23] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[23].Value = DBValueHelper.ConvertToDBString(Printer.Documents.DocumentType);

                parameters[24] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[24].Value = DBValueHelper.ConvertToDBString(Printer.Documents.DocumentName);

                parameters[25] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[25].Value = DBValueHelper.ConvertToDBString(Printer.Documents.DocumentPath);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPrinterAdd, parameters);
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
                return Printer;

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
        #endregion [ Add Printer ]

        #region [ Update Printer ]
        internal Printer ModifyPrinter(Printer Printer, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[23];

                parameters[0] = new SqlParameter("@PrinterID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(Printer.PrinterID);

                parameters[1] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(Printer.Hostname);

                parameters[2] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(Printer.Manufacture);

                parameters[3] = new SqlParameter("@ModelID", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(Printer.PrinterModel.MasterDetailID);

                parameters[4] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(Printer.ModifiedBy);

                parameters[5] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(Printer.SerialNumber);

                parameters[6] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(Printer.InstalledOn);

                parameters[7] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(Printer.WarrantyExpiresOn);

                parameters[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(Printer.IPAddress);

                parameters[9] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(Printer.Subnet);

                parameters[10] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(Printer.Gateway);

                parameters[11] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(Printer.AdminUserName);

                parameters[12] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(Printer.AdminPassword);

                parameters[13] = new SqlParameter("@OSVersionID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(Printer.OSVersion.MasterDetailID);

                parameters[14] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(Printer.Firmware);

                parameters[15] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(Printer.PrinterModules);

                parameters[16] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(Printer.PrinterInterfaces);

                parameters[17] = new SqlParameter("@AssignedUsers", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(Printer.AssignedUsers);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(Printer.PrinterNotes);

                //DOCUMENTS
                parameters[19] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(Printer.Documents.Type);

                parameters[20] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(Printer.Documents.DocumentType);

                parameters[21] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(Printer.Documents.DocumentName);

                parameters[22] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(Printer.Documents.DocumentPath);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPrinterUpdate, parameters);
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
                return Printer;

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
        #endregion [ Update Printer ]

        #region[ Delete Printer By PrinterID ]
        //Delete/Update Status to the Printer from the DB based on the given parameters
        public bool DeletePrinterByPrinterID(int PrinterID)
        {
            SqlDataReader reader = null;
            bool isDeleted = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@PrinterId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(PrinterID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeletePrinterByPrinterID, parameters);
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
        #endregion[ Delete Printer By PrinterID ]

        #region [ Get Printer By PrinterID ]

        public Printer GetPrinterByPrinterID(int PrinterID)
        {
            DataSet ds = new DataSet();
            PrinterList = new List<Printer>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@PrinterID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(PrinterID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPrinterByPrinterID_List, parameters);
                if (ds != null)
                {
                    PrinterList = ProcessDataSet(ds);
                    if (PrinterList.Count > 0)
                    {
                        return PrinterList[0];
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
        #endregion [ Get Printer By PrinterID ]

        #region [Get All Printers]
        public List<Printer> GetAllPrinters(int siteID, string searchFilter)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);

                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPPrinter_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                return PrinterList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ GET ALL Printer ]

        #region [ ProcessDataSet]
        //Parses the data reader and converts to object
        private List<Printer> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        #region [ ConvertToObject]
        private List<Printer> ConvertToObject(DataSet ds)
        {
            PrinterList = new List<Printer>();


            DataTable PrinterDT = new DataTable();
            DataTable GlobalMasterDetailDT = new DataTable();
            DataTable PrinterInterfaceDT = new DataTable();
            DataTable PrinterModulesDT = new DataTable();
            DataTable assignedUserDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    PrinterDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    GlobalMasterDetailDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    PrinterInterfaceDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    PrinterModulesDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    assignedUserDT = ds.Tables[4];

                if (PrinterDT.Rows.Count > 0)
                {
                    PrinterList = (from DataRow Printer in PrinterDT.Rows

                                   select new Printer
                                   {
                                       PrinterID = DataRowHelper.ConvertToInteger(Printer[columnPrinterID]),
                                       Hostname = DataRowHelper.ConvertToString(Printer[columnHostname], ""),
                                       Manufacture = DataRowHelper.ConvertToString(Printer[columnManufacture], ""),
                                       PrinterModel = (from DataRow PrinterModel in GlobalMasterDetailDT.Rows
                                                       where PrinterModel.Field<int>(columnModelID) == DataRowHelper.ConvertToInteger(Printer[columnModelID])
                                                       select (new GlobalMasterDetail
                                                       {
                                                           MasterDetailID = DataRowHelper.ConvertToInteger(PrinterModel[columnModelID]),
                                                           MasterValue = DataRowHelper.ConvertToString(PrinterModel[columnModelName], "")
                                                       })).FirstOrDefault(),

                                       SerialNumber = DataRowHelper.ConvertToString(Printer[columnSerialNumber]),
                                       InstalledOn = DataRowHelper.ConvertToString(Printer[columnInstalledOn]),
                                       WarrantyExpiresOn = DataRowHelper.ConvertToString(Printer[columnWarrantyExpiresOn]),
                                       IPAddress = DataRowHelper.ConvertToString(Printer[columnIPAddress], ""),
                                       Subnet = DataRowHelper.ConvertToString(Printer[columnSubnet], ""),
                                       Gateway = DataRowHelper.ConvertToString(Printer[columnGateway], ""),
                                       AdminUserName = DataRowHelper.ConvertToString(Printer[columnAdminUserName], ""),
                                       AdminPassword = DataRowHelper.ConvertToString(Printer[columnAdminPassword], ""),
                                       PrinterModules = DataRowHelper.ConvertToString(Printer[columnPrinterModuleIDS], ""),
                                       PrinterModuleList = (from DataRow PrinterModule in PrinterModulesDT.Rows
                                                            where PrinterModule.Field<int>(columnPrinterID) == DataRowHelper.ConvertToInteger(Printer[columnPrinterID])
                                                            select (new PrinterModule
                                                            {
                                                                PrinterID = DataRowHelper.ConvertToInteger(PrinterModule[columnPrinterID]),
                                                                PrinterModuleID = DataRowHelper.ConvertToInteger(PrinterModule[columnModuleID]),
                                                                Module = (from DataRow module in GlobalMasterDetailDT.Rows
                                                                          where module.Field<int>(columnModuleID) == DataRowHelper.ConvertToInteger(PrinterModule[columnModuleID])
                                                                          select (new GlobalMasterDetail
                                                                          {
                                                                              MasterDetailID = DataRowHelper.ConvertToInteger(module[columnModuleID]),
                                                                              MasterValue = DataRowHelper.ConvertToString(module[columnModuleName], "")
                                                                          })).FirstOrDefault()
                                                            })).ToList(),
                                       AssignedUsers = DataRowHelper.ConvertToString(Printer[columnAssignedUsers], ""),
                                       PrinterAssignedUserList = (from DataRow userPrinter in assignedUserDT.Rows
                                                                  where userPrinter.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(Printer[columnPrinterID])
                                                                  select (new AssignedUser
                                                                  {
                                                                      SystemAssignedUserID = DataRowHelper.ConvertToInteger(userPrinter[columnUserID]),
                                                                      System = (from DataRow system in assignedUserDT.Rows
                                                                                where system.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(userPrinter[columnSystemID])
                                                                                select (new SystemMaster
                                                                                {
                                                                                    SystemMasterID = DataRowHelper.ConvertToInteger(system[columnSystemMasterID]),
                                                                                    SystemMasterName = DataRowHelper.ConvertToString(system[columnSystemMasterName], "")
                                                                                })).FirstOrDefault(),
                                                                      ClientID = DataRowHelper.ConvertToInteger(userPrinter[columnClientID]),
                                                                      User = (from DataRow user in assignedUserDT.Rows
                                                                              where user.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(userPrinter[columnUserUserID])
                                                                              select (new User
                                                                              {
                                                                                  UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                                                                  UserName = DataRowHelper.ConvertToString(user[columnUserName], ""),
                                                                                  FirstName = DataRowHelper.ConvertToString(user[columnFirstName], "")
                                                                              })).FirstOrDefault()
                                                                  })).ToList(),

                                       OSVersion = (from DataRow osVersion in GlobalMasterDetailDT.Rows
                                                    where osVersion.Field<int>(columnOSVersionID) == DataRowHelper.ConvertToInteger(Printer[columnOSVersionID])
                                                    select (new GlobalMasterDetail
                                                    {
                                                        MasterDetailID = DataRowHelper.ConvertToInteger(osVersion[columnOSVersionID]),
                                                        MasterValue = DataRowHelper.ConvertToString(osVersion[columnOSVersionName], "")
                                                    })).FirstOrDefault(),
                                       Firmware = DataRowHelper.ConvertToString(Printer[columnFirmware], ""),
                                       ViewDocumentPath = DataRowHelper.ConvertToString(Printer[columnDocumentPath]),
                                       PrinterInterfaces = DataRowHelper.ConvertToString(Printer[columnPrinterInterfaces], ""),
                                       PrinterInterfaceList = (from DataRow printerInterface in PrinterInterfaceDT.Rows
                                                               where printerInterface.Field<int>(columnPrinterID) == DataRowHelper.ConvertToInteger(Printer[columnPrinterID])
                                                               select (new PrinterInterface
                                                               {
                                                                   PrinterID = DataRowHelper.ConvertToInteger(printerInterface[columnPrinterID]),
                                                                   PrinterInterfaceID = DataRowHelper.ConvertToInteger(printerInterface[columnPrinterInterfaceID]),
                                                                   InterfaceValue = DataRowHelper.ConvertToString(printerInterface[columnInterfaceValue], "")
                                                               })).ToList(),

                                       PrinterNotes = DataRowHelper.ConvertToString(Printer[columnPrinterNotes], ""),
                                       StatusID = DataRowHelper.ConvertToInteger(Printer[columnStatusID]),
                                       CreatedBy = DataRowHelper.ConvertToInteger(Printer[columnCreatedBy]),
                                       CreatedOn = DataRowHelper.ConvertToDateTime(Printer[columnCreatedOn]),
                                       ModifiedBy = DataRowHelper.ConvertToInteger(Printer[columnModifiedBy]),
                                       ModifiedOn = DataRowHelper.ConvertToDateTime(Printer[columnModifiedOn]),

                                       //This Static binding is for Showing the View Col in Jq Grid
                                       View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Printers&id=" + DataRowHelper.ConvertToString(Printer[columnPrinterID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                   }).ToList();
                }
            }
            return PrinterList;
        }
        #endregion [ ConvertToObject ]




    }
}
