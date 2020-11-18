using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class Tank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TankId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public float Capacity { get; set; }
        [Required]
        public float Stock { get; set; }
        public ICollection<TankDistributor> TankDistributors { get; set; }
    }
}
