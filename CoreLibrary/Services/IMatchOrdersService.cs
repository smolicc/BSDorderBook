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
        Result MatchOrders(List<CryptoExchange> cryptoExchanges, List<OrderBook> orderBooks, string orderType, decimal targetAmount);
    }
}
