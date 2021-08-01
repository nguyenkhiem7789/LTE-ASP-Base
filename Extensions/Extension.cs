using System;

namespace Extensions
{
    public static class Extension
    {
        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null) return defaultInt;
            if (!int.TryParse(item.ToString(), out var result))
                return defaultInt;
            return result;
        }

        public static int AsEnumToInt(this object item, int defaultInt = default(int))
        {
            if (item == null) return defaultInt;
            return (int) item;
        }

        public static long AsEnumToLong(this object item, long defaultLong = default(long))
        {
            if (item == null) return defaultLong;
            return (long) item;
        }

        public static long AsUnixTimeStamp(this DateTime item)
        {
            try
            {
                return (long) item.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            }
            catch (Exception)
            {
                return (long) DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            }
        }

        public static string AsString(this object item, string defaultString = default(string))
        {
            if (item == null || item.Equals(DBNull.Value)) return defaultString;
            var value = item.ToString().Trim();
            return string.IsNullOrEmpty(value) ? defaultString : value;
        }

        public static string AsEmpty(this object item)
        {
            return item == null ? string.Empty : item.ToString().Trim();
        }

        public static string AsDateView(this DateTime? item)
        {
            if(item.HasValue) return item.Value.ToString("dd/MM/yyyy");
            return string.Empty;
        }

        public static string AsDateView(this DateTime? item, string format)
        {
            if (item.HasValue) return item.Value.ToString(format);
            return string.Empty;
        }

        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        public static DateTime GetCurrentDateUtc()
        {
            return DateTime.Now;
        }
    }
}