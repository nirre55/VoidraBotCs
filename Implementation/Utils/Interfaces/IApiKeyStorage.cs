namespace Implementation.Utils.Interfaces
{
    public interface IApiKeyStorage
    {
        void SaveApiKey(string key);
        void SaveApiSecret(string secret);
        string? LoadApiKey();
        string? LoadApiSecret();
    }
}
