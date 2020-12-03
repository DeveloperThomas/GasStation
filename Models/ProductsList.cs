using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class ProductsList
    {
        public Product Product { get; set; }
        [ForeignKey("Product")]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        public Transaction Transaction { get; set; }
        [ForeignKey("Transaction")]
        [Column(Order = 2)]
        public int TransactionId { get; set; }
        [Required]
        public float Amount { get; set; }
        public decimal Price { get; set; }
    }
}
