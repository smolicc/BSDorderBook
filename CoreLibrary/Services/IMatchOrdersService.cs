using CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Services
{
    public interface IMatchOrdersService
    {
        List<string> MatchOrders(List<CryptoExchange> cryptoExchanges, List<OrderBook> orderBooks, bool orderType, decimal targetAmount);
    }
}
