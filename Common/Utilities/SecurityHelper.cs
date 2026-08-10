using PasswordGenerator;
using Spire.Doc.Documents.Rendering;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utilities
{
    public static class SecurityHelper
    {
        public static string GetSha256Hash(string input)
        {
            //using (var sha256 = new SHA256CryptoServiceProvider())
            using (var sha256 = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = sha256.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
                //return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
            }
        }

        public static string CreatePassword(byte len , bool complex = true)
        {
            Password password = new Password(complex , complex , true , false , len);
            return password.Next();
        }

    }
}
