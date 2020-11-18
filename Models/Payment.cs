using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public Transaction Transaction { get; set; }
        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public string Type { get; set; }
        public bool? ConfirmationRequested { get; set; }
        [Required]
        public float Value { get; set; }
    }
}
