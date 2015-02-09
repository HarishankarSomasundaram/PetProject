using Microsoft.ApplicationBlocks.Data;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.DAL
{
    internal class CompanyDAL
    {
        #region [ Declarations ]
        private List<Company> CompanyList;
        private readonly string columnCompanyID = "CompanyID";
        private readonly string columnCompanyName = "CompanyName";
        private readonly string columnCompanyAddress1 = "CompanyAddress1";
        private readonly string columnCompanyAddress2 = "CompanyAddress2";
        private readonly string columnCompanyAddress3 = "CompanyAddress3";
        private readonly string columnPostalCode = "PostalCode";
        private readonly string columnPhoneNumber = "PhoneNumber";
        private readonly string columnWebAddress = "WebAddress";
        private readonly string columnStatusID = "StatusID";
        private readonly string columnCreatedBy = "CreatedBy";
        private readonly string columnModifiedBy = "ModifiedBy";
        private readonly string columnCreatedOn = "CreatedOn";
        private readonly string columnModifiedOn = "ModifiedOn";


        #endregion [ Declarations ]

        public List<Company> GetAllCompanies()
        {
            //return SqlHelper.ExecuteDataset(DBConnectionManager.CurrentConnection, DalHelper.SPGetAllCompanies);

            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@CompanyId", SqlDbType.Int);
                parameters[0].Value = null;
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPCompanies_List, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return CompanyList;
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
        public List<Company> GetApplicationUserByUserName(string username)
        {
            SqlDataReader reader = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Username", SqlDbType.VarChar);
                parameters[0].Value = DBValueHelper.ConvertToDBString(username);
                reader = SqlHelper.ExecuteReader(DBConnectionManager.CurrentConnection, CommandType.StoredProcedure, DalHelper.SPGetApplicationUserByUserName, parameters);
                if (reader != null)
                {
                    return ProcessDataReader(reader);
                }
                return CompanyList;
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

        private List<Company> ProcessDataReader(SqlDataReader reader)
        {
            if (!reader.IsClosed && reader.HasRows)
            {
                CompanyList = new List<Company>();
                while (reader.Read())
                    CompanyList.Add(ConvertToObject(reader));
                return CompanyList;
            }
            return null;
        }

        private Company ConvertToObject(IDataRecord dataRecord)
        {
            Company company = new Company();
            company.CompanyID = DataRowHelper.ConvertToInteger(dataRecord, columnCompanyID);
            company.CompanyName = DataRowHelper.ConvertToString(dataRecord, columnCompanyName);
            company.CompanyAddress1 = DataRowHelper.ConvertToString(dataRecord, columnCompanyAddress1);
            company.CompanyAddress2 = DataRowHelper.ConvertToString(dataRecord, columnCompanyAddress2);
            company.CompanyAddress3 = DataRowHelper.ConvertToString(dataRecord, columnCompanyAddress3);
            company.PostalCode = DataRowHelper.ConvertToString(dataRecord, columnPostalCode);
            company.PhoneNumber = DataRowHelper.ConvertToString(dataRecord, columnPhoneNumber);
            company.WebAddress = DataRowHelper.ConvertToString(dataRecord, columnWebAddress);
            company.StatusID = DataRowHelper.ConvertToInteger(dataRecord, columnStatusID);
            company.CreatedBy = DataRowHelper.ConvertToInteger(dataRecord, columnCreatedBy);
            company.CreatedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnCreatedOn);
            company.ModifiedBy = DataRowHelper.ConvertToInteger(dataRecord, columnModifiedBy);
            company.ModifiedOn = DataRowHelper.ConvertToDateTime(dataRecord, columnModifiedOn);

            return company;
        }
    }
}
