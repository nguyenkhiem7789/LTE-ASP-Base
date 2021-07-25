namespace Extensions
{
    public static class Extensions
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
        
        
    }
}