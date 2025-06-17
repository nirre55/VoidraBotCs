namespace Implementation.Utils.Interfaces
{
    public interface IApiKeyStorage
    {
        string GetStorageFilePath(string platform);
        void SaveAll(string apiKey, string apiSecret, bool sandMode, string platform);
        string? LoadApiKey(string platform);
        string? LoadApiSecret(string platform);
        bool LoadSandMode(string platform);

    }
}
