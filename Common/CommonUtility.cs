using System;

namespace Common
{
    public class CommonUtility
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string GenerateCodeFromId(long id, int padLeft = 1)
        {
            string c = "123456789qwertyuiopasdfghjklzxcvbnm".ToUpper();
            string code = string.Empty;
            int length = c.Length;
            long index = id;
            while (true)
            {
                int lastIndex = (int) (index % length) - 1;
                if (lastIndex < 0)
                {
                    lastIndex = c.Length - 1;
                }
                code = c[lastIndex] + code;
                if (index > length)
                {
                    index = index / length;
                    if (index < length)
                    {
                        code = c[(int) index - 1] + code;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            code = code.PadLeft(padLeft, '0');
            return code;
        }
    }
}