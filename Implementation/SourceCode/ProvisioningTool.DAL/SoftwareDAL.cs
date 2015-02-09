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

    internal class SoftwareDAL
    {
        #region [ Declarations ]
        private List<Software> SoftwareList;
        DataSet dsSoftware;
        private Software software;

        private readonly string columnSoftwareID = "SoftwareID";
        private readonly string columnApplication = "Application";
        private readonly string columnSoftwareDescription = "SoftwareDescription";
        private readonly string columnLicenseKey = "LicenseKey";
        private readonly string columnServer = "Server";
        private readonly string columnPathID = "PathID";
        private readonly string columnAssignedUserID = "AssignedUserID";
        private readonly string columnAssignedUserNames = "AssignedUserNames";
        private readonly string columnVersion = "Version";
        private readonly string columnNotes = "Notes";
        private readonly string columnInstalledOn = "InstalledOn";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnClientID = "ClientID";
        private readonly string columnSystemMasterID = "SystemMasterID";
        private readonly string columnUserID = "UserID";
        private readonly string columnSystemID = "SystemID";   
        private readonly string columnFirstName = "FirstName";
        private readonly string columnLastName = "LastName";
        private readonly string columnUserName = "UserName";
        private readonly string columnSystemMasterName = "SystemMasterName";

        #endregion [ Declarations ]

        internal SoftwareDAL()
        {
        }
        #region [ Add Software ]
        internal Software AddSoftware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@Application", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.Software.Application);

                parameters[1] = new SqlParameter("@SoftwareDescription", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.Software.SoftwareDescription);

                parameters[2] = new SqlParameter("@LicenseKey", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.Software.LicenseKey);

                parameters[3] = new SqlParameter("@Server", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.Software.Server);

                parameters[4] = new SqlParameter("@PathID", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.Software.PathID);

                parameters[5] = new SqlParameter("@AssignedUserID", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.Software.SelectedAssignedUsers);

                parameters[6] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.Software.Notes);

                parameters[7] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.Software.StatusID);

                parameters[8] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[8].Value = DBValueHelper.ConvertToDBInteger(request.Software.CreatedBy);

                parameters[9] = new SqlParameter("@InstalledOn", SqlDbType.DateTime);
                parameters[9].Value = DBValueHelper.ConvertToDBDate(request.Software.InstalledOn);

                parameters[10] = new SqlParameter("@Version", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.Software.Version);

                parameters[11] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[11].Value = DBValueHelper.ConvertToDBInteger(request.sessionSiteID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSoftwareAdd, parameters);
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
                return request.Software;

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
        #endregion [ Add Software ]

        #region [ Update Software ]
        internal Software ModifySoftware(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[11];

                parameters[0] = new SqlParameter("@SoftwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.Software.SoftwareID);

                parameters[1] = new SqlParameter("@Application", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.Software.Application);

                parameters[2] = new SqlParameter("@SoftwareDescription", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.Software.SoftwareDescription);

                parameters[3] = new SqlParameter("@LicenseKey", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBString(request.Software.LicenseKey);

                parameters[4] = new SqlParameter("@Server", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.Software.Server);

                parameters[5] = new SqlParameter("@PathID", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.Software.PathID);

                parameters[6] = new SqlParameter("@AssignedUserID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.Software.SelectedAssignedUsers);

                parameters[7] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[7].Value = DBValueHelper.ConvertToDBString(request.Software.Notes);

                parameters[8] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[8].Value = DBValueHelper.ConvertToDBInteger(request.Software.ModifiedBy);

                parameters[9] = new SqlParameter("@InstalledOn", SqlDbType.VarChar);
                parameters[9].Value = DBValueHelper.ConvertToDBString(request.Software.InstalledOn);

                parameters[10] = new SqlParameter("@Version", SqlDbType.VarChar);
                parameters[10].Value = DBValueHelper.ConvertToDBString(request.Software.Version);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSoftwareUpdate, parameters);
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
                return request.Software;

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
        #endregion [ Update Software ]

        #region[ Delete Software ]
        //Delete/Update Status to 2 the Software from the DB based on the given parameters
        public bool DeleteSoftwareBySoftwareID(int softwareID)
        {
            SqlDataReader reader = null;
            dsSoftware = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SoftwareId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(softwareID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteSoftwareBySoftwareID, parameters);
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
        #endregion[Delete Software]

        #region [Get All Softwares]
        public List<Software> GetAllSoftwares(int siteID, string searchFilter)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllSoftware);

            SqlDataReader reader = null;
            dsSoftware = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                parameters[1] = new SqlParameter("@searchFilter", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(searchFilter);

                dsSoftware = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSoftware_List, parameters);
                if (dsSoftware != null)
                {
                    SoftwareList = ConvertAllSoftwareAttributesToObjectList(dsSoftware);
                    if (SoftwareList != null && SoftwareList.Count > 0)
                    {
                        return SoftwareList;
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
        #endregion [ GET ALL Software ]

        #region [Get Software And Software Attribute Details By SoftwareID]

        public Software GetSoftwareAndSoftwareDetailsBySoftwareID(int softwareID)
        {

            dsSoftware = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SoftwareID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(softwareID);
                dsSoftware = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSoftwareBySoftwareID, parameters);
                if (dsSoftware != null)
                {
                    SoftwareList = ConvertAllSoftwareAttributesToObjectList(dsSoftware);
                    if (SoftwareList != null && SoftwareList.Count > 0)
                    {
                        return SoftwareList[0];
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


        #region [Convert All Software Attributes To Object List]
        //All the Software Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<Software> ConvertAllSoftwareAttributesToObjectList(DataSet ds)
        {
            SoftwareList = new List<Software>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();

            DataTable Softwaredt = new DataTable();
            DataTable assignedUserDT = new DataTable();
            DataTable userDT = new DataTable();
            DataTable systemMasterDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0] != null)
                        Softwaredt = ds.Tables[0];
                    if (ds.Tables[1] != null)
                        assignedUserDT = ds.Tables[1];
                    if (ds.Tables[3] != null)
                        userDT = ds.Tables[3];
                    if (ds.Tables[2] != null)
                        systemMasterDT = ds.Tables[2];

                    //Convert Software Data table to its Corresponding List
                    if (Softwaredt.Rows.Count > 0)
                    {
                        SoftwareList = (from DataRow software in Softwaredt.Rows
                                        select new Software
                                        {
                                            SoftwareID = DataRowHelper.ConvertToInteger(software[columnSoftwareID]),
                                            Application = DataRowHelper.ConvertToString(software[columnApplication]),
                                            SoftwareDescription = DataRowHelper.ConvertToString(software[columnSoftwareDescription], ""),
                                            LicenseKey = DataRowHelper.ConvertToString(software[columnLicenseKey]),
                                            Server = DataRowHelper.ConvertToString(software[columnServer], ""),
                                            PathID = DataRowHelper.ConvertToString(software[columnPathID], ""),
                                            SelectedAssignedUsers = DataRowHelper.ConvertToString(software[columnAssignedUserNames], ""),
                                            AssignedUser = (from DataRow softwareAssignedUser in assignedUserDT.Rows
                                                            where softwareAssignedUser.Field<int>(columnClientID) == DataRowHelper.ConvertToInteger(software[columnSoftwareID])
                                                                       select (new AssignedUser
                                                                       {
                                                                           ClientID = DataRowHelper.ConvertToInteger(softwareAssignedUser[columnSystemID]),
                                                                           User = (from DataRow user in userDT.Rows
                                                                                   where user.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(softwareAssignedUser[columnUserID])
                                                                                   select (new User
                                                                                   {
                                                                                       UserID = DataRowHelper.ConvertToInteger(user[columnUserID]),
                                                                                       FirstName = DataRowHelper.ConvertToString(user[columnFirstName]),
                                                                                       LastName = DataRowHelper.ConvertToString(user[columnLastName]),
                                                                                       UserName = DataRowHelper.ConvertToString(user[columnUserName])
                                                                                   })).FirstOrDefault(),
                                                                           System = (from DataRow systemMaster in systemMasterDT.Rows
                                                                                     where systemMaster.Field<int>(columnSystemMasterID) == DataRowHelper.ConvertToInteger(softwareAssignedUser[columnSystemID])
                                                                                     select (new SystemMaster
                                                                                     {
                                                                                         SystemMasterID = DataRowHelper.ConvertToInteger(systemMaster[columnSystemMasterID]),
                                                                                         SystemMasterName = DataRowHelper.ConvertToString(systemMaster[columnSystemMasterName])
                                                                                     })).FirstOrDefault(),
                                                                       })).ToList(),
                                            InstalledOn = DataRowHelper.ConvertToString(software[columnInstalledOn]),
                                            Notes = DataRowHelper.ConvertToString(software[columnNotes], ""),
                                            Version = DataRowHelper.ConvertToString(software[columnVersion], ""),
                                            StatusID = DataRowHelper.ConvertToInteger(software[columnStatusID]),
                                            CreatedBy = DataRowHelper.ConvertToInteger(software[columnCreatedBy]),
                                            CreatedOn = DataRowHelper.ConvertToDateTime(software[columnCreatedOn]),
                                            ModifiedBy = DataRowHelper.ConvertToInteger(software[columnModifiedBy]),
                                            ModifiedOn = DataRowHelper.ConvertToDateTime(software[columnModifiedOn]),
                                            View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Softwares&id=" + DataRowHelper.ConvertToString(software[columnSoftwareID]) + " style='color: blue;text-decoration: underline;'>More</a>"),
                                        }).ToList();
                        return SoftwareList;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            return null;
        }

        #endregion
    }
}
