using CoreLibrary.Data;
using CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Services
{
    public class MatchOrdersService : IMatchOrdersService
    {
        public Result MatchOrders(List<CryptoExchange> cryptoExchanges, List<OrderBook> orderBooks, string orderType, decimal targetAmount)
        {
            Result result = new()
            {
                BestExecutionPlan = [],
                FinalResponse = ""
            };

            decimal remainingAmount = targetAmount;
            decimal totalPrice = 0;

            if (orderType == "buy") //Buy
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
                        result.FinalResponse = $"Total for {targetAmount} BTC is: {Math.Round(totalPrice, 2)} EUR";
                        break;
                    }

                    CryptoExchange exchangeBalance = cryptoExchanges.First(b => b.Id == ask.Exchange);

                    BigInteger exBalanceEUR = new BigInteger(exchangeBalance.EURBalance * 100_000_000m);
                    BigInteger OrderPrice = new BigInteger(ask.Order.Price * 100_000_000m);

                    BigInteger OrderAmount = new BigInteger(ask.Order.Amount * 100_000_000m);
                    BigInteger remainingAmountInSatoshis = new BigInteger(remainingAmount * 100_000_000m);

                    BigInteger maxBuyable = (exBalanceEUR * 100_000_000) / OrderPrice;
                    BigInteger amountToBuy = BigInteger.Min(BigInteger.Min(maxBuyable, OrderAmount), remainingAmountInSatoshis);

                    if(amountToBuy > 0)
                    {
                        decimal amountToBuyDecimal = (decimal)amountToBuy / 100_000_000m;

                        SortedOrder so = new()
                        {
                            Exchange = ask.Exchange,
                            Order = ask.Order
                        };

                        so.Order.Amount = amountToBuyDecimal;
                        result.BestExecutionPlan.Add(so);

                        exBalanceEUR -= (amountToBuy  * OrderPrice) / 100_000_000;
                        totalPrice += (decimal)(amountToBuy * OrderPrice) / (100_000_000m * 100_000_000m);
                        remainingAmountInSatoshis -= amountToBuy;

                        exchangeBalance.EURBalance = Math.Round((decimal)exBalanceEUR / 100_000_000m, 2);
                        remainingAmount = (decimal)remainingAmountInSatoshis / 100_000_000m;
                    }

                    if (cryptoExchanges.All(o => o.EURBalance == 0) && remainingAmount > 0)
                    {
                        result.FinalResponse = $"Crypto exchange EUR balance too low for remaining {remainingAmount} BTC";
                        break;
                    }
                }
            }
            else if (orderType == "sell") //Sell
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
                        result.FinalResponse = $"Total for {targetAmount} BTC is: {Math.Round(totalPrice, 2)} EUR";
                        break;
                    }

                    CryptoExchange exchangeBalance = cryptoExchanges.First(b => b.Id == bid.Exchange);

                    BigInteger exBalanceBTC = new BigInteger(exchangeBalance.BTCBalance * 100_000_000m);
                    BigInteger orderAmount = new BigInteger(bid.Order.Amount * 100_000_000m);
                    BigInteger orderPrice = new BigInteger(bid.Order.Price * 100_000_000m);
                    BigInteger remainingAmountInSatoshis = new BigInteger(remainingAmount * 100_000_000m);

                    BigInteger amountToSell = BigInteger.Min(BigInteger.Min(exBalanceBTC, orderAmount), remainingAmountInSatoshis);

                    if (amountToSell > 0)
                    {
                        decimal amountToSellDecimal = (decimal)amountToSell / 100_000_000m;

                        SortedOrder so = new()
                        {
                            Exchange = bid.Exchange,
                            Order = bid.Order
                        };

                        so.Order.Amount = amountToSellDecimal;
                        result.BestExecutionPlan.Add(so);

                        exchangeBalance.BTCBalance -= amountToSellDecimal;
                        totalPrice += (decimal)(amountToSell * orderPrice) / (100_000_000m * 100_000_000m);

                        remainingAmount = (decimal)(remainingAmountInSatoshis - amountToSell) / 100_000_000m;
                    }

                    if (cryptoExchanges.All(o => o.BTCBalance == 0) && remainingAmount > 0)
                    {
                        result.FinalResponse = $"Crypto exchange BTC balance too low for remaining {remainingAmount} BTC";
                        break;
                    }
                }
            }

            if (remainingAmount > 0 && result.FinalResponse == "")
            {
                result.FinalResponse = "Out of orders";
            }

            return result;
        }
    }
}
