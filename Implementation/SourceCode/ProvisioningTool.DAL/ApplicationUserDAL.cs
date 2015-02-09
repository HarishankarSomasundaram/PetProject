using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProvisioningTool.DAL
{
    internal class ApplicationUserDAL
    {
        #region [ Declarations ]
        private List<ApplicationUser> ApplicationUserList;
        private readonly string columnApplicationUserID = "ApplicationUserID";
        private readonly string columnApplicationUsername = "ApplicationUsername";
        private readonly string columnApplicationPassword = "ApplicationPassword";
        private readonly string columnRoleName = "RoleName";
        private readonly string columnEmailID = "EmailID";
        private readonly string columnRoleID = "RoleID";
        private readonly string columnStatusID = "StatusID";
        #endregion [ Declarations ]


        #region [ Add ApplicationUser ]
        internal ApplicationUser AddApplicationUser(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[6];

                parameters[0] = new SqlParameter("@ApplicationUsername", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.ApplicationUser.ApplicationUsername);

                parameters[1] = new SqlParameter("@ApplicationPassword", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.ApplicationUser.ApplicationPassword);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.ApplicationUser.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.ApplicationUser.CreatedBy);

                parameters[4] = new SqlParameter("@RoleID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.ApplicationUser.Role.RoleID);

                parameters[5] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.ApplicationUser.EmailID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPApplicationUserAdd, parameters);
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
                return request.ApplicationUser;

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
        #endregion [ Add ApplicationUser ]

        #region [ Update ApplicationUser ]
        internal ApplicationUser ModifyApplicationUser(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[7];

                parameters[0] = new SqlParameter("@ApplicationUserID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.ApplicationUser.ApplicationUserID);

                parameters[1] = new SqlParameter("@ApplicationUsername", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.ApplicationUser.ApplicationUsername);

                parameters[2] = new SqlParameter("@ApplicationPassword", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.ApplicationUser.ApplicationPassword);

                parameters[3] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.ApplicationUser.ModifiedBy);

                parameters[4] = new SqlParameter("@RoleID", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.ApplicationUser.Role.RoleID);


                parameters[5] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.ApplicationUser.StatusID);

                parameters[6] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.ApplicationUser.EmailID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPApplicationUserUpdate, parameters);
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
                return request.ApplicationUser;

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
        #endregion [ Update ApplicationUser ]

        public List<ApplicationUser> GetAllApplicationUsers()
        {
            SqlDataReader reader = null;
            DataSet dsApplicationUser = new DataSet();
            try
            {
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGetAllApplicationUsers);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return ApplicationUserList;
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


        public List<ApplicationUser> GetApplicationUserByUserName(string username, out string SqlMessage)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Username", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(username);
                SqlMessage = "";
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGetApplicationUserByUserName, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return ApplicationUserList;
            }
            catch (SqlException SQLException)
            {
                SqlMessage = SQLException.Message;
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }


        public List<ApplicationUser> SearchApplicationUserByKey(string Key, out string SqlMessage)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Key", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(Key);
                SqlMessage = "";
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSearchApplicationUserByKey, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return ApplicationUserList;
            }
            catch (SqlException SQLException)
            {
                SqlMessage = SQLException.Message;
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }

        public List<ApplicationUser> GetApplicationUserByUserNameAndEmail(string username, string email, out string SqlMessage)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Username", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(username, "");
                parameters[1] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(email);
                SqlMessage = "";
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGetApplicationUserByUserNameAndEmail, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return ApplicationUserList;
            }
            catch (SqlException SQLException)
            {
                SqlMessage = SQLException.Message;
                throw SQLException;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }

        #region[ Delete ApplicationUser ]
        //Delete/Update Status to 2 the ApplicationUser from the DB based on the given parameters
        public bool DeleteApplicationUserByApplicationUserID(int applicationUserID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ApplicationUserId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(applicationUserID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteApplicationUserByApplicationUserID, parameters);
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
        #endregion[Delete ApplicationUser]

        #region [Get Software And Software Attribute Details By SoftwareID]

        public ApplicationUser GetApplicationUserByApplicationUserID(int applicationUserID)
        {

            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ApplicationUserID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(applicationUserID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGetApplicationUserByApplicationUserID, parameters);
                if (reader != null)
                {
                    ApplicationUserList = new List<ApplicationUser>();
                    ApplicationUserList = ProcessDataReader(reader);
                    if (ApplicationUserList != null)
                    {
                        if (ApplicationUserList.Count > 0)
                        {
                            return ApplicationUserList[0];
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                return null;

            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }

        #endregion

        private List<ApplicationUser> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                ApplicationUserList = new List<ApplicationUser>();
                while (reader.Read())
                    ApplicationUserList.Add(ConvertToObject(reader));
                return ApplicationUserList;
            }
            return null;
        }

        private ApplicationUser ConvertToObject(IDataRecord dataRecord)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Role = new Role();
            applicationUser.ApplicationUserID = DataRowHelper.ConvertToInteger(dataRecord, columnApplicationUserID);
            applicationUser.ApplicationUsername = DataRowHelper.ConvertToString(dataRecord, columnApplicationUsername);
            applicationUser.ApplicationPassword = DataRowHelper.ConvertToString(dataRecord, columnApplicationPassword);
            applicationUser.Role.RoleName = DataRowHelper.ConvertToString(dataRecord, columnRoleName);
            applicationUser.Role.RoleID = DataRowHelper.ConvertToInteger(dataRecord, columnRoleID);
            applicationUser.EmailID = DataRowHelper.ConvertToString(dataRecord, columnEmailID);
            applicationUser.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
            applicationUser.Status = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID) != 2 ? DataRowHelper.ConvertToString("<span class='complete'>Active</span>") : DataRowHelper.ConvertToString("<span class='pending'>InActive</span>");
            applicationUser.Role.RoleID = DataRowHelper.ConvertToInteger(dataRecord, columnRoleID);
            applicationUser.View = DataRowHelper.ConvertToString("<a href=ManageSystemEngineer.aspx?do=m&nav=AppplicationUser&id=" + DataRowHelper.ConvertToString(applicationUser.ApplicationUserID) + " style='color: blue;text-decoration: underline;'>More</a>");
            return applicationUser;
        }
    }
}
