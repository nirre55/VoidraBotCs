using Implementation.Utils;

namespace Implementation.Tests.Utils
{
    public class ApiKeyStorageTests : IDisposable
    {
        private readonly string _testPlatform = "TestPlatform_" + Guid.NewGuid();
        private readonly ApiKeyStorage _storage;
        private readonly string _appDir;

        public ApiKeyStorageTests()
        {
            _storage = new ApiKeyStorage(_testPlatform);
            _appDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "VoidraApp",
                _testPlatform
            );
        }

        [Fact]
        public void SaveAll_And_LoadValues_ShouldReturnCorrectData()
        {
            // Arrange
            string expectedKey = "TestApiKey";
            string expectedSecret = "TestApiSecret";
            bool expectedSandMode = true;

            // Act
            _storage.SaveAll(expectedKey, expectedSecret, expectedSandMode);

            // Assert
            Assert.Equal(expectedKey, _storage.LoadApiKey());
            Assert.Equal(expectedSecret, _storage.LoadApiSecret());
            Assert.True(_storage.LoadSandMode());
        }

        [Fact]
        public void Load_WhenFileMissing_ShouldReturnNullsAndFalse()
        {
            // Aucun appel à SaveAll, donc fichier inexistant
            Assert.Null(_storage.LoadApiKey());
            Assert.Null(_storage.LoadApiSecret());
            Assert.False(_storage.LoadSandMode());
        }

        [Fact]
        public void SaveAll_WithSandModeFalse_ShouldReturnFalse()
        {
            _storage.SaveAll("key", "secret", false);
            Assert.False(_storage.LoadSandMode());
        }

        public void Dispose()
        {
            if (Directory.Exists(_appDir))
                Directory.Delete(_appDir, true);
        }
    }
}
