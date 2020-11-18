using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public LoyaltyCard LoyaltyCard { get; set; }
        [ForeignKey("LoyaltyCard")]
        public int LoyaltyCardId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public ICollection<ProductsList> ProductsLists { get; set; }
        public Payment Payment { get; set; }
        public Invoice Invoice { get; set; }
    }
}
