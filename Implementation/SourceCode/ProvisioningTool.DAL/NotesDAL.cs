using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;
using Library;


namespace ProvisioningTool.DAL
{
    internal class NotesDAL
    {
        internal NotesDAL()
        {

        }


        #region [ Update Notes Master ]
        internal NotesMaster UpdateNotes(NotesMaster notesmaster, out bool isUpdated, out int rowsAffected)
        {
            
            SqlDataReader reader = null;
            try
            {

                rowsAffected = 0;
                isUpdated = false;
                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@NotesMasterName", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(notesmaster.NotesMasterName);

                parameters[1] = new SqlParameter("@NotesClientID", SqlDbType.Int);
                parameters[1].Value = DBValueHelper.ConvertToDBInteger(notesmaster.NotesDetails.NotesClientID);

                parameters[2] = new SqlParameter("@Notes", SqlDbType.VarChar);
                parameters[2].Value = DBValueHelper.ConvertToDBString(notesmaster.NotesDetails.Notes);

                parameters[3] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                parameters[3].Value = DBValueHelper.ConvertToDBInteger(notesmaster.ModifiedBy);

                parameters[4] = new SqlParameter("@isFromIOS", SqlDbType.Int);
                parameters[4].Value = DBValueHelper.ConvertToDBInteger(notesmaster.NotesDetails.isFromIOS);

                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPNotesUpdate, parameters);
                if (reader != null)
                {
                    reader.Read();
                    rowsAffected = DataRowHelper.ConvertToInteger(reader, DalHelper.columnNameRowsAffected);
                    isUpdated = DataRowHelper.ConvertToBoolean(reader, DalHelper.columnNameIsUpdated);
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                return notesmaster;

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
        #endregion [ Update Notes Master ]
    }
}
