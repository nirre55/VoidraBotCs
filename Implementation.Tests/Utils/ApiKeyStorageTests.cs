using Implementation.Utils;


namespace Implementation.Tests.Utils
{
    public class ApiKeyStorageTests
    {
        [Fact]
        public void SaveAndLoadApiKey_ShouldReturnSameValue()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var storage = new ApiKeyStorage(tempDir);

            string testKey = "TestAPIKEY123";
            storage.SaveApiKey(testKey);

            string loadedKey = storage.LoadApiKey();
            Assert.Equal(testKey, loadedKey);
        }

        [Fact]
        public void SaveAndLoadSecretKey_ShouldReturnSameValue()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var storage = new ApiKeyStorage(tempDir);

            string testSecret = "SecretKey456";
            storage.SaveApiSecret(testSecret);

            string loadedSecret = storage.LoadApiSecret();
            Assert.Equal(testSecret, loadedSecret);
        }
    }
}
