using FINANCE.TRACKER.Models.Login;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FINANCE.TRACKER.Models
{
    public class Helper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Helper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Encrypt(string plainText)
        {
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte key
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte IV

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] buffer = Convert.FromBase64String(cipherText);

            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        public int GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            try
            {
                var encryptedData = httpContext?.Session.GetString("UserSession");

                if (!string.IsNullOrEmpty(encryptedData))
                {
                    string decrypted = Decrypt(encryptedData);
                    var userData = JsonSerializer.Deserialize<UserDataModel>(decrypted);

                    return userData?.UserId ?? 0;
                }
                else
                {
                    throw new Exception("User session not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
