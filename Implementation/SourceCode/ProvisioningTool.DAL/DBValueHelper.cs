using System;

namespace ProvisioningTool.DAL
{
    public static class DBValueHelper
    {
        static DBValueHelper() { }

        #region [ConvertToDBString]
        public static object ConvertToDBString(string value)
        {
            return ConvertToDBString(value, string.Empty);
        }
        #endregion [ConvertToDBString]

        #region [ConvertToDBString]
        public static object ConvertToDBString(string value, string defaultValue)
        {
            object retVal;
            retVal = !string.IsNullOrEmpty(value) && value.Trim().Length != 0 ? value.Trim() : (!string.IsNullOrEmpty(defaultValue)) ? defaultValue : Convert.DBNull;
            return retVal;
        }
        #endregion [ConvertToDBString]

        #region [ConvertToDBInteger]
        public static object ConvertToDBInteger(int value)
        {
            return ConvertToDBInteger(value, 0);
        }
        #endregion [ConvertToDBInteger]

        #region [ConvertToDBInteger]
        public static object ConvertToDBInteger(int value, int defaultValue)
        {
            object retVal;
            retVal = value != 0 ? value : (defaultValue != 0 ? defaultValue : Convert.DBNull);
            return retVal;
        }
        #endregion [ConvertToDBInteger]

        #region [ConvertToDBInteger]
        public static object ConvertToDBInteger(int value, int defaultValue, bool isNullable)
        {
            object retVal;
            if (isNullable)
                retVal = value != 0 ? value : (defaultValue != 0 ? defaultValue : Convert.DBNull);
            else
                retVal = value != 0 ? value : defaultValue;
            return retVal;
        }
        #endregion [ConvertToDBInteger]

        #region [ConvertToDBInt]
        public static object ConvertToDBInt(int value)
        {
            return ConvertToDBInteger(value, 0);
        }
        #endregion [ConvertToDBInt]

        #region [ConvertToDBBoolean]
        /// <summary>
        /// Converts the boolean value to DB boolean value
        /// </summary>
        /// <param name="value">Value to be converted as bool</param>
        /// <returns></returns>
        public static object ConvertToDBBoolean(bool value)
        {
            //No implementation is required, cos, value can be either true / false.
            //This method is included only for maintaining the uniformity
            return value;
        }
        #endregion [ConvertToDBBoolean]

        #region [ConvertToDBDate]
        public static object ConvertToDBDate(object value)
        {
            return ConvertToDBDate(value, DateTime.Now);
        }
        #endregion [ConvertToDBDate]

        #region [ConvertToDBDate]
        public static object ConvertToDBDate(object objectValue, DateTime defaultValue)
        {
            //object retVal;
            //retVal = !string.IsNullOrEmpty(value) && value.Trim().Length != 0 ? value.Trim() : (!string.IsNullOrEmpty(defaultValue)) ? defaultValue : Convert.DBNull;
            //return retVal;

            //Declare the output value
            DateTime DateTimeValue = new DateTime();

            //If te output value is Null, return default value
            if (objectValue == null)
                return defaultValue.ToString("dd/mm/yyyy");

            //Check the whether the object value can be converted to an DateTime, and if it is, returh the out DateTime value
            if (DateTime.TryParse(objectValue.ToString(), out DateTimeValue))
                return DateTimeValue;
            //If the object value cannot be converted to an DateTime value return defaultValue
            return defaultValue;
        }
        #endregion [ConvertToDBDate]

        #region [ConvertTODBDecimal]
        /// <summary>
        /// Converts the decimal value into DB decimal value.
        /// If the decimal value is 0, method will return the DB Null.
        /// </summary>
        /// <param name="value">Value to be converted as decimal</param>
        /// <returns>Valid decimal value or defaultValue</returns>
        public static object ConvertToDBDecimal(decimal value)
        {

            return ConvertToDBDecimal(value, 0);
        }
        #endregion [ConvertTODBDecimal]

        #region [ConvertTODBDecimal]
        /// <summary>
        /// Converts the decimal value into DB decial value.
        /// If the decimal value is 0, method will return the default value specified
        /// If the default value is also 0,methos will return DB Null.
        /// </summary>
        /// <param name="value">Value to be converted as decimal</param>
        /// <param name="defaultValue">Default value to be returned if decimal value is 0</param>
        /// <returns>Valid decimal value or defaultValue</returns>

        public static object ConvertToDBDecimal(decimal value, decimal defaultValue)
        {
            object retVal;
            retVal = value != 0 ? value : (defaultValue != 0 ? defaultValue : Convert.DBNull);
            return retVal;
        }
        #endregion [ConvertTODBDecimal]

        #region [ConvertTODBDecimal]
        /// <summary>
        /// Converts the decimal value into DB decial value.
        /// If the decimal value is 0, method will return the default value specified
        /// If the default value is also 0,methos will return DB Null.
        /// </summary>
        /// <param name="value">Value to be converted as decimal</param>
        /// <param name="defaultValue">Default value to be returned if decimal value is 0</param>
        /// <returns>Valid decimal value or defaultValue</returns>

        public static object ConvertToDBDecimal(decimal value, decimal defaultValue, bool isNullable)
        {
            object retVal;
            if (isNullable)
                retVal = value != 0 ? value : (defaultValue != 0 ? defaultValue : Convert.DBNull);
            else
                retVal = value != 0 ? value : defaultValue;
            return retVal;
        }
        #endregion [ConvertTODBDecimal]
    }
}
