// See https://aka.ms/new-console-template for more information
using CoreLibrary.Data;
using CoreLibrary.Services;
using CoreLibrary.Models;
using System;


Console.WriteLine("Hello World!");

Result response = new MatchOrdersService().MatchOrders(true, 2);

foreach (SortedOrder result in response.BestExecutionPlan)
{
    Console.WriteLine(result.Order.Amount + " BTC at " + result.Order.Price + " EUR from " + result.Exchange);
}
Console.WriteLine(response.FinalResponse);