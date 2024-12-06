// See https://aka.ms/new-console-template for more information
using CoreLibrary.Data;
using CoreLibrary.Services;
using CoreLibrary.Models;
using System;


Console.WriteLine("Hello World!");

DataRepository dr = new("CryptoExchangesData.json", "OrderBooksData.json");

List<CryptoExchange> cryptoExchanges = dr.LoadCryptoExchanges();
List<OrderBook> orderBooks = dr.LoadOrderBooks();

Result response = new MatchOrdersService().MatchOrders(cryptoExchanges, orderBooks, true, 2);

foreach (SortedOrder result in response.BestExecutionPlan)
{
    Console.WriteLine(result.Order.Amount + " BTC at " + result.Order.Price + " EUR from " + result.Exchange);
}
Console.WriteLine(response.FinalResponse);