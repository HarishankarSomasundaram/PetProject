using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;
using Library;

namespace ProvisioningTool.DAL
{

    internal class FirewallDAL
    {
        #region [ Declarations ]
        private List<Firewall> firewallList;

        private readonly string columnFirewallID = "FirewallID";
        private readonly string columnManufacture = "Manufacture";
        private readonly string columnHostname = "Hostname";
        private readonly string columnFirewallModel = "FirewallModel";
        private readonly string columnModelID = "ModelID";
        private readonly string columnModelName = "ModelName";
        private readonly string columnMemory = "Memory";
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
        private readonly string columnModuleName = "ModuleName";
        private readonly string columnFirewallModuleIDS = "FirewallModuleIDS";
        private readonly string columnInterfaceID = "InterfaceID";
        private readonly string columnSiteToSiteID = "FirewallSiteID";
        private readonly string columnSiteToSiteValue = "SiteToSiteValue";
        private readonly string columnSiteToSitePassKey = "SiteToSitePassKey";
        private readonly string columnFirewallKey = "PasswordKey";
        private readonly string columnFirewallInterfaceID = "FirewallInterfaceID";
        private readonly string columnInterfaceValue = "InterfaceValue";
        private readonly string columnFirewallInterfaces = "FirewallInterfaces";
        private readonly string columnFirewallNotes = "FirewallNotes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnDocumentPath = "DocumentPath";

        #endregion [ Declarations ]

        internal FirewallDAL()
        {
        }

