using System;
using System.Data;
using Library;

namespace ProvisioningTool.DAL
{
    public static class DataRowHelper
    {
        static DataRowHelper() { }

        #region [ boolean conversion functions ]

        public static bool ConvertToBoolean(object objectValue)
        {
            return ConvertToBoolean(objectValue, false);
        }

        public static bool ConvertToBoolean(object objectValue, bool defaultValue)
        {
            //bool boolValue;
            if (Convert.IsDBNull(objectValue))
                return defaultValue;
            bool objInt = ConvertHelper.ConvertToBoolean(objectValue, false);
            if (objInt == false)
            {
                return false;
            }
            else if (objInt == true)
            {
                return true;
            }
            //if (bool.TryParse(objectValue.ToString(), out boolValue))
            //{
            //  return boolValue;
            //}
            return defaultValue;
        }

        public static bool ConvertToBoolean(IDataRecord dataRecord, string columnName)
        {
            return ConvertToBoolean(dataRecord, columnName, false);
        }

        public static bool ConvertToBoolean(IDataRecord dataRecord, string columnName, bool defaultValue)
        {
            if (dataRecord != null)
                return ConvertToBoolean(GetObjectValueFromDataRecord(dataRecord, columnName), defaultValue);
            return defaultValue;
        }
        #endregion

        #region [ boolean conversion functions ]

        public static bool ConvertToBoolean2(object objectValue)
        {
            return ConvertToBoolean2(objectValue, false);
        }

        public static bool ConvertToBoolean2(object objectValue, bool defaultValue)
        {
            //bool boolValue;
            if (Convert.IsDBNull(objectValue))
                return defaultValue;
            int objInt = ConvertHelper.ConvertToInteger(objectValue, 0);
            if (objInt == 0)
            {
                return false;
            }
            else if (objInt == 1)
            {
                return true;
            }
            //if (bool.TryParse(objectValue.ToString(), out boolValue))
            //{
            //  return boolValue;
            //}
            return defaultValue;
        }

        public static bool ConvertToBoolean2(IDataRecord dataRecord, string columnName)
        {
            return ConvertToBoolean2(dataRecord, columnName, false);
        }

        public static bool ConvertToBoolean2(IDataRecord dataRecord, string columnName, bool defaultValue)
        {
            if (dataRecord != null)
                return ConvertToBoolean2(GetObjectValueFromDataRecord(dataRecord, columnName), defaultValue);
            return defaultValue;
        }
        #endregion

        #region [ integer conversion functions ]
        public static int ConvertToInteger(object objectValue)
        {
            return ConvertToInteger(objectValue, 0);
        }

        public static int ConvertToInteger(object objectValue, int defaultValue)
        {
            int integerValue;
            if (Convert.IsDBNull(objectValue))
                return defaultValue;
            if (int.TryParse(objectValue.ToString(), out integerValue))
            {
                return integerValue;
            }
            return defaultValue;
        }

        //public static int ConvertToInteger(DataRow objDataRow, string columnName)
        //{
        //    return ConvertToInteger(objDataRow, columnName, 0, false);
        //}

        //public static int ConvertToInteger(DataRow objDataRow, string columnName, int defaultValue, bool throwException)
        //{
        //    if (objDataRow != null)
        //    {
        //        if (IsDataRowContainsColumn(objDataRow, columnName, throwException))
        //        {
        //            return ConvertToInteger(objDataRow[columnName], defaultValue);
        //        }
        //    }
        //    return defaultValue;
        //}

        public static int ConvertToInteger(IDataRecord dataRecord, string columnName)
        {
            return ConvertToInteger(dataRecord, columnName, 0);
        }

        public static int ConvertToInteger(IDataRecord dataRecord, string columnName, int defaultValue)
        {
            if (dataRecord != null)
                return ConvertToInteger(GetObjectValueFromDataRecord(dataRecord, columnName), defaultValue);
            return defaultValue;
        }
        #endregion

        #region [ string functions ]
        public static string ConvertToString(object objectValue)
        {
            return ConvertToString(objectValue, null);
        }

        public static string ConvertToString(object objectValue, string defaultValue)
        {
            if (Convert.IsDBNull(objectValue) || objectValue == null)
                return defaultValue;
            string strVal = Convert.ToString(objectValue);
            if (strVal.Trim().Length == 0)
            {
                return defaultValue;
            }
            return strVal.Trim();
        }

        public static string ConvertToString(IDataRecord dataRecord, string columnName)
        {
            return ConvertToString(dataRecord, columnName, null);
        }

        public static string ConvertToString(IDataRecord dataRecord, string columnName, string defaultValue)
        {
            if (dataRecord != null)
                return ConvertToString(GetObjectValueFromDataRecord(dataRecord, columnName), defaultValue);
            return defaultValue;
        }

