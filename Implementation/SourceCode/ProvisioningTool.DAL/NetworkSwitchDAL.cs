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



namespace ProvisioningTool.DAL
{

    internal class NetworkSwitchDAL
    {
        #region [ Declarations ]
        private List<NetworkSwitch> networkSwitchList;
        private NetworkSwitch networkSwitch;

        private readonly string columnNetworkSwitchID = "NetworkSwitchID";
        private readonly string columnNetworkSwitchModelID = "NetworkSwitchModelID";
        private readonly string columnNetworkSwitchModelName = "NetworkSwitchModelName";
        private readonly string columnHostname = "Hostname";
        private readonly string columnSerialNumber = "SerialNumber";
        private readonly string columnInstalledOn = "InstalledOn";
        private readonly string columnWarrantyExpiresOn = "WarrantyExpiresOn";
        private readonly string columnSpeed = "Speed";
        private readonly string columnPOE = "POE";
        private readonly string columnPower = "Power";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnSubnet = "Subnet";
        private readonly string columnGateway = "Gateway";
        private readonly string columnAdminUserName = "AdminUserName";
        private readonly string columnAdminPassword = "AdminPassword";
        private readonly string columnOSVersionID = "OSVersionID";
        private readonly string columnOSVersionName = "OSVersionName";
        private readonly string columnNetworkSwitchInterfaceID = "NetworkSwitchInterfaceID";
        private readonly string columnInterfaceValue = "InterfaceValue";
        private readonly string columnNetworkSwitchInterfaces = "NetworkSwitchInterfaces";
        private readonly string columnFirmware = "Firmware";
        private readonly string columnModuleID = "ModuleID";
        private readonly string columnModuleName = "ModuleName";
        private readonly string columnNetworkSwitchModuleIDS = "NetworkSwitchModuleIDS";
        
        private readonly string columnSFPType = "SFPType";
        private readonly string columnVLAN = "VLAN";
        private readonly string columnNetworkSwitchNotes = "NetworkSwitchNotes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";

        #endregion [ Declarations ]

