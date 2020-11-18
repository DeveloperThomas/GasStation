using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class Distributor
    {
        [Key]
        public int DistributorId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public bool IsLocked { get; set; }
        [Required]
        public float Counter { get; set; }
        public List<Fueling> Fuelings { get; set; }
        public ICollection<TankDistributor> TankDistributors { get; set; }
    }
}