        #region [ Add Firewall ]
        internal Firewall AddFirewall(Firewall firewall, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                DateTime checkDate = new DateTime();

                SqlParameter[] parameters = new SqlParameter[26];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(firewall.Hostname);

                parameters[1] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(firewall.Manufacture);

                parameters[2] = new SqlParameter("@ModelID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(firewall.FirewallModel.MasterDetailID);

                parameters[3] = new SqlParameter("@Memory", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(firewall.Memory);

                parameters[4] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(firewall.SerialNumber);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.DateTime);

                if (firewall.InstalledOn.CompareTo(ConvertHelper.ConvertToString(checkDate)) != 0)
                    parameters[5].Value = DBValueHelper.ConvertToDBDate(firewall.InstalledOn);
                else
                    parameters[5].Value = DBNull.Value;

                parameters[6] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.DateTime);
                if (firewall.InstalledOn.CompareTo(ConvertHelper.ConvertToString(checkDate)) != 0)
                    parameters[6].Value = DBValueHelper.ConvertToDBDate(firewall.WarrantyExpiresOn);
                else
                    parameters[6].Value = DBNull.Value;



                parameters[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(firewall.IPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(firewall.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(firewall.Gateway);

                parameters[10] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(firewall.AdminUserName);

                parameters[11] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(firewall.AdminPassword);

                parameters[12] = new SqlParameter("@OSVersionID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(firewall.OSVersion.MasterDetailID);

                parameters[13] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(firewall.Firmware);

                parameters[14] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(firewall.FirewallModules);

                parameters[15] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(firewall.FirewallInterfaces);

                parameters[16] = new SqlParameter("@SiteToSiteValue", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(firewall.FirewallSiteToSites);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(firewall.FirewallNotes);

                parameters[19] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(firewall.StatusID);

                parameters[20] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(firewall.CreatedBy);

                parameters[21] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[21].Value = firewall.Site.SiteID;


                //DOCUMENTS
                parameters[22] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(firewall.Documents.Type);

                parameters[23] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[23].Value = DBValueHelper.ConvertToDBString(firewall.Documents.DocumentType);

                parameters[24] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[24].Value = DBValueHelper.ConvertToDBString(firewall.Documents.DocumentName);

                parameters[25] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[25].Value = DBValueHelper.ConvertToDBString(firewall.Documents.DocumentPath);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPFierwallAdd, parameters);
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
                return firewall;

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
        #endregion [ Add Firewall ]

        #region [ Update Firewall ]
        internal Firewall ModifyFirewall(Firewall firewall, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[24];

                parameters[0] = new SqlParameter("@FirewallID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(firewall.FirewallID);

                parameters[1] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(firewall.Hostname);

                parameters[2] = new SqlParameter("@Manufacture", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(firewall.Manufacture);

                parameters[3] = new SqlParameter("@ModelID", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(firewall.FirewallModel.MasterDetailID);

                parameters[4] = new SqlParameter("@Memory", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(firewall.Memory);

                parameters[5] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(firewall.SerialNumber);

                parameters[6] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(firewall.InstalledOn);

                parameters[7] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(firewall.WarrantyExpiresOn);

                parameters[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(firewall.IPAddress);

                parameters[9] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(firewall.Subnet);

                parameters[10] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(firewall.Gateway);

                parameters[11] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(firewall.AdminUserName);

                parameters[12] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(firewall.AdminPassword);

                parameters[13] = new SqlParameter("@OSVersionID", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(firewall.OSVersion.MasterDetailID);

                parameters[14] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(firewall.Firmware);

                parameters[15] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(firewall.FirewallModules);

                parameters[16] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(firewall.FirewallInterfaces);

                parameters[17] = new SqlParameter("@SiteToSiteValue", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(firewall.FirewallSiteToSites);

                parameters[18] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(firewall.FirewallNotes);

                parameters[19] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[19].Value = DBValueHelper.ConvertToDBInteger(firewall.ModifiedBy);


                //DOCUMENTS
                parameters[20] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(firewall.Documents.Type);

                parameters[21] = new SqlParameter("@DocumentType", SqlDbType.VarChar);
                parameters[21].Value = DBValueHelper.ConvertToDBString(firewall.Documents.DocumentType);

                parameters[22] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                parameters[22].Value = DBValueHelper.ConvertToDBString(firewall.Documents.DocumentName);

                parameters[23] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                parameters[23].Value = DBValueHelper.ConvertToDBString(firewall.Documents.DocumentPath);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPFirewallUpdate, parameters);
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
                return firewall;

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
        #endregion [ Update Firewall ]

        #region[ Delete Firewall By FirewallID ]
        //Delete/Update Status to the Firewall from the DB based on the given parameters
        public bool DeleteFirewallByFirewallID(int firewallID)
        {
            SqlDataReader reader = null;
            bool isDeleted = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@FirewallId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(firewallID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteFirewallByFirewallID, parameters);
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
        #endregion[ Delete Firewall By FirewallID ]

        #region [ Get Firewall By FirewallID ]

        public Firewall GetFirewallByFirewallID(int firewallID)
        {
            DataSet ds = new DataSet();
            firewallList = new List<Firewall>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@FirewallID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(firewallID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPFirewallByFirewallID_List, parameters);
                if (ds != null)
                {
                    firewallList = ProcessDataSet(ds);
                    if (firewallList.Count > 0)
                    {
                        return firewallList[0];
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
        #endregion [ Get Firewall By FirewallID ]

        #region [Get All Firewalls]
        public List<Firewall> GetAllFirewalls(int siteID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPFirewall_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                return firewallList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ GET ALL Firewall ]

        #region [ ProcessDataSet]
        //Parses the data reader and converts to object
        private List<Firewall> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        #region [ ConvertToObject]
        private List<Firewall> ConvertToObject(DataSet ds)
        {
            firewallList = new List<Firewall>();


            DataTable firewallDT = new DataTable();
            DataTable GlobalMasterDetailDT = new DataTable();
            DataTable firewallInterfaceDT = new DataTable();
            DataTable firewallModulesDT = new DataTable();
            DataTable firewallSiteToSiteDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    firewallDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    GlobalMasterDetailDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    firewallInterfaceDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    firewallModulesDT = ds.Tables[3];
                if (ds.Tables[4] != null)
                    firewallSiteToSiteDT = ds.Tables[4];

                if (firewallDT.Rows.Count > 0)
                {
                    firewallList = (from DataRow firewall in firewallDT.Rows

                                    select new Firewall
                                  {
                                      FirewallID = DataRowHelper.ConvertToInteger(firewall[columnFirewallID]),
                                      Hostname = DataRowHelper.ConvertToString(firewall[columnHostname], ""),
                                      Manufacture = DataRowHelper.ConvertToString(firewall[columnManufacture], ""),
                                      FirewallModel = (from DataRow firewallModel in GlobalMasterDetailDT.Rows
                                                       where firewallModel.Field<int>(columnModelID) == DataRowHelper.ConvertToInteger(firewall[columnModelID])
                                                       select (new GlobalMasterDetail
                                                       {
                                                           MasterDetailID = DataRowHelper.ConvertToInteger(firewallModel[columnModelID]),
                                                           MasterValue = DataRowHelper.ConvertToString(firewallModel[columnModelName], "")
                                                       })).FirstOrDefault(),
                                      Memory = DataRowHelper.ConvertToString(firewall[columnMemory], ""),
                                      SerialNumber = DataRowHelper.ConvertToString(firewall[columnSerialNumber]),
                                      InstalledOn = DataRowHelper.ConvertToString(firewall[columnInstalledOn], ""),
                                      WarrantyExpiresOn = DataRowHelper.ConvertToString(firewall[columnWarrantyExpiresOn], ""),
                                      IPAddress = DataRowHelper.ConvertToString(firewall[columnIPAddress], ""),
                                      Subnet = DataRowHelper.ConvertToString(firewall[columnSubnet], ""),
                                      Gateway = DataRowHelper.ConvertToString(firewall[columnGateway], ""),
                                      AdminUserName = DataRowHelper.ConvertToString(firewall[columnAdminUserName], ""),
                                      AdminPassword = DataRowHelper.ConvertToString(firewall[columnAdminPassword], ""),
                                      OSVersion = (from DataRow osVersion in GlobalMasterDetailDT.Rows
                                                   where osVersion.Field<int>(columnOSVersionID) == DataRowHelper.ConvertToInteger(firewall[columnOSVersionID])
                                                   select (new GlobalMasterDetail
                                                   {
                                                       MasterDetailID = DataRowHelper.ConvertToInteger(osVersion[columnOSVersionID]),
                                                       MasterValue = DataRowHelper.ConvertToString(osVersion[columnOSVersionName], "")
                                                   })).FirstOrDefault(),
                                      Firmware = DataRowHelper.ConvertToString(firewall[columnFirmware], ""),
                                      FirewallNotes = DataRowHelper.ConvertToString(firewall[columnFirewallNotes], ""),
                                      FirewallModuleList = (from DataRow firewallModule in firewallModulesDT.Rows
                                                            where firewallModule.Field<int>(columnFirewallID) == DataRowHelper.ConvertToInteger(firewall[columnFirewallID])
                                                            select (new FirewallModule
                                                          {
                                                              FirewallID = DataRowHelper.ConvertToInteger(firewallModule[columnFirewallID]),
                                                              FirewallModuleID = DataRowHelper.ConvertToInteger(firewallModule[columnModuleID]),
                                                              Module = (from DataRow module in GlobalMasterDetailDT.Rows
                                                                        where module.Field<int>(columnModuleID) == DataRowHelper.ConvertToInteger(firewallModule[columnModuleID])
                                                                        select (new GlobalMasterDetail
                                                                        {
                                                                            MasterDetailID = DataRowHelper.ConvertToInteger(module[columnModuleID]),
                                                                            MasterValue = DataRowHelper.ConvertToString(module[columnModuleName], "")
                                                                        })).FirstOrDefault()
                                                          })).ToList(),
                                      FirewallModules = DataRowHelper.ConvertToString(firewall[columnFirewallModuleIDS]),
                                      ViewDocumentPath = DataRowHelper.ConvertToString(firewall[columnDocumentPath]),
                                      FirewallInterfaces = DataRowHelper.ConvertToString(firewall[columnFirewallInterfaces], ""),
                                      FirewallSiteToSites = DataRowHelper.ConvertToString(firewall[columnSiteToSitePassKey], ""),

                                      FirewallInterfaceList = (from DataRow firewallInterface in firewallInterfaceDT.Rows
                                                               where firewallInterface.Field<int>(columnFirewallID) == DataRowHelper.ConvertToInteger(firewall[columnFirewallID])
                                                               select (new FirewallInterface
                                                               {
                                                                   FirewallID = DataRowHelper.ConvertToInteger(firewallInterface[columnFirewallID]),
                                                                   FirewallInterfaceID = DataRowHelper.ConvertToInteger(firewallInterface[columnFirewallInterfaceID]),
                                                                   InterfaceValue = DataRowHelper.ConvertToString(firewallInterface[columnInterfaceValue], "")
                                                               })).ToList(),

                                      FirewallSiteToSiteList = (from DataRow firewallSiteToSite in firewallSiteToSiteDT.Rows
                                                                where firewallSiteToSite.Field<int>(columnFirewallID) == DataRowHelper.ConvertToInteger(firewall[columnFirewallID])
                                                                select (new FirewallSiteToSite
                                                                {
                                                                    FirewallID = DataRowHelper.ConvertToInteger(firewallSiteToSite[columnFirewallID]),
                                                                    FirewallSiteToSiteID = DataRowHelper.ConvertToInteger(firewallSiteToSite[columnSiteToSiteID]),
                                                                    SiteToSiteValue = DataRowHelper.ConvertToString(firewallSiteToSite[columnSiteToSiteValue], ""),
                                                                    PasswordKey = DataRowHelper.ConvertToString(firewallSiteToSite[columnFirewallKey], "")
                                                                })).ToList(),

                                      StatusID = DataRowHelper.ConvertToInteger(firewall[columnStatusID]),
                                      CreatedBy = DataRowHelper.ConvertToInteger(firewall[columnCreatedBy]),
                                      CreatedOn = DataRowHelper.ConvertToDateTime(firewall[columnCreatedOn]),
                                      ModifiedBy = DataRowHelper.ConvertToInteger(firewall[columnModifiedBy]),
                                      ModifiedOn = DataRowHelper.ConvertToDateTime(firewall[columnModifiedOn]),

                                      //This Static binding is for Showing the View Col in Jq Grid
                                      View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Firewalls&id=" + DataRowHelper.ConvertToString(firewall[columnFirewallID]) + " style='color: blue;text-decoration: underline;'>More</a>")
                                  }).ToList();
                }
            }
            return firewallList;
        }
        #endregion [ ConvertToObject ]

    }
}
