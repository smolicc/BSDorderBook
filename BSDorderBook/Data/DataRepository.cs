using BSDorderBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSDorderBook.Data
{
    internal class DataRepository
    {
        private readonly string filePathCE;
        private readonly string filePathOB;

        public DataRepository(string filePathCE, string filePathOB)
        {
            this.filePathCE = filePathCE;
            this.filePathOB = filePathOB;
        }

        public List<CryptoExchange> LoadCryptoExchanges()
        {
            using (StreamReader r = new StreamReader(filePathCE))
            {
                string json = r.ReadToEnd();
                List<CryptoExchange> items = JsonConvert.DeserializeObject<List<CryptoExchange>>(json) ?? new List<CryptoExchange>();

                return items;
            }
        }

        public List<OrderBook> LoadOrderBooks()
        {
            using (StreamReader r = new StreamReader(filePathOB))
            {
                string json = r.ReadToEnd();
                List<OrderBook> items = JsonConvert.DeserializeObject<List<OrderBook>>(json) ?? new List<OrderBook>();

                return items;
            }
        }
    }
}
