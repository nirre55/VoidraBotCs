using Implementation.Utils.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Implementation.Utils
{
    public class ApiKeyStorage : IApiKeyStorage
    {
        private readonly string _filePath;

        private record CredentialData(string ApiKey, string ApiSecret, bool SandMode);

        public ApiKeyStorage(string platform)
        {
            string appDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "VoidraApp",
                platform
            );
            Directory.CreateDirectory(appDir);
            _filePath = Path.Combine(appDir, "credentials.enc");
        }

        public void SaveAll(string apiKey, string apiSecret, bool sandMode)
        {
            var data = new CredentialData(apiKey, apiSecret, sandMode);
            string json = JsonSerializer.Serialize(data);
            SaveEncrypted(_filePath, json);
        }

        public string? LoadApiKey() => Load()?.ApiKey;
        public string? LoadApiSecret() => Load()?.ApiSecret;
        public bool LoadSandMode() => Load()?.SandMode ?? false;

        private CredentialData? Load()
        {
            string? json = LoadEncrypted(_filePath);
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
