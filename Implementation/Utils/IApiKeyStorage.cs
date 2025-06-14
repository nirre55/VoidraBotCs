namespace Implementation.Utils
{
    public interface IApiKeyStorage
    {
        void SaveApiKey(string key);
        void SaveApiSecret(string secret);
        string? LoadApiKey();
        string? LoadApiSecret();
    }
}
