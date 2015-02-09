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

    internal class ChecklistDAL
    {
        #region [ Declarations ]
        private List<Checklist> ChecklistItems;
        DataSet dsChecklist;
        private Checklist checklist;

        private readonly string columnCheckListID = "CheckListID";
        private readonly string columnIsChecklistDone = "IsChecklistDone";
        private readonly string columnUserID = "UserID";
        private readonly string columnUserAccountCreation = "UserAccountCreation";
        private readonly string columnAddUserToDepartment = "AddUserToDepartment";
        private readonly string columnAddUserToSecurityGroup = "AddUserToSecurityGroup";
        private readonly string columnAddLoginScript = "AddLoginScript";
        private readonly string columnCreateEmailAccount = "CreateEmailAccount";
        private readonly string columnEmailAddress = "EmailAddress";
        private readonly string columnAddUserToEmailDistributions = "AddUserToEmailDistributions";
        private readonly string columnHostedAntispam = "HostedAntispam";
        private readonly string columnAssignedPrinters = "AssignedPrinters";
        private readonly string columnPerformTest = "PerformTest";
        private readonly string columnCustomerLANdiagram = "CustomerLANdiagram";
        private readonly string columnThanktheCustomer = "ThanktheCustomer";
        private readonly string columnAllTaskCompleted = "AllTaskCompleted";
        private readonly string columnNotes = "Notes";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnStatusID = "StatusID";

        #region [User Attributes]
        private readonly string columnFirstName = "FirstName";
        private readonly string columnLastName = "LastName";
        private readonly string columnUserName = "UserName";
        private readonly string columnPassword = "Password";

        private readonly string columnTitleID = "TitleID";
        private readonly string columnTitleName = "TitleName";

        private readonly string columnDepartmentID = "DepartmentID";
        private readonly string columnDepartmentName = "DepartmentName";

        private readonly string columnUserInfoID = "UserInfoID";
        private readonly string columnMasterID = "MasterID";
        private readonly string columnMasterName = "MasterName";

        private readonly string columnEmail = "Email";
        private readonly string columnPhone1 = "Phone1";
        private readonly string columnPhone2 = "Phone2";


        //Columns to get Multiple selected attribute values of User 
        private readonly string columnSelectedectedAppIDs = "SelectedAppIDs";
        private readonly string columnSelectedectedComputerIDs = "SelectedComputerIDs";
        private readonly string columnSelectedLaptopItems = "SelLaptopItems";
        private readonly string columnSelectedectedMobilePhoneIDs = "SelectedMobilePhoneIDs";
        private readonly string columnSelectedNetworkShareItems = "SelNetworkShareItems";
        private readonly string columnSelectedectedPrinterIDs = "SelectedPrinterIDs";
        private readonly string columnSelectedRemoteAccessItems = "SelRemoteAccessItems";
        private readonly string columnSelectedectedSecurityGroupIDs = "SelectedSecurityGroupIDs";
        private readonly string columnSelectedServerItems = "SelServerItems";
        private readonly string columnSelectedectedTabletIDs = "SelectedTabletIDs";
        #endregion
        #endregion [ Declarations ]

        internal ChecklistDAL()
        {
        }
        #region [ Add Checklist ]
        internal Checklist AddChecklist(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[18];

                parameters[0] = new SqlParameter("@UserID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.Checklist.User.UserID);

                parameters[1] = new SqlParameter("@UserAccountCreation", SqlDbType.Bit);
                parameters[1].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.UserAccountCreation);

                parameters[2] = new SqlParameter("@AddUserToDepartment", SqlDbType.Bit);
                parameters[2].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddUserToDepartment);

                parameters[3] = new SqlParameter("@AddUserToSecurityGroup", SqlDbType.Bit);
                parameters[3].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddUserToSecurityGroup);

                parameters[4] = new SqlParameter("@AddLoginScript", SqlDbType.Bit);
                parameters[4].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddLoginScript);

                parameters[5] = new SqlParameter("@CreateEmailAccount", SqlDbType.Bit);
                parameters[5].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.CreateEmailAccount);

                parameters[6] = new SqlParameter("@EmailAddress", SqlDbType.Bit);
                parameters[6].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.EmailAddress);

                parameters[7] = new SqlParameter("@AddUserToEmailDistributions", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddUserToEmailDistributions);

                parameters[8] = new SqlParameter("@HostedAntispam", SqlDbType.Bit);
                parameters[8].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.HostedAntispam);

                parameters[9] = new SqlParameter("@AssignedPrinters", SqlDbType.Bit);
                parameters[9].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AssignedPrinters);

                parameters[10] = new SqlParameter("@PerformTest", SqlDbType.Bit);
                parameters[10].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.PerformTest);

                parameters[11] = new SqlParameter("@CustomerLANdiagram", SqlDbType.Bit);
                parameters[11].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.CustomerLANdiagram);

                parameters[12] = new SqlParameter("@ThanktheCustomer", SqlDbType.Bit);
                parameters[12].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.ThanktheCustomer);

                parameters[13] = new SqlParameter("@AllTaskCompleted", SqlDbType.Bit);
                parameters[13].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AllTaskCompleted);

                parameters[14] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[14].Value = DBValueHelper.ConvertToDBString(request.Checklist.Notes);

                parameters[15] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[15].Value = DBValueHelper.ConvertToDBInteger(request.Checklist.CreatedBy);

                parameters[17] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[17].Value = DBValueHelper.ConvertToDBInteger(request.Checklist.StatusID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCheckListItemsAdd, parameters);
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
                return request.Checklist;

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
        #endregion [ Add Checklist ]

        #region [ Update Checklist ]
        internal Checklist ModifyChecklist(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[17];

                parameters[0] = new SqlParameter("@CheckListID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.Checklist.CheckListID);

                parameters[1] = new SqlParameter("@UserID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.Checklist.User.UserID);

                parameters[2] = new SqlParameter("@UserAccountCreation", SqlDbType.Bit);
                parameters[2].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.UserAccountCreation);

                parameters[3] = new SqlParameter("@AddUserToDepartment", SqlDbType.Bit);
                parameters[3].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddUserToDepartment);

                parameters[4] = new SqlParameter("@AddUserToSecurityGroup", SqlDbType.Bit);
                parameters[4].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddUserToSecurityGroup);

                parameters[5] = new SqlParameter("@AddLoginScript", SqlDbType.Bit);
                parameters[5].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddLoginScript);

                parameters[6] = new SqlParameter("@CreateEmailAccount", SqlDbType.Bit);
                parameters[6].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.CreateEmailAccount);

                parameters[7] = new SqlParameter("@EmailAddress", SqlDbType.Bit);
                parameters[7].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.EmailAddress);

                parameters[8] = new SqlParameter("@AddUserToEmailDistributions", SqlDbType.Bit);
                parameters[8].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AddUserToEmailDistributions);

                parameters[9] = new SqlParameter("@HostedAntispam", SqlDbType.Bit);
                parameters[9].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.HostedAntispam);

                parameters[10] = new SqlParameter("@AssignedPrinters", SqlDbType.Bit);
                parameters[10].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AssignedPrinters);

                parameters[11] = new SqlParameter("@PerformTest", SqlDbType.Bit);
                parameters[11].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.PerformTest);

                parameters[12] = new SqlParameter("@CustomerLANdiagram", SqlDbType.Bit);
                parameters[12].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.CustomerLANdiagram);

                parameters[13] = new SqlParameter("@ThanktheCustomer", SqlDbType.Bit);
                parameters[13].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.ThanktheCustomer);

                parameters[14] = new SqlParameter("@AllTaskCompleted", SqlDbType.Bit);
                parameters[14].Value = DBValueHelper.ConvertToDBBoolean(request.Checklist.AllTaskCompleted);

                parameters[15] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[15].Value = DBValueHelper.ConvertToDBString(request.Checklist.Notes);

                parameters[16] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[16].Value = DBValueHelper.ConvertToDBInteger(request.Checklist.ModifiedBy);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCheckListItemsUpdate, parameters);
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
                return request.Checklist;

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
        #endregion [ Update Checklist ]

        #region[ Delete Checklist ]
        //Delete/Update Status to 2 the Checklist from the DB based on the given parameters
        public bool DeleteChecklistByChecklistID(int checklistID)
        {
            SqlDataReader reader = null;
            dsChecklist = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ChecklistId", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(checklistID);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPDeleteCheckListItemsByCheckListID, parameters);
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
        #endregion[Delete Checklist]

        #region [Get All Checklists]
        public List<Checklist> GetAllChecklists(int siteID)
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllChecklist);

            SqlDataReader reader = null;
            dsChecklist = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SiteID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(siteID);
                dsChecklist = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCheckListItems_List, parameters);
                if (dsChecklist != null)
                {
                    ChecklistItems = ConvertAllChecklistAttributesToObjectList(dsChecklist);
                    if (ChecklistItems != null && ChecklistItems.Count > 0)
                    {
                        return ChecklistItems;
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
        #endregion [ GET ALL Checklist ]

        #region [Get Checklist And Checklist Attribute Details By userID]

        public Checklist GetChecklistAndChecklistDetailsByUserID(int userID)
        {

            dsChecklist = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@UserID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(userID);
                dsChecklist = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCheckListItemsByUserID_List, parameters);
                if (dsChecklist != null)
                {
                    ChecklistItems = ConvertAllChecklistAttributesToObjectList(dsChecklist);
                    if (ChecklistItems != null && ChecklistItems.Count > 0)
                    {
                        return ChecklistItems[0];
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

        //All the Checklist Attributes list with Corresponding values
        ///this will build the list atttributes--such as [ .. to List]
        public List<Checklist> ConvertAllChecklistAttributesToObjectList(DataSet ds)
        {
            ChecklistItems = new List<Checklist>();
            //List<UserApp> userAppsDetailList = new List<UserApp>();


            DataTable Checklistdt = new DataTable();
            //DataTable userAppsDetaildt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    Checklistdt = ds.Tables[0];

                    //Convert Checklist Data table to its Corresponding List
                    if (Checklistdt.Rows.Count > 0)
                    {
                        ChecklistItems = (from DataRow checklist in Checklistdt.Rows
                                          select new Checklist
                                          {

                                              CheckListID = DataRowHelper.ConvertToInteger(checklist[columnCheckListID], 0),
                                              User = (from DataRow checklistUser in Checklistdt.Rows
                                                      where checklistUser.Field<int>(columnUserID) == DataRowHelper.ConvertToInteger(checklist[columnUserID])
                                                      select (new User
                                                      {
                                                          UserID = DataRowHelper.ConvertToInteger(checklist[columnUserID], 0),
                                                          FirstName = DataRowHelper.ConvertToString(checklist[columnFirstName], ""),
                                                          LastName = DataRowHelper.ConvertToString(checklist[columnLastName], ""),
                                                          UserName = DataRowHelper.ConvertToString(checklist[columnUserName], ""),
                                                          Password = DataRowHelper.ConvertToString(checklist[columnPassword], ""),

                                                          //Title.MasterDetailID = DataRowHelper.ConvertToInteger(checklist[columnTitleID],""),
                                                          TitleID = DataRowHelper.ConvertToInteger(checklist[columnTitleID]),
                                                          TitleName = DataRowHelper.ConvertToString(checklist[columnTitleName], ""),

                                                          //Department.MasterDetailID = DataRowHelper.ConvertToInteger(checklist[columnDepartmentID],""),
                                                          DepartmentID = DataRowHelper.ConvertToInteger(checklist[columnDepartmentID]),
                                                          DepartmentName = DataRowHelper.ConvertToString(checklist[columnDepartmentName], ""),

                                                          SelectedApps = DataRowHelper.ConvertToString(checklist[columnSelectedectedAppIDs], ""),
                                                          SelectedComputer = DataRowHelper.ConvertToString(checklist[columnSelectedectedComputerIDs], ""),
                                                          SelectedMobilePhone = DataRowHelper.ConvertToString(checklist[columnSelectedectedMobilePhoneIDs], ""),
                                                          SelectedPrinter = DataRowHelper.ConvertToString(checklist[columnSelectedectedPrinterIDs], ""),
                                                          SelectedSecurityGroup = DataRowHelper.ConvertToString(checklist[columnSelectedectedSecurityGroupIDs], ""),

                                                          SelectedTablet = DataRowHelper.ConvertToString(checklist[columnSelectedectedTabletIDs], ""),
                                                          SelectedLaptop = DataRowHelper.ConvertToString(checklist[columnSelectedLaptopItems], ""),
                                                          SelectedNetworkShares = DataRowHelper.ConvertToString(checklist[columnSelectedNetworkShareItems], ""),
                                                          SelectedRemoteAccess = DataRowHelper.ConvertToString(checklist[columnSelectedRemoteAccessItems], ""),
                                                          SelectedServers = DataRowHelper.ConvertToString(checklist[columnSelectedServerItems], ""),
                                                          //Convert each Object List to sub Lists

                                                          Email = DataRowHelper.ConvertToString(checklist[columnEmail], ""),
                                                          Phone1 = DataRowHelper.ConvertToString(checklist[columnPhone1], ""),
                                                          Phone2 = DataRowHelper.ConvertToString(checklist[columnPhone2], ""),

                                                      })).FirstOrDefault(),

                                              UserAccountCreation = DataRowHelper.ConvertToBoolean(checklist[columnUserAccountCreation]),
                                              AddUserToDepartment = DataRowHelper.ConvertToBoolean(checklist[columnAddUserToDepartment]),
                                              AddUserToSecurityGroup = DataRowHelper.ConvertToBoolean(checklist[columnAddUserToSecurityGroup]),
                                              AddLoginScript = DataRowHelper.ConvertToBoolean(checklist[columnAddLoginScript]),
                                              CreateEmailAccount = DataRowHelper.ConvertToBoolean(checklist[columnCreateEmailAccount]),
                                              EmailAddress = DataRowHelper.ConvertToBoolean(checklist[columnEmailAddress]),
                                              AddUserToEmailDistributions = DataRowHelper.ConvertToBoolean(checklist[columnAddUserToEmailDistributions]),
                                              HostedAntispam = DataRowHelper.ConvertToBoolean(checklist[columnHostedAntispam]),
                                              AssignedPrinters = DataRowHelper.ConvertToBoolean(checklist[columnAssignedPrinters]),
                                              PerformTest = DataRowHelper.ConvertToBoolean(checklist[columnPerformTest]),
                                              CustomerLANdiagram = DataRowHelper.ConvertToBoolean(checklist[columnCustomerLANdiagram]),
                                              ThanktheCustomer = DataRowHelper.ConvertToBoolean(checklist[columnThanktheCustomer]),
                                              AllTaskCompleted = DataRowHelper.ConvertToBoolean(checklist[columnAllTaskCompleted]),
                                              Notes = DataRowHelper.ConvertToString(checklist[columnNotes]),
                                              CreatedBy = DataRowHelper.ConvertToInteger(checklist[columnCreatedBy]),
                                              ModifiedBy = DataRowHelper.ConvertToInteger(checklist[columnModifiedBy]),
                                              StatusID = DataRowHelper.ConvertToInteger(checklist[columnStatusID]),
                                              View = DataRowHelper.ConvertToString("<a href=CustomerInfo.aspx?do=m&nav=Provisioning%20Check%20List&id=" + DataRowHelper.ConvertToString(checklist[columnUserID]) + "#hTab-2 style='color: blue;text-decoration: underline;'>Print</a>"),
                                              IsChecklistDone = DataRowHelper.ConvertToBoolean(checklist[columnAllTaskCompleted]) == false ? ConvertHelper.ConvertToString("<span class='pending'>Pending</span>") : ConvertHelper.ConvertToString("<span class='complete'>Completed</sapn>")
                                          }).ToList();
                    }
                }

                return ChecklistItems;

            }
            return null;
        }

    }
}
