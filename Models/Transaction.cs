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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TransactionId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public LoyaltyCard LoyaltyCard { get; set; }
        [ForeignKey("LoyaltyCard")]
        public int LoyaltyCardId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string PaymentType { get; set; }
        public bool? PaymentConfirmationRequested { get; set; }
        public ICollection<ProductsList> ProductsLists { get; set; }
        public Invoice Invoice { get; set; }
    }
}
