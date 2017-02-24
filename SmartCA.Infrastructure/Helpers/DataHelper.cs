using System;
using SmartCA.Infrastructure.DomainBase;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Helpers
{
    /// <summary>
    /// Static helper class used by the factories when getting 
    /// data from ADO.NET objects (i.e. IDataReader)
    /// </summary>
    public static class DataHelper
    {
        private const string MinSqlDateValue = "1/1/1753";

        #region Static Data Helper Methods

        public static DateTime GetDateTime(object value)
        {
            DateTime dateValue = DateTime.MinValue;
            if ((value != null) && (value != DBNull.Value))
            {
                if ((DateTime)value > DateTime.Parse(DataHelper.MinSqlDateValue))
                {
                    dateValue = (DateTime)value;
                }
            }
            return dateValue;
        }

        public static DateTime? GetNullableDateTime(object value)
        {
            DateTime? dateTimeValue = null;
            DateTime dbDateTimeValue;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (DateTime.TryParse(value.ToString(), out dbDateTimeValue))
                {
                    dateTimeValue = dbDateTimeValue;
                }
            }
            return dateTimeValue;
        }

        public static int GetInteger(object value)
        {
            int integerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                int.TryParse(value.ToString(), out integerValue);
            }
            return integerValue;
        }

        public static int? GetNullableInteger(object value)
        {
            int? integerValue = null;
            int parseIntegerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (int.TryParse(value.ToString(), out parseIntegerValue))
                {
                    integerValue = parseIntegerValue;
                }
            }
            return integerValue;
        }

        public static decimal GetDecimal(object value)
        {
            decimal decimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                decimal.TryParse(value.ToString(), out decimalValue);
            }
            return decimalValue;
        }

        public static decimal? GetNullableDecimal(object value)
        {
            decimal? decimalValue = null;
            decimal parseDecimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (decimal.TryParse(value.ToString(), out parseDecimalValue))
                {
                    decimalValue = parseDecimalValue;
                }
            }
            return decimalValue;
        }

        public static double GetDouble(object value)
        {
            double doubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                double.TryParse(value.ToString(), out doubleValue);
            }
            return doubleValue;
        }

        public static double? GetNullableDouble(object value)
        {
            double? doubleValue = null;
            double parseDoubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (double.TryParse(value.ToString(), out parseDoubleValue))
                {
                    doubleValue = parseDoubleValue;
                }
            }

            return doubleValue;
        }

        public static Guid GetGuid(object value)
        {
            Guid guidValue = Guid.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch
                {
                    // really do nothing, because we want to return a value for the guid = Guid.Empty;
                }
            }
            return guidValue;
        }

        public static Guid? GetNullableGuid(object value)
        {
            Guid? guidValue = null;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch
                {
                    // really do nothing, because we want to return a value for the guid = null;
                }
            }
            return guidValue;
        }

        public static string GetString(object value)
        {
            string stringValue = string.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                stringValue = value.ToString().Trim();
            }
            return stringValue;
        }

        public static bool GetBoolean(object value)
        {
            bool bReturn = false;
            if (value != null && value != DBNull.Value)
            {
                bReturn = Convert.ToBoolean(value);
            }
            return bReturn;
        }

        public static bool? GetNullableBoolean(object value)
        {
            bool? bReturn = null;
            if (value != null && value != DBNull.Value)
            {
                bReturn = (bool)value;
            }

            return bReturn;
        }

        public static T GetEnumValue<T>(object databaseValue) where T : struct
        {
            T enumValue = default(T);

            if (databaseValue != null && databaseValue.ToString().Length > 0)
            {
                object parsedValue = Enum.Parse(typeof(T), databaseValue.ToString());
                if (parsedValue != null)
                {
                    enumValue = (T)parsedValue;
                }
            }

            return enumValue;
        }

        public static byte[] GetByteArrayValue(object value)
        {
            byte[] arrayValue = null;
            if (value != null && value != DBNull.Value)
            {
                arrayValue = (byte[])value;
            }
            return arrayValue;
        }

        public static string EntityListToDelimited<T>(IList<T> entities) where T : IEntity
        {
            StringBuilder builder = new StringBuilder(20);
            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append(entities[i].Key.ToString());
                }
            }
            return builder.ToString();
        }

        public static bool ReaderContainsColumnName(DataTable schemaTable, string columnName)
        {
            bool containsColumnName = false;
            foreach (DataRow row in schemaTable.Rows)
            {
                if (row["ColumnName"].ToString() == columnName)
                {
                    containsColumnName = true;
                    break;
                }
            }
            return containsColumnName;
        }

        public static object GetSqlValue(object value)
        {
            if (value != null)
            {
                if (value is Guid)
                {
                    return GetSqlValue((Guid)value);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(string value)
        {
            if (value != null)
            {
                return string.Format("N'{0}'", value.Replace("'", "''"));
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(DateTime value)
        {
            if (value != null)
            {
                if (value == DateTime.MinValue)
                {
                    return string.Format("'{0}'", 
                        DataHelper.MinSqlDateValue);
                }
                return string.Format("'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(DateTime? value)
        {
            if (value.HasValue)
            {
                return DataHelper.GetSqlValue(value.Value);
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(bool value)
        {
            return true ? "1" : "0";
        }

        public static object GetSqlValue(Guid value)
        {
            if (value != null)
            {
                return string.Format("'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(Enum value)
        {
            if (value != null)
            {
                return DataHelper.GetSqlValue(value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        #endregion
    }
}
