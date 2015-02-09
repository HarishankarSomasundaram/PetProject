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

    internal class WirelessDAL
    {
        #region [ Declarations ]
        private List<Wireless> WirelessList;
        private Wireless wireless;
        DataSet dsWireless;
        private readonly string columnWirelessID = "WirelessID";
        private readonly string columnHostname = "Hostname";
        private readonly string columnWirelessManufactureID = "WirelessManufactureID";
        private readonly string columnWirelessManufactureName = "WirelessManufactureName";
        private readonly string columnWirelessDeviceTypeID = "WirelessTypeID";
        private readonly string columnWirelessDeviceTypeName = "WirelessTypeName";
        private readonly string columnWirelessModelID = "WirelessModelID";
        private readonly string columnWirelessModelName = "WirelessModelName";
        private readonly string columnSerialNumber = "SerialNumber";
        private readonly string columnInstalledOn = "InstalledOn";
        private readonly string columnWarrantyExpiresOn = "WarrantyExpiresOn";
        private readonly string columnIPAddress = "IPAddress";
        private readonly string columnSubnet = "Subnet";
        private readonly string columnGateway = "Gateway";
        private readonly string columnAdminUserName = "AdminUserName";
        private readonly string columnAdminPassword = "AdminPassword";
        private readonly string columnSSID = "SSID";
        private readonly string columnAuthentication = "Authentication";
        private readonly string columnWirelessEncryption = "WirelessEncryption";
        private readonly string columnNotes = "Notes";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";

        #endregion [ Declarations ]

        internal WirelessDAL()
        {
        }
        #region [ Add Wireless ]
        internal Wireless AddWireless(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[19];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.Wireless.Hostname);

                parameters[1] = new SqlParameter("@WirelessManufactureID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.WirelessManufacture.MasterDetailID);

                parameters[2] = new SqlParameter("@WirelessDeviceTypeValue", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.Wireless.WirelessTypeValue);

                parameters[3] = new SqlParameter("@WirelessModelID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.WirelessModel.MasterDetailID);

                parameters[4] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.Wireless.SerialNumber);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.Date);
                parameters[5].Value = DBValueHelper.ConvertToDBDate(request.Wireless.InstalledOn);

                parameters[6] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.Date);
                parameters[6].Value = DBValueHelper.ConvertToDBDate(request.Wireless.WarrantyExpiresOn);

                parameters[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.Wireless.IPAddress);

                parameters[8] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.Wireless.Subnet);

                parameters[9] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.Wireless.Gateway);

                parameters[10] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.Wireless.AdminUserName);

                parameters[11] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(request.Wireless.AdminPassword);

                parameters[12] = new SqlParameter("@SSID", SqlDbType.Int);
                parameters[12].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.SSID);

                parameters[13] = new SqlParameter("@Authentication", SqlDbType.VarChar);
                parameters[13].Value = DBValueHelper.ConvertToDBString(request.Wireless.Authentication);

                parameters[14] = new SqlParameter("@WirelessEncryption", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(request.Wireless.WirelessEncryption);

                parameters[15] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(request.Wireless.Notes);

                parameters[16] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[16].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.StatusID);

                parameters[17] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[17].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.CreatedBy);

                parameters[18] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[18].Value = request.sessionSiteID;


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWirelessAdd, parameters);
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
                return request.Wireless;

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
        #endregion [ Add Wireless ]

        #region [ Update Wireless ]
        internal Wireless ModifyWireless(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[19];

                parameters[0] = new SqlParameter("@WirelessID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.WirelessID);

                parameters[1] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.Wireless.Hostname);

                parameters[2] = new SqlParameter("@WirelessManufactureID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.WirelessManufacture.MasterDetailID);

                parameters[3] = new SqlParameter("@WirelessDeviceTypeValue", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.Wireless.WirelessTypeValue);

                parameters[4] = new SqlParameter("@WirelessModelID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.WirelessModel.MasterDetailID);

                parameters[5] = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.Wireless.SerialNumber);

                parameters[6] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.Wireless.InstalledOn);

                parameters[7] = new SqlParameter("@WarrantyExpiresOn", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.Wireless.WarrantyExpiresOn);

                parameters[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                parameters[8].Value = DBValueHelper.ConvertToDBString(request.Wireless.IPAddress);

                parameters[9] = new SqlParameter("@Subnet", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.Wireless.Subnet);

                parameters[10] = new SqlParameter("@Gateway", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.Wireless.Gateway);

                parameters[11] = new SqlParameter("@AdminUserName", SqlDbType.VarChar);
                parameters[11].Value = DBValueHelper.ConvertToDBString(request.Wireless.AdminUserName);

                parameters[12] = new SqlParameter("@AdminPassword", SqlDbType.VarChar);
                parameters[12].Value = DBValueHelper.ConvertToDBString(request.Wireless.AdminPassword);

                parameters[13] = new SqlParameter("@SSID", SqlDbType.Int);
                parameters[13].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.SSID);

                parameters[14] = new SqlParameter("@Authentication", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(request.Wireless.Authentication);

                parameters[15] = new SqlParameter("@WirelessEncryption", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(request.Wireless.WirelessEncryption);

                parameters[16] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[16].Value = DBValueHelper.ConvertToDBString(request.Wireless.Notes);

                parameters[17] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[17].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.StatusID);

                parameters[18] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[18].Value = DBValueHelper.ConvertToDBInteger(request.Wireless.ModifiedBy);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWirelessUpdate, parameters);
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
                return request.Wireless;

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
        #endregion [ Update Wireless ]

        #region[ Delete Wireless ]
        //Delete/Update Status to 2 the Wireless from the DB based on the given parameters
        public bool DeleteWirelessByWirelessID(int wirelessID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@WirelessId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(wirelessID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteWirelessByWirelessID, parameters);
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
        #endregion[Delete Wireless]

        #region [Get All Wirelesss]
        public List<Wireless> GetAllWirelesss(int siteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllWireless);

            SqlDataReader reader = null;
            dsWireless = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                
                dsWireless = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWireless_List, parameters);
                if (dsWireless != null)
                {
                    WirelessList = ConvertAllWirelessAttributesToObjectList(dsWireless);
                    if (WirelessList != null && WirelessList.Count > 0)
                    {
                        return WirelessList;
                    }
                    else
                        return null;
                }
                else
                    return null;
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
        #endregion [ GET ALL Wireless ]

        #region [Get Wireless And Wireless Attribute Details By WirelessID]

        public Wireless GetWirelessAndWirelessDetailsByWirelessID(int wirelessID)
        {

            dsWireless = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@WirelessID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(wirelessID);
                dsWireless = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPWirelessByWirelessID, parameters);
                if (dsWireless != null)
                {
                    WirelessList = ConvertAllWirelessAttributesToObjectList(dsWireless);
                    if (WirelessList != null && WirelessList.Count > 0)
                    {
                        return WirelessList[0];
                    }
                    else
                        return null;
                }
                else
                    return null;

            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        #endregion
        #region [ private methods ]

        #region [ Convert All Wireless Attributes To Object List]
        //All the Wireless Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<Wireless> ConvertAllWirelessAttributesToObjectList(DataSet ds)
        {
            WirelessList = new List<Wireless>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();


            DataTable Wirelessdt = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    Wirelessdt = ds.Tables[0];

                    //Convert Wireless Data table to its Corresponding List
                    if (Wirelessdt.Rows.Count > 0)
                    {
                        WirelessList = (from DataRow wireless in Wirelessdt.Rows
                                        select new Wireless
                                        {

                                            WirelessID = DataRowHelper.ConvertToInteger(wireless[columnWirelessID]),
                                            Hostname = DataRowHelper.ConvertToString(wireless[columnHostname],""),

                                            WirelessManufacture = (from DataRow networkSwitchModel in Wirelessdt.Rows
                                                                   where networkSwitchModel.Field<int>(columnWirelessManufactureID) == DataRowHelper.ConvertToInteger(wireless[columnWirelessManufactureID])
                                                                   select (new GlobalMasterDetail
                                                                   {
                                                                       MasterDetailID = DataRowHelper.ConvertToInteger(networkSwitchModel[columnWirelessManufactureID]),
                                                                       MasterValue = DataRowHelper.ConvertToString(networkSwitchModel[columnWirelessManufactureName],"")
                                                                   })).FirstOrDefault(),


                                            //WirelessType = (from DataRow networkSwitchModel in Wirelessdt.Rows
                                            //                where networkSwitchModel.Field<int>(columnWirelessDeviceTypeID) == DataRowHelper.ConvertToInteger(wireless[columnWirelessDeviceTypeID])
                                            //                select (new GlobalMasterDetail
                                            //                {
                                            //                    MasterDetailID = DataRowHelper.ConvertToInteger(networkSwitchModel[columnWirelessDeviceTypeID]),
                                            //                    MasterValue = DataRowHelper.ConvertToString(networkSwitchModel[columnWirelessDeviceTypeName],"")
                                            //                })).FirstOrDefault(),
                                            WirelessTypeValue = DataRowHelper.ConvertToString(wireless[columnWirelessDeviceTypeName], ""),

                                            WirelessModel = (from DataRow networkSwitchModel in Wirelessdt.Rows
                                                             where networkSwitchModel.Field<int>(columnWirelessModelID) == DataRowHelper.ConvertToInteger(wireless[columnWirelessModelID])
                                                             select (new GlobalMasterDetail
                                                             {
                                                                 MasterDetailID = DataRowHelper.ConvertToInteger(networkSwitchModel[columnWirelessModelID]),
                                                                 MasterValue = DataRowHelper.ConvertToString(networkSwitchModel[columnWirelessModelName],"")
                                                             })).FirstOrDefault(),
                                            SerialNumber = DataRowHelper.ConvertToString(wireless[columnSerialNumber],""),
                                            InstalledOn = DataRowHelper.ConvertToString(wireless[columnInstalledOn]),
                                            WarrantyExpiresOn = DataRowHelper.ConvertToString(wireless[columnWarrantyExpiresOn]),
                                            IPAddress = DataRowHelper.ConvertToString(wireless[columnIPAddress],""),
                                            Subnet = DataRowHelper.ConvertToString(wireless[columnSubnet],""),
                                            Gateway = DataRowHelper.ConvertToString(wireless[columnGateway],""),
                                            AdminUserName = DataRowHelper.ConvertToString(wireless[columnAdminUserName],""),
                                            AdminPassword = DataRowHelper.ConvertToString(wireless[columnAdminPassword],""),
                                            SSID = DataRowHelper.ConvertToInteger(wireless[columnSSID]),
                                            Authentication = DataRowHelper.ConvertToString(wireless[columnAuthentication],""),
                                            WirelessEncryption = DataRowHelper.ConvertToString(wireless[columnWirelessEncryption],""),
                                            Notes = DataRowHelper.ConvertToString(wireless[columnNotes],""),
                                            StatusID = DataRowHelper.ConvertToInteger(wireless[columnStatusID]),
                                            CreatedBy = DataRowHelper.ConvertToInteger(wireless[columnCreatedBy]),
                                            CreatedOn = DataRowHelper.ConvertToDateTime(wireless[columnCreatedOn]),
                                            ModifiedBy = DataRowHelper.ConvertToInteger(wireless[columnModifiedBy]),
                                            ModifiedOn = DataRowHelper.ConvertToDateTime(wireless[columnModifiedOn]),
                                            View = ConvertHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=wireless&id=" + ConvertHelper.ConvertToString(wireless[columnWirelessID]) + " style='color: blue;text-decoration: underline;'>More</a>"),
                                        }).ToList();

                    }
                }

                return WirelessList;

            }
            return null;
        }
        #endregion


        #endregion [ private methods ]
    }
}
