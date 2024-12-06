using CoreLibrary.Data;
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
        public Result MatchOrders(List<CryptoExchange> cryptoExchanges, List<OrderBook> orderBooks, bool orderType, decimal targetAmount)
        {
            Result result = new()
            {
                BestExecutionPlan = [],
                FinalResponse = ""
            };

            decimal remainingAmount = targetAmount;
            decimal totalPrice = 0;

            if (orderType == true) //Buy
            {
                List<SortedOrder> sortedAsks = [.. orderBooks
                    .SelectMany(book => book.Asks.Select(a => new SortedOrder
                    {
                        Exchange = book.AcqTime,
                        Order = a.Order
                    })) 
                    .OrderBy(x => x.Order.Price)];

                foreach (SortedOrder ask in sortedAsks)
                {
                    if (remainingAmount == 0)
                    {
                        result.FinalResponse = $"Total for {targetAmount} BTC is: {totalPrice} EUR";
                        break;
                    }

                    CryptoExchange exchangeBalance = cryptoExchanges.First(b => b.Id == ask.Exchange);

                    decimal maxBuyable = exchangeBalance.EURBalance / ask.Order.Price;
                    decimal amountToBuy = Math.Min(Math.Min(maxBuyable, ask.Order.Amount), remainingAmount);

                    if (amountToBuy > 0)
                    {
                        SortedOrder so = new()
                        {
                            Exchange = ask.Exchange,
                            Order = ask.Order
                        };

                        so.Order.Amount = amountToBuy;
                        result.BestExecutionPlan.Add(so);
                        
                        exchangeBalance.EURBalance -= amountToBuy * ask.Order.Price;
                        totalPrice += amountToBuy * ask.Order.Price;
                        remainingAmount -= amountToBuy;
                    }
                }
            }
            else if (orderType == false) //Sell
            {
                List<SortedOrder> sortedBids = [.. orderBooks
                    .SelectMany(book => book.Bids.Select(a => new SortedOrder
                    {
                        Exchange = book.AcqTime,
                        Order = a.Order
                    }))
                    .OrderByDescending(x => x.Order.Price)];

                foreach (var bid in sortedBids)
                {
                    if (remainingAmount == 0)
                    {
                        result.FinalResponse = $"Total for {targetAmount} BTC is: {totalPrice} EUR";
                        break;
                    }

                    CryptoExchange exchangeBalance = cryptoExchanges.First(b => b.Id == bid.Exchange);

                    decimal amountToSell = Math.Min(Math.Min(exchangeBalance.BTCBalance, bid.Order.Amount), remainingAmount);

                    if (amountToSell > 0)
                    {
                        SortedOrder so = new()
                        {
                            Exchange = bid.Exchange,
                            Order = bid.Order
                        };

                        so.Order.Amount = amountToSell;
                        result.BestExecutionPlan.Add(so);

                        exchangeBalance.BTCBalance -= amountToSell;
                        totalPrice += amountToSell * bid.Order.Price;
                        remainingAmount -= amountToSell;
                    }
                }
            }

            if (remainingAmount > 0)
            {
                result.FinalResponse = $"Crypto exchange balance too low for remaining {remainingAmount} BTC";
            }

            return result;
        }
    }
}