        #endregion

        #region [ find functions ]

        public static object GetObjectValueFromDataRecord(IDataRecord dataRecord, string columnName)
        {
            try
            {
                object returnValue;
                returnValue = dataRecord.GetValue(dataRecord.GetOrdinal(columnName));
                return returnValue;
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception(String.Format("Data Column : {0} not found in data record", columnName));
            }
        }
        #endregion

        #region [ datetime functions ]
        public static DateTime ConvertToDateTime(object objectValue)
        {
            return ConvertToDateTime(objectValue, DateTime.MinValue);
        }
        public static DateTime ConvertToDateTime(object objectValue, DateTime defaultValue)
        {
            //Declare the output value
            DateTime DateTimeValue = new DateTime();

            //If te output value is Null, return default value
            if (objectValue == null)
                return defaultValue;

            //Check the whether the object value can be converted to an DateTime, and if it is, returh the out DateTime value
            if (DateTime.TryParse(objectValue.ToString(), out DateTimeValue))
                return DateTimeValue;
            //If the object value cannot be converted to an DateTime value return defaultValue
            return defaultValue;
        }
        public static DateTime ConvertToDateTime(IDataRecord dataRecord, string columnName)
        {
            return ConvertToDateTime(dataRecord, columnName, DateTime.MinValue);
        }

        public static DateTime ConvertToDateTime(IDataRecord dataRecord, string columnName, DateTime defaultValue)
        {
            if (dataRecord != null)
                return ConvertToDateTime(GetObjectValueFromDataRecord(dataRecord, columnName), defaultValue);
            return defaultValue;
        }

        #endregion

        #region [ ConvertToDecimal ]
        /// <summary>
        /// Converts the objectValue decimal to decimal
        /// If the objectValue is DB Null, method will return 0 else returns the valid decimal value
        /// </summary>
        /// <param name="objectValue">Value to be converted as decimal</param>
        /// <returns>Valid decimal value or 0</returns>
        public static decimal ConvertToDecimal(object objectValue)
        {
            return ConvertToDecimal(objectValue, 0);
        }
        #endregion [ConvertToDecimal]

        #region [ ConvertToDecimal ]
        /// <summary>
        /// Converts the objectValue decimal to decimal
        /// If the objectValue is DB Null, method will return 0 else returns the valid decimal value
        /// </summary>
        /// <param name="objectValue">Value to be converted as decimal</param>
        /// <param name="defaultValue">Default value to be returned if the objectValue is DB null</param>
        /// <returns>Valid decimal value or defaultValue</returns>

        public static decimal ConvertToDecimal(object objectValue, decimal defaultValue)
        {
            // Declare the output value
            decimal decimalValue;
            //If the object value is DB Null,return default value
            if (Convert.IsDBNull(objectValue))
                return defaultValue;
            //Check whether the object value can be converted to a decimal and if it is, return the out decimal value
            if (decimal.TryParse(objectValue.ToString(), out decimalValue))
            {
                return decimalValue;
            }
            //If the object value cannot be converted to an decimal value return defaultValue
            return defaultValue;
        }
        #endregion [ ConvertToDecimal ]

        #region [ConvertToDecimal]
        /// <summary>
        /// Reads the value from data data record for the given column name and converts to Decimal.
        /// If the value is DB null or null, method will return o else return the valid Decimal Value
        /// </summary>
        /// <param name="dataRecord">IDataRecord</param>
        /// <param name="columnName">Name of the column in the dataRecord from which the value to be extracted</param>
        /// <returns>Valid decimal value or 0</returns>
        public static decimal ConvertToDecimal(IDataRecord dataRecord, string columnName)
        {
            return ConvertToDecimal(dataRecord, columnName, 0);
        }

        #endregion[ConvertToDecimal]

        #region [ConvertToDecimal]
        /// <summary>
        /// Reads the value from data data record for the given column name and converts to Decimal.
        /// If the value is DB null or null, method will return default value else return the valid Decimal Value
        /// </summary>
        /// <param name="dataRecord">IDataRecord</param>
        /// <param name="columnName">Name of the column in the dataRecord from which the value to be extracted</param>
        /// <returns>Valid decimal value or defaultValue</returns>
        public static decimal ConvertToDecimal(IDataRecord dataRecord, string columnName, decimal defaultValue)
        {
            //Check the datarecord for null
            if (dataRecord != null)
                return ConvertToDecimal(GetObjectValueFromDataRecord(dataRecord, columnName), defaultValue);
            //If column found convert to integer value
            return defaultValue;

        #endregion[ConvertToDecimal]
        }
    }
}