using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class FuelingReport
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Fueling> Fuelings { get; set; }
    }
}
