// See https://aka.ms/new-console-template for more information
using BSDorderBook.Data;
using System;

namespace BSDorderBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dr = new DataRepository("CryptoExchangesData.json", "OrderBooksData.json");

        }
    }
}
