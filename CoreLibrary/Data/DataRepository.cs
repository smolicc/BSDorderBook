using CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Data
{
    public class DataRepository(string ceFileName, string obFileName)
    {
        public List<CryptoExchange> LoadCryptoExchanges()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = $"{assembly.GetName().Name}.Resources.{ceFileName}";

            using var stream = assembly.GetManifestResourceStream(resourcePath);
            using var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            List<CryptoExchange> items = JsonConvert.DeserializeObject<List<CryptoExchange>>(json) ?? [];

            return items;
        }

        public List<OrderBook> LoadOrderBooks()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = $"{assembly.GetName().Name}.Resources.{obFileName}";

            using var stream = assembly.GetManifestResourceStream(resourcePath);
            using var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            List<OrderBook> items = JsonConvert.DeserializeObject<List<OrderBook>>(json) ?? [];

            return items;
        }
    }
}
