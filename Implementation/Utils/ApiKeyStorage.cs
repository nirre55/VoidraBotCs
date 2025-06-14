using System.Security.Cryptography;
using System.Text;

namespace Implementation.Utils
{
    public class ApiKeyStorage : IApiKeyStorage
    {
        private readonly string _appDir;

        public ApiKeyStorage(string? appDir = null)
        {
            _appDir = appDir ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "VoidraApp"
            );
        }

        private string ApiKeyPath => Path.Combine(_appDir, "api.key");
        private string ApiSecretPath => Path.Combine(_appDir, "api.secret");

        public void SaveApiKey(string key) => SaveEncrypted(ApiKeyPath, key);
        public void SaveApiSecret(string secret) => SaveEncrypted(ApiSecretPath, secret);
        public string? LoadApiKey() => LoadEncrypted(ApiKeyPath);
        public string? LoadApiSecret() => LoadEncrypted(ApiSecretPath);

        private void SaveEncrypted(string path, string plainText)
        {
            Directory.CreateDirectory(_appDir);
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(path, encrypted);
        }

        private string? LoadEncrypted(string path)
        {
            if (!File.Exists(path))
                return null;

            try
            {
                byte[] encrypted = File.ReadAllBytes(path);
                byte[] decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch
            {
                return null;
            }
        }
    }
}