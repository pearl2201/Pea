using System;
using System.Collections.Generic;
using System.Text;

namespace Pea.Networking.Utils
{
    public static class SerializationExtensions
    {
        public static byte[] ToBytes(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string FromBytes(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
