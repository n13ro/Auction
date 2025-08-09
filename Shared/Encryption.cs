using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    class Encryption
    {
        public static string HashedValue(string value)
        {
            var hashedSha256 = SHA256.HashData(Encoding.UTF8.GetBytes(value));
            return Convert.ToString(hashedSha256);
        }
        

        public static bool IsEqualHashValues(string value1, string value2)
        {
            HashedValue(value1);
            return value1.Equals(value2);
        }
    }
}
