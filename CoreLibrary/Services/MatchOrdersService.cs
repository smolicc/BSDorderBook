using CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Services
{
    public class MatchOrdersService : IMatchOrdersService
    {
        public List<string> MatchOrders(List<CryptoExchange> cryptoExchanges, List<OrderBook> orderBooks, bool orderType, decimal targetAmount)
        {
            List<string> finalOrders = new List<string>();
            decimal remainingAmount = targetAmount;
            decimal totalPrice = 0;

            if (orderType == true) //Buy
            {
                var sortedAsks = orderBooks
                    .SelectMany(book => book.Asks.Select(a => (Exchange: book.AcqTime, a.Order)))
                    .OrderBy(x => x.Order.Price)
                    .ToList();

                foreach (var ask in sortedAsks)
                {
                    if (remainingAmount == 0)
                    {
                        finalOrders.Add($"Total for {targetAmount} BTC is: {totalPrice} EUR");
                        break;
                    }

                    CryptoExchange exchangeBalance = cryptoExchanges.First(b => b.Id == ask.Exchange);

                    decimal maxBuyable = exchangeBalance.EURBalance / ask.Order.Price;
                    decimal amountToBuy = Math.Min(Math.Min(maxBuyable, ask.Order.Amount), remainingAmount);

                    if (amountToBuy > 0)
                    {
                        finalOrders.Add($"Buy {amountToBuy} BTC at {ask.Order.Price} EUR from {ask.Exchange}");
                        
                        exchangeBalance.EURBalance -= amountToBuy * ask.Order.Price;
                        totalPrice += amountToBuy * ask.Order.Price;
                        remainingAmount -= amountToBuy;
                    }
                }
            }
            else if (orderType == false) //Sell
            {

            }

            if (remainingAmount > 0)
            {

            }

            return finalOrders;
        }
    }
}
