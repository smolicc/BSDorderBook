// See https://aka.ms/new-console-template for more information
using CoreLibrary.Data;
using CoreLibrary.Services;
using CoreLibrary.Models;
using System;


Console.WriteLine("Hello World!");

DataRepository dr = new("CryptoExchangesData.json", "OrderBooksData.json");

while (true)
{
    string orderType;
    while (true)
    {
        Console.Write("Enter order type (buy/sell): ");
        orderType = Console.ReadLine()?.Trim().ToLower();

        if (orderType == "buy" || orderType == "sell")
            break;

        Console.WriteLine("Invalid order type. Please enter 'buy' or 'sell'.\n");
    }

    decimal amount;
    while (true)
    {
        Console.Write("Enter the amount of BTC: ");
        string amountInput = Console.ReadLine()?.Trim();

        if (decimal.TryParse(amountInput, out amount) && amount > 0)
            break;

        Console.WriteLine("Invalid amount. Please enter a positive number.\n");
    }


    List<CryptoExchange> cryptoExchanges = dr.LoadCryptoExchanges();
    List<OrderBook> orderBooks = dr.LoadOrderBooks();

    Result result = new MatchOrdersService().MatchOrders(cryptoExchanges, orderBooks, orderType, amount);

    foreach (SortedOrder order in result.BestExecutionPlan)
    {
        if (order.Order.Type == "Sell")
            Console.WriteLine("Buy " + order.Order.Amount + " BTC at " + order.Order.Price + " EUR from " + order.Exchange);
        else if (order.Order.Type == "Buy")
            Console.WriteLine("Sell " + order.Order.Amount + " BTC at " + order.Order.Price + " EUR from " + order.Exchange);
    }
    Console.WriteLine(result.FinalResponse);
}