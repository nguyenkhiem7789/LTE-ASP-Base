using System;

namespace Common
{
    public class CommonUtility
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}