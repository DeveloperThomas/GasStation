using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class TankDistributor
    {
        public Tank Tank { get; set; }
        [ForeignKey("Tank")]
        [Column(Order = 1)]
        public int TankId { get; set; }
        public Distributor Distributor { get; set; }
        [ForeignKey("Distributor")]
        [Column(Order = 2)]
        public int DistributorId { get; set; }
    }
}
