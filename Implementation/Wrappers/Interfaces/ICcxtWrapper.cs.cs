using ccxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Wrappers.Interfaces
{
    public interface ICcxtWrapper
    {
        Task<double> GetBalanceAsync(string asset);
        Task<Order> CreateOrderAsync(string symbol, string type, string side, double amount, double? price = null);

    }
}
