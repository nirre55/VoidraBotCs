using Implementation.Utils;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Implementation.Tests.Utils
{
    public class ApiKeyStorageTests : IDisposable
    {
        private readonly ApiKeyStorage _storage;
        private readonly string _testPlatform = "TestPlatform";
        private readonly string _testFilePath;

        public ApiKeyStorageTests()
        {
            _storage = new ApiKeyStorage();
            _testFilePath = _storage.GetStorageFilePath(_testPlatform);
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Fact]
        public void GetStorageFilePath_ShouldCreateDirectoryAndReturnValidPath()
        {
            // Arrange & Act
            string path = _storage.GetStorageFilePath(_testPlatform);

            // Assert
            Assert.True(Directory.Exists(Path.GetDirectoryName(path)));
            Assert.EndsWith("credentials.enc", path);
        }

        [Fact]
        public void SaveAll_ShouldSaveEncryptedData()
        {
            // Arrange
            string apiKey = "testApiKey";
            string apiSecret = "testApiSecret";
            bool sandMode = true;

            // Act
            _storage.SaveAll(apiKey, apiSecret, sandMode, _testPlatform);

            // Assert
            Assert.True(File.Exists(_testFilePath));
            byte[] encryptedData = File.ReadAllBytes(_testFilePath);
            Assert.NotEmpty(encryptedData);
        }

        [Fact]
        public void LoadApiKey_ShouldReturnSavedApiKey()
        {
            // Arrange
            string expectedApiKey = "testApiKey";
            _storage.SaveAll(expectedApiKey, "secret", false, _testPlatform);

            // Act
            string? actualApiKey = _storage.LoadApiKey(_testPlatform);

            // Assert
            Assert.Equal(expectedApiKey, actualApiKey);
        }

        [Fact]
        public void LoadApiSecret_ShouldReturnSavedApiSecret()
        {
            // Arrange
            string expectedSecret = "testSecret";
            _storage.SaveAll("key", expectedSecret, false, _testPlatform);

            // Act
            string? actualSecret = _storage.LoadApiSecret(_testPlatform);

            // Assert
            Assert.Equal(expectedSecret, actualSecret);
        }

        [Fact]
        public void LoadSandMode_ShouldReturnSavedSandMode()
        {
            // Arrange
            bool expectedSandMode = true;
            _storage.SaveAll("key", "secret", expectedSandMode, _testPlatform);

            // Act
            bool actualSandMode = _storage.LoadSandMode(_testPlatform);

            // Assert
            Assert.Equal(expectedSandMode, actualSandMode);
        }

        [Fact]
        public void Load_WhenFileDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            string nonExistentPlatform = "NonExistentPlatform";

            // Act
            string? apiKey = _storage.LoadApiKey(nonExistentPlatform);
            string? apiSecret = _storage.LoadApiSecret(nonExistentPlatform);
            bool sandMode = _storage.LoadSandMode(nonExistentPlatform);

            // Assert
            Assert.Null(apiKey);
            Assert.Null(apiSecret);
            Assert.False(sandMode);
        }

        [Fact]
        public void Load_WhenFileIsCorrupted_ShouldReturnNull()
        {
            // Arrange
            File.WriteAllBytes(_testFilePath, new byte[] { 1, 2, 3, 4, 5 });

            // Act
            string? apiKey = _storage.LoadApiKey(_testPlatform);
            string? apiSecret = _storage.LoadApiSecret(_testPlatform);
            bool sandMode = _storage.LoadSandMode(_testPlatform);

            // Assert
            Assert.Null(apiKey);
            Assert.Null(apiSecret);
            Assert.False(sandMode);
        }
    }
}
