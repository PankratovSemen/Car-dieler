using System.Security.Cryptography;
using System.Text;

namespace Car_DeailerShip_Vue.Server.Model.Encryption
{
    public static class EncryptionData
    {
        public static string Encrypt(string data)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
            
        }

       
    }
}
