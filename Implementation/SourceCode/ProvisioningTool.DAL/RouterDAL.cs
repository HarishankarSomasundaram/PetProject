using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProvisioningTool.DAL
{
    internal class RouterDAL
    {
        #region [ Declarations ]
        private List<Router> routerList;
        private readonly string columnRouterID = "RouterID";
        private readonly string columnHostname = "Hostname";
        private readonly string columnRouterModel = "RouterModel";
        private readonly string columnSerialNumber = "SerialNumber";
        private readonly string columnInstalledOn = "InstalledOn";
        private readonly string columnWarrantyExpiresOn = "WarrantyExpiresOn";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnSubnet = "Subnet";
        private readonly string columnGateway = "Gateway";
        private readonly string columnAdminUserName = "AdminUserName";
        private readonly string columnAdminPassword = "AdminPassword";
        private readonly string columnOSVersion = "OSVersion";
        private readonly string columnFirmware = "Firmware";
        private readonly string columnModuleID = "ModuleID";
        private readonly string columnInterfaceID = "InterfaceID";
        private readonly string columnSiteToSiteID = "SiteToSiteID";
        private readonly string columnRouterKey = "RouterKey";
        private readonly string columnNotes = "Notes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnOSVersionID = "OSVersionID";
        private readonly string columnManufacture = "Manufacture";
        private readonly string columnMemory = "Memory";
        private readonly string columnModelID = "ModelID";
        private readonly string columnModelName = "ModelName";
        private readonly string columnOSVersionName = "OSVersionName";
        private readonly string columnRouterInterfaceID = "RouterInterfaceID";
        private readonly string columnInterfaceValue = "InterfaceValue";
        private readonly string columnModuleName = "ModuleName";
        private readonly string columnRouterInterfaces = "RouterInterfaces";
        private readonly string columnRouterModules = "RouterModules";
        private readonly string columnRouterNotes = "RouterNotes";
        private readonly string columnNotesMasterName = "NotesMasterName";
        private readonly string columnRouterInfo = "RouterInfo";

        private readonly string columnNotesMasterID = "NotesMasterID";
        private readonly string columnNotesDetailID = "NotesDetailID";
        private readonly string columnNotesClientID = "NotesClientID";
        private readonly string columnRouterSiteToSites = "RouterSiteToSites";
        private readonly string columnDocumentPath = "DocumentPath";

        #endregion [ Declarations ]

        internal RouterDAL()
        {
        }

        #region [ Add Router ]
        internal Router AddRouter(Router router, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(router.Hostname);

                parameters[1] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(router.Manufacture);

                parameters[2] = new SqlParameter("@Memory", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(router.Memory);

                parameters[3] = new SqlParameter("@ModelID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(router.RouterModel.MasterDetailID);

                parameters[4] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(router.SerialNumber);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.Date);
                parameters[5].Value = DBValueHelper.ConvertToDBDate(router.InstalledOn);

                parameters[6] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.Date);
                parameters[6].Value = DBValueHelper.ConvertToDBDate(router.WarrantyExpiresOn);

                parameters[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(router.IPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(router.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(router.Gateway);

                parameters[10] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(router.AdminUserName);

                parameters[11] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(router.AdminPassword);

                parameters[12] = new SqlParameter("@OSVersionID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(router.OSVersion.MasterDetailID);

                parameters[13] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(router.Firmware);

                parameters[14] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(router.CreatedBy);

                parameters[15] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(router.RouterModules);

                parameters[16] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(router.RouterInterfaces);

                parameters[17] = new SqlParameter("@SiteToSites", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(router.RouterSiteToSites);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(router.RouterNotes);

                parameters[19] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(router.Documents.Type);

                parameters[20] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(router.Documents.DocumentType);

                parameters[21] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(router.Documents.DocumentName);

                parameters[22] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(router.Documents.DocumentPath);

                parameters[23] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[23].Value = DBValueHelper.ConvertToDBInteger(router.Site.SiteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPRouterAdd, parameters);
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
                return router;

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

        #region [ Modify Router By RouterID ]
        internal Router ModifyRouter(Router router, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(router.Hostname);

                parameters[1] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(router.Manufacture);

                parameters[2] = new SqlParameter("@Memory", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(router.Memory);

                parameters[3] = new SqlParameter("@ModelID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(router.RouterModel.MasterDetailID);

                parameters[4] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(router.SerialNumber);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(router.InstalledOn);

                parameters[6] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(router.WarrantyExpiresOn);

                parameters[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(router.IPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(router.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(router.Gateway);

                parameters[10] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(router.AdminUserName);

                parameters[11] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(router.AdminPassword);

                parameters[12] = new SqlParameter("@OSVersionID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(router.OSVersion.MasterDetailID);

                parameters[13] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(router.Firmware);

                parameters[14] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(router.ModifiedBy);

                parameters[15] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(router.RouterModules);

                parameters[16] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(router.RouterInterfaces);

                parameters[17] = new SqlParameter("@SiteToSites", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(router.RouterSiteToSites);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(router.RouterNotes);

                parameters[19] = new SqlParameter("@RouterID", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(router.RouterID);

                parameters[20] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(router.Documents.Type);

                parameters[21] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(router.Documents.DocumentType);

                parameters[22] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(router.Documents.DocumentName);

                parameters[23] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[23].Value = DBValueHelper.ConvertToDBString(router.Documents.DocumentPath);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPRouterUpdateByRouterID, parameters);
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
                return router;

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
        #endregion [ Modify Router By RouterID ]

        #region[ Delete Router By RouterID ]
        //Delete/Update Status to 2 the Router from the DB based on the given parameters
        public bool DeleteRouterByRouterID(int routerID)
        {
            SqlDataReader reader = null;
            bool isDeleted = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@RouterId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(routerID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPRouterDeleteByRouterID, parameters);
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
        #endregion[ Delete Router By RouterID ]

        #region [ Get Router By RouterID ]

        public List<Router> GetRouterByRouterID(int routerID)
        {
            DataSet ds = new DataSet();
            routerList = new List<Router>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@RouterID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(routerID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPRouterByRouterID_List, parameters);
                if (ds != null)
                {
                    routerList = ProcessDataSet(ds);
                }
                return routerList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ Get Router By RouterID ]

        #region [Get All Routers]
        public List<Router> GetAllRouters(int siteID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPRouter_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                return routerList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ GET ALL Router ]

        #region [ ProcessDataSet]
        //Parses the data reader and converts to object
        private List<Router> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        #region [ ConvertToObject]
        private List<Router> ConvertToObject(DataSet ds)
        {
            routerList = new List<Router>();


            DataTable routerDT = new DataTable();
            DataTable GlobalMasterDetailDT = new DataTable();
            DataTable routerInterfaceDT = new DataTable();
            DataTable routerModulesDT = new DataTable();
            DataTable routerNotesDT = new DataTable();
            DataTable routerNotesDetailDT = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    routerDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    GlobalMasterDetailDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    routerInterfaceDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    routerModulesDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    routerNotesDT = ds.Tables[4];
                if (ds.Tables[5] != null)
                    routerNotesDetailDT = ds.Tables[5];

                if (routerDT.Rows.Count > 0)
                {
                    routerList = (from DataRow router in routerDT.Rows

                                  select new Router
                                  {
                                      RouterID = DataRowHelper.ConvertToInteger(router[columnRouterID]),
                                      Hostname = DataRowHelper.ConvertToString(router[columnHostname]),
                                      Manufacture = DataRowHelper.ConvertToString(router[columnManufacture]),
                                      Memory = DataRowHelper.ConvertToString(router[columnMemory]),
                                      RouterModel = (from DataRow routerModel in GlobalMasterDetailDT.Rows
                                                     where routerModel.Field<int>(columnModelID) == DataRowHelper.ConvertToInteger(router[columnModelID])
                                                     select (new GlobalMasterDetail
                                                     {
                                                         MasterDetailID = DataRowHelper.ConvertToInteger(routerModel[columnModelID]),
                                                         MasterValue = DataRowHelper.ConvertToString(routerModel[columnModelName])
                                                     })).FirstOrDefault(),
                                      SerialNumber = DataRowHelper.ConvertToString(router[columnSerialNumber]),
                                      InstalledOn = DataRowHelper.ConvertToString(router[columnInstalledOn]),
                                      WarrantyExpiresOn = DataRowHelper.ConvertToString(router[columnWarrantyExpiresOn]),
                                      IPAddress = DataRowHelper.ConvertToString(router[columnIPAddress]),
                                      Subnet = DataRowHelper.ConvertToString(router[columnSubnet]),
                                      Gateway = DataRowHelper.ConvertToString(router[columnGateway]),
                                      AdminUserName = DataRowHelper.ConvertToString(router[columnAdminUserName]),
                                      AdminPassword = DataRowHelper.ConvertToString(router[columnAdminPassword]),
                                      OSVersion = (from DataRow osVersion in GlobalMasterDetailDT.Rows
                                                   where osVersion.Field<int>(columnOSVersionID) == DataRowHelper.ConvertToInteger(router[columnOSVersionID])
                                                   select (new GlobalMasterDetail
                                                   {
                                                       MasterDetailID = DataRowHelper.ConvertToInteger(osVersion[columnOSVersionID]),
                                                       MasterValue = DataRowHelper.ConvertToString(osVersion[columnOSVersionName])
                                                   })).FirstOrDefault(),
                                      Firmware = DataRowHelper.ConvertToString(router[columnFirmware]),
                                      StatusID = DataRowHelper.ConvertToInteger(router[columnStatusID]),
                                      CreatedBy = DataRowHelper.ConvertToInteger(router[columnCreatedBy]),
                                      CreatedOn = DataRowHelper.ConvertToDateTime(router[columnCreatedOn]),
                                      ModifiedBy = DataRowHelper.ConvertToInteger(router[columnModifiedBy]),
                                      ModifiedOn = DataRowHelper.ConvertToDateTime(router[columnModifiedOn]),
                                      RouterInterfaceList = (from DataRow routerInterface in routerInterfaceDT.Rows
                                                             where routerInterface.Field<int>(columnRouterID) == DataRowHelper.ConvertToInteger(router[columnRouterID])
                                                             select (new RouterInterface
                                                             {
                                                                 RouterID = DataRowHelper.ConvertToInteger(routerInterface[columnRouterID]),
                                                                 RouterInterfaceID = DataRowHelper.ConvertToInteger(routerInterface[columnRouterInterfaceID]),
                                                                 InterfaceValue = DataRowHelper.ConvertToString(routerInterface[columnInterfaceValue])
                                                             })).ToList(),
                                      RouterInterfaces = DataRowHelper.ConvertToString(router[columnRouterInterfaces]),
                                      RouterModuleList = (from DataRow routerModule in routerModulesDT.Rows
                                                          where routerModule.Field<int>(columnRouterID) == DataRowHelper.ConvertToInteger(router[columnRouterID])
                                                          select (new RouterModule
                                                          {
                                                              RouterID = DataRowHelper.ConvertToInteger(routerModule[columnRouterID]),
                                                              RouterModuleID = DataRowHelper.ConvertToInteger(routerModule[columnModuleID]),
                                                              Module = (from DataRow module in GlobalMasterDetailDT.Rows
                                                                        where module.Field<int>(columnModuleID) == DataRowHelper.ConvertToInteger(routerModule[columnModuleID])
                                                                        select (new GlobalMasterDetail
                                                                        {
                                                                            MasterDetailID = DataRowHelper.ConvertToInteger(module[columnModuleID]),
                                                                            MasterValue = DataRowHelper.ConvertToString(module[columnModuleName])
                                                                        })).FirstOrDefault()
                                                          })).ToList(),
                                      RouterModules = DataRowHelper.ConvertToString(router[columnRouterModules]),
                                      ViewDocumentPath = DataRowHelper.ConvertToString(router[columnDocumentPath]),
                                      Notes = (from DataRow routerNotes in routerNotesDT.Rows
                                               where routerNotes.Field<string>(columnNotesMasterName) == DataRowHelper.ConvertToString(columnRouterInfo)
                                               select (new NotesMaster
                                               {
                                                   NotesMasterID = DataRowHelper.ConvertToInteger(routerNotes[columnNotesMasterID]),
                                                   NotesMasterName = DataRowHelper.ConvertToString(routerNotes[columnNotesMasterName]),
                                                   NotesDetailList = (from DataRow notesDetail in routerNotesDetailDT.Rows
                                                                      where notesDetail.Field<int>(columnNotesMasterID) == DataRowHelper.ConvertToInteger(routerNotes[columnNotesMasterID]) &&
                                                                      notesDetail.Field<int>(columnNotesClientID) == DataRowHelper.ConvertToInteger(router[columnRouterID])
                                                                      select (new NotesDetail
                                                                      {
                                                                          NotesDetailID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesDetailID]),
                                                                          NotesMasterID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesMasterID]),
                                                                          NotesClientID = DataRowHelper.ConvertToInteger(notesDetail[columnNotesClientID]),
                                                                          Notes = DataRowHelper.ConvertToString(notesDetail[columnNotes])
                                                                      })).ToList()
                                               })).FirstOrDefault(),
                                      RouterNotes = DataRowHelper.ConvertToString(router[columnRouterNotes]),
                                      //RouterSiteToSiteList = (from DataRow routerModule in routerModulesDT.Rows
                                      //                        where routerModule.Field<int>(columnRouterID) == DataRowHelper.ConvertToInteger(router[columnRouterID])
                                      //                        select (new RouterModule
                                      //                        {
                                      //                            RouterID = DataRowHelper.ConvertToInteger(routerModule[columnRouterID]),
                                      //                            RouterModuleID = DataRowHelper.ConvertToInteger(routerModule[columnModuleID]),
                                      //                            Module = (from DataRow module in GlobalMasterDetailDT.Rows
                                      //                                      where module.Field<int>(columnModuleID) == DataRowHelper.ConvertToInteger(routerModule[columnModuleID])
                                      //                                      select (new GlobalMasterDetail
                                      //                                      {
                                      //                                          MasterDetailID = DataRowHelper.ConvertToInteger(module[columnModuleID]),
                                      //                                          MasterValue = DataRowHelper.ConvertToString(module[columnModuleName])
                                      //                                      })).FirstOrDefault()
                                      //                        })).ToList(),
                                      RouterSiteToSites = DataRowHelper.ConvertToString(router[columnRouterSiteToSites]),
                                      
                                      
                                      //This Static binding is for Showing the View Col in Jq Grid
                                      View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Routers&id=" + DataRowHelper.ConvertToString(router[columnRouterID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                  }).ToList();
                }
            }
            return routerList;
        }
        #endregion [ ConvertToObject ]
    }
}
