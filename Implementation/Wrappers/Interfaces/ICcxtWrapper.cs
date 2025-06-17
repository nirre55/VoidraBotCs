using ccxt;

namespace Implementation.Wrappers.Interfaces
{
    public interface ICcxtWrapper
    {
        Task<double> GetBalanceAsync(string asset);
        Task<Order> CreateOrderAsync(string symbol, string type, string side, double amount, double price);
    }
} 