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
    internal class HardDiskDAL
    {
        #region [ Declarations ]
        List<HardDisk> hardDiskList;
        List<HardDiskDrive> hardDiskDriveList;
        HardDiskDrive hardDiskDrive;
        HardDisk hardDisk;

        #region [Colunm Attributes]

        private readonly string columnSystemHardDiskID = "SystemHardDiskID";
        private readonly string columnHardDriveName = "HardDriveName";
        private readonly string columnSize = "Size";
        private readonly string columnSizeUnit = "SizeUnit";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnModifiedOn = "ModifiedOn";
        private readonly string columnSystemHardDiskDriveID = "SystemHardDiskDriveID";
        private readonly string columnDriveCharacter = "DriveCharacter";

        #endregion  [Colunm Attributes]

        #endregion [ Declarations ]

        #region [ Constructor ]

        internal HardDiskDAL()
        {
        }

        #endregion [ Constructor ]

        #region [ Add HardDisk ]
        public HardDisk AddHardDisk(PTRequest request, out bool isDuplicate, out int rowsAffected,out int hardDiskID)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected =hardDiskID= 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[6];


                parameters[0] = new SqlParameter("@Size", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.Size);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.CreatedBy);

                parameters[4] = new SqlParameter("@SystemHardDiskName", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.HardDisk.HardDiskName);

                parameters[5] = new SqlParameter("@SizeUnit", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.HardDisk.SizeUnit);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSystemHardDiskAdd, parameters);
                if (reader != null)
                {
                    reader.Read();
                    rowsAffected = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameRowsAffected);
                    isDuplicate = DataRowHelper.ConvertToBoolean(reader, DalHelper.columnNameIsDuplicate);
                    hardDiskID = DataRowHelper.ConvertToInteger(reader, columnSystemHardDiskID);
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                return request.HardDisk;

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
        #endregion [ Add HardDisk ]

        #region [ Add HardDiskDrive ]
        public HardDiskDrive AddHardDiskDrive(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[7];


                parameters[0] = new SqlParameter("@SystemHardDiskID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.SystemHardDiskID);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.CreatedBy);

                parameters[4] = new SqlParameter("@SizeUnit", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.HardDiskDrive.SizeUnit);

                parameters[5] = new SqlParameter("@Size", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.Size);

                parameters[6] = new SqlParameter("@DriveCharacter", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.HardDiskDrive.DriveCharacter);


                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSystemHardDiskDriveAdd, parameters);
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
                return request.HardDiskDrive;

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
        #endregion [ Add HardDiskDriveDrive ]

        #region [ Delete HardDisk ]
        internal bool DeleteHardDiskByHardDiskID(int HardDiskID)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("@SystemHardDiskID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(HardDiskID);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSystemHardDiskDelete, parameters);
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
        #endregion [ Delete HardDisk ]

        #region [ Update HardDisk ]
        public HardDisk ModifyHardDisk(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[7];


                parameters[0] = new SqlParameter("@Size", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.Size);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.CreatedBy);

                parameters[4] = new SqlParameter("@SystemHardDiskName", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.HardDisk.HardDiskName);

                parameters[5] = new SqlParameter("@SizeUnit", SqlDbType.VarChar);
                parameters[5].Value = DBValueHelper.ConvertToDBString(request.HardDisk.SizeUnit);

                parameters[6] = new SqlParameter("@SystemHardDiskID", SqlDbType.Int);
                parameters[6].Value = DBValueHelper.ConvertToDBInteger(request.HardDisk.SystemHardDiskID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSystemHardDiskEdit, parameters);
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
                return request.HardDisk;

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
        #endregion [ Update HardDisk ]

        #region [ Update HardDiskDrive ]
        public HardDiskDrive ModifyHardDiskDrive(PTRequest request, out bool isDuplicate, out int rowsAffected)
        {
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isDuplicate = false;
                SqlParameter[] parameters = new SqlParameter[8];


                parameters[0] = new SqlParameter("@HeadingID", SqlDbType.Int);
                parameters[0] = new SqlParameter("@SystemHardDiskID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.SystemHardDiskID);

                parameters[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.CreatedBy);

                parameters[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                parameters[2].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.StatusID);

                parameters[3] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.CreatedBy);

                parameters[4] = new SqlParameter("@SizeUnit", SqlDbType.VarChar);
                parameters[4].Value = DBValueHelper.ConvertToDBString(request.HardDiskDrive.SizeUnit);

                parameters[5] = new SqlParameter("@Size", SqlDbType.Int);
                parameters[5].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.Size);

                parameters[6] = new SqlParameter("@DriveCharacter", SqlDbType.VarChar);
                parameters[6].Value = DBValueHelper.ConvertToDBString(request.HardDiskDrive.DriveCharacter);

                parameters[7] = new SqlParameter("@SystemHardDiskDriveID", SqlDbType.Int);
                parameters[7].Value = DBValueHelper.ConvertToDBInteger(request.HardDiskDrive.SystemHardDiskDriveID);



                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSystemHardDiskDriveEdit, parameters);
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
                return request.HardDiskDrive;

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
        #endregion [ Update HardDiskDrive ]

        #region [Get All HardDisk]
        public List<HardDisk> GetAllHardDisk()
        {
            SqlDataReader reader = null;
            try
            {
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSystemHardDiskList);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return hardDiskList;
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
        #endregion  [Get All HardDisk]


        #region [Get Hard Drive by Hard Drive ID ]
        public HardDisk GetHardDiskByHardDiskID(int HardDiskID)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SystemHardDiskID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInt(HardDiskID);
                ds = SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPSystemHardDiskbySystemHardDiskID, parameters);
                if (ds != null)
                {
                    hardDiskList = ConvertAllUserAttributesToObject(ds);
                    if (hardDiskList != null && hardDiskList.Count > 0)
                    {
                        return hardDiskList[0];
                    }
                    else
                        return null;
                }
                return hardDisk;
            }
            catch (SqlException SQLException)
            {
                throw SQLException;
            }
        }
        #endregion [Get Hard Drive by Hard Drive ID ]

        #region [ Private Function ]

        private List<HardDisk> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                hardDiskList = new List<HardDisk>();
                while (reader.Read())
                    hardDiskList.Add(ConvertToObject(reader));
                return hardDiskList;
            }
            return null;
        }

        private HardDisk ConvertToObject(IDataRecord dataRecord)
        {
            hardDisk = new HardDisk();

            hardDisk.SystemHardDiskID = DataRowHelper.ConvertToInteger(dataRecord, columnSystemHardDiskID);
            hardDisk.HardDiskName = DataRowHelper.ConvertToString(dataRecord, columnHardDriveName);
            hardDisk.Size = DataRowHelper.ConvertToInteger(dataRecord, columnSize);
            hardDisk.SizeUnit = DataRowHelper.ConvertToString(dataRecord, columnSizeUnit);
            hardDisk.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
            hardDisk.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
            hardDisk.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
            hardDisk.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);
            return hardDisk;
        }

        private List<HardDisk> ConvertAllUserAttributesToObject(DataSet ds)
        {
            hardDiskList = new List<HardDisk>();

            DataTable hardDiskDT = new DataTable();
            DataTable hardDiskDetailDT = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0] != null)
                    hardDiskDT = ds.Tables[0];
                if (ds.Tables[1] != null)
                    hardDiskDetailDT = ds.Tables[1];
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hardDiskList = (from DataRow hardDisk in hardDiskDT.Rows

                                        select new HardDisk
                                        {
                                            SystemHardDiskID = DataRowHelper.ConvertToInteger(hardDisk[columnSystemHardDiskID]),
                                            HardDiskName = DataRowHelper.ConvertToString(hardDisk[columnHardDriveName]),
                                            Size = DataRowHelper.ConvertToInteger(hardDisk[columnSize]),
                                            SizeUnit = DataRowHelper.ConvertToString(hardDisk[columnSizeUnit]),
                                            HardDiskDrive = (from DataRow hardiskDrive in hardDiskDetailDT.Rows
                                                             where hardiskDrive.Field<int>(columnSystemHardDiskID) == DataRowHelper.ConvertToInteger(hardDisk[columnSystemHardDiskID])
                                                             select (new HardDiskDrive
                                                                   {
                                                                       SystemHardDiskDriveID = DataRowHelper.ConvertToInteger(hardiskDrive[columnSystemHardDiskDriveID]),
                                                                       Size = DataRowHelper.ConvertToInteger(hardiskDrive[columnSize]),
                                                                       SizeUnit = DataRowHelper.ConvertToString(hardiskDrive[columnSizeUnit]),
                                                                       DriveCharacter = DataRowHelper.ConvertToString(hardiskDrive[columnDriveCharacter]),
                                                                   })).ToList(),
                                        }).ToList();
                    }
                }


            }
            return hardDiskList;
        }

        #endregion [ Private Function ]
    }
}
