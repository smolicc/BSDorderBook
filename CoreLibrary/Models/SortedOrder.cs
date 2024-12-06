using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models
{
    public class SortedOrder
    {
        public required string Exchange { get; set; }
        public required Order Order { get; set; }
    }
}
