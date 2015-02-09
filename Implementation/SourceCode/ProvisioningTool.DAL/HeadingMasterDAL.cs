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
    internal class HeadingMasterDAL
    {
        #region [ Declarations ]
        List<HeadingMaster> headingMasterList;
        HeadingMaster headingMaster;

        #region [Colunm Attributes]
        private readonly string columnHeadingMasterID = "HeadingMasterID";
        private readonly string columnHeadingMasterName = "HeadingMasterName";
        private readonly string columnPriority = "Pri";
        #endregion  [Colunm Attributes]

        #endregion [ Declarations ]

        #region [ Constructor ]

        internal HeadingMasterDAL()
        {
        }

        #endregion [ Constructor ]

        #region [ Add HeadingMaster ]
        public HeadingMaster AddHeadingMaster(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[5];


                parameters[0] = new SqlParameter("@HeadingMasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.HeadingMaster.HeadingMasterName);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.CreatedBy);

                parameters[4] = new SqlParameter("@Priority", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.Priority);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPHeadingMasterAdd, parameters);
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
                return request.HeadingMaster;

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
        #endregion [ Add HeadingMaster ]

        #region [ Delete HeadingMaster ]
        internal bool DeleteHeadingMasterByHeadingMasterID(int HeadingMasterID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@HeadingMasterID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(HeadingMasterID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPHeadingMasterDelete, parameters);
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
        #endregion [ Delete HeadingMaster ]

        #region [ Update HeadingMaster ]
        public HeadingMaster ModifyHeadingMaster(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[9];



                parameters[0] = new SqlParameter("@HeadingMasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(request.HeadingMaster.HeadingMasterName);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.CreatedBy);

                parameters[4] = new SqlParameter("@Priority", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.Priority);

                parameters[5] = new SqlParameter("@HeadingMasterID", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.HeadingMaster.HeadingMasterID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPHeadingMasterEdit, parameters);
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
                return request.HeadingMaster;

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
        #endregion [ Update HeadingMaster ]

        #region [Get All HeadingMaster]
        public List<HeadingMaster> GetAllHeadingMaster()
        {
            SqlDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
              
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPHeadingMasterList);
                if (ds != null)
                {
                    headingMasterList = ProcessDataReader(ds);
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

        #region [Get Heading Master by Heading Master ID ]
        public HeadingMaster GetHeadingMasterByHeadingMasterID(int HeadingMasterID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@HeadingMasterID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(HeadingMasterID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPHeadingMasterbyHeadingMasterID, parameters);
                if (ds != null)
                {
                    headingMasterList = ProcessDataReader(ds);
                }
                return headingMasterList[0];
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [Get Heading Master by Heading Master ID ]

        #region [ Private Function ]

        private List<HeadingMaster> ProcessDataReader(DataSet ds)
        {
            headingMasterList = new List<HeadingMaster>();

            DataTable headingMasterDT = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    headingMasterDT = ds.Tables[0];
            }

            if (headingMasterDT.Rows.Count > 0)
            {
                headingMasterList = (from DataRow headingMaster in headingMasterDT.Rows

                                     select new HeadingMaster
                                     {
                                          HeadingMasterID = DataRowHelper.ConvertToInteger(headingMaster[columnHeadingMasterID]),
                                          HeadingMasterName = DataRowHelper.ConvertToString(headingMaster[ columnHeadingMasterName]),
                                          Priority = DataRowHelper.ConvertToInteger(headingMaster[ columnPriority]),
                                     }).ToList();
            }
            return headingMasterList;
        }

        #endregion [ Private Function ]
    }
}
