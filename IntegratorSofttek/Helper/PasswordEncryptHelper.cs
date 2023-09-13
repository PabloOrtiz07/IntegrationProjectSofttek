using System.Text;
using System.Security.Cryptography;

namespace IntegratorSofttek.Helper
{
    public static class PasswordEncryptHelper
    {
        public static string EncryptPassword(string password)
        {
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var stream = Array.Empty<byte>();
            var sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }
    }
}
