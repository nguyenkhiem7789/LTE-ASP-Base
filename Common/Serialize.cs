using System;
using System.IO;
using ProtoBuf;

namespace Common
{
    public class Serialize
    {
        public static byte[] ProtoBufSerialize(object item)
        {
            if (item != null)
            {
                try
                {
                    var ms = new MemoryStream();
                    Serializer.Serialize(ms, item);
                    var rt = ms.ToArray();
                    return rt;
                }
                catch (ProtoBuf.ProtoException ex)
                {
                    throw new Exception("Unable to serialize object", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to serialize object", ex);
                }
            }
            else
            {
                throw new Exception("Object serialize is null");
            } 
        }

        public static T ProtoBufDeserialize<T>(byte[] byteArray)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                try
                {
                    var ms = new MemoryStream(byteArray);
                    return Serializer.Deserialize<T>(ms);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to deserialize object" + typeof(T).FullName, ex);
                }
            }
            else
            {
                throw new Exception("Object Deserialize is null or empty");
            }
        }
    }
}