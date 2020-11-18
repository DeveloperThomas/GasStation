using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models
{
    public class LoyaltyCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int LoyaltyCardId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int BuildingNumber { get; set; }
        [Required]
        public int PostalCode { get; set; }
    }
}
