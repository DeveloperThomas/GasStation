using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        public int? LoyaltyPointsPrice { get; set; }
        [Required]
        public float Stock { get; set; }
        public bool Is_Fuel { get; set; }
        public List<Tank> Tanks { get; set; }
        public List<Discount> Discounts { get; set; }
        public ICollection<ProductsList> ProductsLists { get; set; }
    }
}
