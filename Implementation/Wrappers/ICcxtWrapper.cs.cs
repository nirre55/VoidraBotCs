using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Wrappers
{
    public interface ICcxtWrapper
    {
        Task<double> GetBalanceAsync(string asset);
    }
}
