using CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Data
{
    public class DataRepository(string filePathCE, string filePathOB)
    {
        public List<CryptoExchange> LoadCryptoExchanges()
        {
            using StreamReader r = new(filePathCE);
            string json = r.ReadToEnd();
            List<CryptoExchange> items = JsonConvert.DeserializeObject<List<CryptoExchange>>(json) ?? [];

            return items;
        }

        public List<OrderBook> LoadOrderBooks()
        {
            using StreamReader r = new(filePathOB);
            string json = r.ReadToEnd();
            List<OrderBook> items = JsonConvert.DeserializeObject<List<OrderBook>>(json) ?? [];

            return items;
        }
    }
}
