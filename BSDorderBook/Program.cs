// See https://aka.ms/new-console-template for more information
using CoreLibrary.Data;
using CoreLibrary.Services;
using CoreLibrary.Models;
using System;


Console.WriteLine("Hello World!");

List<string> response = new MatchOrdersService().MatchOrders(false, 3);

foreach (string responseItem in response)
{
    Console.WriteLine(responseItem);
}