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

    internal class MobileDeviceDAL
    {
        #region [ Declarations ]
        private List<MobileDevice> MobileDeviceList;
        private MobileDevice mobileDevice;

        private readonly string columnMobileDeviceID = "MobileDeviceID";
        private readonly string columnHostname = "Hostname";
        private readonly string columnMobileDeviceTypeID = "MobileDeviceTypeID";
        private readonly string columnMobileDeviceTypeName = "MobileDeviceTypeName";
        private readonly string columnMobileDeviceManufactureID = "MobileDeviceManufactureID";
        private readonly string columnMobileDeviceManufactureName = "MobileDeviceManufactureName";
        private readonly string columnMobileDeviceModelID = "MobileDeviceModelID";
        private readonly string columnMobileDeviceModelName = "MobileDeviceModelName";
        private readonly string columnAssignedUserID = "AssignedUserID";
        private readonly string columnAssignedUserName = "AssignedUserName";
        private readonly string columnInstalledOn = "InstalledOn";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";

        #endregion [ Declarations ]

        internal MobileDeviceDAL()
        {
        }
        #region [ Add MobileDevice ]
        internal MobileDevice AddMobileDevice(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[9];

                parameters[0] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.MobileDevice.Hostname);

                parameters[1] = new SqlParameter("@MobileDeviceTypeID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.MobileDeviceType.MasterDetailID);

                parameters[2] = new SqlParameter("@MobileDeviceManufactureID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.MobileDeviceManufacture.MasterDetailID);

                parameters[3] = new SqlParameter("@MobileDeviceModelID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.MobileDeviceModel.MasterDetailID);

                parameters[4] = new SqlParameter("@AssignedUserID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.AssignedUser.UserID);

                parameters[5] = new SqlParameter("@InstalledOn", SqlDbType.DateTime);
                parameters[5].Value = DBValueHelper.ConvertToDBDate(request.MobileDevice.InstalledOn);

                parameters[6] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.StatusID);

                parameters[7] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.CreatedBy);

                parameters[8] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[8].Value = DBValueHelper.ConvertToDBInteger(request.sessionSiteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPMobileDeviceAdd, parameters);
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
                return request.MobileDevice;

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
        #endregion [ Add MobileDevice ]

        #region [ Update MobileDevice ]
        internal MobileDevice ModifyMobileDevice(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter("@MobileDeviceID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.MobileDeviceID);

                parameters[1] = new SqlParameter("@Hostname", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.MobileDevice.Hostname);

                parameters[2] = new SqlParameter("@MobileDeviceTypeID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.MobileDeviceType.MasterDetailID);

                parameters[3] = new SqlParameter("@MobileDeviceManufactureID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.MobileDeviceManufacture.MasterDetailID);

                parameters[4] = new SqlParameter("@MobileDeviceModelID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.MobileDeviceModel.MasterDetailID);

                parameters[5] = new SqlParameter("@AssignedUserID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.AssignedUser.UserID);

                parameters[6] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.MobileDevice.InstalledOn);

                parameters[7] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.MobileDevice.ModifiedBy);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPMobileDeviceUpdate, parameters);
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
                return request.MobileDevice;

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
        #endregion [ Update MobileDevice ]

        #region[ Delete MobileDevice ]
        //Delete/Update Status to 2 the MobileDevice from the DB based on the given parameters
        public bool DeleteMobileDeviceByMobileDeviceID(int mobileDeviceID)
        {
            SqlDataReader reader = null;
            bool isDeleted = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@MobileDeviceId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(mobileDeviceID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteMobileDeviceByMobileDeviceID, parameters);
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
        #endregion[Delete MobileDevice]

        #region [Get All MobileDevices]
        public List<MobileDevice> GetAllMobileDevices(int siteID, string searchFilter)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllMobileDevice);

            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);
                
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPMobileDevice_List, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return MobileDeviceList;
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
        #endregion [ GET ALL MobileDevice ]

        #region [Get MobileDevice And MobileDevice Attribute Details By MobileDeviceID]

        public MobileDevice GetMobileDeviceAndMobileDeviceDetailsByMobileDeviceID(int mobileDeviceID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@MobileDeviceID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(mobileDeviceID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPMobileDeviceByMobileDeviceID, parameters);
                if (ds != null)
                {
                    mobileDevice = ConvertAllMobileDeviceAttributesToObject(ds);
                }
                return mobileDevice;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        #endregion

        #region [ ConvertAllMobileDeviceAttributesToObject]
        //All the MobileDevice Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public MobileDevice ConvertAllMobileDeviceAttributesToObject(DataSet ds)
        {
            MobileDeviceList = new List<MobileDevice>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();


            DataTable MobileDevicedt = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    MobileDevicedt = ds.Tables[0];

                    //Convert MobileDevice Data table to its Corresponding List
                    if (MobileDevicedt.Rows.Count > 0)
                    {
                        MobileDeviceList = (from DataRow mobileDevice in MobileDevicedt.Rows
                                            select new MobileDevice
                                            {

                                                MobileDeviceID = DataRowHelper.ConvertToInteger(mobileDevice[columnMobileDeviceID]),
                                                Hostname = DataRowHelper.ConvertToString(mobileDevice[columnHostname],""),
                                                MobileDeviceType = (from DataRow mobileDeviceType in MobileDevicedt.Rows
                                                                    where mobileDeviceType.Field<int>(columnMobileDeviceTypeID) == DataRowHelper.ConvertToInteger(mobileDevice[columnMobileDeviceTypeID])
                                                                    select (new GlobalMasterDetail
                                                                    {
                                                                        MasterDetailID = DataRowHelper.ConvertToInteger(mobileDeviceType[columnMobileDeviceTypeID]),
                                                                        MasterValue = DataRowHelper.ConvertToString(mobileDeviceType[columnMobileDeviceTypeName],"")
                                                                    })).FirstOrDefault(),
                                                MobileDeviceManufacture = (from DataRow mobileDeviceManufacture in MobileDevicedt.Rows
                                                                           where mobileDeviceManufacture.Field<int>(columnMobileDeviceManufactureID) == DataRowHelper.ConvertToInteger(mobileDevice[columnMobileDeviceManufactureID])
                                                                           select (new GlobalMasterDetail
                                                                           {
                                                                               MasterDetailID = DataRowHelper.ConvertToInteger(mobileDeviceManufacture[columnMobileDeviceManufactureID]),
                                                                               MasterValue = DataRowHelper.ConvertToString(mobileDeviceManufacture[columnMobileDeviceManufactureName],"")
                                                                           })).FirstOrDefault(),
                                                MobileDeviceModel = (from DataRow mobileDeviceModel in MobileDevicedt.Rows
                                                                     where mobileDeviceModel.Field<int>(columnMobileDeviceModelID) == DataRowHelper.ConvertToInteger(mobileDevice[columnMobileDeviceModelID])
                                                                     select (new GlobalMasterDetail
                                                                     {
                                                                         MasterDetailID = DataRowHelper.ConvertToInteger(mobileDeviceModel[columnMobileDeviceModelID]),
                                                                         MasterValue = DataRowHelper.ConvertToString(mobileDeviceModel[columnMobileDeviceModelName],"")
                                                                     })).FirstOrDefault(),
                                                AssignedUser = (from DataRow mobileDeviceAssignedUser in MobileDevicedt.Rows
                                                                where mobileDeviceAssignedUser.Field<int>(columnAssignedUserID) == DataRowHelper.ConvertToInteger(mobileDevice[columnAssignedUserID])
                                                                select (new User
                                                                {
                                                                    UserID = DataRowHelper.ConvertToInteger(mobileDeviceAssignedUser[columnAssignedUserID]),
                                                                    UserName = DataRowHelper.ConvertToString(mobileDeviceAssignedUser[columnAssignedUserName],"")
                                                                })).FirstOrDefault(),


                                                InstalledOn = DataRowHelper.ConvertToString(mobileDevice[columnInstalledOn]),
                                                StatusID = DataRowHelper.ConvertToInteger(mobileDevice[columnStatusID]),
                                                CreatedBy = DataRowHelper.ConvertToInteger(mobileDevice[columnCreatedBy]),
                                                CreatedOn = DataRowHelper.ConvertToDateTime(mobileDevice[columnCreatedOn]),
                                                ModifiedBy = DataRowHelper.ConvertToInteger(mobileDevice[columnModifiedBy]),
                                                ModifiedOn = DataRowHelper.ConvertToDateTime(mobileDevice[columnModifiedOn]),
                                            }).ToList();


                    }
                }

                return MobileDeviceList[0];

            }
            return null;
        }
        #endregion

        #region [ private methods ]
        //Parses the data reader and converts to object
        private List<MobileDevice> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                MobileDeviceList = new List<MobileDevice>();
                while (reader.Read())
                    MobileDeviceList.Add(ConvertToObject(reader));
                return MobileDeviceList;
            }
            return null;
        }
        private MobileDevice ConvertToObject(IDataRecord dataRecord)
        {
            MobileDevice mobileDevice = new MobileDevice();
            mobileDevice.MobileDeviceType = new GlobalMasterDetail();
            mobileDevice.MobileDeviceManufacture = new GlobalMasterDetail();
            mobileDevice.MobileDeviceModel = new GlobalMasterDetail();
            mobileDevice.AssignedUser = new User();
            mobileDevice.MobileDeviceID = DataRowHelper.ConvertToInteger(dataRecord, columnMobileDeviceID);
            mobileDevice.Hostname = DataRowHelper.ConvertToString(dataRecord, columnHostname);
            mobileDevice.MobileDeviceType.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnMobileDeviceTypeID);
            mobileDevice.MobileDeviceType.MasterValue = DataRowHelper.ConvertToString(dataRecord, columnMobileDeviceTypeName);
            mobileDevice.MobileDeviceManufacture.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnMobileDeviceManufactureID);
            mobileDevice.MobileDeviceManufacture.MasterValue = DataRowHelper.ConvertToString(dataRecord, columnMobileDeviceManufactureName);
            mobileDevice.MobileDeviceModel.MasterDetailID = DataRowHelper.ConvertToInteger(dataRecord, columnMobileDeviceModelID);
            mobileDevice.MobileDeviceModel.MasterValue = DataRowHelper.ConvertToString(dataRecord, columnMobileDeviceModelName);
            mobileDevice.AssignedUser.UserID = DataRowHelper.ConvertToInteger(dataRecord, columnAssignedUserID);
            mobileDevice.AssignedUser.UserName = DataRowHelper.ConvertToString(dataRecord, columnAssignedUserName);
            mobileDevice.InstalledOn = DataRowHelper.ConvertToString(dataRecord, columnInstalledOn);
            mobileDevice.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
            mobileDevice.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
            mobileDevice.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
            mobileDevice.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
            mobileDevice.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);

            return mobileDevice;
        }
        #endregion [ private methods ]
    }
}
