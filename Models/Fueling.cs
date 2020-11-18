using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class Fueling
    {
        [Key]
        public int FuelingId { get; set; }
        public Distributor Distributor { get; set; }
        [ForeignKey("Distributor")]
        public int DistributorId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public float Amount { get; set; }
    }
}
