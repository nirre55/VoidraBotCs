using Implementation.Utils.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Implementation.Utils
{
    public class ApiKeyStorage : IApiKeyStorage
    {
        private record CredentialData(string ApiKey, string ApiSecret, bool SandMode);

        public string GetStorageFilePath(string platform)
        {
            string appDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "VoidraApp",
                platform
            );
            Directory.CreateDirectory(appDir);
            return Path.Combine(appDir, "credentials.enc");
        }

        public void SaveAll(string apiKey, string apiSecret, bool sandMode, string platform)
        {
            var data = new CredentialData(apiKey, apiSecret, sandMode);
            string json = JsonSerializer.Serialize(data);
            SaveEncrypted(GetStorageFilePath(platform), json);
        }

        public string? LoadApiKey(string platform) => Load(platform)?.ApiKey;
        public string? LoadApiSecret(string platform) => Load(platform)?.ApiSecret;
        public bool LoadSandMode(string platform) => Load(platform)?.SandMode ?? false;

        private CredentialData? Load(string platform)
        {
            string? json = LoadEncrypted(GetStorageFilePath(platform));
            if (string.IsNullOrEmpty(json)) return null;
            try
            {
                return JsonSerializer.Deserialize<CredentialData>(json);
            }
            catch
            {
                return null;
            }
        }

        private void SaveEncrypted(string path, string plainText)
        {
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
