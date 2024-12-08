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

    Result response = new MatchOrdersService().MatchOrders(cryptoExchanges, orderBooks, orderType, amount);

    foreach (SortedOrder result in response.BestExecutionPlan)
    {
        if (result.Order.Type == "Sell")
            Console.WriteLine("Buy " + result.Order.Amount + " BTC at " + result.Order.Price + " EUR from " + result.Exchange);
        else if (result.Order.Type == "Buy")
            Console.WriteLine("Sell " + result.Order.Amount + " BTC at " + result.Order.Price + " EUR from " + result.Exchange);
    }
    Console.WriteLine(response.FinalResponse);
}