        internal NetworkSwitchDAL()
        {
        }
        #region [ Add NetworkSwitch ]
        internal NetworkSwitch AddNetworkSwitch(NetworkSwitch networkSwitch, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[23];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(networkSwitch.Hostname);

                parameters[1] = new SqlParameter("@NetworkSwitchModelID", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.NetworkSwitchModel.MasterDetailID);

                parameters[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(networkSwitch.SerialNumber);

                parameters[3] = new SqlParameter("@InstalledOn", SqlDbType.Date);
                parameters[3].Value = DBValueHelper.ConvertToDBDate(networkSwitch.InstalledOn);

                parameters[4] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.Date);
                parameters[4].Value = DBValueHelper.ConvertToDBDate(networkSwitch.WarrantyExpiresOn);

                parameters[5] = new SqlParameter("@Speed", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(networkSwitch.Speed);

                parameters[6] = new SqlParameter("@POE", SqlDbType.Bit);
                parameters[6].Value = DBValueHelper.ConvertToDBBoolean(networkSwitch.POE);

                parameters[7] = new SqlParameter("@Power", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(networkSwitch.Power);

                parameters[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(networkSwitch.IPAddress);

                parameters[9] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(networkSwitch.Subnet);

                parameters[10] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(networkSwitch.Gateway);

                parameters[11] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(networkSwitch.AdminUserName);

                parameters[12] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(networkSwitch.AdminPassword);

                parameters[13] = new SqlParameter("@OSVersion", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.OSVersion.MasterDetailID);

                parameters[14] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(networkSwitch.Firmware);

                parameters[15] = new SqlParameter("@Modules", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(networkSwitch.NetworkSwitchModules);

                parameters[16] = new SqlParameter("@SFPType", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(networkSwitch.SFPType);

                parameters[17] = new SqlParameter("@VLAN", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(networkSwitch.VLAN);

                parameters[18] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(networkSwitch.NetworkSwitchInterfaces);

                parameters[19] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(networkSwitch.Notes);

                parameters[20] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[20].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.StatusID);

                parameters[21] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[21].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.CreatedBy);

                parameters[22] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[22].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.Site.SiteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkSwitchAdd, parameters);
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
                return networkSwitch;

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
        #endregion [ Add NetworkSwitch ]

        #region [ Update NetworkSwitch ]
        internal NetworkSwitch ModifyNetworkSwitch(NetworkSwitch networkSwitch, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[23];

                parameters[0] = new SqlParameter("@NetworkSwitchID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.NetworkSwitchID);

                parameters[1] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(networkSwitch.Hostname);

                parameters[2] = new SqlParameter("@NetworkSwitchModelID", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.NetworkSwitchModel.MasterDetailID);

                parameters[3] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(networkSwitch.SerialNumber);

                parameters[4] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(networkSwitch.InstalledOn);

                parameters[5] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(networkSwitch.WarrantyExpiresOn);

                parameters[6] = new SqlParameter("@Speed", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(networkSwitch.Speed);

                parameters[7] = new SqlParameter("@POE", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(networkSwitch.POE);

                parameters[8] = new SqlParameter("@Power", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(networkSwitch.Power);

                parameters[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(networkSwitch.IPAddress);

                parameters[10] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(networkSwitch.Subnet);

                parameters[11] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(networkSwitch.Gateway);

                parameters[12] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(networkSwitch.AdminUserName);

                parameters[13] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(networkSwitch.AdminPassword);

                parameters[14] = new SqlParameter("@OSVersion", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.OSVersion.MasterDetailID);

                parameters[15] = new SqlParameter("@Firmware", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(networkSwitch.Firmware);

                parameters[16] = new SqlParameter("@Modules", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(networkSwitch.NetworkSwitchModules);

                parameters[17] = new SqlParameter("@SFPType", SqlDbType.VarChar);
                parameters[17].Value = DBValueHelper.ConvertToDBString(networkSwitch.SFPType);

                parameters[18] = new SqlParameter("@VLAN", SqlDbType.VarChar);
                parameters[18].Value = DBValueHelper.ConvertToDBString(networkSwitch.VLAN);

                parameters[19] = new SqlParameter("@InterfaceValue", SqlDbType.VarChar);
                parameters[19].Value = DBValueHelper.ConvertToDBString(networkSwitch.NetworkSwitchInterfaces);

                parameters[20] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[20].Value = DBValueHelper.ConvertToDBString(networkSwitch.Notes);

                parameters[21] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[21].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.StatusID);

                parameters[22] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[22].Value = DBValueHelper.ConvertToDBInteger(networkSwitch.ModifiedBy);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkSwitchUpdate, parameters);
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
                return networkSwitch;

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
        #endregion [ Update NetworkSwitch ]

        #region[ Delete NetworkSwitch ]
        //Delete/Update Status to 2 the NetworkSwitch from the DB based on the given parameters
        public bool DeleteNetworkSwitchByNetworkSwitchID(int networkSwitchID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@NetworkSwitchID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(networkSwitchID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteNetworkSwitchByNetworkSwitchID, parameters);
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
        #endregion[Delete NetworkSwitch]

        #region [Get All NetworkSwitchs]
        public List<NetworkSwitch> GetAllNetworkSwitchs(int siteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllNetworkSwitch);

            SqlDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkSwitch_List, parameters);
                if (ds != null)
                {
                    return ProcessDataSet(ds);
                }
                else
                    return networkSwitchList;
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
        #endregion [ GET ALL NetworkSwitch ]

        #region [Get NetworkSwitch And NetworkSwitch Attribute Details By NetworkSwitchID]

        public NetworkSwitch GetNetworkSwitchByNetworkSwitchID(int networkSwitchID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                networkSwitchList = new List<NetworkSwitch>();
                parameters[0] = new SqlParameter("@NetworkSwitchID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(networkSwitchID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNetworkSwitchAndNetworkSwitchDetailsByNetworkSwitchID, parameters);
                if (ds != null)
                {
                    networkSwitchList = ProcessDataSet(ds);
                    if (networkSwitchList.Count > 0)
                    {
                        return networkSwitchList[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else {
                    return null;
                }
                
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        #endregion


        #region [ private methods ]
        //Parses the data reader and converts to object
        #region [ ProcessDataSet]

        //Parses the data reader and converts to object
        private List<NetworkSwitch> ProcessDataSet(DataSet ds)
        {
            if (ds != null)
            {
                return ConvertToObject(ds);
            }
            return null;
        }
        #endregion [ ProcessDataSet]

        #region [ ConvertAllNetworkSwitchAttributesToObject]
        //All the NetworkSwitch Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<NetworkSwitch> ConvertToObject(DataSet ds)
        {
            networkSwitchList = new List<NetworkSwitch>();

            DataTable NetworkSwitchdt = new DataTable();
            DataTable GlobalMasterDetailDT = new DataTable();
            DataTable networkSwitchInterfaceDT = new DataTable();
            DataTable networkSwitchModulesDT = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    NetworkSwitchdt = ds.Tables[0];

                    if (ds.Tables[1] != null)
                        GlobalMasterDetailDT = ds.Tables[1];
                    if (ds.Tables[2] != null)
                        networkSwitchInterfaceDT = ds.Tables[2];
                    if (ds.Tables[3] != null)
                        networkSwitchModulesDT = ds.Tables[3];


                    //Convert NetworkSwitch Data table to its Corresponding List
                    if (NetworkSwitchdt.Rows.Count > 0)
                    {
                        networkSwitchList = (from DataRow networkSwitch in NetworkSwitchdt.Rows
                                             select new NetworkSwitch
                                             {

                                                 NetworkSwitchID = DataRowHelper.ConvertToInteger(networkSwitch[columnNetworkSwitchID]),
                                                 Hostname = DataRowHelper.ConvertToString(networkSwitch[columnHostname],""),
                                                 NetworkSwitchModel = (from DataRow networkSwitchModel in GlobalMasterDetailDT.Rows
                                                                       where networkSwitchModel.Field<int>(columnNetworkSwitchModelID) == DataRowHelper.ConvertToInteger(networkSwitch[columnNetworkSwitchModelID])
                                                                       select (new GlobalMasterDetail
                                                                       {
                                                                           MasterDetailID = DataRowHelper.ConvertToInteger(networkSwitchModel[columnNetworkSwitchModelID]),
                                                                           MasterValue = DataRowHelper.ConvertToString(networkSwitchModel[columnNetworkSwitchModelName],"")
                                                                       })).FirstOrDefault(),
                                                 SerialNumber = DataRowHelper.ConvertToString(networkSwitch[columnSerialNumber],""),
                                                 InstalledOn = DataRowHelper.ConvertToString(networkSwitch[columnInstalledOn]),
                                                 WarrantyExpiresOn = DataRowHelper.ConvertToString(networkSwitch[columnWarrantyExpiresOn]),
                                                 Speed = DataRowHelper.ConvertToString(networkSwitch[columnSpeed],""),
                                                 POE = DataRowHelper.ConvertToBoolean(networkSwitch[columnPOE]),
                                                 Power = DataRowHelper.ConvertToString(networkSwitch[columnPower],""),
                                                 IPAddress = DataRowHelper.ConvertToString(networkSwitch[columnIPAddress],""),
                                                 Subnet = DataRowHelper.ConvertToString(networkSwitch[columnSubnet],""),
                                                 Gateway = DataRowHelper.ConvertToString(networkSwitch[columnGateway],""),
                                                 AdminUserName = DataRowHelper.ConvertToString(networkSwitch[columnAdminUserName],""),
                                                 AdminPassword = DataRowHelper.ConvertToString(networkSwitch[columnAdminPassword],""),
                                                 OSVersion = (from DataRow osVersion in GlobalMasterDetailDT.Rows
                                                              where osVersion.Field<int>(columnOSVersionID) == DataRowHelper.ConvertToInteger(networkSwitch[columnOSVersionID])
                                                              select (new GlobalMasterDetail
                                                              {
                                                                  MasterDetailID = DataRowHelper.ConvertToInteger(osVersion[columnOSVersionID]),
                                                                  MasterValue = DataRowHelper.ConvertToString(osVersion[columnOSVersionName],"")
                                                              })).FirstOrDefault(),
                                                 Firmware = DataRowHelper.ConvertToString(networkSwitch[columnFirmware],""),
                                                 NetworkSwitchModules = DataRowHelper.ConvertToString(networkSwitch[columnNetworkSwitchModuleIDS],""),
                                                  
                                                 NetworkSwitchModuleList = (from DataRow networkSwitchModule in networkSwitchModulesDT.Rows
                                                                            where networkSwitchModule.Field<int>(columnNetworkSwitchID) == DataRowHelper.ConvertToInteger(networkSwitch[columnNetworkSwitchID])
                                                                            select (new NetworkSwitchModule
                                                                            {
                                                                                NetworkSwitchID = DataRowHelper.ConvertToInteger(networkSwitchModule[columnNetworkSwitchID]),
                                                                                NetworkSwitchModuleID = DataRowHelper.ConvertToInteger(networkSwitchModule[columnModuleID]),
                                                                                Module = (from DataRow module in GlobalMasterDetailDT.Rows
                                                                                          where module.Field<int>(columnModuleID) == DataRowHelper.ConvertToInteger(networkSwitchModule[columnModuleID])
                                                                                          select (new GlobalMasterDetail
                                                                                          {
                                                                                              MasterDetailID = DataRowHelper.ConvertToInteger(module[columnModuleID]),
                                                                                              MasterValue = DataRowHelper.ConvertToString(module[columnModuleName],"")
                                                                                          })).FirstOrDefault()
                                                                            })).ToList(),
                                                 NetworkSwitchInterfaces = DataRowHelper.ConvertToString(networkSwitch[columnNetworkSwitchInterfaces],""),
                                                 NetworkSwitchInterfaceList = (from DataRow networkSwitchInterface in networkSwitchInterfaceDT.Rows
                                                                               where networkSwitchInterface.Field<int>(columnNetworkSwitchID) == DataRowHelper.ConvertToInteger(networkSwitch[columnNetworkSwitchID])
                                                                               select (new NetworkSwitchInterface
                                                                               {
                                                                                   NetworkSwitchID = DataRowHelper.ConvertToInteger(networkSwitchInterface[columnNetworkSwitchID]),
                                                                                   NetworkSwitchInterfaceID = DataRowHelper.ConvertToInteger(networkSwitchInterface[columnNetworkSwitchInterfaceID]),
                                                                                   InterfaceValue = DataRowHelper.ConvertToString(networkSwitchInterface[columnInterfaceValue])
                                                                               })).ToList(),
                                                 SFPType = DataRowHelper.ConvertToString(networkSwitch[columnSFPType],""),
                                                 VLAN = DataRowHelper.ConvertToString(networkSwitch[columnVLAN],""),
                                                 Notes = DataRowHelper.ConvertToString(networkSwitch[columnNetworkSwitchNotes],""),
                                                 StatusID = DataRowHelper.ConvertToInteger(networkSwitch[columnStatusID]),
                                                 CreatedBy = DataRowHelper.ConvertToInteger(networkSwitch[columnCreatedBy]),
                                                 CreatedOn = DataRowHelper.ConvertToDateTime(networkSwitch[columnCreatedOn]),
                                                 ModifiedBy = DataRowHelper.ConvertToInteger(networkSwitch[columnModifiedBy]),
                                                 ModifiedOn = DataRowHelper.ConvertToDateTime(networkSwitch[columnModifiedOn]),
                                                 //This Static binding is for Showing the View Col in Jq Grid
                                                 View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Network%20Switches&id=" + DataRowHelper.ConvertToString(networkSwitch[columnNetworkSwitchID]) + " style='color: blue;text-decoration: underline;'>More</a>")



                                             }).ToList();



                    }
                }

                return networkSwitchList;

            }
            return null;
        }
        #endregion

        #endregion [ private methods ]
    }
}
