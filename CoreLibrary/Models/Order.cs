using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models
{
    public class Order
    {
        public int? Id { get; set; }
        public DateTime Time { get; set; }
        public required string Type { get; set; }
        public required string Kind { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}
