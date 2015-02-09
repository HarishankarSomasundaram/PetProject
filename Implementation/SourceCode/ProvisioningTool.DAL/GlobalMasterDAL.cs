using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;
using System.Text;

namespace ProvisioningTool.DAL
{
    internal class GlobalMasterDAL
    {
        #region [ Declarations ]
        private List<GlobalMaster> globalMasterList;
        private readonly string columnMasterDetailID = "MasterDetailID";
        private readonly string columnMasterID = "MasterID";
        private readonly string columnSiteID = "SiteID";
        private readonly string columnSiteName = "SiteName";
        private readonly string columnSubManufacturerID = "SubManufacturerID";
        private readonly string columnManufacturers = "Manufacturers";
        private readonly string columnSubTypeID = "SubTypeID";
        private readonly string columnTypes = "Types";
        private readonly string columnMasterName = "MasterName";
        private readonly string columnMasterDescription = "MasterDescription";
        private readonly string columnIsGlobalMaster = "IsGlobalMaster";
        private readonly string columnMasterValue = "MasterValue";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnCreatedUser = "CreatedUser";
        #endregion [ Declarations ]

        #region [ Add ]
        public bool GlobalMasterDetailAdd(GlobalMasterDetail globalMasterDetail, string masterName)
        {
            SqlDataReader reader = null;
            int isUpdateds = 0;
            bool isUpdated = false;
            int rowsAffected = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@MasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(masterName);
                parameters[1] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(globalMasterDetail.CreatedBy);
                parameters[2] = new SqlParameter("@MasterValue", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.MasterValue);
                parameters[3] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.SiteName);
                parameters[4] = new SqlParameter("@Manufacturers", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.Manufacturers);
                parameters[5] = new SqlParameter("@Types", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.Types);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGlobalMasterDetailAdd, parameters);
                if (reader != null)
                {
                    reader.Read();
                    rowsAffected = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameRowsAffected);
                    isUpdateds = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameIsUpdated);

                    if (isUpdateds == 1)
                        isUpdated = true;
                    else
                        isUpdated = false; 

                }

                return isUpdated;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ Add ]

        #region [ Update ]
        public bool GlobalMasterDetailUpdateByMasterDetailID(GlobalMasterDetail globalMasterDetail, string masterName)
        {
            int isUpdateds = 0;
            bool isUpdated = false;
            int rowsAffected = 0;
            
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(masterName);
                parameters[1] = new SqlParameter("@MasterDetailID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(globalMasterDetail.MasterDetailID);
                parameters[2] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(globalMasterDetail.ModifiedBy);
                parameters[3] = new SqlParameter("@MasterValue", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.MasterValue);
                parameters[4] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.SiteName);
                parameters[5] = new SqlParameter("@Manufacturers", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.Manufacturers);
                parameters[6] = new SqlParameter("@Types", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBString(globalMasterDetail.Types);
                
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGlobalMasterDetailUpdateByMasterDetailID, parameters);
                if (reader != null)
                {
                    reader.Read();
                    rowsAffected = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameRowsAffected);
                    isUpdateds = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameIsUpdated);
                    if (isUpdateds == 1) 
                        isUpdated = true; 
                    else
                        isUpdated = false; 

                }
                return isUpdated;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ Update ]

        #region [ Delete]
        public bool GlobalMasterDetailDeleteByMasterDetailID(GlobalMasterDetail globalMasterDetail)
        {
            bool isUpdated = false;
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@MasterDetailID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(globalMasterDetail.MasterDetailID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGlobalMasterDetailDeleteByMasterDetailID, parameters);
                if (reader != null)
                {
                    reader.Read();
                    isUpdated = DataRowHelper.ConvertToBoolean2(reader, DalHelper.columnNameIsUpdated);
                }
                return isUpdated;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [ Delete ]


        public List<GlobalMaster> GetGlobalMasterAndDetailsByMasterName(string masterName, string searchFilter)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@MasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(masterName);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);
                
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGlobalMasterDetailsByMasterName_List, parameters);
                if (ds != null)
                {
                    ConvertToObject(ds);
                }
                return globalMasterList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        public List<GlobalMaster> GetGlobalMasterAndDetailsByMasterName(string masterName,int siteID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@MasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(masterName);
                parameters[1] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(siteID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGlobalMasterDetailsByMasterNameAndSiteID_List, parameters);
                if (ds != null)
                {
                    ConvertToObject(ds);
                }
                return globalMasterList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        public List<GlobalMaster> GetGlobalMasterAndDetailsByMasterDetailID(string masterName, int masterDetailID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@MasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(masterName);
                parameters[1] = new SqlParameter("@MasterDetailID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(masterDetailID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGlobalMasterDetailsByDetailID_List, parameters);
                if (ds != null)
                {
                    ConvertToObject(ds);
                }
                return globalMasterList;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        

        private List<GlobalMaster> ConvertToObject(DataSet ds)
        {
            List<GlobalMasterDetail> globalMasterDetailList = new List<GlobalMasterDetail>();
            DataTable GlobalMasterdt = new DataTable();
            DataTable GlobalMasterDetailsdt = new DataTable();
            DataTable GlobalSiteMasterdt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[1] != null )
                {
                    GlobalMasterdt = ds.Tables[0];
                    GlobalMasterDetailsdt = ds.Tables[1];
                    GlobalSiteMasterdt = ds.Tables[2];
                    if (GlobalMasterDetailsdt.Rows.Count > 0)
                    {
                        globalMasterDetailList = (from DataRow detail in GlobalMasterDetailsdt.Rows
                                                  select new GlobalMasterDetail
                                                  {
                                                      MasterDetailID = DataRowHelper.ConvertToInteger(detail[columnMasterDetailID]),
                                                      MasterID = DataRowHelper.ConvertToInteger(detail[columnMasterID]),
                                                      MasterValue = DataRowHelper.ConvertToString(detail[columnMasterValue]),
                                                      SubManufacturerID = DataRowHelper.ConvertToInteger(detail[columnSubManufacturerID]),
                                                      Manufacturers = DataRowHelper.ConvertToString(detail[columnManufacturers]),
                                                      SubTypeID = DataRowHelper.ConvertToInteger(detail[columnSubTypeID]),
                                                      Types = DataRowHelper.ConvertToString(detail[columnTypes]),
                                                      SiteID = DataRowHelper.ConvertToInteger(detail[columnSiteID]),
                                                      SiteName = DataRowHelper.ConvertToString(detail[columnSiteName]),
                                                      StatusID = DataRowHelper.ConvertToInteger(detail[columnStatusID]),
                                                      CreatedBy = DataRowHelper.ConvertToInteger(detail[columnCreatedBy]),
                                                      CreatedOn = DataRowHelper.ConvertToDateTime(detail[columnCreatedOn]),
                                                      ModifiedBy = DataRowHelper.ConvertToInteger(detail[columnModifiedBy]),
                                                      ModifiedOn = DataRowHelper.ConvertToDateTime(detail[columnModifiedOn]),
                                                      CreatedUser = DataRowHelper.ConvertToString(detail[columnCreatedUser]),
                                                      SiteList = (from DataRow site in GlobalSiteMasterdt.Rows
                                                                  select new Site
                                                                  {
                                                                    SiteID = DataRowHelper.ConvertToInteger(site[columnSiteID]),
                                                                    SiteName = DataRowHelper.ConvertToString(site[columnSiteName])
                                                                  }).ToList()
                                                  }).ToList();


                    }
                    if (GlobalMasterdt.Rows.Count > 0 && globalMasterDetailList != null && globalMasterDetailList.Count > 0)
                    {
                        globalMasterList = (from DataRow master in GlobalMasterdt.Rows
                                            select new GlobalMaster
                                            {
                                                MasterID = DataRowHelper.ConvertToInteger(master[columnMasterID]),
                                                MasterName = DataRowHelper.ConvertToString(master[columnMasterName]),
                                                MasterDescription = DataRowHelper.ConvertToString(master[columnMasterDescription]),
                                                IsGlobalMaster = DataRowHelper.ConvertToBoolean2(master[columnIsGlobalMaster]),
                                                GlobalMasterDetailList = globalMasterDetailList.FindAll(delegate(GlobalMasterDetail tempGlobalMasterDetail) { return tempGlobalMasterDetail.MasterID == DataRowHelper.ConvertToInteger(master[columnMasterID]); }),
                                                StatusID = DataRowHelper.ConvertToInteger(master[columnStatusID]),
                                                CreatedBy = DataRowHelper.ConvertToInteger(master[columnCreatedBy]),
                                                CreatedOn = DataRowHelper.ConvertToDateTime(master[columnCreatedOn]),
                                                ModifiedBy = DataRowHelper.ConvertToInteger(master[columnModifiedBy]),
                                                ModifiedOn = DataRowHelper.ConvertToDateTime(master[columnModifiedOn])
                                            }).ToList();
                    }
                }
                return globalMasterList;
            }
            return null;
        }

        //private List<GlobalMaster> ConvertToObject(DataTable GlobalMasterdt, DataTable GlobalMasterDetailsdt)
        //{
        //    List<GlobalMasterDetail> globalMasterDetailList = new List<GlobalMasterDetail>();
        //    globalMasterDetailList = (from DataRow detail in GlobalMasterDetailsdt.Rows
        //                              select new GlobalMasterDetail
        //                                   {
        //                                       MasterDetailID = DataRowHelper.ConvertToInteger(detail[columnMasterDetailID]),
        //                                       MasterID = DataRowHelper.ConvertToInteger(detail[columnMasterID]),
        //                                       MasterValue = DataRowHelper.ConvertToString(detail[columnMasterValue]),
        //                                       StatusID = DataRowHelper.ConvertToInteger(detail[columnStatusID]),
        //                                       CreatedBy = DataRowHelper.ConvertToInteger(detail[columnCreatedBy]),
        //                                       CreatedOn = DataRowHelper.ConvertToDateTime(detail[columnCreatedOn]),
        //                                       ModifiedBy = DataRowHelper.ConvertToInteger(detail[columnModifiedBy]),
        //                                       ModifiedOn = DataRowHelper.ConvertToDateTime(detail[columnModifiedOn]),
        //                                   }).ToList();    



        //    List<GlobalMaster> globalMasterList = new List<GlobalMaster>();
        //    globalMasterList = (from DataRow master in GlobalMasterdt.Rows
        //                        select new GlobalMaster
        //                             {
        //                                 MasterID = DataRowHelper.ConvertToInteger(master[columnMasterID]),
        //                                 MasterName = DataRowHelper.ConvertToString(master[columnMasterName]),
        //                                 MasterDescription = DataRowHelper.ConvertToString(master[columnMasterDescription]),
        //                                 IsGlobalMaster = DataRowHelper.ConvertToBoolean2(master[columnIsGlobalMaster]),
        //                                 GlobalMasterDetailList= globalMasterDetailList.FindAll(delegate(GlobalMasterDetail tempGlobalMasterDetail) { return tempGlobalMasterDetail.MasterID == DataRowHelper.ConvertToInteger(master[columnMasterID]); })
        //                             }).ToList();
        //    return globalMasterList;
        //}
    }
}
