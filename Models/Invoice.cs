using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class Invoice
    {
        public Transaction Transaction { get; set; }
        [Key]
        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        [Required]
        public int NIP { get; set; }
        public string RegistrationNumber { get; set; }
        [Required]
        public int InvoiceNumber { get; set; }
    }
}
