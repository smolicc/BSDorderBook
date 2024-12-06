using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models
{
    public class OrderBook
    {
        public required string AcqTime { get; set; }
        public required List<Bid> Bids { get; set; }
        public required List<Ask> Asks { get; set; }
    }
}
