using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSDorderBook.Models
{
    internal class CryptoExchange
    {
        public string Id { get; set; }
        public decimal EURBalance { get; set; }
        public decimal BTCBalance { get; set; }
    }
}
