// See https://aka.ms/new-console-template for more information
using CoreLibrary.Data;
using CoreLibrary.Services;
using CoreLibrary.Models;
using System;

namespace BSDorderBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dr = new DataRepository("CryptoExchangesData.json", "OrderBooksData.json");

            IMatchOrdersService moService = new MatchOrdersService();

            List<string> response = moService.MatchOrders(dr.LoadCryptoExchanges(), dr.LoadOrderBooks(), true, 2);

            foreach (string responseItem in response)
            {
                Console.WriteLine(responseItem);
            }
        }
    }
}
