// See https://aka.ms/new-console-template for more information
using CoreLibrary.Data;
using CoreLibrary.Services;
using CoreLibrary.Models;
using System;


Console.WriteLine("Hello World!");

var dr = new DataRepository("CryptoExchangesData.json", "OrderBooksData.json");

List<string> response = new MatchOrdersService().MatchOrders(dr.LoadCryptoExchanges(), dr.LoadOrderBooks(), true, 3);

foreach (string responseItem in response)
{
    Console.WriteLine(responseItem);
}