using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.ModelsView
{
    public class DistributorInTransaction : Distributor
    {
        public string NameOfFuel { get; set; }
        public decimal PriceForLiter { get; set; }
        public bool InTransaction { get; set; }
        public float Sum { get; set; }
        public int TankId { get; set; }
        public int DistributorId { get; set; }
    }
}
