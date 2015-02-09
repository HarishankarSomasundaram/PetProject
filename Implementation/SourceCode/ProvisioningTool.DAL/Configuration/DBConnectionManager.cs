using System;
using System.Data;
using System.Data.SqlClient;
using Library;
using ProvisioningTool.Common;

namespace ProvisioningTool.DAL
{
    public sealed class DBConnectionManager
    {
        #region [ Variable Declarations ]
        [ThreadStatic]
        private static SqlConnection sqlConnection = null;
        private static object syncRoot = new object();
        //holds connection string
        private static string _ConnectionString;
        #endregion

        #region [ Constructor ]
        private DBConnectionManager() { }
        #endregion

        #region [ private methods ]
        public static string GetConnectionString()
        {
            string connectionString = string.Empty;
            connectionString = "Data Source=HARI; User ID=Harishankar;password=Password;Initial Catalog=ProvisioningTool_Local; MultipleActiveResultSets=True";
                //CommonHelper.GetConnectionString("IntelligISConnection");
            if (connectionString != null)
                return connectionString;
            else
                throw new Exception("Invalid Connection String Configuration. Please contact administrator.");
        }
        #endregion

        #region [ public methods ]
        public static void OpenConnection()
        {
            try
            {
                lock (syncRoot)
                {
                    //if connection is not null nad ocnnection is opene, close conn and call dispose
                    if ((sqlConnection != null && sqlConnection.State == ConnectionState.Open))
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                        sqlConnection = null;
                    }
                    //if connectin is not null and for some reason not open too then call dispose method
                    else if (sqlConnection != null)
                    {
                        sqlConnection.Dispose();
                        sqlConnection = null;
                    }
                    //Ensured the sqlconnection is null, hence create a new sql connection
                    sqlConnection = (ConnectionString != null & ConnectionString != string.Empty) ? new SqlConnection(ConnectionString) : new SqlConnection(GetConnectionString());
                    sqlConnection.Open();
                }
            }
            catch
            {
                if (sqlConnection != null) sqlConnection.Dispose(); throw;
            }
        }
        public static void CloseConnection()
        {
            try
            {
                lock (syncRoot)
                {
                    if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlConnection != null) sqlConnection.Dispose();
            }
        }

        public static SqlConnection CurrentConnection
        {
            get
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                    return sqlConnection;
                else
                    OpenConnection();
                return sqlConnection;
            }
        }
        #endregion

        #region [ static properties ]

        public static DBConnectionManager Instance
        {
            get { return new DBConnectionManager(); }
        }
        public static string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
        #endregion
    }
}
