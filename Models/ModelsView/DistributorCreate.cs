using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.ModelsView
{
    public class DistributorCreate
    {
        public Distributor Distributor { get; set; }
        public List<Tank> Tanks { get; set; }
    }
}
