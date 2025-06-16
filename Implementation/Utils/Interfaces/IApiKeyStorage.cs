namespace Implementation.Utils.Interfaces
{
    public interface IApiKeyStorage
    {
        string? LoadApiKey();
        string? LoadApiSecret();
        bool LoadSandMode();
        void SaveAll(string apiKey, string apiSecret, bool sandMode);

    }
}
