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
    internal class GroupPolicyDAL
    {
        #region [ Declarations ]
        private List<GroupPolicySetup> groupPolicySetupList;
        private List<GroupPolicy> groupPolicyList;
        private List<HeadingMaster> headingMasterList;
        private List<FieldTypeMaster> fieldTypeMasterList;
        private GroupPolicySetup groupPolicySetup;
        private GroupPolicy groupPolicy;

        #region [Colunm Attributes]

        private readonly string columnGroupPolicyID = "GroupPolicyID";
        private readonly string columnGroupPolicySetupID = "GroupPolicySetupID";
        private readonly string columnGroupPolicyFieldValue = "GroupPolicyFieldValue";
        private readonly string columnHeadingID = "HeadingID";
        private readonly string columnHeadingMasterName = "HeadingMasterName";
        private readonly string columnFieldTypeName = "FieldTypeName";
        private readonly string columnFieldCount = "FieldCount";
        private readonly string columnFieldName = "FieldName";
        private readonly string columnFieldType = "FieldType";
        private readonly string columnIsRequired = "IsRequired";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnFieldTypeMasterID = "FieldTypeMasterID";
        private readonly string columnHeadingMasterID = "HeadingMasterID";
        private readonly string columnPriority = "Priority";
        private readonly string columnSiteID = "SiteID";
        private readonly string columnCustomerID = "CustomerID";
        #endregion  [Colunm Attributes]

        #endregion [ Declarations ]

        #region [ Constructor ]

        internal GroupPolicyDAL()
        {
        }

        #endregion [ Constructor ]

        #region [ Add GroupPolicy ]
        public GroupPolicy AddGroupPolicy(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[5];


                parameters[0] = new SqlParameter("@GroupPolicySetupID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicy.GroupPolicySetupID);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicy.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicy.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicy.CreatedBy);

                parameters[4] = new SqlParameter("@GroupPolicyFieldValue", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.GroupPolicy.GroupPolicyFieldValue);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicyAdd, parameters);
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
                return request.GroupPolicy;

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
        #endregion [ Add GroupPolicy ]

        #region [ Add GroupPolicy Setup ]
        public GroupPolicySetup AddGroupPolicySetup(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[9];


                parameters[0] = new SqlParameter("@HeadingID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.HeadingID);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.CreatedBy);

                parameters[4] = new SqlParameter("@FieldName", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.GroupPolicySetup.FieldName);

                parameters[5] = new SqlParameter("@FieldCount", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.FieldCount);

                parameters[6] = new SqlParameter("@FieldType", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.FieldType);

                parameters[7] = new SqlParameter("@IsRequired", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(request.GroupPolicySetup.IsRequired);

                parameters[8] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[8].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.SiteID);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicySetupAdd, parameters);
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
                return request.GroupPolicySetup;

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
        #endregion [ Add GroupPolicy Setup]

        #region [ Delete GroupPolicy ]
        internal bool DeleteGroupPolicyByGroupPolicyID(int GroupPolicyID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@GroupPolicySetupID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(GroupPolicyID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicySetupDelete, parameters);
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
        #endregion [ Delete GroupPolicy ]

        #region [ Delete GroupPolicy ]
        internal bool DeleteGroupPolicy()
        {
            SqlDataReader reader = null;
            try
            {

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicyDelete);
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
        #endregion [ Delete GroupPolicy ]

        #region [ Update GroupPolicySetup ]
        public GroupPolicySetup ModifyGroupPolicySetup(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[9];


                parameters[0] = new SqlParameter("@HeadingID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.HeadingID);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.CreatedBy);

                parameters[4] = new SqlParameter("@FieldName", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.GroupPolicySetup.FieldName);

                parameters[5] = new SqlParameter("@FieldCount", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.FieldCount);

                parameters[6] = new SqlParameter("@FieldType", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.FieldType);

                parameters[7] = new SqlParameter("@IsRequired", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(request.GroupPolicySetup.IsRequired);

                parameters[8] = new SqlParameter("@GroupPolicySetupID", SqlDbType.Int);
                parameters[8].Value = DBValueHelper.ConvertToDBInteger(request.GroupPolicySetup.GroupPolicySetupID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicySetupEdit, parameters);
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
                return request.GroupPolicySetup;

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
        #endregion [ Update GroupPolicySetup ]

        #region [Get All GroupPolicySetup]
        public List<GroupPolicySetup> GetAllGroupPolicySetup(int siteID)
        {
            SqlDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
              
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicySetupFull, parameters);
                if (ds != null)
                {
                    return ProcessDataSetGPS(ds);
                }

                return groupPolicySetupList;
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
        #endregion  [Get All GroupPolicySetup]

        #region [Get Group Policy Sety by Group Policy Setup ID ]
        public GroupPolicySetup GetGroupPolicySetupByGroupPolicySetupID(int GroupPolicySetupID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@GroupPolicySetupID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(GroupPolicySetupID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicySetupbyGroupPolicySetupID, parameters);
                if (ds != null)
                {
                    groupPolicySetupList = ProcessDataSetGPS(ds);
                }
                return groupPolicySetupList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [Get Group Policy Sety by Group Policy Setup ID ]

        #region [Get All GroupPolicy]
        public List<GroupPolicy> GetAllGroupPolicy()
        {
            DataSet ds = new DataSet();
            try
            {
                //reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicyList);
                //if (reader != null)
                //{
                //    return ProcessDataReaderGP(reader);
                //}
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGroupPolicyList);
                if (ds != null)
                {
                    groupPolicyList = ProcessDataSetGP(ds);
                }
                return groupPolicyList;
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
                if (ds != null )
                    ds.Dispose();
            }
        }
        #endregion  [Get All GroupPolicy]

        #region [Get All FieldTypeMaster]
        public List<FieldTypeMaster> GetAllFieldTypeMaster()
        {
            SqlDataReader reader = null;
            try
            {
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPFieldTypeMasterList);
                if (reader != null)
                {
                    return ProcessDataReaderFTM(reader);
                }
                return fieldTypeMasterList;
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
        #endregion  [Get All FieldTypeMaster]

        #region [Get All HeadingMaster]
        public List<HeadingMaster> GetAllHeadingMaster()
        {
            SqlDataReader reader = null;
            try
            {
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPHeadingMasterList);
                if (reader != null)
                {
                    return ProcessDataReaderHM(reader);
                }
                return headingMasterList;
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
        #endregion  [Get All HeadingMaster]

        #region [ Private Function ]

        private List<GroupPolicy> ProcessDataReaderGP(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                groupPolicyList = new List<GroupPolicy>();
                while (reader.Read())
                    groupPolicyList.Add(ConvertToObjectGP(reader));
                return groupPolicyList;
            }
            return null;
        }

        private List<FieldTypeMaster> ProcessDataReaderFTM(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                fieldTypeMasterList = new List<FieldTypeMaster>();
                while (reader.Read())
                    fieldTypeMasterList.Add(ConvertToObjectFTM(reader));
                return fieldTypeMasterList;
            }
            return null;
        }

        private List<HeadingMaster> ProcessDataReaderHM(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                headingMasterList = new List<HeadingMaster>();
                while (reader.Read())
                    headingMasterList.Add(ConvertToObjectHM(reader));
                return headingMasterList;
            }
            return null;
        }

        private GroupPolicy ConvertToObjectGP(IDataRecord dataRecord)
        {
            groupPolicy = new GroupPolicy();

            groupPolicy.GroupPolicyID = DataRowHelper.ConvertToInteger(dataRecord, columnGroupPolicyID);
            groupPolicy.GroupPolicySetupID = DataRowHelper.ConvertToInteger(dataRecord, columnGroupPolicySetupID);
            groupPolicy.GroupPolicyFieldValue = DataRowHelper.ConvertToString(dataRecord, columnGroupPolicyFieldValue);

            return groupPolicy;
        }

        private FieldTypeMaster ConvertToObjectFTM(IDataRecord dataRecord)
        {
            FieldTypeMaster fieldTypeMaster = new FieldTypeMaster();

            fieldTypeMaster.FieldTypeMasterID = DataRowHelper.ConvertToInteger(dataRecord, columnFieldTypeMasterID);
            fieldTypeMaster.FieldTypeName = DataRowHelper.ConvertToString(dataRecord, columnFieldTypeName);

            return fieldTypeMaster;
        }

        private HeadingMaster ConvertToObjectHM(IDataRecord dataRecord)
        {
            HeadingMaster headingMaster = new HeadingMaster();

            headingMaster.HeadingMasterID = DataRowHelper.ConvertToInteger(dataRecord, columnHeadingMasterID);
            headingMaster.HeadingMasterName = DataRowHelper.ConvertToString(dataRecord, columnHeadingMasterName);

            return headingMaster;
        }

        private List<GroupPolicySetup> ProcessDataSetGPS(DataSet ds)
        {
            groupPolicySetupList = new List<GroupPolicySetup>();

            
            DataTable groupPolicySetupDT = new DataTable();
            DataTable HeadingMasterDT = new DataTable();
            DataTable FieldMasterDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    groupPolicySetupDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    HeadingMasterDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    FieldMasterDT = ds.Tables[2];
            }

             if (groupPolicySetupDT.Rows.Count > 0)
                {
                    groupPolicySetupList = (from DataRow groupPolicysetup in groupPolicySetupDT.Rows

                                      select new GroupPolicySetup
                                      {
                                                GroupPolicySetupID = DataRowHelper.ConvertToInteger(groupPolicysetup[columnGroupPolicySetupID]),
                                                HeadingID = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnHeadingID]),
                                                HeadingName = DataRowHelper.ConvertToString(groupPolicysetup[ columnHeadingMasterName]),
                                                HeadingCount = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnPriority]),
                                                FieldCount = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnFieldCount]),
                                                FieldType = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnFieldType]),
                                                FieldName = DataRowHelper.ConvertToString(groupPolicysetup[ columnFieldName]),
                                                FieldTypeName = DataRowHelper.ConvertToString(groupPolicysetup[ columnFieldTypeName]),
                                                IsRequired = DataRowHelper.ConvertToBoolean(groupPolicysetup[ columnIsRequired]),
                                                CreatedBy = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnCreatedBy]),
                                                ModifiedBy = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnModifiedBy]),
                                                CreatedOn = DataRowHelper.ConvertToDateTime(groupPolicysetup[ columnCreatedOn]),
                                                ModifiedOn = DataRowHelper.ConvertToDateTime(groupPolicysetup[ columnModifiedOn]),
                                                SiteID = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnSiteID]),
                                                CustomerID = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnCustomerID]),
                                                View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Group%20Policy&id=" + DataRowHelper.ConvertToString(groupPolicysetup[columnGroupPolicySetupID]) + " style='color: blue;text-decoration: underline;'>More</a>"),

                                                HeadingMaster = (from DataRow headingMaster in HeadingMasterDT.Rows
                                                                 where headingMaster.Field<int>(columnHeadingMasterID) == DataRowHelper.ConvertToInteger(groupPolicysetup[columnHeadingID])
                                                                 select (new HeadingMaster
                                                                   {
                                                                       HeadingMasterID = DataRowHelper.ConvertToInteger(headingMaster[columnHeadingMasterID]),
                                                                       HeadingMasterName = DataRowHelper.ConvertToString(headingMaster[columnHeadingMasterName])
                                                                   })).FirstOrDefault(),
                                                FieldTypeMaster = (from DataRow fieldTypeMaster in FieldMasterDT.Rows
                                                                   where fieldTypeMaster.Field<int>(columnFieldTypeMasterID) == DataRowHelper.ConvertToInteger(groupPolicysetup[columnFieldTypeMasterID])
                                                                   select (new FieldTypeMaster
                                                                   {
                                                                       FieldTypeMasterID = DataRowHelper.ConvertToInteger(fieldTypeMaster[columnFieldTypeMasterID]),
                                                                       FieldTypeName = DataRowHelper.ConvertToString(fieldTypeMaster[columnFieldTypeName])
                                                                   })).FirstOrDefault(),
                                      }).ToList();
             }

            return groupPolicySetupList;
        }

        private List<GroupPolicy> ProcessDataSetGP(DataSet ds)
        {
            groupPolicyList = new List<GroupPolicy>();

            
            DataTable groupPolicyDT = new DataTable();
            DataTable groupPolicySetupDT = new DataTable();
            DataTable HeadingMasterDT = new DataTable();
            DataTable FieldMasterDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    groupPolicyDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    groupPolicySetupDT = ds.Tables[1];
                if (ds.Tables[2] != null)
                    HeadingMasterDT = ds.Tables[2];
                if (ds.Tables[3] != null)
                    FieldMasterDT = ds.Tables[3];
            }

            if (groupPolicyDT.Rows.Count > 0)
                {
                    groupPolicyList = (from DataRow groupPolicy in groupPolicyDT.Rows

                                      select new GroupPolicy
                                      {
                                                     GroupPolicyID = DataRowHelper.ConvertToInteger(groupPolicy[ columnGroupPolicyID]),
                                                    GroupPolicySetupID = DataRowHelper.ConvertToInteger(groupPolicy[  columnGroupPolicySetupID]),
                                                    GroupPolicyFieldValue = DataRowHelper.ConvertToString(groupPolicy[  columnGroupPolicyFieldValue]),
                                                    GroupPolicySetup = (from DataRow groupPolicysetup in groupPolicySetupDT.Rows
                                                                 where groupPolicysetup.Field<int>(columnGroupPolicySetupID) == DataRowHelper.ConvertToInteger(groupPolicy[columnGroupPolicySetupID])
                                                                 select (new GroupPolicySetup
                                                                   {
                                                                        GroupPolicySetupID = DataRowHelper.ConvertToInteger(groupPolicysetup[columnGroupPolicySetupID]),
                                                                        HeadingID = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnHeadingID]),
                                                                        HeadingName = DataRowHelper.ConvertToString(groupPolicysetup[ columnHeadingMasterName]),
                                                                        HeadingCount = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnPriority]),
                                                                        FieldCount = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnFieldCount]),
                                                                        FieldType = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnFieldType]),
                                                                        FieldName = DataRowHelper.ConvertToString(groupPolicysetup[ columnFieldName]),
                                                                        FieldTypeName = DataRowHelper.ConvertToString(groupPolicysetup[ columnFieldTypeName]),
                                                                        IsRequired = DataRowHelper.ConvertToBoolean(groupPolicysetup[ columnIsRequired]),
                                                                        CreatedBy = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnCreatedBy]),
                                                                        ModifiedBy = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnModifiedBy]),
                                                                        CreatedOn = DataRowHelper.ConvertToDateTime(groupPolicysetup[ columnCreatedOn]),
                                                                        ModifiedOn = DataRowHelper.ConvertToDateTime(groupPolicysetup[ columnModifiedOn]),
                                                                        SiteID = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnSiteID]),
                                                                        CustomerID = DataRowHelper.ConvertToInteger(groupPolicysetup[ columnCustomerID]),
                                                                        View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Group%20Policy&id=" + DataRowHelper.ConvertToString(groupPolicysetup[columnGroupPolicySetupID]) + " style='color: blue;text-decoration: underline;'>More</a>"),

                                                                        HeadingMaster = (from DataRow headingMaster in HeadingMasterDT.Rows
                                                                                         where headingMaster.Field<int>(columnHeadingMasterID) == DataRowHelper.ConvertToInteger(groupPolicysetup[columnHeadingID])
                                                                                         select (new HeadingMaster
                                                                                           {
                                                                                               HeadingMasterID = DataRowHelper.ConvertToInteger(headingMaster[columnHeadingMasterID]),
                                                                                               HeadingMasterName = DataRowHelper.ConvertToString(headingMaster[columnHeadingMasterName])
                                                                                           })).FirstOrDefault(),
                                                                        FieldTypeMaster = (from DataRow fieldTypeMaster in FieldMasterDT.Rows
                                                                                           where fieldTypeMaster.Field<int>(columnFieldTypeMasterID) == DataRowHelper.ConvertToInteger(groupPolicysetup[columnFieldTypeMasterID])
                                                                                           select (new FieldTypeMaster
                                                                                           {
                                                                                               FieldTypeMasterID = DataRowHelper.ConvertToInteger(fieldTypeMaster[columnFieldTypeMasterID]),
                                                                                               FieldTypeName = DataRowHelper.ConvertToString(fieldTypeMaster[columnFieldTypeName])
                                                                                           })).FirstOrDefault(),
                                                                   })).FirstOrDefault(),
                                      }).ToList();
             }

             return groupPolicyList;
        }
        

        #endregion [ Private Function ]
    }
}
