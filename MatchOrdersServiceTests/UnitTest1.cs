using CoreLibrary.Services;
using CoreLibrary.Models;

namespace MatchOrdersServiceTests
{
    public class UnitTest1
    {
        private readonly List<OrderBook> orderBooks =
        [
            new() {
                AcqTime = "Exchange1",
                Bids =
                [
                    new Bid { Order = new Order {Id = 1, Time = DateTime.Now, Type = "Buy", Kind = "Limit", Amount = 1, Price = 1000 } },
                    new Bid { Order = new Order {Id = 2, Time = DateTime.Now, Type = "Buy", Kind = "Limit", Amount = 0.5m, Price = 900 } }
                ],
                Asks =
                [
                    new Ask { Order = new Order {Id = 3, Time = DateTime.Now, Type = "Sell", Kind = "Limit", Amount = 1, Price = 1100 } },
                    new Ask { Order = new Order {Id = 4, Time = DateTime.Now, Type = "Sell", Kind = "Limit", Amount = 0.5m, Price = 1200 } }
                ]
            },
            new() {
                AcqTime = "Exchange2",
                Bids =
                [
                    new Bid { Order = new Order {Id = 5, Time = DateTime.Now, Type = "Buy", Kind = "Limit", Amount = 1.5m, Price = 950 } },
                    new Bid { Order = new Order {Id = 6, Time = DateTime.Now, Type = "Buy", Kind = "Limit", Amount = 1, Price = 920 } }
                ],
                Asks =
                [
                    new Ask { Order = new Order {Id = 7, Time = DateTime.Now, Type = "Sell", Kind = "Limit", Amount = 2, Price = 1150 } },
                    new Ask { Order = new Order {Id = 8, Time = DateTime.Now, Type = "Sell", Kind = "Limit", Amount = 1, Price = 1250 } }
                ]
            }
        ];

        private readonly List<CryptoExchange> exchangeBalances =
        [
            new() {
                Id = "Exchange1",
                EURBalance = 1650,
                BTCBalance = 2
            },
            new() {
                Id = "Exchange2",
                EURBalance = 1500,
                BTCBalance = 1
            }
        ];

        [Fact]
        public void TestBuyOrder()
        {
            var service = new MatchOrdersService();
            Result result = service.MatchOrders(exchangeBalances, orderBooks, true, 1.5m);

            Assert.Equal(2, result.BestExecutionPlan.Count);
            Assert.Equal("Exchange1", result.BestExecutionPlan[0].Exchange);
            Assert.Equal(1100, result.BestExecutionPlan[0].Order.Price);
            Assert.Equal(1, result.BestExecutionPlan[0].Order.Amount);

            Assert.Equal("Exchange2", result.BestExecutionPlan[1].Exchange);
            Assert.Equal(1150, result.BestExecutionPlan[1].Order.Price);
            Assert.Equal(0.5m, result.BestExecutionPlan[1].Order.Amount);

            Assert.Equal("Total for 1,5 BTC is: 1675,0 EUR", result.FinalResponse);
        }

        [Fact]
        public void TestSellOrder()
        {
            var service = new MatchOrdersService();
            Result result = service.MatchOrders(exchangeBalances, orderBooks, false, 1.5m);

            Assert.Equal(2, result.BestExecutionPlan.Count);
            Assert.Equal("Exchange1", result.BestExecutionPlan[0].Exchange);
            Assert.Equal(1000, result.BestExecutionPlan[0].Order.Price);
            Assert.Equal(1, result.BestExecutionPlan[0].Order.Amount);

            Assert.Equal("Exchange2", result.BestExecutionPlan[1].Exchange);
            Assert.Equal(950, result.BestExecutionPlan[1].Order.Price);
            Assert.Equal(0.5m, result.BestExecutionPlan[1].Order.Amount);

            Assert.Equal("Total for 1,5 BTC is: 1475,0 EUR", result.FinalResponse);
        }

        [Fact]
        public void TestSellOrder_LowBTCBalance()
        {
            var service = new MatchOrdersService();
            var result = service.MatchOrders(exchangeBalances, orderBooks, false, 4);

            Assert.Equal(3, result.BestExecutionPlan.Count);
            Assert.Equal("Exchange1", result.BestExecutionPlan[0].Exchange);
            Assert.Equal(1000, result.BestExecutionPlan[0].Order.Price);
            Assert.Equal(1, result.BestExecutionPlan[0].Order.Amount);

            Assert.Equal("Crypto exchange balance too low for remaining 1,5 BTC", result.FinalResponse);
            Assert.Equal(950, result.BestExecutionPlan[1].Order.Price);
            Assert.Equal(1, result.BestExecutionPlan[1].Order.Amount);
        }

        [Fact]
        public void TestBuyOrder_LowEURBalance()
        {
            var service = new MatchOrdersService();
            var result = service.MatchOrders(exchangeBalances, orderBooks, true, 3);

            Assert.Equal(3, result.BestExecutionPlan.Count);
            Assert.Equal("Exchange1", result.BestExecutionPlan[0].Exchange);
            Assert.Equal(1100, result.BestExecutionPlan[0].Order.Price);
            Assert.Equal(1, result.BestExecutionPlan[0].Order.Amount);

            Assert.Equal("Crypto exchange balance too low for remaining 0,2373188405797101449275362319 BTC", result.FinalResponse);
            Assert.Equal(1.3043478260869565217391304348M, result.BestExecutionPlan[1].Order.Amount);
            Assert.Equal(0.4583333333333333333333333333M, result.BestExecutionPlan[2].Order.Amount);
        }
    }
}