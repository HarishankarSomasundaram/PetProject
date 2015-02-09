using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library;
using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Common;
using ProvisioningTool.Entity;

namespace ProvisioningTool.DAL
{
    internal class HistoryTrackerDAL
    {
        #region [ Declarations ]
        private List<HistoryTracker> HistoryTrackerList;

        private readonly string columnHistoryTrackerID = "HistoryTrackerID";
        private readonly string columnTrackerValue = "TrackerValue";
        private readonly string columnHistoryMasterID = "HistoryMasterID";
        private readonly string columnHistoryMasterName = "HistoryMasterName";
        private readonly string columnHistoryFieldName = "HistoryFieldName";
        private readonly string columnHistoryFieldID = "HistoryFieldID";
        private readonly string columnHistoryMasterFieldMappingID = "HistoryMasterFieldMappingID";
        private readonly string columnHistory = "History";

        #endregion [ Declarations ]

        internal HistoryTrackerDAL()
        {
        }

        #region[ GetAllHistoryTrackers ]
        //Returns the History tracker value from db based on tracker params      
        public HistoryTracker GetHistoryTrackerDetails(PTRequest request)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@TableID", SqlDbType.Int);
                parameters[0].Value = DBValueHelper.ConvertToDBInteger(request.HistoryTracker.HistoryTrackerID);
                parameters[1] = new SqlParameter("@HistoryMasterName", SqlDbType.VarChar);
                parameters[1].Value = DBValueHelper.ConvertToDBString(request.HistoryTracker.HistoryMasterName);
                parameters[2] = new SqlParameter("@HistoryFieldName", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(request.HistoryTracker.HistoryFieldName);
                parameters[3] = new SqlParameter("@IsGlobalMaster", SqlDbType.VarChar);
                parameters[3].Value = DBValueHelper.ConvertToDBBoolean(request.HistoryTracker.IsGlobalMaster);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPHistoryTrackerByMasterNameFieldNameAndTableID_List, parameters);
                if (reader != null)
                {
                    HistoryTrackerList = ProcessDataReader(reader);
                    if (HistoryTrackerList != null)
                    {
                        if (HistoryTrackerList.Count > 0)
                        {
                            return HistoryTrackerList[0];
                        }
                        else return null;
                    }
                    else return null;
                }
                else return null;

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
        #endregion[ GetAllHistoryTrackers ]


        #region [ private methods ]
        //Parses the data reader and converts to object
        private List<HistoryTracker> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                HistoryTrackerList = new List<HistoryTracker>();
                while (reader.Read())
                    HistoryTrackerList.Add(ConvertToObject(reader));
                return HistoryTrackerList;
            }
            return null;
        }

        //Converts each data record into object
        private HistoryTracker ConvertToObject(IDataRecord dataRecord)
        {
            HistoryTracker HistoryTracker = new HistoryTracker();

            //HistoryTracker.HistoryTrackerID = DataRowHelper.ConvertToInteger(dataRecord, columnHistoryTrackerID);
            //HistoryTracker.TrackerValue = DataRowHelper.ConvertToString(dataRecord, columnTrackerValue);
            //HistoryTracker.HistoryMasterName = DataRowHelper.ConvertToString(dataRecord, columnHistoryMasterName);
            //HistoryTracker.HistoryMasterID = DataRowHelper.ConvertToInteger(dataRecord, columnHistoryMasterID);
            //HistoryTracker.HistoryFieldName = DataRowHelper.ConvertToString(dataRecord, columnHistoryFieldName);
            //HistoryTracker.HistoryFieldID = DataRowHelper.ConvertToInteger(dataRecord, columnHistoryFieldID);
            HistoryTracker.HistoryHtml = DataRowHelper.ConvertToString(dataRecord, columnHistory);
            return HistoryTracker;
        }
        #endregion [ private methods ]
    }
}
