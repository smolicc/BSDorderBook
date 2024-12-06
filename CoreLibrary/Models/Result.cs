using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models
{
    public class Result
    {
        public required List<SortedOrder> BestExecutionPlan { get; set; }

        public required string FinalResponse { get; set; }
    }
}
