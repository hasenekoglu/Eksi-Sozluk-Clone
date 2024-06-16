using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static string Encrpt(string password)
        {
            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hasBytes =md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hasBytes);
        }
    }
}
