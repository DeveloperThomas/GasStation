using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class ReportSale
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Product> Products { get; set; }
    }
}
